Public Class persona
    Private _rut As String
    Private _razonsocial As String
    Private _direccion As String
    Private _giro As String
    Private _comuna As String
    Private _fono As String
    Private _ciudad As String

    Public Sub New(ByVal _rut As String, ByVal _rs As String, ByVal _dir As String _
    , ByVal _giro As String, ByVal _Com As String, ByVal _fono As String, ByVal _ciudad As String)
        Me._rut = _rut
        Me._razonsocial = _rs
        Me._direccion = _dir
        Me._giro = _giro
        Me._fono = _fono
        Me._comuna = _Com
        Me._ciudad = _ciudad
    End Sub
    Public Property Ciudad() As String

        Get
            Return _ciudad
        End Get

        Set(ByVal value As String)
            _ciudad = value
        End Set

    End Property
    Public Property Rut() As String

        Get
            Return _rut
        End Get

        Set(ByVal value As String)
            _rut = value
        End Set

    End Property

    Public Property razonsocial() As String

        Get
            Return _razonsocial
        End Get

        Set(ByVal value As String)
            _razonsocial = value
        End Set

    End Property


    Public Property direccion() As String

        Get
            Return _direccion
        End Get

        Set(ByVal value As String)
            _direccion = value
        End Set

    End Property

    Public Property fono() As String

        Get
            Return _fono
        End Get

        Set(ByVal value As String)
            _fono = value
        End Set

    End Property

    Public Property giro() As String

        Get
            Return _giro
        End Get

        Set(ByVal value As String)
            _giro = value
        End Set

    End Property

    Public Property comuna() As String

        Get
            Return _comuna
        End Get

        Set(ByVal value As String)
            _comuna = value
        End Set

    End Property

End Class
