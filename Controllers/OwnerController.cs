using Microsoft.AspNetCore.Mvc;
using PracticalTraining.Models;
using PracticalTraining.Models.DatabaseMANKA;
using PracticalTraining.ViewModels;
using Microsoft.AspNetCore.Http;
using Documentation;
using System.IO;

namespace PracticalTraining.Controllers
{
    public class OwnerController: Controller
    {
        private MANKADocumentation doc;

        public OwnerController() { }

        public IActionResult Index()
        {
            UserInfoList.CreateList();
            return View(new OwnerViewModel());
        }

        public FileResult DownloadPlaceReport()
        {
            doc = new MANKADocumentation();
            return Download(doc.BuildDocumentОтчетОПосещаемостиПомещений());
        }

        public FileResult DownloadClientReport()
        {
            doc = new MANKADocumentation();
            return Download(doc.BuildDocumentОтчетОПотокеКлиентов());
        }

        public FileResult DownloadTimeReport()
        {
            doc = new MANKADocumentation();
            return Download(doc.BuildDocumentТабельУчетаРабочегоВремени());
        }

        public FileResult DownloadPayReport()
        {
            doc = new MANKADocumentation();
            return Download(doc.BuildDocumentТабельРасчетаОплатыТруда());
        }

        public FileResult Download(string path)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = Path.GetFileName(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
