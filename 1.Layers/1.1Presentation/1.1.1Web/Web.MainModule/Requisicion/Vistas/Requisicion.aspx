<%@ Page Title="" Language="C#" EnableViewState="true" EnableViewStateMac="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requisicion.aspx.cs" Inherits="Web.MainModule.Requisicion.Vista.Requisicion" %>

<asp:Content ID="ContentRequisicion" ContentPlaceHolderID="ctRequisicion" runat="server">
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
                </div>
                <div class="body" id="divNoRequi" runat="server" visible="false">
                    <div class="alert alert-success">
                        <strong>
                            <asp:Label ID="lblNoRequisicion" runat="server" Text="" /></strong>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEmpresas" runat="server" Visible="false" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNombreEmpresa" runat="server" Text="Nombre de la empresa"></asp:Label>
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
                                <div class="row clearfix">
                                    <div class="col-lg-3 col-md-12">
                                        <div class="form-group form-float">
                                            <%--Fecha requerida--%>
                                            <label class="card-inside-title">Fecha requerida:</label>
                                            <asp:TextBox runat="server" ID="txtFechaRequerida" CssClass="form-control datetimepicker" placeholder="Fecha requerida..." name="date" />
                                            <asp:Label runat="server" ID="reqFecha" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-12">
                                        <div class="form-group form-float">
                                            <%--Solicitante--%>
                                            <label class="card-inside-title">Solicitante:</label>
                                            <asp:DropDownList runat="server" ID="ddlSolicitante" CssClass="form-control z-index show-tick" data-live-search="true" OnSelectedIndexChanged="ddlSolicitante_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Label runat="server" ID="reqSol" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-12 form-float">
                                        <div class="form-group">
                                            <%--Motivo de compra--%>
                                            <label class="card-inside-title">Motivo de compra:</label>
                                            <asp:TextBox runat="server" ID="txtMotivoCompra" CssClass="form-control" placeholder="Motivo de la compra..." TextMode="MultiLine" OnTextChanged="txtMotivoCompra_TextChanged"></asp:TextBox>
                                            <asp:Label runat="server" ID="reqMotivo" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-12">
                                        <div class="form-group form-float">
                                            <%--Se requiere en--%>
                                            <label class="card-inside-title">Se requiere en:</label>
                                            <asp:TextBox runat="server" ID="txtRequeridoEn" CssClass="form-control" placeholder="Se requiere en..." TextMode="MultiLine" OnTextChanged="txtRequeridoEn_TextChanged"></asp:TextBox>
                                            <asp:Label runat="server" ID="reqReq" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                </div>
                                <div class="body" id="divCampos" runat="server" visible="false">
                                    <div class="alert alert-danger">
                                        <strong>
                                            <asp:Label ID="lblErrorCampos" runat="server" Text="" /></strong>
                                    </div>
                                </div>
                                <div class="row clearfix" runat="server" id="divDatos1">
                                    <div class="col-lg-2 col-md-12">
                                        <%--Tipo de compra--%>
                                        <label class="card-inside-title">Tipo de compra:</label>
                                        <asp:DropDownList ID="ddlTipoCompra" runat="server" CssClass="form-control z-index show-tick">
                                            <asp:ListItem Value="0" Text="Seleccione tipo de producto"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Producto"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Servicio"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="reqTipo" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-4 col-md-12">
                                        <%--Producto--%>
                                        <label class="card-inside-title">Producto:</label>
                                        <asp:DropDownList runat="server" ID="ddlProdcutos" CssClass="form-control z-index show-tick" data-live-search="true">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="reqProd" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-2 col-md-12">
                                        <%--Cantidad--%>
                                        <div class="form-group">
                                            <label class="card-inside-title">Cantidad:</label>
                                            <asp:TextBox ID="txtCantidad" runat="server" placeholder="0" type="number" CssClass="form-control" />
                                        </div>
                                        <asp:Label runat="server" ID="reqCant" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-lg-3 col-md-12">
                                        <%--Centro de costo--%>
                                        <label class="card-inside-title">Centro de costo:</label>
                                        <asp:DropDownList runat="server" ID="ddlCentroCostos" CssClass="form-control z-index show-tick" data-live-search="true">
                                            <asp:ListItem Value="0" Text="Nombre del Centro"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Esto es un centro de costos"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="reqCC" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                </div>
                                <div class="row clearfix js-sweetalert" id="divDatos2" runat="server">
                                    <div class="col-lg-6 col-md-12">
                                        <div class="form-group">
                                            <%--Aplicacion--%>
                                            <label class="card-inside-title">Aplicación:</label>
                                            <asp:TextBox ID="txtDetalle" runat="server" placeholder="Comentarios..." CssClass="form-control" />
                                            <asp:Label runat="server" ID="reqApli" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12" align="right">
                                        <div class="form-group">
                                            <label class="card-inside-title">&nbsp;</label>
                                            <asp:Button ID="btnAgregar" CssClass="btn btn-raised btn-primary waves-effect btn-round" runat="server" data-type="ajax-loader" OnClick="btnAgregar_Click" Text="Agregar" />
                                            <%--<button class="btn btn-raised btn-primary waves-effect btn-round" data-type="ajax-loader">Agregar &nbsp;<span class="zmdi zmdi-plus"></span></button>--%>
                                        </div>
                                    </div>
                                </div>
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
                                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtOpinion" rows="4" CssClass="form-control no-resize" placeholder="Porfavor escriba su opinion"></asp:TextBox>
                                                             <asp:Label runat="server" ID="reqOpinion" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="body" id="DivCamposPord" runat="server" visible="false">
                                    <div class="alert alert-danger">
                                        <strong>
                                            <asp:Label ID="lblErrorPord" runat="server" Text="" /></strong>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="body table-responsive">
                                        <!--Grid productos agregados nueva requisicion-->
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
                                                    <%--5 Accion--%>
                                                    <HeaderTemplate>
                                                        <b>Acción</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbEdit" CommandName="Editar" CommandArgument='<%# Eval("IdProducto") %>'>
                                                                    <i class="material-icons">edit</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbBorrar" CommandName="Borrar" CommandArgument='<%# Eval("IdProducto") %>'>
                                                                    <i class="material-icons">delete</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--Grid productos revision--%>
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
                                        <%--Grid productos autorizacion--%>
                                        <asp:GridView ID="gvProductoAut" runat="server" AutoGenerateColumns="false" OnRowCommand="gvProductoAut_RowCommand" OnRowDataBound="gvProductoAut_RowDataBound">
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
                                                        <asp:CheckBox ID="chbAutEntrega" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%-- 8 Requiere comprar--%>
                                                    <HeaderTemplate>
                                                        Requiere comprar
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequiereComp" runat="server" Text='<%# Bind("CantidadAComprar") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <%--9 Autoriza Compra--%>
                                                    <HeaderTemplate>
                                                        Autoriza Compra
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chbAutCompra" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="reqGrid" CssClass="alert-danger" Visible="false" Text="Debes agregar al menos un producto" />
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-4 col-md-8" align="center">
                                        <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                                    </div>
                                    <div class="col-lg-4 col-md-8" align="center">
                                        <asp:Button ID="btnCancelar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" Enabled="false" />
                                    </div>
                                    <div class="col-lg-4 col-md-8" align="center">
                                        <asp:Button ID="BtnCrear" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Crear" OnClick="BtnCrear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <div class="modal fade" id="ModalCancelar" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <asp:UpdatePanel ID="upModalCancelar" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="title" id="ModalCancelarLabel">Motivo de cancelcion</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:TextBox ID="txtMotivoCancela" runat="server" placeholder="Por cual motivo decea cancelar..."></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary btn-round waves-effect">Cancelar</button>
                            <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Regresar</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal fade" id="ModalConfirmacion" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="title" id="ModalConfirmacionLabel">¿Esta seguro?</h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-round waves-effect">SI</button>
                    <button type="button" class="btn btn-danger btn-simple btn-round waves-effect" data-dismiss="modal">Regresar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

