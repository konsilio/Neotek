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
                                <li class="breadcrumb-item active"><asp:Label runat="server" ID="lblRuta" Text="Compras / Requisición / Nueva"></asp:Label> </li>

                                <%--<asp:Label ID="lblMensaje" runat="server"></asp:Label>--%>
                                <%--<asp:Button runat="server" ID="btnCompra" OnClick="btnCompra_Click" CssClass="form-control" Text="Compras" />--%>
                            </ul>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="header">
                                    <h2><strong>Input</strong> <small>Different sizes and widths</small> </h2>                        
                                </div>
                                <div class="body">
                                    <h2 class="card-inside-title">Basic Examples</h2>
                                    <div class="row clearfix">
                                        <div class="col-sm-12">
                                            <div class="form-group">                                    
                                                <input type="text" class="form-control" placeholder="Username" />
                                            </div>
                                            <div class="form-group">                                   
                                                <input type="password" class="form-control" placeholder="Password" />
                                            </div>
                                        </div>
                                    </div>
                                    <h2 class="card-inside-title">Different Widths</h2>
                                    <div class="row clearfix">
                                        <div class="col-sm-6">
                                            <div class="form-group">                                    
                                                <input type="text" class="form-control" placeholder="col-sm-6" />                                   
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-6" />                                    
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-sm-4">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-4" />                                    
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-4" />                                   
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-4" />                                    
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row clearfix">
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-3" />                                   
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-3" />                                   
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-3" />                                   
                                            </div>
                                        </div>
                                        <div class="col-lg-3 col-md-6">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" placeholder="col-sm-3" />                                   
                                            </div>
                                        </div>
                                    </div>                        
                                    <h2 class="card-inside-title">Input Status</h2>
                                    <div class="row clearfix">
                                        <div class="col-sm-6">
                                            <div class="form-group">                                   
                                                <input type="text" class="form-control" value="Focused" placeholder="Statu Focused" />                                    
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">                                    
                                                <input type="text" class="form-control" placeholder="Disabled" disabled />                                   
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
