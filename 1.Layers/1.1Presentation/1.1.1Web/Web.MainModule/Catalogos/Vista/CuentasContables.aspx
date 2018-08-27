<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentasContables.aspx.cs" Inherits="Web.MainModule.Catalogos.CuentasContables" %>

<asp:Content ID="ContentCuentaContable" ContentPlaceHolderID="ctCuentasContables" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <h2>&nbsp;Cuentas contables</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="~/DashBoard/Vista/Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item active">
                                <asp:Label runat="server" ID="lblRuta" Text="Catalogos / Cuentas contables "></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="card">
                        <div class="header">
                            <b><strong>Insertar Cuenta contable</strong></b>
                        </div>
                        <div class="body">
                            <div class="row clearfix">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <b>Número</b>
                                        <asp:TextBox runat="server" ID="txtNumero" CssClass="form-control form-control-sm" />
                                    </div>
                                      <asp:Label runat="server" ID="reqNum" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <b>Descripción</b>
                                        <asp:TextBox runat="server" ID="txtDesc" CssClass="form-control form-control-sm" />
                                    </div>
                                    <asp:Label runat="server" ID="reqDesc" CssClass="alert-danger" Visible="false" Text="Campo requerido" />
                                </div>
                                <div class="col-md-1">
                                    <b>&nbsp; </b>
                                    <div class="form-group">
                                          <a href="#ModalConfirmacion" data-toggle="modal" id="btnok" runat="server" data-target="#ModalConfirmacion" class="btn btn-raised btn-primary btn-round btn-sm">
                                             <i class="zmdi zmdi-plus"></i>
                                        </a>
                                       <%-- <button class="btn btn-primary btn-icon  btn-icon-mini btn-round btn-sm">
                                            <i class="zmdi zmdi-plus"></i>
                                        </button>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row clearfix">
                    <div class="card">
                        <div class="body">
                            <div class="row clearfix">
                                <div class="col-md-3">
                                    <b>Gasera</b>
                                    <asp:DropDownList ID="ddlEmpresas" CssClass="form-control show-tick" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <b>Numero</b>
                                        <asp:TextBox runat="server" ID="txtNumeroCtaCtble" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvCuentasContables" AutoGenerateColumns="false" OnRowCommand="gvCuentasContables_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Gasera" DataField="NombreGasera" />
                                            <asp:BoundField HeaderText="Número" DataField="Numero" />
                                            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <p>Acción</p>
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
</asp:Content>
