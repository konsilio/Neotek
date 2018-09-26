package com.example.neotecknewts.sagasapp.Model;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

public class CalibracionDTO implements Serializable {
    private int IdCAlmacenGas;
    private String NombreCAlmacenGas;
    private int IdTipoMedidor;
    private String NombreMedidor;
    private double PorcentajeCalibracion;
    private int IdDestinoCalibracion;
    private int P5000;
    private double Porcentaje;
    private String ClaveOperacion;
    private int CantidadFotografias;
    private List<String> Imagenes;
    private List<URI> ImagenesUri;

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
}
