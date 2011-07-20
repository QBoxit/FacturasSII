Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml

Class xmlSignature

    Public Sub New(ByVal filename As [String])
        _filename = filename
    End Sub

    Public Function SignXML() As [Boolean]
        Dim rsa As New RSACryptoServiceProvider()
        Dim xmlDoc As New XmlDocument()
        xmlDoc.PreserveWhitespace = True
        Dim fname As [String] = _filename
        '"C:\\SigTest.xml"; 
        xmlDoc.Load(fname)
        Dim xmlSig As New SignedXml(xmlDoc)
        xmlSig.SigningKey = rsa
        Dim reference As New Reference()
        reference.Uri = ""
        Dim env As New XmlDsigC14NTransform(False)
        reference.AddTransform(env)
        xmlSig.AddReference(reference)
        xmlSig.ComputeSignature()
        Dim xmlDigitalSignature As XmlElement = xmlSig.GetXml()
        xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, True))
        xmlDoc.Save(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed" & "/SignedXML.xml")
        Return True
    End Function

    Private _filename As [String]

End Class

