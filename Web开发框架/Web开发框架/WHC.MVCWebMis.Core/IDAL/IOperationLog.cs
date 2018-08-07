using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using WHC.Pager.Entity;
using WHC.Framework.ControlUtil;
using WHC.MVCWebMis.Entity;

namespace WHC.MVCWebMis.IDAL
{
    /// <summary>
    /// 用户关键操作记录
    /// </summary>
	public interface IOperationLog : IBaseDAL<OperationLogInfo>
	{
    }
}