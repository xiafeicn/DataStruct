using System.Runtime.Serialization;

namespace KolTool.Model
{
    [DataContract]
    public class FilterInfo
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public bool Checked { get; set; }
    }
}
