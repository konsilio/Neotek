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
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Echovoice.JSON;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml;
using System.Net;

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

            if (TempData["year"] != null)
            {
                model.Years = (List<YearsDTO>)TempData["year"];
                TempData["year"] = model.Years;
            }
            else
            {
                List<YearsDTO> listYears = ObtenerAños();
                model.Years = listYears;
            }
            ViewBag.json = TempData["json"];
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

                    if (listPreCarga != null || listPreCarga.Count > 0)
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
        public ActionResult PreCarga(HttpPostedFileBase preCarga)
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
            string res = HistoricoServicio.GetJson(modelo, tkn);
            //TempData["json"] = HttpUtility.JavaScriptStringEncode(res);
            TempData["json"] = res;

            var temp = JsonConvert.DeserializeObject<JsonDTO>(res);
            TempData["data"] = temp.data;
            TempData["meses"] = temp.labels;
            TempData["year"] = modelo.Years;
            return RedirectToAction("HistoricoVentas", modelo);
        }
        public ActionResult GenerarExcel(HistoricoVentasConsulta modelo = null)
        {
            if (Session["StringToken"] == null) return RedirectToAction("Index", "Home");
            tkn = Session["StringToken"].ToString();
            ExcelPackage sLDocument = new ExcelPackage();

            modelo.Years = (List<YearsDTO>)TempData["year"];
            TempData["year"] = modelo.Years;

            var worksheet = sLDocument.Workbook.Worksheets.Add("Ventas Generales");
            var datos = (List<object>)TempData["data"];
            var mes = (List<string>)TempData["meses"];
            List<YearsDTO> Montototal = HistoricoServicio.GetVentasTotalesxMes(modelo, tkn);
            string pahtFile = "";
 
            //create a new piechart of type Line
            ExcelBarChart lineChart = worksheet.Drawings.AddChart("BarChart", eChartType.ColumnClustered) as ExcelBarChart;
            if (modelo.IdTipoReporte == 1)
            {

                pahtFile = "VentasGeneral.xlsx";
                var rangeLabel = worksheet.Cells["B1:M1"];
                var rows = 2;
                var rowsMes = 2;
                for (int r = 0; r < datos.Count; r++)
                {
                    char[] charArray = datos[r].ToString().ToCharArray();
                    rows = rowsMes;

                    //create the ranges for the chart
                    var range1 = worksheet.Cells["B" + rows.ToString() + ":K" + rows.ToString()];
                    //add the ranges to the chart
                    lineChart.Series.Add(range1, rangeLabel);
                    if (charArray[6].Equals('y'))
                    {
                        worksheet.Cells[rows, 1].Value = charArray[11].ToString() + charArray[12].ToString() + charArray[13].ToString() + charArray[14].ToString();
                    }
                    lineChart.Series[r].Header = worksheet.Cells["A" + rows.ToString()].Value.ToString();
                    rows = 2;
                    for (int i = 0; i < mes.Count; i++)
                    {
                        char[] charMes = mes[i].ToCharArray();

                        string mesfila = "";

                        for (int x = 0; x < charMes.Length; x++)
                        {
                            mesfila = mesfila += charMes[x];
                        }

                        if (mesfila != "")
                        {
                            worksheet.Cells[1, rows].Value = mesfila;
                        }

                        for (int o = 0; o < Montototal[r].MesesVenta.Count; o++)
                        {
                            if (Montototal[r].MesesVenta[o].mes == mesfila)
                            {
                                worksheet.Cells[rowsMes, rows].Value = Montototal[r].MesesVenta[o].montoTotal;
                            }
                        }

                        rows++;
                    }
                    rowsMes++;
                }


                //set the title
                lineChart.Title.Text = "Venta General";
            }
            else
            {

                if (modelo.IdTipoReporte == 2)
                {
                    pahtFile = "PipasvsCamionetas.xlsx";
                }
                else
                {
                    pahtFile = "VentasLocalvsForaneas.xlsx";
                }


                var rangeLabel = worksheet.Cells["B1:M1"];
                var rowsMes = 2;
                var rowsaños = 2;
                var rowPipa = 2;
                var tempa = "";
                var rows = 2;
                var cont = 0;
                List<string> rangelabe = new List<string>();



                for (int r = 0; r < datos.Count; r++)
                {
                    char[] charArray = datos[r].ToString().ToCharArray();
                    var m = datos[r].ToString().Split('{');



                    foreach (var item in m)
                    {
                        var rowCamio = 1;

                        if (item != "")
                        {
                            var p = m[1].Split(',');
                            foreach (var prop in p)
                            {
                                var valores = prop.ToString().Split(':');
                                if (valores.ToList()[0].Contains("y"))
                                {
                                    //El mes y el año
                                    var año = valores[1].ToString().Split(' ').ToList()[2];
                                    char[] años = año.ToCharArray();

                                    if (tempa == "" || tempa != año)
                                    {
                                        for (int i = 0; i < mes.Count; i++)
                                        {
                                            var range1 = worksheet.Cells["B" + rowsaños.ToString() + ":K" + rowsaños.ToString()];
                                            worksheet.Cells[rowsaños, 1].Value = mes[i] + "-" + años[2] + años[3];
                                            rangelabe.Add(mes[i] + "-" + años[2] + años[3]);
                                            lineChart.Series.Add(range1, rangeLabel);
                                            rowsaños++;
                                        }

                                        if (tempa != "")
                                        {
                                            rows = 2;
                                            cont += 2;
                                            rowPipa = 2;
                                        }
                                    }

                                    if (r >= 3)
                                    {
                                        rowCamio += cont;
                                        //rowPipa++; 
                                    }

                                    var meses = valores[1].ToString().Split(' ').ToList()[1];
                                    char[] tamañoMes = meses.ToCharArray();
                                    worksheet.Cells[1, rows].Value = meses.Substring(1, tamañoMes.Length - 1);

                                    rows++;
                                    tempa = año;
                                    //rowCamio++;
                                }

                                if (valores.ToList()[0].Contains("a"))
                                {
                                    //Ventas de Cmaionetas
                                    var sumaCamioneta = valores[1].ToString().Split(' ').ToList()[1];
                                    char[] tamSuma = sumaCamioneta.ToCharArray();


                                    if (tamSuma[1] == '0')
                                    {
                                        var doubleValue = Double.Parse(sumaCamioneta.Substring(1, tamSuma.Length - 2));
                                        worksheet.Cells[rowCamio, rowPipa].Value = doubleValue;
                                    }
                                    else
                                    {
                                        var doubleValue = Double.Parse(sumaCamioneta.Substring(1, tamSuma.Length - 2));
                                        worksheet.Cells[rowCamio, rowPipa].Value = doubleValue;
                                    }

                                    sumaCamioneta = "";
                                    //rowPipa++;    
                                }

                                if (valores.ToList()[0].Contains("b"))
                                {
                                    //Ventas de Pipas

                                    var sumaPipa = valores[1].ToString().Split(' ').ToList()[1];
                                    char[] tamSumaPi = sumaPipa.ToCharArray();
                                    if (tamSumaPi[1] == '0')
                                    {
                                        var doubleValue = Double.Parse(sumaPipa.Substring(1, tamSumaPi.Length - 5));
                                        worksheet.Cells[rowCamio, rowPipa].Value = doubleValue;
                                    }
                                    else
                                    {

                                        var doubleValue = Double.Parse(sumaPipa.Substring(1, tamSumaPi.Length - 5));

                                        worksheet.Cells[rowCamio, rowPipa].Value = doubleValue;
                                    }
                                    sumaPipa = "";

                                }
                                rowCamio++;
                            }

                            rowPipa++;


                        }
                    }


                }

                for (int e = 0; e < rangelabe.Count(); e++)
                {
                    lineChart.Series[e].Header = worksheet.Cells["A" + rowsMes.ToString()].Value.ToString();
                    rowsMes++;
                }
                rangelabe = null;
                rowsaños++;
                //rowPipa = 2;
                //set the title
                if (modelo.IdTipoReporte == 2)
                {
                    lineChart.Title.Text = "PipasvsCamionetas";
                }
                else
                {
                    lineChart.Title.Text = "LocalvsForanea";
                }

            }

            //position of the legend
            lineChart.Legend.Position = eLegendPosition.Right;

            //size of the chart
            lineChart.SetSize(700, 600);

            //add the chart at cell B6
            lineChart.SetPosition(1, 0, 8, 0);

            //  FileInfo infor = new FileInfo(pahtFile);

            MemoryStream memoryStream = new MemoryStream();
            sLDocument.SaveAs(memoryStream);
            memoryStream.Position = 0;
            TempData["data"] = datos;
            TempData["meses"] = mes;

            return new FileStreamResult(memoryStream, "application/vnd.ms-excel") { FileDownloadName = pahtFile };
            // Descraga(pahtFile, memoryStream);

           // return RedirectToAction("HistoricoVentas", modelo);

        }
        public List<YearsDTO> ObtenerAños()
        {
            var respuesta = HistoricoServicio.GetYears(tkn);
            return respuesta;
        }

        //public ActionResult Descraga(string File, MemoryStream ms)
        //{
        //    return new FileStreamResult(ms, "application/vnd.ms-excel") { FileDownloadName = File };

        //}
        
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
