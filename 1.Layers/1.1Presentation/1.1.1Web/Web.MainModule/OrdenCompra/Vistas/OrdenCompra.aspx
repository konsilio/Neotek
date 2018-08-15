<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="Web.MainModule.OrdenCompra.Vistas.OrdenCompra" %>
<%@ Register Src="~/Controles/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="dtp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="OrdenCompra" ContentPlaceHolderID="ctOrdenCompra" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <h2>Compras</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="~/DashBoard/Vista/Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item active">
                                <asp:Label runat="server" ID="lblRuta" Text="Compras / Ordern de Compra /Generar "></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="lblNombreEmpresa" runat="server" Text="Nombre de la empresa"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Número de Requisición:</label>&nbsp;
                                    <asp:Label runat="server" ID="lblNumRequisicion" Text="R000000000"></asp:Label>
                                </div>
                                <div class="input-group">
                                    <label>Número de Requisición:</label>&nbsp;
                                    <asp:Label runat="server" ID="lblNunOrdenCompra" Text="OC00000000"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-12">
                            <div class="form-group form-float">
                                <%--Fecha requerida--%>
                                <label class="card-inside-title">Fecha requerida:</label>
                                <dtp:DateTimePicker ID="txtFechaRequerida" CssClass="dataTxt" runat="server" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-12">
                            <div class="form-group form-float">
                                <%--Solicitante--%>
                                <label class="card-inside-title">Solicitante:</label>
                                <asp:DropDownList runat="server" ID="ddlSolicitante" CssClass="form-control z-index show-tick" data-live-search="true" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-12 form-float">
                            <div class="form-group">
                                <%--Motivo de compra--%>
                                <label class="card-inside-title">Motivo de compra:</label>
                                <asp:TextBox runat="server" ID="txtMotivoCompra" CssClass="form-control" placeholder="Motivo de la compra..." TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-12">
                            <div class="form-group form-float">
                                <%--Se requiere en--%>
                                <label class="card-inside-title">Se requiere en:</label>
                                <asp:TextBox runat="server" ID="txtRequeridoEn" CssClass="form-control" placeholder="Se requiere en..." TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix" runat="server" id="divProveedor">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Proveedor:</label>&nbsp;
                                    <asp:Label runat="server" ID="lblProveedor" Text="Nombre Proveedor"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <dtp:DateTimePicker ID="dtpFechaEntrada" runat="server" Visible="false" />
                        </div>
                    </div>
                    <div class="row clearfix" runat="server" id="divCrearOC">
                        <div class="body table-responsive">
                            <asp:GridView CssClass="table table-hover" runat="server" ID="dgListaproductos" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <%--0 Tipo de compra--%>
                                        <HeaderTemplate>
                                            <b>Tipo</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--1 Producto--%>
                                        <HeaderTemplate>
                                            <b>Producto</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Producto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--2 Cantidad--%>
                                        <HeaderTemplate>
                                            <b>Cantidad</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("CantidadAComprar") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--3 Unidad--%>
                                        <HeaderTemplate>
                                            <b>Unidad</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgUnidad" runat="server" Text='<%# Bind("Unidad") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--4 Aplicacion--%>
                                        <HeaderTemplate>
                                            <b>Aplicación</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--5 Centro costo--%>
                                        <HeaderTemplate>
                                            <b>Centro de costo</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--6 Proveedor--%>
                                        <HeaderTemplate>
                                            Proveedor
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlProveedor" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <%--7 Cuenta contable--%>
                                        <HeaderTemplate>
                                            Cuenta Contable
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlCuentaContable" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--8 Precio--%>
                                        <HeaderTemplate>
                                            Precio
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--9 Descuento--%>
                                        <HeaderTemplate>
                                            %Desc
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" placeholder="%" type="number" CssClass="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--10 IVA--%>
                                        <HeaderTemplate>
                                            %IVA
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" placeholder="%" type="number" CssClass="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--11 IEPS--%>
                                        <HeaderTemplate>
                                            %IEPS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" placeholder="%" type="number" CssClass="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--12 Importe--%>
                                        <HeaderTemplate>
                                            <b>Importe</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text="$ 0" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row clearfix" runat="server" id="divAutorizarOC">
                        <div class="body table-responsive">
                            <asp:GridView CssClass="table table-hover"  runat="server" ID="gvProdcutosOrdenCompra" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <%--0 Tipo de compra--%>
                                        <HeaderTemplate>
                                            <b>Tipo</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--1 Producto--%>
                                        <HeaderTemplate>
                                            <b>Producto</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Producto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--2 Cantidad--%>
                                        <HeaderTemplate>
                                            <b>Cantidad</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("CantidadAComprar") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--3 Unidad--%>
                                        <HeaderTemplate>
                                            <b>Unidad</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgUnidad" runat="server" Text='<%# Bind("Unidad") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--4 Aplicacion--%>
                                        <HeaderTemplate>
                                            <b>Aplicación</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--5 Centro costo--%>
                                        <HeaderTemplate>
                                            <b>Centro de costo</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--6 Cuenta contable--%>
                                        <HeaderTemplate>
                                            Cuenta Contable
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCuentaContable" runat="server" Text='<%# Bind("CuantaContable") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--7 Cantidad--%>
                                        <HeaderTemplate>
                                            Cantidad
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCantidad" Text='<%# Bind("Cantidad") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--8 Precio--%>
                                        <HeaderTemplate>
                                            Precio
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblPrecio" Text='<%# Bind("Precio") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--9 Descuento--%>
                                        <HeaderTemplate>
                                            %Desc
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Descuento") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--10 IVA--%>
                                        <HeaderTemplate>
                                            %IVA
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" placeholder="%" type="number" CssClass="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--11 IEPS--%>
                                        <HeaderTemplate>
                                            %IEPS
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPrecio" runat="server" placeholder="%" type="number" CssClass="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--12 Importe--%>
                                        <HeaderTemplate>
                                            <b>Importe</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row clearfix" runat="server" id="divEntrada">
                        <div class="body table-responsive">
                            <asp:GridView CssClass="table table-hover" runat="server" ID="gvEntrada" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField>
                                        <%--0 Tipo de compra--%>
                                        <HeaderTemplate>
                                            <b>Tipo</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--1 Producto--%>
                                        <HeaderTemplate>
                                            <b>Producto</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Producto") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--2 Cantidad--%>
                                        <HeaderTemplate>
                                            <b>Requeridos</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("CantidadAComprar") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--3 Unidad--%>
                                        <HeaderTemplate>
                                            <b>Unidad</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgUnidad" runat="server" Text='<%# Bind("Unidad") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <%--4 Aplicacion--%>
                                        <HeaderTemplate>
                                            <b>Aplicación</b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Cantidad
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCantidadaMercancia" runat="server" type="number" CssClass="form-control" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-4" align="center">
                            <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                        </div>
                        <div class="col-sm-4" align="center">
                            <asp:Button ID="btnCancelar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Enabled="false" />
                        </div>
                        <div class="col-sm-4" align="center">
                            <asp:Button ID="BtnCrear" CssClass="btn btn-raised btn-primary btn-round" runat="server" OnClientClick="return confirm('¿Esta Seguro?');" Text="Crear" OnClick="BtnCrear_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
