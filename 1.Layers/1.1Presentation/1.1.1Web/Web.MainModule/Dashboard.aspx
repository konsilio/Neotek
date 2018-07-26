<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Web.MainModule.Dashboard" %>
<asp:Content ID="ctDashBoard" ContentPlaceHolderID="ctDashBoard" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="col-lg-5 col-md-5 col-sm-12">
                        <h2>Dashboard</h2>
                        <ul class="breadcrumb padding-0">
                            <li class="breadcrumb-item"><a href="Dashboard.aspx"><i class="zmdi zmdi-home"></i></a></li>
                            <li class="breadcrumb-item active">Dashboard</li>
                        </ul>
                        <asp:Label ID="lblMensaje" runat="server" Text="Sin token"></asp:Label>
                    </div>                    
                </div>
            </div>          
        </div>
    </section>
</asp:Content>
