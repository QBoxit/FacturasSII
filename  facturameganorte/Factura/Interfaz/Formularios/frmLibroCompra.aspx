<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Interfaz/Formularios/Principal.Master" CodeBehind="frmLibroCompra.aspx.vb" Inherits="Factura.frmLibroCompra" 
    title="MEGANORTE - Libro De Compra"%>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="~/Script/validarut.js" type="text/javascript"></script> 

   <script type="text/javascript">
      function ValidNum(e) {
      var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
      return ((tecla > 47 && tecla < 58) || tecla == 46);
                           }
  </script>

    <table>
        <tr>
            <td style="width: 459px; text-align: center; height: 21px;">
                <strong>LIBRO DE COMPRAS</strong></td>
        </tr>
        <tr>
            <td style="width: 459px; height: 71px;">
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Double">
                    
                        <table style="width: 181px">
                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="lblRut" runat="server" Text="R.U.T. :" Width="53px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtRut" runat="server" Font-Bold="True" Width="300px"  AutoPostBack="True"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label4" runat="server" Text="Fecha:"></asp:Label></td>
                                <td style="text-align: left">
                                    <bdp:basicdatepicker id="txtFecha" runat="server" font-bold="True" DateFormat="dd-MM-yyyy" ReadOnly="True"></bdp:basicdatepicker>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="Label2" runat="server" Text="N° Documento:" Width="98px"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDocumento" runat="server" Font-Bold="True" Width="300px"></asp:TextBox></td>
                                <td style="text-align: left">
                                    <asp:Label ID="Label5" runat="server" Text="Iva:"></asp:Label></td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtIva" runat="server" Font-Bold="True" Width="300px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: left; height: 26px;">
                                    <asp:Label ID="Label3" runat="server" Text="Proveedor:"></asp:Label></td>
                                <td style="text-align: left; height: 26px;">
                                    <asp:TextBox ID="txtProveedor" runat="server" Font-Bold="True" Width="300px"></asp:TextBox></td>
                                <td style="height: 26px; text-align: left;">
                                    <asp:Label ID="Label6" runat="server" Text="Neto:"></asp:Label></td>
                                <td style="text-align: left; height: 26px;">
                                    <asp:TextBox ID="txtNeto" runat="server" Font-Bold="True" Width="300px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: left; height: 26px;">
                                </td>
                                <td style="text-align: left; height: 26px;">
                                </td>
                                <td style="height: 26px; text-align: left;">
                                    <asp:Label ID="Label7" runat="server" Text="Total:"></asp:Label></td>
                                <td style="text-align: left; height: 26px;">
                                    <asp:TextBox ID="txtTotal" runat="server" Font-Bold="True" Width="300px"></asp:TextBox></td>
                            </tr>
                        </table>
                    <table style="width: 545px">
                        <tr>
                            <td style="height: 30px">
                                <asp:Button ID="Button1" runat="server" Text="Ingresar Datos" /></td>
                            <td style="width: 625px; height: 30px;">
                                <asp:Label ID="errorFaltaDatos" runat="server" Font-Bold="True" ForeColor="#FF0033"
                                    Width="632px"></asp:Label></td>
                        </tr>
                    </table>
                    &nbsp;&nbsp;
                </asp:Panel>
                &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 459px; height: 9px;">
                <br />
                <table style="width: 423px">
                    <tr>
                        <td style="width: 216px">
                            <asp:DropDownList ID="ComboMes" runat="server" Font-Bold="True" Width="145px" >
                                <asp:ListItem Value="01">Enero</asp:ListItem>
                                <asp:ListItem Value="02">Febrero</asp:ListItem>
                                <asp:ListItem Value="03">Marzo</asp:ListItem>
                                <asp:ListItem Value="04">Abril</asp:ListItem>
                                <asp:ListItem Value="05">Mayo</asp:ListItem>
                                <asp:ListItem Value="06">Junio</asp:ListItem>
                                <asp:ListItem Value="07">Julio</asp:ListItem>
                                <asp:ListItem Value="08">Agosto</asp:ListItem>
                                <asp:ListItem Value="09">Septiembre</asp:ListItem>
                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                <asp:ListItem Value="12">Dicembre</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td style="width: 216px">
                            <asp:Label ID="lblAnio" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_visualizar.png" />
                        </td>
                        <td>
                            </td>
                        <td style="width: 1px">
                            <strong><span style="text-decoration: underline"></span></strong></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="height: 21px; text-align: left; width: 81px;">
                            <strong><span style="text-decoration: underline">IVA Mes</span></strong></td>
                        <td style="height: 21px">
                            <strong>:</strong></td>
                        <td style="height: 21px; text-align: left">
                            <asp:Label ID="lblivaMes" runat="server" Font-Bold="True" ForeColor="Red" Width="427px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 81px;">
                            <strong><span style="text-decoration: underline">NETO Mes</span></strong></td>
                        <td>
                            <strong>:</strong></td>
                        <td style="text-align: left">
                            <asp:Label ID="lblnetoMes" runat="server" Font-Bold="True" ForeColor="Red" Width="427px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 81px;">
                            <strong><span style="text-decoration: underline">Total Mes</span></strong></td>
                        <td>
                            <strong>:</strong></td>
                        <td style="text-align: left">
                            <asp:Label ID="lbltotalMes" runat="server" Font-Bold="True" ForeColor="Red" Width="427px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 81px; text-align: left">
                            <asp:Label ID="Label1" runat="server" Width="84px"></asp:Label></td>
                        <td>
                        </td>
                        <td style="text-align: left">
                        </td>
                    </tr>
                </table>
                <br />
                <table style="width: 366px">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLC" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="739px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="RUT" HeaderText="Rut" />
                                    <asp:BoundField DataField="NDOCUMENTO" HeaderText="N&#176; Documento" />
                                    <asp:BoundField DataField="PROVEEDOR" HeaderText="Proveedor" />
                                    <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                                    <asp:BoundField DataField="IVA" HeaderText="Iva" >
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NETO" DataFormatString="{0:c}" HeaderText="Neto" >
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TOTAL" DataFormatString="{0:c}" HeaderText="Total" >
                                        <ItemStyle Font-Bold="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
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
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
