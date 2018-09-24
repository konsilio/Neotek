/*
 * Clase modelo DTO para el registro de traspasos
 * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
 * @commpany Neoteck
 * @Date 24/09/2018
 * @Update 24/09/2018
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

public class TraspasoDTO implements Serializable{

    @SerializedName("IdCAlmacenGasSalida")
    private int IdCAlmacenGasSalida;

    @SerializedName("IdCAlmacenGasEntrada")
    private int IdCAlmacenGasEntrada;

    @SerializedName("IdTipoMedidorSalida")
    private int IdTipoMedidorSalida;

    @SerializedName("P5000Salida")
    private int P5000Salida;

    @SerializedName("P5000Entrada")
    private int P5000Entrada;

    @SerializedName("PorcentajeSalida")
    private double PorcentajeSalida;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesUri")
    private List<URI> ImagenesUri;

    @SerializedName("NombreMedidor")
    private String NombreMedidor;

    @SerializedName("CantidadDeFotos")
    private int CantidadDeFotos;

    public TraspasoDTO(){
        this.Imagenes = new ArrayList<>();
        this.ImagenesUri = new ArrayList<>();
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

    public int getIdTipoMedidorSalida() {
        return IdTipoMedidorSalida;
    }

    public void setIdTipoMedidorSalida(int idTipoMedidorSalida) {
        IdTipoMedidorSalida = idTipoMedidorSalida;
    }

    public int getP5000Salida() {
        return P5000Salida;
    }

    public void setP5000Salida(int p5000Salida) {
        P5000Salida = p5000Salida;
    }

    public int getP5000Entrada() {
        return P5000Entrada;
    }

    public void setP5000Entrada(int p5000Entrada) {
        P5000Entrada = p5000Entrada;
    }

    public double getPorcentajeSalida() {
        return PorcentajeSalida;
    }

    public void setPorcentajeSalida(double porcentajeSalida) {
        PorcentajeSalida = porcentajeSalida;
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

    public List<URI> getImagenesUri() {
        return ImagenesUri;
    }

    public void setImagenesUri(List<URI> imagenesUri) {
        ImagenesUri = imagenesUri;
    }

    public String getNombreMedidor() {
        return NombreMedidor;
    }

    public void setNombreMedidor(String nombreMedidor) {
        NombreMedidor = nombreMedidor;
    }

    public int getCantidadDeFotos() {
        return CantidadDeFotos;
    }

    public void setCantidadDeFotos(int cantidadDeFotos) {
        CantidadDeFotos = cantidadDeFotos;
    }
}
