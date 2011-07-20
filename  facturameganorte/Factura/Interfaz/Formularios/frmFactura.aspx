<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Interfaz/Formularios/Principal.Master" CodeBehind="frmFactura.aspx.vb" Inherits="Factura.frmFactura" 
    title="MEGANORTE - Factura" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>
    <%@ Assembly name="System.Data" %>
<%@ Assembly name="Npgsql" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script type="text/javascript">
      function ValidNum(e) {
      var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
      return ((tecla > 47 && tecla < 58) || tecla == 46);
                           }
  </script>
                          
     <table style="width: 688px; height: 256px">
        <tr>
            <td align="center" colspan="2" valign="middle">
                &nbsp;<asp:Label ID="lblGuia" runat="server" Text="Factura" Font-Bold="True" Font-Size="16pt"></asp:Label></td>
        </tr>
        <tr>
            <td align="center" colspan="2" valign="middle" style="text-align: left">
                <table style="width: 217px">
                    <tr>
                        <td style="width: 213px; height: 21px; text-align: left">
                            <strong>DATOS CLIENTE:</strong></td>
                    </tr>
                </table>
            <br />
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Double">
                    <strong>
                    <br />
                    </strong>
                    <table style="width: 181px">
                        <tr>
                            <td style="text-align: left; width: 95px;">
                <asp:Label ID="lblRut" runat="server" Text="R.U.T. :"></asp:Label>
                            </td>
                            <td style="text-align: left">
                <asp:TextBox ID="txtRut" runat="server" Width="301px" TabIndex="5" Font-Bold="True" AutoPostBack ></asp:TextBox></td>
                            <td style="width: 44px; text-align: left;">
                <asp:Label ID="lblFono" runat="server" Text="Fono:"></asp:Label></td>
                            <td style="text-align: left">
                <asp:TextBox ID="txtFono" runat="server" Width="300px" TabIndex="2" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: left; height: 26px; width: 95px;">
                <asp:Label ID="lblDireccion" runat="server" Text="Dirección:" Width="9px"></asp:Label></td>
                            <td style="text-align: left; height: 26px;">
                <asp:TextBox ID="txtDireccion" runat="server" Width="300px" TabIndex="1" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 44px; text-align: left; height: 26px;">
                <asp:Label ID="lblGiro" runat="server" Text="Giro:"></asp:Label></td>
                            <td style="text-align: left; height: 26px;">
                <asp:TextBox ID="txtGiro" runat="server" Width="300px" TabIndex="6" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 95px;">
                <asp:Label ID="lblRazon" runat="server" Text="Razón Social:" Width="88px"></asp:Label></td>
                            <td style="text-align: left">
                <asp:TextBox ID="txtRazon" runat="server" Width="301px" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 44px; text-align: left;">
                <asp:Label ID="lblComuna" runat="server" Text="Comuna:"></asp:Label></td>
                            <td style="text-align: left">
                <asp:TextBox ID="txtComuna" runat="server" Width="300px" TabIndex="3" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px; text-align: left">
                            </td>
                            <td style="text-align: left">
                            </td>
                            <td style="width: 44px; text-align: left">
                                Ciudad:</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCiudad" runat="server" Font-Bold="True" Width="298px"></asp:TextBox></td>
                        </tr>
                    </table>
                    &nbsp;<asp:Label ID="avisorut" runat="server" BorderColor="Gray" Font-Bold="True"
                        ForeColor="Red" Text="Nota: Ingresar rut sin puntos ni guion" Width="758px"></asp:Label>&nbsp;<table
                    style="width: 143px">
                    <tr>
                        <td style="height: 26px">
                            &nbsp;<asp:ImageButton ID="aduser" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_ingresar.png" />
                        </td>
                        <td style="width: 624px; height: 26px">
                            <asp:ImageButton ID="btnlimpiar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_limpiar.png" />
                        </td>
                        <td style="width: 72px; height: 26px">
                            <asp:ImageButton ID="updateUser" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_actualizar.png" />
                        </td>
              
                    </tr>
                </table>
                    &nbsp;
                    &nbsp;&nbsp;
                </asp:Panel>
                </td>
        </tr>
        <tr>
            <td align="center" style="width: 76px; height: 15px;" valign="middle">
                <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
        </tr>
        <tr>
            <td align="left" valign="bottom" colspan="2" rowspan="1" id="LabelMensaje">
                <table style="width: 217px">
                    <tr>
                        <td style="width: 213px; height: 21px; text-align: left">
                            <strong>DATOS FACTURA:</strong></td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="Panel2" runat="server" BorderStyle="Double">
                    <table>
                        <tr>
                            <td style="width: 21px">
                <asp:Label ID="lblItem" runat="server" Text="Item:"></asp:Label></td>
                            <td style="width: 303px">
                <asp:TextBox ID="txtItem" runat="server" TabIndex="9" Width="300px" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 1px">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label></td>
                            <td style="width: 3px">
                                <BDP:BasicDatePicker ID="txtfecha" runat="server" ReadOnly="True" DateFormat="dd/MM/yyyy" Font-Bold="True">
                                </BDP:BasicDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 21px; height: 38px;">
                <asp:Label ID="lblCodigo" runat="server" Text="Código:"></asp:Label></td>
                            <td style="width: 303px; height: 38px;">
                <asp:TextBox ID="txtCodigo" runat="server" TabIndex="10" Width="300px" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 1px; height: 38px;">
                                <asp:Label ID="lblvendedor" runat="server" Text="Vendedor:"></asp:Label></td>
                            <td style="width: 3px; height: 38px;">
                                <asp:TextBox ID="TextVendedor" runat="server" Width="240px" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                <asp:Label ID="lblDetalle" runat="server" Text="Detalle:"></asp:Label></td>
                            <td style="width: 303px">
                <asp:TextBox ID="txtDetalle" runat="server" TabIndex="11" Width="300px" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 1px">
                                <asp:Label ID="lblordencompra" runat="server" Text="Orden de Compra" Width="112px"></asp:Label></td>
                            <td style="width: 3px">
                                <asp:TextBox ID="TextOrdenCompra" runat="server" Width="240px" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                        
                     
                            <td style="width: 21px">
                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad:"></asp:Label></td>
                            <td style="width: 303px">
                <asp:TextBox ID="txtCantidad" runat="server" TabIndex="12" Width="300px" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 1px">
                                <asp:Label ID="lblgia" runat="server" Text="Guia Despacho" Width="104px"></asp:Label></td>
                            <td style="width: 3px">
                                <asp:TextBox ID="TextGuiaDespacho" runat="server" Width="240px" Font-Bold="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                <asp:Label ID="lblPrecioUnitario" runat="server" Text="Precio Unitario:" Width="96px"></asp:Label></td>
                            <td style="width: 303px">
                <asp:TextBox ID="txtPrecioUnitario" runat="server" TabIndex="13" Width="300px" Font-Bold="True"></asp:TextBox></td>
                            <td style="width: 1px; text-align: right">
                                <asp:Label ID="lblcondicion" runat="server" Text="Condición Venta" Width="104px"></asp:Label></td>
                            <td style="width: 3px">
                                <asp:DropDownList ID="DropCondVenta" runat="server" Width="80px" Font-Bold="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 21px">
                            </td>
                            <td style="width: 303px">
                            </td>
                            <td style="width: 1px; text-align: right">
                                <asp:Label ID="Label1" runat="server" Text="Iva" Width="38px"></asp:Label></td>
                            <td style="width: 3px">
                                <asp:DropDownList ID="DropIva" runat="server" Font-Bold="True" Width="80px">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                    &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <table>
                        <tr>
                            <td>
                <asp:ImageButton ID="imgbtnAgregar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_agregar.png" Width="101px" Height="31px" /></td>
                            <td>
                            <asp:Label ID="ingresado" runat="server" BackColor="Transparent" BorderColor="Black" ForeColor="Red" Height="21px" Width="653px"></asp:Label></td>
                        </tr>
                    </table>
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                    <hr style="width: 779px; height: 4px" />
                    &nbsp;&nbsp;<table style="width: 254px">
                        <tr>
                            <td style="text-align: left">
                                <strong>OBTENER FACTURA:</strong></td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    <table style="width: 381px">
                        <tr>
                            <td style="height: 35px">
                <asp:ImageButton ID="imgbtnGuardar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_guardar.png" TabIndex="15" /></td>
                            <td style="height: 35px">
                <asp:ImageButton ID="imgbtnLimpiar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_limpiar.png" /></td>
                            <td style="width: 119px; height: 35px;">
                                <asp:ImageButton ID="VistaPrevia" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_previo.png" />
                            </td>
                            <td style="width: 150px; height: 35px;">
                <asp:ImageButton ID="imgbtnExportar" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_EnviarSII.png" TabIndex="16" />
                            </td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </asp:Panel>
                &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                <br />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" valign="middle">
                &nbsp; &nbsp;&nbsp;
                &nbsp;&nbsp;
                <asp:GridView ID="gvwDatos" datakeynames="ITEM" runat="server" CellPadding="4" ForeColor="#333333"
                    GridLines="None" AutoGenerateColumns="False" Width="784px" >
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="ITEM" HeaderText="Item" />
                        <asp:BoundField DataField="CODIGO" HeaderText="C&#243;digo" />
                        <asp:BoundField DataField="DETALLE" HeaderText="Detalle" />
                        <asp:BoundField DataField="CANTIDAD" HeaderText="Cantidad" />
                        <asp:BoundField DataField="PRECIOUNITARIO" HeaderText="Precio Unitario" />
                        <asp:CommandField ButtonType="Button" DeleteText="Eliminar" EditText="Editar" ShowDeleteButton="True"
                            ShowEditButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="height: 22px" valign="middle">
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    &nbsp;
    <CR:CrystalReportViewer ID="CrView" runat="server" AutoDataBind="true" DisplayGroupTree="False"
        DisplayToolbar="False" />
</asp:Content>
