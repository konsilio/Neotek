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
        public List<HistoricoVentaModel> listPreCarga = new List<HistoricoVentaModel>();

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
            listPreCarga = (List<HistoricoVentaModel>)TempData["HistoricoVentas"];
            TempData["HistoricoVentas"] = listPreCarga;
            ViewBag.HistoricoVentas = listPreCarga;
            return View(model);
        }
        public ActionResult HistoricoVentas(int? page, HistoricoVentasConsulta model = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();

            List<YearsDTO> listYears = ObtenerAños();
    
            model.Years = listYears;
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
            
            listPreCarga = (List<HistoricoVentaModel>)TempData["HistoricoVentas"];

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
                                MontoVenta = Convert.ToDouble(row.ItemArray[2]),
                                EsPipa = Convert.ToBoolean(row.ItemArray[3]),
                                EsCamioneta = Convert.ToBoolean(row.ItemArray[4]),
                                EsLocal = Convert.ToBoolean(row.ItemArray[5])
                            });
                        }
                    }
                    reader.Close();

                 
                        var respuesta = HistoricoServicio.GuardarNuevoHistorico(listaHistorico, tkn);
                        TempData["RespuestaDTO"] = respuesta;
                                                             
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");

                    if(listPreCarga != null || listPreCarga.Count > 0)
                    {
                        var respuesta = HistoricoServicio.GuardarNuevoHistorico(listPreCarga, tkn);
                        TempData["RespuestaDTO"] = respuesta;
                        listPreCarga = null;
                     
                    }
                }
            }

            return RedirectToAction("Index");
            //return RedirectToAction("Index");
        }   
        public ActionResult PreCarga (HttpPostedFileBase preCarga)
        {
            if (preCarga != null && preCarga.ContentLength > 0)
            {
                // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                // to get started. This is how we avoid dependencies on ACE or Interop:
                Stream stream = preCarga.InputStream;

                // We return the interface, so that
                IExcelDataReader reader = null;

                if (preCarga.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (preCarga.FileName.EndsWith(".xlsx"))
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
                            MontoVenta = Convert.ToDouble(row.ItemArray[2]),
                            EsPipa = Convert.ToBoolean(row.ItemArray[3]),
                            EsCamioneta = Convert.ToBoolean(row.ItemArray[4]),
                            EsLocal = Convert.ToBoolean(row.ItemArray[5])
                        });
                    }
                }
                reader.Close();                
                TempData["HistoricoVentas"] = listaHistorico;                
            }
            else
            {
                ModelState.AddModelError("File", "Please Upload Your file");
            }
            return RedirectToAction("Index");
        }

        public ActionResult ObtenerJsonGrf(HistoricoVentasConsulta modelo = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            var otroGato = modelo;            
            var respuesta = HistoricoServicio.GetJson(modelo,tkn);
            
            return RedirectToAction("HistoricoVentas",modelo) ;
        }
        public List<YearsDTO> ObtenerAños()
        {
            var respuesta = HistoricoServicio.GetYears(tkn);
            return respuesta;
        }

        public ActionResult Eliminar(int? id)
        {

            return RedirectToAction("Index");
        }
        public ActionResult Modificar(int? id, HistoricoVentaModel model = null)
        {

            return RedirectToAction("Index");
        }

        public ActionResult Obtener()
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();

            try
            {
                var respuesta = HistoricoServicio.GetListaHistoricos(tkn);
                ViewBag.HistoricosVentas = respuesta;
                return View(respuesta);
            }
            catch (Exception ex)
            {          
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }

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
