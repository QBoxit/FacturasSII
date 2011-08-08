Imports ExLogger3.Logica
Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

'comentario
Partial Public Class frmNotaCredito
    Inherits System.Web.UI.Page

    Dim dtDatos As New DataTable
    Dim DataNC As New DataNotaCredito
    Dim CL As New ControladorLogica
    Dim fac As factura
    Dim criterio As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session("tb4") Is Nothing Then
            Session("tb4") = dtDatos
        Else
            dtDatos = DirectCast(Session("tb4"), DataTable)
        End If
        If Not Page.IsPostBack Then
            Try
                Me.DropCriterio.SelectedIndex = -1
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
        criterio = Me.DropCriterio.SelectedValue
    End Sub

    Protected Sub imgbtnExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnExportar.Click
        If BasicDatePicker1.Text = "" Then
            MsgBox("Debe ingresar la fecha")
        Else
            Try
                Me.CargarReporte()
                Me.ExportarReporte()

            Catch ex As Exception
                Dim log As Logger = Logger.getInstance
                log.guardarExcepcion(ex)
                'Utilidades.MuestraMensaje("Mensaje", Me.UpdatePanel1, Me.Page, "Ha ocurrido un error <br/> por favor contacte al administrador del sistema", 3)
            End Try
        End If
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
        Me.textDireccion.Text = String.Empty
        Me.textCondVenta.Text = String.Empty
        Me.textComuna.Text = String.Empty
        Me.txtCiudad.Text = String.Empty
        Me.TextDetalle.Text = String.Empty
        dtDatos.Clear()
        Me.gvwDatos.DataBind()

    End Sub

    Protected Sub imgbtnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLimpiar.Click
        Try
            Me.Limpiar()
        Catch ex As Exception

        End Try
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
        Dim arrayItemaux As New ArrayList
        Dim ivaAplicado As Integer = 0
        Dim numeroNC As Integer = CInt(CL.ObtenerIDNotaCredito()) + 1
        Dim detalle As String
        


        Try
            ' Carga De Datos del Destinatario
            myRow.Item("NumeroNC") = numeroNC
            myRow.Item("Nombre") = Me.TextRazonSocial.Text
            myRow.Item("Direccion") = Me.textDireccion.Text
            myRow.Item("Giro") = Me.textGiro.Text
            myRow.Item("Rut") = Me.textRut.Text
            myRow.Item("Comuna") = Me.textComuna.Text
            myRow.Item("Telefono") = Me.textFono.Text
            myRow.Item("FacturaNum") = Me.textIdFactura.Text
            myRow.Item("Vendedor") = Me.textVendedor.Text
            myRow.Item("OrdenCompra") = Me.txtOrdenCompra.Text
            myRow.Item("Ncedible") = "DOCUMENTO ORIGINAL"

            If criterio = "anulacionItem" Then
                myRow.Item("Detalle") = ""
            Else
                If criterio = "anulacionDatos" Then
                    myRow.Item("Detalle") = TextDetalle.Text
                End If
            End If

            Fecha = BasicDatePicker1.Text

            myRow.Item("FechaDia") = Fecha.Chars(0) + Fecha.Chars(1)
            fechaAux = Fecha.Chars(3) + Fecha.Chars(4)
            myRow.Item("FechaMes") = CL.convierteMes(fechaAux)
            myRow.Item("FechaAÑo") = Fecha.Chars(6) + Fecha.Chars(7) + Fecha.Chars(8) + Fecha.Chars(9)

            'Carga De Datos de Items - NC

            If criterio = "anulacionItem" Then
                detalle = "Sin Detalle"
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

                            Dim nuevoItem As New item(filaGrid.Cells(0).Text, filaGrid.Cells(1).Text, filaGrid.Cells(2).Text, filaGrid.Cells(3).Text, filaGrid.Cells(4).Text)
                            arrayItemaux.Add(nuevoItem)
                        End If
                    Next fila
                End With

            Else
                If criterio = "anulacionDatos" Then
                    detalle = TextDetalle.Text
                    With Me.gvwDatos
                        ' Recorrer las filas del dataGridView  
                        For fila As Integer = 0 To .Rows.Count - 1
                            filaGrid = Me.gvwDatos.Rows(fila)
                            myFila = DataNC.DataTableItem.NewRow
                            ' Recorrer la cantidad de columnas que contiene el dataGridView  

                            myFila.Item("Item") = filaGrid.Cells(0).Text
                            myFila.Item("Codigo") = filaGrid.Cells(1).Text
                            myFila.Item("Descripcion") = filaGrid.Cells(2).Text
                            myFila.Item("Cantidad") = filaGrid.Cells(3).Text
                            myFila.Item("PrecioUnitario") = filaGrid.Cells(4).Text
                            TotalPorItem = CInt(filaGrid.Cells(3).Text) * CInt(filaGrid.Cells(4).Text)
                            Total = Total + TotalPorItem
                            myFila.Item("TotalItem") = TotalPorItem
                            DataNC.DataTableItem.Rows.Add(myFila)
                            Dim nuevoItem As New item(filaGrid.Cells(0).Text, filaGrid.Cells(1).Text, filaGrid.Cells(2).Text, filaGrid.Cells(3).Text, filaGrid.Cells(4).Text)
                            arrayItemaux.Add(nuevoItem)
                        Next fila
                    End With
                End If
            End If


            Dim FacturaList As ArrayList = CL.BuscarFactura(Me.textIdFactura.Text)
            Dim FacturaAux As factura = DirectCast(FacturaList.Item(0), factura)

            ivaAplicado = (CDbl(FacturaAux.Iva) * 100) / CDbl(FacturaAux.Total)
            Iva = Total * (ivaAplicado / 100)
            Neto = Total - Iva

            Dim NotaCredito As New NotaDeCredito(numeroNC, FacturaAux.NumeroFactura, Fecha, Iva, Neto, Total, detalle)
            CL.ingresarNotaCredito(NotaCredito, arrayItemaux)

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

    Protected Sub updateUser_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles updateUser.Click
        Dim res As Integer
        Dim valido As Boolean
        Dim p As persona


        res = MsgBox("NOTA:" & vbCrLf & "Desea Actualizar Datos de : " + Me.textRut.Text, vbQuestion + vbYesNo + vbDefaultButton2, "Aviso")

        If res = 6 Then
            If Me.textRut.Text <> "" And Me.TextRazonSocial.Text <> "" And Me.textDireccion.Text <> "" And Me.textGiro.Text <> "" And Me.textComuna.Text <> "" And Me.textFono.Text <> "" Then
                valido = CL.validarRut(Me.textRut.Text)

                If valido Then
                    p = New persona(Me.textRut.Text, Me.TextRazonSocial.Text, Me.textDireccion.Text, Me.textGiro.Text, Me.textComuna.Text, Me.textFono.Text, Me.txtCiudad.Text)
                    CL.ActualizarPersona(p)
                    MsgBox(Me.textRut.Text + " A Sido Actualizado Exitosamente")

                End If
            End If
        End If
    End Sub

    Public Sub selectCriterio()
        If criterio = "anulacionItem" Then
            readOnlyText()
            Me.PanelGrid.Visible = True
            Me.PanelDetalle.Visible = False
            imgbtnExportar.Visible = True
            updateUser.Visible = False
        Else
            If criterio = "anulacionDatos" Then
                readOnlyText()
                Me.PanelGrid.Visible = False
                Me.PanelDetalle.Visible = True
                imgbtnExportar.Visible = True
                updateUser.Visible = True
            Else
                If criterio = "actualizar" Then
                    readOnlyText()
                    imgbtnExportar.Visible = False
                    updateUser.Visible = True
                    Me.PanelGrid.Visible = False
                    Me.PanelDetalle.Visible = False
                End If
            End If
        End If
    End Sub

    Public Sub readOnlyText()
        Me.textComuna.ReadOnly = False
        Me.txtCiudad.ReadOnly = False
        Me.textIdFactura.ReadOnly = False
        Me.textFono.ReadOnly = False
        Me.textDireccion.ReadOnly = False
        Me.textGiro.ReadOnly = False
        Me.TextRazonSocial.ReadOnly = False
    End Sub

    Protected Sub ButtonCriterio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCriterio.Click
        Me.selectCriterio()
    End Sub

    Public Sub rellenarDatos()
        Dim factura As New ArrayList
        Dim cliente As New ArrayList
        Dim itemFactura As New ArrayList

        factura = CL.BuscarFactura(Me.textIdFactura.Text)
        If factura.Count > 0 Then
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
                Me.textDireccion.Text = cliente.Item(2)
                Me.textCondVenta.Text = fac.CondicionVenta
                Me.textComuna.Text = cliente.Item(4)
                Me.txtCiudad.Text = cliente.Item(6)
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


    Protected Sub BttnFactura_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BttnFactura.Click
        rellenarDatos()
    End Sub
End Class