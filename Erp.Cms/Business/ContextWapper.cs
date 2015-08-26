namespace Erp.Cms.Business
{
    using CAF;

    using Erp.Cms.Business;

    /// <summary>
    /// 上下文包装类用于封装Contex
    /// </summary>
    internal class ContextWapper : SingletonBase<ContextWapper>
    {
        public ApplicationDbContext Context
        {
            get
            {
                var context = new ApplicationDbContext();
                return context;
            }
        }
    }
}
