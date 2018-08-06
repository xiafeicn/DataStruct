using System.Collections.Generic;

namespace KolTool.Model
{
    public class BaseEntity<T>
    {
        public List<T> data { get; set; }
        public int total { get; set; }

        public Meta meta { get; set; }
    }

    public class OrgTitle
    {
        public string professional_title { get; set; }
        public List<string> academic_title { get; set; }
        public List<string> admin_title { get; set; }

        public string id { get; set; }
    }


    public class ENCN
    {
        public string cn { get; set; }
        public string en { get; set; }
    }

    public class CriteriaENCN
    {
        public string[] Exclusion_criteria_cn { get; set; }
        public string[] Inclusion_criteria_cn { get; set; }
        public string[] Exclusion_criteria_en { get; set; }
        public string[] Inclusion_criteria_en { get; set; }
    }

    public class ENCNINFO : ENCN
    {
        public string info { get; set; }
    }

    public class ENCNEntites
    {
        public string[] cn { get; set; }
        public string[] en { get; set; }
    }

    public class Meta
    {

        public int count { get; set; }

        public int all { get; set; }//总数量
    }
}
