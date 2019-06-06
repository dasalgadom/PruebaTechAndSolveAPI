using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using PruebaTechAndSolve.Business.Interface;
using PruebaTechAndSolve.Business.Implentation;
using System.IO;
using System.Collections.Generic;

namespace PruebaTechAndSolve.Controllers
{
    [RoutePrefix("api/UploadFile")]
    public class FileController : ApiController
    {

        IOperationsBusiness operationbusiness => new OperationsBusiness();

        public FileController()
        {
        }

        [Route("file")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadFile(int document)
        {
            var httpRequest = HttpContext.Current.Request;
            var txtfile = httpRequest.Files["fileinput"];
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/File/");

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            txtfile.SaveAs(path + txtfile.FileName);
            string fileText = System.IO.File.ReadAllText(path + txtfile.FileName);

            var elements = fileText.Split('\n');
            var result = operationbusiness.CalculateProcess(elements);

            if (result.SuccessfulOperation)
            {
                bool isExists = Directory.Exists(path);
                if (!isExists)
                {
                    Directory.CreateDirectory(path);
                }
                var NameDocument = String.Format("{0}-{1}.txt", "File_Output", DateTime.Now.ToString("yyyy-MM-dd-hhmmss"));
                var pathFullFinal = Path.Combine(path, NameDocument);

                if (!File.Exists(pathFullFinal))
                {
                    using (StreamWriter sw = File.CreateText(pathFullFinal))
                    {
                        foreach (var item in result.Result)

                        {
                            sw.WriteLine(item);
                        }
                    }
                }

                var response = await operationbusiness.SaveProcess(document, pathFullFinal);

                if (response.SuccessfulOperation) return Content(HttpStatusCode.OK, result.Result);
                return Content(HttpStatusCode.InternalServerError, "Error");
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, "Error");
            }
        }

    }
}
