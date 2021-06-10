using System.Collections.Generic;
using Newtonsoft.Json;

namespace Totvs.Sample.Shop.Dto.BulkResponse {
    public class BulkResponseItemDto {
        public int status { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public string href { get; set; }
        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public string code { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public string message { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public string detailedMessage { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public string helpUrl { get; set; }

        [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<BulkResponseItemDto> details { get; set; }
    }
}