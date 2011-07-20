Imports System.Data

Partial Public Class Principal
    Inherits System.Web.UI.MasterPage
    Dim data As New DataTable

#Region "Declaraciones"

#End Region

#Region "Carga"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
#End Region

#Region "Eventos"
    Protected Sub TreeView1_SelectedNodeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TreeView1.SelectedNodeChanged
        Data = DirectCast(Session("tb3"), DataTable)
        If Not Data Is Nothing Then
            Data.Clear()
            Session("tb3") = Data
        End If
        Try
            If Me.TreeView1.SelectedNode.Value.Equals("nodoDebito") Then
                Response.Redirect("frmConstruccion.aspx")
            Else
                If Me.TreeView1.SelectedNode.Value.Equals("nodoMenu") Then

                    Response.Redirect("frmBienvenida.aspx")
                Else
                    If Me.TreeView1.SelectedNode.Value.Equals("nodoCredito") Then

                        Response.Redirect("frmNotaCredito.aspx")

                    Else
                        If Me.TreeView1.SelectedNode.Value.Equals("nodoFactura") Then

                            Response.Redirect("frmFactura.aspx")
                        Else
                            If Me.TreeView1.SelectedNode.Value.Equals("nodoCompra") Then

                                Response.Redirect("frmLibroCompra.aspx")
                            Else
                                If Me.TreeView1.SelectedNode.Value.Equals("nodoVenta") Then

                                    Response.Redirect("frmLibroVenta.aspx")
                                Else
                                    If Me.TreeView1.SelectedNode.Value.Equals("nodoRecibidos") Then

                                        Response.Redirect("frmDocmentosRecividos.aspx")
                                    Else
                                        If Me.TreeView1.SelectedNode.Value.Equals("nodoEmitidos") Then

                                            Response.Redirect("frmDocumentosEmitidos.aspx")
                                        End If
                                    End If

                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class