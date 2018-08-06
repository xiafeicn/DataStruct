using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KolTool.Model
{
    [DataContract]
    public class NameListEntity<T> where T : IJanusBaseEntity
    {
        [DataMember]
        public List<T> data { get; set; }
    }
}
