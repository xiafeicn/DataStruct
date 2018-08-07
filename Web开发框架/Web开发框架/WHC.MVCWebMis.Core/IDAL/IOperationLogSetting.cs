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
    /// 记录操作日志的数据表配置
    /// </summary>
	public interface IOperationLogSetting : IBaseDAL<OperationLogSettingInfo>
	{
    }
}