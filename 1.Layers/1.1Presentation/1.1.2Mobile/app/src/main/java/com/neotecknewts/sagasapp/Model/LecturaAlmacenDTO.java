package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

/**
 * <h3>LecturaAlmacenDTO</h3>
 * Clase modelo para las lecturas inicial y final de los
 * almacenes
 * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
 * @commpany Neoteck
 * @date 04/09/2018
 * @update 04/09/2018
 */
public class LecturaAlmacenDTO implements Serializable {
    @SerializedName("IdTipoMedidor")
    private int IdTipoMedior;

    @SerializedName("NombreTipoMedidor")
    private String NombreTipoMedidor;

    @SerializedName("CantidadFotografiasMedidor")
    private int CantidadFotografias;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesURI")
    private List<URI> ImagenesURI;

    @SerializedName("IdCAlmacenGas")
    private int IdAlmacen;

    @SerializedName("NombreAlmacen")
    private String NombreAlmacen;

    @SerializedName("PorcentajeMedidor")
    private double PorcentajeMedidor;

    @SerializedName("ClaveProceso")
    private String ClaveOperacion;

    @SerializedName("FechaAplicacion")
    private String FechaAplicacion;

    private double CapacidadAlmacen;

    public LecturaAlmacenDTO(){
        Imagenes = new ArrayList<>();
        ImagenesURI = new ArrayList<>();
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public int getIdAlmacen() {
        return IdAlmacen;
    }

    public void setIdAlmacen(int idAlmacen) {
        IdAlmacen = idAlmacen;
    }

    public String getNombreAlmacen() {
        return NombreAlmacen;
    }

    public void setNombreAlmacen(String nombreAlmacen) {
        NombreAlmacen = nombreAlmacen;
    }

    public int getIdTipoMedior() {
        return IdTipoMedior;
    }

    public void setIdTipoMedior(int idTipoMedior) {
        IdTipoMedior = idTipoMedior;
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

    public void setCantidadFotografias(int numeroDeFotos) {
        CantidadFotografias = numeroDeFotos;
    }

    public double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public List<URI> getImagenesURI() {
        return ImagenesURI;
    }

    public void setImagenesURI(List<URI> imagenesURI) {
        ImagenesURI = imagenesURI;
    }

    public List<String> getImagenes() {
        return Imagenes;
    }

    public void setImagenes(List<String> imagenes) {
        Imagenes = imagenes;
    }

    public String getFechaAplicacion() {
        return FechaAplicacion;
    }

    public void setFechaAplicacion(String fechaAplicacion) {
        FechaAplicacion = fechaAplicacion;
    }

    public double getCapacidadAlmacen() {
        return CapacidadAlmacen;
    }

    public void setCapacidadAlmacen(double capacidadAlmacen) {
        CapacidadAlmacen = capacidadAlmacen;
    }
}
