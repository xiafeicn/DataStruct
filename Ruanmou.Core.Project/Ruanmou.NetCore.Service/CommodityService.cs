using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Ruanmou.NetCore.Interface;
using Microsoft.EntityFrameworkCore;
using Ruanmou.EFCore.Model;

namespace Ruanmou.NetCore.Servcie
{
    public class CommodityService : BaseService, ICommodityService
    {
        #region Identity
        private DbSet<Commodity> _CommodityDbSet = null;
        public CommodityService(DbContext dbContext)
            : base(dbContext)
        {
            this._CommodityDbSet = dbContext.Set<Commodity>();
        }
        #endregion

        public override void Dispose()
        {
            Console.WriteLine("已释放");
            base.Dispose();
        }
    }
}
