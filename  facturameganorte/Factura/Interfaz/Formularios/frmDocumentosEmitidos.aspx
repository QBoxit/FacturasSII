<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Interfaz/Formularios/Principal.Master" CodeBehind="frmDocumentosEmitidos.aspx.vb" Inherits="Factura.frmDocumentosEmitidos" 
    title="Documentos Emitidos"  %>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table >
    <tr>
        <td style="width: 766px; height: 9px">
            <strong>DOCUMENTOS EMITIDOS.</strong></td>
    </tr>
        <tr>
            <td style="width: 766px; height: 10px;">
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
                        <td style="width: 216px; text-align: left;">
                            <asp:DropDownList ID="DropAnio" runat="server" Font-Bold="True" Width="79px">
                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                <asp:ListItem Value="2021">2021</asp:ListItem>
                                <asp:ListItem Value="2022">2022</asp:ListItem>
                                <asp:ListItem Value="2023">2023</asp:ListItem>
                                <asp:ListItem Value="2024">2024</asp:ListItem>
                                <asp:ListItem Value="2025">2025</asp:ListItem>
                                <asp:ListItem Value="2026">2026</asp:ListItem>
                                <asp:ListItem Value="2027">2027</asp:ListItem>
                                <asp:ListItem Value="2028">2028</asp:ListItem>
                                <asp:ListItem Value="2029">2029</asp:ListItem>
                                <asp:ListItem Value="2030">2030</asp:ListItem>
                            </asp:DropDownList>
                            </td>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Interfaz/Imagenes/btn_ver.png" />
                        </td>
                        <td>
                            </td>
                        <td style="width: 1px">
                            </td>
                        <td style="width: 1px">
                            <strong><span style="text-decoration: underline">
                            </span></strong></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="errorFaltaDatos" runat="server" Font-Bold="True" Width="672px"></asp:Label></td>
                    </tr>
                </table>
                    &nbsp;&nbsp;
                    </asp:Panel>
                &nbsp;&nbsp; &nbsp;<br />
                <table style="width: 366px">
                    <tr>
                        <td style="width: 773px">
                            <asp:GridView ID="gvLV" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="760px">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
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
                                    <asp:TemplateField HeaderText="Fact">
  <ItemTemplate>
    <asp:ImageButton ID="VerPdf" runat="server" 
      CommandName="VerPdfButton" 
      CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
      ImageUrl="~/Interfaz/Imagenes/pdf.png"  />
  </ItemTemplate> 
</asp:TemplateField>                                
                                    <asp:TemplateField HeaderText="Cedible">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="VerCedible" runat="server" 
      CommandName="VerPdfCedibleButton" 
      CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
      ImageUrl="~/Interfaz/Imagenes/pdf.png"  />
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
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
