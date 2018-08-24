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
                                <asp:TextBox runat="server" ID="TxtSolicitante" CssClass="form-control" data-live-search="true" Enabled="false" />

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
                                            <label>$ </label>
                                            <asp:Label ID="lbldgImporte" runat="server" CssClass="text-right" Text="0.00" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-sm-right" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row clearfix" runat="server" id="divAutorizarOC">
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
                                        <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
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
                                        <asp:TextBox ID="txtgvDescuento" runat="server" placeholder="%" type="number" CssClass="form-control" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <%--12 Importe--%>
                                    <HeaderTemplate>
                                        <b>Importe</b>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldgImporte" runat="server" Text='<%# Bind("Aplicacion") %>' />
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
                    <div class="col-sm-4 center">
                        <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
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
    <%--Modal de cancelar--%>
    <div class="modal fade" id="ModalMensaje" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalMensajeLabel">Orden de Compra exitosa</h4>
                </div>
                <div class="modal-body">
                    <div class="body">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtMensajeOrdenCompra" runat="server" TextMode="MultiLine" Rows="5" />
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
