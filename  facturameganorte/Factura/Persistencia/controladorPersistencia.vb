Imports System.Data
Imports System.IO
Imports System.Web.Mail

Public Class controladorPersistencia
    Dim BD As New bd
    Public Sub New()

    End Sub

    Public Sub ingresarPersona(ByVal p As persona)

        Dim query As String

        query = "insert into cliente values('" + p.Rut + "','" + p.razonsocial + _
                 "','" + p.direccion + "','" + p.giro + "','" + p.comuna + "','" + p.fono + "','" + p.Ciudad + "')"
        BD.EjecutarSinRetorno(query)

    End Sub

    Public Sub ActualizarPersona(ByVal p As persona)
        Dim query As String

        query = "update cliente set (razonsocial, direccion, giro,comuna, telefono,ciudad)=('" _
        + p.razonsocial + "','" + p.direccion + "','" + p.giro + "','" _
        + p.comuna + "','" + p.fono + "','" + p.Ciudad + "') where rut='" + p.Rut + "'"

        BD.EjecutarSinRetorno(query)
    End Sub

    Public Function retornaCliente(ByVal rutAux As String) As ArrayList
        Dim Array As New ArrayList
        Dim query As String = "select * from cliente where rut='" & rutAux & "'"
        Array = BD.ObtenerCliente(query)
        Return Array
    End Function

    Public Sub ingresarItemFactura(ByVal i As item,byval fac as Integer)
        Dim query As String
        query = "insert into itemFactura values('" + i.Codigo + "','" + i.Item + _
                 "','" + i.Detalle + "','" + i.PrecioUnitario + "','" + i.Cantidad + "','" + fac + "')"
    End Sub

    Public Function PreguntaLibroVenta(ByVal fact As factura) As Boolean

        Dim query As String
        Dim mes As String = fact.Fecha.Chars(3) + fact.Fecha.Chars(4)
        Dim año As String = fact.Fecha.Chars(6) + fact.Fecha.Chars(7) + fact.Fecha.Chars(8) + fact.Fecha.Chars(9)

        query = "select id from libroventa where mes ='" + mes _
         + "'" + "and anio='" + año + "'"

        If BD.EjecutarConReturn(query) = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ObtenerIDFactura() As String
        Dim query As String
        Dim array As New Hashtable
        query = "select max(id) maxima from factura"
        array = BD.EjecutarConRetorno(query)
        If BD.obtenerid(query) <> "" Then
            Return BD.obtenerid(query)
        End If
        Return ""
    End Function

    Public Function obtieneIDLibroVenta(ByVal fact) As Integer
        Dim año As String = fact.Fecha.Chars(6) + fact.Fecha.Chars(7) + fact.Fecha.Chars(8) + fact.Fecha.Chars(9)
        Dim mes As String = fact.Fecha.Chars(3) + fact.Fecha.Chars(4)
        Dim query As String
        Dim id As Integer = 0
        query = " select id from libroventa where mes = '" + mes + "' and anio = '" + año + "'"

        id = BD.EjecutarConInt(query)

        Return id
    End Function

    Public Function BuscarFactura(ByVal id As String) As ArrayList
        Dim array As New ArrayList
        Dim query As String
        query = "select * from factura where id=" + id + ""
        array = BD.Obtenerfactura(query)
        Return array
    End Function

    Public Sub IngresarFactura(ByVal fact As factura, ByVal array As ArrayList)

        Dim queryLibroVenta As String
        Dim queryfactura As String
        Dim queryItem As String
        Dim queryXML As String
        Dim queryhistorialfactura As String
        Dim idLibroVenta As String = ""
        Dim XmlByte As Byte()
        Dim año As String = fact.Fecha.Chars(6) + fact.Fecha.Chars(7) + fact.Fecha.Chars(8) + fact.Fecha.Chars(9)
        Dim mes As String = fact.Fecha.Chars(3) + fact.Fecha.Chars(4)

        ' se crea o actualiza el libro de venta
        If Me.PreguntaLibroVenta(fact) = True Then
            queryLibroVenta = " update libroventa  set iva= iva + " + fact.Iva + ", neto = neto + " + fact.Neto _
            + ",total = total + " + fact.Total + " where mes = '" + mes + _
            "' and anio = '" + año + "'"
            BD.EjecutarSinRetorno(queryLibroVenta)
        Else
            queryLibroVenta = " insert into libroventa (mes,anio,iva,neto,total) values('" + mes + "','" + _
            año + "'," + fact.Iva + "," + _
            fact.Neto + "," + fact.Total + ")"

            BD.EjecutarSinRetorno(queryLibroVenta)

        End If

        idLibroVenta = Me.obtieneIDLibroVenta(fact)

        queryfactura = "insert into factura (vendedor, ordencompra,nguiadespacho,condicionventa, neto,total,iva,fecha,fkrutcliente,fklibroventa) values ('" _
        + fact.Vendedor + "','" + fact.OrdenDeCompra + "','" + fact.NguiaDespacho + "','" + fact.CondicionVenta _
         + "','" + fact.Neto + "','" + fact.Total + "','" + fact.Iva + "','" + fact.Fecha + "','" + fact.Cliente + "'," + idLibroVenta + ")"

        BD.EjecutarSinRetorno(queryfactura)

        Dim i As Integer
        Dim identificadorFact As String = Me.ObtenerIDFactura

        'metodo para ingresar items de una factura
        For i = 0 To array.Count - 1

            queryItem = "insert into itemfactura (coditem,nombre,descripcion,preciounitario,cantidad,fkfactura) " _
            + "values('" + DirectCast(array.Item(i), item).Codigo + "','" _
            + DirectCast(array.Item(i), item).Item + "','" + DirectCast(array.Item(i), item).Detalle + _
            "','" + DirectCast(array.Item(i), item).PrecioUnitario + "','" + DirectCast(array.Item(i), item).Cantidad + _
            "','" + identificadorFact + "')"

            BD.EjecutarSinRetorno(queryItem)

        Next


        'CREACION DEL XML

        Dim nuevoXml As New ClassXML

        nuevoXml.creaXml("96.730.160-4", "96.730.160-4", fact.Cliente, fact.Fecha, "MegaNorte S.A", "PRESTACIÓN DE SERVICIOS DE RECURSOS" + _
        "TÉCNICOS EN MATERIAS INHERENTES A SEGURIDAD PRIVADA. IMPORTACIÓN, COMERCIALIZACIÓN Y SERVICIO TÉCNICO DE EQUIPOS DE COMUNICACIONES Y" + _
        "RADIODIFUSIÓN ", "GUARDIA VIEJA 181, OFICINA 804", fact.Persona.razonsocial, fact.Persona.giro, fact.Persona.direccion, _
        fact.Persona.comuna, fact.Persona.Ciudad, fact.Total, fact.Neto, fact.Iva, array)


        'AQUI
        Dim numFact As String = Me.ObtenerIDFactura()
        XmlByte = Me.ConvertXmlToByte(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/DTE" + numFact + ".xml")
        BD.EjecutarBinarioSinRetorno(XmlByte)



        'Me.enviarMailSii(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/DTE" + numFact + ".xml")
        'prueba
        'Me.ConvertByteToXml(XmlByte)

    End Sub

    Public Sub ingresarItemLibroVentas(ByVal itl As libroCompraData)

        Dim query As String

        query = "insert into librocompra (rut,ndocumento,proveedor,dia,mes,anio,iva,neto,total) values ('" + itl.Rut + _
        "','" + itl.Ndocmento + "','" + itl.Proveedor + "','" + itl.Dia + "','" + itl.Mes + "','" + itl.Anio + "'," + itl.Iva + "," _
        + itl.Neto + "," + itl.Total + ")"

        BD.EjecutarSinRetorno(query)
    End Sub

    Public Function ObtenerItemFactura(ByVal id As String) As ArrayList

        Dim array As New ArrayList
        Dim query As String
        query = "select * from itemfactura where fkfactura=" + id + ""
        array = BD.ObtenerItemFactura(query)
        Return array
    End Function

    Public Function ObtenerItemNC(ByVal id As String) As ArrayList

        Dim array As New ArrayList
        Dim query As String
        query = "select * from itemnc where fkidnc=" + id + ""
        array = BD.ObtenerItemFactura(query)
        Return array
    End Function

    Public Function ObtenerLcMes(ByVal mes As String, ByVal anio As String) As ArrayList

        Dim array As New ArrayList
        Dim query As String
        query = "select * from librocompra where mes='" + mes + "' and anio ='" + anio + "'"
        array = BD.ObtenerLcMes(query)
        Return array

    End Function


    Public Function ObtenerLvMes(ByVal mes As String, ByVal anio As String) As ArrayList

        Dim array As New ArrayList
        Dim query As String
        query = "select * from libroventa where mes='" + mes + "' and anio ='" + anio + "'"
        array = BD.ObtenerLvMes(query)
        Return array

    End Function


    Public Function ObtenerFacturaLV(ByVal id As String) As ArrayList
        Dim array As New ArrayList
        Dim query As String
        query = "select * from factura where fklibroventa=" + id + ""
        array = BD.Obtenerfactura(query)
        Return array
    End Function

    Public Function ObtenerNotaCreditoMesAnio(ByVal mes As String, ByVal anio As String) As ArrayList
        Dim array As New ArrayList
        Dim query As String
        query = "select * from notacredito where mes='" + mes + "' and anio='" + anio + "'"
        array = BD.ObtenerNotaCredito(query)
        Return array
    End Function

    Public Function ObtenerNotaCredito(ByVal id As String) As ArrayList
        Dim array As New ArrayList
        Dim query As String
        query = "select * from notacredito where id='" + id + "'"
        array = BD.ObtenerNotaCredito(query)
        Return array
    End Function

    Public Function ConvertXmlToByte(ByVal Path As String) As Byte()
        Dim sPath As String
        sPath = Path
        Dim Ruta As New FileStream(sPath, FileMode.Open, FileAccess.Read)
        Dim Binario(CInt(Ruta.Length)) As Byte
        Ruta.Read(Binario, 0, CInt(Ruta.Length))
        Ruta.Close()
        Return Binario
    End Function

    Public Function ConvertByteToXml(ByVal Bin As Byte()) As Boolean

        Dim oFileStream As FileStream
        Dim pathTemporal As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/xmlBinario.xml"

        If File.Exists(pathTemporal) Then File.Delete(pathTemporal)
        oFileStream = New FileStream(pathTemporal, FileMode.CreateNew)
        oFileStream.Write(Bin, 0, Bin.Length)
        oFileStream.Close()
        If File.Exists(pathTemporal) Then File.Delete(pathTemporal)
    End Function


    Private Sub enviarMailSii(ByVal pathAdjunto As String)
        Dim mailMsg As New System.Net.Mail.MailMessage()

        Dim smtp As New System.Net.Mail.SmtpClient

        Dim Anexos As New ArrayList()

        smtp.Host = "smtp.gmail.com" 'servidor de correo

        smtp.Credentials = New System.Net.NetworkCredential("nombreCuenta", "Password")

        Dim mailsend As String = "nombrecuenta" 'direccion de correo a la que se le enviará el mail
        'Añadimos archivos al Array
        Anexos.Add(pathAdjunto)
        'Anexos.Add("C:\archivo2.doc")
        With (mailMsg)
            .From = New System.Net.Mail.MailAddress(mailsend)
            .To.Add(mailsend)
            .CC.Add("ibarra.alexis@gmail.com")
            .Subject = "ejemplo de envio"
            .Body = "ejemplo de envio de correo con sistema factura"
            .IsBodyHtml = False
            .Priority = System.Net.Mail.MailPriority.Normal
            If (Anexos.Count > 0) Then
                For i As Integer = 0 To Anexos.Count - 1
                    If (System.IO.File.Exists(Anexos(i))) Then
                        .Attachments.Add(New System.Net.Mail.Attachment(Anexos(i))) 'adjuntamos los archivos()
                    Else
                        MsgBox("El archivo " + Anexos(i) + " No existe")
                        Exit Sub
                    End If
                Next
            End If
        End With
        Try
            smtp.Send(mailMsg)
            MsgBox("Mensaje enviado satisfactoriamente")
        Catch ex As Exception
            MsgBox("ERROR: " & ex.Message)
        End Try
    End Sub


    'Metodos para ingresar item de Nota credito

    Public Sub ingresarNotaCredito(ByVal nc As NotaDeCredito, ByVal arrayItem As ArrayList)
        Dim i As Integer
        Dim queryNotaCredito As String
        Dim queryItem As String
        Dim queryLibroVenta As String
        Dim numNC As Integer = 0
        Dim dia As String = nc.Fecha.Chars(0) + nc.Fecha.Chars(1)
        Dim año As String = nc.Fecha.Chars(6) + nc.Fecha.Chars(7) + nc.Fecha.Chars(8) + nc.Fecha.Chars(9)
        Dim mes As String = nc.Fecha.Chars(3) + nc.Fecha.Chars(4)



        queryNotaCredito = "insert into notacredito (fkfactura,dia,mes,anio,iva,neto,total)values('" + nc.Factura + "','" + dia + "','" + mes + _
                           "','" + año + "','" + nc.Iva + "','" + nc.Neto + "','" + nc.Total + "')"
        BD.EjecutarSinRetorno(queryNotaCredito)

        'ingresar items de una factura
        For i = 0 To arrayItem.Count - 1
            queryItem = "insert into itemnc (coditem,nombre,descripcion,preciounitario,cantidad,fkidnc) " _
            + "values('" + CStr(DirectCast(arrayItem.Item(i), item).Codigo) + "','" _
            + CStr(DirectCast(arrayItem.Item(i), item).Item) + "','" + CStr(DirectCast(arrayItem.Item(i), item).Detalle) + _
            "'," + CStr(DirectCast(arrayItem.Item(i), item).PrecioUnitario) + "," + CStr(DirectCast(arrayItem.Item(i), item).Cantidad) + _
            "," + nc.Id + ")"
            BD.EjecutarSinRetorno(queryItem)
        Next

        If Me.PreguntaLibroVentaNotaCredito(nc) = True Then
            queryLibroVenta = " update libroventa  set iva= iva - " + nc.Iva + ", neto = neto - " + nc.Neto _
            + ",total = total - " + nc.Total + " where mes = '" + mes + _
            "' and anio = '" + año + "'"
            BD.EjecutarSinRetorno(queryLibroVenta)
        Else
            Try
                Dim nuevoIva As String = CDbl(nc.Iva) * -1
                Dim nuevoNeto As String = CDbl(nc.Neto) * -1
                Dim nuevoTotal As String = CDbl(nc.Total) * -1

                queryLibroVenta = " insert into libroventa (mes,anio,iva,neto,total) values('" + mes + "','" + _
                año + "'," + nuevoIva + "," + _
                nuevoNeto + "," + nuevoTotal + ")"
                BD.EjecutarSinRetorno(queryLibroVenta)

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End If


    End Sub

    Public Function ObtenerIDNotaCredito() As String
        Dim query As String
        Dim array As New Hashtable
        query = "select max(id) maxima from notacredito"
        array = BD.EjecutarConRetorno(query)
        If BD.obtenerid(query) <> "" Then
            Return BD.obtenerid(query)
        End If
        Return ""
    End Function

   
    Public Function PreguntaLibroVentaNotaCredito(ByVal nc As NotaDeCredito) As Boolean

        Dim query As String
        Dim mes As String = nc.Fecha.Chars(3) + nc.Fecha.Chars(4)
        Dim año As String = nc.Fecha.Chars(6) + nc.Fecha.Chars(7) + nc.Fecha.Chars(8) + nc.Fecha.Chars(9)

        query = "select id from libroventa where mes ='" + mes _
         + "'" + "and anio='" + año + "'"

        If BD.EjecutarConReturn(query) = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function obtieneValorLibroVenta(ByVal mes As String, ByVal año As String) As ArrayList
        Dim query As String
        query = "select iva,neto,total from libroventa where mes ='" + mes _
                + "'" + "and anio='" + año + "'"
        Return BD.obtieneValorLibroVentaBD(query)

    End Function
End Class
