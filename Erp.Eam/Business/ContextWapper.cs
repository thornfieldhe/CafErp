namespace Erp.Eam.Business
{
    using CAF;

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
