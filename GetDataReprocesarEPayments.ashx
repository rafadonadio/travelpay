<%@ WebHandler Language="C#" Class="GetDataReprocesarEPayments" %>

using System;
using System.Web;
using System.Text;
using System.Configuration;
using Core;
using ar.com.hsbc.epayments.www;

public class GetDataReprocesarEPayments : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
		context.Response.Write(DataReprocesarEPayments(context));
    }
	private string DataReprocesarEPayments(HttpContext context){

		PU_DispatcherWS wsPU = new PU_DispatcherWS();
		//WSPU_GetOrderDetailsResp ds = wsPU.GetOrders(context.Request.QueryString["IdIventure"], context.Request.QueryString["shopCode"]);
		string idTicket = "";
		idTicket = context.Request.QueryString["IdIventure"]; 
		//idTicket = "324427";

		WSPU_GetOrderDetailsResp ds = wsPU.GetOrders(idTicket, context.Request.QueryString["shopCode"]);
		int cantorder =  ds.Order.Rows.Count;
		StringBuilder msg = new StringBuilder();
		return "XML Encriptado";

	}
    public bool IsReusable {
        get {
            return false;
        }
    }

}