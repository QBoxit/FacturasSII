Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Diagnostics
Imports System.Xml
Imports System.Security.Cryptography.Xml


Public Class FirmadorXml

    ''' <summary>
    ''' Indica si el certificado ya ha sido solicitado al usuario.
    ''' </summary>
    Private Shared miCertificadoSolicitado As Boolean
    ''' <summary>
    ''' Certificado que se utiliza para firmar digitalmente, si es null despues de haberlo
    ''' solicitado al usuario se utiliza una llave creada en el almacen de claves de Windows.
    ''' </summary>
    Private Shared miCertificado As X509Certificate2
    ''' <summary>
    ''' Constructor.
    ''' </summary>
    Public Function FirmadorXml()

    End Function

    ''' <summary>
    ''' Firma un documento XML si se selecciona un certificado.
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    Public Sub Firmar(ByVal path As String)

        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(path)
        GarantizarCertificadoDeUsuario()

        Dim key As AsymmetricAlgorithm = miCertificado.PrivateKey
        Dim signedXml As New SignedXml(xmlDoc)
        signedXml.SigningKey = key
        ' Create a reference to be signed.
        Dim reference As New Reference()
        reference.Uri = ""
        Dim env As New XmlDsigEnvelopedSignatureTransform()
        reference.AddTransform(env)
        signedXml.AddReference(reference)
        signedXml.ComputeSignature()
        Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()
        xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
        xmlDoc.Save(path)

    End Sub

    ''' <summary>
    ''' Verifica la firma digital de un documento XML.
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    ''' <returns></returns>
    Public Function VerificarFirma(ByVal xmlDoc As XmlDocument) As Boolean
        Dim nodeList As XmlNodeList = xmlDoc.GetElementsByTagName("Signature")
        If nodeList.Count = 0 Then
            'el documento no está firmado, se considera valido
            Return True

        Else
            'si tiene una firma debe ser de un certificado
            Return VerificarFirmaConCertificado(xmlDoc)

        End If

    End Function

    ''' <summary>
    ''' Verifica la firma digital de un documento XML utilizando un certificado de usuario.
    ''' </summary>
    ''' <param name="xmlDoc"></param>
    ''' <returns></returns>
    Private Function VerificarFirmaConCertificado(ByVal xmlDoc As XmlDocument) As Boolean
        Dim certificado As X509Certificate2 = SeleccionarCertificado()
        If certificado IsNot Nothing Then
            Dim nodeList As XmlNodeList = xmlDoc.GetElementsByTagName("Signature")
            Dim signedXml As New SignedXml(xmlDoc)
            signedXml.LoadXml(CType(nodeList(0), XmlElement))
            Return signedXml.CheckSignature(certificado, True)

        Else
            Return False

        End If

    End Function

    ''' <summary>
    ''' Solicita el certificado al usuario una sola vez por sesión.
    ''' </summary>
    Public Shared Sub GarantizarCertificadoDeUsuario()
        If Not miCertificadoSolicitado Then
            miCertificado = SeleccionarCertificado()
            miCertificadoSolicitado = True

        End If

    End Sub

    ''' <summary>
    ''' Selecciona un certificado de usuario.
    ''' </summary>
    ''' <returns></returns>
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

    'Public Shared Function BuscarCertificado() As X509Certificates.X509Certificate2

    '    Dim objStore As New X509Store(StoreName.My)
    '    Dim certificado As New X509Certificates.X509Certificate2

    '    objStore.Open(OpenFlags.ReadOnly)

    '    For Each objCert As X509Certificate2 In objStore.Certificates
    '        certificado = objCert
    '    Next
    '    objStore.Close()

    '    If certificado.HasPrivateKey Then
    '        Return certificado
    '    Else
    '        MsgBox(certificado.SubjectName.Name)
    '    End If


    'End Function




End Class
