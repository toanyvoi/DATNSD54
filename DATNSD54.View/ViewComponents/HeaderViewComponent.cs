using Microsoft.AspNetCore.Mvc;

namespace DATNSD54.View.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
