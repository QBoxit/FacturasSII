Imports System
Imports System.Data
Imports Npgsql
Public Class bd

    Private conn As NpgsqlConnection

    Private stringConnection As String = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=1a2b3c;Database=facturacion;"


    Public Sub New()
        Try
            conn = New NpgsqlConnection(stringConnection)
        Catch ex As NpgsqlException
            MsgBox(ex)
        End Try

    End Sub

    Private Sub Open()
        conn.Open()
    End Sub

    Private Sub Close()
        conn.Close()
    End Sub

    Public Function EjecutarConReturn(ByVal query As String) As Boolean
        Dim dr As Npgsql.NpgsqlDataReader
        Dim dato As String = ""

        Try
            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            While dr.Read()
                dato = CStr(dr(dr.GetName(0).ToString))
            End While

            Me.Close()

            If dato = "" Then
                Return False
            Else
                Return True
            End If

        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function

    Public Function EjecutarConInt(ByVal query As String) As Integer
        Dim dr As Npgsql.NpgsqlDataReader
        Dim dato As Integer = 0

        Try
            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            While dr.Read()
                dato = CInt(dr(dr.GetName(0).ToString))
            End While

            Me.Close()

            If dato = 0 Then
                Return 0
            Else
                Return dato
            End If

        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function

    Public Function EjecutarConRetorno(ByVal query As String) As Hashtable
        Dim array As New Hashtable
        Dim dr As Npgsql.NpgsqlDataReader
        Try
            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()

            Dim i As Integer
            While dr.Read()
                For i = 0 To dr.FieldCount - 1
                    Dim dato As String = CStr(dr(dr.GetName(i).ToString))
                    Dim nombrecampo As String = dr.GetName(i).ToString
                    array.Add(nombrecampo, dato)
                Next i
            End While

            Me.Close()

            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function

    Public Function obtenerid(ByVal query As String) As String
        Dim dato As String = ""
        Dim dr As Npgsql.NpgsqlDataReader
        Try
            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()

            Dim i As Integer
            While dr.Read()
                For i = 0 To dr.FieldCount - 1
                    dato = CStr(dr("maxima"))
                Next i
            End While

            Me.Close()

            Return dato
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return dato

    End Function

    Public Function ObtenerCliente(ByVal query As String) As ArrayList
        Dim array As New ArrayList
        Dim dr As Npgsql.NpgsqlDataReader

        Try

            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            While dr.Read()

                array.Add(CStr(dr("rut")))
                array.Add(CStr(dr("razonsocial")))
                array.Add(CStr(dr("direccion")))
                array.Add(CStr(dr("giro")))
                array.Add(CStr(dr("comuna")))
                array.Add(CStr(dr("telefono")))
                array.Add(CStr(dr("ciudad")))

            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Function Obtenerfactura(ByVal query As String) As ArrayList
        Dim array As New ArrayList

        Dim dr As Npgsql.NpgsqlDataReader

        Try

            Me.Open()
            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            Dim fact As factura
            While dr.Read()
                fact = New factura( _
                CStr(dr("vendedor")), _
                CStr(dr("fkrutcliente")), _
                CStr(dr("ordencompra")), _
                CStr(dr("nguiadespacho")), _
                CStr(dr("condicionventa")), _
                CStr(dr("fecha")), _
                CStr(dr("neto")), _
                CStr(dr("total")), _
                CStr(dr("iva")), _
                CStr(dr("id")) _
                )
                array.Add(fact)

            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Function ObtenerNotaCredito(ByVal query As String) As ArrayList
        Dim array As New ArrayList

        Dim dr As Npgsql.NpgsqlDataReader

        Try

            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            Dim nc As NotaDeCredito

            While dr.Read()
                nc = New NotaDeCredito(CStr(dr("id")), CStr(dr("fkfactura")), CStr(dr("dia")) + "/" + CStr(dr("mes")) + "/" + CStr(dr("anio")), CStr(dr("iva")), CStr(dr("neto")), CStr(dr("total")))
                array.Add(nc)

            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function

    Public Function ObtenerLcMes(ByVal query As String) As ArrayList
        Dim array As New ArrayList

        Dim dr As Npgsql.NpgsqlDataReader

        Try

            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            Dim itemlc As libroCompraData
            While dr.Read()
                itemlc = New libroCompraData _
                ( _
                CStr(dr("rut")), _
                CStr(dr("ndocumento")), _
                CStr(dr("proveedor")), _
                CStr(dr("dia")), _
                CStr(dr("mes")), _
                CStr(dr("anio")), _
                CStr(dr("iva")), _
                CStr(dr("neto")), _
                CStr(dr("total")) _
                )

                array.Add(itemlc)
       
            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Function ObtenerLvMes(ByVal query As String) As ArrayList
        Dim array As New ArrayList

        Dim dr As Npgsql.NpgsqlDataReader

        Try

            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()

            While dr.Read()
         
                array.Add(CStr(dr("id")))
                array.Add(CStr(dr("mes")))
                array.Add(CStr(dr("anio")))
                array.Add(CStr(dr("iva")))
                array.Add(CStr(dr("neto")))
                array.Add(CStr(dr("total")))


            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function

    Public Function ObtenerItemFactura(ByVal query As String) As ArrayList

        Dim array As New ArrayList

        Dim dr As Npgsql.NpgsqlDataReader

        Try

            Me.Open()

            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()
            Dim item As item
            While dr.Read()
                item = New item(CStr(dr("nombre")), CStr(dr("coditem")), CStr(dr("descripcion")), CStr(dr("cantidad")), CStr(dr("preciounitario")))

                array.Add(item)

            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing

    End Function

    Public Sub EjecutarSinRetorno(ByVal query As String)
        Try
            Me.Open()
            Dim command As New NpgsqlCommand(query, conn)
            command.ExecuteNonQuery()
            Me.Close()
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Sub EjecutarBinarioSinRetorno(ByVal bin As Byte())

        Try
            Me.Open()
            Dim command As NpgsqlCommand = New NpgsqlCommand("insert into historialfactura(data) values(:bytesData)", conn)
            Dim param As NpgsqlParameter = New NpgsqlParameter(":bytesData", DbType.Binary)
            param.Value = bin
            command.Parameters.Add(param)
            command.ExecuteNonQuery()
            Me.Close()
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try

    End Sub

    Public Function obtieneValorLibroVentaBD(ByVal query As String) As ArrayList
        Dim array As New ArrayList
        Dim dr As Npgsql.NpgsqlDataReader

        Try
            Me.Open()
            Dim mydataset As New DataSet
            Dim command As New NpgsqlCommand(query, conn)
            dr = command.ExecuteReader()

            While dr.Read()
                array.Add(CStr(dr("iva")))
                array.Add(CStr(dr("neto")))
                array.Add(CStr(dr("total")))
            End While
            Me.Close()
            Return array
        Catch ex As NpgsqlException
            MsgBox(ex.ToString)
        End Try
        Return Nothing
    End Function





End Class
