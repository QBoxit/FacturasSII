<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Interfaz/Formularios/Principal.Master" CodeBehind="frmDocmentosRecividos.aspx.vb" Inherits="Factura.frmDocmentosRecividos" 
    title="Página sin título" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <table >
    <tr>
        <td style="width: 459px; height: 9px">
            <strong>DOCUMENTOS RECIBIDOS</strong></td>
    </tr>
                 <tr>
                     <td style="width: 459px; height: 9px">
                     <asp:Panel ID="Panel1" runat="server" BorderStyle="Double">
                <table style="width: 423px">
                    <tr>
                        <td style="width: 65px; text-align: left;">
                            <asp:DropDownList ID="ComboMes" runat="server" Font-Bold="True" Width="115px">
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
                        <td style="width: 56px; text-align: left;">
                            <asp:Label ID="lblAnio" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                        <td style="text-align: left">
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_visualizar.png" />
                            </td>
                    </tr>
                </table>
                <table>
        
                    <tr>
                        <td style="width: 1305px; text-align: left; height: 21px;" >
                            <asp:Label ID="Label1" runat="server" Width="84px"></asp:Label></td>
                        <td style="height: 21px">
                        </td>
                        <td style="text-align: left; height: 21px; width: 527px;" >
                            <asp:Label ID="errorFaltaDatos" runat="server" Font-Bold="True" Width="624px"></asp:Label></td>
                    </tr>
                </table>
                </asp:Panel>
                     </td>
                 </tr>
                 <tr>
                     <td style="width: 459px; height: 9px">
                <asp:GridView ID="gvDocRecv" runat="server" AutoGenerateColumns="False" CellPadding="4"
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
                
</asp:Content>
