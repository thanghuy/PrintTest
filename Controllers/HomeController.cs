using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrintPDF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConverter _converter;

        public HomeController(IConverter converter)
        {
            this._converter = converter;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pdfSetting = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10},
                DocumentTitle = "PDF Repost"
            };
            var obj = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = "ABC",
                WebSettings = { DefaultEncoding = "utf-8"},
            };
            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = pdfSetting,
                Objects = {obj}
            };
            var file = _converter.Convert(pdf);
            return File(file, "application/pdf");
        }
    }
}
