using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WHC.Pager.Entity;
using WHC.Framework.Commons;
using WHC.MVCWebMis.BLL;
using WHC.MVCWebMis.Entity;
using WHC.Framework.ControlUtil;

namespace WHC.MVCWebMis.Controllers
{
    /// <summary>
    /// 角色可访问数据（组织机构）的控制器类
    /// </summary>
    public class RoleDataController : BusinessController<RoleData, RoleDataInfo>
    {
        public RoleDataController() : base()
        {
        }

        /// <summary>
        /// 保存角色的数据权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="belongCompanys">所属公司</param>
        /// <param name="belongDepts">所属机构</param>
        /// <returns></returns>
        public ActionResult UpdateRoleData(int roleId, string belongCompanys, string belongDepts)
        {
            bool result = BLLFactory<RoleData>.Instance.UpdateRoleData(roleId, belongCompanys, belongDepts);

            return Content(result);
        }

        /// <summary>
        /// 获取角色包含的数据权限（组织机构ID列表）
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public ActionResult GetRoleDataList(int roleId)
        {
            Dictionary<int,int> dict = BLLFactory<RoleData>.Instance.GetRoleDataDict(roleId);

            List<int> list = new List<int>(); 
            list.AddRange(dict.Keys);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
