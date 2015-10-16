// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tools.cs" company="">
//   
// </copyright>
// <summary>
//   工具类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Business
{
    using System;

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