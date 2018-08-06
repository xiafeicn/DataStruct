using System;
using System.Collections.Generic;
using GBI.Metrix.Model;
using Newtonsoft.Json;

namespace KolTool.Model
{
    public class SearchPamarater
    {
        public List<KeyWordItem> listKeyWords { get; set; }

        public List<FilterTag> filter { get; set; }

        public Dictionary<string, string> urlParam { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public bool is_detail_page { get; set; }

        public string order { get; set; }

        [JsonIgnore]
        public string language { get; set; }

        [JsonIgnore]
        public int page { get; set; }

        [JsonIgnore]
        public int startRow { get; set; }

        [JsonIgnore]
        public int limit { get; set; }

        [JsonIgnore]
        public List<KeyValuePair<string, bool>> Columns { get; set; }

    }


    public class ExportField
    {
        public string sortName { get; set; }
        public string searchTags { get; set; }
        public string tags { get; set; }
        public string advtags { get; set; }
    }

    public class KolPattern
    {
        public bool IsDefault { get; set; }

        public List<int> listHcpIds { get; set; }

        public string Dimension { get; set; }

        public string Year { get; set; }

        public string weight { get; set; }

        public string CheckedHcp { get; set; }

        public KolPattern()
        {
            listHcpIds = new List<int>(new int[] { 232205, 806917, 792025, 267532, 255259, 252074, 267524, 252073, 286922 });
            Dimension = "kol3";
            Year = "all";
            IsDefault = true;
        }
    }
}
