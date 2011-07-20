Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography
Imports System.IO
Imports System.ComponentModel


Class XML_C14N
    Private _filename As [String]
    Private isCommented As [Boolean] = False
    Private xmlDoc As XmlDocument = Nothing

    Public Sub New(ByVal filename As [String])
        _filename = filename
        xmlDoc = New XmlDocument()
        xmlDoc.Load(_filename)
    End Sub

    'implement this spec http://www.w3.org/TR/2001/REC-xml-c14n-20010315 
    Public Function XML_Canonalize() As [String]
        'create c14n instance and load in xml file 
        Dim c14n As New XmlDsigC14NTransform(isCommented)
        c14n.LoadInput(xmlDoc)
        'get canonalised stream 
        Dim s1 As Stream = DirectCast(c14n.GetOutput(GetType(Stream)), Stream)
        Dim sha1 As SHA1 = New SHA1CryptoServiceProvider()
        Dim output As [Byte]() = sha1.ComputeHash(s1)
        Convert.ToBase64String(output)
        'create new xmldocument and save 
        Dim newFilename As [String] = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/C14N.xml"
        Dim xmldoc2 As New XmlDocument()
        xmldoc2.Load(s1)
        xmldoc2.Save(newFilename)
        Return newFilename
    End Function

    Public Sub set_isCommented(ByVal value As [Boolean])
        isCommented = value
    End Sub

    Public Function get_isCommented() As [Boolean]
        Return isCommented
    End Function

End Class
