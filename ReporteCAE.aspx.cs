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
public partial class ReporteCAE : Page, IBasicPage
{
	public Boolean LoginVisible { get { return true; } }
	public Control ControlFocus { get { return cboAmbiente; } }
	protected void Page_Load(object sender, EventArgs e)
	{
		rgvFechaDesde.MaximumValue = DateTime.Today.AddYears(10).ToShortDateString();
		rgvFechaDesde.MinimumValue = DateTime.Today.AddYears(-10).ToShortDateString();
		rgvFechaDesde.ErrorMessage = "Vencimiento Inválido. Valores entre " + rgvFechaDesde.MinimumValue + " y " + rgvFechaDesde.MaximumValue;
		rgvFechaHasta.MaximumValue = DateTime.Today.AddYears(10).ToShortDateString();
		rgvFechaHasta.MinimumValue = DateTime.Today.AddYears(-10).ToShortDateString();
		rgvFechaHasta.ErrorMessage = "Vencimiento Inválido. Valores entre " + rgvFechaHasta.MinimumValue + " y " + rgvFechaHasta.MaximumValue;

		if (!IsPostBack)
		{
			CargarEmpresas();
		}
	}
	private void CargarEmpresas()
	{
		StringBuilder sql = new StringBuilder();
		sql.Append("SELECT DISTINCT CUIT FROM EmpresaCAE ORDER BY CUIT");
		cboEmpresa.DataSource = Core.FacadeDao.DevolverDataTableMFD(sql.ToString());
		cboEmpresa.DataTextField = "CUIT";
		cboEmpresa.DataValueField = "CUIT";

		cboEmpresa.DataBind();
		cboEmpresa.Items.Insert(0, new ListItem("[ Todas ]", "%"));
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
			//sql.Append("SELECT TOP " + (topeReporte + 1).ToString() + " CUIT, Count(CAE) CntCae, CAE, Count(NroComprobante) CntNroComprobante, NroComprobante, idCAEws IdCabecera" + Environment.NewLine);
			//sql.Append(" FROM EmpresaCAE" + Environment.NewLine);
			//sql.Append(" WHERE  Fecha BETWEEN '" + fechaDesde.ToString("yyyyMMdd") + "' AND '" + fechaHasta.AddDays(1).ToString("yyyyMMdd") + "'" + Environment.NewLine);
			//sql.Append(" AND Produccion = "+cboAmbiente.SelectedValue);
			//int[] indices = cboEmpresa.GetSelectedIndices();
			//ArrayList prov = new ArrayList();
			//for (int i = 0; i < indices.Length; i++)
			//{
			//    prov.Add(cboEmpresa.Items[indices[i]].Value);
			//}
			//if (!prov.Contains("%") && prov.Count > 0)
			//{
			//    StringBuilder idsProv = new StringBuilder();
			//    foreach (string idProv in prov)
			//    {
			//        idsProv.Append("'" + idProv + "', ");
			//    }
			//    idsProv.Remove(idsProv.Length - 2, 2);
			//    sql.Append(" AND CUIT IN (" + idsProv.ToString() + ")" + Environment.NewLine);
			//}
			//sql.Append(" GROUP BY CUIT, CAE, NroComprobante, idCAEws" + Environment.NewLine);

			sql.Append(" SELECT TOP " + (topeReporte + 1).ToString() + " Fecha, CUIT, CAE, NroComprobante, idCAEws IdCabecera" + Environment.NewLine);
			sql.Append(" FROM EmpresaCAE" + Environment.NewLine);
			sql.Append(" WHERE  Fecha BETWEEN '" + fechaDesde.ToString("yyyyMMdd") + "' AND '" + fechaHasta.AddDays(1).ToString("yyyyMMdd") + "'" + Environment.NewLine);
			sql.Append(" AND Produccion = " + cboAmbiente.SelectedValue);
			int[] indices = cboEmpresa.GetSelectedIndices();
			ArrayList prov = new ArrayList();
			for (int i = 0; i < indices.Length; i++)
			{
				prov.Add(cboEmpresa.Items[indices[i]].Value);
			}
			if (!prov.Contains("%") && prov.Count > 0)
			{
				StringBuilder idsProv = new StringBuilder();
				foreach (string idProv in prov)
				{
					idsProv.Append("'" + idProv + "', ");
				}
				idsProv.Remove(idsProv.Length - 2, 2);
				sql.Append(" AND CUIT IN (" + idsProv.ToString() + ")" + Environment.NewLine);
			}
			sql.Append(" ORDER BY Fecha, CUIT, CAE, NroComprobante, idCAEws" + Environment.NewLine);
			if (prov.Count > 0)
			{
				DataTable table = Core.FacadeDao.DevolverDataTableMFD(sql.ToString());
				grilla.DataSource = table;
				grilla.DataBind();
				grilla.Visible = true;
				lblAviso.Text = "";
				if (table.Rows.Count == topeReporte + 1)
				{
					lblAviso.Text = "Debe refinar el rango de fechas.<br>Se están mostrando sólo los primeros " + topeReporte.ToString() + " registros del reporte.<br>";
					table.Rows.RemoveAt(topeReporte);
					table.AcceptChanges();
					grilla.DataSource = table;
					grilla.DataBind();
				}
				else if (((DataTable)grilla.DataSource).Rows.Count == 0)
				{
					lblAviso.Text = "Las empresas seleccionados no tienen <br>CAEs obtenidos en la fecha seleccionada";
				}
			}
			else
			{
				grilla.Visible = false;
				lblAviso.Text = "Debe seleccionar al menos una empresa.";
			}
		}
	}
}
