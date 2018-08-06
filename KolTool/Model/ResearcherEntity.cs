using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KolTool.Model
{
    [DataContract]
    public class ResearcherEntity : LinkField, IJanusBaseEntity
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
        public string academic_ability { get; set; }


        [DataMember]
        public string[] academic_title { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string[] admin_title { get; set; }

        [DataMember]
        public string[] professional_title { get; set; }

        [DataMember]
        public string[] department_ids { get; set; }

        [DataMember]
        public string[] type_list { get; set; }

        [DataMember]
        public ResumeEntity resume { get; set; }

        [DataMember]
        public List<HcpOrg> institution_list { get; set; }


        [DataMember]
        public string institution_title { get; set; }

        [DataMember]
        public List<SocityTitle> associations { get; set; }

        [DataMember]
        public List<Special> specialty_list { get; set; }

        [DataMember]
        public string paper_sum_if { get; set; }

        [DataMember]
        public string paper_3_year_count { get; set; }

        [DataMember]
        public string paper_avg_if { get; set; }

        [DataMember]
        public string paper_max_if { get; set; }

        [DataMember]
        public string ct_multi_country_count { get; set; }

        [DataMember]
        public string ct_pi_count { get; set; }

        [DataMember]
        public string paper_corauthor_count { get; set; }


        [DataMember]
        public string Prefecture { get; set; }

        [DataMember]
        public int paper_first_count { get; set; }

        [DataMember]
        public int paper_key_count { get; set; }

    }

    public class HcpOrg
    {
        public string[] association_title { get; set; }
        public string institution_id { get; set; }
        public string raw_institution_name { get; set; }
    }

    [DataContract]
    public class Special
    {
        [DataMember]
        public string raw_specialty_name { get; set; }
    }

    public class SocityTitle
    {
        public List<string> association_title { get; set; }
        public string institution_id { get; set; }
        public string summit_no { get; set; }

    }

    public class ResumeEntity
    {
        public string resume { get; set; }
        public string update_time { get; set; }
    }
}
