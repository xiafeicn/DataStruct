using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.MVCWebMis.Entity;
using WHC.MVCWebMis.BLL;
using WHC.MVCWebMis.Entity;

namespace WHC.MVCWebMis.Controllers
{
    /// <summary>
    /// 角色业务操作控制器
    /// </summary>
    public class RoleController : BusinessController<Role, RoleInfo>
    {       
        public RoleController() : base()
        {
        }

        /// <summary>
        /// 获取角色分类：系统角色、业务角色、应用角色...
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleCategorys()
        {
            List<CListItem> listItem = new List<CListItem>();
            string[] enumNames = EnumHelper.GetMemberNames<RoleCategoryEnum>();

            foreach (string item in enumNames)
            {
                listItem.Add(new CListItem(item));
            }
            return Json(listItem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditUsers(string roleId, string newList)
        {
            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
            {
                if (!string.IsNullOrWhiteSpace(newList))
                {
                    List<int> list = new List<int>();
                    foreach (string id in newList.Split(','))
                    {
                        list.Add(id.ToInt32());
                    }

                    BLLFactory<Role>.Instance.EditRoleUsers(roleId.ToInt32(), list);
                    return Content("true");
                }
            }
            return Content("");
        }

        public ActionResult EditOUs(string roleId, string newList)
        {
            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
            {
                if (!string.IsNullOrWhiteSpace(newList))
                {
                    List<int> list = new List<int>();
                    foreach (string id in newList.Split(','))
                    {
                        list.Add(id.ToInt32());
                    }

                    BLLFactory<Role>.Instance.EditRoleOUs(roleId.ToInt32(), list);
                    return Content("true");
                }
            }
            return Content("");
        }
        public ActionResult EditFunctions(string roleId, string newList)
        {
            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
            {
                if (!string.IsNullOrWhiteSpace(newList))
                {
                    List<string> list = new List<string>();
                    foreach (string id in newList.Split(','))
                    {
                        list.Add(id);
                    }

                    BLLFactory<Role>.Instance.EditRoleFunctions(roleId.ToInt32(), list);
                    return Content("true");
                }
            }
            return Content("");
        }

        public ActionResult EditUserRelation(string roleId, string addList, string removeList)
        {
            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
            {
                if (!string.IsNullOrWhiteSpace(removeList))
                {
                    foreach (string id in removeList.Split(','))
                    {
                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
                        {
                            BLLFactory<Role>.Instance.RemoveUser(Convert.ToInt32(id), Convert.ToInt32(roleId));
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(addList))
                {
                    foreach (string id in addList.Split(','))
                    {
                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
                        {
                            BLLFactory<Role>.Instance.AddUser(Convert.ToInt32(id), Convert.ToInt32(roleId));
                        }
                    }
                }

                return Content("true");
            }
            return Content("");
        }

        public ActionResult EditOURelation(string roleId, string addList, string removeList)
        {
            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
            {
                if (!string.IsNullOrWhiteSpace(removeList))
                {
                    foreach (string id in removeList.Split(','))
                    {
                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
                        {
                            BLLFactory<Role>.Instance.RemoveOU(Convert.ToInt32(id), Convert.ToInt32(roleId));
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(addList))
                {
                    foreach (string id in addList.Split(','))
                    {
                        if (!string.IsNullOrEmpty(id) && ValidateUtil.IsValidInt(id))
                        {
                            BLLFactory<Role>.Instance.AddOU(Convert.ToInt32(id), Convert.ToInt32(roleId));
                        }
                    }
                }
                return Content("true");
            }
            return Content("");
        }

        public ActionResult EditFunctionRelation(string roleId, string addList, string removeList)
        {
            if (!string.IsNullOrEmpty(roleId) && ValidateUtil.IsValidInt(roleId))
            {
                if (!string.IsNullOrWhiteSpace(removeList))
                {
                    foreach (string id in removeList.Split(','))
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            BLLFactory<Role>.Instance.RemoveFunction(id, Convert.ToInt32(roleId));
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(addList))
                {
                    foreach (string id in addList.Split(','))
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            BLLFactory<Role>.Instance.AddFunction(id, Convert.ToInt32(roleId));
                        }
                    }
                }
                return Content("true");
            }
            return Content("");
        }

        public ActionResult GetRolesByUser(string userid)
        {
            if (!string.IsNullOrEmpty(userid) && ValidateUtil.IsValidInt(userid))
            {
                List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByUser(Convert.ToInt32(userid));
                return Json(roleList, JsonRequestBehavior.AllowGet);
            }

            return Content("");
        }

        public ActionResult GetRolesByFunction(string functionId)
        {
            if (!string.IsNullOrEmpty(functionId))
            {
                List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByFunction(functionId);
                return Json(roleList, JsonRequestBehavior.AllowGet);
            }
            return Content("");
        }

        public ActionResult GetRolesByOU(string ouid)
        {
            if (!string.IsNullOrEmpty(ouid) && ValidateUtil.IsValidInt(ouid))
            {
                List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByOU(Convert.ToInt32(ouid));
                return Json(roleList, JsonRequestBehavior.AllowGet);
            }
            return Content("");
        }

        /// <summary>
        /// 新增和编辑同时需要修改的内容
        /// </summary>
        /// <param name="info"></param>
        private void SetCommonInfo(RoleInfo info)
        {
            info.Editor = CurrentUser.FullName;
            info.Editor_ID = CurrentUser.ID.ToString();
            info.EditTime = DateTime.Now;

            OUInfo companyInfo = BLLFactory<OU>.Instance.FindByID(info.Company_ID);
            if (companyInfo != null)
            {
                info.CompanyName = companyInfo.Name;
            }
        }
        public override ActionResult Insert(RoleInfo info)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.InsertKey);

            string filter = string.Format("Name='{0}' ", info.Name);
            bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定角色名称重复，请重新输入！");
            }

            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.FullName;
            info.Creator_ID = CurrentUser.ID.ToString();
            SetCommonInfo(info);

            return base.Insert(info);
        }

        public override ActionResult Insert2(RoleInfo info)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.InsertKey);

            string filter = string.Format("Name='{0}' ", info.Name);
            bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定角色名称重复，请重新输入！");
            }

            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.FullName;
            info.Creator_ID = CurrentUser.ID.ToString();
            SetCommonInfo(info);

            return base.Insert2(info);
        }

        /// <summary>
        /// 重写方便写入公司、部门、编辑时间的名称等信息
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="info">对象信息</param>
        /// <returns></returns>
        protected override bool Update(string id, RoleInfo info)
        {
            string filter = string.Format("Name='{0}' and ID <>'{1}'", info.Name, info.ID);
            bool isExist = BLLFactory<Role>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定角色名称重复，请重新输入！");
            }

            SetCommonInfo(info);

            return base.Update(id, info);
        }

        /// <summary>
        /// 获取用户的部门角色树结构(分级需要）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyRoleTreeJson(int userId)
        {
            StringBuilder content = new StringBuilder();
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                OUInfo groupInfo = GetMyTopGroup(userInfo);
                if (groupInfo != null)
                {
                    EasyTreeData topnode = new EasyTreeData("dept" + groupInfo.ID, groupInfo.Name, GetIconcls(groupInfo.Category));
                    AddRole(groupInfo, topnode);

                    if (groupInfo.Category == "集团")
                    {
                        List<OUInfo> list = BLLFactory<OU>.Instance.GetAllCompany();
                        foreach (OUInfo info in list)
                        {
                            EasyTreeData companyNode = new EasyTreeData("dept" + info.ID, info.Name, GetIconcls(info.Category));
                            topnode.children.Add(companyNode);

                            AddRole(info, companyNode);
                        }
                    }

                    content.Append(base.ToJson(topnode));
                }
            }
            string json = string.Format("[{0}]", content.ToString().Trim(','));
            return Content(json);
        }

        private void AddRole(OUInfo ouInfo, EasyTreeData treeNode)
        {
            List<RoleInfo> roleList = BLLFactory<Role>.Instance.GetRolesByCompany(ouInfo.ID.ToString());
            foreach (RoleInfo roleInfo in roleList)
            {
                EasyTreeData roleNode = new EasyTreeData("role" + roleInfo.ID, roleInfo.Name, "icon-group-key");
                treeNode.children.Add(roleNode);
            }
        }
    }
}
