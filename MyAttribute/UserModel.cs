using MyAttribute.AttributeExtend;
using System;
namespace MyAttribute
{
    /// <summary>
    /// User:实体类(业务信息)
    /// </summary>

    [Custom]
    [Custom(Description = "123")]
    [Custom(Remark = "123")]
    [Custom(Description = "123", Remark = "456")]

    [Custom(123)]
    [Custom(123, Description = "123")]
    [Custom(123, Remark = "123")]
    [Custom(123, Description = "123", Remark = "456")]
    //[CustomAttribute]
    [CustomAttributeChild]
    //数据库表 叫User

    [Table("User")]
    public class UserModel
    {
        //public string TableName = "User";//破坏了类型封装

        //加不加特性，编译出来的IL是一样的
        [Custom]
        [Custom(Description = "123")]
        [Custom(Remark = "123")]
        [Custom(Description = "123", Remark = "456")]

        [Custom(123)]
        [Custom(123, Description = "123")]
        [Custom(123, Remark = "123")]
        [Custom(123, Description = "123", Remark = "456")]
        public int Id { get; set; }

        #region Model
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            set;
            get;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set;
            get;
        }
        /// <summary>
        /// EMaill
        /// </summary>
        [EmailValidate]
        public string Email
        {
            set;
            get;
        }
        //数据库里是E-Mail

        //public string EmailName = "E-Mail";//被破坏了


        /// <summary>
        /// 手提
        /// </summary>
        public string Mobile
        {
            set;
            get;
        }
        /// <summary>
        /// 企业ID
        /// </summary>
        [IntValidate(100, 10000)]
        public int CompanyId
        {
            set;
            get;
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName
        {
            set;
            get;
        }
        /// <summary>
        /// 用户状态  0正常 1冻结 2删除
        /// </summary>
        public int? State
        {
            set;
            get;
        }
        /// <summary>
        /// 用户类型  1 普通用户 2管理员 4超级管理员
        /// </summary>
        public int? UserType
        {
            set;
            get;
        }
        /// <summary>
        /// 最后一次登陆时间
        /// </summary>
        public DateTime? LastLoginTime
        {
            set;
            get;
        }

        /// <summary>
        /// 最后一次修改人
        /// </summary>
        public int? LastModifierId
        {
            set;
            get;
        }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public DateTime? LastModifyTime
        {
            set;
            get;
        }
        #endregion Model

    }
}

