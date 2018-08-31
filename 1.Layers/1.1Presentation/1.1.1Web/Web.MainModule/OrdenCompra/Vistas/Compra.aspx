﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="Web.MainModule.OrdenCompra.Vistas.Compra" %>

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
                                <asp:Label runat="server" ID="lblRuta" Text="Compras / Ordern de Compra "></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="card">
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlEmpresas" runat="server" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtNoRequisicion" CssClass="form-control" placeholder="N° de Requisición" AutoPostBack="true" OnTextChanged="txtNoRequisicion_TextChanged" />
                            </div>
                        </div>
                    </div>
                    <div class="body row clearfix">
                        <div class="table-responsive">
                            <asp:GridView runat="server" AllowPaging="true" PageSize="5" OnPageIndexChanging="dgRequisisiones_PageIndexChanging" AutoGenerateColumns="false" ID="dgRequisisiones" CssClass="table m-b-0" OnRowCommand="dgRequisisiones_RowCommand" EmptyDataText="No hay nunguna requisicion pendiente">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Gasera
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDgGaseraNombre" Text='<%# Bind("NombreComercial") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            N° Requisición
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDgNoRequisicion" Text='<%# Bind("NumeroRequisicion") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Fecha Requerida
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDgFechaRequerida" Text='<%# Bind("FechaRequerida") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Solicitante
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSolictante" Text='<%# Bind("UsuarioSolicitante") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Estatus
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="Estatus" Text='<%# Bind("RequisicionEstatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Accion
                                        </HeaderTemplate>
                                        <ItemTemplate>            
                                                    <asp:LinkButton runat="server" ID="lbGenerarOrden" Text=" " CommandName="Requisicion" CommandArgument='<%# Bind("IdRequisicion") %>'>
                                                                    <i class="material-icons">launch</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                    </asp:LinkButton>                                               
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="body">
                        <div class="row clearfix">
                            <%-- <div class="col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlGaserasOC" runat="server" OnSelectedIndexChanged="ddlGaserasOC_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtNoOrdenRequisicionOC" CssClass="form-control" placeholder="N° de Requisición" AutoPostBack="true" OnTextChanged="txtNoOrdenRequisicionOC_TextChanged" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtNoOrdenCompra" CssClass="form-control" placeholder="N° Orden de Compra" AutoPostBack="true" OnTextChanged="txtNoOrdenCompra_TextChanged" />
                                </div>
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="col-sm-4">
                                <label>Proveedor</label>
                                <asp:DropDownList CssClass="form-control show-tick" runat="server" ID="ddlFiltroProveedores" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-4">
                                <label>Estatus</label>
                                <asp:DropDownList CssClass="form-control show-tick" runat="server" ID="ddlFiltroEstatus" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="col-sm-3">
                                <label>Fecha de registro</label>
                                <div class="input-group">
                                    <dtp:DateTimePicker ID="dtpFechaRegiistrDe" cssclass="dataTxt" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label>&nbsp; </label>
                                <div class="input-group">
                                    <dtp:DateTimePicker ID="dtpFechaRegiistrA" cssclass="dataTxt" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label>Fecha de requisición</label>
                                <div class="input-group">
                                    <dtp:DateTimePicker ID="dtpFechaRequisicionDe" cssclass="dataTxt" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label>&nbsp;</label>
                                <div class="input-group">
                                    <dtp:DateTimePicker ID="dtpFechaRequisicionA" cssclass="dataTxt" runat="server" />
                                </div>
                            </div>
                            <div class="col-sm-1">
                                <b>&nbsp; </b>
                                <div class="input-group">
                                    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-danger btn-simple btn-round btn-sm" OnClick="btnBuscar_Click">
                                            <i class="material-icons">search</i>
                                            <span class="icon-name"></span> 
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="table-responsive">
                                <asp:GridView runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvOrdenCompra_PageIndexChanging"
                                    AutoGenerateColumns="false" ID="gvOrdenCompra" CssClass="table m-b-0" OnRowCommand="gvOrdenCompra_RowCommand"
                                    OnRowDataBound="gvOrdenCompra_RowDataBound" EmptyDataText="No hay Orden de Compra disponible">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Gasera
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDgGasera" Text='<%# Bind("IdEmpresa") %>' Visible="false"></asp:Label>
                                                <asp:Label runat="server" ID="lblDgGaseraNombre" Text='<%# Bind("Empresa") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                N° Requisición
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDgNoRequisicion" Text='<%# Bind("NumeroRequisicion") %>'></asp:Label>
                                                <%--  <asp:Label runat="server" ID="lblIdRequisicionEstatus" Text='<%# Bind("IdRequisicionEstatus") %>' Visible="false"></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                No. Orden de Compra
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblIdRequisicionEstatus" Text='<%# Bind("IdOrdenCompra") %>' Visible="false"></asp:Label>
                                                <asp:Label runat="server" ID="lblDgFechaRequerida" Text='<%# Bind("NumOrdenCompra") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField>
                                        <HeaderTemplate>
                                            Tipo
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblSolictante" Text='<%# Bind("TipoProdServ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Solicitante
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblSolictante" Text='<%# Bind("UsuarioSolicitante") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Proveedor
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblProveedor" Text='<%# Bind("Proveedor") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Importe
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                $
                                        <asp:Label runat="server" ID="lblTotal" Text='<%# Bind("Total") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Estatus
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblgvIdEstatus" Text='<%# Bind("IdOrdenCompraEstatus")%>' Visible="false" />
                                                <asp:Label runat="server" ID="Estatus" Text='<%# Bind("OrdenCompraEstatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Accion
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbPDF" Visible="false" CommandName="PDF" CommandArgument='<%# Bind("IdOrdenCompra") %>' >
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbAutOC" Text=" " Visible="false" CommandName="OrdenCompra" CommandArgument='<%# Bind("IdOrdenCompra") %>'>
                                                                    <i class="material-icons">spellcheck</i>                                                                    
                                                                    <span class="icon-name"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbAgregarMercancia" Text=" " Visible="false" CommandName="AgregarMerca" CommandArgument='<%# Bind("IdOrdenCompra") %>'>
                                                                    <i class="material-icons"> assignment_turned_in</i>                                                                    
                                                                    <span class="icon-name"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbVisualizarOC" Text=" " Visible="false" CommandName="Ver" CommandArgument='<%# Bind("IdOrdenCompra") %>'>
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

