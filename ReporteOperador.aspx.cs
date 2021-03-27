using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Core;

public partial class ReporteOperador : Page, IBasicPage
{
	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return txtFechaDesde; } }
	protected void Page_Load(object sender, EventArgs e)
	{
		rgvFechaDesde.MaximumValue = DateTime.Today.AddYears(10).ToShortDateString();
		rgvFechaDesde.MinimumValue = DateTime.Today.AddYears(-10).ToShortDateString();
		rgvFechaDesde.ErrorMessage = "Vencimiento Inválido. Valores entre " + rgvFechaDesde.MinimumValue + " y " + rgvFechaDesde.MaximumValue;
		rgvFechaHasta.MaximumValue = DateTime.Today.AddYears(10).ToShortDateString();
		rgvFechaHasta.MinimumValue = DateTime.Today.AddYears(-10).ToShortDateString();
		rgvFechaHasta.ErrorMessage = "Vencimiento Inválido. Valores entre " + rgvFechaHasta.MinimumValue + " y " + rgvFechaHasta.MaximumValue;
	}
	
	protected void btnReporte_Click(object sender, EventArgs e)
	{
		Page.Validate();
		if (Page.IsValid)
		{
			DateTime fechaDesde = Convert.ToDateTime(txtFechaDesde.Text);
			DateTime fechaHasta = Convert.ToDateTime(txtFechaHasta.Text);
			StringBuilder sql = new StringBuilder();
			string sTope = ConfigurationManager.AppSettings["TopeReporte"];
			int topeReporte = 0;
			try
			{
				topeReporte = Convert.ToInt32(sTope);
			}
			catch
			{
				throw new Exception("Error al convertir a entero el valor TopeReporte del web.config");
			} 
			
			if (chkAgrupado.Checked)
			{
				sql.Append("SELECT TOP " + (topeReporte + 1).ToString() + " Nombre Operador, SUM(ImportePagado) Monto, Count(ImportePagado) CntTransacciones" + Environment.NewLine);
				sql.Append(" FROM Iventure i, Proveedor p, Usuario u" + Environment.NewLine);
				sql.Append(" WHERE i.IdProveedor = p.IdProveedor AND Estado='PAGADO'" + Environment.NewLine);
				sql.Append(" AND FechaPago BETWEEN '" + fechaDesde.ToString("yyyyMMdd") + "' AND '" + fechaHasta.AddDays(1).ToString("yyyyMMdd") + "'" + Environment.NewLine);
				sql.Append(" AND i.Vendedor = u.IdUsuario"+ Environment.NewLine);
				sql.Append(" AND i.IdProveedor = "+((Proveedor)(Session["Proveedor"])).Id.ToString() + Environment.NewLine);
				sql.Append(" GROUP BY Nombre" + Environment.NewLine);
				sql.Append(" ORDER BY Nombre" + Environment.NewLine);
			}
			else {
				sql.Append("SELECT TOP " + (topeReporte + 1).ToString() + " FechaPago Fecha, Nombre Operador, ImportePagado Monto" + Environment.NewLine);
				sql.Append(" FROM Iventure i, Proveedor p, Usuario u" + Environment.NewLine);
				sql.Append(" WHERE i.IdProveedor = p.IdProveedor AND Estado='PAGADO'" + Environment.NewLine);
				sql.Append(" AND FechaPago BETWEEN '" + fechaDesde.ToString("yyyyMMdd") + "' AND '" + fechaHasta.AddDays(1).ToString("yyyyMMdd") + "'" + Environment.NewLine);
				sql.Append(" AND i.Vendedor = u.IdUsuario" + Environment.NewLine);
				sql.Append(" AND i.IdProveedor = " + ((Proveedor)(Session["Proveedor"])).Id.ToString() + Environment.NewLine);
				sql.Append(" ORDER BY FechaPago, Nombre" + Environment.NewLine);
			}
			

			lblAviso.Text = "";
			DataTable table ; 
			if (chkAgrupado.Checked)
			{
				grillaAgrupada.DataSource = Core.FacadeDao.DevolverDataTable(sql.ToString());
				grillaAgrupada.DataBind();
				grillaAgrupada.Visible = true;
				grilla.Visible = false;
				if (((DataTable)grillaAgrupada.DataSource).Rows.Count == topeReporte + 1)
				{
					lblAviso.Text = "Debe refinar el rango de fechas. Se están mostrando sólo los primeros "+topeReporte.ToString()+" registros del reporte.<br>";
				}
				else if (((DataTable)grillaAgrupada.DataSource).Rows.Count == 0)
				{
					lblAviso.Text = "No hay transacciones realizadas en la fecha seleccionada";
				}
			}
			else {
				table = Core.FacadeDao.DevolverDataTable(sql.ToString());
				grilla.DataSource = table;
				grilla.DataBind();
				grilla.Visible = true;
				grillaAgrupada.Visible = false;
				if (table.Rows.Count == topeReporte + 1)
				{
					lblAviso.Text = "Debe refinar el rango de fechas.<br>Se están mostrando sólo los primeros " + topeReporte.ToString() + " registros del reporte.<br>";
					table.Rows.RemoveAt(topeReporte);
					table.AcceptChanges();
					grilla.DataSource = table;
					grilla.DataBind();
				}
				else if (table.Rows.Count == 0)
				{
					lblAviso.Text = "No hay transacciones realizadas en la fecha seleccionada";
				}
			}
			
		}
	}
}
