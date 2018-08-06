namespace KolTool.Base
{
    public  class MiscEntity
    {
        public string dataid { set; get; }
        public string id { set; get; }
        public string name_cn { set; get; }
        public string name_en { set; get; }

        public string table { get; set; }

        public string parent_id { get; set; }
        public string parent_dataid { get; set; }
    }
}
