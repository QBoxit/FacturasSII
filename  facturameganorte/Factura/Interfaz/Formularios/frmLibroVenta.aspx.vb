Imports System
Imports System.Globalization
Partial Public Class frmLibroVenta
    Inherits System.Web.UI.Page

    Dim CL As New ControladorLogica

    Private anio As String
    Private mactual As String
    Private dtDatos As New DataTable

    Private ivat As Integer = 0
    Private netot As Integer = 0
    Private totalt As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblAnio.Text = DateTime.Now.Year

        If Session("tb1") Is Nothing Then
            Session("tb1") = Me.dtDatos
        Else
            dtDatos = DirectCast(Session("tb1"), DataTable)
        End If

        If Not Page.IsPostBack Then
            Try
                Me.ComboMes.SelectedIndex = DateTime.Now.Month - 1
                dtDatos.Columns.Add(New DataColumn("RUT"))
                dtDatos.Columns.Add(New DataColumn("NDOCUMENTO"))
                dtDatos.Columns.Add(New DataColumn("RAZONSOCIAL"))
                dtDatos.Columns.Add(New DataColumn("FECHA"))
                dtDatos.Columns.Add(New DataColumn("IVA"))
                dtDatos.Columns.Add(New DataColumn("NETO"))
                dtDatos.Columns.Add(New DataColumn("TOTAL"))
            Catch
            End Try
        End If

        Me.llenarDataGrid()
        Me.gvLV.DataSource = Me.dtDatos
        Me.gvLV.DataBind()

    End Sub

  

    Private Sub llenarDataGrid()

        Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
        nfi.CurrencyDecimalDigits = 0
        nfi.CurrencyGroupSeparator = "."

        Me.ivat = 0
        Me.netot = 0
        Me.totalt = 0
   
        Me.dtDatos.Clear()
        Me.gvLV.DataSource = Me.dtDatos

        Dim mes As String
        mes = Me.ComboMes.SelectedValue
        Dim itemLv, factura, cliente As ArrayList
        itemLv = CL.ObtenerLvMes(mes, Me.lblAnio.Text)
        If itemLv.Count > 0 Then
            factura = CL.ObtenerFacturaLV(itemLv.Item(0))

            Try
                Dim i As Integer

                For i = 0 To factura.Count

                    Dim drDatos As DataRow = dtDatos.NewRow
                    Dim fact As factura

                    fact = DirectCast(factura.Item(i), factura)
                    cliente = CL.retornaCliente(fact.Cliente)

                    drDatos.Item("RUT") = cliente.Item(0)
                    drDatos.Item("NDOCUMENTO") = fact.NumeroFactura
                    drDatos.Item("RAZONSOCIAL") = cliente.Item(1)
                    drDatos.Item("FECHA") = fact.Fecha
                    drDatos.Item("IVA") = CInt(fact.Iva).ToString("C", nfi)
                    drDatos.Item("NETO") = CInt(fact.Neto).ToString("C", nfi)
                    drDatos.Item("TOTAL") = CInt(fact.Total).ToString("C", nfi)
                    ' MsgBox(ivat + " " + netot + " " + totalt)

                    dtDatos.Rows.Add(drDatos)

                    Me.gvLV.DataSource = dtDatos
                    Me.gvLV.DataBind()

                Next i
            Catch ex As Exception
            End Try
            errorFaltaDatos.Text = ""
        Else
            Me.errorFaltaDatos.ForeColor = Drawing.Color.Red
            errorFaltaDatos.Text = "La fecha Ingresada no Cuenta con Ventas Realizadas"
            Dim eraceformat As Integer = 0
            Me.lblivaMes.Text = eraceformat.ToString("C", nfi)
            Me.lblnetoMes.Text = eraceformat.ToString("C", nfi)
            Me.lbltotalMes.Text = eraceformat.ToString("C", nfi)
            Me.dtDatos.Clear()
            Me.gvLV.DataSource = Me.dtDatos
            Me.gvLV.DataBind()
        End If
        Try
            Dim arrayLV As ArrayList = CL.obtieneValorLibroVenta(mes, lblAnio.Text)
            Me.lblivaMes.Text = arrayLV.Item(0).ToString
            Me.lblnetoMes.Text = arrayLV.Item(1).ToString
            Me.lbltotalMes.Text = arrayLV.Item(2).ToString
        Catch
            MsgBox("Libro de Venta Aun no ha sido Creado,intente con otro mes")
        End Try
    End Sub

    Protected Sub btnVerMesLV_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Me.llenarDataGrid()
    End Sub
End Class