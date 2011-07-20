Public Class NotaDeCredito

    Private _id As String
    Private _factura As String
    Private _fecha As String
    Private _iva As String
    Private _neto As String
    Private _total As String


    Public Sub New(ByVal _id As String, ByVal _factura As String, ByVal _fecha As String, ByVal _iva As String, ByVal _neto As String, ByVal _total As String)

        Me._id = _id
        Me._factura = _factura
        Me._fecha = _fecha
        Me._iva = _iva
        Me._neto = _neto
        Me._total = _total

    End Sub

    Public Property Id() As String

        Get
            Return _id
        End Get

        Set(ByVal value As String)
            _id = value
        End Set

    End Property

    Public Property Factura() As String
        Get
            Return _factura
        End Get

        Set(ByVal value As String)
            _factura = value
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

    Public Property Iva() As String

        Get
            Return _iva
        End Get

        Set(ByVal value As String)
            _iva = value
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

End Class
