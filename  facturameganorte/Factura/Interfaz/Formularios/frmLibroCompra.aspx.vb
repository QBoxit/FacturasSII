Imports ExLogger3.Logica
Imports System
Imports System.Globalization
Partial Public Class frmLibroCompra

    Inherits System.Web.UI.Page
    Dim CL As New ControladorLogica

    Private anio As String
    Private mactual As String
    Private dtDatos As New DataTable

    Private ivat As Integer = 0
    Private netot As Integer = 0
    Private totalt As Integer = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.txtRut.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")
        Me.txtIva.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")
        Me.txtNeto.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")
        Me.txtTotal.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")

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
        Me.gvLC.DataSource = Me.dtDatos
        Me.gvLC.DataBind()

    End Sub

    Private Sub limpiar()

        Me.txtRut.Text = String.Empty
        Me.txtDocumento.Text = String.Empty
        Me.txtProveedor.Text = String.Empty
        Me.txtFecha.Clear()
        Me.txtIva.Text = String.Empty
        Me.txtNeto.Text = String.Empty
        Me.txtTotal.Text = String.Empty
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.txtDocumento.Text <> "" And Me.txtFecha.Text <> "" And Me.txtIva.Text <> "" And Me.txtNeto.Text <> "" _
        And Me.txtProveedor.Text <> "" And Me.txtRut.Text <> "" And Me.txtTotal.Text <> "" Then

            Dim dia As String = Me.txtFecha.Text.Chars(0) + Me.txtFecha.Text.Chars(1)
            Dim mes As String = Me.txtFecha.Text.Chars(3) + Me.txtFecha.Text.Chars(4)
            Dim anio As String = Me.txtFecha.Text.Chars(6) + Me.txtFecha.Text.Chars(7) + Me.txtFecha.Text.Chars(8) + _
            Me.txtFecha.Text.Chars(9)


            Dim itemlibroventa As New libroCompraData(Me.txtRut.Text, Me.txtDocumento.Text, Me.txtProveedor.Text, dia, _
            mes, anio, Me.txtIva.Text, Me.txtNeto.Text, Me.txtTotal.Text)

            CL.ingresarItemLibroVentas(itemlibroventa)
            Me.errorFaltaDatos.ForeColor = Drawing.Color.Blue
            Me.errorFaltaDatos.Text = "Los datos Han Sido Ingresado Exitosamente"
            limpiar()

            Me.llenarDataGrid()
        Else
            Me.errorFaltaDatos.ForeColor = Drawing.Color.Red
            Me.errorFaltaDatos.Text = "Llene todos los campos para continuar"
            limpiar()
        End If

    End Sub

    Protected Sub txtRut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRut.TextChanged
        Dim valido As Boolean = CL.validarRut(Me.txtRut.Text)

        If valido Then
            Me.txtRut.Text = CL.formato(Me.txtRut.Text)
            Me.errorFaltaDatos.Text = ""

        Else
            Me.errorFaltaDatos.ForeColor = Drawing.Color.Red
            Me.errorFaltaDatos.Text = "El RUT Ingresado No Es Valido, Reintente Nuevamente"

            limpiar()
        End If

    End Sub

   


    Private Sub llenarDataGrid()

        Me.ivat = 0
        Me.netot = 0
        Me.totalt = 0

        Me.dtDatos.Clear()
        Me.gvLC.DataSource = Me.dtDatos

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

                    ivat += CInt(item.Iva)
                    netot += CInt(item.Neto)
                    totalt += CInt(item.Total)

                    dtDatos.Rows.Add(drDatos)

                    Me.gvLC.DataSource = dtDatos
                    Me.gvLC.DataBind()

                Next i

            Catch ex As Exception

            End Try
            errorFaltaDatos.Text = ""
        Else
            Me.errorFaltaDatos.ForeColor = Drawing.Color.Red
            errorFaltaDatos.Text = "La fecha Ingresada no Cuenta con compras Realizadas"
            Dim eraceformat As Integer = 0
            Me.lblivaMes.Text = eraceformat.ToString("C", nfi)
            Me.lblnetoMes.Text = eraceformat.ToString("C", nfi)
            Me.lbltotalMes.Text = eraceformat.ToString("C", nfi)
            Me.dtDatos.Clear()
            Me.gvLC.DataSource = Me.dtDatos
            Me.gvLC.DataBind()

        End If

        Me.lblivaMes.Text = ivat.ToString("C", nfi)
        Me.lblnetoMes.Text = netot.ToString("C", nfi)
        Me.lbltotalMes.Text = totalt.ToString("C", nfi)


    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Me.llenarDataGrid()
    End Sub
End Class