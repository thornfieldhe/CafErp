﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
//   
// </copyright>
// <summary>
//   工具类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Cms.Business
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 工具类
    /// </summary>
    public class Tools
    {
        public static string GetTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssff");
        }
    }
}