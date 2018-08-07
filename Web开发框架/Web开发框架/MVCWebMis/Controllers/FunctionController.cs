using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using WHC.Framework.Commons;
using WHC.MVCWebMis.BLL;
using WHC.MVCWebMis.Common;
using WHC.MVCWebMis.Entity;
using WHC.Framework.ControlUtil;
using System.Data.Common;

namespace WHC.MVCWebMis.Controllers
{
    public class FunctionController : BusinessController<Function, FunctionInfo>
    {
        public FunctionController() :base()
        {
        }

        protected override void ConvertAuthorizedInfo()
        {
            //屏蔽基类调用

            //base.ConvertAuthorizedInfo();
        }

        public ActionResult GetTreeList()
        {
            List<FunctionInfo> comboList = BLLFactory<Function>.Instance.GetAll();
            comboList = CollectionHelper<FunctionInfo>.Fill("-1", 0, comboList, "PID", "ID", "Name");
            return Json(comboList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的功能树Json
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllTreeJson()
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                EasyTreeData pNode = new EasyTreeData(typeInfo.OID, typeInfo.Name, "icon-computer");
                treeList.Add(pNode);

                string systemType = typeInfo.OID;//系统标识ID
                //绑定树控件
                List<FunctionNodeInfo> functionList = BLLFactory<Function>.Instance.GetTree(systemType);
                foreach (FunctionNodeInfo info in functionList)
                {
                    EasyTreeData item = new EasyTreeData(info.ID, info.Name, "icon-key");
                    pNode.children.Add(item);

                    AddChildNode(info.Children, item);
                }
            }

            string content = ToJson(treeList);
            return Content(content.Trim(','));
        }

        /// <summary>
        /// 获取所有的功能树Json(用于树控件选择）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFunctionTreeJson()
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                EasyTreeData pNode = new EasyTreeData(typeInfo.OID, typeInfo.Name, "icon-computer");
                treeList.Add(pNode);

                string systemType = typeInfo.OID;//系统标识ID
                //绑定树控件
                List<FunctionNodeInfo> functionList = BLLFactory<Function>.Instance.GetTree(systemType);
                foreach (FunctionNodeInfo info in functionList)
                {
                    EasyTreeData item = new EasyTreeData(info.ID, info.Name, "icon-key");
                    pNode.children.Add(item);

                    AddChildNode(info.Children, item);
                }
            }

            string content = ToJson(treeList);
            return Content(content.Trim(','));
        }

        private void AddChildNode(List<FunctionNodeInfo> list, EasyTreeData fnode)
        {
            foreach (FunctionNodeInfo info in list)
            {
                EasyTreeData item = new EasyTreeData(info.ID, info.Name, "icon-key");
                fnode.children.Add(item);

                AddChildNode(info.Children, item);
            }
        }

        /// <summary>
        /// 获取指定角色的功能集合
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <returns></returns>
        public ActionResult GetFunctions(string roleid)
        {
            ActionResult result = Content("");
            if (!string.IsNullOrEmpty(roleid) && ValidateUtil.IsValidInt(roleid))
            {
                List<FunctionInfo> roleList = BLLFactory<Function>.Instance.GetFunctionsByRole(Convert.ToInt32(roleid));
                result = Json(roleList, JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        /// <summary>
        /// 新增和编辑同时需要修改的内容
        /// </summary>
        /// <param name="info"></param>
        private void SetCommonInfo(FunctionInfo info)
        {
            //info.Editor = CurrentUser.FullName;
            //info.Editor_ID = CurrentUser.ID.ToString();
            //info.EditTime = DateTime.Now;
        }
        public override ActionResult Insert(FunctionInfo info)
        {
            string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}' ", info.ControlID, info.SystemType_ID);
            bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定功能控制ID重复，请重新输入！");
            }

            //info.CreateTime = DateTime.Now;
            //info.Creator = CurrentUser.FullName;
            //info.Creator_ID = CurrentUser.ID.ToString();
            SetCommonInfo(info);

            return base.Insert(info);
        }

        public override ActionResult Insert2(FunctionInfo info)
        {
            string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}' ", info.ControlID, info.SystemType_ID);
            bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定功能控制ID重复，请重新输入！");
            }

            //info.CreateTime = DateTime.Now;
            //info.Creator = CurrentUser.FullName;
            //info.Creator_ID = CurrentUser.ID.ToString();
            SetCommonInfo(info);

            return base.Insert2(info);
        }

        /// <summary>
        /// 重写方便写入公司、部门、编辑时间的名称等信息
        /// </summary>
        /// <param name="id">对象ID</param>
        /// <param name="info">对象信息</param>
        /// <returns></returns>
        protected override bool Update(string id, FunctionInfo info)
        {
            string filter = string.Format("ControlID='{0}' and SystemType_ID='{1}' and ID <>'{2}'",
                info.ControlID, info.SystemType_ID, info.ID);
            bool isExist = BLLFactory<Function>.Instance.IsExistRecord(filter);
            if (isExist)
            {
                throw new ArgumentException("指定功能控制ID重复，请重新输入！");
            }

            SetCommonInfo(info);

            return base.Update(id, info);
        }

        /// <summary>
        /// 批量添加功能操作
        /// </summary>
        /// <param name="mainInfo">主功能信息</param>
        /// <param name="controlString">附加的操作功能列表，以逗号分开多个，如：add,delete,edit,view,export,import</param>
        /// <returns></returns>
        public ActionResult BatchAddFunction(FunctionInfo mainInfo, string controlString)
        {
            List<string> controlList = new List<string>();
            if(!string.IsNullOrWhiteSpace(controlString))
            {
                foreach(string item in controlString.ToLower().Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        controlList.Add(item);
                    }
                }
            }

            bool result = false;
            using (DbTransaction trans = BLLFactory<Function>.Instance.CreateTransaction())
            {
                try
                {
                    if (trans != null)
                    {
                        bool sucess = BLLFactory<Function>.Instance.Insert(mainInfo, trans);
                        if (sucess)
                        {
                            FunctionInfo subInfo = null;
                            int sortCodeIndex = 1;

                            #region 子功能操作
                            if (controlList.Contains("add"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Add", mainInfo.ControlID);
                                subInfo.Name = string.Format("添加{0}", mainInfo.Name);

                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("delete"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Delete", mainInfo.ControlID);
                                subInfo.Name = string.Format("删除{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("edit") || controlList.Contains("modify"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Edit", mainInfo.ControlID);
                                subInfo.Name = string.Format("修改{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("view"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/View", mainInfo.ControlID);
                                subInfo.Name = string.Format("查看{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("import"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Import", mainInfo.ControlID);
                                subInfo.Name = string.Format("导入{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            if (controlList.Contains("export"))
                            {
                                subInfo = CreateSubFunction(mainInfo);
                                subInfo.SortCode = (sortCodeIndex++).ToString("D2");
                                subInfo.ControlID = string.Format("{0}/Export", mainInfo.ControlID);
                                subInfo.Name = string.Format("导出{0}", mainInfo.Name);
                                BLLFactory<Function>.Instance.Insert(subInfo, trans);
                            }
                            #endregion

                            trans.Commit();
                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    LogTextHelper.Error(ex);
                    throw;
                }
            }
            return Content(result);
        }

        private FunctionInfo CreateSubFunction(FunctionInfo mainInfo)
        {
            FunctionInfo subInfo = new FunctionInfo();
            subInfo.PID = mainInfo.ID;
            subInfo.SystemType_ID = mainInfo.SystemType_ID;
            return subInfo;
        }

        /// <summary>
        /// 获取用户的可操作功能
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult GetFunctionTreeJsonByUser(int userId)
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();           

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                EasyTreeData parentNode = new EasyTreeData(typeInfo.OID, typeInfo.Name, "icon-organ");
                List<FunctionNodeInfo> list = BLLFactory<Function>.Instance.GetFunctionNodesByUser(userId, typeInfo.OID);
                AddFunctionNode(parentNode, list);

                treeList.Add(parentNode);
            }

            if(treeList.Count == 0)
            {
                treeList.Insert(0, new EasyTreeData(-1, "无"));
            }

            string json = ToJson(treeList);
            return Content(json);
        }

        private void AddFunctionNode(EasyTreeData node, List<FunctionNodeInfo> list)
        {
            foreach (FunctionNodeInfo info in list)
            {
                EasyTreeData subNode = new EasyTreeData(info.ID, info.Name, info.Children.Count > 0 ? "icon-group-key" : "icon-key");
                node.children.Add(subNode);

                AddFunctionNode(subNode, info.Children);
            }
        }

        /// <summary>
        /// 根据用户角色获取其对应的所能访问的权限集合
        /// </summary>
        /// <param name="userId">当前用户ID</param>
        /// <returns></returns>
        public ActionResult GetRoleFunctionByUser(int userId)
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();

            bool isSuperAdmin = false;
            UserInfo userInfo = BLLFactory<User>.Instance.FindByID(userId);
            if (userInfo != null)
            {
                isSuperAdmin = BLLFactory<User>.Instance.UserInRole(userInfo.Name, RoleInfo.SuperAdminName);
            }

            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                EasyTreeData parentNode = new EasyTreeData(typeInfo.OID, typeInfo.Name, "icon-organ");

                //如果是超级管理员，不根据角色获取，否则根据角色获取对应的分配权限
                //也就是说，公司管理员只能分配自己被授权的功能，而超级管理员不受限制
                List<FunctionNodeInfo> allNode = new List<FunctionNodeInfo>();
                if (isSuperAdmin)
                {
                    allNode = BLLFactory<Function>.Instance.GetTree(typeInfo.OID);
                }
                else
                {
                    allNode = BLLFactory<Function>.Instance.GetFunctionNodesByUser(userId, typeInfo.OID);
                }

                AddFunctionNode(parentNode, allNode);
                treeList.Add(parentNode);
            }

            if (treeList.Count == 0)
            {
                treeList.Insert(0, new EasyTreeData(-1, "无"));
            }

            string json = ToJson(treeList);
            return Content(json);
        }
    }
}
