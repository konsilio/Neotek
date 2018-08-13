package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import android.net.Uri;

import java.net.URI;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */
public class PrecargaPapeletaDTO implements Serializable {

    @SerializedName("IdOrdenCompraExpedidor")
    private int IdOrdenCompraExpedidor;

    @SerializedName("IdOrdenCompraPorteador")
    private int IdOrdenCompraPorteador;

    @SerializedName("IdProveedorPorteador")
    private int IdProveedorPorteador;

    @SerializedName("IdProveedorExpedidor")
    private int IdProveedorExpedidor;

    @SerializedName("Fecha")
    private Date Fecha;

    @SerializedName("FechaEmbarque")
    private Date FechaEmbarque;

    @SerializedName("NumeroEmbarque")
    private String NumeroEmbarque;

    @SerializedName("PlacasTractor")
    private String PlacasTractor;

    @SerializedName("NombreOperador")
    private String NombreOperador;

    @SerializedName("Producto")
    private String Producto;

    @SerializedName("NumeroTanque")
    private String NumeroTanque;

    @SerializedName("PresionTanque")
    private Double PresionTanque;

    @SerializedName("CapacidadTanque")
    private Double CapacidadTanque;

    @SerializedName("PorcentajeTanque")
    private Double PorcentajeTanque;

    @SerializedName("Masa")
    private Double Masa;

    @SerializedName("Sello")
    private String Sello;

    @SerializedName("ValorCarga")
    private Double ValorCarga;

    @SerializedName("NombreResponsable")
    private String NombreResponsable;

    @SerializedName("Imagenes")
    private List<byte[]> Imagenes;

    @SerializedName("ImagenesURL")
    private List<URI> ImagenesURI;


    public PrecargaPapeletaDTO(){
        Imagenes = new ArrayList<>();
        ImagenesURI = new ArrayList<>();
    }
    public int getIdOrdenCompraExpedidor() {
        return IdOrdenCompraExpedidor;
    }

    public void setIdOrdenCompraExpedidor(int idOrdenCompraExpedidor) {
        IdOrdenCompraExpedidor = idOrdenCompraExpedidor;
    }

    public int getIdOrdenCompraPorteador() {
        return IdOrdenCompraPorteador;
    }

    public void setIdOrdenCompraPorteador(int idOrdenCompraPorteador) {
        IdOrdenCompraPorteador = idOrdenCompraPorteador;
    }

    public int getIdProveedorPorteador() {
        return IdProveedorPorteador;
    }

    public void setIdProveedorPorteador(int idProveedorPorteador) {
        IdProveedorPorteador = idProveedorPorteador;
    }

    public Date getFecha() {
        return Fecha;
    }

    public void setFecha(Date fecha) {
        Fecha = fecha;
    }

    public Date getFechaEmbarque() {
        return FechaEmbarque;
    }

    public void setFechaEmbarque(Date fechaEmbarque) {
        FechaEmbarque = fechaEmbarque;
    }

    public String getNumeroEmbarque() {
        return NumeroEmbarque;
    }

    public void setNumeroEmbarque(String numeroEmbarque) {
        NumeroEmbarque = numeroEmbarque;
    }

    public String getPlacasTractor() {
        return PlacasTractor;
    }

    public void setPlacasTractor(String placasTractor) {
        PlacasTractor = placasTractor;
    }

    public String getNombreOperador() {
        return NombreOperador;
    }

    public void setNombreOperador(String nombreOperador) {
        NombreOperador = nombreOperador;
    }

    public String getProducto() {
        return Producto;
    }

    public void setProducto(String producto) {
        Producto = producto;
    }

    public String getNumeroTanque() {
        return NumeroTanque;
    }

    public void setNumeroTanque(String numeroTanque) {
        NumeroTanque = numeroTanque;
    }

    public Double getPresionTanque() {
        return PresionTanque;
    }

    public void setPresionTanque(Double presionTanque) {
        PresionTanque = presionTanque;
    }

    public Double getCapacidadTanque() {
        return CapacidadTanque;
    }

    public void setCapacidadTanque(Double capacidadTanque) {
        CapacidadTanque = capacidadTanque;
    }

    public Double getPorcentajeTanque() {
        return PorcentajeTanque;
    }

    public void setPorcentajeTanque(Double porcentajeTanque) {
        PorcentajeTanque = porcentajeTanque;
    }

    public Double getMasa() {
        return Masa;
    }

    public void setMasa(Double masa) {
        Masa = masa;
    }

    public String getSello() {
        return Sello;
    }

    public void setSello(String sello) {
        Sello = sello;
    }

    public Double getValorCarga() {
        return ValorCarga;
    }

    public void setValorCarga(Double valorCarga) {
        ValorCarga = valorCarga;
    }

    public String getNombreResponsable() {
        return NombreResponsable;
    }

    public void setNombreResponsable(String nombreResponsable) {
        NombreResponsable = nombreResponsable;
    }

    public int getIdProveedorExpedidor() {
        return IdProveedorExpedidor;
    }

    public void setIdProveedorExpedidor(int idProveedorExpedidor) {
        IdProveedorExpedidor = idProveedorExpedidor;
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
}