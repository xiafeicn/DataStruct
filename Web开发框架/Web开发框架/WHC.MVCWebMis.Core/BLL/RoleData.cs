using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.MVCWebMis.Entity;
using WHC.MVCWebMis.IDAL;
using WHC.Framework.ControlUtil;
using WHC.Framework.Commons;

namespace WHC.MVCWebMis.BLL
{
    /// <summary>
    /// 角色的数据权限
    /// </summary>
	public class RoleData : BaseBLL<RoleDataInfo>
    {
        public RoleData() : base()
        {
            base.Init(this.GetType().FullName, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// 获取用户所属角色对应的数据权限集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public List<RoleDataInfo> FindByUser(int userId)
        {
            //获取用户包含的角色
            List<RoleInfo> rolesByUser = BLLFactory<Role>.Instance.GetRolesByUser(userId);
            List<int> roleList = new List<int>();
            foreach (RoleInfo info in rolesByUser)
            {
                roleList.Add(info.ID);
            }

            //根据角色获取对应的数据权限集合
            List<RoleDataInfo> list = new List<RoleDataInfo>();
            foreach (int roleId in roleList)
            {
                RoleDataInfo info = FindByRoleId(roleId);
                if (info != null)
                {
                    list.Add(info);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据角色ID获取对应的记录对象
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public RoleDataInfo FindByRoleId(int roleId)
        {
            string condition = string.Format("Role_ID = {0}", roleId);
            return baseDal.FindSingle(condition);
        }

        /// <summary>
        /// 保存角色的数据权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="belongCompanys">包含公司</param>
        /// <param name="belongDepts">包含部门</param>
        /// <returns></returns>
        public bool UpdateRoleData(int roleId, string belongCompanys, string belongDepts)
        {
            bool result = false;
            RoleDataInfo info = FindByRoleId(roleId);
            if (info != null)
            {
                info.BelongCompanys = belongCompanys;
                info.BelongDepts = belongDepts;

                result = baseDal.Update(info, info.ID);
            }
            else
            {
                info = new RoleDataInfo();
                info.Role_ID = roleId;
                info.BelongCompanys = belongCompanys;
                info.BelongDepts = belongDepts;

                result = baseDal.Insert(info);
            }
            return result;
        }

        /// <summary>
        /// 获取数据库的配置，角色数据权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns></returns>
        public Dictionary<int, int> GetRoleDataDict(int roleID)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            //获取用户的角色权限
            RoleDataInfo roleDataInfo = FindByRoleId(roleID);
            if (roleDataInfo != null)
            {
                //包含公司
                if (!string.IsNullOrEmpty(roleDataInfo.BelongCompanys))
                {
                    string[] companyArray = roleDataInfo.BelongCompanys.Split(',');
                    if (companyArray != null)
                    {
                        foreach (string company in companyArray)
                        {
                            int id = company.ToInt32();
                            if (!dict.ContainsKey(id))
                            {
                                dict.Add(id, id);
                            }
                        }
                    }
                }
                //包含部门
                if (!string.IsNullOrEmpty(roleDataInfo.BelongDepts))
                {
                    string[] deptArray = roleDataInfo.BelongDepts.Split(',');
                    if (deptArray != null)
                    {
                        foreach (string dept in deptArray)
                        {
                            int id = dept.ToInt32();
                            if (!dict.ContainsKey(id))
                            {
                                dict.Add(id, id);
                            }
                        }
                    }
                }
                //排除部门

            }
            return dict;
        }
    }
}
