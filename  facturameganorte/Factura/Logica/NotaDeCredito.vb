Public Class NotaDeCredito

    Private _id As String
    Private _factura As String
    Private _fecha As String

    Public Sub New(ByVal _id As String, ByVal _factura As String, ByVal _fecha As String)

        Me._id = _id
        Me._factura = _factura
        Me._fecha = _fecha

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

End Class
