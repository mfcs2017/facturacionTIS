﻿using System;
using System.IO;
using System.ServiceModel;

namespace FacturacionElectronica.Homologacion
{
    using GuiaRemisionService;
    using Res;
    using Security;

    /// <summary>
    /// Class Managed WebService Sunat(Others Documents)
    /// </summary>
    public class SunatCe
    {
        #region Properties
        internal string BaseUrl { get; set; }
        #endregion

        #region Construct
        /// <summary>
        /// Administrador de WebService de la Sunat. Necesita Clave SOL
        /// </summary>
        /// <param name="ruc">Ruc del emisor</param>
        /// <param name="user">Nombre de Usuario en la Sunat</param>
        /// <param name="clave">Clave SOL</param>
        protected SunatCe(string ruc, string user, string clave)
        {
            ServiceHelper.User = string.Concat(ruc, user);
            ServiceHelper.Password = clave;
        }
        #endregion

        #region Method Sunat
        /// <summary>
        /// Recibe la ruta XML con un único formato digital y devuelve la Constancia de Recepción – SUNAT. 
        /// </summary>
        /// <param name="pathFileXml">Ruta del Archivo XML</param>
        /// <returns>La respuesta contenida en el XML de Respuesta de la Sunat, si existe</returns>
        public SunatResponse SendDocument(string pathFileXml)
        {
            var nameOfFileZip = Path.GetFileNameWithoutExtension(pathFileXml) + Resources.ExtensionFile;

            var response = new SunatResponse
            {
                Success = false
            };
            try
            {
                var zipBytes = ProcessZip.CompressFile(pathFileXml);
                using (var service = ServiceHelper.GetService<billServiceClient>(BaseUrl))
                {
                    var resultBytes = service.sendBill(nameOfFileZip, zipBytes);
                    var outputXml = ProcessZip.ExtractFile(resultBytes, Path.GetTempPath());
                    response = new SunatResponse
                    {
                        Success = true,
                        ApplicationResponse = ProcessXml.GetAppResponse(outputXml),
                        ContentZip = resultBytes
                    };


                }
            }
            catch (FaultException ex)
            {
                response.Error = GetErrorFromFault(ex);
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
        /// Envia una Resumen o Comunicaciones de Baja a Sunat
        /// </summary>
        /// <param name="pathFileXml">Ruta del archivo XML que contiene el resumen</param>
        /// <returns>Retorna un estado booleano que indica si no hubo errores, con un string que contiene el Nro Ticket,
        /// con el que posteriormente, utilizando el método getStatus se puede obtener Constancia de Recepcióno</returns>
        public TicketResponse SendSummary(string pathFileXml)
        {
            var nameOfFileZip = Path.GetFileNameWithoutExtension(pathFileXml) + Resources.ExtensionFile;
            var res = new TicketResponse();
            try
            {
                var zipBytes = ProcessZip.CompressFile(pathFileXml);
                using (var service = ServiceHelper.GetService<billServiceClient>(BaseUrl))
                {
                    res.Ticket = service.sendSummary(nameOfFileZip, zipBytes);
                    res.Success = true;
                }
            }
            catch (FaultException ex)
            {
                res.Error = GetErrorFromFault(ex);
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
        public SunatResponse GetStatus(string pstrTicket)
        {
            var res = new SunatResponse();
            try
            {
                using (var service = ServiceHelper.GetService<billServiceClient>(BaseUrl))
                {
                    var response = service.getStatus(pstrTicket);
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
                            res.Error = new ErrorResponse { Description = "En Proceso..." };
                            break;
                    }
                }
            }
            catch (FaultException ex)
            {
                res.Error = GetErrorFromFault(ex);
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
        #endregion

        #region Private Methods

        private static ErrorResponse GetErrorFromFault(FaultException ex)
        {
            var errMsg = ProcessXml.GetDescriptionError(ex.Message);
            if (string.IsNullOrEmpty(errMsg))
            {
                var msg = ex.CreateMessageFault();
                if (msg.HasDetail)
                {
                    var dets = msg.GetReaderAtDetailContents();
                    errMsg = dets.ReadElementString(dets.Name);
                }
            }
            return new ErrorResponse
            {
                Code = ex.Message,
                Description = errMsg
            };
        }

        #endregion
    }
}
