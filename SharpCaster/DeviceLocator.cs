using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Rssdp;
using Rssdp.Infrastructure;
using SharpCaster.Annotations;
using SharpCaster.Models;
using Zeroconf;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace SharpCaster
{
    public class DeviceLocator : INotifyPropertyChanged
    {
        public ObservableCollection<Chromecast> DiscoveredDevices { get; set; }

        public DeviceLocator()
        {
            DiscoveredDevices = new ObservableCollection<Chromecast>();
        }

        public async Task<ObservableCollection<Chromecast>> LocateDevicesAsync()
        {
            return await LocateDevicesAsync(new SsdpDeviceLocator());
        }

        public async Task<ObservableCollection<Chromecast>> LocateDevicesAsync(string localIpAdress)
        {
            return await LocateDevicesAsync(new SsdpDeviceLocator(new SsdpCommunicationsServer(new SocketFactory(localIpAdress))));
        }

        private async Task<ObservableCollection<Chromecast>> LocateDevicesAsync(SsdpDeviceLocator deviceLocator)
        {
            using (deviceLocator)
            {
                var deviceType = "urn:dial-multiscreen-org:device:dial:1";
                //var deviceType = "ssdp:all";
                deviceLocator.NotificationFilter = deviceType;
                var foundDevices = await deviceLocator.SearchAsync(deviceType, TimeSpan.FromMilliseconds(5000));

                foreach (var foundDevice in foundDevices)
                {
                    var fullDevice = await foundDevice.GetDeviceInfo();
                    Uri myUri;
                    Uri.TryCreate("https://" + foundDevice.DescriptionLocation.Host, UriKind.Absolute, out myUri);
                    var chromecast = new Chromecast
                    {
                        DeviceUri = myUri,
                        FriendlyName = fullDevice.FriendlyName,
                        Port = 8009
                    };
                    DiscoveredDevices.Add(chromecast);

                }
            }


            var chromecasts = await ZeroconfResolver.ResolveAsync("_googlecast._tcp.local.");
            foreach (var foundDevice in chromecasts)
            {
                Boolean isGroup = false;
                string groupName = "";
                Int32 groupPort = 0;
                foreach (var s in foundDevice.Services.Keys)
                {
                    IService currentService = foundDevice.Services[s];
                    groupPort = currentService.Port;
                    foreach (var p in currentService.Properties) 
                    {
                        foreach (var k in p.Keys)
                        {
                            if (k == "fn")
                            {
                                groupName = p[k];
                            }
                            if (k == "md")
                            {
                                if (p[k]  == "Google Cast Group") {
                                    isGroup = true;
                                }
                            }
                        }

                    }
                }
                if (isGroup)
                {
                    Uri groupURI = new Uri("https://" + foundDevice.IPAddress, UriKind.Absolute);
                    var groupcast = new Chromecast
                    {
                        DeviceUri = groupURI,
                        FriendlyName = groupName,
                        Port = groupPort
                    };
                    DiscoveredDevices.Add(groupcast);
                }
                Uri myUri;
                if (Uri.TryCreate("https://" + foundDevice.IPAddress, UriKind.Absolute, out myUri) == true)
                {
                    Boolean add = true;
                    if (isGroup == false)
                    {
                        var result = DiscoveredDevices.Where(x => x.DeviceUri.Host == myUri.Host).FirstOrDefault();
                        if (result != null)
                        {
                            add = false;
                        }

                    }
                    if (add)
                    {
                        using (HttpClient wc = new HttpClient())
                        {
                            try
                            {
                                var data = await wc.GetStringAsync("http://" + foundDevice.IPAddress + ":8008/setup/eureka_info?options=detail");
                                var results = JsonConvert.DeserializeObject<ChromeCastModel>(data);
                                var chromecast = new Chromecast
                                {
                                    DeviceUri = myUri,
                                    FriendlyName = results.Name,
                                    Port = 8009
                                };
                                DiscoveredDevices.Add(chromecast);
                            }
                            catch (Exception e)
                            {
                                System.Diagnostics.Debug.WriteLine(e);
                            }
                        }
                    }

                }

            }


            return DiscoveredDevices;
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}