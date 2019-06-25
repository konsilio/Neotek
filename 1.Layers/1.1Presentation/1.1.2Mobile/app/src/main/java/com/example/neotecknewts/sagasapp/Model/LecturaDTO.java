package com.example.neotecknewts.sagasapp.Model;

import android.util.Log;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.Date;
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

    @SerializedName("IdCAlmacenGas")
    private int IdEstacionCarburacion;

    @SerializedName("ImagenP5000")
    private String ImagenP5000;

    @SerializedName("ImagenP5000URI")
    private URI ImagenP5000URI;

    @SerializedName("CantidadP5000")
    private int CantidadP5000;

    @SerializedName("PorcentajeMedidor")
    private Double PorcentajeMedidor;

    @SerializedName("ClaveProceso")
    private String ClaveProceso;

    @SerializedName("FechaAplicacion")
    private Date FechaAplicacion;

    private double CapacidadAlmacen;
    //endregion

    public Date getFechaAplicacion() {
        return FechaAplicacion;
    }

    public void setFechaAplicacion(Date fechaAplicacion) {
        FechaAplicacion = fechaAplicacion;
    }

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

    public String getImagenP5000() {
        return ImagenP5000;
    }

    public void setImagenP5000(String imagenP5000) {
        ImagenP5000 = imagenP5000;
    }

    public URI getImagenP5000URI() {
        return ImagenP5000URI;
    }

    public void setImagenP5000URI(URI imagenP5000URI) {
        ImagenP5000URI = imagenP5000URI;
    }

    public int getCantidadP5000() {
        Log.d("getCantidadP5000", CantidadP5000+"");
        return CantidadP5000;
    }
    public void setCantidadP5000(int cantidadP500) {
        CantidadP5000 = cantidadP500;
    }

    public Double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(Double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public String getClaveProceso()
    {
        Log.d("claveproceso",ClaveProceso+"");
        if(ClaveProceso.isEmpty()){

        }
        return ClaveProceso;
    }

    public void setClaveProceso(String claveProceso) {

        ClaveProceso = claveProceso;
    }

    public double getCapacidadAlmacen() {
        return CapacidadAlmacen;
    }

    public void setCapacidadAlmacen(double capacidadAlmacen) {
        CapacidadAlmacen = capacidadAlmacen;
    }

    //endregion
}
