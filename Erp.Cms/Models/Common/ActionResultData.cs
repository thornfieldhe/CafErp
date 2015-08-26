// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionResultData.cs" company="">
//   
// </copyright>
// <summary>
//   The action result data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Cms.Models
{
    using System;

    /// <summary>
    /// The action result data.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ActionResultData<T> : ActionResultStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultData{T}"/> class. 
        /// 
        /// </summary>
        public ActionResultData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultData{T}"/> class. 
        /// </summary>
        /// <param name="result">
        /// </param>
        /// <param name="status">
        /// </param>
        /// <param name="errorCode">
        /// 100:默认为系统异常，其它数值用于需要时使用switch过滤
        /// </param>
        /// <param name="errorMessage">
        /// </param>
        public ActionResultData(
            T result,
            ActionStatuses status = ActionStatuses.OK,
            int errorCode = 0,
            string errorMessage = "")
        {
            this.Data = result;
            this.Status = status;
            this.Message = errorMessage;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultData{T}"/> class. 
        /// 带异常信息的初始化函数
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public ActionResultData(Exception ex)
            : base(ex)
        {
        }

        /// <summary>
        /// 返回值
        /// </summary>
        public T Data
        {
            get; set;
        }
    }

    /// <summary>
    /// 执行状态
    /// </summary>
    public enum ActionStatuses
    {
        /// <summary>
        /// 成功
        /// </summary>
        OK = 0,

        /// <summary>
        ///失败
        /// </summary>
        Error = 1
    }

    /// <summary>
    /// 无返回值封装类
    /// </summary>
    public class ActionResultStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultStatus"/> class. 
        /// 成功信息初始化函数 
        /// </summary>>
        public ActionResultStatus()
        {
            this.Status = ActionStatuses.OK;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultStatus"/> class. 
        /// 带错误信息初始化函数
        /// </summary>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="errorMessage">
        /// The error message.
        /// </param>>
        public ActionResultStatus(int errorCode = 0, string errorMessage = "")
        {
            this.Status = ActionStatuses.Error;
            this.Message = errorMessage;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResultStatus"/> class. 
        /// 异常初始化函数
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>>
        public ActionResultStatus(Exception ex)
        {
            this.Status = ActionStatuses.Error;
            this.Message = ex.Message;
            this.ErrorCode = 100;
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ActionStatuses Status
        {
            get;
            set;
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode
        {
            get; protected set;
        }
    }
}