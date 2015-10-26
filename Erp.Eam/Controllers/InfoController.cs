namespace Erp.Eam.Controllers
{
    using System;
    using System.Web.Mvc;

    using Erp.Eam.Business;
    using Erp.Eam.Models;

    public class InfoController : BaseController
    {
        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteInfo(Guid id)
        {
            try
            {
                var item = Info.Get(id);
                if (item != null)
                {
                    item.Delete();
                    return Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return Json(new ActionResultStatus(10, "信息不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InfoIndex()
        {
            return PartialView("_InfoIndex");
        }

        /// <summary>
        /// 新增单位
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateInfo(InfoView info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("into");
            }

            try
            {
                var item = new Info() { Name = info.Name, Category = info.Category };
                item.Create();
                return Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 更新单位
        /// </summary>
        /// <param name="infoView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateInfo(InfoView infoView)
        {
            try
            {
                var info = Info.Get(infoView.Id);
                if (info != null)
                {
                    info.Name = infoView.Name;
                    info.Category = infoView.Category;
                    info.Save();
                    return Json(new ActionResultStatus(), JsonRequestBehavior.AllowGet);
                }

                return Json(new ActionResultStatus(10, "分类不存在！"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ActionResultStatus(ex), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 获取分类列表
        /// </summary>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="pageIndex">
        /// The page Index.
        /// </param>
        /// <param name="pageSize">
        /// The page Size.
        /// </param>
        /// <returns>
        /// </returns>
        public ActionResult GetInfoList(InfoCategory category, int pageIndex, int pageSize = 20)
        {
            var pager = new Pager<InfoView>() { PageIndex = pageIndex, PageSize = pageSize };
            pager = Info.Pages(pager, r => r.Category == category, r => r.Name, r => new InfoView() { Name = r.Name, Category = r.Category, Id = r.Id }, true);
            return this.Json(pager, JsonRequestBehavior.AllowGet);
        }
    }
}


