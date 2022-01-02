using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;

        public HomeController(IUnitOfWork<Owner> owner, IUnitOfWork<PortfolioItem> portfolio)
        {
            _owner = owner;
            _portfolio = portfolio;
        }
        public IActionResult Index()
        {
            var homeVM = new HomeVM()
            {
                Owner = _owner.Entity.GetAll().First(),
                PortfolioItems = _portfolio.Entity.GetAll().ToList()
            };
            return View(homeVM);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
