<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Web.MainModule.Compras" %>

<asp:Content ID="ctCompras" ContentPlaceHolderID="ctCompras" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <h2>Compras</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="~/DashBoard/Vista/Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item active">
                                <asp:Label runat="server" ID="lblRuta" Text="Compras / Requisición "></asp:Label>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="header">
                                <h2><strong></strong><small></small></h2>
                            </div>
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-sm-4">
                                        <asp:DropDownList runat="server" ID="ddlEmpresas" data-live-search="true" AutoPostBack="true" CssClass="form-control z-index show-tick" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="txtNoRequisicion" class="form-control" placeholder="N° de Requisición" OnTextChanged="txtNoRequisicion_TextChanged" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button runat="server" ID="btnNuevaReq" Text="Nueva Requisición" CssClass="btn btn-primary btn-round btn-block" OnClick="btnNuevaReq_Click" />
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-2 col-md-6">
                                        <label>Estatus</label>
                                        <select class="form-control show-tick" runat="server" id="ddlFiltroEstatus">
                                            <option value="0">Estatus</option>
                                        </select>
                                    </div>
                                    <div class="col-lg-2 col-md-6">
                                        <label>Fecha de registro</label>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="zmdi zmdi-calendar"></i>
                                            </span>
                                            <input type="text" class="form-control datetimepicker" placeholder="De:">
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-6">
                                        <label>&nbsp; </label>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="zmdi zmdi-calendar-note"></i>
                                            </span>
                                            <input type="text" class="form-control datetimepicker" placeholder="A:">
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-6">
                                        <label>Fecha de requisición</label>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="zmdi zmdi-calendar"></i>
                                            </span>
                                            <input type="text" class="form-control datetimepicker" placeholder="De:">
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-6">
                                        <label>&nbsp;</label>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="zmdi zmdi-calendar"></i>
                                            </span>
                                            <input type="text" class="form-control datetimepicker" placeholder="A:">
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="body table-responsive">
                                        <asp:GridView runat="server" AllowPaging="true" PageSize="6" OnPageIndexChanging="dgRequisisiones_PageIndexChanging" AutoGenerateColumns="false" ID="dgRequisisiones" CssClass="table m-b-0" OnRowCommand="dgRequisisiones_RowCommand" OnRowDataBound="dgRequisisiones_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Gasera
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDgGasera" Text='<%# Bind("IdEmpresa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        N° Requisición
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDgNoRequisicion" Text='<%# Bind("NumeroRequisicion") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblIdRequisiconEstatus" Text='<%# Bind("IdRequisicionEstatus") %>' Visible="false"></asp:Label>
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
                                                        <asp:Label runat="server" ID="lblDgSolicitante" Text='<%# Bind("IdUsuarioSolicitante") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Estatus
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="Estatus" Text="Creada"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Accion
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                            <div class="demo-google-material-icon">
                                                                <asp:LinkButton runat="server" ID="lbDgPDF" Text=" " CommnadnName="VerPDF" CommandArgument='<%# Bind("NumeroRequisicion") %>'>
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lbDgOjo" Text=" " CommandName="VerRequi" CommandArgument='<%# Eval("NumeroRequisicion") + "|" + Eval("IdRequisicionEstatus") %>'>
                                                                    <i class="material-icons">content_paste</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lbAutoriza" Text=" " CommandName="VerAut" CommandArgument='<%# Eval("NumeroRequisicion") + "|" + Eval("IdRequisicionEstatus") %>'>
                                                                    <i class="material-icons">spellcheck</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                </asp:LinkButton>
                                                            </div>
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
</asp:Content>

