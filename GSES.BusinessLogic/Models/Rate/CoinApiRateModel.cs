using Newtonsoft.Json;

namespace GSES.BusinessLogic.Models.Rate
{
    [JsonObject]
    public class CoinApiRateModel : BaseRateModel
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("asset_id_base")]
        public string From { get; set; }

        [JsonProperty("asset_id_quote")]
        public string To { get; set; }
    }
}
