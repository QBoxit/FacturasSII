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


    Public Function AutentificacionAutomatica() As String

        Dim newSeed As New Seed.CrSeedService
        Dim newToken As New Token.GetTokenFromSeedService
        Dim Semilla As String
        Dim Token As String
        Dim clsXML As New ClassXML
        Dim StrByte As String
        Dim StrHash As String
        Dim StrByteFinal As String

        Dim SeedSign As String
        Dim TokenObtenido As String
        Dim ResultadoSII As String

        Dim PathSeed As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Seed.xml"
        Dim PathToken As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml"
        Dim PathTokenAux As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/TokenAux.xml"
        Dim PathResultado As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/ResultadoObtenido.xml"

        Try
            ConvertStringtoXml(newSeed.getSeed(), "Seed")
            Semilla = obtieneLecturaXML(PathSeed, "SEMILLA")
            clsXML.CreaXmlToken(Semilla)

            'firma el Xml
            SignXML(PathToken)

            SeedSign = ConvertXmlToString(PathToken)
            ResultadoSII = newToken.getToken(SeedSign)
            ConvertStringtoXml(ResultadoSII, "ResultadoObtenido")

            Dim estado = obtieneLecturaXML(PathResultado, "ESTADO")

            If estado = "00" Then
                TokenObtenido = obtieneLecturaXML(PathResultado, "TOKEN")
                Return TokenObtenido
            Else
                MsgBox("Error con conexion SII, Error numero : " + estado)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function


    Private Function ConvertStringtoXml(ByVal stringXML As String, ByVal name As String) As String
        Dim DatosXml As String = stringXML
        Dim pathFile As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/" + name + ".xml"
        Dim xmlDoc As New XmlDocument
        DatosXml = HttpUtility.UrlDecode(DatosXml)
        xmlDoc.LoadXml(DatosXml + Environment.NewLine)
        xmlDoc.Save(pathFile)
        Return DatosXml
    End Function

    Private Function ConvertXmlToString(ByVal path As String) As String

        'Load the xml file into XmlDocument object.
        Dim xmlDoc As New XmlDocument
        Try
            xmlDoc.Load(path)
        Catch e As XmlException
            MsgBox(e.Message)
        End Try
        'Now create StringWriter object to get data from xml document.
        Dim sw As New StringWriter
        Dim xw As New XmlTextWriter(sw)
        xmlDoc.WriteTo(xw)
        Return sw.ToString()
    End Function

    Public Function obtieneLecturaXML(ByVal path As String, ByVal NameItem As String) As String
        Dim reader As New XmlTextReader(path)
        Dim semilla As String = ""
        Dim nombreNodo As String = ""
        reader.WhitespaceHandling = WhitespaceHandling.None
        Do While (reader.Read())
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
                    If nombreNodo = NameItem Then
                        semilla = reader.Value
                    End If
            End Select
        Loop
        Return semilla
    End Function

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

    Public Function obtienePrivateKeyFactura() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FolioFactura.xml"
        Dim privateKey As String = Me.obtieneLecturaXML(path, "RSASK")
        Return privateKey
    End Function

    Public Function obtienePublicKeyFactura() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FolioFactura.xml"
        Dim publicKey As String = Me.obtieneLecturaXML(Path, "RSAPUBK")
        Return publicKey
    End Function

    Public Function obtienePrivateKeyNotaCredito() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FoliosNotaCredito.xml"
        Dim privateKey As String = Me.obtieneLecturaXML(Path, "RSASK")
        Return privateKey
    End Function

    Public Function obtienePublicKeyNotaCredito() As String
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FoliosNotaCredito.xml"
        Dim publicKey As String = Me.obtieneLecturaXML(Path, "RSAPUBK")
        Return publicKey
    End Function

    'Este metodo es el encargado de general el xmlFacturas Firmalo y enviarlo al SII
    Public Sub FirmaEnviaSiiFactura(ByVal rutEmisor As String, ByVal rutEnvia As String, ByVal rutReceptor As String, ByVal fechaEmision As String, _
        ByVal rznSocial As String, ByVal giroEmisor As String, ByVal DirOrigen As String, ByVal rznsocialReceptor As String, ByVal giroReceptor As String, _
        ByVal dirReceptor As String, ByVal comunaRecept As String, ByVal ciudadRecept As String, ByVal total As Double, ByVal montoNeto As Double, _
        ByVal tasaIva As Double, ByVal ArregloItem As ArrayList)

        Dim CP As New controladorPersistencia()
        Dim IdFactura As String = CP.ObtenerIDFactura()
        Dim path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/DTE" + IdFactura + ".xml"
        Dim Xml As New ClassXML

        Dim token As String = AutentificacionAutomatica()

        Xml.creaXml(rutEmisor, rutEnvia, rutReceptor, fechaEmision, rznSocial, giroEmisor, DirOrigen, rznsocialReceptor, giroReceptor, _
        dirReceptor, comunaRecept, ciudadRecept, total, montoNeto, tasaIva, ArregloItem, path)
        SignXML(path)

    End Sub

    'Este metodo es el encargado de general el xmlFacturas Firmalo y enviarlo al SII
    Public Sub FirmaEnviaSiiNotaCredito()

    End Sub

    Public Sub ArrojaDatosCertificados()
        AutentificacionAutomatica()
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
                    MsgBox(cert.SubjectName.Name)
                End If

            Next

            Return certSeleccionado

        Finally
            store.Close()

        End Try

    End Function

    

   
End Class
