// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StockInController.cs" company="">
//   
// </copyright>
// <summary>
//   入库单控制器
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Erp.Eam.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using Erp.Eam.Models;

    using Microsoft.AspNet.Identity;

    using TAF;
    using TAF.Mvc;
    using TAF.Utility;

    /// <summary>
    /// The manage controller.
    /// </summary>
    public class StockInController : BaseController<StockIn, StockInView, StockInListView>
    {
        public ActionResult BatchAdding(IList<StockChangeListView> list)
        {
            try
            {
                var stockIn = new StockIn()
                {
                    Details = Mapper.Map<List<StockInDetail>>(list),
                    CreatedBy = User.Identity.GetUserId(),
                    ModifyBy = User.Identity.GetUserId()
                };
                stockIn.Create();
                return Json(new ActionResultStatus());
            }
            catch (Exception ex)
            {
                return Json(new ActionResultStatus(ex));
            }
        }
    }
}



