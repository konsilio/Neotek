﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaEmpresa.aspx.cs" Inherits="Web.MainModule.Catalogos.Vista.NuevaEmpresa" %>

<asp:Content ID="ContentNuevaEmpresa" ContentPlaceHolderID="ctNuevaEmpresa" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="card">
                        <div class="row clearfix">
                            <div class="col-lg-5 col-md-5 col-sm-12">
                                <h2>&nbsp;Catálogo / Gasera / Nueva</h2>
                            </div>
                        </div>
                        <div class="body">
                            <div class="row clearfix">
                                <div class="col-md-3">
                                    <fieldset>
                                        <legend><b>Generales</b>
                                        </legend>
                                        Nombre Comercial:*
                                        <asp:TextBox runat="server" ID="txtNombreC" CssClass="form-control" />
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="col-md-3">
                                    <fieldset>
                                        <legend><b>Fiscales</b>
                                        </legend>
                                        Razón social:*
                                        <asp:TextBox runat="server" ID="txtRazonS" CssClass="form-control" />
                                        Rfc:*
                                        <asp:TextBox runat="server" ID="txtRfc" CssClass="form-control" />
                                    </fieldset>
                                </div>
                            </div>

                            <div class="row clearfix">
                                <div class="col-md-3">
                                    <fieldset>
                                        <legend><b>Contacto</b>
                                        </legend>
                                        Persona de contacto 1:
                                        <asp:TextBox runat="server" ID="txtPersona1" CssClass="form-control" />
                                        Teléfono 1:
                                        <asp:TextBox runat="server" ID="txtTel1" CssClass="form-control" />
                                        Celular 1:
                                        <asp:TextBox runat="server" ID="txtCel1" CssClass="form-control" />
                                        <div class="row clearfix">
                                            <div class="col-md-3">
                                        Persona de contacto 2:
                                        <asp:TextBox runat="server" ID="txtPersona2" CssClass="form-control" />
                                        Teléfono 2:
                                        <asp:TextBox runat="server" ID="txtTel2" CssClass="form-control" />
                                        Celular 2:
                                        <asp:TextBox runat="server" ID="txtCel2" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row clearfix">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

