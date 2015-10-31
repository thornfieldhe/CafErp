namespace Erp.Eam.Business
{
    using System.Data.Entity;

    using CAF;

    /// <summary>
    /// 上下文包装类用于封装Contex
    /// </summary>
    internal class ContextWapper : SingletonBase<ContextWapper>, IContextWapper
    {
        public DbContext Context
        {
            get
            {
                var context = new DbContext();
                return context;
            }
        }
    }
}
