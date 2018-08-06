using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GBI.Metrix.Model
{
    [DataContract]
    public class SaveSearch
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SaveName { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int ModuleId { get; set; }
        [DataMember]
        public string SearchFilter { get; set; }

        [DataMember]
        public Nullable<int> ResultCount { get; set; }
        [DataMember]
        public Nullable<DateTime> SaveTime { get; set; }
        [DataMember]
        public Nullable<DateTime> ModifyTime { get; set; }
        [DataMember]
        public bool? IsAble { get; set; }
    }

    public enum SearchKeywordTypeEnum
    {
        hcp_tag = 1,
        organization_tag = 2,
        compound_tag = 3,
        disease_tag = 4,
        target_tag = 5,
        drug_tag = 6,

        name_tag = 0,
        content_tag = -1,
        deptname_tag = -2,
    }


    public class KeyWordItem
    {
        /// <summary>
        /// 搜索类型 ("hcp_tag", "organization_tag", "compound_tag", "disease_tag", "target_tag","content","name")
        /// </summary>
        public string SearchType { get; set; }

        /// <summary>
        /// 搜索的对象Id
        /// </summary>
        public string SearchId { get; set; }
        /// <summary>
        /// 搜索的对象值
        /// </summary>
        public string SearchValue { get; set; }

        public string id { get; set; }

        public override string ToString()
        {
            return string.Concat(SearchType, SearchId, SearchValue);
        }
    }

    public class FilterTag
    {
        public string requestkey { get; set; }
        public string requestvalue { get; set; }
        public string name { get; set; }
        public string category { get; set; }
    }
}
