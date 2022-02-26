using LibraryCoreProject.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreProject.ViewComponents
{
    public class TopMenuViewComponent : ViewComponent
    {
        private readonly MyDBContext _context;

        public TopMenuViewComponent(MyDBContext context)
        {
            _context = context;

        }
        //view components have only one action called InvokeAsync()
        public async Task<IViewComponentResult> InvokeAsync(string sid)
        {
            var model = _context.Books.Where(xx => xx.catid == sid).ToList();
            //Default view for view component is named as Default
            return await Task.FromResult((IViewComponentResult)View("Default", model));
        }
    }

}
