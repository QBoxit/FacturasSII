Imports System
Imports System.Globalization
Imports ExLogger3.Logica
Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports Npgsql
Imports System.Security.Cryptography.SHA1

Partial Public Class frmDocumentosEmitidos
    Inherits System.Web.UI.Page

    Dim CL As New ControladorLogica
    Private dtDatos As New DataTable
    Dim DataFactura As New DataSetFactura
    Dim intGRID As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("tb1") Is Nothing Then
            Session("tb1") = Me.dtDatos
        Else
            dtDatos = DirectCast(Session("tb1"), DataTable)
        End If

        If Not Page.IsPostBack Then
            Try

                Me.ComboMes.SelectedIndex = DateTime.Now.Month - 1
                Me.DropAnio.Text = DateTime.Now.Year
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

        Me.dtDatos.Clear()
        Me.gvLV.DataSource = Me.dtDatos

        Dim mes As String
        mes = Me.ComboMes.SelectedValue
        Dim itemLv, factura, cliente As ArrayList
        itemLv = CL.ObtenerLvMes(mes, Me.DropAnio.Text)
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
  
            Me.dtDatos.Clear()
            Me.gvLV.DataSource = Me.dtDatos
            Me.gvLV.DataBind()
        End If

        

    End Sub
    Protected Sub SampleGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLV.RowDataBound

        e.Row.ID = "VerPdf" & CStr(intGRID)
        intGRID += 1

    End Sub

    Private Sub showfacture(ByVal p As persona, ByVal f As factura, ByVal item As ArrayList)
        Try
            Me.CargarReporte(p, f, item)
            Me.ExportarReporte()
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub
    Public Sub CargarReporte(ByVal p As persona, ByVal f As factura, ByVal item As ArrayList)
        Try
            Dim reporte As New ReporteFactura
            Dim ds As New DataSet
            Dim dt1 As New DataTable
            Dim oRep As New ReportDocument
            Dim ResultadoCarga As Boolean
            ResultadoCarga = Me.CargarFactura(p, f, item)

            If (ResultadoCarga = True) Then
                reporte.SetDataSource(DataFactura)
                Me.MostrarReporte(reporte)

            Else
                MsgBox("Ha ocurrido un error Intentelo Nuevamente")
            End If

        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub
    Function CargarFactura(ByVal p As persona, ByVal f As factura, ByVal item As ArrayList) As Boolean

        Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
        nfi.CurrencyDecimalDigits = 0
        nfi.CurrencyGroupSeparator = "."

        Dim myRow As DataRow = DataFactura.DataTableDatosFact.NewRow
        Dim dato As item
        Dim myFila As DataRow
        Dim TotalPorItem As Integer = 0.0
        Dim Fecha As String = ""
        Dim Neto As Integer = 0.0
        Dim Iva As Integer = 0
        Dim Total As Integer = 0.0
        Dim fechaAux As String = ""

        Try

            ' Carga De Datos del Destinatario
            myRow.Item("ColumnNombre") = p.razonsocial
            myRow.Item("ColumnDireccion") = p.direccion
            myRow.Item("ColumnGiro") = p.giro
            myRow.Item("ColumnRut") = p.Rut
            myRow.Item("ColumnComuna") = p.comuna
            myRow.Item("ColumnNumeroFactura") = f.NumeroFactura
            myRow.Item("ColumnGuiaDespacho") = f.NguiaDespacho
            myRow.Item("ColumnOrdenCompra") = f.OrdenDeCompra
            myRow.Item("ColumnCondVenta") = f.CondicionVenta
            myRow.Item("ColumnTelefono") = p.fono
            myRow.Item("ColumnVendedor") = f.Vendedor
            Fecha = f.Fecha

            myRow.Item("ColumnDia") = Fecha.Chars(0) + Fecha.Chars(1)
            fechaAux = Fecha.Chars(3) + Fecha.Chars(4)
            myRow.Item("ColumnMes") = CL.convierteMes(fechaAux)
            myRow.Item("ColumnAño") = Fecha.Chars(6) + Fecha.Chars(7) + Fecha.Chars(8) + Fecha.Chars(9)

            'Carga De Datos de Items - Factura

            With item
                ' Recorrer las filas del dataGridView  
                For fila As Integer = 0 To .Count - 1
                    dato = DirectCast(.Item(fila), item)
                    myFila = DataFactura.DataTableItemFactura.NewRow
                    ' Recorrer la cantidad de columnas que contiene el dataGridView  
                    myFila.Item("factItem") = dato.Item
                    myFila.Item("factCodigo") = dato.Codigo
                    myFila.Item("factDetalla") = dato.Detalle
                    myFila.Item("factCantidad") = dato.Cantidad
                    myFila.Item("factPrecioUnitario") = CInt(dato.PrecioUnitario)
                    TotalPorItem = CInt(dato.Cantidad) * CInt(dato.PrecioUnitario)
                    Total = Total + TotalPorItem
                    myFila.Item("factPrecioTotal") = CInt(TotalPorItem).ToString("C", nfi)
                    DataFactura.DataTableItemFactura.Rows.Add(myFila)
                Next fila
            End With

            Neto = f.Neto
            Iva = f.Total

            myRow.Item("ColumnNeto") = Neto.ToString("C", nfi)
            myRow.Item("ColumnIva") = Iva.ToString("C", nfi)
            myRow.Item("ColumnTotal") = Total.ToString("C", nfi)
            myRow.Item("ColumnTotalNumero") = CL.Letras(Total)
            DataFactura.DataTableDatosFact.Rows.Add(myRow)

            'Return True

        Catch
            'Return False
        End Try
        Return True
    End Function
    Public Sub ExportarReporte()
        Try
            If Not Session("REPORTE_FACTURA") Is Nothing Then
                Dim reporte As ReportClass = Session("REPORTE_FACTURA")
                Dim info As CrystalDecisions.CrystalReports.Engine.ReportClass = Session("REPORTE_FACTURA")
                info.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                info.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
                Dim rptStream As New System.IO.MemoryStream
                rptStream = CType(info.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat), System.IO.MemoryStream)
                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.Buffer = False
                Response.ContentType = "application/vnd.pdf"
                Response.AddHeader("Content-Disposition", "attachment;filename=Factura.pdf")
                Response.Flush()
                Response.BinaryWrite(rptStream.ToArray())
                Response.End()
            End If
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub
    Public Sub MostrarReporte(ByVal reporte As CrystalDecisions.CrystalReports.Engine.ReportClass)
        Try
            reporte.PrintOptions.PaperSize = CrystalDecisions.[Shared].PaperSize.PaperLetter
            Session("REPORTE_FACTURA") = reporte
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub
    
    

    Protected Sub VerPdf_Click1(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLV.RowCommand

        If (e.CommandName = "VerPdfButton") Then

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            Dim row As GridViewRow = gvLV.Rows(index)

            Dim nfactrura As String = row.Cells(1).Text
            Dim nrutcliente As String = row.Cells(0).Text

            Dim fac As factura = DirectCast(CL.BuscarFactura(nfactrura).Item(0), factura)

            Dim itemfac As ArrayList = CL.ObtenerItemFactura(nfactrura)

            Dim cliente As ArrayList = CL.retornaCliente(nrutcliente)

            Dim per As persona = New persona(cliente.Item(0), cliente.Item(1), _
            cliente.Item(2), cliente.Item(3), cliente.Item(4), cliente.Item(5), cliente.Item(6))

            'MsgBox(per.Rut + " " + per.razonsocial + " " + per.giro + " " + per.fono + " " + per.direccion + " " + per.comuna + " " + per.Ciudad)
            ' MsgBox(fac.Iva + " " + fac.Neto + " " + fac.Total + " " + fac.Fecha + " " + fac.Vendedor)
            Me.showfacture(per, fac, itemfac)


        End If
    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click

    End Sub
End Class