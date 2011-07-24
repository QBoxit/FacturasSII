<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Interfaz/Formularios/Principal.Master" CodeBehind="frmLibroVenta.aspx.vb" Inherits="Factura.frmLibroVenta" 
    title="MEGANORTE - Libro De Ventas" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table >
    <tr>
        <td style="width: 459px; height: 9px">
            <strong>LIBRO DE VENTAS</strong></td>
    </tr>
        <tr>
            <td style="width: 739px; height: 9px;">
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Double">
                <table style="width: 423px">
                    <tr>
                        <td style="width: 216px">
                            <asp:DropDownList ID="ComboMes" runat="server" Font-Bold="True" Width="145px">
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
                            <asp:ImageButton ID="btnVerMesLV" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_visualizar.png" />
                        </td>
                        <td>
                            </td>
                        <td style="width: 1px">
                            <strong><span style="text-decoration: underline"></span></strong></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="height: 21px; text-align: left; width: 126px;">
                            <strong><span style="text-decoration: underline">IVA Mes</span></strong></td>
                        <td style="height: 21px">
                            <strong>:</strong></td>
                        <td style="height: 21px; text-align: left">
                            <asp:Label ID="lblivaMes" runat="server" Font-Bold="True" ForeColor="Red" Width="354px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            <strong><span>NETO Mes</span></strong></td>
                        <td>
                            :</td>
                        <td style="text-align: left">
                            <asp:Label ID="lblnetoMes" runat="server" Font-Bold="True" ForeColor="Red" Width="354px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 126px;">
                            <strong><span style="text-decoration: underline">Total Mes</span></strong></td>
                        <td>
                            <strong>:</strong></td>
                        <td style="text-align: left">
                            <asp:Label ID="lbltotalMes" runat="server" Font-Bold="True" ForeColor="Red" Width="354px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 126px; text-align: left; height: 21px;">
                            <asp:Label ID="Label1" runat="server" Width="84px"></asp:Label></td>
                        <td style="height: 21px">
                        </td>
                        <td style="text-align: left; height: 21px;">
                            <asp:Label ID="errorFaltaDatos" runat="server" Font-Bold="True" Width="672px"></asp:Label></td>
                    </tr>
                </table>
                </asp:Panel>
                <br />
                <table style="width: 366px">
                    <tr>
                        <td>
                            <asp:GridView ID="gvLV" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="739px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="RUT" HeaderText="Rut" />
                                    <asp:BoundField DataField="NDOCUMENTO" HeaderText="N&#176; Documento" />
                                    <asp:BoundField DataField="RAZONSOCIAL" HeaderText="R.Social" />
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
