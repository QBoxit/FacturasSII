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


    Public Sub AutentificacionAutomatica()

        Dim newSeed As New Seed.CrSeedService
        Dim newToken As New Token.GetTokenFromSeedService
        Dim Semilla As String
        Dim Token As String
        Dim clsXML As New ClassXML
        Dim StrByte As String
        Dim StrHash As String
        Dim StrByteFinal As String
        Dim SeedSign As String
        Dim ResultadoSII As String
        Dim PathSeed As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Seed.xml"
        Dim PathToken As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml"
        Dim PathTokenAux As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/TokenAux.xml"
        Try
            ConvertStringtoXml(newSeed.getSeed(), "Seed")
            Semilla = obtieneSeed(PathSeed)
            clsXML.CreaXmlToken(Semilla)

            SignXML(PathToken)
            SeedSign = ConvertXmlToString(PathToken)
            ResultadoSII = newToken.getToken(SeedSign)


            '------METODOS PRUEBA-------
            'Dim rsa_Key As New RSACryptoServiceProvider(2048)
            'If func_FirmarArchivoXml(PathToken, PathTokenAux, rsa_Key) Then
            '    IO.File.Delete(PathToken)
            '    Rename(PathTokenAux, PathToken)
            'End If


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

    Private Function obtieneSeed(ByVal path As String) As String
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
                    If nombreNodo = "SEMILLA" Then
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

            cert = BuscarCertificado()

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

            Dim keyInfo As New KeyInfo
            keyInfo.AddClause(New KeyInfoX509Data(cert))
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

    Public Function BuscarCertificado() As X509Certificates.X509Certificate2

        Dim objStore As New X509Store(StoreName.My)
        Dim certificado As New X509Certificates.X509Certificate2

        objStore.Open(OpenFlags.ReadOnly)

        For Each objCert As X509Certificate2 In objStore.Certificates
            certificado = objCert
        Next
        objStore.Close()

        Return certificado

    End Function

    Private Function func_FirmarArchivoXml(ByVal strParamFileName As String, ByVal strParamSignedFileName As String, ByVal Key As RSA) As Boolean
        Dim doc As New XmlDocument()

        Try
            func_FirmarArchivoXml = True

            'Format the document to ignore white spaces.
            doc.PreserveWhitespace = False
            doc.Load(strParamFileName)

            'Create a SignedXml object.  
            Dim signedXml As New SignedXml(doc)


            'Create a reference to be signed.
            Dim reference As New Reference()
            reference.Uri = ""

            'Add an enveloped transformation to the reference.
            Dim env As New XmlDsigEnvelopedSignatureTransform()
            reference.AddTransform(env)

            'Add the reference to the SignedXml object.
            signedXml.AddReference(reference)

            'Add an RSAKeyValue KeyInfo (optional; helps recipient find key to validate).
            Dim keyInfo As New KeyInfo()
            Dim Certificado As X509Certificate2

            Certificado = Me.BuscarCertificado()

            ' Add the key to the SignedXml document. 
            signedXml.SigningKey = Key
            signedXml.SigningKey = Certificado.PrivateKey

            Dim kdata As New KeyInfoX509Data(Certificado)

            keyInfo.AddClause(kdata)

            signedXml.KeyInfo = keyInfo

            ' Compute the signature.
            signedXml.ComputeSignature()

            Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()

            ' Append the element to the XML document.
            doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, True))

            If TypeOf doc.FirstChild Is XmlDeclaration Then
                doc.RemoveChild(doc.FirstChild)
            End If

            'Save the signed XML document to a file specified
            'using the passed string.
            Dim xmltw As New XmlTextWriter(strParamSignedFileName, New UTF8Encoding(False))
            doc.WriteTo(xmltw)
            xmltw.Close()
            Threading.Thread.Sleep(10)
            doc.Save(strParamFileName)
            'doc.RemoveAll()
            env = Nothing
            'doc = Nothing

            xmltw = Nothing
            keyInfo = Nothing
            reference = Nothing
            signedXml = Nothing
            xmlDigitalSignature = Nothing

        Catch err As Exception
            MsgBox(err.Message, MsgBoxStyle.Critical & Chr(13) & "Archivo No firmado digitalmente", "Firma Digital")
        Finally
            doc = Nothing
        End Try



    End Function

End Class
