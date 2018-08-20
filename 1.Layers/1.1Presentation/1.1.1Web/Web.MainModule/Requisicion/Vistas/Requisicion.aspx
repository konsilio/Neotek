﻿<%@ Page Title="" Language="C#" EnableViewState="true" EnableViewStateMac="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requisicion.aspx.cs" Inherits="Web.MainModule.Requisicion.Vista.Requisicion" %>

<%@ Register Src="~/Controles/DateTimePicker.ascx" TagName="DateTimePicker" TagPrefix="dtp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="ContentRequisicion" ContentPlaceHolderID="ctRequisicion" runat="server">
    <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
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
                                <asp:Label runat="server" ID="lblRuta" Text="Requisición / Nueva "></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <div class="body" id="divNoRequi" runat="server" visible="false">
                        <div class="alert alert-success">
                            <strong>
                                <asp:Label ID="lblNoRequisicion" runat="server" Text="" /></strong>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEmpresas" CssClass="form-control show-tick" runat="server" Visible="true" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <label>Número de Requisición:</label>&nbsp;
                                                    <asp:Label runat="server" ID="lblIdRequisicion" Text="R000000000"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row clearfix z-index" runat="server" id="divCamposRequi">
                                    <div class="col-lg-3">
                                        <%--Fecha requerida--%>
                                        <b>Fecha requerida:</b>
                                        <dtp:DateTimePicker ID="txtFechaRequerida" CssClass="dataTxt z-index" runat="server" />
                                        <asp:Label runat="server" ID="reqFecha" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-3">
                                        <%--Solicitante--%>
                                        <b>Solicitante:</b>
                                        <asp:DropDownList runat="server" ID="ddlSolicitante" CssClass="form-control z-index show-tick" data-live-search="true" data-show-subtext="true" OnSelectedIndexChanged="ddlSolicitante_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="reqSol" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-3">
                                        <%--Motivo de compra--%>
                                        <b>Motivo de compra:</b>
                                        <asp:TextBox runat="server" ID="txtMotivoCompra" CssClass="form-control" placeholder="Motivo de la compra..." TextMode="MultiLine" OnTextChanged="txtMotivoCompra_TextChanged"></asp:TextBox>
                                        <asp:Label runat="server" ID="reqMotivo" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-3">
                                        <%--Se requiere en--%>
                                        <b>Se requiere en:</b>
                                        <asp:TextBox runat="server" ID="txtRequeridoEn" CssClass="form-control" placeholder="Se requiere en..." TextMode="MultiLine" OnTextChanged="txtRequeridoEn_TextChanged"></asp:TextBox>
                                        <asp:Label runat="server" ID="reqReq" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div id="divCampos" runat="server" visible="false" class="container">
                                        <div class="alert alert-danger">
                                            <div class="alert-icon">
                                                <i class="zmdi zmdi-block"></i>
                                            </div>
                                            <strong>
                                                <asp:Label ID="lblErrorCampos" runat="server" Text="" /></strong>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix" runat="server" id="divDatos1">
                                    <div class="col-lg-2">
                                        <%--Tipo de compra--%>
                                        <b>Tipo de compra:</b>
                                        <asp:DropDownList ID="ddlTipoCompra" runat="server" CssClass="form-control z-index show-tick" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCompra_SelectedIndexChanged">
                                            <asp:ListItem Value="2" Text="Producto"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Servicio"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="reqTipo" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-4">
                                        <%--Producto--%>
                                        <b>Producto:</b>
                                        <asp:DropDownList runat="server" ID="ddlProdcutos" AutoPostBack="true" CssClass="form-control show-tick" data-live-search="true" OnSelectedIndexChanged="ddlProdcutos_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="reqProd" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-2">
                                        <%--Cantidad--%>
                                        <b>
                                            <asp:Label ID="lblCantidadUnidad" runat="server" Text="Cantidad: " /></b>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtCantidad" runat="server" placeholder="0" TextMode="Number" CssClass="form-control" />
                                        </div>
                                        <asp:Label runat="server" ID="reqCant" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-3">
                                        <%--Centro de costo--%>
                                        <b>Centro de costo:</b>
                                        <asp:DropDownList runat="server" ID="ddlCentroCostos" CssClass="form-control z-index show-tick" data-live-search="true"></asp:DropDownList>
                                        <asp:Label runat="server" ID="reqCC" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                </div>
                                <div class="row clearfix js-sweetalert" id="divDatos2" runat="server">
                                    <div class="col-lg-8">
                                        <%--Aplicacion--%>
                                        <b>Aplicación:</b>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDetalle" runat="server" placeholder="Comentarios..." CssClass="form-control" />
                                        </div>
                                        <asp:Label runat="server" ID="reqApli" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-2 col-md-6">
                                        <label>&nbsp; </label>
                                        <div class="form-group">
                                            <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-danger btn-simple btn-round btn-sm" data-type="ajax-loader" OnClick="btnAgregar_Click">
                                            <i class="material-icons">add</i>    
                                            <span class="icon-name">Agregar</span>                                                                                    
                                            </asp:LinkButton>
                                        </div>

                                        <%--<asp:Button ID="btnAgregar" CssClass="btn btn-raised btn-primary waves-effect btn-round" data-type="ajax-loader" OnClick="btnAgregar_Click" Text="Agregar" />--%>
                                    </div>
                                </div>
                                <div id="DivCamposPord" runat="server" visible="false" class="container">
                                    <div class="alert alert-danger">
                                        <div class="alert-icon">
                                            <i class="zmdi zmdi-block"></i>
                                        </div>
                                        <strong>
                                            <asp:Label ID="lblErrorPord" runat="server" Text="" /></strong>
                                    </div>
                                </div>
                                <!--Grid productos agregados nueva requisicion-->
                                <div class="row clearfix">
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table table-hover" OnRowCommand="dgListaproductos_RowCommand" runat="server" ID="dgListaproductos" Width="100%" AutoGenerateColumns="false" EmptyDataText="No se han agregado productos y/o servicios a la requisicion">
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
                                                        <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("Cantidad") %>' />
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
                                                    <%--5 Centro de costo--%>
                                                    <HeaderTemplate>
                                                        <b>Centro de Costo</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--5 Accion--%>
                                                    <HeaderTemplate>
                                                        <b>Acción</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbEdit" CommandName="Editar" CommandArgument='<%# Eval("IdProducto")  + "|" + Eval("IdCentroCosto")  %>'>
                                                                    <i class="material-icons">edit</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbBorrar" CommandName="Borrar" CommandArgument='<%# Eval("IdProducto") + "|" + Eval("IdCentroCosto") %>'>
                                                                    <i class="material-icons">delete</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <%--Grid productos revision--%>
                                <div class="row clearfix">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvProductosRevision" AutoGenerateColumns="false" CssClass="table table-hover" Visible="false" OnRowCommand="gvProductosRevision_RowCommand">
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
                                                        <asp:Label ID="lbldgProductoID" runat="server" Text='<%# Bind("IdProducto") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--2 Cantidad--%>
                                                    <HeaderTemplate>
                                                        <b>Cantidad</b>
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
                                                    <%--5 Centro de costo--%>
                                                    <HeaderTemplate>
                                                        <b>Centro de Costo</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--6 Almacen--%>
                                                    <HeaderTemplate>
                                                        Almacen
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAlmacen" runat="server" Text='<%# Bind("CantidadAlmacenActual") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--7 Revision Fisica--%>
                                                    <HeaderTemplate>
                                                        Revision Fisica
                                                    </HeaderTemplate>
                                                    <ItemTemplate>                                                     
                                                        <asp:CheckBox ID="chbRevision" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <%--Grid productos autorizacion--%>
                                <div class="row clearfix">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvProductoAut" runat="server" AutoGenerateColumns="false" OnRowCommand="gvProductoAut_RowCommand" OnRowDataBound="gvProductoAut_RowDataBound" CssClass="table table-hover" OnSelectedIndexChanged="gvProductoAut_SelectedIndexChanged">
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
                                                        <asp:Label ID="lbldgProductoID" runat="server" Text='<%# Bind("IdProducto") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--2 Cantidad--%>
                                                    <HeaderTemplate>
                                                        <b>Cantidad</b>
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
                                                    <%--5 Centro de costo--%>
                                                    <HeaderTemplate>
                                                        <b>Centro de Costo</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldgCentroCosto" runat="server" Text='<%# Bind("CentroCosto") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--6 Almacen--%>
                                                    <HeaderTemplate>
                                                        Existencias
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAlmacen" runat="server" Text='<%# Bind("CantidadAlmacenActual") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--7 Autoriza Entrega--%>
                                                    <HeaderTemplate>
                                                        Autoriza entrega
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chbAutEntrega" runat="server" CssClass="checkbox" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%-- 8 Requiere comprar--%>
                                                    <HeaderTemplate>
                                                        Requiere comprar
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRequiereComp" CssClass="form-control" runat="server" Text='<%# Bind("CantidadAComprar") %>' TextMode="Number"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--9 Autoriza Compra--%>
                                                    <HeaderTemplate>
                                                        Autoriza Compra
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chbAutCompra" runat="server" CssClass="checkbox" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <asp:Label runat="server" ID="reqGrid" CssClass="alert-danger" Visible="false" Text="Debes agregar al menos un producto" />
                                <div class="row clearfix" runat="server" id="divOpinion" visible="false">
                                    <div class="col-lg-12">
                                        <div class="card">
                                            <div class="header">
                                                <h2><strong></strong></h2>
                                            </div>
                                            <div class="body">
                                                <h2 class="card-inside-title">Opinión de Almacen</h2>
                                                <div class="row clearfix">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtOpinion" Rows="4" CssClass="form-control no-resize" placeholder="Porfavor escriba su opinion"></asp:TextBox>
                                                                <asp:Label runat="server" ID="reqOpinion" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-4 col-md-8 text-center">
                                        <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                                    </div>
                                    <div class="col-lg-4 col-md-8 text-center">
                                        <a href="#ModalCancelar" data-toggle="modal" id="btnCancel" runat="server" data-target="#ModalCancelar" class="btn btn-raised btn-primary btn-round disabled">Cancelar
                                        </a>
                                    </div>
                                    <div class="col-lg-4 col-md-8 text-center">
                                        <a href="#ModalConfirmacion" data-toggle="modal" id="btnok" runat="server" data-target="#ModalConfirmacion" class="btn btn-raised btn-primary btn-round">
                                            <asp:Label ID="lblbtnCrear" runat="server" Text="Crear" />
                                        </a>
                                    </div>
                                </div>
                                <div>
                                    <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                                        data-toggle="modal" data-target="#myModal">
                                        Launch demo modal
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
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
                    <asp:Button ID="btnCancelar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Regresar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalConfirmacion" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalConfirmacionLabel">¿Esta seguro?</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="BtnCrear" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Si" OnClick="BtnCrear_Click" />
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalMensaje" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalMensajeLabel">Ree</h4>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnFin" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Si" OnClick="btnFin_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

