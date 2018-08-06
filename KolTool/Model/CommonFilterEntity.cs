using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KolTool.Model
{
    public class CommonFilterEntity
    {
        #region ct

        [DataMember]
        public List<MiscCountEntity> completion_year { get; set; }
        [DataMember]
        public List<MiscCountEntity> country_type_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> gender_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> phase_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> recruitment_status_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> study_type_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> is_committee_approved { get; set; }
       [DataMember]
        public List<MiscCountEntity> research_result { get; set; }
      

        [DataMember]
        public List<MiscCountEntity> source { get; set; }

        [DataMember]
        public List<MiscCountEntity> provinces { get; set; }

        [DataMember]
        public int count { get; set; }
        #endregion

        #region hcp
        /// <summary>
        /// 学术头衔
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> academic_title { get; set; }
        /// <summary>
        /// 管理头衔
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> admin_title { get; set; }
        /// <summary>
        /// 专业头衔
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> professional_title { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> type_list { get; set; }
        /// <summary>
        /// 专业经验
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> experience { get; set; }
        /// <summary>
        /// 医院等级
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> hospital_level { get; set; }

        /// <summary>
        /// 医院等级
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> hospital_tier { get; set; }

        /// <summary>
        /// PI
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> pi { get; set; }

        /// <summary>
        /// 医院等级(org)
        /// </summary>
        [DataMember]
        public List<MiscCountEntity> hospital_tiers { get; set; }
        #endregion

        #region org
        public List<MiscCountEntity> type { get; set; }
        public List<MiscCountEntity> province_id { get; set; }

        public List<MiscCountEntity> fd_ids { get; set; }

        public List<MiscCountEntity> gcp_ids { get; set; }

        public List<MiscCountEntity> medical_institution_id { get; set; }
        public List<MiscCountEntity> certification { get; set; }
        #endregion

        #region compound
        [DataMember]
        public List<MiscCountEntity> compound_type { get; set; }
        #endregion

        #region target
        [DataMember]
        public List<MiscCountEntity> target_type_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> disease_research_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> target_organism_id { get; set; }
        #endregion

        #region paper
        [DataMember]
        public List<IFEntity> IF { get; set; }
        [DataMember]
        public List<MiscCountEntity> source_id { get; set; }

        [DataMember]
        public List<MiscCountEntity> journal_country { get; set; }
        [DataMember]
        public List<MiscCountEntity> publish_year { get; set; }
        #endregion

        #region grant
        [DataMember]
        public List<GrantAmountEntity> grant_amount { get; set; }
        [DataMember]
        public List<MiscCountEntity> grant_type_id { get; set; }
        [DataMember]
        public List<MiscCountEntity> start_year { get; set; }

        [DataMember]
        public List<MiscCountEntity> is_related_paper { get; set; }
        #endregion

        #region guideline

        [DataMember]
        public List<MiscCountEntity> disease_parent { get; set; }



        [DataMember]
        public List<MiscCountEntity> guideline_type { get; set; }

        [DataMember]
        public List<MiscCountEntity> link_framer { get; set; }
        #endregion
    }
    [DataContract]
    public class GrantAmountEntity
    {
        [DataMember]
        public string range { get; set; }
        [DataMember]
        public string count { get; set; }

    }
    [DataContract]
    public class MiscCountEntity
    {

        //[DataMember]
        //public string field { get; set; }
        [DataMember(Name = "val")]
        public string value { get; set; }
        [DataMember]
        public string count { get; set; }
    }
    [DataContract]
    public class IFEntity
    {
        [DataMember]
        public string range { get; set; }
        [DataMember]
        public string count { get; set; }

    }
}
