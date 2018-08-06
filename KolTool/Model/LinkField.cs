using System.Runtime.Serialization;

namespace KolTool.Model
{
    [DataContract]
    public class LinkField
    {
        [DataMember]
        public string[] link_pi { get; set; }

        [DataMember]
        public string[] pi_cn { get; set; }

        [DataMember]
        public string[] pi_en { get; set; }

        [DataMember]
        public string[] disease_cn { get; set; }

        [DataMember]
        public string[] disease_en { get; set; }


        [DataMember]

        public string[] link_disease { get; set; }

        [DataMember]
        public string[] link_compound { get; set; }

        [DataMember]
        public string[] compound_en { get; set; }
        [DataMember]
        public string[] compound_cn { get; set; }

        [DataMember]
        public string[] link_target { get; set; }

        [DataMember]
        public string[] target_en { get; set; }
        [DataMember]
        public string[] target_cn { get; set; }

        [DataMember]

        public string[] link_location { get; set; }
        [DataMember]

        public string[] link_hcp { get; set; }
        [DataMember]
        public string[] hcp_cn { get; set; }
        [DataMember]
        public string[] hcp_en { get; set; }
        [DataMember]
        public string[] drug_name_cns { get; set; }
        [DataMember]
        public string[] drug_name_ens { get; set; }
        [DataMember]
        public string[] link_drug { get; set; }
        [DataMember]

        public string[] link_organization { get; set; }
        [DataMember]
        public string[] organization_cn { get; set; }
        [DataMember]
        public string[] organization_en { get; set; }
    }
}
