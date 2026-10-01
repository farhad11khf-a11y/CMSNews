using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSNews.Models.Models;
using CMSNews.Models.Context;
using CMSNews.Service.Service;
using CMSNews.Models.ViewModels;
using CMSNews.App_Start;

namespace CMSNews.Controllers
{
    public class _PartialNewsController : Controller
    {
        DbCMSNewsContext db = new DbCMSNewsContext();
        NewsGroupService _newsGroupService;
        NewsService _newsService;

        public _PartialNewsController()
        {
            _newsGroupService = new NewsGroupService(db);
            _newsService = new NewsService(db);
        }
        public ActionResult ShowNewsGroup(int locId)
        {
            ViewBag.locId = locId;
            var newsGroups = _newsGroupService.GetAll();
            List<NewsGroupsViewModel> newsGroupsViewModels = AutoMapperConfig
                .mapper.Map<IEnumerable<NewsGroup>, List<NewsGroupsViewModel>>(newsGroups);
            return PartialView(newsGroupsViewModels);
        }
        public ActionResult LatestNews(int count)
        {

            var latestnews = _newsService.GetAll()
                .Where(t => t.IsActive).OrderByDescending(u => u.NewsId).Take(count);

            List<NewsViewModel> latestnewsViewModels = AutoMapperConfig
                .mapper.Map<IEnumerable<News>, List<NewsViewModel>>(latestnews);
            return PartialView(latestnewsViewModels);
        }
        public ActionResult MostViewedNews(int count)
        {

            var mostViewedNews = _newsService.GetAll()
                .Where(t => t.IsActive).OrderByDescending(u => u.see).Take(count);

            List<NewsViewModel> mostViewedNewsViewModels = AutoMapperConfig
                .mapper.Map<IEnumerable<News>, List<NewsViewModel>>(mostViewedNews);
            return PartialView(mostViewedNewsViewModels);
        }
        public ActionResult LatestNews1()
        {

            var latestnews = _newsService.GetAll()
                .Where(t => t.IsActive).ToList().LastOrDefault();

           NewsViewModel latestnewsViewModels = AutoMapperConfig
                .mapper.Map<News, NewsViewModel>(latestnews);
            return PartialView(latestnewsViewModels);
        }
        public ActionResult Details(int id)
        {

            var detailsNews = _newsService.GetEntity(id);
            if (detailsNews == null || !detailsNews.IsActive)
            {
                return HttpNotFound();
            }
            detailsNews.see++;
            _newsService.Update(detailsNews);
            _newsService.Save();

            NewsViewModel detailsNewsViewModels = AutoMapperConfig
                 .mapper.Map<News, NewsViewModel>(detailsNews);
            return View(detailsNewsViewModels);
        }
        [Route("Nees")]
        public ActionResult ShowNewsList(int? id)
        {

            var listnews = _newsService.GetAll().Where(t => t.IsActive).OrderByDescending(u => u.RegisterDate).ToList();
            if (id != null)
            {
                listnews = listnews.Where(t => t.NewsGroupId == id).ToList();
            }
            List<NewsViewModel> listNewsViewModels = AutoMapperConfig
                .mapper.Map<IEnumerable<News>, List<NewsViewModel>>(listnews);
            return View(listNewsViewModels);
        }

    } 
}