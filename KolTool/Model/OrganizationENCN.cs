using Newtonsoft.Json;

namespace KolTool.Model
{
    public class OrganizationENCN
    {
        [JsonProperty("organization_id")]
        public string OrganizationId { get; set; }

        [JsonProperty("organization_name_en")]
        public string OrganizationEN { get; set; }

        [JsonProperty("organization_name_cn")]
        public string OrganizationCN { get; set; }
    }
}
