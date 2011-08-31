Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Collections


Public Class ClassXML

    Dim CP As New controladorPersistencia
    Dim CL As New ControladorLogica

    Public Sub creaXml(ByVal rutEmisor As String, ByVal rutEnvia As String, ByVal rutReceptor As String, ByVal fechaEmision As String, _
    ByVal rznSocial As String, ByVal giroEmisor As String, ByVal DirOrigen As String, ByVal rznsocialReceptor As String, ByVal giroReceptor As String, _
    ByVal dirReceptor As String, ByVal comunaRecept As String, ByVal ciudadRecept As String, ByVal total As Double, ByVal montoNeto As Double, _
    ByVal tasaIva As Double, ByVal ArregloItem As ArrayList, ByVal pathOrg As String)


        Dim IdFactura As String = CP.ObtenerIDFactura()
        Dim counter As Integer = 0
        Dim doc As New XmlTextWriter(pathOrg, Encoding.UTF8)

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
        doc.WriteValue("FALTA")
        doc.WriteEndElement()

        doc.WriteStartElement("TmstFirmaEnv")
        doc.WriteValue("FALTA")
        doc.WriteEndElement()

        doc.WriteStartElement("SubTotDTE")
        doc.WriteStartElement("TpoDTE")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("NroDTE")
        doc.WriteValue("FALTA")
        doc.WriteEndElement()

        doc.WriteEndElement()
        doc.WriteEndElement() ' fin caratula

        doc.WriteStartElement("DTE")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        doc.WriteStartElement("Documento")
        doc.WriteStartAttribute("ID")
        doc.WriteValue("FALTA")
        doc.WriteEndAttribute()

        doc.WriteStartElement("Encabezado")
        doc.WriteStartElement("IdDoc")
        doc.WriteStartElement("TipoDTE")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("Folio")
        doc.WriteValue(IdFactura)
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
        doc.WriteValue("FALTA")
        doc.WriteEndElement()

        doc.WriteStartElement("CdgSIISucur")
        doc.WriteValue("FALTA")
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
            doc.WriteValue(item.Codigo)
            doc.WriteEndElement()
            doc.WriteEndElement() 'fin cdgItem

            doc.WriteStartElement("NmbItem")
            doc.WriteValue(item.Item)
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

        Dim PrimerItem As item = DirectCast(ArregloItem.Item(0), item)
        Dim nombrePrimerItem As String = PrimerItem.Item
        Dim monto As Integer = CInt(PrimerItem.PrecioUnitario) * CInt(PrimerItem.Cantidad)

        doc.WriteStartElement("TED")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()


        doc.WriteStartElement("RE")
        doc.WriteValue(rutEmisor)
        doc.WriteEndElement()

        doc.WriteStartElement("TD")
        doc.WriteValue("33")
        doc.WriteEndElement()

        doc.WriteStartElement("F")
        doc.WriteValue(IdFactura)
        doc.WriteEndElement()

        doc.WriteStartElement("FE")
        doc.WriteValue(fechaEmision)
        doc.WriteEndElement()

        doc.WriteStartElement("RR")
        doc.WriteValue(rutReceptor)
        doc.WriteEndElement()

        doc.WriteStartElement("RSR")
        doc.WriteValue(rznSocial)
        doc.WriteEndElement()

        doc.WriteStartElement("MNT")
        doc.WriteValue(monto)
        doc.WriteEndElement()

        doc.WriteStartElement("IT1")
        doc.WriteValue(nombrePrimerItem)
        doc.WriteEndElement()

        'Fin de datos precargados


        ' Lectura de datos desde Folios Autorizados
        '------------------------------------------
        Dim FirmaDig As New FirmaDigital
        Dim Path As String = System.AppDomain.CurrentDomain.BaseDirectory() + "/XmlFiles/Folios/FolioFactura.xml"
        Dim RE As String = CL.obtieneLecturaXML(Path, "RE")
        Dim RS As String = CL.obtieneLecturaXML(Path, "RS")
        Dim TD As String = CL.obtieneLecturaXML(Path, "TD")
        Dim D As String = CL.obtieneLecturaXML(Path, "D")
        Dim H As String = CL.obtieneLecturaXML(Path, "H")
        Dim FA As String = CL.obtieneLecturaXML(Path, "FA")
        Dim M As String = CL.obtieneLecturaXML(Path, "M")
        Dim E As String = CL.obtieneLecturaXML(Path, "E")
        Dim IDK As String = CL.obtieneLecturaXML(Path, "IDK")
        Dim FRMA As String = CL.obtieneLecturaXML(Path, String.Format("FRMA algortimo={0}SHA1withRSA{0} ", Chr(34)))
        Dim RSASK As String = FirmaDig.obtienePrivateKeyFactura()
        Dim RSAPUBK As String = FirmaDig.obtienePublicKeyFactura()

        doc.WriteStartElement("AUTORIZACION")
        doc.WriteStartElement("CAF")
        doc.WriteStartAttribute("version")
        doc.WriteValue("1.0")
        doc.WriteEndAttribute()

        doc.WriteStartElement("DA")
        doc.WriteStartElement("RE")
        doc.WriteValue(RE)
        doc.WriteEndElement()

        doc.WriteStartElement("RS")
        doc.WriteValue(RS)
        doc.WriteEndElement()

        doc.WriteStartElement("TD")
        doc.WriteValue(TD)
        doc.WriteEndElement()

        doc.WriteStartElement("RNG")
        doc.WriteStartElement("D")
        doc.WriteValue(D)
        doc.WriteEndElement()

        doc.WriteStartElement("H")
        doc.WriteValue(H)
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin RNG

        doc.WriteStartElement("FA")
        doc.WriteValue(FA)
        doc.WriteEndElement()

        doc.WriteStartElement("RSAPK")
        doc.WriteStartElement("M")
        doc.WriteValue(M)
        doc.WriteEndElement()

        doc.WriteStartElement("E")
        doc.WriteValue(E)
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin RSAPK

        doc.WriteStartElement("IDK")
        doc.WriteValue(IDK)
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin DA

        doc.WriteStartElement("FRMA")
        doc.WriteStartAttribute("algortimo")
        doc.WriteValue("SHA1withRSA")
        doc.WriteEndAttribute()
        doc.WriteValue(FRMA)
        doc.WriteEndElement()
        doc.WriteEndElement() 'fin CAF

        doc.WriteStartElement("RSASK")
        doc.WriteValue(RSASK)
        doc.WriteEndElement() 'Fin RSASK

        doc.WriteStartElement("RSAPUBK")
        doc.WriteValue(RSAPUBK)
        doc.WriteEndElement() 'Fin RSASK
        doc.WriteEndElement() 'Fin Autorizacion

        doc.WriteEndElement() 'fin Documento

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
