<%@ WebService Language="C#" Class="MFDWebService" %>

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;

using System.Net.Security;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;


using FrameWork.Tools.NTP;
using Entities;
using BusinessLogic;
using ar.gov.afip.wswhomo;
using ar.gov.afip.wsaahomo;
using Adapters;
using Tools.XML.Generator;
using System.Globalization;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class MFDWebService  : System.Web.Services.WebService {

	#region Atributos
	private LoginTicketsBL loginTicketsBL;
	private LoginTicketRequestsBL loginTicketRequestsBL;
	private CultureInfo culture = new CultureInfo("es-AR");

	#region Constantes
	private const string LoginTicketResponseNodeName = "loginTicketResponse";
	private const string HeaderNodeName = "header";
	private const string SourceNodeName = "source";
	private const string DestinationNodeName = "destination";
	private const string UniqueIDNodeName = "uniqueId";
	private const string GenerationTimeNodeName = "generationTime";
	private const string ExpirationTimeNodeName = "expirationTime";
	private const string CredentialsNodeName = "credentials";
	private const string TokenNodeName = "token";
	private const string SignNodeName = "sign";
	private const string loginTicketRequestNodeName = "loginTicketRequest";
	private const string ServiceNodeName = "service";
	#endregion Constantes
	#endregion Atributos
	#region Métodos Públicos
	public string GetCAE(Comprobantes comprobante, out string logError, out string nroComprobante, LoginTicketsBL loginTickets, LoginTicketRequestsBL loginTicketRequests, bool autogenerarNroComprobante)
	{
		loginTicketsBL = loginTickets;
		loginTicketRequestsBL = loginTicketRequests;
		return GetCAE(comprobante, out logError, out nroComprobante, autogenerarNroComprobante);
	}
	[WebMethod]
	public string GetCAE(XmlDocument xmlEmpresa, XmlDocument xmlComprobante, out string logError, out string nroComprobante)
	{
		XmlNode node;
		Comprobantes comprobante = new Comprobantes();
		node = xmlComprobante.SelectSingleNode("//cuitCliente");
		comprobante.Cliente = new Clientes();
		comprobante.Cliente.CUIT = node.InnerXml;
		node = xmlComprobante.SelectSingleNode("//numeroComprobante");
		comprobante.NroCbante = node.InnerXml;
		node = xmlComprobante.SelectSingleNode("//tipo");
		comprobante.Tipo = new TiposComprobantes();
		comprobante.Tipo.Codigo = node.InnerXml;
		node = xmlComprobante.SelectSingleNode("//emision");
		comprobante.Emision = Convert.ToDateTime(node.InnerXml, culture);
		node = xmlComprobante.SelectSingleNode("//subtotalNeto ");
		comprobante.SubtotalNeto = Convert.ToInt32(node.InnerXml);
		node = xmlComprobante.SelectSingleNode("//iva1");
		comprobante.Iva1 = Convert.ToDouble(node.InnerXml);
		node = xmlComprobante.SelectSingleNode("//iva2");
		comprobante.Iva2 = Convert.ToDouble(node.InnerXml);
		node = xmlComprobante.SelectSingleNode("//total");
		comprobante.Total = Convert.ToDouble(node.InnerXml);
		node = xmlComprobante.SelectSingleNode("//idComprobante");
		comprobante.Id = Convert.ToInt32(node.InnerXml);

		comprobante.Empresa = new Empresas();
		node = xmlEmpresa.SelectSingleNode("//dnOrigen");
		comprobante.Empresa.DNOrigen = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//dnDestino");
		comprobante.Empresa.DNDestino = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//uniqueID");
		comprobante.Empresa.Id = Convert.ToInt32(node.InnerXml);
		node = xmlEmpresa.SelectSingleNode("//senderName");
		comprobante.Empresa.SenderName = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//cuit");
		comprobante.Empresa.Cuit = node.InnerXml;
		/*comprobante.Empresa.CAI = "CAI1455";
		comprobante.Empresa.CertificadoDestinoFileName = @"C:\Vero\Proyectos\MFD\MFDHOMO\Certificados\viset\CRT-root.cer";
		comprobante.Empresa.CertificadoFileName = @"C:\Vero\Proyectos\MFD\MFDHOMO\Certificados\viset\viset.crt";
		comprobante.Empresa.CertificadoPassword = "walter";
		comprobante.Empresa.LoginTicketFileName = @"C:\Vero\Proyectos\MFD\MFDHOMO\Certificados\viset\LoginTicketRequest.xml";
		*/
		return GetCAE(comprobante, out logError, out nroComprobante, false);
	}
	[WebMethod]
	public long? GetUltimoId(XmlDocument xmlEmpresa)
	{
		XmlNode node;
		Comprobantes comprobante = new Comprobantes();
		comprobante.Empresa = new Empresas();
		node = xmlEmpresa.SelectSingleNode("//dnOrigen");
		comprobante.Empresa.DNOrigen = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//dnDestino");
		comprobante.Empresa.DNDestino = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//uniqueID");
		comprobante.Empresa.Id = Convert.ToInt32(node.InnerXml);
		node = xmlEmpresa.SelectSingleNode("//senderName");
		comprobante.Empresa.SenderName = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//cuit");
		comprobante.Empresa.Cuit = node.InnerXml;
		return GetUltimoId(comprobante);
	}
	[WebMethod]
	public int? GetUltimoNroComprobante(XmlDocument xmlEmpresa, XmlDocument xmlComprobante)
	{
		XmlNode node;
		Comprobantes comprobante = new Comprobantes();
		node = xmlComprobante.SelectSingleNode("//numeroComprobante");
		comprobante.NroCbante = node.InnerXml;
		node = xmlComprobante.SelectSingleNode("//tipo");
		comprobante.Tipo = new TiposComprobantes();
		comprobante.Tipo.Codigo = node.InnerXml;
		comprobante.Empresa = new Empresas();
		node = xmlEmpresa.SelectSingleNode("//dnOrigen");
		comprobante.Empresa.DNOrigen = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//dnDestino");
		comprobante.Empresa.DNDestino = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//uniqueID");
		comprobante.Empresa.Id = Convert.ToInt32(node.InnerXml);
		node = xmlEmpresa.SelectSingleNode("//senderName");
		comprobante.Empresa.SenderName = node.InnerXml;
		node = xmlEmpresa.SelectSingleNode("//cuit");
		comprobante.Empresa.Cuit = node.InnerXml;
		return GetUltimoNroComprobante(comprobante);
	}
	#endregion Métodos Públicos
	#region Métodos Privados
	private long? GetUltimoId(Comprobantes comprobante)
	{
		LoginTickets loginTicket = GetLoginTicket(comprobante.Empresa);
		if (loginTicket != null)
		{
			FEAuthRequest authRequest = new FEAuthRequest();
			authRequest.Token = loginTicket.Token;
			authRequest.Sign = loginTicket.Sign;
			authRequest.cuit = Convert.ToInt64(comprobante.Empresa.Cuit.Replace("-", ""));
			Service service = new Service();
			/*FEUltNroResponse ultid = service.FEUltNroRequest(authRequest);
			UltNroResponse nroo = ultid.nro;
			vError err = ultid.RError;*/
			return ((FEUltNroResponse)(service.FEUltNroRequest(authRequest))).nro.value;
		}
		return null;

	}
	private int? GetUltimoNroComprobante(Comprobantes comprobante)
	{
		LoginTickets loginTicket = GetLoginTicket(comprobante.Empresa);
		if (loginTicket != null)
		{
			FEAuthRequest authRequest = new FEAuthRequest();
			authRequest.Token = loginTicket.Token;
			authRequest.Sign = loginTicket.Sign;
			authRequest.cuit = Convert.ToInt64(comprobante.Empresa.Cuit.Replace("-", ""));
			ComprobantesAdapter adapter = new ComprobantesAdapter(comprobante);
			Service service = new Service();
			return ((FERecuperaLastCMPResponse)(service.FERecuperaLastCMPRequest(authRequest, adapter.GenerarFELastCMPtype()))).cbte_nro;
		}
		return null;
	}

	private string GetCAE(Comprobantes comprobante, out string logError, out string nroComprobante, bool autogenerarNroComprobante)
	{
		
		logError = String.Empty;
		string result = String.Empty;
		nroComprobante = "-1";
		
		//TODO: PONER LoginTickets loginTicket = GetLoginTicket(comprobante.Empresa);
		LoginTickets loginTicket = null;
		if (loginTicket != null)
		{
			//FEAuthRequest authRequest = new FEAuthRequest();
			//authRequest.Token = loginTicket.Token;
			//authRequest.Sign = loginTicket.Sign;
			//authRequest.cuit = Convert.ToInt64(comprobante.Empresa.Cuit.Replace("-", ""));
			////ComprobantesAdapter adapter = new ComprobantesAdapter(comprobante);
			//Service service = new Service();
			//FERequest request = adapter.GenerarFERequest(comprobante.Id);
			//FEResponse response = service.FEAutRequest(authRequest, request);

		//    if (autogenerarNroComprobante && response.FedResp != null && response.FedResp[0].resultado == "R" && response.FedResp[0].motivo.StartsWith("11") && response.FedResp[0].cae == "NULL")
		//    {
		//        //Caso en que falla por correlatividad de comprobantes
		//        FERecuperaLastCMPResponse ultimoComEmitido = service.FERecuperaLastCMPRequest(authRequest, adapter.GenerarFELastCMPtype());
		//        comprobante.NroCbante = comprobante.NroCbante.Substring(0, 5) + (ultimoComEmitido.cbte_nro + 1).ToString().PadLeft(8, '0');
		//        adapter = new ComprobantesAdapter(comprobante);
		//        request = new FERequest();
		//        //Generación de otro ID
		//        long idDate = DateTime.Now.Year * 10000000000;
		//        idDate += DateTime.Now.Month * 100000000;
		//        idDate += DateTime.Now.Day * 1000000;
		//        idDate += DateTime.Now.Hour * 10000;
		//        idDate += DateTime.Now.Minute * 100;
		//        idDate += DateTime.Now.Second;
		//        request = adapter.GenerarFERequest(idDate);
		//        service = new Service();
		//        response = service.FEAutRequest(authRequest, request);
		//    }
		//    if (response.FedResp != null && response.FedResp[0].resultado == "R" && !String.IsNullOrEmpty(response.FedResp[0].motivo))
		//    {
		//        ArrayList motivos = new ArrayList();
		//        motivos.AddRange(response.FedResp[0].motivo.Split(";".ToCharArray()));
		//        StringBuilder logMotivos = new StringBuilder();
		//        if (motivos.Contains("01")) logMotivos.Append("01 \"La cuit informada no corresponde a un responsable inscripto en el iva activo\\n");
		//        if (motivos.Contains("02")) logMotivos.Append("02 \"La cuit informada no se encuentra autorizada a emitir comprobantes electronicos originales o el periodo de inicio autorizado es posterior al de la generacion de la solicitud\"\n");
		//        if (motivos.Contains("03")) logMotivos.Append("03 \"La cuit informada registra inconvenientes con el domicilio fiscal\"\n");
		//        if (motivos.Contains("04")) logMotivos.Append("04 \"El punto de venta informado no se encuentra declarado para ser utilizado en el presente regimen\"\n");
		//        if (motivos.Contains("05")) logMotivos.Append("05 \"La fecha del comprobante indicada no puede ser anterior en mas de cinco dias, si se trata de una venta, o anterior o posterior en mas de diez dias, si se trata de una prestacion de servicios, consecutivos de la fecha de remision del archivo art. 22 De la rg nro 2177-\"\n");
		//        if (motivos.Contains("06")) logMotivos.Append("06 \"La cuit informada no se encuentra autorizada a emitir comprobantes clase \"a\"\"\n");
		//        if (motivos.Contains("07")) logMotivos.Append("07 \"Para la clase de comprobante solicitado -comprobante clase a- debera consignar en el campo codigo de documento identificatorio del comprador el codigo \"80\"\n");
		//        if (motivos.Contains("08")) logMotivos.Append("08 \"La cuit indicada en el campo nro de identificacion del comprador es invalida\"\n");
		//        if (motivos.Contains("09")) logMotivos.Append("09 \"La cuit indicada en el campo nro de identificacion del comprador no existe en el padron unico de contribuyentes\"\n");
		//        if (motivos.Contains("10")) logMotivos.Append("10 \"La cuit indicada en el campo nro de identificacion del comprador no corresponde a un responsable inscripto en el iva activo\"\n");
		//        if (motivos.Contains("11")) logMotivos.Append("11 \"El nro de comprobante desde informado no es correlativo al ultimo nro de comprobante registrado/hasta solicitado para ese tipo de comprobante y punto de venta\"\n");
		//        if (motivos.Contains("12")) logMotivos.Append("12 \"El rango informado se encuentra autorizado con anterioridad para la misma cuit, tipo de comprobante y punto de venta\"\n");
		//        if (motivos.Contains("13")) logMotivos.Append("13 \"La cuit indicada se encuentra comprendida en el regimen establecido por la resolucion general nro 2177 y/o en el titulo I de la resolucion general nro 1361 art. 24 De la rg nro 2177\"\n");
		//        if (logMotivos.Length > 0)
		//        {
		//            logError = logMotivos.ToString();
		//        }
		//    }
		//    nroComprobante = comprobante.NroCbante;
		//    try
		//    {
		//        result = response.FedResp[0].cae;
		//    }
		//    catch (Exception ex)
		//    {
		//        nroComprobante = "-1";
		//        result = ex.Message;
		//    }
		}
		return System.Configuration.ConfigurationManager.AppSettings["urlTravelPay"] + @"\" + @"LoginTicketRequest.xsd";
		//return result;

	}
	private LoginTickets GetLoginTicket(Empresas empresa)
	{
		LoginTickets result = null;
		string ntpServer;
		NTPClient ntp;
		if (loginTicketsBL != null)
		{
			result = loginTicketsBL.GetCurrentLoginTicket(empresa);
			ntpServer = GeneralSettings.Instance.NTPServer;
		}
		if (result == null)
		{
			//Sincronización de reloj por NTP
			ntp = new NTPClient("time.afip.gov.ar");
			ntp.Connect(true);

			XmlDocument xmlDocument;
			LoginTicketRequests loginTicketRequest;
			//Generacion del Ticket de Requerimiento de Acceso
			if (loginTicketRequestsBL != null)
			{
				loginTicketRequest = loginTicketRequestsBL.GenerateLoginTicket(empresa);
				xmlDocument = loginTicketRequestsBL.GenerateLoginTicketRequestXML(loginTicketRequest);
			}
			else
			{
				loginTicketRequest = new LoginTicketRequests(empresa);
				xmlDocument = GenerateLoginTicketRequestXML(loginTicketRequest);
			}
			// Conversion del XML a bytes
			StringWriter sw = new StringWriter();
			xmlDocument.WriteTo(new XmlTextWriter(sw));
			byte[] loginTicketRequestXml = new ASCIIEncoding().GetBytes(sw.ToString());

			//Firma del Ticket de Requerimiento de Acceso
			X509Certificate2 certOrigen = GetSenderCertificate(empresa);
			byte[] loginTicketRequestCMS = Sign(loginTicketRequestXml, certOrigen);

			//Codificacion a base 64
			//string loginTicketRequestBase64 = Convert.ToBase64String(encryptedTicket);
			string loginTicketRequestBase64 = Convert.ToBase64String(loginTicketRequestCMS);

			//Transmisión del Ticket de Requerimiento de Acceso al WSAA
			LoginCMSService loginService = new LoginCMSService();
			ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
			string loginServiceResponse = loginService.loginCms(loginTicketRequestBase64);

			//Extracción del Ticket de Acceso devuelto por el WSAA
			XmlDocument loginTicketXml = new XmlDocument();
			loginTicketXml.LoadXml(loginServiceResponse);
			if (loginTicketRequestsBL != null)
			{
				result = loginTicketsBL.GetLoginTicketFromXML(loginTicketXml, loginTicketRequest);
			}
			else
			{
				result = GetLoginTicketFromXML(loginTicketXml, null);
			}
		}
		return result;
	}
	private X509Certificate2 GetSenderCertificate(Empresas empresa)
	{
		if (String.IsNullOrEmpty(empresa.SenderName))
		{
			string error = "Error en GetSenderCertificate: Es necesario configurar el Sender Name, en parametros empresas.";
			throw new Exception(error);
		}
		String senderName = empresa.SenderName;//"MFD Solutions"
		//  Open the AddressBook local user X509 certificate store.
		X509Store storeAddressBook = new X509Store(StoreName.My, StoreLocation.CurrentUser);
		storeAddressBook.Open(OpenFlags.ReadOnly);

		X509Certificate2 result = storeAddressBook.Certificates[0];
		X509Certificate2Collection certColl = storeAddressBook.Certificates.Find(X509FindType.FindBySubjectName, senderName, false);

		//  Check to see if the certificate suggested by the example
		//  requirements is not present.
		if (certColl.Count > 0) result = certColl[0];
		storeAddressBook.Close();

		return result;
	}
	private byte[] Sign(byte[] data, X509Certificate2 signingCert)
	{
		// create ContentInfo
		ContentInfo content = new ContentInfo(data);

		// SignedCms represents signed data
		SignedCms signedMessage = new SignedCms(content);

		// create a signer
		CmsSigner signer = new CmsSigner(signingCert);

		// sign the data
		signedMessage.ComputeSignature(signer);

		// create PKCS #7 byte array
		byte[] signedBytes = signedMessage.Encode();

		// return signed data
		return signedBytes;
	}
	private bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		//TODO: creo que debería validar que el certificado sea el de la AFIP
		return true;
	}
	private LoginTickets GetLoginTicketFromXML(XmlDocument document, global::Entities.LoginTicketRequests request)
	{
		LoginTickets result = null;
		XmlNode headerNode = document[LoginTicketResponseNodeName][HeaderNodeName];
		XmlNode credentialsNode = document[LoginTicketResponseNodeName][CredentialsNodeName];
		DateTime generationTime = XmlConvert.ToDateTime(headerNode[GenerationTimeNodeName].InnerText, XmlDateTimeSerializationMode.RoundtripKind);
		DateTime expirationTime = XmlConvert.ToDateTime(headerNode[ExpirationTimeNodeName].InnerText, XmlDateTimeSerializationMode.RoundtripKind);
		long requestID = XmlConvert.ToInt64(headerNode[UniqueIDNodeName].InnerText);

		//TODO: validar que lo que devolvio el WS sea válido (expiration, source, destination, etc)
		result = new LoginTickets(requestID, request, generationTime, expirationTime, credentialsNode[TokenNodeName].InnerText, credentialsNode[SignNodeName].InnerText);
		return result;
	}
	private XmlDocument GenerateLoginTicketRequestXML(LoginTicketRequests loginTicketRequest)
	{
		XmlQualifiedName qualifiedName = new XmlQualifiedName(loginTicketRequestNodeName);
		//TODO: SACAR EL hardcode del XSD
		XMLGenerator generator = new XMLGenerator(System.Configuration.ConfigurationManager.AppSettings["urlTravelPay"] +@"\"+ @"LoginTicketRequest.xsd", qualifiedName);
		Stream xmlStream = new MemoryStream();
		XmlTextWriter writer = new XmlTextWriter(xmlStream, Encoding.UTF8);
		writer.Formatting = Formatting.Indented;
		generator.WriteXml(writer);
		XmlDocument document = new XmlDocument();
		xmlStream.Seek(0, SeekOrigin.Begin);
		XmlReader reader = new XmlTextReader(xmlStream);
		document.Load(reader);
		reader.Close();
		writer.Close();
		xmlStream.Close(); xmlStream.Dispose();

		XmlElement headerNode = document[loginTicketRequestNodeName][HeaderNodeName];
		headerNode[SourceNodeName].InnerText = loginTicketRequest.Empresa.DNOrigen;
		headerNode[DestinationNodeName].InnerText = loginTicketRequest.Empresa.DNDestino;
		//TODO: ver cual debe ser el loginTicketRequest.Id
		headerNode[UniqueIDNodeName].InnerText = loginTicketRequest.Id.ToString();
		headerNode[GenerationTimeNodeName].InnerText = XmlConvert.ToString(loginTicketRequest.GenerationTime, XmlDateTimeSerializationMode.RoundtripKind);
		headerNode[ExpirationTimeNodeName].InnerText = XmlConvert.ToString(loginTicketRequest.ExpirationTime, XmlDateTimeSerializationMode.RoundtripKind);
		document[loginTicketRequestNodeName][ServiceNodeName].InnerText = "wsfe";

		return document;
	}
	#endregion Métodos Privados
    
}

