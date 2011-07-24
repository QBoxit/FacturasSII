<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Interfaz/Formularios/Principal.Master" CodeBehind="frmNotaCredito.aspx.vb" Inherits="Factura.frmNotaCredito" 
    title="MEGANORTE - Nota De Credito" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 688px; height: 256px">
        <tr>
            <td align="center" colspan="4" valign="middle" style="width: 838px">
                <asp:Label ID="lblGuia" runat="server" Text="Nota de Credito" Font-Bold="True" Font-Size="16pt"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="4" valign="middle" style="height: 196px; text-align: left; width: 838px;">
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Double">
                    <table style="width: 793px">
                        <tr>
                            <td style="width: 91px; text-align: left">
                <asp:Label ID="lblRazon" runat="server" Text="Factura" Width="38px"></asp:Label></td>
                            <td style="width: 307px">
                <asp:TextBox ID="textIdFactura" runat="server" Width="297px" AutoPostBack Font-Bold="True" ></asp:TextBox></td>
                            <td style="width: 102px">
                <asp:Label ID="lblFono" runat="server" Text="Fono:"></asp:Label></td>
                            <td style="width: 309px">
                <asp:TextBox ID="textFono" runat="server" Width="300px" TabIndex="2" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px; text-align: left">
                <asp:Label ID="lblDireccion" runat="server" Text="R.U.T :"></asp:Label></td>
                            <td style="width: 307px">
                <asp:TextBox ID="textRut" runat="server" Width="300px" TabIndex="1" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 102px">
                <asp:Label ID="lblPedido" runat="server" Text="Direccion :" Width="72px"></asp:Label></td>
                            <td style="width: 309px">
                <asp:TextBox ID="textDireccion" runat="server" Width="300px" TabIndex="4" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px; text-align: left">
                <asp:Label ID="lblComuna" runat="server" Text="Comuna:"></asp:Label></td>
                            <td style="width: 307px">
                <asp:TextBox ID="textComuna" runat="server" Width="300px" TabIndex="3" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 102px">
                <asp:Label ID="lblGiro" runat="server" Text="Giro:"></asp:Label></td>
                            <td style="width: 309px">
                <asp:TextBox ID="textGiro" runat="server" Width="300px" TabIndex="6" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px; text-align: left">
                <asp:Label ID="Label3" runat="server" Text="Cond. Venta :" Width="85px"></asp:Label></td>
                            <td style="width: 307px">
                <asp:TextBox ID="textCondVenta" runat="server" Width="300px" TabIndex="5" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 102px">
                <asp:Label ID="Label2" runat="server" Text="Guia Despacho :" Width="107px"></asp:Label></td>
                            <td style="width: 309px">
                <asp:TextBox ID="textGuiaDespacho" runat="server" TabIndex="10" Width="300px" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px; text-align: left">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label></td>
                            <td style="width: 307px">
                                <BDP:BasicDatePicker ID="BasicDatePicker1" runat="server" DateFormat="dd/MM/yyyy"
                                    ReadOnly="True">
                                </BDP:BasicDatePicker>
                            </td>
                            <td style="width: 102px">
                <asp:Label ID="lblPrecioUnitario" runat="server" Text="Razon Social :" Width="96px"></asp:Label></td>
                            <td style="width: 309px">
                <asp:TextBox ID="TextRazonSocial" runat="server" TabIndex="10" Width="300px" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px; text-align: left">
                <asp:Label ID="Label1" runat="server" Text="Vendedor :"></asp:Label></td>
                            <td style="width: 307px">
                <asp:TextBox ID="textVendedor" runat="server" TabIndex="9" Width="300px" Font-Bold="True" ReadOnly="True"></asp:TextBox></td>
                            <td style="width: 102px">
                                <asp:Label ID="Label4" runat="server" Text="Orden de Compra :" Width="127px"></asp:Label></td>
                            <td style="width: 309px">
                                <asp:TextBox ID="txtOrdenCompra" runat="server" Width="300px" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>
            <asp:Label ID="avisoNT" runat="server" Font-Bold="True" ForeColor="#FF0000" Width="812px"></asp:Label><br />
                <table style="width: 821px">
                    <tr>
                        <td style="height: 22px">
                            </td>
                    </tr>
                    <tr>
                        <td style="height: 26px">
                            <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            &nbsp;<asp:GridView ID="gvwDatos" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" AutoGenerateColumns="False" Width="739px">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                       
                        <asp:BoundField DataField="ITEM" HeaderText="Item" />
                        <asp:BoundField DataField="CODIGO" HeaderText="C&#243;digo" />
                        <asp:BoundField DataField="DETALLE" HeaderText="Detalle" />
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                        <asp:BoundField DataField="PRECIOUNITARIO" HeaderText="Precio Unitario " DataFormatString="{0:c}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="itemSelector" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4" style="height: 22px; width: 838px;" valign="middle">
                <asp:ImageButton ID="imgbtnGuardar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_guardar.png" TabIndex="15" />
                <asp:ImageButton ID="imgbtnLimpiar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_limpiar.png" />
                <asp:ImageButton ID="imgbtnExportar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_exportar.png" TabIndex="16" /></td>
        </tr>
    </table>
</asp:Content>
