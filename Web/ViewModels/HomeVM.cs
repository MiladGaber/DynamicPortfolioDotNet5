using Core.Entities;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class HomeVM
    {
        public Owner Owner { get; set; }
        public List<PortfolioItem> PortfolioItems { get; set; }
    }
}
