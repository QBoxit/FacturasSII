Imports ExLogger3.Logica
Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Partial Public Class frmNotaCredito
    Inherits System.Web.UI.Page

    Dim dtDatos As New DataTable
    Dim DataNC As New DataNotaCredito
    Dim CL As New ControladorLogica



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("tb4") Is Nothing Then
            Session("tb4") = dtDatos
        Else
            dtDatos = DirectCast(Session("tb4"), DataTable)
        End If
        If Not Page.IsPostBack Then
            Try
                dtDatos.Columns.Add(New DataColumn("ITEM"))
                dtDatos.Columns.Add(New DataColumn("CODIGO"))
                dtDatos.Columns.Add(New DataColumn("DETALLE"))
                dtDatos.Columns.Add(New DataColumn("CANTIDAD"))
                dtDatos.Columns.Add(New DataColumn("PRECIOUNITARIO"))

                Me.gvwDatos.DataSource = dtDatos
                Me.gvwDatos.DataBind()

            Catch
            End Try

        End If

    End Sub

    Protected Sub imgbtnExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnExportar.Click
        Try
            Me.CargarReporte()
            Me.ExportarReporte()

        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
            'Utilidades.MuestraMensaje("Mensaje", Me.UpdatePanel1, Me.Page, "Ha ocurrido un error <br/> por favor contacte al administrador del sistema", 3)
        End Try
    End Sub


#Region "Metodos"

    Public Sub CargarReporte()
        Try
            Dim reporte As New NotaCredito
            Dim ds As New DataSet
            Dim dt1 As New DataTable
            Dim oRep As New ReportDocument
            Dim filas As Integer = Me.gvwDatos.Rows.Count
            Dim ResultadoCarga As Boolean
            ResultadoCarga = Me.CargarNotaCredito()

            If (ResultadoCarga = True) Then
                reporte.SetDataSource(DataNC)
                Me.MostrarReporte(reporte)

            Else
                MsgBox("Ha ocurrido un error Intentelo Nuevamente")
            End If

        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub


    Public Sub MostrarReporte(ByVal reporte As CrystalDecisions.CrystalReports.Engine.ReportClass)
        Try
            reporte.PrintOptions.PaperSize = CrystalDecisions.[Shared].PaperSize.PaperLetter
            Session("NotaCredito") = reporte
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub

    Public Sub ExportarReporte()
        Try
            If Not Session("NotaCredito") Is Nothing Then
                Dim reporte As ReportClass = Session("NotaCredito")
                Dim info As CrystalDecisions.CrystalReports.Engine.ReportClass = Session("NotaCredito")
                info.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
                info.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
                Dim rptStream As New System.IO.MemoryStream
                rptStream = CType(info.ExportToStream(CrystalDecisions.[Shared].ExportFormatType.PortableDocFormat), System.IO.MemoryStream)
                Response.Clear()
                Response.ClearContent()
                Response.ClearHeaders()
                Response.Buffer = False
                Response.ContentType = "application/vnd.pdf"
                Response.AddHeader("Content-Disposition", "attachment;filename=GuiaDespacho.pdf")
                Response.Flush()
                Response.BinaryWrite(rptStream.ToArray())
                Response.End()
            End If
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub

#End Region

    Private Sub Limpiar()

        Me.textVendedor.Text = String.Empty
        Me.textRut.Text = String.Empty
        Me.TextRazonSocial.Text = String.Empty
        Me.textIdFactura.Text = String.Empty
        Me.textGuiaDespacho.Text = String.Empty
        Me.textGiro.Text = String.Empty
        Me.textFono.Text = String.Empty
        Me.textFecha.Text = String.Empty
        Me.textDireccion.Text = String.Empty
        Me.textCondVenta.Text = String.Empty
        Me.textComuna.Text = String.Empty
    End Sub

    Protected Sub imgbtnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLimpiar.Click
        Try
            Me.Limpiar()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub textIdFactura_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textIdFactura.TextChanged

        Dim factura As New ArrayList
        Dim cliente As New ArrayList
        Dim itemFactura As New ArrayList

        factura = CL.BuscarFactura(Me.textIdFactura.Text)
        If factura.Count > 0 Then
            Dim fac As factura
            fac = DirectCast(factura.Item(0), factura)
            cliente = CL.retornaCliente(fac.Cliente)

            If cliente.Count > 0 Then

                itemFactura = CL.ObtenerItemFactura(Me.textIdFactura.Text)


                Me.avisoNT.Text = ""
                Me.textVendedor.Text = fac.Vendedor
                Me.textRut.Text = fac.Cliente
                Me.TextRazonSocial.Text = cliente.Item(1)
                Me.textIdFactura.Text = fac.NumeroFactura
                Me.textGuiaDespacho.Text = fac.NguiaDespacho
                Me.textGiro.Text = cliente.Item(3)
                Me.textFono.Text = cliente.Item(5)
                Me.textFecha.Text = fac.Fecha
                Me.textDireccion.Text = cliente.Item(2)
                Me.textCondVenta.Text = fac.CondicionVenta
                Me.textComuna.Text = cliente.Item(6)
                Me.txtOrdenCompra.Text = fac.OrdenDeCompra
                Me.llenarDataGrid(itemFactura)

            End If
        Else
            Me.avisoNT.Text = "Numero De factura Ingresado no valido, reintente Nuevamente"
            Me.Limpiar()
            Me.dtDatos.Clear()
            Me.gvwDatos.DataSource = Me.dtDatos
            Me.gvwDatos.DataBind()

        End If
        Me.dtDatos.Clear()
        Me.gvwDatos.DataSource = Me.dtDatos

    End Sub

    Private Sub llenarDataGrid(ByVal itemFactura As ArrayList)
        Try
            Dim i As Integer

            For i = 0 To itemFactura.Count
                Dim drDatos As DataRow = dtDatos.NewRow
                Dim item As item
                item = DirectCast(itemFactura.Item(i), item)

                drDatos.Item("ITEM") = item.Item
                drDatos.Item("CODIGO") = item.Codigo
                drDatos.Item("DETALLE") = item.Detalle
                drDatos.Item("CANTIDAD") = item.Cantidad
                drDatos.Item("PRECIOUNITARIO") = item.PrecioUnitario
                dtDatos.Rows.Add(drDatos)

                Me.gvwDatos.DataSource = dtDatos
                Me.gvwDatos.DataBind()

            Next i

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim atLeastOneRowDeleted As Boolean = False

        ' Iterate through the Products.Rows property
        For Each row As GridViewRow In Me.gvwDatos.Rows

            ' Access the CheckBox
            Dim cb As CheckBox = row.FindControl("itemSelector")
            If cb IsNot Nothing AndAlso cb.Checked Then
                ' Delete row! (Well, not really...)
                atLeastOneRowDeleted = True

                ' First, get the ProductID for the selected row
                Dim productID As Integer = Convert.ToInt32(gvwDatos.DataKeys(row.RowIndex - 1).Value)

                ' "Delete" the row
                avisoNT.Text &= String.Format("This would have deleted ProductID {0}<br />", productID)

                '... To actually delete the product, use ...
                ' Dim productAPI As New ProductsBLL
                ' productAPI.DeleteProduct(productID)
                '............................................
            End If
        Next

        ' Show the Label if at least one row was deleted...
        avisoNT.Visible = atLeastOneRowDeleted

    End Sub

    Function CargarNotaCredito() As Boolean

        Dim myRow As DataRow = DataNC.DataTableDatos.NewRow
        Dim filaGrid As GridViewRow
        Dim myFila As DataRow
        Dim TotalPorItem As Integer = 0
        Dim Fecha As String = ""
        Dim Neto As Integer = 0.0
        Dim Iva As Integer = 0
        Dim Total As Integer = 0.0
        Dim fechaAux As String = ""

        Try
            ' Carga De Datos del Destinatario
            myRow.Item("NumeroNC") = "NUMERO PRUEBA"
            myRow.Item("Nombre") = Me.TextRazonSocial.Text
            myRow.Item("Direccion") = Me.textDireccion.Text
            myRow.Item("Giro") = Me.textGiro.Text
            myRow.Item("Rut") = Me.textRut.Text
            myRow.Item("Comuna") = Me.textComuna.Text
            myRow.Item("Telefono") = Me.textFono.Text
            myRow.Item("FacturaNum") = Me.textIdFactura.Text
            myRow.Item("Vendedor") = Me.textVendedor.Text
            myRow.Item("OrdenCompra") = Me.txtOrdenCompra.Text

            Fecha = Me.textFecha.Text

            myRow.Item("FechaDia") = Fecha.Chars(0) + Fecha.Chars(1)
            fechaAux = Fecha.Chars(3) + Fecha.Chars(4)
            myRow.Item("FechaMes") = CL.convierteMes(fechaAux)
            myRow.Item("FechaAÑo") = Fecha.Chars(6) + Fecha.Chars(7) + Fecha.Chars(8) + Fecha.Chars(9)

            'Carga De Datos de Items - NC

            With Me.gvwDatos
                ' Recorrer las filas del dataGridView  
                For fila As Integer = 0 To .Rows.Count - 1
                    filaGrid = Me.gvwDatos.Rows(fila)
                    myFila = DataNC.DataTableItem.NewRow
                    ' Recorrer la cantidad de columnas que contiene el dataGridView  
                    Dim cb As CheckBox = filaGrid.FindControl("itemSelector")

                    If cb.Checked = True Then
                        myFila.Item("Item") = filaGrid.Cells(0).Text
                        myFila.Item("Codigo") = filaGrid.Cells(1).Text
                        myFila.Item("Descripcion") = filaGrid.Cells(2).Text
                        myFila.Item("Cantidad") = filaGrid.Cells(3).Text
                        myFila.Item("PrecioUnitario") = filaGrid.Cells(4).Text
                        TotalPorItem = CInt(filaGrid.Cells(3).Text) * CInt(filaGrid.Cells(4).Text)
                        Total = Total + TotalPorItem
                        myFila.Item("TotalItem") = TotalPorItem
                        DataNC.DataTableItem.Rows.Add(myFila)

                    End If

                Next fila
            End With

            Neto = Total / (CDbl(0.19) + 1)
            Iva = Total - Neto

            myRow.Item("Neto") = Neto
            myRow.Item("Iva") = Iva
            myRow.Item("Total") = Total
            myRow.Item("ValorLetras") = CL.Letras(1000)
            DataNC.DataTableDatos.Rows.Add(myRow)

            Return True

        Catch
            Return False
        End Try
    End Function


End Class