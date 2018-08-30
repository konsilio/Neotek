<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="Web.MainModule.OrdenCompra.Vistas.OrdenCompra" %>

<%@ Register Src="~/Controles/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="dtp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="OrdenCompra" ContentPlaceHolderID="ctOrdenCompra" runat="server">
    <style>
        .calculaPrecio {
        }
    </style>
    <script type="text/javascript">
        var CalcularImporte = function () {
            var index;
            var idtxtPrec;
            var idtxtDesc;
            var idddlIVA;
            var idddlIEPS;
            var precio = 0;
            var descuento = 0;
            var cantidad = 0;
            var iva;
            var ieps;
            var total;
            var subtotal;
            var id = $(".calculaPrecio").context.activeElement.id;
            if (id == "") {
                id = $(".calculaPrecio").context.activeElement.dataset.id;
            }

            if (id.includes("txtPrecio")) {
                idtxtPrec = "#" + id;
                index = id.replace("ctOrdenCompra_dgListaproductos_txtPrecio_", "");
                idtxtDesc = "#ctOrdenCompra_dgListaproductos_txtgvDescuento_" + index;
                idddlIVA = "#ctOrdenCompra_dgListaproductos_ddlGvIVA_" + index;
                idddlIEPS = "#ctOrdenCompra_dgListaproductos_ddlGvIEPS_" + index;
            }
            if (id.includes("txtgvDescuento")) {
                idtxtDesc = "#" + id;
                index = id.replace("ctOrdenCompra_dgListaproductos_txtgvDescuento_", "");
                idtxtPrec = "#ctOrdenCompra_dgListaproductos_txtPrecio_" + index;
                idddlIVA = "#ctOrdenCompra_dgListaproductos_ddlGvIVA_" + index;
                idddlIEPS = "#ctOrdenCompra_dgListaproductos_ddlGvIEPS_" + index;
            }
            if (id.includes("ddlGvIVA")) {
                idddlIVA = "#" + id;
                index = id.replace("ctOrdenCompra_dgListaproductos_ddlGvIVA_", "");
                idtxtPrec = "#ctOrdenCompra_dgListaproductos_txtPrecio_" + index;
                idtxtDesc = "#ctOrdenCompra_dgListaproductos_txtgvDescuento_" + index;
                idddlIEPS = "#ctOrdenCompra_dgListaproductos_ddlGvIEPS_" + index;
            }
            if (id.includes("ddlGvIEPS")) {
                idddlIEPS = "#" + id;
                index = id.replace("ctOrdenCompra_dgListaproductos_ddlGvIEPS_", "");
                idtxtPrec = "#ctOrdenCompra_dgListaproductos_txtPrecio_" + index;
                idtxtDesc = "#ctOrdenCompra_dgListaproductos_txtgvDescuento_" + index;
                idddlIVA = "#ctOrdenCompra_dgListaproductos_ddlGvIVA_" + index;
            }
            precio = $(idtxtPrec).val();
            cantidad = $("#ctOrdenCompra_dgListaproductos_lbldgCantidad_" + index).html();
            descuento = ((precio * cantidad) * ($(idtxtDesc).val() / 100));
            subtotal = (precio * cantidad) - (descuento);
            iva = ((subtotal) * ($(idddlIVA + " option:selected").val().replace("IVA", "") / 100));
            ieps = ((subtotal) * ($(idddlIEPS + " option:selected").val().replace("IEPS", "") / 100));
            total = subtotal + iva + ieps;
            $("#ctOrdenCompra_dgListaproductos_lbldgImporte_" + index).html(total.format("N2"));
        }
    </script>
    <script type="text/javascript">
        $("#lblMensajeOrdenCompra").html('new-label').trigger('labelchanged')
        $("#lblMensajeOrdenCompra").on('labelchanged', function () {
            ocument.getElementById('btnMensaje').clic;
        })
    </script>
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <h2>Compras</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="~/DashBoard/Vista/Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item active">
                                <asp:Label runat="server" ID="lblRuta" Text="Compras / Ordern de Compra / Nueva "></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="card">
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:Label ID="lblNombreEmpresa" runat="server" Text="Nombre de la empresa"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <b>Número de Requisición:</b>&nbsp;
                                    <asp:Label runat="server" ID="lblNumRequisicion" Text="R000000000"></asp:Label>
                                </div>
                                <div class="input-group">
                                    <b>Número de Requisición:</b>&nbsp;
                                    <asp:Label runat="server" ID="lblNunOrdenCompra" Text="OC00000000" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-12">
                            <div class="form-group form-float">
                                <%--Fecha requerida--%>
                                <b>Fecha requerida:</b>
                                <dtp:DateTimePicker ID="txtFechaRequerida" CssClass="dataTxt" runat="server" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-12">
                            <div class="form-group form-float">
                                <%--Solicitante--%>
                                <b>Solicitante:</b>
                                <asp:TextBox runat="server" ID="TxtSolicitante" CssClass="form-control" data-live-search="true" Enabled="false" />

                            </div>
                        </div>
                        <div class="col-lg-3 col-md-12 form-float">
                            <div class="form-group">
                                <%--Motivo de compra--%>
                                <b>Motivo de compra:</b>
                                <asp:TextBox runat="server" ID="txtMotivoCompra" CssClass="form-control" placeholder="Motivo de la compra..." TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-12">
                            <div class="form-group form-float">
                                <%--Se requiere en--%>
                                <b>Se requiere en:</b>
                                <asp:TextBox runat="server" ID="txtRequeridoEn" CssClass="form-control" placeholder="Se requiere en..." TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix" runat="server" id="divProveedor">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <b>Proveedor:</b>&nbsp;
                                    <asp:Label runat="server" ID="lblProveedor" Text="Nombre Proveedor"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <dtp:DateTimePicker ID="dtpFechaEntrada" runat="server" Visible="false" />
                        </div>
                    </div>
                    <div class="body">
                        <div class="row clearfix" runat="server" id="divCrearOC">
                            <div class="table-responsive">
                                <asp:GridView CssClass="table table-hover" runat="server" OnRowEditing="dgListaproductos_RowEditing" ID="dgListaproductos" Width="100%" AutoGenerateColumns="false" OnRowDataBound="dgListaproductos_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <%--0 Tipo de compra--%>
                                            <HeaderTemplate>
                                                <b>Tipo</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-sm-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--1 Producto--%>
                                            <HeaderTemplate>
                                                <b>Producto</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Producto") %>' />
                                                <asp:Label ID="lblidProducto" runat="server" Text='<%# Bind("IdProducto") %>' Visible="false" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-sm-center" />
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
                                            <ItemStyle CssClass="text-sm-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--4 Aplicacion--%>
                                            <HeaderTemplate>
                                                <b>Aplicación</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-sm-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--5 Centro costo--%>
                                            <HeaderTemplate>
                                                <b>Centro de costo</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                                <asp:Label ID="lbldgidCentroCosto" runat="server" Text='<%# Bind("IdCentroCosto") %>' Visible="false" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-sm-center" />
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
                                                <%--<input type="number" name="txtgvPrecio" runat="server" id="txtgvPrecio" class="form-control" onchange="CalcularImporte(<%=dgListaproductos.ClientID%>, this.id)" />--%>
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control calculaPrecio" Width="100px" TextMode="Number" onchange="javascript: CalcularImporte();" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--9 Descuento--%>
                                            <HeaderTemplate>
                                                %Desc
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDescuento" runat="server" Width="100px" placeholder="%" type="number" CssClass="form-control calculaPrecio" onchange="javascript: CalcularImporte();" />
                                                <%--<input type="number" name="txtgvDescuento"  class="form-control" Width="100px" placeholder="%" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--10 IVA--%>
                                            <HeaderTemplate>
                                                %IVA
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlGvIVA" runat="server" Width="100px" placeholder="%" CssClass="form-control calculaPrecio" CommandName="ddl_IVA" onchange="javascript: CalcularImporte();" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--11 IEPS--%>
                                            <HeaderTemplate>
                                                %IEPS
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlGvIEPS" runat="server" Width="100px" placeholder="%" CssClass="form-control calculaPrecio" onchange="javascript: CalcularImporte();" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--12 Importe--%>
                                            <HeaderTemplate>
                                                <b>Importe</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <label>
                                                    $ </b>
                                            <asp:Label ID="lbldgImporte" runat="server" CssClass="text-right" Text="0.00" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-sm-right" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row clearfix" runat="server" id="divAutorizarOC" visible="false">
                            <div class="body table-responsive">
                                <asp:GridView CssClass="table table-hover" runat="server" ID="gvProdcutosOrdenCompra" Width="100%" AutoGenerateColumns="false">
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
                                            <%--2 Requeridos--%>
                                            <HeaderTemplate>
                                                <b>Requeridos</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("Cantidad") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--3 Unidad--%>
                                            <HeaderTemplate>
                                                <b>Unidad</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgUnidad" runat="server" Text='<%# Bind("UnidadMedida") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--4 Detalle--%>
                                            <HeaderTemplate>
                                                <b>Detalle</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Descripcion") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--5 Centro costo--%>
                                            <HeaderTemplate>
                                                <b>Centro de costo</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--6 Cuenta contable--%>
                                            <HeaderTemplate>
                                                Cuenta Contable
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCuentaContable" runat="server" Text='<%# Bind("CuentaContable") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--7 Cantidad--%>
                                            <HeaderTemplate>
                                                Cantidad
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblgvAutoCantidad" Text='<%# Bind("Cantidad") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--8 Precio--%>
                                            <HeaderTemplate>
                                                Precio
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblgvAutoPrecio" Text='<%# Bind("Precio") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--9 Descuento--%>
                                            <HeaderTemplate>
                                                %Desc
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAutoDescuento" runat="server" Text='<%# Bind("Descuento") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--10 IVA--%>
                                            <HeaderTemplate>
                                                %IVA
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAutoIVA" runat="server" Text='<%# Bind("IVA") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--11 IEPS--%>
                                            <HeaderTemplate>
                                                %IEPS
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAutoIEPS" runat="server" Text='<%# Bind("IEPS") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <%--12 Importe--%>
                                            <HeaderTemplate>
                                                <b>Importe</b>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldgImporte" runat="server" Text='<%# Bind("Importe") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row clearfix" runat="server" id="divEntrada" visible="false">
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
                    </div>
                </div>
                <div class="card" id="divGvvComplementoGas" runat="server" visible="false">
                    <div class="body">
                        <div class="row clearfix">
                            <div class="row clearfix">
                                <div class="table-responsive">
                                    <asp:GridView CssClass="table table-hover" runat="server" OnRowEditing="dgListaproductos_RowEditing" ID="gvComplementoGas" Width="100%" AutoGenerateColumns="false" OnRowDataBound="dgListaproductos_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <%--0 Tipo de compra--%>
                                                <HeaderTemplate>
                                                    <b>Tipo</b>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto") %>' />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="text-sm-center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <%--1 Producto--%>
                                                <HeaderTemplate>
                                                    <b>Producto</b>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Producto") %>' />
                                                    <asp:Label ID="lblidProducto" runat="server" Text='<%# Bind("IdProducto") %>' Visible="false" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="text-sm-center" />
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
                                                <ItemStyle CssClass="text-sm-center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <%--4 Aplicacion--%>
                                                <HeaderTemplate>
                                                    <b>Aplicación</b>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="text-sm-center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <%--5 Centro costo--%>
                                                <HeaderTemplate>
                                                    <b>Centro de costo</b>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                                    <asp:Label ID="lbldgidCentroCosto" runat="server" Text='<%# Bind("IdCentroCosto") %>' Visible="false" />
                                                </ItemTemplate>
                                                <ItemStyle CssClass="text-sm-center" />
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
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card" runat="server" id="divDatosComplementoGas">
                    <div class="body">
                        <%--Campos para complemento de gas--%>
                        <div class="panel-group" id="accordion_1" role="tablist" aria-multiselectable="true">
                            <%--Papeleta--%>
                            <div class="panel panel-primary">
                                <div class="panel-heading" role="tab" id="heading_Papeleta">
                                    <h4 class="panel-title"><a role="button" data-toggle="collapse" data-parent="#accordion_1" href="#collapse_Papeleta" aria-expanded="true" aria-controls="collapse_Papeleta">Papeleta</a></h4>
                                </div>
                                <div id="collapse_Papeleta" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="heading_Papeleta">
                                    <div class="panel-body">
                                        <div class="row clearfix">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="dtpFechaPapeleta">Fecha papeleta:</label>
                                                    <dtp:DateTimePicker ID="dtpFechaPapeleta" CssClass="dataTxt form-control-sm" runat="server" Enabled="True" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="dtpFechaEmbarque">Fecha embarque:</label>
                                                    <dtp:DateTimePicker ID="dtpFechaEmbarque" CssClass="dataTxt form-control-sm" runat="server" Enabled="True" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label for="">Número embarque</label>
                                                    <asp:TextBox ID="txtNumEmbarque" CssClass="form-control" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-4">
                                                <label>% Magnatel tractor (papeleta)</label>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtPorcentajeMagnatelPapeleta" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr>
                                        <div class="row clearfix">
                                            <h6>Tractor</h6>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Placa tractor:</label>
                                                    <asp:TextBox ID="txtPlacaTractor" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Nombre operador:</label>
                                                    <asp:TextBox ID="txtNombreOperador" runat="server" CssClass="form-control" />
                                                </div>

                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Presión tanque tractor:</label>
                                                    <asp:TextBox ID="txtPrecsionTantqueTractor" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Número tanque tractor:</label>
                                                    <asp:TextBox ID="txtNumeroTanqueTractor" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Capasidad (Ltr) tanque tractor:</label>
                                                    <asp:TextBox ID="txtCapasidadLtrTanqueTractor" runat="server" CssClass="form-control " />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>% Magnatel tractor (Ocultar)</label>
                                                    <asp:TextBox ID="txtPorcentajeMagnatelTractorOcultar" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr>
                                        <div class="row clearfix">
                                            <h6>Descarga</h6>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Fecha descarga:</label>
                                                    <dtp:DateTimePicker ID="dtpFechaDescarga" CssClass="dataTxt" runat="server" Enabled="True" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-3 content-center">
                                                <div class="form-group">
                                                    <label>% Magnatel Tractor</label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Inicial</label>
                                                    <asp:TextBox ID="txtMagnatelTractorInicial" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Final</label>
                                                    <asp:TextBox ID="txtMagnatelTractorFinal" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-3 content-center">
                                                <div class="checkbox">
                                                    <input id="chBoxAlmacenPrestado" runat="server" type="checkbox">
                                                    <label for="chBoxAlmacenPrestado">¿Almacen alterno?</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-3 content-center">
                                                <div class="form-group">
                                                    <label>% Magnatel Almacen</label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Inicial</label>
                                                    <asp:TextBox ID="txtMagnatelAlmacenInicial" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Final</label>
                                                    <asp:TextBox ID="txtMagnatelAlmacenFinal" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr>
                                        <div class="row clearfix">
                                            <h6>Kilos Finales</h6>
                                        </div>
                                        <div class="row clearfix">
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Papeleta</label>
                                                    <asp:TextBox ID="txtKFinalesPapeleta" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Descargados</label>
                                                    <asp:TextBox ID="txtKFinalesDescargados" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>Diferencia</label>
                                                    <asp:TextBox ID="txtKFinalesDiferencia" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr>
                                        <div class="row clearfix">
                                            <div class="col-md-2">
                                                <asp:Button ID="btnGuardarDatosPapeleta" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Guardar" OnClick="btnGuardarDatosPapeleta_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--Costos--%>
                            <div class="panel panel-primary">
                                <div class="panel-heading" role="tab" id="headingCostos">
                                    <h4 class="panel-title"><a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion_1" href="#collapseCostos" aria-expanded="false"
                                        aria-controls="collapseCostos">Costos</a></h4>
                                </div>
                                <div id="collapseCostos" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingCostos">
                                    <div class="panel-body">
                                        <div class="row">
                                            <%--Pago al expedidor de gas--%>
                                            <div class="col-md-6 blockquote blockquote-info">
                                                <div class="row clearfix">
                                                    <b>Pago al expedidor de gas </b>
                                                </div>
                                                <hr>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtReferenciaPormedioMensualGallon">Referencia promedio mensual MB Non Tet Propano Gallon</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtReferenciaPormedioMensualGallon" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="txtReferenciaPormedioMensualGallon">(En dolares)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtTarifaServicioGallon">Tarifa de Servicio por gallon</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTarifaServicioGallon" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="txtTarifaServicioGallon">(En dolares)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtTipoCambioDOF">Tipo de cambio DOF:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtTipoCambioDOF" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="form-control-label">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="lbPlPrecioPorGalon">Precio por galon</label>
                                                    </div>
                                                    <div class="col-md-4 text-right">
                                                        <div class="form-group">
                                                            <div class="form-group">
                                                                <asp:Label ID="lbPlPrecioPorGalon" runat="server" Text="$ 0.00" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="lbPlPrecioPorGalon">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-4 form-control-label">
                                                        <label for="txtFactorConvercionGaL">Factor de convercion Galones a litros:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFactorConvercionGaL" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="lblImporteLitros">Importe en Litros</label>
                                                    </div>
                                                    <div class="col-md-4 text-right">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblImporteLitros" runat="server" Text="$ 0.00" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="lblImporteLitros">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-4 form-control-label">
                                                        <label for="txtFactorConvercionaKg">Factor de conversion a Kilogramos:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFactorConvercionaKg" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtPVMP">PVPM($/Kg) CON DESCUENTO:</label>
                                                    </div>
                                                    <div class="col-md-4 text-right">
                                                        <div class="form-group">
                                                            <asp:Label ID="txtPVMP" runat="server" Text="$ 0.00" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="txtPVMP">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-4 form-control-label">
                                                        <label for="ddlIVACostos">IVA</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlIVACostos" runat="server" CssClass="form-control z-index">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtPrecioConIVA">PRECIO DE VENTA CON IVA:</label>
                                                    </div>
                                                    <div class="col-md-4 text-right">
                                                        <div class="form-group">
                                                            <asp:Label ID="txtPrecioConIVA" runat="server" Text="$ 0.00" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="txtPrecioConIVA">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <hr>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="btnSolicitarPagoExpedidor">IMPORTE A PAGAR</label>
                                                    </div>
                                                    <div class="col-md-4 form-control-label text-right">
                                                        <asp:Label ID="lblImporteAPagar" runat="server" Text="$ 0.00" />
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="lblImporteAPagar">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnguardarDatosExpedidor" CssClass="btn btn-raised btn-primary btn-round" Text="Guardar" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnSolicitarPagoExpedidor" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Solisitar pago" OnClick="btnSolicitarPagoExpedidor_Click" />
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <%--pago al porteador--%>
                                            <div class="col-md-6 blockquote blockquote-info">
                                                <div class="row clearfix">
                                                    <b>Pago al porteador de gas</b>
                                                </div>
                                                <hr>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtFactorConversion">Factor de conversión</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtFactorConversion" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="lblPrecioTransporte">Precio del transporte:</label>
                                                    </div>
                                                    <div class="col-md-4 text-right">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblPrecioTransporte" runat="server" Text="$ 0.00" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3  form-control-label">
                                                        <label for="lblPrecioTransporte">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="txtCasetas">Casetas:</label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <asp:TextBox ID="txtCasetas" runat="server" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="txtCasetas">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="lblSubtotal">Subtotal:</label>
                                                    </div>
                                                    <div class="col-md-4 text-right">
                                                        <div class="form-group">
                                                            <asp:Label ID="lblSubtotal" runat="server" Text="$ 0.00" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="lblSubtotal">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-4 form-control-label">
                                                        <label for="ddlIvaPorteador">IVA</label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="ddlIvaPorteador" runat="server" CssClass="form-control z-index">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr>
                                                <div class="row clearfix">
                                                    <div class="col-md-5 form-control-label">
                                                        <label for="btnSolicitarPaogPorteador">IMPORTE A PAGAR</label>
                                                    </div>
                                                    <div class="col-md-4 form-control-label text-right">
                                                        <asp:Label ID="ImportePagarPorteador" runat="server" Text="$ 00.0" />
                                                    </div>
                                                    <div class="col-md-3 form-control-label">
                                                        <label for="btnSolicitarPaogPorteador">(En pesos)</label>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnGuardarDatosPorteador" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Guardar" OnClick="btnSolicitarPaogPorteador_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button ID="btnSolicitarPaogPorteador" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Solicitar Pago" OnClick="btnSolicitarPaogPorteador_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card" runat="server" id="divImagenes" visible="false">
                    <div class="body">
                        <div id="aniimated-thumbnials" class="list-unstyled row clearfix">
                            <!-- Imagenes -->
                            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 m-b-20">
                                <a href="../../assets/images/image-gallery/1.jpg">
                                    <img class="img-fluid img-thumbnail" src="../../assets/images/image-gallery/1.jpg" alt="">
                                </a>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 m-b-20">
                                <a href="../../assets/images/image-gallery/2.jpg">
                                    <img class="img-fluid img-thumbnail" src="../../assets/images/image-gallery/2.jpg" alt="">
                                </a>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 m-b-20">
                                <a href="../../assets/images/image-gallery/3.jpg">
                                    <img class="img-fluid img-thumbnail" src="../../assets/images/image-gallery/3.jpg" alt="">
                                </a>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-12 m-b-20">
                                <a href="../../assets/images/image-gallery/4.jpg">
                                    <img class="img-fluid img-thumbnail" src="../../assets/images/image-gallery/4.jpg" alt="">
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-sm-4 center">
                        <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                        <a href="#ModalMensaje" data-toggle="modal" id="btnMensaje" runat="server" data-target="#ModalMensaje" class="btn btn-raised btn-primary btn-round">Ver</a>
                    </div>
                    <div class="col-sm-4 center">
                        <a href="#ModalCancelar" data-toggle="modal" id="btnCancel" runat="server" data-target="#ModalCancelar" class="btn btn-raised btn-primary btn-round disabled">Cancelar
                        </a>
                    </div>
                    <div class="col-sm-4 center">
                        <a href="#ModalConfirmacion" data-toggle="modal" id="btnok" runat="server" data-target="#ModalConfirmacion" class="btn btn-raised btn-primary btn-round">
                            <asp:Label ID="lblbtnCrear" runat="server" Text="Crear" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <%--Modal confirmacion--%>
    <div class="modal fade" id="ModalConfirmacion" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalConfirmacionLabel">¿Esta seguro?</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnCrear" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Crear" OnClick="BtnCrear_Click" />

                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <%--Modal de cancelar--%>
    <div class="modal fade" id="ModalCancelar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalCancelarLabel">Motivo de cancelcion</h4>
                </div>
                <div class="modal-body">
                    <div class="body">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtMotivoCancela" TextMode="MultiLine" CssClass="form-control tex" runat="server" Rows="5" placeholder="Cual es el motivo de la cancelacion..."></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancelar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Enabled="false" />
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Regresar</button>
                </div>
            </div>
        </div>
    </div>
    <%--Modal de mensaje--%>
    <div class="modal fade" id="ModalMensaje" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalMensajeLabel">Orden de Compra exitosa</h4>
                </div>
                <div class="modal-body">
                    <div class="body">
                        <div class="col-sm-12">
                            <asp:Label ID="lblMensajeOrdenCompra" runat="server" TextMode="MultiLine" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="Button1" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Crear" OnClick="BtnCrear_Click" />
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Regresar</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/OrdenCompraJS.js"></script>
    <script src="https://www.gstatic.com/firebasejs/5.4.1/firebase.js"></script>
    <script>
        // Initialize Firebase
        var config = {
            apiKey: "AIzaSyAmt7VWV6PGYouqEHEFi2n6sJHmJUuwzRk",
            authDomain: "sagas-23404.firebaseapp.com",
            databaseURL: "https://sagas-23404.firebaseio.com",
            projectId: "sagas-23404",
            storageBucket: "sagas-23404.appspot.com",
            messagingSenderId: "744018811790"
        };
        firebase.initializeApp(config);
    </script>
</asp:Content>
