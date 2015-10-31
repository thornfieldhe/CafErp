namespace Erp.Eam.Business
{
    public interface IContextWapper
    {
        DbContext Context
        {
            get; 
        }
    }
}