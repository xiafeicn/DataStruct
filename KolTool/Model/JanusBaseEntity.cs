using System.Runtime.Serialization;

namespace KolTool.Model
{
    public interface IJanusBaseEntity
    {
        [DataMember]
        string id { get; set; }

        [DataMember]
        ENCN title { get; set; }

        [DataMember]
        string name_en { get; set; }

        [DataMember]
        string name_cn { get; set; }
    }
}
