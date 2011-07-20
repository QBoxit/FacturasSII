Public Class libroCompraData

    Private _rut As String
    Private _ndocumento As String
    Private _provedor As String
    Private _dia As String
    Private _mes As String
    Private _anio As String
    Private _iva As Integer
    Private _neto As Integer
    Private _total As Integer


    Public Sub New(ByVal _rut As String, ByVal _ndocumento As String, ByVal _provedor As String _
    , ByVal _dia As String, ByVal _mes As String, ByVal _anio As String, ByVal _iva As Integer, ByVal _neto As Integer, ByVal _
    _total As Integer)

        Me._rut = _rut
        Me._ndocumento = _ndocumento
        Me._provedor = _provedor
        Me._dia = _dia
        Me._mes = _mes
        Me._anio = _anio
        Me._iva = _iva
        Me._neto = _neto
        Me._total = _total

    End Sub

    Public Property Rut() As String

        Get
            Return _rut
        End Get

        Set(ByVal value As String)
            _rut = value
        End Set

    End Property

    Public Property Ndocmento() As String

        Get
            Return _ndocumento
        End Get

        Set(ByVal value As String)
            _ndocumento = value
        End Set

    End Property
    Public Property Proveedor() As String

        Get
            Return _provedor
        End Get

        Set(ByVal value As String)
            _provedor = value
        End Set

    End Property
    Public Property Dia() As String

        Get
            Return _dia
        End Get

        Set(ByVal value As String)
            _dia = value
        End Set

    End Property
    Public Property Mes() As String

        Get
            Return _mes
        End Get

        Set(ByVal value As String)
            _mes = value
        End Set

    End Property
    Public Property Anio() As String

        Get
            Return _anio
        End Get

        Set(ByVal value As String)
            _anio = value
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
