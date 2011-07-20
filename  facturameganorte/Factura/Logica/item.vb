Public Class item

    Private _item As String
    Private _codigo As String
    Private _detalle As String
    Private _cantidad As String
    Private _precioUnitario As String


    Public Sub New(ByVal _item As String, ByVal _codigo As String, ByVal _detalle As String _
, ByVal _cantidad As String, ByVal _precioUnitario As String)
        Me._item = _item
        Me._codigo = _codigo
        Me._detalle = _detalle
        Me._cantidad = _cantidad
        Me._precioUnitario = _precioUnitario

    End Sub

    Public Property Item() As String

        Get
            Return _item
        End Get

        Set(ByVal value As String)
            _item = value
        End Set

    End Property

    Public Property Codigo() As String

        Get
            Return _codigo
        End Get

        Set(ByVal value As String)
            _codigo = value
        End Set

    End Property

    Public Property Detalle() As String

        Get
            Return _detalle
        End Get

        Set(ByVal value As String)
            _detalle = value
        End Set

    End Property

    Public Property Cantidad() As String

        Get
            Return _cantidad
        End Get

        Set(ByVal value As String)
            _cantidad = value
        End Set

    End Property

    Public Property PrecioUnitario() As String

        Get
            Return _precioUnitario
        End Get

        Set(ByVal value As String)
            _precioUnitario = value
        End Set

    End Property

End Class





