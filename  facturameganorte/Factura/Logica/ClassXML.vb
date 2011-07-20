Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Collections


Public Class ClassXML

    Dim CP As New controladorPersistencia

    Public Sub creaXml(ByVal rutEmisor As String, ByVal rutEnvia As String, ByVal rutReceptor As String, ByVal fechaEmision As String, _
    ByVal rznSocial As String, ByVal giroEmisor As String, ByVal DirOrigen As String, ByVal rznsocialReceptor As String, ByVal giroReceptor As String, ByVal dirReceptor As String, _
    ByVal comunaRecept As String, ByVal ciudadRecept As String, ByVal total As Double, ByVal montoNeto As Double, ByVal tasaIva As Double, ByVal ArregloItem As ArrayList)


        Dim IdFactura As String = CP.ObtenerIDFactura()
        Dim counter As Integer = 0
        Dim doc As New XmlTextWriter(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/DTE" + IdFactura + ".xml", Encoding.UTF8)

        doc.WriteStartDocument()
        doc.WriteStartElement("EnvioDTE")
        doc.WriteStartAttribute("xmlns")
        doc.WriteValue("http://www.sii.cl/SiiDte")
        doc.WriteEndAttribute()

        doc.WriteStartAttribute("xmlns:xsi")
        doc.WriteValue("http://www.w3.org/2001/XMLSchema-instance")
        doc.WriteEndAttribute()

        doc.WriteStartAttribute("xsi:schemaLocation")
        doc.WriteValue("http://www.sii.cl/SiiDte EnvioDTE_v10.xsd")
        doc.WriteEndAttribute()

        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        'Fin Cabecera

        doc.WriteStartElement("SetDTE")
        doc.WriteStartAttribute("ID")
        doc.WriteValue("SetDoc")
        doc.WriteEndAttribute()

        doc.WriteStartElement("Caratula")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        doc.WriteStartElement("RutEmisor")
        doc.WriteValue(rutEmisor)
        doc.WriteEndElement()

        doc.WriteStartElement("RutEnvia")
        doc.WriteValue(rutEnvia)
        doc.WriteEndElement()

        doc.WriteStartElement("RutReceptor")
        doc.WriteValue(rutReceptor)
        doc.WriteEndElement()

        doc.WriteStartElement("FchResol")
        doc.WriteValue(fechaEmision)
        doc.WriteEndElement()

        doc.WriteStartElement("NroResol")
        doc.WriteValue("1")
        doc.WriteEndElement()

        doc.WriteStartElement("TmstFirmaEnv")
        doc.WriteValue("2003-10-13T09:33:22")
        doc.WriteEndElement()

        doc.WriteStartElement("SubTotDTE")
        doc.WriteStartElement("TpoDTE")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("NroDTE")
        doc.WriteValue("1")
        doc.WriteEndElement()

        doc.WriteEndElement()
        doc.WriteEndElement() ' fin caratula

        doc.WriteStartElement("DTE")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        doc.WriteStartElement("Documento")
        doc.WriteStartAttribute("ID")
        doc.WriteValue("F06T33")
        doc.WriteEndAttribute()

        doc.WriteStartElement("Encabezado")
        doc.WriteStartElement("IdDoc")
        doc.WriteStartElement("TipoDTE")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("Folio")
        doc.WriteValue("60")
        doc.WriteEndElement()

        doc.WriteStartElement("FchEmis")
        doc.WriteValue(fechaEmision)
        doc.WriteEndElement()

        doc.WriteEndElement() 'fin Id Doc

        doc.WriteStartElement("Emisor")
        doc.WriteStartElement("RUTEmisor")
        doc.WriteValue(rutEmisor)
        doc.WriteEndElement()

        doc.WriteStartElement("RznSoc")
        doc.WriteValue(rznSocial)
        doc.WriteEndElement()

        doc.WriteStartElement("GiroEmis")
        doc.WriteValue(giroEmisor)
        doc.WriteEndElement()

        doc.WriteStartElement("Acteco")
        doc.WriteValue("31341")
        doc.WriteEndElement()

        doc.WriteStartElement("CdgSIISucur")
        doc.WriteValue("1234")
        doc.WriteEndElement()

        doc.WriteStartElement("DirOrigen")
        doc.WriteValue(DirOrigen)
        doc.WriteEndElement()

        doc.WriteStartElement("CmnaOrigen")
        doc.WriteValue("Antofagasta")
        doc.WriteEndElement()

        doc.WriteStartElement("CiudadOrigen")
        doc.WriteValue("Antofagasta")
        doc.WriteEndElement()

        doc.WriteEndElement() 'fin de Emisor

        doc.WriteStartElement("Receptor")
        doc.WriteStartElement("RUTRecep")
        doc.WriteValue(rutReceptor)
        doc.WriteEndElement()

        doc.WriteStartElement("RznSocRecep")
        doc.WriteValue(rznsocialReceptor)
        doc.WriteEndElement()

        doc.WriteStartElement("GiroRecep")
        doc.WriteValue(giroReceptor)
        doc.WriteEndElement()

        doc.WriteStartElement("DirRecep")
        doc.WriteValue(dirReceptor)
        doc.WriteEndElement()

        doc.WriteStartElement("CmnaRecep")
        doc.WriteValue(comunaRecept)
        doc.WriteEndElement()

        doc.WriteStartElement("CiudadRecep")
        doc.WriteValue(ciudadRecept)
        doc.WriteEndElement()

        doc.WriteEndElement() 'fin de Receptor

        doc.WriteStartElement("Totales")
        doc.WriteStartElement("MntNeto")
        doc.WriteValue(montoNeto)
        doc.WriteEndElement()

        doc.WriteStartElement("TasaIVA")
        doc.WriteValue(tasaIva)
        doc.WriteEndElement()

        doc.WriteStartElement("MntTotal")
        doc.WriteValue(total)
        doc.WriteEndElement()

        doc.WriteEndElement() 'fin de Totales
        doc.WriteEndElement() 'fin de Encabezado

        Dim item As item

        For counter = 0 To ArregloItem.Count - 1

            item = DirectCast(ArregloItem.Item(counter), item)

            doc.WriteStartElement("Detalle")
            doc.WriteStartElement("NroLinDet")
            doc.WriteValue(counter + 1)
            doc.WriteEndElement()

            doc.WriteStartElement("CdgItem")
            doc.WriteStartElement("TpoCodigo")
            doc.WriteValue("INT1")
            doc.WriteEndElement()

            doc.WriteStartElement("VlrCodigo")
            doc.WriteValue("011")
            doc.WriteEndElement()
            doc.WriteEndElement() 'fin cdgItem

            doc.WriteStartElement("NmbItem")
            doc.WriteValue(item.Codigo)
            doc.WriteEndElement()

            doc.WriteStartElement("DscItem")
            doc.WriteEndElement()

            doc.WriteStartElement("QtyItem")
            doc.WriteValue(item.Cantidad)
            doc.WriteEndElement()

            doc.WriteStartElement("PrcItem")
            doc.WriteValue(item.PrecioUnitario)
            doc.WriteEndElement()

            doc.WriteStartElement("MontoItem")
            doc.WriteValue(CInt(item.PrecioUnitario) * CInt(item.Cantidad))
            doc.WriteEndElement()

            doc.WriteEndElement() 'fin Detalle
        Next
        doc.WriteStartElement("TED")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        doc.WriteStartElement("DD")
        doc.WriteStartElement("RE")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("TD")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("F")
        doc.WriteValue("60")
        doc.WriteEndElement()

        doc.WriteStartElement("FE")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("RR")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("RSR")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("MNT")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("IT1")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("CAF")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        doc.WriteStartElement("DA")
        doc.WriteStartElement("RE")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("RS")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("TD")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("RNG")
        doc.WriteStartElement("D")
        doc.WriteValue("1")
        doc.WriteEndElement()

        doc.WriteStartElement("H")
        doc.WriteValue("200")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin RNG

        doc.WriteStartElement("FA")
        doc.WriteValue("")
        doc.WriteEndElement()

        doc.WriteStartElement("RSAPK")
        doc.WriteStartElement("M")
        doc.WriteValue("asdadsaudaa8d0a8da0s")
        doc.WriteEndElement()

        doc.WriteStartElement("E")
        doc.WriteValue("")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin RSAPK

        doc.WriteStartElement("IDK")
        doc.WriteValue("100")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin DA

        doc.WriteStartElement("FRMA")
        doc.WriteStartAttribute("algortimo")
        doc.WriteValue("SHA1withRSA")
        doc.WriteEndAttribute()
        doc.WriteValue("23453454345353453")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin CAF


        doc.WriteStartElement("TSTED")
        doc.WriteValue("20000-200000:111")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin DD

        doc.WriteStartElement("FRMT")
        doc.WriteStartAttribute("algortimo")
        doc.WriteValue("SHA1withRSA")
        doc.WriteEndAttribute()
        doc.WriteValue("23453454345353453")
        doc.WriteEndElement()
        doc.WriteEndElement() ' fin TED

        doc.WriteStartElement("TmstFirma")
        doc.WriteValue("20000:0000")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin Documento

        doc.WriteStartElement("Signature")
        doc.WriteStartAttribute("xmls")
        doc.WriteValue("http://www.w3.org/2000/09/xmldsig#")
        doc.WriteEndAttribute()

        doc.WriteStartElement("SignedInfo")
        doc.WriteStartElement("CanonicalizationMethod")
        doc.WriteStartAttribute("Algorithm")
        doc.WriteValue("http://www.w3.org/TR/2001/REC-xml-c14n-20010315")
        doc.WriteEndAttribute()
        doc.WriteEndElement()

        doc.WriteStartElement("SignatureMethod")
        doc.WriteStartAttribute("Algorithm")
        doc.WriteValue("http://www.w3.org/2000/09/xmldsig#rsa-sha1")
        doc.WriteEndElement()
        doc.WriteEndElement()

        doc.WriteStartElement("Reference")
        doc.WriteStartAttribute("URI")
        doc.WriteValue("#F60T33")
        doc.WriteEndAttribute()

        doc.WriteStartElement("Transforms")
        doc.WriteStartElement("Transform")
        doc.WriteStartAttribute("Algorithm")
        doc.WriteValue("http://www.w3.org/TR/2001/REC-xml-c14n-20010315")
        doc.WriteEndAttribute()
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin Transforms

        doc.WriteStartElement("DigestMethod")
        doc.WriteStartAttribute("Algorithm")
        doc.WriteValue("http://www.w3.org/2000/09/xmldsig#sha1")
        doc.WriteEndAttribute()
        doc.WriteEndElement()

        doc.WriteStartElement("DigestValue")
        doc.WriteValue("hlmQtu/AyjUjTDhM3852wvRCr8w=</")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin reference
        doc.WriteEndElement() 'fin SignedInfo

        doc.WriteStartElement("SignatureValue")
        doc.WriteValue("sBnr8Yq14vVAcrN/pKLD/BrqUFczKMW3y1t3JOrdsxhhq6IxvS13SgyMXbIN")
        doc.WriteEndElement()

        doc.WriteStartElement("KeyInfo")
        doc.WriteStartElement("KeyValue")
        doc.WriteStartElement("RSAKeyValue")
        doc.WriteStartElement("Modulus")
        doc.WriteValue("asdasdasdadasdasdasdasd")
        doc.WriteEndElement()

        doc.WriteStartElement("Exponent")
        doc.WriteValue("AQAB")
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin RSAKeyValue
        doc.WriteEndElement() 'fin KeyValue

        doc.WriteStartElement("X509Data")
        doc.WriteStartElement("X509Certificate")
        doc.WriteValue("Aqui va certificado digital")
        doc.WriteEndElement()
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin keyinfo
        doc.WriteEndElement() 'fin Signature
        doc.WriteEndElement() 'fin DTE
        doc.WriteEndElement() ' fin set Dte

        'doc.WriteStartElement("Signature")
        'doc.WriteStartAttribute("xmlns")
        'doc.WriteValue("http://www.w3.org/2000/09/xmldsig#")
        'doc.WriteEndAttribute()

        'doc.WriteStartElement("SignedInfo")
        'doc.WriteStartElement("CanonicalizationMethod")
        'doc.WriteStartAttribute("Algorithm")
        'doc.WriteValue("http://www.w3.org/TR/2001/REC-xml-c14n-20010315")
        'doc.WriteEndAttribute()
        'doc.WriteEndElement()

        'doc.WriteStartElement("SignatureMethod")
        'doc.WriteStartAttribute("Algorithm")
        'doc.WriteValue("http://www.w3.org/2000/09/xmldsig#rsa-sha1")
        'doc.WriteEndAttribute()
        'doc.WriteEndElement()

        'doc.WriteStartElement("Reference")
        'doc.WriteStartAttribute("URI")
        'doc.WriteValue("#F60T33")
        'doc.WriteEndAttribute()

        'doc.WriteStartElement("Transforms")
        'doc.WriteStartAttribute("Algorithm")
        'doc.WriteValue("http://www.w3.org/TR/2001/REC-xml-c14n-20010315")
        'doc.WriteEndAttribute()
        'doc.WriteEndElement()

        'doc.WriteStartElement("DigestMethod")
        'doc.WriteStartAttribute("Algorithm")
        'doc.WriteValue("http://www.w3.org/2000/09/xmldsig#sha1")
        'doc.WriteEndAttribute()
        'doc.WriteEndElement()

        'doc.WriteStartElement("DigestValue")
        'doc.WriteValue("hlmQtu/AyjUjTDhM3852wvRCr8w")
        'doc.WriteEndElement()
        'doc.WriteEndElement() 'fin reference
        'doc.WriteEndElement() ' fin SignedInfo

        'doc.WriteStartElement("SignatureValue")
        'doc.WriteValue("ADSDADS")
        'doc.WriteEndElement()

        'doc.WriteStartElement("KeyInfo")
        'doc.WriteStartElement("KeyValue")
        'doc.WriteStartElement("RSAKeyValue")
        'doc.WriteStartElement("Modulus")
        'doc.WriteValue("ADSADSADADSADSSD")
        'doc.WriteEndElement()

        'doc.WriteStartElement("Exponent")
        'doc.WriteValue("AQAB")
        'doc.WriteEndElement()
        'doc.WriteEndElement() 'fin RSAKeyValue
        'doc.WriteEndElement() 'fin KeyValue

        'doc.WriteStartElement("X509Data")
        'doc.WriteStartElement("X509Certificate")
        'doc.WriteValue("aqui va firma digital")
        'doc.WriteEndElement()
        'doc.WriteEndElement() ' fin X509Data
        'doc.WriteEndElement() 'fin KeyInfo
        'doc.WriteEndElement() ' fin Signature


        doc.WriteEndDocument() ' fin documento



        doc.Flush()
        doc.Close()
    End Sub

    Public Sub CreaXmlToken(ByVal seed As String)
        Dim doc As New XmlTextWriter(System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/XmlSeed/Token.xml", Encoding.UTF8)

        doc.WriteStartDocument()
        doc.WriteStartElement("getToken")
        doc.WriteStartElement("Item")
        doc.WriteStartElement("Semilla")
        doc.WriteValue(seed)
        doc.WriteEndElement() 'fin Semilla
        doc.WriteEndElement() 'Fin Item


        doc.WriteEndElement() 'Fin getToken
        doc.WriteEndDocument() ' fin documento

        doc.Flush()
        doc.Close()

    End Sub

End Class
