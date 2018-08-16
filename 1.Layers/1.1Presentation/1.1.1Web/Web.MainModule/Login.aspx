<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.MainModule.Login" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link rel="icon" href="favicon.ico" type="image/x-icon">
    <!-- Favicon-->
    <link rel="stylesheet" href="assets/plugins/bootstrap/css/bootstrap.min.css">
    <!-- Morris Chart Css-->
    <link rel="stylesheet" href="assets/plugins/morrisjs/morris.css" />
    <!-- Colorpicker Css -->
    <link rel="stylesheet" href="assets/plugins/bootstrap-colorpicker/css/bootstrap-colorpicker.css" />
    <!-- Multi Select Css -->
    <link rel="stylesheet" href="assets/plugins/multi-select/css/multi-select.css">
    <!-- Bootstrap Spinner Css -->
    <link rel="stylesheet" href="assets/plugins/jquery-spinner/css/bootstrap-spinner.css">
    <!-- Bootstrap Tagsinput Css -->
    <link rel="stylesheet" href="assets/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css">
    <!-- Bootstrap Select Css -->
    <link rel="stylesheet" href="assets/plugins/bootstrap-select/css/bootstrap-select.css" />
    <!-- noUISlider Css -->
    <link rel="stylesheet" href="assets/plugins/nouislider/nouislider.min.css" />
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
                                    <img src="assets/images/LogoCorpKonsilio183x183.png" alt="" />
                                    Corporativo Konsilio</h4>
                                <h3>SAGAS. Sistema de Administracion de Gas <strong>Ver. 1.0</strong></h3>
                                <p>
                                </p>
                                <div class="footer">
                                    <ul class="social_link list-unstyled">
                                        <%-- <li><a href="https://thememakker.com" title="ThemeMakker"><i class="zmdi zmdi-globe"></i></a></li>
                                        <li><a href="https://themeforest.net/user/thememakker" title="Themeforest"><i class="zmdi zmdi-shield-check"></i></a></li>
                                        <li><a href="https://www.linkedin.com/company/thememakker/" title="LinkedIn"><i class="zmdi zmdi-linkedin"></i></a></li>
                                        <li><a href="https://www.facebook.com/thememakkerteam" title="Facebook"><i class="zmdi zmdi-facebook"></i></a></li>
                                        <li><a href="http://twitter.com/thememakker" title="Twitter"><i class="zmdi zmdi-twitter"></i></a></li>
                                        <li><a href="http://plus.google.com/+thememakker" title="Google plus"><i class="zmdi zmdi-google-plus"></i></a></li>
                                        <li><a href="https://www.behance.net/thememakker" title="Behance"><i class="zmdi zmdi-behance"></i></a></li>--%>
                                    </ul>
                                    <hr>
                                    <ul>
                                        <%--     <li><a href="http://thememakker.com/contact/" target="_blank">Contactanos</a></li>
                                        <li><a href="http://thememakker.com/about/" target="_blank">Nosotros</a></li>
                                        <li><a href="http://thememakker.com/services/" target="_blank">Servicios</a></li>
                                        <li><a href="javascript:void(0);">FAQ</a></li>--%>
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
                                    <h5>Inicio de Sesion</h5>
                                </div>
                               
                                <div class="from">
                                    <div >
                                     <asp:DropDownList CssClass="selectpicker" data-live-search="true" runat="server" ID="ddlRazon"></asp:DropDownList>
                                    <%-- <select runat="server" id="ddlRazon" class="form-control z-index show-tick" data-show-subtext="true" data-live-search="true" title="Razon social">
                                        </select>--%>
                                        </div>
                                    <br />
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="Email" CssClass="form-control" placeholder="Usuario" />
                                        <span class="input-group-addon"><i class="zmdi zmdi-account-circle"></i></span>
                                    </div>
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="Password" TextMode="Password" class="form-control" placeholder="Contraseña" />
                                        <span class="input-group-addon"><i class="zmdi zmdi-lock"></i></span>
                                    </div>
                                    <%--  <div class="checkbox">
                                        <input id="remember_me" type="checkbox" />
                                        <label for="remember_me">
                                            Recordarme?
                                        </label>
                                    </div>--%>
                                </div>
                                <div class="footer">
                                    <asp:Button ID="btnIniciar" runat="server" OnClick="btnIniciar_Click" class="btn btn-primary btn-round btn-block" Text="Iniciar" />
                                    <%--<a href="Dashboard.aspx" class="btn btn-primary btn-round btn-block">Iniciar</a>--%>
                                    <%--<asp:Button ID="btnRegistra" runat="server" class="btn btn-primary btn-simple btn-round btn-block" Text="Registrate" />--%>
                                </div>
                                <div class="alert alert-danger" runat="server" id="divMensaje" visible="false">
                                    <asp:Label ID="lblMensaje" runat="server" Text="Error"></asp:Label>
                                </div>
                                <%--<a href="forgot-password.html" class="link">¿Perdiste tu contraseña?</a>--%>
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
    <!-- Lib Scripts Plugin Js -->
    <script src="assets/bundles/vendorscripts.bundle.js"></script>
    <!-- Lib Scripts Plugin Js -->

    <script src="../../assets/plugins/bootstrap-colorpicker/js/bootstrap-colorpicker.js"></script>
    <!-- Bootstrap Colorpicker Js -->
    <script src="../../assets/plugins/jquery-inputmask/jquery.inputmask.bundle.js"></script>
    <!-- Input Mask Plugin Js -->
    <script src="../../assets/plugins/multi-select/js/jquery.multi-select.js"></script>
    <!-- Multi Select Plugin Js -->
    <script src="../../assets/plugins/jquery-spinner/js/jquery.spinner.js"></script>
    <!-- Jquery Spinner Plugin Js -->
    <script src="../../assets/plugins/bootstrap-tagsinput/bootstrap-tagsinput.js"></script>
    <!-- Bootstrap Tags Input Plugin Js -->
    <script src="../../assets/plugins/nouislider/nouislider.js"></script>
    <!-- noUISlider Plugin Js -->
    <script src="../../assets/plugins/bootstrap-select/css/bootstrap-select.css"></script>

    <script src="../../assets/bundles/mainscripts.bundle.js"></script>
    <!-- Custom Js -->
    <script src="../../assets/js/pages/forms/advanced-form-elements.js"></script>
</body>
</html>
