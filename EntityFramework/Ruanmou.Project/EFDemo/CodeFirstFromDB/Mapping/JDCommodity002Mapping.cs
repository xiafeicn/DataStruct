using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.CodeFirstFromDB.Mapping
{
    public class JDCommodity002Mapping : EntityTypeConfiguration<JDCommodity002>
    {
        public JDCommodity002Mapping()
        {
            this.ToTable("JD_Commodity_002");
        }
    }
}
