namespace Erp.Cms.Business
{
    using TAF;

    using Erp.Cms.Business;

    using TAF.Core;
    using System.Data.Entity;

    /// <summary>
    /// 上下文包装类用于封装Contex
    /// </summary>
    internal class ContextWapper : IContextWapper
    {
        public DbContext Context
        {
            get
            {
                var context = new ApplicationDbContext();
                return context;
            }
        }
    }
}
