<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Web.MainModule.Compras" %>

<asp:Content ID="ctCompras" ContentPlaceHolderID="ctCompras" runat="server">
    <form runat="server">
        <section class="content home">
            <div class="container-fluid">
                <div class="block-header">
                    <div class="row clearfix">
                        <div class="col-lg-5 col-md-5 col-sm-12">
                            <h2>Compras</h2>
                            <ul class="breadcrumb padding-0">
                                <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                                <li class="breadcrumb-item active">
                                    <asp:Label runat="server" ID="lblRuta" Text="Compras / Requisición "></asp:Label>
                                </li>
                                <%--<asp:Label ID="lblMensaje" runat="server"></asp:Label>--%>
                                <%--<asp:Button runat="server" ID="btnCompra" OnClick="btnCompra_Click" CssClass="form-control" Text="Compras" />--%>
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
                                            <asp:DropDownList runat="server" ID="ddlEmpresas" data-live-search="true" CssClass="form-control z-index show-tick">
                                            </asp:DropDownList>                                            
                                            <%--<select class="form-control show-tick" runat="server" id="ddlGaseras">
                                                
                                            </select>--%>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <input type="text" class="form-control" placeholder="N° de Requisición" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Button runat="server" ID="btnNuevaReq" Text="Nueva Requisición" CssClass="btn btn-primary btn-round btn-block" OnClick="btnNuevaReq_Click" />
                                        </div>
                                    </div>
                                     <div class="row clearfix">
                                        <div class="col-lg-2 col-md-6">
                                            <label>Estatus</label>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <label>Fecha de registro</label>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            
                                               <label> </label>
                                            
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <label>Fecha de requisición</label>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <label> </label>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-2 col-md-6">
                                            <select class="form-control show-tick" runat="server" id="ddlFiltroEstatus">
                                                <option value="0">Estatus</option>
                                            </select>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="zmdi zmdi-calendar"></i>
                                                </span>
                                                <input type="text" class="form-control datetimepicker" placeholder="De">
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="zmdi zmdi-calendar-note"></i>
                                                </span>
                                                <input type="text" class="form-control datetimepicker" placeholder="A ">
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="zmdi zmdi-calendar"></i>
                                                </span>
                                                <input type="text" class="form-control datetimepicker" placeholder="De">
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-6">
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <i class="zmdi zmdi-calendar"></i>
                                                </span>
                                                <input type="text" class="form-control datetimepicker" placeholder="A ">
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row clearfix">
                                        <div class="body table-responsive">
                                            <table class="table m-b-0">
                                                <thead>
                                                    <tr>
                                                        <th>Gasera</th>
                                                        <th>N° Requisición</th>
                                                        <th>Fecha Requerida</th>
                                                        <th>Solicitante</th>
                                                        <th>Estatus</th>
                                                        <th>Accion</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <th scope="row">Empresa 1</th>
                                                        <td>R20180001</td>
                                                        <td>15/08/2018</td>
                                                        <td>Kevin Salomon</td>
                                                        <td>Revisar Existencias</td>
                                                        <td>
                                                            <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                                <div class="demo-google-material-icon">
                                                                    <asp:LinkButton runat="server" ID="lbDgPDF" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>                                                                
                                                                    <asp:LinkButton runat="server" ID="LinkButton9" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Empresa 2</th>
                                                        <td>R20180002</td>
                                                        <td>15/08/2018</td>
                                                        <td>Kevin Salomon</td>
                                                        <td>Generar orden de compra</td>
                                                        <td>
                                                           <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                                <div class="demo-google-material-icon">
                                                                    <asp:LinkButton runat="server" ID="LinkButton1" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>                                                                
                                                                    <asp:LinkButton runat="server" ID="LinkButton2" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Empresa 3</th>
                                                        <td>R20180003</td>
                                                        <td>15/08/2018</td>
                                                        <td>Kevin Salomon</td>
                                                        <td>Autorizar orden de compra</td>
                                                        <td>
                                                           <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                                <div class="demo-google-material-icon">
                                                                    <asp:LinkButton runat="server" ID="LinkButton3" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>                                                                
                                                                    <asp:LinkButton runat="server" ID="LinkButton4" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Empresa 2</th>
                                                        <td>R20180004</td>
                                                        <td>15/08/2018</td>
                                                        <td>Kevin Salomon</td>
                                                        <td>Proceso de compra</td>
                                                        <td>
                                                           <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                                <div class="demo-google-material-icon">
                                                                    <asp:LinkButton runat="server" ID="LinkButton5" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>                                                                
                                                                    <asp:LinkButton runat="server" ID="LinkButton6" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th scope="row">Empresa 4</th>
                                                        <td>R20180005</td>
                                                        <td>15/08/2018</td>
                                                        <td>Kevin Salomon</td>
                                                        <td>Atendida</td>
                                                        <td>
                                                           <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                                <div class="demo-google-material-icon">
                                                                    <asp:LinkButton runat="server" ID="LinkButton7" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>                                                                
                                                                    <asp:LinkButton runat="server" ID="LinkButton8" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>

                                            <%--<asp:DataGrid runat="server" ID="dgRequisisiones" CssClass="table m-b-0">
                                                <Columns>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <th>Gasera</th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDgGasera"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <th>N° Requisición</th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDgNoRequisicion"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <th>Fecha Requerida</th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDgFechaRequerida"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <th>Solicitante</th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDgSolicitante"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <th>Estatus</th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="Estatus"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderTemplate>
                                                            <th>Accion</th>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <div class="col-sm-6 col-md-4 col-lg-3 col-xl-3">
                                                                <div class="demo-google-material-icon">
                                                                    <asp:LinkButton runat="server" ID="lbDgPDF" Text=" ">
                                                                    <i class="material-icons">picture_as_pdf</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>                                                                
                                                                    <asp:LinkButton runat="server" ID="lbDgOjo" Text=" ">
                                                                    <i class="material-icons">remove_red_eye</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                                    </asp:LinkButton>
                                                                </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>--%>
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
