<%@ WebService Language="C#" Class="CaeCounter" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Core;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CaeCounter  : System.Web.Services.WebService {

    [WebMethod]
    public void SetCAEByCUIT(string cuit, string cae, string nroComprobante, double idCAEws, bool produccion) {
		
		FacadeDao.SetCAEByCUIT(cuit, cae, nroComprobante, idCAEws, produccion);
    }
    
}

