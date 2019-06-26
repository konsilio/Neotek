/*
 * RecargaDTO
 * Clase modelo DTO que permite almacenar los datos de la recarga
 * @author Jorge Omar Tovar Martínez jorge.tovar@noeteck.com.mx
 * @company Neoteck
 * @date 06/12/2018
 * @update 06/12/2018
 */
package com.neotecknewts.sagasapp.Model;

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
    private int P5000Salida;

    @SerializedName("P5000Entrada")
    private int P5000Entrada;

    @SerializedName("ClaveOperacion")
    private String  ClaveOperacion;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesUri")
    private List<URI> ImagenesUri;

    @SerializedName("Cilindros")
    private List<CilindrosDTO> Cilindros;//Cambiar por List<CilindrosDTO>

    @SerializedName("NombreMedidorEntrada")
    private String NombreMedidorEntrada;

    @SerializedName("NombreMedidorSalida")
    private String NombreMedidorSalida;

    @SerializedName("ProcentajeEntrada")
    private double ProcentajeEntrada;

    @SerializedName("ProcentajeSalida")
    private double ProcentajeSalida;

    @SerializedName("CantidadFotosEntrada")
    private int CantidadFotosEntrada;

    @SerializedName("CantidadFotosSalida")
    private int CantidadFotosSalida;

    @SerializedName("FechaAplicacion")
    private String FechaApliacacion;

    @SerializedName("NombreEstacionSalida")
    private String NombreEstacionSalida;

    @SerializedName("NombreEstacionEntrada")
    private String NombreEstacionEntrada;

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

    public List<CilindrosDTO> getCilindros() {
        return Cilindros;
    }

    public void setCilindros(List<CilindrosDTO> cilindros) {
        Cilindros = cilindros;
    }

    public String getNombreMedidorEntrada() {
        return NombreMedidorEntrada;
    }

    public void setNombreMedidorEntrada(String nombreMedidorEntrada) {
        NombreMedidorEntrada = nombreMedidorEntrada;
    }

    public String getNombreMedidorSalida() {
        return NombreMedidorSalida;
    }

    public void setNombreMedidorSalida(String nombreMedidorSalida) {
        NombreMedidorSalida = nombreMedidorSalida;
    }

    public double getProcentajeEntrada() {
        return ProcentajeEntrada;
    }

    public void setProcentajeEntrada(double procentajeEntrada) {
        ProcentajeEntrada = procentajeEntrada;
    }

    public double getProcentajeSalida() {
        return ProcentajeSalida;
    }

    public void setProcentajeSalida(double procentajeSalida) {
        ProcentajeSalida = procentajeSalida;
    }

    public int getCantidadFotosEntrada() {
        return CantidadFotosEntrada;
    }

    public void setCantidadFotosEntrada(int cantidadFotosEntrada) {
        CantidadFotosEntrada = cantidadFotosEntrada;
    }

    public int getCantidadFotosSalida() {
        return CantidadFotosSalida;
    }

    public void setCantidadFotosSalida(int cantidadFotosSalida) {
        CantidadFotosSalida = cantidadFotosSalida;
    }

    public String getFechaApliacacion() {
        return FechaApliacacion;
    }

    public void setFechaApliacacion(String fechaApliacacion) {
        FechaApliacacion = fechaApliacacion;
    }

    public String getNombreEstacionSalida() {
        return NombreEstacionSalida;
    }

    public void setNombreEstacionSalida(String nombreEstacionSalida) {
        NombreEstacionSalida = nombreEstacionSalida;
    }

    public String getNombreEstacionEntrada() {
        return NombreEstacionEntrada;
    }

    public void setNombreEstacionEntrada(String nombreEstacionEntrada) {
        NombreEstacionEntrada = nombreEstacionEntrada;
    }
}
