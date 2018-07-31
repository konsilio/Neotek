<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Requisicion.aspx.cs" Inherits="Web.MainModule.Requisicion" %>

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
                                    <asp:Label runat="server" ID="lblRuta" Text="Compras / Requisición / Nueva "></asp:Label>

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
                                                <input type="text" class="form-control datetimepicker" placeholder="Fecha requerida...">
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
                                                <asp:ListItem Text="Producto"></asp:ListItem>
                                                <asp:ListItem Text="Servicio"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-4 col-md-12">
                                            <%--Producto--%>
                                            <label class="card-inside-title">Producto:</label>
                                            <asp:DropDownList runat="server" ID="ddlProdcutos" CssClass="form-control z-index show-tick" data-live-search="true">
                                                <asp:ListItem Text="Nombre del producto"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2 col-md-12">
                                            <%--Cantidad--%>
                                            <label class="card-inside-title">Cantidad:</label>
                                            <asp:TextBox ID="txtCantidad" runat="server" placeholder="0" CssClass="form-control" />
                                        </div>
                                        <div class="col-lg-3 col-md-12">
                                            <%--Centro de costo--%>
                                            <label class="card-inside-title">Centro de costo:</label>
                                            <asp:DropDownList runat="server" ID="ddlCentroCostos" CssClass="form-control z-index show-tick" data-live-search="true">
                                                <asp:ListItem Text="Nombre del Centro"></asp:ListItem>
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
                                        <div class="col-lg-4 col-md-12">
                                            &nbsp;
                                        </div>
                                        <div class="col-lg-2 col-md-12">
                                            <div class="input-group">
                                                <asp:button ID="btnAgregar" CssClass="btn btn-raised btn-primary waves-effect btn-round" runat="server" data-type="ajax-loader" OnClick="btnAgregar_Click" Text="Agregar" />
                                                <%--<button class="btn btn-raised btn-primary waves-effect btn-round" data-type="ajax-loader">Agregar &nbsp;<span class="zmdi zmdi-plus"></span></button>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12">
                                            <!--Data Grid con los productos Agregados -->
                                            <asp:GridView runat="server" ID="dgListaproductos" EmptyDataText="No se han agregado productos al requerimiento">                                                
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <%--Tipo de compra--%>
                                                        <HeaderTemplate>
                                                            <label>Tipo</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgTipo" runat="server" Text='<%# Bind("TipoProducto")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <%--Producto--%>
                                                        <HeaderTemplate>
                                                            <label>Producto</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgProducto" runat="server" Text='<%# Bind("Prodcuto") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <%--Cantidad--%>
                                                        <HeaderTemplate>
                                                            <label>Cantidad</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgCantidad" runat="server" Text='<%# Bind("Cantidad") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <%--Unidad--%>
                                                        <HeaderTemplate>
                                                            <label>Unidad</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgUnidad" runat="server" Text='<%# Bind("Unidad") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <%--Aplicacion--%>
                                                        <HeaderTemplate>
                                                            <label>Aplicación</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldgAplicacion" runat="server" Text='<%# Bind("Aplicacion") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <%--Accion--%>
                                                        <HeaderTemplate>
                                                            <label>Acción</label>
                                                        </HeaderTemplate>
                                                        <HeaderTemplate>
                                                            <div class="demo-google-material-icon">
                                                                <asp:LinkButton runat="server" ID="lbEdit" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lbBorrar" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-6 col-md-12">
                                            <asp:Button ID="btnRegresar" CssClass="btn btn-raised btn-primary btn-round" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                                        </div>
                                        <div class="col-lg-6 col-md-12" align="right">
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
    </form>
</asp:Content>

