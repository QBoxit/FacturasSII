
Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography
Imports System.Text

Public Class ControladorLogica
    Dim CP As New controladorPersistencia

    Public Sub New()

    End Sub

    Public Function formato(ByVal rut As String) As String
        Dim largo As Integer
        Dim i As Integer
        Dim cad As String = ""
        Dim res As String = ""
        Dim j As Integer = 0

        cad = rut.Trim.PadLeft(9, "0")
        largo = Len(cad)

        For i = 1 To largo
            If i Mod 3 = 0 Then
                If j = 2 Then
                    res &= "-"
                    j = j + 1
                Else
                    res &= "."
                    j = j + 1
                End If
            End If

            res &= Mid(cad, i, 1)
        Next
        Return res
    End Function

    Public Function validarRut(ByVal rut_Verificar As String) As Boolean

        Dim rutLimpio As String
        Dim digitoVerificador As String
        Dim suma As Integer
        Dim contador As Integer = 2
        Dim valida As Boolean = True
        Dim retorno As Boolean = False

        rutLimpio = rut_Verificar.Replace(".", "")
        rutLimpio = rutLimpio.Replace("-", "")
        rutLimpio = rutLimpio.Replace(" ", "")
        rutLimpio = rutLimpio.Substring(0, rutLimpio.Length - 1)
        digitoVerificador = rut_Verificar.Substring(rut_Verificar.Length - 1, 1)

        Dim i As Integer

        For i = rutLimpio.Length - 1 To 0 Step -1

            If contador > 7 Then
                contador = 2
            End If

            Try
                suma += Integer.Parse(rutLimpio(i).ToString()) * contador
                contador += 1
            Catch ex As Exception
                valida = False
            End Try

        Next

        If valida Then
            Dim dv As Integer = 11 - (suma Mod 11)
            Dim DigVer As String = dv.ToString()

            If DigVer = "10" Then
                DigVer = "K"
            End If

            If DigVer = "11" Then
                DigVer = "0"
            End If

            If DigVer = digitoVerificador.ToUpper Then
                retorno = True

            Else
                retorno = False
            End If
        End If
        Return retorno
    End Function

    Public Function convierteMes(ByVal texto As String) As String
        Dim mes As String = ""
        If (texto = "01") Then
            mes = "Enero"
        End If
        If (texto = "02") Then
            mes = "Febrero"
        End If
        If (texto = "03") Then
            mes = "Marzo"
        End If
        If (texto = "04") Then
            mes = "Abril"
        End If
        If (texto = "05") Then
            mes = "Mayo"
        End If
        If (texto = "06") Then
            mes = "Junio"
        End If
        If (texto = "07") Then
            mes = "Julio"
        End If
        If (texto = "08") Then
            mes = "Agosto"
        End If
        If (texto = "09") Then
            mes = "Septiembre"
        End If
        If (texto = "10") Then
            mes = "Octubre"
        End If
        If (texto = "11") Then
            mes = "Noviembre"
        End If
        If (texto = "12") Then
            mes = "Diciembre"
        End If
        Return mes

    End Function

    Public Function Letras(ByVal numero As String) As String
        '********Declara variables de tipo cadena************
        Dim palabras As String = ""
        Dim entero As String = ""
        Dim dec As String = ""
        Dim flag As String = ""

        '********Declara variables de tipo entero***********
        Dim num, x, y As Integer

        flag = "N"

        '**********Número Negativo***********
        If Mid(numero, 1, 1) = "-" Then
            numero = Mid(numero, 2, numero.ToString.Length - 1).ToString
            palabras = "menos "
        End If

        '**********Si tiene ceros a la izquierda*************
        For x = 1 To numero.ToString.Length
            If Mid(numero, 1, 1) = "0" Then
                numero = Trim(Mid(numero, 2, numero.ToString.Length).ToString)
                If Trim(numero.ToString.Length) = 0 Then palabras = ""
            Else
                Exit For
            End If
        Next

        '*********Dividir parte entera y decimal************
        For y = 1 To Len(numero)
            If Mid(numero, y, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, y, 1)
                Else
                    dec = dec + Mid(numero, y, 1)
                End If
            End If
        Next y

        If Len(dec) = 1 Then dec = dec & "0"

        '**********proceso de conversión***********
        flag = "N"

        If Val(numero) <= 999999999 Then
            For y = Len(entero) To 1 Step -1
                num = Len(entero) - (y - 1)
                Select Case y
                    Case 3, 6, 9
                        '**********Asigna las palabras para las centenas***********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" And Mid(entero, num + 2, 1) = "0" Then
                                    palabras = palabras & "cien "
                                Else
                                    palabras = palabras & "ciento "
                                End If
                            Case "2"
                                palabras = palabras & "doscientos "
                            Case "3"
                                palabras = palabras & "trescientos "
                            Case "4"
                                palabras = palabras & "cuatrocientos "
                            Case "5"
                                palabras = palabras & "quinientos "
                            Case "6"
                                palabras = palabras & "seiscientos "
                            Case "7"
                                palabras = palabras & "setecientos "
                            Case "8"
                                palabras = palabras & "ochocientos "
                            Case "9"
                                palabras = palabras & "novecientos "
                        End Select
                    Case 2, 5, 8
                        '*********Asigna las palabras para las decenas************
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    flag = "S"
                                    palabras = palabras & "diez "
                                End If
                                If Mid(entero, num + 1, 1) = "1" Then
                                    flag = "S"
                                    palabras = palabras & "once "
                                End If
                                If Mid(entero, num + 1, 1) = "2" Then
                                    flag = "S"
                                    palabras = palabras & "doce "
                                End If
                                If Mid(entero, num + 1, 1) = "3" Then
                                    flag = "S"
                                    palabras = palabras & "trece "
                                End If
                                If Mid(entero, num + 1, 1) = "4" Then
                                    flag = "S"
                                    palabras = palabras & "catorce "
                                End If
                                If Mid(entero, num + 1, 1) = "5" Then
                                    flag = "S"
                                    palabras = palabras & "quince "
                                End If
                                If Mid(entero, num + 1, 1) > "5" Then
                                    flag = "N"
                                    palabras = palabras & "dieci"
                                End If
                            Case "2"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "veinte "
                                    flag = "S"
                                Else
                                    palabras = palabras & "veinti"
                                    flag = "N"
                                End If
                            Case "3"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "treinta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "treinta y "
                                    flag = "N"
                                End If
                            Case "4"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cuarenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cuarenta y "
                                    flag = "N"
                                End If
                            Case "5"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cincuenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cincuenta y "
                                    flag = "N"
                                End If
                            Case "6"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "sesenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "sesenta y "
                                    flag = "N"
                                End If
                            Case "7"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "setenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "setenta y "
                                    flag = "N"
                                End If
                            Case "8"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "ochenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "ochenta y "
                                    flag = "N"
                                End If
                            Case "9"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "noventa "
                                    flag = "S"
                                Else
                                    palabras = palabras & "noventa y "
                                    flag = "N"
                                End If
                        End Select
                    Case 1, 4, 7
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If flag = "N" Then
                                    If y = 1 Then
                                        palabras = palabras & "uno "
                                    Else
                                        palabras = palabras & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then palabras = palabras & "dos "
                            Case "3"
                                If flag = "N" Then palabras = palabras & "tres "
                            Case "4"
                                If flag = "N" Then palabras = palabras & "cuatro "
                            Case "5"
                                If flag = "N" Then palabras = palabras & "cinco "
                            Case "6"
                                If flag = "N" Then palabras = palabras & "seis "
                            Case "7"
                                If flag = "N" Then palabras = palabras & "siete "
                            Case "8"
                                If flag = "N" Then palabras = palabras & "ocho "
                            Case "9"
                                If flag = "N" Then palabras = palabras & "nueve "
                        End Select
                End Select

                '***********Asigna la palabra mil***************
                If y = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or _
                    (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And _
                    Len(entero) <= 6) Then palabras = palabras & "mil "
                End If

                '**********Asigna la palabra millón*************
                If y = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        palabras = palabras & "millón "
                    Else
                        palabras = palabras & "millones "
                    End If
                End If
            Next y

            '**********Une la parte entera y la parte decimal*************
            If dec <> "" Then
                Letras = palabras & "con " & dec
            Else
                Letras = palabras
            End If
        Else
            Letras = ""
        End If
    End Function

    Public Sub ingresarPersona(ByVal p As persona)
        CP.ingresarPersona(p)
    End Sub

    Public Sub ActualizarPersona(ByVal p As persona)
        CP.ActualizarPersona(p)
    End Sub

    Public Function retornaCliente(ByVal rutAux As String) As ArrayList
        Dim Array As New ArrayList
        Array = CP.retornaCliente(rutAux)
        Return Array
    End Function

    Public Function BuscarFactura(ByVal numeroFactura As String) As ArrayList

        Dim array As New ArrayList
        array = CP.BuscarFactura(numeroFactura)
        Return array

    End Function

    Public Sub IngresarFactura(ByVal fact As factura, ByVal array As ArrayList)
        CP.IngresarFactura(fact, array)
    End Sub

    Public Sub ingresarNotaCredito(ByVal nc As NotaDeCredito, ByVal arrayItem As ArrayList)
        CP.ingresarNotaCredito(nc, arrayItem)
    End Sub


    Public Function ObtenerIDNotaCredito() As String
        Return CP.ObtenerIDNotaCredito()
    End Function

    Public Sub ingresarItemLibroVentas(ByVal ItemLibroVenta As libroCompraData)
        CP.ingresarItemLibroVentas(ItemLibroVenta)
    End Sub

    Public Function ObtenerItemFactura(ByVal numeroFactura As String) As ArrayList
        Dim array As New ArrayList
        array = CP.ObtenerItemFactura(numeroFactura)
        Return array
    End Function
    Public Function ObtenerItemNC(ByVal numeroNC As String) As ArrayList
        Dim array As New ArrayList
        array = CP.ObtenerItemNC(numeroNC)
        Return array
    End Function


    Public Function ObtenerLcMes(ByVal mes As String, ByVal anio As String) As ArrayList

        Dim array As New ArrayList
        array = CP.ObtenerLcMes(mes, anio)
        Return array

    End Function

    Public Function ObtenerLvMes(ByVal mes As String, ByVal anio As String) As ArrayList

        Dim array As New ArrayList
        array = CP.ObtenerLvMes(mes, anio)
        Return array

    End Function

    Public Function ObtenerFacturaLV(ByVal id As String) As ArrayList
        Dim array As New ArrayList
        array = CP.ObtenerFacturaLV(id)
        Return array
    End Function

    Public Function ObtenerNotaCreditoMesAnio(ByVal mes As String, ByVal anio As String) As ArrayList
        Dim array As New ArrayList
        array = CP.ObtenerNotaCreditoMesAnio(mes, anio)
        Return array
    End Function

    Public Function ObtenerNotaCredito(ByVal id As String) As ArrayList
        Dim array As New ArrayList
        array = CP.ObtenerNotaCredito(id)
        Return array
    End Function
    Public Function obtieneIDMax() As Integer
        Dim IdFactura As Integer
        IdFactura = CInt(CP.ObtenerIDFactura())
        Return IdFactura
    End Function



    'METODOS PARA AUTENTIFICACION AUTOMATICA SII
    Public Sub AutentificacionAutomatica()
        Dim newSeed As New Seed.CrSeedService
        Dim newToken As New Token.GetTokenFromSeedService
        Dim Semilla As String
        Dim Token As String
        Dim clsXML As New ClassXML
        Dim StrByte As String
        Dim StrHash As String

        Try
            ConvertStringtoXml(newSeed.getSeed(), "Seed")
            Semilla = obtieneSeed(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Seed.xml")
            clsXML.CreaXmlToken(Semilla)
            StrByte = CanonicalizaXml(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml")
            StrHash = CalculaHash(StrByte)

            'Dim Canonilaliza As New XML_C14N(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml")
            'Canonilaliza.XML_Canonalize()
            'Dim Firma As New xmlSignature(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml")
            'Firma.SignXML()
            'Token = newToken.getToken("Aca va el xml firmado")

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Function ConvertStringtoXml(ByVal stringXML As String, ByVal name As String) As String
        Dim DatosXml As String = stringXML
        Dim pathFile As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/" + name + ".xml"
        Dim xmlDoc As New XmlDocument
        DatosXml = HttpUtility.UrlDecode(DatosXml)
        xmlDoc.LoadXml(DatosXml + Environment.NewLine)
        xmlDoc.Save(pathFile)
        Return DatosXml
    End Function

    Private Function obtieneSeed(ByVal path As String) As String
        Dim reader As New XmlTextReader(path)
        Dim semilla As String = ""
        reader.WhitespaceHandling = WhitespaceHandling.None
        Do While (reader.Read())
            Dim nombreNodo As String = ""
            Select Case reader.NodeType
                Case XmlNodeType.Element 'Mostrar comienzo del elemento.
                    nombreNodo = reader.Name
                    If reader.HasAttributes Then 'If attributes exist
                        While reader.MoveToNextAttribute()
                            'Mostrar nombre y valor del atributo.
                            nombreNodo = reader.Name
                        End While
                    End If
                Case XmlNodeType.Text 'Mostrar el texto de cada elemento.
                    If nombreNodo = "SEMILLA" Then
                        semilla = reader.Value
                    End If
            End Select
        Loop

        Return semilla
    End Function

    Private Function CanonicalizaXml(ByVal s1 As String) As String
        Dim StrReturn As String = ""
        Dim x1 As New XmlDocument()
        x1.Load(s1)
        Dim t As New XmlDsigC14NTransform()
        t.LoadInput(x1)
        Dim s As Stream = DirectCast(t.GetOutput(GetType(Stream)), Stream)
        s.Position = 0
        Dim nuevoXml As String = New StreamReader(s).ReadToEnd()
        StrReturn = ConvertStringtoXml(nuevoXml, "C14N")
        Return StrReturn
    End Function

    Private Function CalculaHash(ByVal StrConvertHash) As String
        Dim StrOriginal As String = StrConvertHash
        Dim byteOriginal As Byte() = Encoding.ASCII.GetBytes(StrOriginal)
        Dim objAlgoritmo As SHA1 = SHA1.Create()
        Dim bytHash As Byte() = objAlgoritmo.ComputeHash(byteOriginal)
        Dim StrHash As String = Convert.ToBase64String(bytHash)
        Return StrHash
    End Function





End Class
