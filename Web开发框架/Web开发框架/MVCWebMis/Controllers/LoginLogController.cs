using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WHC.Pager.Entity;
using WHC.Framework.Commons;
using WHC.MVCWebMis.BLL;
using WHC.MVCWebMis.Entity;
using WHC.MVCWebMis.Entity;
using WHC.Framework.ControlUtil;

namespace WHC.MVCWebMis.Controllers
{
    public class LoginLogController : BusinessController<LoginLog, LoginLogInfo>
    {
        public LoginLogController() : base()
        {
        }

        public ActionResult GetTreeJson()
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();

            //添加一个未分类和全部客户的组别
            EasyTreeData topNode = new EasyTreeData("-1", "所有记录", "icon-house");
            treeList.Add(topNode);

            EasyTreeData companyNode = new EasyTreeData( "-2","所属公司", "");
            treeList.Add(companyNode);

            List<OUInfo> companyList = new List<OUInfo>();
            if (BLLFactory<User>.Instance.UserInRole(CurrentUser.Name, RoleInfo.SuperAdminName))
            {
                companyList = BLLFactory<OU>.Instance.GetAllCompany();
            }
            else
            {
                OUInfo myCompanyInfo = BLLFactory<OU>.Instance.FindByID(CurrentUser.Company_ID);
                if (myCompanyInfo != null)
                {
                    companyList.Add(myCompanyInfo);
                }
            }

            string belongCompany = "-1,";
            foreach (OUInfo info in companyList)
            {
                belongCompany += string.Format("{0},", info.ID);

                //添加公司节点
                EasyTreeData subNode = new EasyTreeData(info.ID, info.Name, "icon-organ");
                companyNode.children.Add(subNode);

                //下面在添加系统类型节点
                List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
                foreach (SystemTypeInfo typeInfo in typeList)
                {
                    EasyTreeData typeNode = new EasyTreeData(typeInfo.OID, typeInfo.Name, "icon-computer");
                    typeNode.id = string.Format("Company_ID='{0}' AND SystemType_ID='{1}' ", info.ID, typeInfo.OID);
                    subNode.children.Add(typeNode);
                }

                EasyTreeData securityNode = new EasyTreeData("Security", "权限管理系统", "icon-key");
                securityNode.id = string.Format("Company_ID='{0}' AND SystemType_ID='{1}' ", info.ID, "Security");
                subNode.children.Add(securityNode);
            }
            //修改全部为所属公司的ID
            belongCompany = belongCompany.Trim(',');
            topNode.id = string.Format("Company_ID in ({0})", belongCompany);

            string content = ToJson(treeList);
            return Content(content.Trim(','));
        }
    }
}
