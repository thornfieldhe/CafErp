// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IocConfig.cs" company="">
//   
// </copyright>
// <summary>
//   依赖注入配置
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Business
{
    using Autofac;

    using TAF.Core;

    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class IocConfig : TAF.DI.ConfigBase
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="builder">
        /// 容器生成器
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<ContextWapper>().As<IContextWapper>();
        }
    }
}