package com.example.neotecknewts.sagasapp.Model;

import android.support.annotation.Nullable;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class EmpresaDTO {
    @SerializedName("IdEmpresa")
    private short IdEmpresa;

    @SerializedName("EsAdministracionCentral")
    private boolean EsAdministracionCentral;

    @SerializedName("NombreComercial")
    private String NombreComercial;

    @SerializedName("FechaRegistro")
    private Date FechaRegistro;

    @SerializedName("IdPais")
    private byte IdPais;

    @SerializedName("IdEstadoRep")
    private Byte IdEstadoRep;

    @SerializedName("EstadoProvincia")
    private String EstadoProvincia;

    @SerializedName("Municipio")
    private String Municipio;

    @SerializedName("CodigoPostal")
    private String CodigoPostal;

    @SerializedName("Colonia")
    private String Colonia;

    @SerializedName("Calle")
    private String Calle;

    @SerializedName("NumExt")
    private String NumExt;

    @SerializedName("NumInt")
    private String NumInt;

    @SerializedName("Telefono1")
    private String Telefono1;

    @SerializedName("Telefono2")
    private String Telefono2;

    @SerializedName("Telefono3")
    private String Telefono3;

    @SerializedName("Celular1")
    private String Celular1;

    @SerializedName("Celular2")
    private String Celular2;

    @SerializedName("Celular3")
    private String Celular3;

    @SerializedName("Email1")
    private String Email1;

    @SerializedName("Email2")
    private String Email2;

    @SerializedName("Email3")
    private String Email3;

    @SerializedName("SitioWeb1")
    private String SitioWeb1;

    @SerializedName("SitioWeb2")
    private String SitioWeb2;

    @SerializedName("SitioWeb3")
    private String SitioWeb3;

    @SerializedName("Rfc")
    private String Rfc;

    @SerializedName("RazonSocial")
    private String RazonSocial;

    @SerializedName("FactorLitrosAKilos")
    private Double FactorLitrosAKilos;

    @SerializedName("CierreInventario")
    private Date CierreInventario;

    @SerializedName("InventarioSano")
    private byte InventarioSano;

    @SerializedName("InventarioCrítico")
    private byte InventarioCrítico;

    @SerializedName("MaxRemaGaseraMensual")
    private Double MaxRemaGaseraMensual;

    @SerializedName("UrlLogotipoMenu")
    private String UrlLogotipoMenu;

    @SerializedName("UrlLogotipoLogin")
    private String UrlLogotipoLogin;

    public short getIdEmpresa() {
        return IdEmpresa;
    }

    public void setIdEmpresa(short idEmpresa) {
        IdEmpresa = idEmpresa;
    }

    public boolean getIdAdministracionCentral() {
        return EsAdministracionCentral;
    }

    public void setIdAdministracionCentral(boolean esAdministracionCentral) {
        EsAdministracionCentral = esAdministracionCentral;
    }

    public String getNombreComercial() {
        return NombreComercial;
    }

    public void setNombreComercial(String nombreComercial) {
        NombreComercial = nombreComercial;
    }

    public Date getFechaRegistro() {
        return FechaRegistro;
    }

    public void setFechaRegistro(Date fechaRegistro) {
        FechaRegistro = fechaRegistro;
    }

    public byte getIdPais() {
        return IdPais;
    }

    public void setIdPais(byte idPais) {
        IdPais = idPais;
    }

    public byte getIdEstadoRep() {
        return IdEstadoRep;
    }

    public void setIdEstadoRep(byte idEstadoRep) {
        IdEstadoRep = idEstadoRep;
    }

    public String getEstadoProvincia() {
        return EstadoProvincia;
    }

    public void setEstadoProvincia(String estadoProvincia) {
        EstadoProvincia = estadoProvincia;
    }

    public String getMunicipio() {
        return Municipio;
    }

    public void setMunicipio(String municipio) {
        Municipio = municipio;
    }

    public String getCodigoPostal() {
        return CodigoPostal;
    }

    public void setCodigoPostal(String codigoPostal) {
        CodigoPostal = codigoPostal;
    }

    public String getColonia() {
        return Colonia;
    }

    public void setColonia(String colonia) {
        Colonia = colonia;
    }

    public String getCalle() {
        return Calle;
    }

    public void setCalle(String calle) {
        Calle = calle;
    }

    public String getNumExt() {
        return NumExt;
    }

    public void setNumExt(String numExt) {
        NumExt = numExt;
    }

    public String getNumInt() {
        return NumInt;
    }

    public void setNumInt(String numInt) {
        NumInt = numInt;
    }

    public String getTelefono1() {
        return Telefono1;
    }

    public void setTelefono1(String telefono1) {
        Telefono1 = telefono1;
    }

    public String getTelefono2() {
        return Telefono2;
    }

    public void setTelefono2(String telefono2) {
        Telefono2 = telefono2;
    }

    public String getTelefono3() {
        return Telefono3;
    }

    public void setTelefono3(String telefono3) {
        Telefono3 = telefono3;
    }

    public String getCelular1() {
        return Celular1;
    }

    public void setCelular1(String celular1) {
        Celular1 = celular1;
    }

    public String getCelular2() {
        return Celular2;
    }

    public void setCelular2(String celular2) {
        Celular2 = celular2;
    }

    public String getCelular3() {
        return Celular3;
    }

    public void setCelular3(String celular3) {
        Celular3 = celular3;
    }

    public String getEmail1() {
        return Email1;
    }

    public void setEmail1(String email1) {
        Email1 = email1;
    }

    public String getEmail2() {
        return Email2;
    }

    public void setEmail2(String email2) {
        Email2 = email2;
    }

    public String getEmail3() {
        return Email3;
    }

    public void setEmail3(String email3) {
        Email3 = email3;
    }

    public String getSitioWeb1() {
        return SitioWeb1;
    }

    public void setSitioWeb1(String sitioWeb1) {
        SitioWeb1 = sitioWeb1;
    }

    public String getSitioWeb2() {
        return SitioWeb2;
    }

    public void setSitioWeb2(String sitioWeb2) {
        SitioWeb2 = sitioWeb2;
    }

    public String getSitioWeb3() {
        return SitioWeb3;
    }

    public void setSitioWeb3(String sitioWeb3) {
        SitioWeb3 = sitioWeb3;
    }

    public String getRfc() {
        return Rfc;
    }

    public void setRfc(String rfc) {
        Rfc = rfc;
    }

    public String getRazonSocial() {
        return RazonSocial;
    }

    public void setRazonSocial(String razonSocial) {
        RazonSocial = razonSocial;
    }

    public Double getFactorLitrosAKilos() {
        return FactorLitrosAKilos;
    }

    public void setFactorLitrosAKilos(Double factorLitrosAKilos) {
        FactorLitrosAKilos = factorLitrosAKilos;
    }

    public Date getCierreInventario() {
        return CierreInventario;
    }

    public void setCierreInventario(Date cierreInventario) {
        CierreInventario = cierreInventario;
    }

    public byte getInventarioSano() {
        return InventarioSano;
    }

    public void setInventarioSano(byte inventarioSano) {
        InventarioSano = inventarioSano;
    }

    public byte getInventarioCrítico() {
        return InventarioCrítico;
    }

    public void setInventarioCrítico(byte inventarioCrítico) {
        InventarioCrítico = inventarioCrítico;
    }

    public Double getMaxRemaGaseraMensual() {
        return MaxRemaGaseraMensual;
    }

    public void setMaxRemaGaseraMensual(Double maxRemaGaseraMensual) {
        MaxRemaGaseraMensual = maxRemaGaseraMensual;
    }

    public String getUrlLogotipoMenu() {
        return UrlLogotipoMenu;
    }

    public void setUrlLogotipoMenu(String urlLogotipoMenu) {
        UrlLogotipoMenu = urlLogotipoMenu;
    }

    public String getUrlLogotipoLogin() {
        return UrlLogotipoLogin;
    }

    public void setUrlLogotipoLogin(String urlLogotipoLogin) {
        UrlLogotipoLogin = urlLogotipoLogin;
    }
}
