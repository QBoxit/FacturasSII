Imports ExLogger3.Logica
Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.OleDb
Imports Npgsql
Imports System.Security.Cryptography.SHA1
Imports System
Imports System.Globalization

Partial Public Class frmFactura
    Inherits System.Web.UI.Page
   
    Dim dtDatos As New DataTable
    Dim dataset As New DataSet

    Dim arrayAux As New ArrayList
    Dim CL As New ControladorLogica
    Dim res As Integer
    Dim DataFactura As New DataSetFactura
    Dim personaFactura As New persona("", "", "", "", "", "", "")

    Public Event RowDeleting As GridViewDeleteEventHandler
    Public Event rowUpdate As GridViewUpdatedEventHandler

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        'Me.Button1.Attributes.Add("onclick", "return confirm('Are you sure you want to delete?');")
        'Dim dsad As New ClassXML
        'dsad.creaXml()
        txtCantidad.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")
        txtPrecioUnitario.Attributes.Add("onkeypress", "javascript:return ValidNum(event);")

        'se guardan datos en secion para el positback de la pagina respecto al los datos del datagridview
        If Session("tb3") Is Nothing Then
            Session("tb3") = dtDatos
        Else
            dtDatos = DirectCast(Session("tb3"), DataTable)
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
                Me.dataset.Tables.Add(dtDatos)
         
            Catch
            End Try

        End If
        Me.DropCondVenta.Items.Clear()
        Me.DropIva.Items.Clear()
        Me.DropCondVenta.Items.Add("Efectivo")
        Me.DropCondVenta.Items.Add("Cheque")
        Me.DropCondVenta.Items.Add("Otros")
        Me.DropIva.Items.Add("0,19")
        Me.DropIva.Items.Add("0,18")
        Me.DropIva.Items.Add("0,20")


    End Sub


#Region "Metodos"

    Function CargarFactura() As Boolean
        Dim nfi As NumberFormatInfo = New CultureInfo("en-US", False).NumberFormat
        nfi.CurrencyDecimalDigits = 0
        nfi.CurrencyGroupSeparator = "."

        Dim myRow As DataRow = DataFactura.DataTableDatosFact.NewRow
        Dim filaGrid As GridViewRow
        Dim myFila As DataRow
        Dim TotalPorItem As Integer = 0.0
        Dim Fecha As String = ""
        Dim Neto As Integer = 0.0
        Dim Iva As Integer = 0
        Dim Total As Integer = 0.0
        Dim fechaAux As String = ""



        Try
            If (Me.txtRazon.Text <> "" And Me.txtDireccion.Text <> "" And Me.txtGiro.Text <> "" And Me.txtRut.Text <> "" And Me.txtComuna.Text <> "" And Me.txtFono.Text <> "") Then

                ' Carga De Datos del Destinatario
                myRow.Item("ColumnNombre") = Me.txtRazon.Text
                myRow.Item("ColumnDireccion") = Me.txtDireccion.Text
                myRow.Item("ColumnGiro") = Me.txtGiro.Text
                myRow.Item("ColumnRut") = Me.txtRut.Text
                myRow.Item("ColumnComuna") = Me.txtComuna.Text
                myRow.Item("ColumnNumeroFactura") = CL.obtieneIDMax()
                myRow.Item("ColumnGuiaDespacho") = Me.TextGuiaDespacho.Text
                myRow.Item("ColumnOrdenCompra") = Me.TextOrdenCompra.Text
                myRow.Item("ColumnCondVenta") = Me.DropCondVenta.Text
                myRow.Item("ColumnTelefono") = Me.txtFono.Text
                myRow.Item("ColumnVendedor") = Me.TextVendedor.Text
                Fecha = txtfecha.Text

                myRow.Item("ColumnDia") = Fecha.Chars(0) + Fecha.Chars(1)
                fechaAux = Fecha.Chars(3) + Fecha.Chars(4)
                myRow.Item("ColumnMes") = CL.convierteMes(fechaAux)
                'myRow.Item("ColumnAño") = Fecha.Chars(6) + Fecha.Chars(7) + Fecha.Chars(8) + Fecha.Chars(9)

                'Carga De Datos de Items - Factura

                With Me.gvwDatos
                    ' Recorrer las filas del dataGridView  
                    For fila As Integer = 0 To .Rows.Count - 1
                        filaGrid = Me.gvwDatos.Rows(fila)
                        myFila = DataFactura.DataTableItemFactura.NewRow
                        ' Recorrer la cantidad de columnas que contiene el dataGridView  
                        myFila.Item("factItem") = filaGrid.Cells(0).Text
                        myFila.Item("factCodigo") = filaGrid.Cells(1).Text
                        myFila.Item("factDetalla") = filaGrid.Cells(2).Text
                        myFila.Item("factCantidad") = filaGrid.Cells(3).Text
                        myFila.Item("factPrecioUnitario") = CInt(filaGrid.Cells(4).Text)
                        TotalPorItem = CInt(filaGrid.Cells(3).Text) * CInt(filaGrid.Cells(4).Text)
                        Total = Total + TotalPorItem
                        myFila.Item("factPrecioTotal") = CInt(TotalPorItem).ToString("C", nfi)
                        DataFactura.DataTableItemFactura.Rows.Add(myFila)
                    Next fila
                End With

                Dim IvaVerdadero As Double = CDbl(Me.DropIva.Text) + 1
                Neto = (Total / IvaVerdadero)
                Iva = Total - Neto

                myRow.Item("ColumnNeto") = Neto.ToString("C", nfi)
                myRow.Item("ColumnIva") = Iva.ToString("C", nfi)
                myRow.Item("ColumnTotal") = Total.ToString("C", nfi)
                myRow.Item("ColumnTotalNumero") = CL.Letras(Total)
                DataFactura.DataTableDatosFact.Rows.Add(myRow)

                Return True
            Else
                MsgBox("Debe Ingresar los datos del destinatario Previamente")
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Sub meteReporte()
        Dim ruta As String = System.AppDomain.CurrentDomain.BaseDirectory()
        Dim oRep As New ReportDocument
        Me.CargarFactura()
        oRep.Load(ruta + "\Interfaz\Reportes\ReporteFactura.rpt")
        oRep.SetDataSource(DataFactura)
        CrView.ReportSource = oRep
    End Sub

    Public Sub CargarReporte()
        Try
            Dim reporte As New ReporteFactura
            Dim ds As New DataSet
            Dim dt1 As New DataTable
            Dim oRep As New ReportDocument
            Dim filas As Integer = Me.gvwDatos.Rows.Count
            Dim ResultadoCarga As Boolean
            ResultadoCarga = Me.CargarFactura()

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
    Public Sub MostrarReporte(ByVal reporte As CrystalDecisions.CrystalReports.Engine.ReportClass)
        Try
            reporte.PrintOptions.PaperSize = CrystalDecisions.[Shared].PaperSize.PaperLetter
            Session("REPORTE_FACTURA") = reporte
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub
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

    Public Sub Limpiar()
        Me.txtItem.Text = String.Empty
        Me.txtCodigo.Text = String.Empty
        Me.txtDetalle.Text = String.Empty
        Me.txtCantidad.Text = String.Empty
        Me.txtPrecioUnitario.Text = String.Empty
        ' Me.dtDatos.Clear()
    End Sub
    Public Sub LimpiarCliente()
        Me.txtRut.Text = String.Empty
        Me.txtRazon.Text = String.Empty
        Me.txtDireccion.Text = String.Empty
        Me.txtFono.Text = String.Empty
        Me.txtGiro.Text = String.Empty
        Me.txtComuna.Text = String.Empty
        Me.txtCiudad.Text = String.Empty
    End Sub

#End Region

    
    Protected Sub imgbtnLimpiar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnLimpiar.Click
        Try
            Me.txtRazon.Text = String.Empty
            Me.txtCantidad.Text = String.Empty
            Me.txtCodigo.Text = String.Empty
            Me.txtComuna.Text = String.Empty

            Me.txtDetalle.Text = String.Empty
            Me.txtDireccion.Text = String.Empty
            Me.txtFono.Text = String.Empty
            Me.txtGiro.Text = String.Empty
            Me.txtItem.Text = String.Empty

            Me.txtPrecioUnitario.Text = String.Empty
            Me.txtRut.Text = String.Empty

            Me.gvwDatos.DataSource = Nothing
            Me.gvwDatos.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub imgbtnExportar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnExportar.Click

        Dim res As Integer
        Dim i As Integer
        Dim row As GridViewRow

        Dim Neto As Integer = 0.0
        Dim Iva As Integer = 0
        Dim Total As Integer = 0.0
        Dim TotalPorItem As Integer = 0.0
        Me.dtDatos.Clear()
        'Me.gvwDatos.DataSource = dtDatos
        'Me.gvwDatos.DataBind()

        Dim item, codigo, detalle, cantidad, precioUni As String

        Dim arrayItem As New ArrayList
        res = MsgBox("Esta Seguro De Querer Generar La Factura, Esta Accion No Se Puede Revertir", vbQuestion + vbYesNo + vbDefaultButton2, "Aviso")

   

        If res = "6" Then


            ' validar parametros nulos al crear la factura.
            Dim fact As factura = New factura(Me.TextVendedor.Text, Me.txtRut.Text, Me.TextOrdenCompra.Text, Me.TextGuiaDespacho.Text _
             , Me.DropCondVenta.Text.ToString, Me.txtfecha.Text, 0, 0, 0, Me.personaFactura)


            For i = 0 To Me.gvwDatos.Rows.Count - 1
                row = Me.gvwDatos.Rows(i)

                item = row.Cells(0).Text
                codigo = row.Cells(1).Text
                detalle = row.Cells(2).Text
                cantidad = row.Cells(3).Text
                precioUni = row.Cells(4).Text

                TotalPorItem = CInt(row.Cells(3).Text) * CInt(row.Cells(4).Text)
                Total = Total + TotalPorItem

                Dim it As item = New item(item, codigo, detalle, cantidad, precioUni)
                arrayItem.Add(it)
                'MsgBox(item + " " + codigo + " " + detalle + " " + cantidad + " " + precioUni)

            Next

            'ARREGLAR
            Neto = Total / (CDbl(Me.DropIva.Text) + 1)
            Iva = Total - Neto

            fact.Neto = Neto
            fact.Iva = Iva
            fact.Total = Neto + Iva

            CL.IngresarFactura(fact, arrayItem)

            showfacture()
            MsgBox("La Factura ha Sido Generada Exitosamente", , "Aviso")

        End If

    End Sub
    Private Sub showfacture()
        Try
            Me.CargarReporte()
            Me.ExportarReporte()
        Catch ex As Exception
            Dim log As Logger = Logger.getInstance
            log.guardarExcepcion(ex)
        End Try
    End Sub
    Protected Sub imgbtnAgregar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAgregar.Click
        Try
            Dim drDatos As DataRow = dtDatos.NewRow

            If Me.txtItem.Text <> "" And Me.txtCodigo.Text <> "" And Me.txtDetalle.Text <> _
            "" And Me.txtCantidad.Text <> "" And Me.txtPrecioUnitario.Text <> "" Then

                drDatos.Item("ITEM") = Me.txtItem.Text
                drDatos.Item("CODIGO") = Me.txtCodigo.Text
                drDatos.Item("DETALLE") = Me.txtDetalle.Text
                drDatos.Item("CANTIDAD") = Me.txtCantidad.Text
                drDatos.Item("PRECIOUNITARIO") = CInt(Me.txtPrecioUnitario.Text)

                dtDatos.Rows.Add(drDatos)

                Me.gvwDatos.DataSource = dtDatos
                Me.gvwDatos.DataBind()
                Me.Limpiar()

                Me.ingresado.Text = "Los datos han sido Ingresados correctamentes."
            Else
                Me.ingresado.Text = "Complete todos los datos."


            End If

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub gvwDatos_RowDeleting(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvwDatos.RowDeleting
        'Try
        '    Dim dato As Integer = Me.gvwDatos.DataKeys(e.RowIndex).Value()
        '    Me.gvwDatos.DeleteRow(dato)
        '    Me.gvwDatos.DataSource = dtDatos
        'Catch
        'End Try

    End Sub
   
    Protected Sub gvwDatos_RowEditing(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvwDatos.RowEditing

    End Sub


    Protected Sub txtCantidad_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged
        If Not IsNumeric(txtCantidad.Text) Then
            Response.Write("Solo deben ser numeros")
        Else
            Response.Write("Haz introducido numeros")
        End If
    End Sub

    Protected Sub txtRut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRut.TextChanged
        Dim rutEntero As String = txtRut.Text
        Dim Array2 As New ArrayList
        Dim valido As Boolean
        Dim rutFormato As String

        If rutEntero = "" Then
            Me.avisorut.Text = "Favor Ingrese un RUT"
        Else
            rutFormato = CL.formato(rutEntero)
            valido = CL.validarRut(rutFormato)

            If valido = False Then
                Me.avisorut.Text = "El RUT Ingresado No Es Valido, Reintente Nuevamente"
                Me.txtRut.Text = String.Empty
                Me.LimpiarCliente()
                updateUser.Visible = False
                aduser.Visible = False
            Else
                Array2 = CL.retornaCliente(rutFormato)
                If (Array2.Count = 0) Then

                    res = MsgBox("NOTA:" & vbCrLf & "El usuario no se encuentra en la el sistema, desea ingresarlo?", vbQuestion + vbYesNo + vbDefaultButton2, "Aviso")

                    If res = 6 Then
                        Me.txtRut.Text = rutFormato
                        aduser.Visible = True
                        updateUser.Visible = True
                    Else
                        Me.txtRut.Text = ""
                    End If
                Else
                    Me.avisorut.Text = ""
                    updateUser.Visible = True
                    Me.txtRut.Text = Array2.Item(0)
                    Me.txtRazon.Text = Array2.Item(1)
                    Me.txtDireccion.Text = Array2.Item(2)
                    Me.txtGiro.Text = Array2.Item(3)
                    Me.txtComuna.Text = Array2.Item(4)
                    Me.txtFono.Text = Array2.Item(5)
                    Me.txtCiudad.Text = Array2.Item(6)

                    personaFactura.Rut = Array2.Item(0)
                    personaFactura.razonsocial = Array2.Item(1)
                    personaFactura.direccion = Array2.Item(2)
                    personaFactura.giro = Array2.Item(3)
                    personaFactura.comuna = Array2.Item(4)
                    personaFactura.fono = Array2.Item(5)
                    personaFactura.Ciudad = Array2.Item(6)

                End If
            End If
        End If
    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim valido As Boolean
        Dim p As persona
        Dim res As Integer
        res = MsgBox("NOTA:" & vbCrLf & "Desea Ingresar Datos de : " + Me.txtRut.Text + " Al Sistema", vbQuestion + vbYesNo + vbDefaultButton2, "Aviso")

        If res = 6 And Me.txtRut.Text <> "" And Me.txtRazon.Text <> "" And Me.txtDireccion.Text <> "" And Me.txtGiro.Text <> "" And Me.txtComuna.Text <> "" And Me.txtFono.Text <> "" Then
            valido = CL.validarRut(Me.txtRut.Text)

            If (valido) Then
                p = New persona(Me.txtRut.Text, Me.txtRazon.Text, Me.txtDireccion.Text, Me.txtGiro.Text, Me.txtComuna.Text, Me.txtFono.Text, Me.txtCiudad.Text)

                CL.ingresarPersona(p)
                MsgBox("Los Datos Han Sido Ingresados Exitosamente")
                aduser.Visible = False
            Else
                MsgBox("Rut Ingresado No Valido, Reintente Nuevamente")
            End If
        Else
            MsgBox("Para Ingresar Un Cliente Debe Llenar Todos Los Campos")

        End If
    End Sub


    Protected Sub btnlimpiar_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnlimpiar.Click
        Me.LimpiarCliente()
        updateUser.Visible = False
        aduser.Visible = False
    End Sub

    Protected Sub updateUser_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles updateUser.Click
        Dim res As Integer
        Dim valido As Boolean
        Dim p As persona


        res = MsgBox("NOTA:" & vbCrLf & "Desea Actualizar Datos de : " + Me.txtRut.Text, vbQuestion + vbYesNo + vbDefaultButton2, "Aviso")

        If res = 6 Then
            If Me.txtRut.Text <> "" And Me.txtRazon.Text <> "" And Me.txtDireccion.Text <> "" And Me.txtGiro.Text <> "" And Me.txtComuna.Text <> "" And Me.txtFono.Text <> "" Then
                valido = CL.validarRut(Me.txtRut.Text)

                If valido Then
                    p = New persona(Me.txtRut.Text, Me.txtRazon.Text, Me.txtDireccion.Text, Me.txtGiro.Text, Me.txtComuna.Text, Me.txtFono.Text, Me.txtCiudad.Text)
                    CL.ActualizarPersona(p)
                    MsgBox(Me.txtRut.Text + " A Sido Actualizado Exitosamente")

                End If
            End If
        End If
    End Sub

    Protected Sub aduser_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles aduser.Click
        Dim valido As Boolean
        Dim p As persona
        Dim res As Integer
        res = MsgBox("NOTA:" & vbCrLf & "Desea Ingresar Datos de : " + Me.txtRut.Text + " Al Sistema", vbQuestion + vbYesNo + vbDefaultButton2, "Aviso")

        If res = 6 And Me.txtRut.Text <> "" And Me.txtRazon.Text <> "" And Me.txtDireccion.Text <> "" And Me.txtGiro.Text <> "" And Me.txtComuna.Text <> "" And Me.txtFono.Text <> "" Then
            valido = CL.validarRut(Me.txtRut.Text)

            If (valido) Then
                p = New persona(Me.txtRut.Text, Me.txtRazon.Text, Me.txtDireccion.Text, Me.txtGiro.Text, Me.txtComuna.Text, Me.txtFono.Text, Me.txtCiudad.Text)

                CL.ingresarPersona(p)
                MsgBox("Los Datos Han Sido Ingresados Exitosamente")
                aduser.Visible = False
            Else
                MsgBox("Rut Ingresado No Valido, Reintente Nuevamente")
            End If
        Else
            MsgBox("Para Ingresar Un Cliente Debe Llenar Todos Los Campos")

        End If

    End Sub

    Protected Sub VistaPrevia_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles VistaPrevia.Click
        Me.meteReporte()
    End Sub

    Protected Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CL.AutentificacionAutomatica()
    End Sub
End Class