using MVC.Presentacion.App_Code;
using MVC.Presentacion.Models;
using MVC.Presentacion.Models.Seguridad;
using MVC.Presentacion.Models.Historico;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelDataReader;
using System.IO;
using System.Data;


namespace MVC.Presentacion.Controllers
{
    public class HistoricoController : Controller
    {
        string tkn = string.Empty;
        public ActionResult Index(int? page, HistoricoVentaModel model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();

            if (TempData["RespuestaDTO"] != null)
            {
                var Respuesta = (RespuestaDTO)TempData["RespuestaDTO"];
                if (Respuesta.Exito)
                    ViewBag.Msj = Respuesta.Mensaje;
                else
                    ViewBag.MensajeError = Validar(Respuesta);
            }

            return View(model);
        }
        public ActionResult Crear(HttpPostedFileBase upload)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();

            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;

                    // We return the interface, so that
                    IExcelDataReader reader = null;

                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }

                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    List<HistoricoVentaModel> listaHistorico = new List<HistoricoVentaModel>();
                    foreach (DataTable table in result.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {                                               
                            listaHistorico.Add(new HistoricoVentaModel
                            {                     
                                Mes = Convert.ToInt16(row.ItemArray[0]),
                                Anio = Convert.ToInt16(row.ItemArray[1]),
                                MontoVenta = Convert.ToDecimal(row.ItemArray[2]),
                                EsPipa = Convert.ToBoolean(row.ItemArray[3]),
                                EsCamioneta = Convert.ToBoolean(row.ItemArray[4]),
                                EsLocal = Convert.ToBoolean(row.ItemArray[5])                                
                            });
                        }
                    }
                    reader.Close();

                   var respuesta = HistoricoServicio.GuardarNuevoHistorico(listaHistorico, tkn);
                   TempData["RespuestaDTO"] = respuesta;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
            //return RedirectToAction("Index");
        }


        public ActionResult Eliminar(int? id)
        {

            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int? id, HistoricoVentaModel model = null)
        {

            return RedirectToAction("Index");
        }

        private string Validar(RespuestaDTO Resp = null)
        {
            string Mensaje = string.Empty;
            ModelState.Clear();
            if (Resp != null)
            {
                if (Resp.ModelStatesStandar != null)
                    foreach (var error in Resp.ModelStatesStandar.ToList())
                        ModelState.AddModelError(error.Key, error.Value);
                if (Resp.MensajesError != null)
                    Mensaje = Resp.MensajesError[0];
            }
            return Mensaje;
        }
    }
}
