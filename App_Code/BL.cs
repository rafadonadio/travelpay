using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Net;
using System.Text;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using MigraDoc.RtfRendering;


/// <summary>
/// Summary description for BL
/// </summary>
public class BL
{
	static TextFrame addressFrame;

	static BL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
	public static bool SendMail(string destination, string subject, string body, object sessionLogo, bool ingresoSitio)
		{
			SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["PortSMTP"]));
			client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["usuarioSMTP"], ConfigurationManager.AppSettings["passwordSMTP"]);



			MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailSender"], "Travel Pay", System.Text.Encoding.UTF8);
			MailAddress to = new MailAddress(destination);
			MailMessage message = new MailMessage(from, to);
			message.BodyEncoding = System.Text.Encoding.UTF8;
			message.Subject = subject;
			message.SubjectEncoding = System.Text.Encoding.UTF8;
			message.IsBodyHtml = true;
			string logo = "";
			string encabezado = "<table align=Center style='border:solid 1px black;width:675px'><tr><td align=center><img src='"+logo+"'></td></tr><tr><td align=left>";

			Document document = CreateDocument();
			document.UseCmykColor = true;

			// string ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);

#if true_
      RtfDocumentRenderer renderer = new RtfDocumentRenderer();
      renderer.Render(document, "HelloWorld.rtf", null);
#endif

			// ----- Unicode encoding and font program embedding in MigraDoc is demonstrated here -----

			// A flag indicating whether to create a Unicode PDF or a WinAnsi PDF file.
			// This setting applies to all fonts used in the PDF document.
			// This setting has no effect on the RTF renderer.
			bool unicode = false;

			// An enum indicating whether to embed fonts or not.
			// This setting applies to all font programs used in the document.
			// This setting has no effect on the RTF renderer.
			// (The term 'font program' is used by Adobe for a file containing a font. Technically a 'font file'
			// is a collection of small programs and each program renders the glyph of a character when executed.
			// Using a font in PDFsharp may lead to the embedding of one or more font programms, because each outline
			// (regular, bold, italic, bold+italic, ...) has its own fontprogram)
			PdfFontEmbedding embedding = PdfFontEmbedding.Always;  // Set to PdfFontEmbedding.None or PdfFontEmbedding.Always only

			// ----------------------------------------------------------------------------------------





			// Create a renderer for the MigraDoc document.
			PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(unicode, embedding);

			// Associate the MigraDoc document with a renderer
			pdfRenderer.Document = document;

			// Layout and render document to PDF
			pdfRenderer.RenderDocument();

			// Save the document...
			string filename = AppDomain.CurrentDomain.BaseDirectory + "HelloWorld2.pdf";
			pdfRenderer.PdfDocument.Save(filename);
			pdfRenderer.PdfDocument.Close();

			message.Attachments.Add(new Attachment(filename));
		//message.Attachments.Add(new Attachment());

			StringBuilder legal = new StringBuilder();
			if(ingresoSitio)
				legal.Append("<p align=center>Ingresar al Sitio</p> ");
			legal.Append("<br><font size=1>La información contenida en este correo es confidencial y para uso exclusivo "); 
			legal.Append("de los destinatarios del mismo. Esta prohibido a las personas o entidades "); 
			legal.Append("que no sean los destinatarios de este correo, realizar cualquier tipo de "); 
			legal.Append("modificación, copia o distribución del mismo. Toda la información contenida "); 
			legal.Append("en este email está sujeta a los términos y condiciones generales que se "); 
			legal.Append("establecen en la Sección Legales. Si Usted recibe este correo por error, ");
			legal.Append("tenga a bien notificar al emisor y eliminarlo.</font> "); 
			string footer = "</td></tr><tr><td><br><br><br></td></tr><tr><td align=center>" + legal + "</td></tr></table>";

			message.Body = encabezado + body + footer;
			client.Send(message);
			return true;
		}
	/// <summary>
	/// Creates an absolutely minimalistic document.
	/// </summary>
	private static Document CreateDocument()
	{
		// Create a new MigraDoc document
		Document document = new Document();

		// Add a section to the document
		Section section = document.AddSection();

		// Add a paragraph to the section
		Paragraph paragraph = section.AddParagraph();

		paragraph.Format.Font.Color = MigraDoc.DocumentObjectModel.Color.FromCmyk(100, 30, 20, 50);

		// Add some text to the paragraph
		//paragraph.AddFormattedText("Hello, World!  öäüÖÄÜß~§≤≥≈≠", TextFormat.Italic);
		paragraph.AddFormattedText("Hello, World!", TextFormat.Underline);
		//paragraph.AddFormattedText(textBox1.Text, TextFormat.Underline);


		// Add a section to the document
		//Section section2 = document.AddSection();

		// Add a paragraph to the section
		//Paragraph paragraph2 = section2.AddParagraph();

		// Add some text to the paragraph
		//paragraph.AddFormattedText("Hello, World!", TextFormat.Italic);
		//paragraph.AddImage("../../SomeImage.png");
		//paragraph.AddImage("../../Logo.pdf");
		//section.AddImage("../../Logo.pdf");
		//section2..AddImage("../../SomeImage.png");

		// Create footer
		Paragraph paragraphFooter = section.Footers.Primary.AddParagraph();
		paragraphFooter.AddText("PowerBooks Inc · Sample Street 42 · 56789 Cologne · Germany");
		paragraphFooter.Format.Font.Size = 9;
		paragraphFooter.Format.Alignment = ParagraphAlignment.Center;

		// Create the text frame for the address
		addressFrame = section.AddTextFrame();
		addressFrame.Height = "3.0cm";
		addressFrame.Width = "7.0cm";
		addressFrame.Left = ShapePosition.Left;
		addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
		addressFrame.Top = "5.0cm";
		addressFrame.RelativeVertical = RelativeVertical.Page;

		// Put sender in address frame
		paragraphFooter = addressFrame.AddParagraph("PowerBooks Inc · Sample Street 42 · 56789 Cologne");
		paragraphFooter.Format.Font.Name = "Times New Roman";
		paragraphFooter.Format.Font.Size = 7;
		paragraphFooter.Format.SpaceAfter = 3;

		// Add the print date field
		paragraphFooter = section.AddParagraph();
		paragraphFooter.Format.SpaceBefore = "8cm";
		paragraphFooter.Style = "Reference";
		paragraphFooter.AddFormattedText("INVOICE", TextFormat.Bold);
		paragraphFooter.AddTab();
		paragraphFooter.AddText("Cologne, ");
		paragraphFooter.AddDateField("dd.MM.yyyy");
		section.AddImage("../../SomeImage.png");

		return document;
	}
}
