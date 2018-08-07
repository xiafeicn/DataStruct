using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.MVCWebMis.Entity;
using WHC.Pager.Entity;
using WHC.MVCWebMis.BLL;

namespace WHC.MVCWebMis.Controllers
{
    public class MenuController : BusinessController<Menu, MenuInfo>
    {
        /// <summary>
        /// 用作下拉列表的菜单Json数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictJson()
        {
            List<MenuInfo> list = baseBLL.GetAll();
            list = CollectionHelper<MenuInfo>.Fill("-1", 0, list, "PID", "ID", "Name");

            List<CListItem> itemList = new List<CListItem>();
            foreach (MenuInfo info in list)
            {
                itemList.Add(new CListItem(info.Name, info.ID));
            }
            itemList.Insert(0, new CListItem("无", "-1"));
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取菜单的树形展示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuData()
        {
            #region 返回菜单Json格式
            //        {
            //            "default": [
            //                { "menuid": "1", "icon": "icon-computer", "menuname": "权限管理",
            //				  "menus": [
            //						    { "menuid": "13", "menuname": "用户管理", "icon": "icon-user", "url": "ListUser.aspx" },
            //						    { "menuid": "14", "menuname": "组织机构管理", "icon": "icon-organ", "url": "ListOU.aspx" },
            //						    { "menuid": "15", "menuname": "角色管理", "icon": "icon-group-key", "url": "ListRole.aspx" },
            //						    { "menuid": "16", "menuname": "功能管理", "icon": "icon-key", "url": "ListFunction.aspx" }
            //			   ]},
            //               { "menuid": "2", "icon": "icon-user", "menuname": "其他管理",
            //				 "menus": [{ "menuid": "21", "menuname": "修改密码", "icon": "icon-lock", "url": "ModifyPassword.aspx" }
            //			  ]}
            //            ],
            //            "point": [
            //                { "menuid": "3", "icon": "icon-computer", "menuname": "事务中心",
            //				  "menus": [
            //							{ "menuid": "33", "menuname": "测试菜单1", "icon": "icon-user", "url": "../Commonpage/building.htm" },
            //							{ "menuid": "34", "menuname": "测试菜单2", "icon": "icon-organ", "url": "../Commonpage/building.htm" },
            //							{ "menuid": "35", "menuname": "测试菜单3", "icon": "icon-group-key", "url": "../Commonpage/building.htm" },
            //							{ "menuid": "36", "menuname": "测试菜单4", "icon": "icon-key", "url": "../Commonpage/building.htm" }
            //				]},
            //                { "menuid": "4", "icon": "icon-user", "menuname": "其他菜单",
            //                  "menus": [{ "menuid": "41", "menuname": "测试菜单5", "icon": "icon-lock", "url": "../Commonpage/building.htm"}]
            //				}
            //            ],
            //              "1": [{ "menuid": "5", "icon": "icon-computer", "menuname": "行业动态", "menus": [{ "menuid": "1331", "menuname": "政策法规", "icon": "icon-user", "url": "../Expert/ListPolicyLaw.aspx" }, { "menuid": "1333", "menuname": "通知公告", "icon": "icon-user", "url": "../Expert/ListInformation.aspx" }, { "menuid": "1334", "menuname": "动态信息", "icon": "icon-user", "url": "../Expert/ListIndustryNews.aspx"}]}], "1000": [{ "menuid": "1641", "icon": "icon-computer", "menuname": "基础信息", "menus": [{ "menuid": "1504", "menuname": "道路信息", "icon": "icon-user", "url": "../Road/IndexRoad.aspx" }, { "menuid": "1505", "menuname": "桥梁信息", "icon": "icon-user", "url": "../Bridge/IndexBridge.aspx" }, { "menuid": "1506", "menuname": "隧道信息", "icon": "icon-user", "url": "../Tunnel/IndexTunnel.aspx"}] }, { "menuid": "1622", "icon": "icon-computer", "menuname": "路政巡查管理", "menus": [{ "menuid": "1601", "menuname": "排班计划", "icon": "icon-user", "url": "../Schedule/FormList.aspx" }, { "menuid": "1621", "menuname": "PDA终端设备信息", "icon": "icon-user", "url": "../Check/IndexTerminal.aspx" }, { "menuid": "1644", "menuname": "挖掘占道审批信息", "icon": "icon-user", "url": "../Road/ListConstruction.aspx" }, { "menuid": "1645", "menuname": "考勤信息", "icon": "icon-user", "url": "../Check/TotalOnDuty.aspx" }, { "menuid": "1662", "menuname": "责任单位信息", "icon": "icon-user", "url": "../Road/ListAddressbook.aspx" }, { "menuid": "1721", "menuname": "责任单位通讯录", "icon": "icon-user", "url": "../Road/ListAddresslist.aspx" }, { "menuid": "1741", "menuname": "投诉处理", "icon": "icon-user", "url": "../Check/ListJobComplaint.aspx"}] }, { "menuid": "1663", "icon": "icon-computer", "menuname": "巡查监督管理", "menus": [{ "menuid": "1624", "menuname": "巡查问题", "icon": "icon-user", "url": "../Check/indexProblem.aspx" }, { "menuid": "1626", "menuname": "巡查人员监控", "icon": "icon-user", "url": "../GIS/ShowGis.aspx?gettype=1" }, { "menuid": "1643", "menuname": "报警信息", "icon": "icon-user", "url": "../TaskWarning/ListTaskWarning.aspx" }, { "menuid": "1625", "menuname": "任务小结", "icon": "icon-user", "url": "../Check/ListJobSummary.aspx" }, { "menuid": "1642", "menuname": "短信通知", "icon": "icon-user", "url": "../Commonpage/ListSMS.aspx" }, { "menuid": "1761", "menuname": "短信模板", "icon": "icon-user", "url": "../Commonpage/ListSMSTemplate.aspx" }, { "menuid": "1646", "menuname": "整改通知书", "icon": "icon-user", "url": "../Check/ListRectify.aspx"}] }, { "menuid": "1664", "icon": "icon-computer", "menuname": "巡查统计分析", "menus": [{ "menuid": "1661", "menuname": "巡查问题统计", "icon": "icon-user", "url": "../Check/TotalJobProblemN.aspx" }, { "menuid": "1648", "menuname": "任务完成信息", "icon": "icon-user", "url": "../Check/ListTask.aspx" }, { "menuid": "1665", "menuname": "巡查任务统计", "icon": "icon-user", "url": "../Check/TotalTask.aspx"}]}], "3": [{ "menuid": "31", "icon": "icon-computer", "menuname": "用户管理", "menus": [{ "menuid": "32", "menuname": "用户管理", "icon": "icon-user", "url": "../Security/UserFrame.aspx" }, { "menuid": "33", "menuname": "部门管理", "icon": "icon-user", "url": "../Security/GroupFrame.aspx" }, { "menuid": "34", "menuname": "角色管理", "icon": "icon-user", "url": "../Security/ListRoles.aspx" }, { "menuid": "73", "menuname": "功能管理", "icon": "icon-user", "url": "../Security/FunctionFrame.aspx"}] }, { "menuid": "35", "icon": "icon-computer", "menuname": "系统维护", "menus": [{ "menuid": "36", "menuname": "流程设置", "icon": "icon-user", "url": "../App/ListAppForm.aspx" }, { "menuid": "37", "menuname": "申请单管理", "icon": "icon-user", "url": "../App/ListAppApply.aspx" }, { "menuid": "74", "menuname": "流程环节管理", "icon": "icon-user", "url": "../App/ListAppProc.aspx" }, { "menuid": "1285", "menuname": "流程环节用户设置", "icon": "icon-user", "url": "../App/FlowUserFrame.aspx" }, { "menuid": "76", "menuname": "菜单管理", "icon": "icon-user", "url": "../Security/MenuFrame.aspx" }, { "menuid": "75", "menuname": "系统日志", "icon": "icon-user", "url": "../Commonpage/ListSystemLog.aspx" }, { "menuid": "176", "menuname": "数据字典管理", "icon": "icon-user", "url": "../Commonpage/ListMenu.aspx" }, { "menuid": "1667", "menuname": "短信经办人", "icon": "icon-user", "url": "../Commonpage/ListSmsUser.aspx" }, { "menuid": "1422", "menuname": "临时通行口令", "icon": "icon-user", "url": "../Commonpage/SpecialPermit.aspx" }, { "menuid": "1701", "menuname": "整改通知书编码管理", "icon": "icon-user", "url": "../Security/ModifyRectifySerial.aspx"}]}]
            //        }
            #endregion

            Dictionary<string, List<MenuData>> dict = new Dictionary<string, List<MenuData>>();
                                                     
            List<MenuInfo> list = BLLFactory<Menu>.Instance.GetTopMenu(MyConstants.SystemType);
            int i = 0;
            foreach (MenuInfo info in list)
            {
                if (!HasFunction(info.FunctionId))
                {
                    continue;
                }
                           
                List<MenuData> treeList = new List<MenuData>();
                List<MenuNodeInfo> nodeList = BLLFactory<Menu>.Instance.GetTreeByID(info.ID);
                foreach (MenuNodeInfo nodeInfo in nodeList)
                {
                    if (!HasFunction(nodeInfo.FunctionId))
                    {
                        continue;
                    }
                                                                                                                                                                                  
                    MenuData menuData = new MenuData(nodeInfo.ID, nodeInfo.Name, string.IsNullOrEmpty(nodeInfo.WebIcon) ? "icon-computer" : nodeInfo.WebIcon);
                    foreach (MenuNodeInfo subNodeInfo in nodeInfo.Children)
                    {
                        if (!HasFunction(subNodeInfo.FunctionId))
                        {
                            continue;
                        }
                        string icon = string.IsNullOrEmpty(subNodeInfo.WebIcon) ? "icon-organ" : subNodeInfo.WebIcon;
                        menuData.menus.Add(new MenuData(subNodeInfo.ID, subNodeInfo.Name, icon, subNodeInfo.Url));
                    }
                    treeList.Add(menuData);
                }

                //添加到字典里面，如果是第一个，默认用default名称
                string dictName = (i++ == 0) ? "default" : info.ID;
                dict.Add(dictName, treeList);
            }

            string content = ToJson(dict);
            content = RemoveJsonNulls(content);//移除为null的json对象属性
            return Content(content.Trim(','));
        }

        /// <summary>
        /// Removes Json null objects from the serialized string and return a new string
        /// </summary>
        /// <param name="str">The String to be checked</param>
        /// <returns>Returns the new processed string or NULL if null or empty string passed</returns>
        private string RemoveJsonNulls(string str)
        {
            string JsonNullRegEx = "[\"][a-zA-Z0-9_]*[\"]:null[ ]*[,]?";
            string JsonNullArrayRegEx = "\\[( *null *,? *)*]";

            if (!string.IsNullOrEmpty(str))
            {
                Regex regex = new Regex(JsonNullRegEx);
                string data = regex.Replace(str, string.Empty);
                regex = new Regex(JsonNullArrayRegEx);
                return regex.Replace(data, "[]");
            }
            return null;
        }

        /// <summary>
        /// 获取菜单树Json字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuTreeJson()
        {
            List<EasyTreeData> treeList = new List<EasyTreeData>();
            List<SystemTypeInfo> typeList = BLLFactory<SystemType>.Instance.GetAll();
            foreach (SystemTypeInfo typeInfo in typeList)
            {
                //顶级菜单ID默认为-1
                EasyTreeData pNode = new EasyTreeData(typeInfo.OID, typeInfo.Name, "icon-computer");
                treeList.Add(pNode);

                string systemType = typeInfo.OID;//系统标识ID

                //一般情况下，对Ribbon样式而言，一级菜单表示RibbonPage；二级菜单表示PageGroup;三级菜单才是BarButtonItem最终的菜单项。
                List<MenuNodeInfo> menuList = BLLFactory<Menu>.Instance.GetTree(systemType);
                foreach (MenuNodeInfo info in menuList)
                {
                    EasyTreeData item = new EasyTreeData(info.ID, info.Name, "icon-view");
                    pNode.children.Add(item);

                    AddChildNode(info.Children, item);
                }
            }

            string content = ToJson(treeList);
            return Content(content.Trim(','));
        }

        private void AddChildNode(List<MenuNodeInfo> list, EasyTreeData fnode)
        {
            foreach (MenuNodeInfo info in list)
            {
                EasyTreeData item = new EasyTreeData(info.ID, info.Name, "icon-view");
                fnode.children.Add(item);

                AddChildNode(info.Children, item);
            }
        }
    }
}
