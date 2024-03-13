using Newtonsoft.Json;
#pragma warning disable 8618

namespace SchnaeppchenJaeger
{
    public class DTO
    {
        public class Advertiser
        {
            [JsonProperty("uniqueName")]
            public string uniqueName { get; set; }

            [JsonProperty("indexOffer")]
            public bool indexOffer { get; set; }

            [JsonProperty("indexLeaflet")]
            public bool indexLeaflet { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("industryId")]
            public int industryId { get; set; }
        }

        public class Brand
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("resultsCount")]
            public int resultsCount { get; set; }
        }

        public class Brand2
        {
            [JsonProperty("uniqueName")]
            public string uniqueName { get; set; }

            [JsonProperty("indexOffer")]
            public bool indexOffer { get; set; }

            [JsonProperty("indexLeaflet")]
            public bool indexLeaflet { get; set; }

            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("industryId")]
            public int industryId { get; set; }
        }

        public class Category
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("resultsCount")]
            public int resultsCount { get; set; }

            [JsonProperty("uniqueName")]
            public object uniqueName { get; set; }

            [JsonProperty("indexOffer")]
            public bool indexOffer { get; set; }

            [JsonProperty("indexLeaflet")]
            public bool indexLeaflet { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("parentId")]
            public int parentId { get; set; }
        }

        public class ExternalTracking
        {
            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("url")]
            public string url { get; set; }

            [JsonProperty("ugpctfet")]
            public int ugpctfet { get; set; }
        }

        public class Filters
        {
            [JsonProperty("retailers")]
            public List<Retailer> retailers { get; set; }

            [JsonProperty("brands")]
            public List<Brand> brands { get; set; }

            [JsonProperty("categories")]
            public List<Category> categories { get; set; }
        }

        public class Images
        {
            [JsonProperty("count")]
            public int count { get; set; }

            [JsonProperty("isCdnSuccessfullyPurged")]
            public object isCdnSuccessfullyPurged { get; set; }

            [JsonProperty("metadata")]
            public List<Metadata> metadata { get; set; }
        }

        public class Industry
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }
        }

        public class Metadata
        {
            [JsonProperty("aspectRatio")]
            public double aspectRatio { get; set; }

            [JsonProperty("width")]
            public double width { get; set; }

            [JsonProperty("height")]
            public double height { get; set; }
        }

        public class Product
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }
        }

        public class Result
        {
            [JsonProperty("brand")]
            public Brand brand { get; set; }

            [JsonProperty("advertisers")]
            public List<Advertiser> advertisers { get; set; }

            [JsonProperty("categories")]
            public List<Category> categories { get; set; }

            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("description")]
            public string description { get; set; }

            [JsonProperty("price")]
            public double price { get; set; }

            [JsonProperty("oldPrice")]
            public double oldPrice { get; set; }

            [JsonProperty("referencePrice")]
            public double referencePrice { get; set; }

            [JsonProperty("requiresLoyalityMembership")]
            public bool requiresLoyalityMembership { get; set; }

            [JsonProperty("leafletFlightId")]
            public int leafletFlightId { get; set; }

            [JsonProperty("validityDates")]
            public List<ValidityDate> validityDates { get; set; }

            [JsonProperty("externalId")]
            public object externalId { get; set; }

            [JsonProperty("externalTrackingUrls")]
            public List<string> externalTrackingUrls { get; set; }

            [JsonProperty("externalTrackings")]
            public List<ExternalTracking> externalTrackings { get; set; }

            [JsonProperty("externalUrl")]
            public string externalUrl { get; set; }

            [JsonProperty("trackFlightImpression")]
            public object trackFlightImpression { get; set; }

            [JsonProperty("type")]
            public string type { get; set; }

            [JsonProperty("industries")]
            public List<Industry> industries { get; set; }

            [JsonProperty("product")]
            public Product product { get; set; }

            [JsonProperty("unit")]
            public Unit unit { get; set; }

            [JsonProperty("images")]
            public Images images { get; set; }

            [JsonProperty("imageType")]
            public string imageType { get; set; }
        }

        public class Retailer
        {
            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("resultsCount")]
            public int resultsCount { get; set; }
        }

        public class Root
        {
            [JsonProperty("filters")]
            public Filters filters { get; set; }

            [JsonProperty("totalResults")]
            public int totalResults { get; set; }

            [JsonProperty("skippedResults")]
            public int skippedResults { get; set; }

            [JsonProperty("results")]
            public List<Result> results { get; set; }
        }

        public class Unit
        {
            [JsonProperty("shortName")]
            public string shortName { get; set; }

            [JsonProperty("id")]
            public int id { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }
        }

        public class ValidityDate
        {
            [JsonProperty("from")]
            public DateTime from { get; set; }

            [JsonProperty("to")]
            public DateTime to { get; set; }
        }
    }
}