using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KolTool.Model
{
    [DataContract]
    public class DiseaseEntity : IJanusBaseEntity
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public ENCN title { get; set; }

        [DataMember]
        public string name_en { get; set; }

        [DataMember]
        public string name_cn { get; set; }

        [DataMember]
        public int paper_count { get; set; }

        [DataMember]
        public int grant_count { get; set; }

        [DataMember]
        public int clinicaltrial_count { get; set; }

        [DataMember]
        public int ct_static_count { get; set; }
        [DataMember]
        public int grant_static_count { get; set; }
        [DataMember]
        public int paper_static_count { get; set; }
        [DataMember]
        public int paper_count_disease { get; set; }
        [DataMember]
        public int grant_count_disease { get; set; }
        [DataMember]
        public int clinicaltrial_count_disease { get; set; }
        [DataMember]
        public List<int> level { get; set; }
    }
}
