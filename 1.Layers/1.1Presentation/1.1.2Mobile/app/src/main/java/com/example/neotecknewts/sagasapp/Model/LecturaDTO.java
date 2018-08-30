package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

/**
 * LecturaDTO
 * Modelo para la toma de lectura, contiene  los
 * atributos para la toma de lectura
 * @author  Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
 * @company Neoteck
 * @date 29/08/2018
 */
public class LecturaDTO implements Serializable {
    //region Variables privadas
    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("NombreTipoMedidor")
    private String NombreTipoMedidor;

    @SerializedName("CantidadFotografiasMedidor")
    private int CantidadFotografias;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesURL")
    private List<URI> ImagenesURI;

    @SerializedName("NombreEstacionCarburacion")
    private String NombreEstacionCarburacion;

    @SerializedName("IdEstacionCarburacion")
    private int IdEstacionCarburacion;

    @SerializedName("ImagenP500")
    private String ImagenP500;

    @SerializedName("ImagenP500URI")
    private URI ImagenP500URI;

    @SerializedName("CantidadP500")
    private int CantidadP500;

    @SerializedName("PorcentajeMedidor")
    private Double PorcentajeMedidor;

    @SerializedName("ClaveProceso")
    private String ClaveProceso;
    //endregion
    //region Constructores
    public LecturaDTO() {
        Imagenes = new ArrayList<>();
        ImagenesURI = new ArrayList<>();
    }
    //endregion
    //region Gets y Sets
    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public String getNombreTipoMedidor() {
        return NombreTipoMedidor;
    }

    public void setNombreTipoMedidor(String nombreTipoMedidor) {
        NombreTipoMedidor = nombreTipoMedidor;
    }

    public int getCantidadFotografias() {
        return CantidadFotografias;
    }

    public void setCantidadFotografias(int cantidadFotografias) {
        CantidadFotografias = cantidadFotografias;
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
        ImagenesURI = imagenesURI;
    }

    public String getNombreEstacionCarburacion() {
        return NombreEstacionCarburacion;
    }

    public void setNombreEstacionCarburacion(String nombreEstacionCarburacion) {
        NombreEstacionCarburacion = nombreEstacionCarburacion;
    }

    public int getIdEstacionCarburacion() {
        return IdEstacionCarburacion;
    }

    public void setIdEstacionCarburacion(int idEstacionCarburacion) {
        IdEstacionCarburacion = idEstacionCarburacion;
    }

    public String getImagenP500() {
        return ImagenP500;
    }

    public void setImagenP500(String imagenP500) {
        ImagenP500 = imagenP500;
    }

    public URI getImagenP500URI() {
        return ImagenP500URI;
    }

    public void setImagenP500URI(URI imagenP500URI) {
        ImagenP500URI = imagenP500URI;
    }

    public int getCantidadP500() {
        return CantidadP500;
    }

    public void setCantidadP500(int cantidadP500) {
        CantidadP500 = cantidadP500;
    }

    public Double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(Double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public String getClaveProceso() {
        return ClaveProceso;
    }

    public void setClaveProceso(String claveProceso) {
        ClaveProceso = claveProceso;
    }
    //endregion
}
