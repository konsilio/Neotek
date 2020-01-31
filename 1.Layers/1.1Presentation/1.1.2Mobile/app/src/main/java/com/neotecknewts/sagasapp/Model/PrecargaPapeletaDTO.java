package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
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
    private String Fecha;

    @SerializedName("FechaEmbarque")
    private String FechaEmbarque;

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

    @SerializedName("MasaKg")
    private Double Masa;

    @SerializedName("Sello")
    private String Sello;

    @SerializedName("ValorCarga")
    private Double ValorCarga;

    @SerializedName("NombreResponsable")
    private String NombreResponsable;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesURL")
    private List<URI> ImagenesURI;

    @SerializedName("PorcentajeMedidor")
    private Double PorcentajeMedidor;

    @SerializedName("NombreTipoMedidorTractor")
    private String NombreTipoMedidorTractor;

    @SerializedName("IdTipoMedidorTractor")
    private int IdTipoMedidorTractor;

    @SerializedName("CantidadFotosTractor")
    private int CantidadFotosTractor;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("CapacidadTanqueKg")
    private Double CapacidadTanqueKg;

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

    public String getFecha() {
        return Fecha;
    }

    public void setFecha(String fecha) {
        Fecha = fecha;
    }

    public String getFechaEmbarque() {
        return FechaEmbarque;
    }

    public void setFechaEmbarque(String fechaEmbarque) {
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

    public List<String> getImagenes() {
        return Imagenes;
    }

    public void setImagenes(List<String> imagenes) {
        Imagenes = imagenes;
    }

    public List<URI> getImagenesURI() {
        return ImagenesURI;
    }

    public void setImagenesURI(List<URI> imagenesURI) {
        ImagenesURI = new ArrayList<>();
    }

    public Double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(Double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public String getNombreTipoMedidorTractor() {
        return NombreTipoMedidorTractor;
    }

    public void setNombreTipoMedidorTractor(String nombreTipoMedidorTractor) {
        NombreTipoMedidorTractor = nombreTipoMedidorTractor;
    }

    public int getIdTipoMedidorTractor() {
        return IdTipoMedidorTractor;
    }

    public void setIdTipoMedidorTractor(int idTipoMedidorTractor) {
        IdTipoMedidorTractor = idTipoMedidorTractor;
    }

    public int getCantidadFotosTractor() {
        return CantidadFotosTractor;
    }

    public void setCantidadFotosTractor(int cantidadFotosTractor) {
        CantidadFotosTractor = cantidadFotosTractor;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String Clave) {
        ClaveOperacion = "0130000000";
    }

    public Double getCapacidadTanqueKg() {
        return CapacidadTanqueKg;
    }

    public void setCapacidadTanqueKg(Double capacidadTanqueKg) {
        CapacidadTanqueKg = capacidadTanqueKg;
    }
}