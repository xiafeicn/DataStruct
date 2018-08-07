using System;
using System.Collections.Generic;
using System.Data;
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
    public class UserController : BusinessController<User, UserInfo>
    {
        public UserController() : base()
        {
        }

        /// <summary>
        /// 重置用户密码为12345678
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public ActionResult ResetPassword(string id)
        {
            string result = "口令初始化失败";
            if (string.IsNullOrEmpty(id))
            {
                result = "用户id不能为空";
            }
            else
            {
                UserInfo info = BLLFactory<User>.Instance.FindByID(id);
                if (info != null)
                {
                    string defaultPassword = "12345678";
                    bool tempBool = BLLFactory<User>.Instance.ModifyPassword(info.Name, defaultPassword);
                    if (tempBool)
                    {
                        result = "OK";
                    }
                    else
                    {
                        result = "口令初始化失败";
                    }
                }
            }
            return Content(result);
        }

        public ActionResult ModifyPass(string name, string oldpass, string newpass)
        {
            #region MyRegion
            //string result = "";
            //if (oldpass != "123")
            //{
            //    result = "原口令错误";
            //}
            //else if (string.IsNullOrWhiteSpace(newpass))
            //{
            //    result = "新密码不能为空";
            //}
            //else
            //{
            //    result = "OK";
            //}
            //return Content(result); 
            #endregion

            string result = "";
            string identity = BLLFactory<User>.Instance.VerifyUser(name, oldpass, "WareMis");
            if (string.IsNullOrEmpty(identity))
            {
                result = "原口令错误";
            }

            bool tempBool = BLLFactory<User>.Instance.ModifyPassword(name, newpass);
            if (tempBool)
            {
                result = "OK";
            }
            else
            {
                result = "口令修改失败";
            }
            return Content(result);
        }

        /// <summary>
        /// 获取用户的部门树结构(分级需要）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyDeptTreeJson(int userId)
        {
            StringBuilder content = new StringBuilder();
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                OUInfo groupInfo = GetMyTopGroup(userInfo);
                if (groupInfo != null)
                {
                    List<OUNodeInfo> list = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);

                    EasyTreeData treeData = new EasyTreeData(groupInfo.ID, groupInfo.Name, GetIconcls(groupInfo.Category));
                    GetTreeDataWithOUNode(list, treeData);

                    content.Append(base.ToJson(treeData));
                }
            }
            string json = string.Format("[{0}]", content.ToString().Trim(','));
            return Content(json);
        }

        /// <summary>
        /// 获取用户的公司结构(分级需要）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public ActionResult GetMyCompanyTreeJson(int userId)
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();

            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                List<OUNodeInfo> list = new List<OUNodeInfo>();
                if (BLLFactory<User>.Instance.UserInRole(userInfo.Name, RoleInfo.SuperAdminName))
                {
                    list = BLLFactory<OU>.Instance.GetGroupCompanyTree();
                }
                else
                {
                    OUInfo myCompanyInfo = BLLFactory<OU>.Instance.FindByID(userInfo.Company_ID);
                    if (myCompanyInfo != null)
                    {
                        list.Add(new OUNodeInfo(myCompanyInfo));
                    }
                }

                if (list.Count > 0)
                {
                    OUNodeInfo info = list[0];//无论是集团还是公司，节点只有一个
                    EasyTreeData node = new EasyTreeData(info.ID, info.Name, GetIconcls(info.Category));
                    GetTreeDataWithOUNode(info.Children, node);
                    treeList.Add(node);
                }
            }

            string json = ToJson(treeList);
            return Content(json);
        }

        private void GetTreeDataWithOUNode(List<OUNodeInfo> list, EasyTreeData parent)
        {
            List<EasyTreeData> result = new List<EasyTreeData>();
            foreach (OUNodeInfo ouInfo in list)
            {
                EasyTreeData treeData = new EasyTreeData(ouInfo.ID, ouInfo.Name, GetIconcls(ouInfo.Category));
                GetTreeDataWithOUNode(ouInfo.Children, treeData);

                result.Add(treeData);
            }

            parent.children.AddRange(result);
        }

        /// <summary>
        /// 根据公司ID获取对应部门的树Json
        /// </summary>
        /// <param name="parentId">父部门ID</param>
        /// <returns></returns>
        public ActionResult GetDeptTreeJson(string parentId)
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();
            treeList.Insert(0, new EasyTreeData(-1, "无"));

            if (!string.IsNullOrEmpty(parentId))
            {
                OUInfo groupInfo = BLLFactory<OU>.Instance.FindByID(parentId);
                if (groupInfo != null)
                {
                    List<OUNodeInfo> list = BLLFactory<OU>.Instance.GetTreeByID(groupInfo.ID);

                    EasyTreeData treeData = new EasyTreeData(groupInfo.ID, groupInfo.Name, "icon-group");
                    GetTreeDataWithOUNode(list, treeData);

                    treeList.Add(treeData);
                }
            }

            string json = ToJson(treeList);
            return Content(json);
        }
                
        /// <summary>
        /// 根据用户获取对应人员层次的树Json
        /// </summary>
        /// <param name="deptId">用户所在部门</param>
        /// <returns></returns>
        public ActionResult GetUserTreeJson(int deptId)
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();
            treeList.Insert(0, new EasyTreeData(-1, "无"));

            List<UserInfo> list = BLLFactory<User>.Instance.FindByDept(deptId);
            foreach (UserInfo info in list)
            {
                treeList.Add(new EasyTreeData(info.ID, info.FullName, "icon-user"));
            }

            string json = ToJson(treeList);
            return Content(json);
        }

        /// <summary>
        /// 根据角色获取对应的用户
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public ActionResult GetUsersByRole(string roleid)
        {
            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(roleid) && ValidateUtil.IsValidInt(roleid))
            {
                List<UserInfo> roleList = BLLFactory<User>.Instance.GetUsersByRole(Convert.ToInt32(roleid));
                result = JsonDate(roleList);
            }
            return result;
        }

        /// <summary>
        /// 根据机构获取对应的用户
        /// </summary>
        /// <param name="ouid">机构ID</param>
        /// <returns></returns>
        public ActionResult GetUsersByOU(string ouid)
        {
            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(ouid) && ValidateUtil.IsValidInt(ouid))
            {
                List<UserInfo> roleList = BLLFactory<User>.Instance.GetUsersByOU(Convert.ToInt32(ouid));
                result = JsonDate(roleList);
            }
            return result;
        }

        /// <summary>
        /// 获取分页操作的查询条件
        /// </summary>
        /// <returns></returns>
        protected override string GetPagerCondition()
        {
            string condition = "";
            //增加对角色、部门、公司的判断
            string deptId = Request["Dept_ID"] ?? "";    

            if (!string.IsNullOrEmpty(deptId))
            {
                condition = string.Format("Dept_ID = {0} or Company_ID ={0}", deptId);
            }
            else
            {
                condition = base.GetPagerCondition();
            }

            return condition;
        }

        /// <summary>
        /// 重写分页操作，对特殊条件进行处理
        /// </summary>
        /// <returns></returns>
        public override ActionResult FindWithPager()
        {
            string roleId = Request["Role_ID"] ?? "";
            if (!string.IsNullOrEmpty(roleId))
            {
                //检查用户是否有权限，否则抛出MyDenyAccessException异常
                base.CheckAuthorized(AuthorizeKey.ListKey);
                List<UserInfo> list = BLLFactory<User>.Instance.GetUsersByRole(roleId.ToInt32());

                //Json格式的要求{total:22,rows:{}}
                //构造成Json的格式传递
                var result = new { total = list.Count, rows = list };
                return JsonDate(result);
            }
            else
            {
                return base.FindWithPager();
            }
        }

        /// <summary>
        /// 新增和编辑同时需要修改的内容
        /// </summary>
        /// <param name="info"></param>
        private void SetCommonInfo(UserInfo info)
        {
            info.Editor = CurrentUser.FullName;
            info.Editor_ID = CurrentUser.ID.ToString();
            info.EditTime = DateTime.Now;

            info.Company_ID = info.Company_ID;
            info.CompanyName = BLLFactory<OU>.Instance.GetName(info.Company_ID.ToInt32());
            info.Dept_ID = info.Dept_ID;
            info.DeptName = BLLFactory<OU>.Instance.GetName(info.Dept_ID.ToInt32());
        }

        public override ActionResult Insert(UserInfo info)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.InsertKey);

            string filter = string.Format("Name='{0}' ", info.Name);
            bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定用户名重复，请重新输入！");
            }

            info.CreateTime = DateTime.Now;
            info.Creator = CurrentUser.FullName;
            info.Creator_ID = CurrentUser.ID.ToString();
            SetCommonInfo(info);

            return base.Insert(info);
        }

        public override ActionResult Insert2(UserInfo info)
        {
            //检查用户是否有权限，否则抛出MyDenyAccessException异常
            base.CheckAuthorized(AuthorizeKey.InsertKey);

            string filter = string.Format("Name='{0}' ", info.Name);
            bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定用户名重复，请重新输入！");
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
        /// <param name="id">对象主键ID</param>
        /// <param name="info">对象信息</param>
        /// <returns></returns>
        protected override bool Update(string id, UserInfo info)
        {
            string filter = string.Format("Name='{0}' and ID <>'{1}'", info.Name, info.ID);
            bool isExist = BLLFactory<User>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定用户名重复，请重新输入！");
            }

            SetCommonInfo(info);

            return base.Update(id, info);
        }

        public ActionResult ChartIndex()
        {
            return View("ChartIndex");
        }

        /// <summary>
        /// 统计各个分子公司的人数，返回Json字符串，供图表统计
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCompanyUserCountJson()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            List<OUInfo> companyList = BLLFactory<OU>.Instance.GetAllCompany();
            foreach (OUInfo companyInfo in companyList)
            {
                string condition = string.Format("Company_ID={0} AND Deleted=0", companyInfo.ID);
                int count = BLLFactory<User>.Instance.GetRecordCount(condition);
                if (!dict.ContainsKey(companyInfo.Name))
                {
                    dict.Add(companyInfo.Name, count);
                }
            }

            return ToJsonContent(dict);
        }
    }
}
