﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="Web.MainModule.Catalogos.Empresas" %>

<asp:Content ID="ContentEmpresa" ContentPlaceHolderID="ctEmpresa" runat="server">
    <section class="content home">
        <div class="container-fluid">
            <div class="block-header">
                <div class="row clearfix">
                    <div class="card">
                        <div class="body">
                            <div class="row clearfix">
                                <div class="col-md-3">
                                    <b>Gasera</b>
                                    <asp:DropDownList ID="ddlEmpresas" CssClass="form-control show-tick" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">                                    
                                        <b>Estatus</b>
                                         <asp:DropDownList ID="ddlStatus" CssClass="form-control show-tick" runat="server" Visible="true" AutoPostBack="true">
                                    </asp:DropDownList>
                                  
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="col-md-7">
                                    <asp:Button ID="BtnNuevo" CssClass="btn btn-raised btn-primary float-right" runat="server" Text="Nueva" OnClick="BtnNuevo_Click" />
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvEmpresas" AutoGenerateColumns="false" OnRowCommand="gvEmpresas_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Logo" DataField="UrlLogotipoLogin" />
                                            <asp:BoundField HeaderText="Razón social" DataField="RazonSocial" />
                                            <asp:BoundField HeaderText="RFC" DataField="Rfc" />
                                            <asp:BoundField HeaderText="Contacto" DataField="Persona1" />
                                            <asp:BoundField HeaderText="Celular" DataField="Celular1" />
                                            <asp:BoundField HeaderText="Telefono" DataField="Telefono1" />
                                            <asp:BoundField HeaderText="Correo" DataField="Email1" />

                                            <asp:BoundField HeaderText="Calle" DataField="Calle" visible="false"/>
                                            <asp:BoundField HeaderText="Celular2" DataField="Celular2" visible="false"/>
                                            <asp:BoundField HeaderText="Celular3" DataField="Celular3" visible="false"/>

                                            <asp:BoundField HeaderText="Correo2" DataField="Email2" visible="false"/>
                                            <asp:BoundField HeaderText="Correo3" DataField="Email3" visible="false"/>
                                            <asp:BoundField HeaderText="CierreInventario" DataField="CierreInventario" visible="false"/>
                                            <asp:BoundField HeaderText="CodigoPostal" DataField="CodigoPostal" visible="false"/>
                                            <asp:BoundField HeaderText="Colonia" DataField="Colonia" visible="false"/>
                                            <asp:BoundField HeaderText="EsAdministracionCentral" DataField="EsAdministracionCentral" visible="false"/>
                                            <asp:BoundField HeaderText="EstadoProvincia" DataField="EstadoProvincia" visible="false"/>
                                            <asp:BoundField HeaderText="FactorLitrosAKilos" DataField="FactorLitrosAKilos" visible="false"/>
                                            <asp:BoundField HeaderText="FechaRegistro" DataField="FechaRegistro" visible="false"/>
                                            <asp:BoundField HeaderText="IdAdministracionCentral" DataField="IdAdministracionCentral" visible="false"/>

                                            <asp:BoundField HeaderText="IdEmpresa" DataField="IdEmpresa" visible="false"/>
                                            <asp:BoundField HeaderText="IdEstadoRep" DataField="IdEstadoRep" visible="false"/>
                                            <asp:BoundField HeaderText="IdPais" DataField="IdPais" visible="false"/>
                                            <asp:BoundField HeaderText="InventarioCritico" DataField="InventarioCritico" visible="false"/>
                                            <asp:BoundField HeaderText="InventarioSano" DataField="InventarioSano" visible="false"/>
                                            <asp:BoundField HeaderText="MaxRemaGaseraMensual" DataField="MaxRemaGaseraMensual" visible="false"/>
                                            <asp:BoundField HeaderText="Municipio" DataField="Municipio" visible="false"/>
                                            <asp:BoundField HeaderText="NombreComercial" DataField="NombreComercial" visible="false"/>
                                            <asp:BoundField HeaderText="NumExt" DataField="NumExt" visible="false"/>
                                            <asp:BoundField HeaderText="NumInt" DataField="NumInt" visible="false"/>

                                            <asp:BoundField HeaderText="SitioWeb1" DataField="SitioWeb1" visible="false"/>
                                            <asp:BoundField HeaderText="SitioWeb2" DataField="SitioWeb2" visible="false"/>
                                            <asp:BoundField HeaderText="SitioWeb3" DataField="SitioWeb3" visible="false"/>
                                            <asp:BoundField HeaderText="Telefono2" DataField="Telefono2" />
                                            <asp:BoundField HeaderText="Telefono3" DataField="Telefono3" />
                                            <asp:BoundField HeaderText="UrlLogotipoMenu" DataField="UrlLogotipoMenu" />
                                            <asp:BoundField HeaderText="Contacto2" DataField="Persona2" />
                                            <asp:BoundField HeaderText="Contacto3" DataField="Persona3" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <p>Acciones</p>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbEdit" CommandName="Editar" CommandArgument='<%# Eval("IdEmpresa") %>'>
                                                                    <i class="material-icons">edit</i>                                                                    
                                                                    <span class="icon-name"></span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbBorrar" CommandName="Borrar" CommandArgument='<%# Eval("IdEmpresa") %>'>
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
</asp:Content>
