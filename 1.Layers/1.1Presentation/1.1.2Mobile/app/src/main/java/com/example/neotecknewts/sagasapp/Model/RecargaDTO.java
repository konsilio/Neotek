package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

public class RecargaDTO implements Serializable {

    @SerializedName("IdCAlmacenGasSalida")
    private int IdCAlmacenGasSalida;

    @SerializedName("IdCAlmacenGasEntrada")
    private int IdCAlmacenGasEntrada;

    @SerializedName("IdTipoMedidorSalida")
    private int IdTipoMedidorSalida;

    @SerializedName("IdTipoMedidorEntrada")
    private int IdTipoMedidorEntrada;

    @SerializedName("IdTipoEvento")
    private int IdTipoEvento;

    @SerializedName("P5000Salida")
    private double P5000Salida;

    @SerializedName("P5000Entrada")
    private double P5000Entrada;

    @SerializedName("ClaveOperacion")
    private String  ClaveOperacion;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesUri")
    private List<URI> ImagenesUri;

    @SerializedName("Cilindros")
    private List<int[]> Cilindros;

    public RecargaDTO(){
        this.Imagenes = new ArrayList<>();
        this.ImagenesUri = new ArrayList<>();
        this.Cilindros = new ArrayList<>();
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

    public int getIdTipoMedidorEntrada() {
        return IdTipoMedidorEntrada;
    }

    public void setIdTipoMedidorEntrada(int idTipoMedidorEntrada) {
        IdTipoMedidorEntrada = idTipoMedidorEntrada;
    }

    public int getIdTipoEvento() {
        return IdTipoEvento;
    }

    public void setIdTipoEvento(int idTipoEvento) {
        IdTipoEvento = idTipoEvento;
    }

    public double getP5000Salida() {
        return P5000Salida;
    }

    public void setP5000Salida(double p5000Salida) {
        P5000Salida = p5000Salida;
    }

    public double getP5000Entrada() {
        return P5000Entrada;
    }

    public void setP5000Entrada(double p5000Entrada) {
        P5000Entrada = p5000Entrada;
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

    public List<int[]> getCilindros() {
        return Cilindros;
    }

    public void setCilindros(List<int[]> cilindros) {
        Cilindros = cilindros;
    }
}
