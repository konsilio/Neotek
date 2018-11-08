/*
 * CalibracionDTO
 * Clase que sirbe de modelo DTO para los datos de la calibración de gas
 * @author Jorge Omar Tovar Martínez
 * @company Neoteck
 * @date   25/09/2018 16:30
 * @udpate 07/11/2018 10:14
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class CalibracionDTO implements Serializable {

    @SerializedName("IdCalmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("NombreCAlmacenGas")
    private String NombreCAlmacenGas;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("NombreMedidor")
    private String NombreMedidor;

    @SerializedName("PorcentajeMedidor1")
    private double PorcentajeCalibracion;

    @SerializedName("IdDestinoCalibracion")
    private int IdDestinoCalibracion;

    @SerializedName("P5000")
    private int P5000;

    @SerializedName("Porcentaje")
    private double Porcentaje;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("CantidadFotografias")
    private int CantidadFotografias;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesUri")
    private List<URI> ImagenesUri;

    @SerializedName("FechaAplicacion")
    private Date FechaAplicacion;

    @SerializedName("FechaRegistro")
    private Date FechaRegistro;

    @SerializedName("PorcentajeMedidor2")
    private double PorcentajeMedidor2;


    public CalibracionDTO(){
        this.Imagenes = new ArrayList<>();
        this.ImagenesUri = new ArrayList<>();
    }

    public int getIdCAlmacenGas() {
        return IdCAlmacenGas;
    }

    public void setIdCAlmacenGas(int idCAlmacenGas) {
        IdCAlmacenGas = idCAlmacenGas;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public double getPorcentajeCalibracion() {
        return PorcentajeCalibracion;
    }

    public void setPorcentajeCalibracion(double porcentajeCalibracion) {
        PorcentajeCalibracion = porcentajeCalibracion;
    }

    public int getIdDestinoCalibracion() {
        return IdDestinoCalibracion;
    }

    public void setIdDestinoCalibracion(int idDestinoCalibracion) {
        IdDestinoCalibracion = idDestinoCalibracion;
    }

    public int getP5000() {
        return P5000;
    }

    public void setP5000(int p5000) {
        P5000 = p5000;
    }

    public double getPorcentaje() {
        return Porcentaje;
    }

    public void setPorcentaje(double porcentaje) {
        Porcentaje = porcentaje;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
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

    public List<URI> getImagenesUri() {
        return ImagenesUri;
    }

    public void setImagenesUri(List<URI> imagenesUri) {
        ImagenesUri = imagenesUri;
    }

    public String getNombreCAlmacenGas() {
        return NombreCAlmacenGas;
    }

    public void setNombreCAlmacenGas(String nombreCAlmacenGas) {
        NombreCAlmacenGas = nombreCAlmacenGas;
    }

    public String getNombreMedidor() {
        return NombreMedidor;
    }

    public void setNombreMedidor(String nombreMedidor) {
        NombreMedidor = nombreMedidor;
    }

    public Date getFechaAplicacion() {
        return FechaAplicacion;
    }

    public void setFechaAplicacion(Date fechaAplicacion) {
        FechaAplicacion = fechaAplicacion;
    }

    public Date getFechaRegistro() {
        return FechaRegistro;
    }

    public void setFechaRegistro(Date fechaRegistro) {
        FechaRegistro = fechaRegistro;
    }

    public double getPorcentajeMedidor2() {
        return PorcentajeMedidor2;
    }

    public void setPorcentajeMedidor2(double porcentajeMedidor2) {
        PorcentajeMedidor2 = porcentajeMedidor2;
    }
}
