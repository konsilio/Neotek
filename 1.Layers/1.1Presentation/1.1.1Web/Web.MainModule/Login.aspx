<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.MainModule.Login" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css">

    <!-- Custom Css -->
    <link rel="stylesheet" href="assets/css/main.css">
    <link rel="stylesheet" href="assets/css/color_skins.css">
</head>
<body class="theme-black">
    <form runat="server">
        <div class="authentication">
            <div class="container">
                <div class="col-md-12 content-center">
                    <div class="row">
                        <div class="col-lg-6 col-md-12">
                            <div class="company_detail">
                                <h4 class="logo">
                                    <img src="assets/images/logo.svg" alt="">
                                    Alfa</h4>
                                <h3>Gas Mundial<strong>Ver. 1.0</strong></h3>
                                <p>Ingrese los datos de inicio</p>
                                <div class="footer">
                                    <ul class="social_link list-unstyled">
                                        <li><a href="https://thememakker.com" title="ThemeMakker"><i class="zmdi zmdi-globe"></i></a></li>
                                        <li><a href="https://themeforest.net/user/thememakker" title="Themeforest"><i class="zmdi zmdi-shield-check"></i></a></li>
                                        <li><a href="https://www.linkedin.com/company/thememakker/" title="LinkedIn"><i class="zmdi zmdi-linkedin"></i></a></li>
                                        <li><a href="https://www.facebook.com/thememakkerteam" title="Facebook"><i class="zmdi zmdi-facebook"></i></a></li>
                                        <li><a href="http://twitter.com/thememakker" title="Twitter"><i class="zmdi zmdi-twitter"></i></a></li>
                                        <li><a href="http://plus.google.com/+thememakker" title="Google plus"><i class="zmdi zmdi-google-plus"></i></a></li>
                                        <li><a href="https://www.behance.net/thememakker" title="Behance"><i class="zmdi zmdi-behance"></i></a></li>
                                    </ul>
                                    <hr>
                                    <ul>
                                        <li><a href="http://thememakker.com/contact/" target="_blank">Contactanos</a></li>
                                        <li><a href="http://thememakker.com/about/" target="_blank">Nosotros</a></li>
                                        <li><a href="http://thememakker.com/services/" target="_blank">Servicios</a></li>
                                        <li><a href="javascript:void(0);">FAQ</a></li>
                                    </ul>
                                    <ul>
                                        <%--   <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                        <div class="alert alert-danger" role="alert">
                                            <asp:Literal runat="server" ID="FailureText" />
                                        </div>
                                        <asp:PlaceHolder>--%>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5 col-md-12 offset-lg-1">
                            <div class="card-plain">
                                <div class="header">
                                    <h5>Log in</h5>
                                </div>
                                <form class="form">                                    
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="Email" CssClass="form-control" class="form-control" placeholder="Usuario" />
                                        <span class="input-group-addon"><i class="zmdi zmdi-account-circle"></i></span>
                                    </div>
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="Password" TextMode="Password" class="form-control" placeholder="Contraseña"/>
                                        <span class="input-group-addon"><i class="zmdi zmdi-lock"></i></span>
                                    </div>
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <div class="input-group-text">
                                                <asp:CheckBox runat="server" ID="RememberMe" />
                                                <asp:Label runat="server" AssociatedControlID="RememberMe">¿Recordar cuenta?</asp:Label>
                                            </div>
                                        </div>
                                        <input type="text" class="form-control" aria-label="Text input with checkbox">
                                    </div>
                                </form>
                                <div class="footer">
                                    <asp:Button ID="btnIniciar" runat="server" OnClick="btnIniciar_Click" class="btn btn-primary btn-round btn-block" Text="Inicar"/>
                                    <asp:Button ID="btnRegistra" runat="server"  class="btn btn-primary btn-simple btn-round btn-block" Text="Registrate" />
                                </div>
                                <a href="forgot-password.html" class="link">¿Perdiste tu contraseña?</a>
                            </div>
                        </div>
                        <p>
                            <%--<asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Registrarse como usuario nuevo</asp:HyperLink>--%>
                        </p>
                        <p>
                            <%-- Habilite esta opción cuando haya habilitado la confirmación de cuentas para la funcionalidad de restablecimiento de contraseña--%>
                           <%-- <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">¿Olvidó su contraseña?</asp:HyperLink>--%>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/bundles/libscripts.bundle.js"></script>
    <script src="assets/bundles/vendorscripts.bundle.js"></script>
    <!-- Lib Scripts Plugin Js -->
</body>
</html>
