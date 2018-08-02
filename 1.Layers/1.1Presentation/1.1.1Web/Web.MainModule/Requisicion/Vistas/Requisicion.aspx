<%@ Page Title="" Language="C#" EnableViewState="true" EnableViewStateMac="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requisicion.aspx.cs" Inherits="Web.MainModule.Requisicion.Vista.Requisicion" %>

<asp:Content ID="ContentRequisicion" ContentPlaceHolderID="ctRequisicion" runat="server">
    <form runat="server">
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
                    <div class="row clearfix">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="body">
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
                                                    <asp:Label runat="server" ID="lblIdRequisicion" Text="R000000000"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-3 col-md-12">
                                            <div class="form-group">
                                                <%--Fecha requerida--%>
                                                <label class="card-inside-title">Fecha requerida:</label>
                                                <input runat="server" id="txtFechaRequerida" type="text" class="form-control datetimepicker" placeholder="Fecha requerida...">
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-12">
                                            <div class="form-group">
                                                <%--Solicitante--%>
                                                <label class="card-inside-title">Solicitante:</label>
                                                <asp:TextBox runat="server" ID="txtSolicitante" placeholder="Michael (sa)" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-12">
                                            <div class="form-group">
                                                <%--Motivo de compra--%>
                                                <label class="card-inside-title">Motivo de compra:</label>
                                                <asp:TextBox runat="server" ID="txtMotivoCompra" CssClass="form-control" placeholder="Motivo de la compra..." TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-12">
                                            <div class="form-group">
                                                <%--Se requiere en--%>
                                                <label class="card-inside-title">Se requiere en:</label>
                                                <asp:TextBox runat="server" ID="txtRequeridoEn" CssClass="form-control" placeholder="Se requiere en..." TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-2 col-md-12">
                                            <%--Tipo de compra--%>
                                            <label class="card-inside-title">Tipo de compra:</label>
                                            <asp:DropDownList ID="ddlTipoCompra" runat="server" CssClass="form-control z-index show-tick">
                                                <asp:ListItem Value="1" Text="Producto"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Servicio"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4 col-md-12">
                                            <%--Producto--%>
                                            <label class="card-inside-title">Producto:</label>
                                            <asp:DropDownList runat="server" ID="ddlProdcutos" CssClass="form-control z-index show-tick" data-live-search="true">
                                                <asp:ListItem Value="0" Text="Nombre del producto"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Esto es un producto"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2 col-md-12">
                                            <%--Cantidad--%>
                                            <div class="form-group">
                                                <label class="card-inside-title">Cantidad:</label>
                                                <asp:TextBox ID="txtCantidad" runat="server" placeholder="0" CssClass="form-control" />

                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-12">
                                            <%--Centro de costo--%>
                                            <label class="card-inside-title">Centro de costo:</label>
                                            <asp:DropDownList runat="server" ID="ddlCentroCostos" CssClass="form-control z-index show-tick" data-live-search="true">
                                                <asp:ListItem Value="0" Text="Nombre del Centro"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Esto es un centro de costos"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row clearfix js-sweetalert">
                                        <div class="col-lg-6 col-md-12">
                                            <div class="form-group">
                                                <%--Aplicacion--%>
                                                <label class="card-inside-title">Aplicación:</label>
                                                <asp:TextBox ID="txtDetalle" runat="server" placeholder="Comentarios..." CssClass="form-control" />
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
                                    <div class="row clearfix">
                                        <div class="body table-responsive">
                                            <!--Data Grid con los productos Agregados -->
                                            <asp:DataGrid CssClass="table table-hover" runat="server" ID="dgListaproductos" Width="100%" ShowHeader="true" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <%--Tipo de compra--%>
                                                        <HeaderTemplate>
                                                            <b>Tipo</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <%--Producto--%>
                                                        <HeaderTemplate>
                                                            <b>Producto</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Producto") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <%--Cantidad--%>
                                                        <HeaderTemplate>
                                                            <b>Cantidad</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("Cantidad") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <%--Unidad--%>
                                                        <HeaderTemplate>
                                                            <b>Unidad</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgUnidad" runat="server" Text='<%# Bind("Unidad") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <%--Aplicacion--%>
                                                        <HeaderTemplate>
                                                            <b>Aplicación</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <%--Accion--%>
                                                        <HeaderTemplate>
                                                            <b>Acción</b>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lbEdit">
                                                                    <i class="material-icons">edit</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lbBorrar">
                                                                    <i class="material-icons">delete</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-6 col-md-12">
                                            <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                                        </div>
                                        <div class="col-lg-6 col-md-12" align="right">
                                            <asp:Button ID="BtnCrear" CssClass="btn btn-primary" data-toggle="modal" runat="server" Text="Crear" OnClick="BtnCrear_Click" />
                                            <div class="modal fade" id="ModalNumRequi" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="exampleModalLabel">Requisicon exitosa</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:Label runat="server" ID="lblNoRequisicion" ></asp:Label>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Aceptar</button>
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
            </div>
        </section>
    </form>
</asp:Content>

