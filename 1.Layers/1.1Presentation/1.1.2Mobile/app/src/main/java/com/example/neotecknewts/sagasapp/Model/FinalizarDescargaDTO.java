package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 14/08/18.
 */

public class FinalizarDescargaDTO implements Serializable{
    private int IdOrdenCompra;
    private String NombreTipoMedidorTractor;
    private String NombreTipoMedidorAlmacen;
    private int IdTipoMedidorTractor;
    private int IdTipoMedidorAlmacen;
    private int CantidadFotosAlmacen;
    private int CantidadFotosTractor;
    private Double PorcentajeMedidorAlmacen;
    private Double PorcentajeMedidorTractor;
    private int IdAlmacen;
    @SerializedName("Imagenes")
    private List<byte[]> Imagenes;

    @SerializedName("ImagenesURL")
    private List<URI> ImagenesURI;

    public FinalizarDescargaDTO(){
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

    public Double getPorcentajeMedidorAlmacen() {
        return PorcentajeMedidorAlmacen;
    }

    public void setPorcentajeMedidorAlmacen(Double porcentajeMedidorAlmacen) {
        PorcentajeMedidorAlmacen = porcentajeMedidorAlmacen;
    }

    public List<byte[]> getImagenes() {
        return Imagenes;
    }

    public void setImagenes(List<byte[]> imagenes) {
        Imagenes = imagenes;
    }

    public List<URI> getImagenesURI() {
        return ImagenesURI;
    }

    public void setImagenesURI(List<URI> imagenesURI) {
        ImagenesURI = imagenesURI;
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
}
