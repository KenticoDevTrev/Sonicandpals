using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Types.Component;
using DynamicRouting.Kentico.MVC;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Meeg.Kentico.ContentComponents.Cms;
using CMS.DocumentEngine.Types.Generic;
using Generic.Controllers;
using DynamicRouting.Interfaces;
using Generic.Repositories.Interfaces;
using Sap.ViewModels;
using Sap.Models;

[assembly: DynamicRouting(typeof(HomeController), new string[] { Home.CLASS_NAME })]
namespace Generic.Controllers
{

    public class HomeController : Controller
    {

        private readonly IDynamicRouteHelper _DynamicRouteHelper;
        private readonly IGeneralDocumentRepository _GeneralDocumentRepo;
        private readonly IComicRepository _ComicRepo;

        public HomeController(IDynamicRouteHelper dynamicRouteHelper, IGeneralDocumentRepository GeneralDocumentRepo,
            IComicRepository ComicRepo
            )
        {
            _DynamicRouteHelper = dynamicRouteHelper;
            _GeneralDocumentRepo = GeneralDocumentRepo;
            _ComicRepo = ComicRepo;
        }

        /// <summary>
        /// Home Route, with the route.cs settings if the page url is empty (just visiting the domain) then "isHomeRoute
        /// </summary>
        /// <param name="isHomeRoute"></param>
        /// <returns></returns>
        public ActionResult Index(bool isHomeRoute = false)
        {
            _DynamicRouteHelper.GetPage();
            Home page = null;
            string[] HomeColumns = new string[] { PageMetaData.CLASS_NAME.Replace(".", "_"), "DocumentName", "DocumentID" };
            // Get from Dynamic Routing if it's not being hit by the 
            if (!isHomeRoute)
            {
                page = _DynamicRouteHelper.GetPage<Home>(Columns: HomeColumns);
            }

            if (page == null)
            {
                page = (Home)_GeneralDocumentRepo.GetDocuments("/Home", Enums.PathSelectionEnum.ParentAndChildren, 
                    PageTypes: new string[] { Home.CLASS_NAME }, 
                    TopNumber: 1, 
                    Columns: HomeColumns)
                    .FirstOrDefault();
            }

            if (page == null)
            {
                return HttpNotFound();
            }

            // Store Page Meta Data if it exists
            HttpContext.Kentico().PageBuilder().Initialize(page.DocumentID);

            HomePageViewModel model = new HomePageViewModel()
            {
                TodaysComic = _ComicRepo.GetTodaysComic()
            };


            // Use template if it has one.
            return View(model);
        }
    }
}