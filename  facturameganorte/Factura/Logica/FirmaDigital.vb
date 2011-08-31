Imports System.Data
Imports System.IO
Imports System.Xml
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography
Imports System.Text
Imports System
Imports System.Security.Cryptography.X509Certificates
Imports System.Diagnostics





Public Class FirmaDigital
    Dim CL As New ControladorLogica
    Dim PathSeed As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Seed.xml"
    Dim PathToken As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml"
    Dim PathTokenAux As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/TokenAux.xml"
    Dim PathResultado As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/ResultadoObtenido.xml"

    Public Function AutentificacionAutomatica() As String

        Dim newSeed As New Seed.CrSeedService
        Dim newToken As New Token.GetTokenFromSeedService
        Dim Semilla As String
        Dim clsXML As New ClassXML
        Dim SeedSign As String
        Dim TokenObtenido As String
        Dim ResultadoSII As String

        Try
            CL.ConvertStringtoXml(newSeed.getSeed(), "Seed")
            Semilla = CL.obtieneLecturaXML(PathSeed, "SEMILLA")
            clsXML.CreaXmlToken(Semilla)

            'firma el Xml
            SignXML(PathToken)

            SeedSign = CL.ConvertXmlToString(PathToken)
            ResultadoSII = newToken.getToken(SeedSign)
            CL.ConvertStringtoXml(ResultadoSII, "ResultadoObtenido")

            Dim estado = CL.obtieneLecturaXML(PathResultado, "ESTADO")

            If estado = "00" Then
                TokenObtenido = CL.obtieneLecturaXML(PathResultado, "TOKEN")
                Return TokenObtenido
            Else
                MsgBox("Error con conexion SII, Error numero : " + estado)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Sub SignXML(ByVal DireccionXML As String)
        Dim document As New XmlDocument

        'Creamos el objeto de firma XML
        Dim oSignedXml As SignedXml
        Dim oReference As Reference
        Dim oEnv As XmlDsigEnvelopedSignatureTransform
        Dim oEnv1 As XmlDsigC14NTransform
        Dim xmlDigitalSignature As XmlElement
        Dim cert As New X509Certificates.X509Certificate2

        Try

            cert = SeleccionarCertificado()

            'Cargamos el documento XML
            document.PreserveWhitespace = True
            document.Load(DireccionXML)

            oSignedXml = New SignedXml(document)

            'Añadimos la clave a usar     
            oSignedXml.SigningKey = cert.PrivateKey

            'Creamos una nueva referencia para ser firmada
            oReference = New Reference
            oReference.Uri = ""

            'Añadimos una transformación de encapsulación o "ensobramiento" a la referencia
            oEnv = New XmlDsigEnvelopedSignatureTransform
            oReference.AddTransform(oEnv)

            oEnv1 = New XmlDsigC14NTransform()
            oReference.AddTransform(oEnv1)

            'Añadimos la referencia al objeto de firma
            oSignedXml.AddReference(oReference)

            Dim keyInfo As New KeyInfo()
            keyInfo.AddClause(New KeyInfoX509Data(cert))


            Dim rsaKeyValue As New RSAKeyValue(cert.PrivateKey)
            keyInfo.AddClause(rsaKeyValue)

            oSignedXml.KeyInfo = keyInfo

            'La calculamos
            oSignedXml.ComputeSignature()

            'Obtenemos la representación XML de la firma y la guardamos en un objeto XmlElement
            xmlDigitalSignature = oSignedXml.GetXml()

            'Y por último añadimos el elemento al documento XML y lo guardamos
            document.DocumentElement.AppendChild(document.ImportNode(xmlDigitalSignature, True))
            document.Save(DireccionXML)

        Catch ex As Exception
            MsgBox(ex.Source & ". " & ex.Message)
        End Try
    End Sub

    
    Public Function obtienePrivateKeyFactura() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FolioFactura.xml"
        Dim privateKey As String = CL.obtieneLecturaXML(path, "RSASK")
        Return privateKey
    End Function

    Public Function obtienePublicKeyFactura() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FolioFactura.xml"
        Dim publicKey As String = CL.obtieneLecturaXML(path, "RSAPUBK")
        Return publicKey
    End Function

    Public Function obtienePrivateKeyNotaCredito() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FoliosNotaCredito.xml"
        Dim privateKey As String = CL.obtieneLecturaXML(path, "RSASK")
        Return privateKey
    End Function

    Public Function obtienePublicKeyNotaCredito() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FoliosNotaCredito.xml"
        Dim publicKey As String = CL.obtieneLecturaXML(path, "RSAPUBK")
        Return publicKey
    End Function

    'Este metodo es el encargado de general el xmlFacturas Firmalo y enviarlo al SII
    Public Sub FirmaEnviaSiiFactura(ByVal rutEmisor As String, ByVal rutEnvia As String, ByVal rutReceptor As String, ByVal fechaEmision As String, _
        ByVal rznSocial As String, ByVal giroEmisor As String, ByVal DirOrigen As String, ByVal rznsocialReceptor As String, ByVal giroReceptor As String, _
        ByVal dirReceptor As String, ByVal comunaRecept As String, ByVal ciudadRecept As String, ByVal total As Double, ByVal montoNeto As Double, _
        ByVal tasaIva As Double, ByVal ArregloItem As ArrayList)

        Dim CP As New controladorPersistencia()
        Dim nameFile As String = "DTE" + CP.ObtenerIDFactura() + ".xml"
        Dim pathFactura As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/" + nameFile
        Dim Xml As New ClassXML

        Dim token As String = AutentificacionAutomatica()

        Xml.creaXml(rutEmisor, rutEnvia, rutReceptor, fechaEmision, rznSocial, giroEmisor, DirOrigen, rznsocialReceptor, giroReceptor, _
        dirReceptor, comunaRecept, ciudadRecept, total, montoNeto, tasaIva, ArregloItem, pathFactura)
        SignXML(pathFactura)

        'envio por upload
        Dim clienteSII As New GetSocket
        Dim Host As String = "maullin.sii.cl"
        Dim Port As Integer = 443
        Dim Data As String = CL.ConvertXmlToString(pathFactura)
        MsgBox(clienteSII.SocketSendReceive(Host, Port, Data, token, "07794036", "7", " 96730160", "4", pathFactura))

    End Sub

    'Este metodo es el encargado de general el xmlFacturas Firmalo y enviarlo al SII
    Public Sub FirmaEnviaSiiNotaCredito()

    End Sub

    'Private Sub CreaSocketSii()
    '    Dim clienteSII As New GetSocket
    '    Dim Host As String = "maullin.sii.cl"
    '    Dim Port As String = "443"
    '    Dim Data As String = Me.ConvertXmlToString(Me.PathToken)
    '    MsgBox(clienteSII.SocketSendReceive(Host, Port, Data))
    'End Sub

    Public Sub ArrojaDatosCertificados()
        AutentificacionAutomatica()
        'CreaSocketSii()
    End Sub

    Public Shared Function SeleccionarCertificado() As X509Certificate2
        'TODO: Sustituirse por una invocacion a una interfaz que se le inyecta a esta clase para
        'seleccionar certificados.
        'Cargar certificados del usuario que contienen llave privada
        Dim store As New X509Store(StoreLocation.LocalMachine)
        store.Open(OpenFlags.ReadOnly)
        Try
            Dim certSeleccionado As X509Certificate2 = Nothing
            For Each cert As X509Certificate2 In store.Certificates
                If cert.HasPrivateKey Then
                    'If certSeleccionado = Nothing Then
                    'tomaremos el primero de ellos para este ejemplo
                    certSeleccionado = cert
                    'End If
                End If

            Next
            Return certSeleccionado
        Finally
            store.Close()
        End Try

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

    'Public Sub SignXmlDocument(ByVal DireccionXML As String)

    '    Dim key As RSA = RSA.Create()
    '    Dim xmlDocument As New XmlDocument
    '    xmlDocument.PreserveWhitespace = True
    '    xmlDocument.Load(DireccionXML)

    '    Dim signedXml As New SignedXml(xmlDocument)
    '    signedXml.SigningKey = key
    '    Dim keyInfo As New KeyInfo()
    '    Dim rsaKeyValue As New RSAKeyValue(key)
    '    keyInfo.AddClause(rsaKeyValue)
    '    signedXml.KeyInfo = keyInfo

    '    Dim reference As New Reference()
    '    reference.Uri = String.Empty
    '    Dim envelopedSignatureTransform As New XmlDsigEnvelopedSignatureTransform()
    '    reference.AddTransform(envelopedSignatureTransform)
    '    signedXml.AddReference(reference)
    '    signedXml.ComputeSignature()

    '    Dim element As XmlElement = signedXml.GetXml()
    '    xmlDocument.DocumentElement.AppendChild(xmlDocument.ImportNode(element, True))
    '    xmlDocument.Save(DireccionXML)

    'End Sub

    'Public Function BuscarCertificado() As X509Certificates.X509Certificate2

    '    Dim objStore As New X509Store(StoreName.My)
    '    Dim certificado As New X509Certificates.X509Certificate2

    '    objStore.Open(OpenFlags.ReadOnly)

    '    For Each objCert As X509Certificate2 In objStore.Certificates
    '        certificado = objCert
    '    Next
    '    objStore.Close()

    '    Return certificado

    'End Function

    'Private Function CanonicalizaXml(ByVal s1 As String) As String
    '    Dim StrReturn As String = ""
    '    Dim x1 As New XmlDocument()
    '    x1.Load(s1)
    '    Dim t As New XmlDsigC14NTransform()
    '    t.LoadInput(x1)
    '    Dim s As Stream = DirectCast(t.GetOutput(GetType(Stream)), Stream)
    '    s.Position = 0
    '    Dim nuevoXml As String = New StreamReader(s).ReadToEnd()
    '    StrReturn = ConvertStringtoXml(nuevoXml, "C14N")
    '    Return StrReturn
    'End Function

    'Private Function CalculaHash(ByVal StrConvertHash) As String
    '    Dim StrOriginal As String = StrConvertHash
    '    Dim byteOriginal As Byte() = Encoding.ASCII.GetBytes(StrOriginal)
    '    Dim objAlgoritmo As SHA1 = SHA1.Create()
    '    Dim bytHash As Byte() = objAlgoritmo.ComputeHash(byteOriginal)
    '    Dim StrHash As String = Convert.ToBase64String(bytHash)
    '    Return StrHash
    'End Function

    'Public Function SignXML(ByVal _filename As String) As [Boolean]
    '    Dim rsa As New RSACryptoServiceProvider()
    '    Dim xmlDoc As New XmlDocument()
    '    xmlDoc.PreserveWhitespace = True
    '    Dim fname As [String] = _filename
    '    xmlDoc.Load(fname)
    '    Dim xmlSig As New SignedXml(xmlDoc)
    '    xmlSig.SigningKey = rsa
    '    Dim reference As New Reference()
    '    reference.Uri = ""
    '    Dim env As New XmlDsigC14NTransform(False)
    '    reference.AddTransform(env)
    '    xmlSig.AddReference(reference)
    '    xmlSig.ComputeSignature()
    '    Dim xmlDigitalSignature As XmlElement = xmlSig.GetXml()
    '    xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
    '    xmlDoc.Save(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/TokenFinal.xml")
    '    Return True
    'End Function

    

   
End Class
