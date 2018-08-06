using System.Collections.Generic;
using System.Runtime.Serialization;
using GBI.Metrix.Model;

namespace KolTool.Model
{
    [DataContract]
    public class Filter
    {
        [DataMember]
        public string FacetedType { get; set; }
        [DataMember]
        public List<FilterInfo> FacetedList { get; set; }
    }
}
