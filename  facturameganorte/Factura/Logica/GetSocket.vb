Imports System
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports Microsoft.VisualBasic


Public Class GetSocket

    Dim CL As New ControladorLogica
    Public Function ConnectSocket(ByVal server As String, ByVal port As Integer) As Socket
        Dim s As Socket = Nothing
        Dim hostEntry As IPHostEntry = Nothing

        ' Get host related information.
        hostEntry = Dns.GetHostEntry(server)

        ' Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        ' an exception that occurs when the host host IP Address is not compatible with the address family
        ' (typical in the IPv6 case).
        Dim address As IPAddress

        For Each address In hostEntry.AddressList
            Dim endPoint As New IPEndPoint(address, port)
            Dim tempSocket As New Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

            tempSocket.Connect(endPoint)

            If tempSocket.Connected Then
                s = tempSocket
                Exit For
            End If

        Next address

        Return s
    End Function


    ' This method requests the home page content for the specified server.

    Public Function SocketSendReceive(ByVal server As String, ByVal port As Integer, ByVal Data As String, ByVal Token As String, ByVal rutSender As String, _
                                    ByVal dvSender As String, ByVal rutCompany As String, ByVal dvCompany As String, ByVal NombreArchivo As String) As String
        'Set up variables and String to write to the server.
        Dim ascii As Encoding = Encoding.ASCII

        Dim request As String = "POST /cgi_dte/UPL/DTEUpload HTTP/1.0" & vbCrLf & "Accept:image/gif, image/x-xbitmap, image/jpeg, image/pjpeg," _
                                + " application/vnd.ms-powerpoint, application/ms-excel, application/msword, */*" & vbCrLf & "Referer:http://www.google.cl" _
                                & vbCrLf & "Accept-Language: es-cl" & vbCrLf & "Content-Type:multipart/form-data: boundary=---------------------------9022632e1130lc4" & vbCrLf & _
                                "Accept-Encoding: gzip, deflate" & vbCrLf & "User-Agent: Mozilla/4.0 (compatible; PROG 1.0; Windows NT 5.0; YComp" _
                                + "5.0.2.4)" & vbCrLf & "Host: https://maullin.sii.cl" & vbCrLf & "Content-Length: 10240" & vbCrLf & "Connection: Keep-Alive" & vbCrLf & _
                                "Cache-Control: no-Cache" & vbCrLf & "Cookie: TOKEN=" + Token & vbCrLf & vbCrLf & "-----------------------------7d23e2a11301c4" & vbCrLf & _
                                "Content-Disposition: form-data; name=""rutSender""" & vbCrLf & vbCrLf & rutSender & vbCrLf & _
                                "-----------------------------9022632e1130lc4" & vbCrLf & "Content-Disposition: form-data; name=""dvSender""" & vbCrLf & vbCrLf & dvSender & vbCrLf & _
                                "-----------------------------9022632e1130lc4" & vbCrLf & "Content-Disposition: form-data; name=""rutCompany""" & vbCrLf & vbCrLf & rutCompany & vbCrLf & _
                                "-----------------------------9022632e1130lc4" & vbCrLf & "Content-Disposition: form-data; name=""dvCompany""" & vbCrLf & vbCrLf & dvCompany & vbCrLf & _
                                "-----------------------------9022632e1130lc4" & vbCrLf & "Content-Disposition: form-data; name=""archivo""; filename=""" _
                                + NombreArchivo + """" & vbCrLf & "Content-Type: text/xml" & vbCrLf & vbCrLf & "" & vbCrLf & vbCrLf & "-----------------------------7d23e2a11301c4--" & vbCrLf



        MsgBox(request)
        Dim bytesSent As [Byte]() = ascii.GetBytes(request)
        Dim bytesReceived(255) As [Byte]

        ' Create a socket connection with the specified server and port.
        Dim s As Socket = ConnectSocket(server, port)

        If s Is Nothing Then
            Return "Connection failed"
        End If
        ' Send request to the server.
        s.Send(bytesSent, bytesSent.Length, 0)

        ' Receive the server  home page content.
        Dim bytes As Int32

        ' Read the first 256 bytes.
        Dim page As [String] = "Default HTML page on " + server + ":" + ControlChars.Cr + ControlChars.Lf

        ' The following will block until the page is transmitted.
        Do
            bytes = s.Receive(bytesReceived, bytesReceived.Length, 0)
            page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes)
        Loop While bytes > 0
        Return page
    End Function

End Class
