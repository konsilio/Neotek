﻿
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <h2>&nbsp;Cuentas contables</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="~/DashBoard/Vista/Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item active">
                                <asp:Label runat="server" ID="lblRuta" Text="Catalogos / Centros de costo "></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12">
                        <div class="card">
                            <div class="header">
                                <h2><strong>Centro de costo</strong></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-3 form-control-label">
                                        <label for="txtNumeroCentroCosto">Numero</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtNumeroCentroCosto" runat="server" CssClass="form-control" placeholder="Numero" />
                                            <asp:Label runat="server" ID="reqNumCC" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                    <div class="col-md-6 form-control-label">
                                        <label for="txtDescripcion">Descripción</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Descripción" />
                                            <asp:Label runat="server" ID="reqDescripcion" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlTipoCentroCosto">Tipo centro de costo</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlTipoCentroCosto" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqTipoCC" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlEquipoTransporte">Equipo Transporte</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEquipoTransporte" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqEquipo" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlVehiculo">Vehiculo</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlVehiculo" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqVehiculo" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlUnidadAlmacenGas">Unidad de almacen de gas</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlUnidadAlmacenGas" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqUnidad" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlEstacionCarburacion">Estacion de carburación</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEstacionCarburacion" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqEstacion" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlCamioneta">Camioneta</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlCamioneta" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqCamnioneta" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlPipa">Pipa</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlPipa" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqPipa" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <label for="ddlCilindroGas">Cilindro de Gas</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlCilindroGas" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                        <asp:Label runat="server" ID="reqCilindro" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                    </div>
                                    <div class="col-md-3 form-control-label">
                                        <b>&nbsp; </b>
                                        <div class="form-group">
                                            <a href="#ModalConfirmacion" data-toggle="modal" id="btnok" runat="server" data-target="#ModalConfirmacion" class="btn btn-raised btn-primary btn-round btn-sm">
                                                <i class="zmdi zmdi-plus" runat="server" id="iSimboloBoton"></i>
                                            </a>
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
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-4 form-control-label">
                                        <label for="ddlFiltroGasera">Gasera</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlFiltroGasera" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4 form-control-label">
                                        <label for="txtFiltroNumero">Numero</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtFiltroNumero" runat="server" CssClass="form-control" placeholder="Numero" />
                                        </div>
                                    </div>
                                    <div class="col-md-4 form-control-label">
                                        <label for="ddlFiltroCentroCosto">Tipo centro de costo</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlFiltroCentroCosto" runat="server" CssClass="form-control" placeholder="Descripción">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div id="DivAlerta" runat="server" visible="false" class="container">
                                    <div class="alert alert-danger">
                                        <div class="alert-icon">
                                            <i class="zmdi zmdi-block"></i>
                                        </div>
                                        <strong>
                                            <asp:Label ID="lblMensajeError" runat="server" Text="" /></strong>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvCentroCosto" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvCentroCosto_RowCommand" Width="100%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Gasera" DataField="Empresa" />
                                                <asp:BoundField HeaderText="Número" DataField="Numero" />
                                                <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                                <asp:BoundField HeaderText="Tipo" DataField="TipoCentroCosto" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Acción
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbEdit" CommandName="Editar" CommandArgument='<%# Eval("IdCentroCosto") %>'>
                                                                    <i class="material-icons">edit</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lbBorrar" CommandName="Borrar" CommandArgument='<%# Eval("IdCentroCosto") %>'>
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
                </div>
            </div>
        </div>
    </section>
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script src="http://harvesthq.github.io/chosen/chosen.jquery.js"></script>
