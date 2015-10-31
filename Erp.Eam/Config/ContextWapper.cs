namespace Erp.Eam.Business
{
    using System.Data.Entity;

    using TAF;
    using TAF.Core;

    /// <summary>
    /// 上下文包装类用于封装Contex
    /// </summary>
    internal class ContextWapper : IContextWapper
    {
        public DbContext Context
        {
            get
            {
                var context = new EFDbContext();
                return context;
            }
        }
    }
}
