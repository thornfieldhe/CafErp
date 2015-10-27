using System.Web.Mvc;
namespace Erp.Eam.Controllers
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using CAF;
    using Erp.Eam.Business;
    using Erp.Eam.Models;

    public class BaseController<K, T> : Controller where K : EfBusiness<K>, new() where T : IEntityBase, new()
    {
        public virtual ActionResult Index()
        {
            return PartialView("_Index");
        }

        public virtual ActionResult Get(Guid id)
        {
            var item = EfBusiness<K>.Get(id);
            return this.Json(new ActionResultData<K>(item), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetView(Guid id)
        {
            var item = EfBusiness<K>.Get(id);
            var result = Mapper.Map<T>(item);
            return this.Json(new ActionResultData<T>(result), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetAll()
        {
            var items = EfBusiness<K>.GetAll();
            return this.Json(new ActionResultData<List<K>>(items), JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult GetViews()
        {
            var items = EfBusiness<K>.GetAll();
            var result = Mapper.Map<List<T>>(items);
            return this.Json(new ActionResultData<List<T>>(result), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Pager(int pageIndex, int pageSize = 20)
        {
            var pager = new Pager<T>() { PageIndex = pageIndex, PageSize = pageSize };
            pager = EfBusiness<K>.Pages<T>(pager, true);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult Insert(T value)
        {
            try
            {
                var item = Mapper.Map<K>(value);
                item.Create();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual ActionResult Update(T value)
        {
            try
            {
                var item = EfBusiness<K>.Get(value.Id);
                Mapper.Map(value, item);
                item.Save();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual ActionResult Delete(Guid id)
        {
            try
            {
                var item = EfBusiness<K>.Get(id);
                item.Delete();
                return this.Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return this.Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }
    }
}