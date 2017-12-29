// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using SharpCaster.Models;
//
//    var data = ChromeCastModel.FromJson(jsonString);

namespace SharpCaster.Models
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class ChromeCastModel
    {
        [JsonProperty("bssid")]
        public string Bssid { get; set; }

        [JsonProperty("build_version")]
        public string BuildVersion { get; set; }

        [JsonProperty("cast_build_revision")]
        public string CastBuildRevision { get; set; }

        [JsonProperty("closed_caption")]
        public ClosedCaption ClosedCaption { get; set; }

        [JsonProperty("connected")]
        public bool Connected { get; set; }

        [JsonProperty("detail")]
        public Detail Detail { get; set; }

        [JsonProperty("ethernet_connected")]
        public bool EthernetConnected { get; set; }

        [JsonProperty("has_update")]
        public bool HasUpdate { get; set; }

        [JsonProperty("hotspot_bssid")]
        public string HotspotBssid { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("mac_address")]
        public string MacAddress { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("noise_level")]
        public long NoiseLevel { get; set; }

        [JsonProperty("opencast_pin_code")]
        public string OpencastPinCode { get; set; }

        [JsonProperty("opt_in")]
        public OptIn OptIn { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("release_track")]
        public string ReleaseTrack { get; set; }

        [JsonProperty("setup_state")]
        public long SetupState { get; set; }

        [JsonProperty("setup_stats")]
        public SetupStats SetupStats { get; set; }

        [JsonProperty("signal_level")]
        public long SignalLevel { get; set; }

        [JsonProperty("ssdp_udn")]
        public string SsdpUdn { get; set; }

        [JsonProperty("ssid")]
        public string Ssid { get; set; }

        [JsonProperty("time_format")]
        public long TimeFormat { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("tos_accepted")]
        public bool TosAccepted { get; set; }

        [JsonProperty("uma_client_id")]
        public string UmaClientId { get; set; }

        [JsonProperty("uptime")]
        public double Uptime { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("wpa_configured")]
        public bool WpaConfigured { get; set; }

        [JsonProperty("wpa_id")]
        public long WpaId { get; set; }

        [JsonProperty("wpa_state")]
        public long WpaState { get; set; }
    }

    public partial class SetupStats
    {
        [JsonProperty("historically_succeeded")]
        public bool HistoricallySucceeded { get; set; }

        [JsonProperty("num_check_connectivity")]
        public long NumCheckConnectivity { get; set; }

        [JsonProperty("num_connect_wifi")]
        public long NumConnectWifi { get; set; }

        [JsonProperty("num_connected_wifi_not_saved")]
        public long NumConnectedWifiNotSaved { get; set; }

        [JsonProperty("num_initial_eureka_info")]
        public long NumInitialEurekaInfo { get; set; }

        [JsonProperty("num_obtain_ip")]
        public long NumObtainIp { get; set; }
    }

    public partial class OptIn
    {
        [JsonProperty("crash")]
        public bool Crash { get; set; }

        [JsonProperty("location")]
        public bool Location { get; set; }

        [JsonProperty("opencast")]
        public bool Opencast { get; set; }

        [JsonProperty("stats")]
        public bool Stats { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }
    }

    public partial class Detail
    {
        [JsonProperty("icon_list")]
        public IconList[] IconList { get; set; }

        [JsonProperty("locale")]
        public Locale Locale { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model_name")]
        public string ModelName { get; set; }

        [JsonProperty("timezone")]
        public Timezone Timezone { get; set; }
    }

    public partial class Timezone
    {
        [JsonProperty("display_string")]
        public string DisplayString { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }
    }

    public partial class Locale
    {
        [JsonProperty("display_string")]
        public string DisplayString { get; set; }
    }

    public partial class IconList
    {
        [JsonProperty("depth")]
        public long Depth { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class ClosedCaption
    {
    }

    public partial class ChromeCastModel
    {
        public static ChromeCastModel FromJson(string json) => JsonConvert.DeserializeObject<ChromeCastModel>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ChromeCastModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
