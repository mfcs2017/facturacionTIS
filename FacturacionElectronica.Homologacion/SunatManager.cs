﻿/*
 * Url de info: http://orientacion.sunat.gob.pe/index.php/empresas-menu/comprobantes-de-pago-empresas/comprobantes-de-pago-electronicos-empresas/see-desde-los-sistemas-del-contribuyente/2-comprobantes-que-se-pueden-emitir-desde-see-sistemas-del-contribuyente/factura-electronica-desde-see-del-contribuyente/3544-servicio-web-de-consultas
 * Method : getStatusCdr();
 */
using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using FacturacionElectronica.Homologacion.Res;
using FacturacionElectronica.Homologacion.Security;

namespace FacturacionElectronica.Homologacion
{
    /// <summary>
    /// Controlador para comunicaicon con WebServices de SUNAT
    /// </summary>
    public class SunatManager
    {
        #region Fields
        private readonly SolConfig _config;
        private readonly string _url; 
        #endregion

        #region Construct

        /// <summary>
        /// Administrador de WebService de la Sunat. Necesita Clave SOL
        /// </summary>
        /// <param name="config">Config</param>
        public SunatManager(SolConfig config)
        {
            _config = config;
            _url = GetUrlService(config.Service);
        }
        #endregion

        #region Method Sunat

        /// <summary>
        /// Recibe la ruta XML con un único formato digital y devuelve la Constancia de Recepción – SUNAT. 
        /// </summary>
        /// <param name="pathFile">Ruta del Archivo XML</param>
        /// <param name="content">Contenido del archivo</param>
        /// <returns>La respuesta contenida en el XML de Respuesta de la Sunat, si existe</returns>
        public async Task<SunatResponse> SendDocument(string pathFile, byte[] content)
        {
            var fileToZip = pathFile + Resources.ExtensionFile;
            var nameOfFileZip = pathFile + Resources.ExtensionZipFile;

            var response = new SunatResponse
            {
                Success = false
            };
            try
            {
                var zipBytes = ProcessZip.CompressFile(fileToZip, content);
                using (var service = ServiceHelper.GetService<ClientService.billServiceClient>(_config, _url))
                {
                    var result = await service.sendBillAsync(nameOfFileZip, zipBytes);

                    var outputXml = ProcessZip.ExtractFile(result.applicationResponse, Path.GetTempPath());
                    response = new SunatResponse
                    {
                        Success = true,
                        ApplicationResponse = ProcessXml.GetAppResponse(outputXml),
                        ContentZip = result.applicationResponse
                    }; 
                }
            }
            catch (FaultException ex)
            {
                response.Error = new ErrorResponse
                {
                    Code = ex.Code.Name,
                    Description = ProcessXml.GetDescriptionError(ex.Code.Name) ?? ex.Message
                };
            }
            catch (Exception er)
            {
                response.Error = new ErrorResponse
                {
                    Description = er.Message
                };
            }
            
            return response;
        }

        /// <summary>
        /// Envia una Resumen de Boletas o Comunicaciones de Baja a Sunat
        /// </summary>
        /// <param name="pathFile">Ruta del archivo XML que contiene el resumen</param>
        /// <param name="content">Contenido del archivo</param>
        /// <returns>Retorna un estado booleano que indica si no hubo errores, con un string que contiene el Nro Ticket,
        /// con el que posteriormente, utilizando el método getStatus se puede obtener Constancia de Recepcióno</returns>
        public async Task<TicketResponse> SendSummary(string pathFile, byte[] content)
        {
            var fileToZip = pathFile + Resources.ExtensionFile;
            var nameOfFileZip = pathFile + Resources.ExtensionZipFile;
            var res = new TicketResponse();
            try
            {
                var zipBytes = ProcessZip.CompressFile(fileToZip, content);
                using (var service = ServiceHelper.GetService<ClientService.billServiceClient>(_config, _url))
                {
                    var result = await service.sendSummaryAsync(nameOfFileZip, zipBytes);
                    res.Ticket = result.ticket;
                    res.Success = true;
                }
            }
            catch (FaultException ex)
            {
                res.Error = new ErrorResponse
                {
                    Code = ex.Code.Name,
                    Description = ProcessXml.GetDescriptionError(ex.Code.Name)
                };
            }
            catch (Exception er)
            {
                res.Error = new ErrorResponse
                {
                    Description = er.Message
                };
            }
            return res;
        }
        /// <summary>
        /// Devuelve un objeto que indica el estado del proceso y en caso de haber terminado, devuelve adjunta la ruta del XML que contiene la Constancia de Recepción
        /// </summary>
        /// <param name="pstrTicket">Ticket proporcionado por la sunat</param>
        /// <returns>Estado del Ticket, y la ruta de la respuesta si existe</returns>
        public async Task<SunatResponse> GetStatus(string pstrTicket)
        {
            var res = new SunatResponse();
            try
            {
                using (var service = ServiceHelper.GetService<ClientService.billServiceClient>(_config, _url))
                {
                    var result = await service.getStatusAsync(pstrTicket);
                    var response = result.status;
                    switch (response.statusCode)
                    {
                        case "0":
                        case "99":
                            res.Success = true;
                            var pathXml = ProcessZip.ExtractFile(response.content, Path.GetTempPath());
                            res.ApplicationResponse = ProcessXml.GetAppResponse(pathXml);
                            res.ContentZip = response.content;
                            break;
                        case "98":
                            res.Success = false;
                            res.Error = new ErrorResponse { Description = "En Proceso..."};
                            break;
                    }
                }
            }
            catch (FaultException ex)
            {
                res.Error = new ErrorResponse
                {
                    Code = ex.Code.Name,
                    Description = ProcessXml.GetDescriptionError(ex.Code.Name),
                };
            }
            catch (Exception er)
            {
                res.Error = new ErrorResponse
                {
                    Description = er.Message,
                };
            }
            return res;
        }
        /// <summary>
        ///  Obtiene el estado de un Comprobante
        /// </summary>
        /// <param name="ruc">Es el ruc del emisor del comprobante de pago a consultar</param>
        /// <param name="comprobante">Un Comprobante a consultar</param>
        /// <returns></returns>
        public async Task<StatusCompResponse> GetStatusCdr(string ruc, ComprobanteEletronico comprobante)
        {
            var res = new StatusCompResponse();
            try
            {
                using (var service = ServiceHelper.GetService<ClientServiceConsult.billServiceClient>(_config, Resources.UrlServiceConsult))
                {
                    var result = await service.getStatusCdrAsync(ruc, comprobante.Tipo, comprobante.Serie, comprobante.Numero);
                    var response = result.statusCdr;
                    res.Success = true;
                    var pathXml = ProcessZip.ExtractFile(response.content, Path.GetTempPath());
                    res.ApplicationResponse = ProcessXml.GetAppResponse(pathXml);
                    res.Code = response.statusCode;
                    res.Message = response.statusMessage;
                    res.ContentZip = response.content;
                }
            }
            catch (FaultException ex)
            {
                res.Error = new ErrorResponse
                {
                    Code = ex.Code.Name,
                    Description = ProcessXml.GetDescriptionError(ex.Code.Name)
                };
            }
            catch (Exception er)
            {
                res.Error = new ErrorResponse
                {
                    Code = er.Message,
                };
            }
            return res;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Establece el Tipo de Servicio que se utilizara para la conexion con el WebService de Sunat.
        /// </summary>
        /// <param name="service">Tipo de Servicio al que se conectara</param>
        private static string GetUrlService(ServiceSunatType service)
        {
            string url;
            switch (service)
            {
                case ServiceSunatType.Produccion:
                    url = Resources.UrlProduccion;
                    break;
                case ServiceSunatType.Homologacion:
                    url = Resources.UrlHomologacion;
                    break;
                default:
                    url = Resources.UrlBeta;
                    break;
            }
            return url;
        }
        #endregion
    }
}
