using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItem> _portfolioItem;

        private readonly IHostEnvironment _hosting;

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolioItem, IHostEnvironment hosting)
        {
            _hosting = hosting;
            _portfolioItem = portfolioItem;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_portfolioItem.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolioItem.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioVM model)
        {
            if (ModelState.IsValid)
            {
                if(model.File!=null)
                {
                    string uploads = Path.Combine(_hosting.ContentRootPath, @"wwwroot/img/portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\img\\portfolio", model.File.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        ModelState.AddModelError("error", "Image already Existing! ..");
                        return View(model);
                    }
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));   
                }
                PortfolioItem item = new PortfolioItem
                {
                    Description = model.Description,
                    ImgUrl = model.File.FileName,
                    ProjectName = model.ProjectName,
                };
                _portfolioItem.Entity.Insert(item);
                _portfolioItem.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolioItem.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            PortfolioVM portfolioVM = new PortfolioVM
            {
                Id = portfolioItem.Id,
                Description = portfolioItem.Description,
                ProjectName = portfolioItem.ProjectName,
                ImgUrl = portfolioItem?.ImgUrl
            };

            return View(portfolioVM);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\img\\portfolio", model.ImgUrl);
                        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                        string uploads = Path.Combine(_hosting.ContentRootPath, @"wwwroot\\img\\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    PortfolioItem item = new PortfolioItem
                    {
                        Id= model.Id,
                        Description = model.Description,
                        ImgUrl = model.File.FileName,
                        ProjectName = model.ProjectName,
                    };
                    

                    _portfolioItem.Entity.Update(item);
                    _portfolioItem.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolioItem.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var item = _portfolioItem.Entity.GetById(id);
            string itemImgUrl = item.ImgUrl;
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\\img\\portfolio", itemImgUrl);

            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            _portfolioItem.Entity.Delete(id);
            _portfolioItem.Save();
            return RedirectToAction(nameof(Index));
        }
        private bool PortfolioItemExists(Guid id)
        {
            return _portfolioItem.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
