using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WHC.MVCWebMis.BLL;
using WHC.MVCWebMis.Entity;
using WHC.Framework.Commons;
using WHC.Framework.ControlUtil;
using WHC.MVCWebMis.Common;

namespace WHC.MVCWebMis.Controllers
{
    /// <summary>
    /// 数据字典大类的控制器
    /// </summary>
    public class DictTypeController :  BusinessController<DictType, DictTypeInfo>
    {
        /// <summary>
        /// 用作下拉列表的菜单Json数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDictJson()
        {
            List<DictTypeInfo> list = baseBLL.GetAll();
            list = CollectionHelper<DictTypeInfo>.Fill("-1", 0, list, "PID", "ID", "Name");

            List<CListItem> itemList = new List<CListItem>();
            foreach (DictTypeInfo info in list)
            {
                itemList.Add(new CListItem(info.Name, info.ID));
            }
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取树形展示数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTreeJson()
        {
            List<TreeData> treeList = new List<TreeData>();
            List<DictTypeInfo> typeList = BLLFactory<DictType>.Instance.Find("PID='-1' ");
            foreach (DictTypeInfo info in typeList)
            {
                TreeData node = new TreeData(info.ID, info.PID, info.Name);
                GetTreeJson(info.ID, node);

                treeList.Add(node);
            }
            string json = ToJson(treeList);
            return Content(json);
        }

        /// <summary>
        /// 递归获取树形信息
        /// </summary>
        /// <returns></returns>
        private void GetTreeJson(string PID, TreeData treeNode)
        {
            string condition = string.Format("PID='{0}' ", PID);
            List<DictTypeInfo> nodeList = BLLFactory<DictType>.Instance.Find(condition);
            StringBuilder content = new StringBuilder();

            foreach (DictTypeInfo model in nodeList)
            {
                TreeData node = new TreeData(model.ID, model.PID, model.Name);
                treeNode.children.Add(node);

                GetTreeJson(model.ID, node);
            }
        }

    }
}
