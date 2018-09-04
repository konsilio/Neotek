<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedor.aspx.cs" Inherits="Web.MainModule.Catalogos.Proveedor" %>

<asp:Content ID="ContentProveedor" ContentPlaceHolderID="ctProveedor" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <h2>Proveedores</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="index.html"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Forms</a></li>
                            <li class="breadcrumb-item active">Catálogo / Proveedores</li>
                        </ul>
                    </div>
                </div>
            </div>
            <div runat="server" id="divPrincipal">
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2><strong>Proveedor</strong></h2>
                                <ul class="header-dropdown">
                                    <li class="dropdown"><a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><i class="zmdi zmdi-more"></i></a>
                                        <ul class="dropdown-menu dropdown-menu-right">
                                            <li>
                                                <asp:LinkButton runat="server" ID="btnNuevoProveedor" OnClick="btnNuevoProveedor_Click"><i class="material-icons">add</i> Agregar</asp:LinkButton></li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-lg-6 col-md-6 col-sm-12 m-b-20">
                                        <b>Empresa</b>
                                        <asp:DropDownList ID="ddlFiltroEmpresa" runat="server" CssClass="form-control z-index show-tick">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <div class="form-group text-center">
                                            <button class="btn btn-primary btn-round" id="btnProvedorNuevo" runat="server" onserverclick="btnNuevoProveedor_Click">
                                                <i class="material-icons">add</i> Agregar                               
                                            </button>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <div class="form-group text-center">
                                            <button class="btn btn-primary btn-round">
                                                <i class="material-icons">search</i> Buscar                                   
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-3">
                                        <b>Tipo de Proveedor</b>
                                        <select class="form-control show-tick" data-live-search="true">
                                            <option>Proveedor</option>
                                            <option>Acreedor</option>
                                        </select>
                                    </div>
                                    <div class="col-md-3">
                                        <b>Estado</b>
                                        <select class="form-control show-tick" data-live-search="true">
                                            <option>Aguascalientes</option>
                                            <option>Baja California</option>
                                        </select>
                                    </div>
                                    <div class="col-md-3">
                                        <b>RFC</b>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Ej: VECJ880326XXX">
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="checkbox">
                                            <input id="checkbox10" type="checkbox">
                                            <label for="checkbox10">
                                                Trasnporta gas
                                            </label>
                                        </div>
                                        <div class="checkbox">
                                            <input id="checkbox11" type="checkbox">
                                            <label for="checkbox11">
                                                Vende gas                                       
                                            </label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="body table-responsive">
                                <asp:GridView ID="gvProveedor" runat="server" OnRowCommand="gvProveedor_RowCommand" AutoGenerateColumns="false" Width="100%" CssClass="table table-bordered">
                                    <Columns>
                                        <asp:BoundField HeaderText="Nombre" DataField="NombreComercial" />
                                        <asp:BoundField HeaderText="RFC" DataField="Rfc" />
                                        <asp:BoundField HeaderText="Contacto" DataField="Persona1" />
                                        <asp:BoundField HeaderText="Telefono" DataField="Telefono1" />
                                        <asp:BoundField HeaderText="Email" DataField="Email1" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Acciòn
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbEdit" CommandName="Editar" CommandArgument='<%# Eval("IdCuentaContable") %>'>
                                                                    <i class="material-icons">edit</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbBorrar" CommandName="Borrar" CommandArgument='<%# Eval("IdCuentaContable") %>'>
                                                                    <i class="material-icons">delete</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div runat="server" id="divDatos" visible="false">
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2>Información <strong>General</strong></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <b>Empresa</b>
                                        <asp:DropDownList ID="ddlEmpresas" runat="server" CssClass="form-control z-index show-tick"></asp:DropDownList>
                                        <%-- <select class="form-control z-index show-tick" data-live-search="true">
                                        <option>Empresa 1</option>
                                        <option>Empresa 2</option>
                                    </select>--%>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-4">
                                        <b>Tipo de proveedor</b>
                                        <select class="form-control show-tick" data-live-search="true">
                                            <option>Proveedor</option>
                                            <option>Acreedor</option>
                                        </select>
                                    </div>
                                    <div class="col-md-4">
                                        <b>Centa contable</b>
                                        <select class="form-control show-tick" data-live-search="true">
                                            <option>Cuenta 1</option>
                                            <option>Cuenta 2</option>
                                        </select>
                                    </div>
                                    <div class="col-md-4">
                                        <b>Nombre comercial</b>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Nombre Comercial">
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-3">
                                        <div class="checkbox">
                                            <input id="checkbox13" type="checkbox">
                                            <label for="checkbox13">
                                                ¿Vende gas?                                           
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="checkbox">
                                            <input id="checkbox14" type="checkbox">
                                            <label for="checkbox14">
                                                ¿Transporta gas?                                           
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <p>¿Que es lo que vende?</p>
                                            <textarea class="form-control m-b-20" placeholder="Descripción de lo que vende el proveedor" rows="5"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2>Información de <strong>ubicación </strong></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-4">
                                        <b>Pais</b>
                                        <select class="form-control z-index show-tick" data-live-search="true">
                                            <option>Pais 1</option>
                                            <option>Pais 2</option>
                                        </select>
                                    </div>
                                    <div class="col-md-4">
                                        <b>Estado de la rep.</b>
                                        <select class="form-control z-index show-tick" data-live-search="true">
                                            <option>Estado 1</option>
                                            <option>Estado 2</option>
                                        </select>
                                    </div>
                                    <!--Este debe aparecer si eligen un pais diferente a México y desaparecer la lista de estados de la rep.-->
                                    <div class="col-md-4">
                                        <b>Estado.</b>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Estado/Provincia">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Municipio">
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Código postal">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Colonia o fraccionamiento">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Calle">
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Número exterior">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Número interior">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2>Información <strong>Bancaria</strong></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-6">
                                        <b>Banco</b>
                                        <select class="form-control z-index show-tick" data-live-search="true">
                                            <option>Banco 1</option>
                                            <option>Banco 2</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6">
                                        <b>Cuenta</b>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Número de cuenta">
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-6">
                                        <b>Forma de pago</b>
                                        <select class="form-control show-tick" data-live-search="true">
                                            <option>Forma pago 1</option>
                                            <option>Forma pago 2</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6">
                                        <b>Dìas de credito</b>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Días que otorga de crédito">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2>Información <strong>de Contacto</strong></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <b>Nombre del Contacto</b>
                                            <input type="text" class="form-control" placeholder="Nombre del primer contacto">
                                        </div>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Nombre del segundo contacto">
                                        </div>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Nombre del tercer contacto">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <b>Teléfono</b>
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="zmdi zmdi-phone"></i></span>
                                                <input type="text" class="form-control mobile-phone-number" placeholder="Ej: +00 (000) 000-00-00">
                                            </div>
                                            <%--<b>Teléfono</b>--%>
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="zmdi zmdi-phone"></i></span>
                                                <input type="text" class="form-control mobile-phone-number" placeholder="Ej: +00 (000) 000-00-00">
                                            </div>
                                            <%-- <b>Teléfono</b>--%>
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="zmdi zmdi-phone"></i></span>
                                                <input type="text" class="form-control mobile-phone-number" placeholder="Ej: +00 (000) 000-00-00">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <b>Celular</b>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-smartphone"></i></span>
                                            <input type="text" class="form-control mobile-phone-number" placeholder="Ej: +00 (000) 000-00-00">
                                        </div>
                                        <%--   <b>Celular</b>--%>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-smartphone"></i></span>
                                            <input type="text" class="form-control mobile-phone-number" placeholder="Ej: +00 (000) 000-00-00">
                                        </div>
                                        <%--<b>Celular</b>--%>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-smartphone"></i></span>
                                            <input type="text" class="form-control mobile-phone-number" placeholder="Ej: +00 (000) 000-00-00">
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-4">
                                        <b>Correo electrónico</b>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-email"></i></span>
                                            <input type="text" class="form-control email" placeholder="Ej: ejemplo1@example.com">
                                        </div>
                                        <%--<b>Correo electrónico</b>--%>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-email"></i></span>
                                            <input type="text" class="form-control email" placeholder="Ej: ejemplo2@example.com">
                                        </div>
                                        <%-- <b>Correo electrónico</b>--%>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-email"></i></span>
                                            <input type="text" class="form-control email" placeholder="Ej: ejemplo3@example.com">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <b>Sitios web</b>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-email"></i></span>
                                            <input type="text" class="form-control email" placeholder="Ej: ejemplo1.com">
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-email"></i></span>
                                            <input type="text" class="form-control email" placeholder="Ej: ejemplo2.com">
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-email"></i></span>
                                            <input type="text" class="form-control email" placeholder="Ej: ejemplo3.com">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2>Información <strong>Fiscal</strong></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-3">
                                        <b>Tipo persona</b>
                                        <select class="form-control z-index show-tick" data-live-search="true">
                                            <option>Moral</option>
                                            <option>Física</option>
                                        </select>
                                    </div>
                                    <div class="col-md-9">
                                        <b>Regimen Fiscal</b>
                                        <select class="form-control z-index show-tick" data-live-search="true">
                                            <option>Regimen 1</option>
                                            <option>Regimen 2</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-3">
                                        <b>Rfc</b>
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="zmdi zmdi-key"></i></span>
                                            <input type="text" class="form-control key" placeholder="Ex: XXX-000000-XXX">
                                        </div>
                                    </div>
                                    <div class="col-md-9">
                                        <b>Razon Social</b>
                                        <div class="form-group">
                                            <input type="text" class="form-control" placeholder="Razón social">
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
</asp:Content>
