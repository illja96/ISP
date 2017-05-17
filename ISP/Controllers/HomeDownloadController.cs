using ISP.BLL.ModelActions;
using ISP.DAL.DBModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISP.Controllers
{
    public class HomeDownloadController : Controller
    {
        private DownloadActions action;
        private InternetPackageActions internetPackageActions;
        private TVChannelPackageActions tvChannelPackageActions;

        public HomeDownloadController()
        {
            action = new DownloadActions();
            internetPackageActions = new InternetPackageActions();
            tvChannelPackageActions = new TVChannelPackageActions();
        }

        public FileResult DownloadInternetPackage(Guid id, string format = "txt")
        {
            InternetPackage item = internetPackageActions.GetNotCanceled(id);
            return DownloadInternetPackage(item, format);
        }
        private FileResult DownloadInternetPackage(InternetPackage item, string format = "")
        {
            Stream stream;
            string fileName;

            switch (format)
            {
                case "pdf":
                    fileName = string.Format("ПакетИнтернетУслуг{0}.pdf", item.Name);
                    stream = action.DownloadPDF(item);
                    stream.Seek(0, 0);
                    return File(stream, "application/pdf", fileName);

                case "docx":
                    fileName = string.Format("ПакетИнтернетУслуг{0}.docx", item.Name);
                    stream = action.DownloadDOCX(item);
                    stream.Seek(0, 0);
                    return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);

                default:
                    fileName = string.Format("ПакетИнтернетУслуг{0}.txt", item.Name);
                    stream = action.DownloadTXT(item);
                    stream.Seek(0, 0);
                    return File(stream, "text/plain", fileName);
            }
        }

        public FileResult DownloadTVChannelPackage(Guid id, string format = "txt")
        {
            TVChannelPackage item = tvChannelPackageActions.GetNotCanceled(id);
            return DownloadTVChannelPackage(item, format);
        }
        private FileResult DownloadTVChannelPackage(TVChannelPackage item, string format = "txt")
        {
            Stream stream;
            string fileName;

            switch (format)
            {
                case "pdf":
                    fileName = string.Format("ПакетТВ{0}.pdf", item.Name);
                    stream = action.DownloadPDF(item);
                    stream.Seek(0, 0);
                    return File(stream, "application/pdf", fileName);

                case "docx":
                    fileName = string.Format("ПакетТВ{0}.docx", item.Name);
                    stream = action.DownloadDOCX(item);
                    stream.Seek(0, 0);
                    return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);

                default:
                    fileName = string.Format("ПакетТВ{0}.txt", item.Name);
                    stream = action.DownloadTXT(item);
                    stream.Seek(0, 0);
                    return File(stream, "text/plain", fileName);
            }
        }
    }
}