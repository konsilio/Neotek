package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 14/08/18.
 */

public class IniciarDescargaDTO implements Serializable {

    @SerializedName("IdOrdenCompra")
    private int IdOrdenCompra;

    @SerializedName("NombreTipoMedidorTractor")
    private String NombreTipoMedidorTractor;

    @SerializedName("NombreTipoMedidorAlmacen")
    private String NombreTipoMedidorAlmacen;

    @SerializedName("IdTipoMedidorTractor")
    private int IdTipoMedidorTractor;

    @SerializedName("IdTipoMedidorAlmacen")
    private int IdTipoMedidorAlmacen;

    @SerializedName("CantidadFotosAlmacen")
    private int CantidadFotosAlmacen;

    @SerializedName("CantidadFotosTractor")
    private int CantidadFotosTractor;

    @SerializedName("TanquePrestado")
    private boolean TanquePrestado;

    @SerializedName("PorcentajeMedidorAlmacen")
    private Double PorcentajeMedidorAlmacen;

    @SerializedName("PorcentajeMedidorTractor")
    private Double PorcentajeMedidorTractor;

    @SerializedName("IdAlmacen")
    private int IdAlmacen;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesURL")
    private List<URI> ImagenesURI;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("FechaDescarga")
    private String FechaDescarga;

    public IniciarDescargaDTO(){
        Imagenes = new ArrayList<>();
        ImagenesURI = new ArrayList<>();
    }

    public int getIdOrdenCompra() {
        return IdOrdenCompra;
    }

    public void setIdOrdenCompra(int idOrdenCompra) {
        IdOrdenCompra = idOrdenCompra;
    }

    public int getIdTipoMedidorTractor() {
        return IdTipoMedidorTractor;
    }

    public void setIdTipoMedidorTractor(int idTipoMedidorTractor) {
        IdTipoMedidorTractor = idTipoMedidorTractor;
    }

    public int getIdTipoMedidorAlmacen() {
        return IdTipoMedidorAlmacen;
    }

    public void setIdTipoMedidorAlmacen(int idTipoMedidorAlmacen) {
        IdTipoMedidorAlmacen = idTipoMedidorAlmacen;
    }

    public boolean isTanquePrestado() {
        return TanquePrestado;
    }

    public void setTanquePrestado(boolean tanquePrestado) {
        TanquePrestado = tanquePrestado;
    }

    public Double getPorcentajeMedidorAlmacen() {
        return PorcentajeMedidorAlmacen;
    }

    public void setPorcentajeMedidorAlmacen(Double porcentajeMedidorAlmacen) {
        PorcentajeMedidorAlmacen = porcentajeMedidorAlmacen;
    }

    public List<String> getImagenes() {
        return Imagenes;
    }

    public void setImagenes(List<String> imagenes) {
        Imagenes = new ArrayList<>();
    }

    public List<URI> getImagenesURI() {
        return ImagenesURI;
    }

    public void setImagenesURI(List<URI> imagenesURI) {
        ImagenesURI = new ArrayList<>();
    }

    public Double getPorcentajeMedidorTractor() {
        return PorcentajeMedidorTractor;
    }

    public void setPorcentajeMedidorTractor(Double porcentajeMedidorTractor) {
        PorcentajeMedidorTractor = porcentajeMedidorTractor;
    }

    public String getNombreTipoMedidorTractor() {
        return NombreTipoMedidorTractor;
    }

    public void setNombreTipoMedidorTractor(String nombreTipoMedidorTractor) {
        NombreTipoMedidorTractor = nombreTipoMedidorTractor;
    }

    public String getNombreTipoMedidorAlmacen() {
        return NombreTipoMedidorAlmacen;
    }

    public void setNombreTipoMedidorAlmacen(String nombreTipoMedidorAlmacen) {
        NombreTipoMedidorAlmacen = nombreTipoMedidorAlmacen;
    }

    public int getCantidadFotosAlmacen() {
        return CantidadFotosAlmacen;
    }

    public void setCantidadFotosAlmacen(int cantidadFotosAlmacen) {
        CantidadFotosAlmacen = cantidadFotosAlmacen;
    }

    public int getCantidadFotosTractor() {
        return CantidadFotosTractor;
    }

    public void setCantidadFotosTractor(int cantidadFotosTractor) {
        CantidadFotosTractor = cantidadFotosTractor;
    }

    public int getIdAlmacen() {
        return IdAlmacen;
    }

    public void setIdAlmacen(int idAlmacen) {
        IdAlmacen = idAlmacen;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public String getFechaDescarga() {
        return FechaDescarga;
    }

    public void setFechaDescarga(String fechaDescarga) {
        FechaDescarga = fechaDescarga;
    }

    @Override
    public String toString() {
        return "IniciarDescargaDTO{" +
                "IdOrdenCompra=" + IdOrdenCompra +
                ", NombreTipoMedidorTractor='" + NombreTipoMedidorTractor + '\'' +
                ", NombreTipoMedidorAlmacen='" + NombreTipoMedidorAlmacen + '\'' +
                ", IdTipoMedidorTractor=" + IdTipoMedidorTractor +
                ", IdTipoMedidorAlmacen=" + IdTipoMedidorAlmacen +
                ", CantidadFotosAlmacen=" + CantidadFotosAlmacen +
                ", CantidadFotosTractor=" + CantidadFotosTractor +
                ", TanquePrestado=" + TanquePrestado +
                ", PorcentajeMedidorAlmacen=" + PorcentajeMedidorAlmacen +
                ", PorcentajeMedidorTractor=" + PorcentajeMedidorTractor +
                ", IdAlmacen=" + IdAlmacen +
                ", Imagenes=" + Imagenes +
                ", ImagenesURI=" + ImagenesURI +
                ", ClaveOperacion='" + ClaveOperacion + '\'' +
                ", FechaDescarga='" + FechaDescarga + '\'' +
                '}';
    }
}
