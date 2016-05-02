﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Woopin.SGC.Common.Models;
using Woopin.SGC.Model.Ventas;
using Woopin.SGC.Services;
using Woopin.SGC.Services.Afip;
using Woopin.SGC.Services.Common;
using Woopin.SGC.Common.Helpers;
using WebMatrix.WebData;
using Woopin.SGC.Model.Exceptions;
using Woopin.SGC.Services.Afip.Model;
using Woopin.SGC.Model.Common;
using Woopin.SGC.Common.App.Session;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Woopin.SGC.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace Woopin.SGC.Web.Scheduler
{
    public class VentasJobs
    {
        private readonly IVentasService ventasService;
        private readonly IVentasReportService ventasReportService;
        private readonly IAfipService afipservice;
        private readonly ISystemService SystemService;
        private readonly IVentasConfigService ventasConfigService;
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public VentasJobs(IVentasConfigService ventasConfigService, IVentasService ventasService, IAfipService afipservice, IVentasReportService ventasReportService, ISystemService SystemService)
        {
            this.ventasService = ventasService;
            this.afipservice = afipservice;
            this.ventasReportService = ventasReportService;
            this.SystemService = SystemService;
            this.ventasConfigService = ventasConfigService;
        }

        public void SolicitarCAE(int IdComprobante, JobHeader header)
        {
            // Initialize job sessiondata
            this.SystemService.InitializeSessionData(header);

            ComprobanteVenta c = this.ventasService.GetComprobanteVentaCompleto(IdComprobante);

            try
            {
                Task<RespuestaCAE> response = this.afipservice.SolicitarCAE(c);

                response.ContinueWith(f =>
                {
                    if (response.Exception != null)
                        throw response.Exception;

                    this.ventasService.SetearCAEComprobanteVenta(IdComprobante, response.Result.CAE, response.Result.Vencimiento);
                });
            }
            catch(AfipServiceException e)
            {
                log.Error(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw e;
            }
        }

        public void RecordarVencimientos()
        {
            #if DEBUG
                return;
            #endif
            IList<ComprobanteVenta> comprobantes = this.ventasReportService.GetAllComprobantesPendientes();
            EmailerService service = new EmailerService();
            string templatePath = AppDomain.CurrentDomain.BaseDirectory + "EmailTemplates\\VentasVencimientos.html";
            
            foreach (var facturas in comprobantes.GroupBy(x => x.MailCobro))
            {
                WMail mail = new WMail();
                mail.To.Add(facturas.First().MailCobro);
                mail.Subject = "Facturas con Vencimiento";

                string templateLineaFactura = "<tr><td>@@NroFactura@@</td><td>@@Importe@@</td><td>@@Fecha@@</td><tr>";
                string htmlFacturas = "";
                foreach (var factura in facturas)
                {
                    if (factura.FechaVencimiento >= DateTime.Now || factura.FechaVencimiento.AddDays(10) >= DateTime.Now)
                    {
                        htmlFacturas += templateLineaFactura.Replace("@@NroFactura@@", factura.GetLetraNumero())
                                                            .Replace("@@Fecha@@", factura.FechaVencimiento.ToString("dd/MM/yyyy"))
                                                            .Replace("@@Importe@@", factura.Total.ToStringArCurrency());
                    }
                }

                if (htmlFacturas == "")
                {
                    continue; // Salta al proximo email
                }

                mail.IsHtml = true;
                StreamReader streamReader = new StreamReader(templatePath);
                mail.Message = streamReader.ReadToEnd().Replace("@@NombreCobro@@", facturas.First().NombreCobro)
                                                       .Replace("@@Facturas@@", htmlFacturas);
                streamReader.Close();

                service.SendEmail(mail);
            }
        }

        public void ImportarClientes(string path, JobHeader jobHeader)
        {
            // Initialize job sessiondata
            this.SystemService.InitializeSessionData(jobHeader);

            IWorkbook hssfwb;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(path).Contains("xlsx"))
                {
                    hssfwb = new XSSFWorkbook(file);
                }
                else
                {
                    hssfwb = new HSSFWorkbook(file);
                }
            }

            ISheet sheet = hssfwb.GetSheet("Clientes");
            List<Cliente> clientes = new List<Cliente>();
            for (int row = 1; row <= sheet.LastRowNum; row++)
            {
                IRow currentRow = sheet.GetRow(row);

                ICell cuitCell = currentRow.GetCell(0);
                string Cuit = null;
                if (cuitCell != null)
                {
                    Cuit = cuitCell.StringCellValue;
                }

                if (currentRow != null && Cuit != null && Cuit.Length > 0)
                {
                    // Creates the comment cell
                    ICell statusCell = currentRow.CreateCell(13);
                    sheet.AutoSizeColumn(13);
                    statusCell.CellStyle.WrapText = true;

                    try
                    {
                        // Force celltypes
                        currentRow.GetCell(5).SetCellType(CellType.String);
                        currentRow.GetCell(6).SetCellType(CellType.String);
                        currentRow.GetCell(7).SetCellType(CellType.String);
                        currentRow.GetCell(8).SetCellType(CellType.String);
                        currentRow.GetCell(9).SetCellType(CellType.String);
                        currentRow.GetCell(10).SetCellType(CellType.String);
                        currentRow.GetCell(11).SetCellType(CellType.String);

                        Cliente c = new Cliente()
                        {
                            RazonSocial = currentRow.GetCell(1).StringCellValue,
                            CUIT = Cuit,
                            CategoriaIva = new CategoriaIVA() { Nombre = currentRow.GetCell(2).StringCellValue },
                            Email = currentRow.GetCell(3).StringCellValue,
                            Localizacion = new Localizacion() { Nombre = currentRow.GetCell(4).StringCellValue },
                            CodigoPostal = currentRow.GetCell(9).StringCellValue,
                            Direccion = currentRow.GetCell(5).StringCellValue,
                            Numero = currentRow.GetCell(6).StringCellValue,
                            Piso = currentRow.GetCell(8).StringCellValue,
                            Departamento = currentRow.GetCell(7).StringCellValue,
                            CondicionVenta = new ComboItem() { Data = currentRow.GetCell(12).StringCellValue },
                            Telefono = currentRow.GetCell(11).StringCellValue,
                            Localidad = currentRow.GetCell(10).StringCellValue
                        };

                        // Model validations.
                        List<ValidationResult> validations;
                        bool isModelValid = new ModelValidatorHelper<Cliente>(c).Validate(out validations);
                        if (!isModelValid)
                        {
                            string message = String.Join("\n", validations.Select(x=>x.ErrorMessage).ToArray());
                            throw new BusinessException(message);
                        }

                        this.ventasConfigService.ImportCliente(c);
                        statusCell.SetCellValue("Importado");
                    }
                    catch (BusinessException be)
                    {
                        statusCell.SetCellValue(be.ErrorMessage);
                    }
                    catch(Exception e)
                    {
                        string msg = "Ocurrio un error inesperado al importar. Vuelva a intentarlo.";
                        statusCell.SetCellValue(msg);
                    }

                }
            }
            string currentFileName = Path.GetFileName(path);
            string newFileName = "resultado_" + currentFileName;
            string newPath = path.Replace(currentFileName,newFileName);
            using (FileStream file = new FileStream(newPath, FileMode.Create, FileAccess.Write))
            {
                hssfwb.Write(file);
                file.Close();
            }   
 
            // ADDNOTIFICATION
        }

    }
}