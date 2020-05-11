/**
 * Clase modelo DTO para la sección de autoconsumo
 * @author Jorge Omar Tovar Martínez
 * @commpany Neoteck
 * @date 20/09/2018
 * @updated 20/09/2018
 */
package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

public class AutoconsumoDTO implements Serializable{

    @SerializedName("IdCAlmacenGasSalida")
    private int IdCAlmacenGasSalida;

    @SerializedName("IdCAlmacenGasEntrada")
    private int IdCAlmacenGasEntrada;

    @SerializedName("P5000Salida")
    private int P5000Salida;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("Imagenes")
    public List<String> Imagenes;

    @SerializedName("ImagenesUri")
    private List<URI> ImagenesURI;

    @SerializedName("CantidadFotos")
    private int CantidadFotos;

    @SerializedName("NombreTipoMedidor")
    private String NombreTipoMedidor;

    @SerializedName("PorcentajeMedidor")
    private double PorcentajeMedidor;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("FechaAplicacion")
    private String FechaAplicacion;

    @SerializedName("FechaRegistro")
    private String FechaRegistro;

    @SerializedName("NombreEstacion")
    private String NombreEstacion;

    public AutoconsumoDTO (){
        this.ImagenesURI = new ArrayList<>();
        this.Imagenes = new ArrayList<>();
    }

    public int getIdCAlmacenGasSalida() {
        return IdCAlmacenGasSalida;
    }

    public void setIdCAlmacenGasSalida(int idCAlmacenGasSalida) {
        IdCAlmacenGasSalida = idCAlmacenGasSalida;
    }

    public int getIdCAlmacenGasEntrada() {
        return IdCAlmacenGasEntrada;
    }

    public void setIdCAlmacenGasEntrada(int idCAlmacenGasEntrada) {
        IdCAlmacenGasEntrada = idCAlmacenGasEntrada;
    }

    public int getP5000Salida() {
        return P5000Salida;
    }

    public void setP5000Salida(int p5000Salida) {
        P5000Salida = p5000Salida;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
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

    public int getCantidadFotos() {
        return CantidadFotos;
    }

    public void setCantidadFotos(int cantidadFotos) {
        CantidadFotos = cantidadFotos;
    }

    public String getNombreTipoMedidor() {
        return NombreTipoMedidor;
    }

    public void setNombreTipoMedidor(String nombreTipoMedidor) {
        NombreTipoMedidor = nombreTipoMedidor;
    }

    public double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public String getFechaAplicacion() {
        return FechaAplicacion;
    }

    public void setFechaAplicacion(String fechaAplicacion) {
        FechaAplicacion = fechaAplicacion;
    }

    public String getNombreEstacion() {
        return NombreEstacion;
    }

    public void setNombreEstacion(String nombreEstacion) {
        NombreEstacion = nombreEstacion;
    }

    public String getFechaRegistro() {
        return FechaRegistro;
    }

    public void setFechaRegistro(String fechaRegistro) {
        FechaRegistro = fechaRegistro;
    }

    @Override
    public String toString() {
        return "AutoconsumoDTO{" +
                "IdCAlmacenGasSalida=" + IdCAlmacenGasSalida +
                ", IdCAlmacenGasEntrada=" + IdCAlmacenGasEntrada +
                ", P5000Salida=" + P5000Salida +
                ", ClaveOperacion='" + ClaveOperacion + '\'' +
                // ", Imagenes=" + Imagenes +
                ", ImagenesURI=" + ImagenesURI +
                ", CantidadFotos=" + CantidadFotos +
                ", NombreTipoMedidor='" + NombreTipoMedidor + '\'' +
                ", PorcentajeMedidor=" + PorcentajeMedidor +
                ", IdTipoMedidor=" + IdTipoMedidor +
                ", FechaAplicacion='" + FechaAplicacion + '\'' +
                ", FechaRegistro='" + FechaRegistro + '\'' +
                ", NombreEstacion='" + NombreEstacion + '\'' +
                '}';
    }
}
