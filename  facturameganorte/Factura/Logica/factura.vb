Public Class factura
    Private _vendedor As String
    Private _cliente As String
    Private _ordenComrpra As String
    Private _nGuiaDespacho As String
    Private _conVenta
    Private _fecha As String
    Private _neto As Integer
    Private _total As Integer
    Private _iva As Integer
    Private _persona As persona
    Private _nfact As String

    Public Sub New(ByVal _vendedor As String, ByVal _cliente As String, ByVal _ordenComrpra As String, ByVal _nGuiaDespacho As String _
    , ByVal _conVenta As String, ByVal _fecha As String, ByVal _neto As Integer, ByVal _total As Integer, ByVal _iva As Integer, _
    ByVal _persona As persona)

        Me._vendedor = _vendedor
        Me._cliente = _cliente
        Me._ordenComrpra = _ordenComrpra
        Me._nGuiaDespacho = _nGuiaDespacho
        Me._conVenta = _conVenta
        Me._fecha = _fecha
        Me._neto = _neto
        Me._total = _total
        Me._iva = _iva
        Me._persona = _persona

    End Sub

    Public Sub New(ByVal _vendedor As String, ByVal _cliente As String, ByVal _ordenComrpra As String, ByVal _nGuiaDespacho As String _
, ByVal _conVenta As String, ByVal _fecha As String, ByVal _neto As Integer, ByVal _total As Integer, ByVal _iva As Integer _
, ByVal _nfact As String)

        Me._vendedor = _vendedor
        Me._cliente = _cliente
        Me._ordenComrpra = _ordenComrpra
        Me._nGuiaDespacho = _nGuiaDespacho
        Me._conVenta = _conVenta
        Me._fecha = _fecha
        Me._neto = _neto
        Me._total = _total
        Me._iva = _iva
        Me._nfact = _nfact
    End Sub

    Public Property Persona() As persona

        Get
            Return _persona
        End Get

        Set(ByVal value As persona)
            _persona = value
        End Set

    End Property
    Public Property NumeroFactura() As String

        Get
            Return _nfact
        End Get

        Set(ByVal value As String)
            _nfact = value
        End Set

    End Property

    Public Property Vendedor() As String

        Get
            Return _vendedor
        End Get

        Set(ByVal value As String)
            _vendedor = value
        End Set

    End Property

    Public Property Cliente() As String

        Get
            Return _cliente
        End Get

        Set(ByVal value As String)
            _cliente = value
        End Set

    End Property

    Public Property OrdenDeCompra() As String

        Get
            Return _ordenComrpra
        End Get

        Set(ByVal value As String)
            _ordenComrpra = value
        End Set

    End Property

    Public Property NguiaDespacho() As String

        Get
            Return _nGuiaDespacho
        End Get

        Set(ByVal value As String)
            _nGuiaDespacho = value
        End Set

    End Property

    Public Property CondicionVenta() As String

        Get
            Return _conVenta
        End Get

        Set(ByVal value As String)
            _conVenta = value
        End Set

    End Property

    Public Property Fecha() As String
        Get
            Return _fecha
        End Get

        Set(ByVal value As String)
            _fecha = value
        End Set

    End Property

    Public Property Neto() As String

        Get
            Return _neto
        End Get

        Set(ByVal value As String)
            _neto = value
        End Set

    End Property

    Public Property Total() As String

        Get
            Return _total
        End Get

        Set(ByVal value As String)
            _total = value
        End Set

    End Property

    Public Property Iva() As String

        Get
            Return _iva
        End Get

        Set(ByVal value As String)
            _iva = value
        End Set

    End Property

End Class
