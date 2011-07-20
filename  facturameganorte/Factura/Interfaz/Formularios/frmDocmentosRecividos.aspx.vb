
Imports System
Imports System.Globalization
Partial Public Class frmDocmentosRecividos
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


        If Session("tb2") Is Nothing Then
            Session("tb2") = Me.dtDatos

        Else

            dtDatos = DirectCast(Session("tb2"), DataTable)

        End If

        If Not Page.IsPostBack Then
            Try
                Me.ComboMes.SelectedIndex = DateTime.Now.Month - 1
                dtDatos.Columns.Add(New DataColumn("RUT"))
                dtDatos.Columns.Add(New DataColumn("NDOCUMENTO"))
                dtDatos.Columns.Add(New DataColumn("PROVEEDOR"))
                dtDatos.Columns.Add(New DataColumn("FECHA"))
                dtDatos.Columns.Add(New DataColumn("IVA"))
                dtDatos.Columns.Add(New DataColumn("NETO"))
                dtDatos.Columns.Add(New DataColumn("TOTAL"))
            Catch
            End Try

        End If
        Me.llenarDataGrid()
        Me.gvDocRecv.DataSource = Me.dtDatos
        Me.gvDocRecv.DataBind()

    End Sub

  
    Private Sub llenarDataGrid()

        Me.ivat = 0
        Me.netot = 0
        Me.totalt = 0

        Me.dtDatos.Clear()
        Me.gvDocRecv.DataSource = Me.dtDatos

        Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
        nfi.CurrencyDecimalDigits = 0
        nfi.CurrencyGroupSeparator = "."

        Dim mes As String
        mes = Me.ComboMes.SelectedValue
        Dim itemLC As ArrayList
        itemLC = CL.ObtenerLcMes(mes, Me.lblAnio.Text)
        If itemLC.Count > 0 Then

            Try
                Dim i As Integer

                For i = 0 To itemLC.Count
                    Dim drDatos As DataRow = dtDatos.NewRow
                    Dim item As libroCompraData
                    item = DirectCast(itemLC.Item(i), libroCompraData)

                    drDatos.Item("RUT") = item.Rut
                    drDatos.Item("NDOCUMENTO") = item.Ndocmento
                    drDatos.Item("PROVEEDOR") = item.Proveedor
                    drDatos.Item("FECHA") = item.Dia + "/" + item.Mes + "/" + item.Anio
                    drDatos.Item("IVA") = CInt(item.Iva).ToString("C", nfi)
                    drDatos.Item("NETO") = CInt(item.Neto).ToString("C", nfi)
                    drDatos.Item("TOTAL") = CInt(item.Total).ToString("C", nfi)

                    dtDatos.Rows.Add(drDatos)

                    Me.gvDocRecv.DataSource = dtDatos
                    Me.gvDocRecv.DataBind()

                Next i

            Catch ex As Exception

            End Try
            errorFaltaDatos.Text = ""
        Else
            Me.errorFaltaDatos.ForeColor = Drawing.Color.Red
            errorFaltaDatos.Text = "La fecha Ingresada no Cuenta con compras Realizadas"
            Dim eraceformat As Integer = 0

            Me.dtDatos.Clear()
            Me.gvDocRecv.DataSource = Me.dtDatos
            Me.gvDocRecv.DataBind()

        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        llenarDataGrid()
    End Sub

End Class