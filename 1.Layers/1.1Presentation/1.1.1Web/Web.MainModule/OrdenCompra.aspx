<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdenCompra.aspx.cs" Inherits="Web.MainModule.OrdenCompra" %>

<asp:Content ID="OrdenCompra" ContentPlaceHolderID="ctOrdenCompra" runat="server">
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
                                    <asp:Label runat="server" ID="lblRuta" Text="Compras / Ordern de Compra "></asp:Label>
                                </li>
                                <%--<asp:Label ID="lblMensaje" runat="server"></asp:Label>--%>
                                <%--<asp:Button runat="server" ID="btnCompra" OnClick="btnCompra_Click" CssClass="form-control" Text="Compras" />--%>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>
</asp:Content>
