using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using KolTool.Model;

namespace GBI.Metrix.Model.Entity
{
    [DataContract]
    public class InstitutionEntity : IJanusBaseEntity
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
        public string address_cn { get; set; }


        [DataMember]
        public string address_en { get; set; }


        [DataMember]
        public string website { get; set; }

        [DataMember]
        public string level { get; set; }

        [DataMember]
        public string rank { get; set; }


        [DataMember]
        public string geography_info_province { get; set; }

        [DataMember]
        public string geography_info_city { get; set; }

        [DataMember]
        public string[] type { get; set; }


        [DataMember]
        public int hcp_count { get; set; }

        [DataMember]
        public string paper_if_5 { get; set; }

        [DataMember]
        public bool is_gcp { get; set; }


        [DataMember]
        public string[] fd_ids { get; set; }

        [DataMember]
        public string[] gcp_ids { get; set; }

        [DataMember]
        public string fd_department { get; set; }

        [DataMember]
        public string latitude { get; set; }

        [DataMember]
        public string longitude { get; set; }
    }
}
