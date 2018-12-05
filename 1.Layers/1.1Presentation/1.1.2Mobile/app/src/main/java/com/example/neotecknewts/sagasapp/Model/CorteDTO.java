package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class CorteDTO extends RespuestaDTO implements Serializable {
    @SerializedName("IdCorte")
    private int IdCorte;

    @SerializedName("Tiket")
    private String Tiket;

    @SerializedName("Fecha")
    private String Fecha;

    @SerializedName("Monto")
    private double Monto;

    @SerializedName("IdCAlmacenGas")
    private int IdEstacion;

    @SerializedName("NombreEstacion")
    private String NombreEstacion;

    @SerializedName("Total")
    private double Total;

    @SerializedName("P5000Inicial")
    private int P5000Inicial;

    @SerializedName("P5000Final")
    private int P5000Final;

    @SerializedName("Anticipos")
    private double Anticipos;

    @SerializedName("MontoCorte")
    private double MontoCorte;

    @SerializedName("LitrosCorte")
    private double LitrosCorte;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("Hora")
    private String Hora;

    @SerializedName("FechaVenta")
    private String FechaVenta;

    @SerializedName("TotalAnticipos")
    private double TotalAnticipos;

    @SerializedName("Recibe")
    private String Recibe;

    @SerializedName("FechaCorte")
    private String FechaCorte;

    @SerializedName("Conceptos")
    private List<VentasCorteDTO> Conceptos;

    public CorteDTO(){
        this.Conceptos = new ArrayList<>();
    }

    public int getId() {
        return IdCorte;
    }

    public void setId(int id) {
        IdCorte = id;
    }

    public String getTiket() {
        return Tiket;
    }

    public void setTiket(String tiket) {
        Tiket = tiket;
    }

    public String getFecha() {
        return Fecha;
    }

    public void setFecha(String fecha) {
        Fecha = fecha;
    }

    public double getMonto() {
        return Monto;
    }

    public void setMonto(double monto) {
        Monto = monto;
    }

    public int getIdEstacion() {
        return IdEstacion;
    }

    public void setIdEstacion(int idEstacion) {
        IdEstacion = idEstacion;
    }

    public String getNombreEstacion() {
        return NombreEstacion;
    }

    public void setNombreEstacion(String nombreEstacion) {
        NombreEstacion = nombreEstacion;
    }

    public double getTotal() {
        return Total;
    }

    public void setTotal(double total) {
        Total = total;
    }

    public int getP5000Inicial() {
        return P5000Inicial;
    }

    public void setP5000Inicial(int p5000Inicial) {
        P5000Inicial = p5000Inicial;
    }

    public int getP5000Final() {
        return P5000Final;
    }

    public void setP5000Final(int p5000Final) {
        P5000Final = p5000Final;
    }

    public double getAnticipos() {
        return Anticipos;
    }

    public void setAnticipos(double anticipos) {
        Anticipos = anticipos;
    }

    public double getMontoCorte() {
        return MontoCorte;
    }

    public void setMontoCorte(double montoCorte) {
        MontoCorte = montoCorte;
    }

    public double getLitrosCorte() {
        return LitrosCorte;
    }

    public void setLitrosCorte(double litrosCorte) {
        LitrosCorte = litrosCorte;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public String getHora() {
        return Hora;
    }

    public void setHora(String hora) {
        Hora = hora;
    }

    public String getFechaVenta() {
        return FechaVenta;
    }

    public void setFechaVenta(String fechaVenta) {
        FechaVenta = fechaVenta;
    }

    public double getTotalAnticipos() {
        return TotalAnticipos;
    }

    public void setTotalAnticipos(double totalAnticipos) {
        TotalAnticipos = totalAnticipos;
    }

    public String getRecibe() {
        return Recibe;
    }

    public void setRecibe(String recibe) {
        Recibe = recibe;
    }

    public String getFechaCorte() {
        return FechaCorte;
    }

    public void setFechaCorte(String fechaCorte) {
        FechaCorte = fechaCorte;
    }

    public List<VentasCorteDTO> getConceptos() {
        return Conceptos;
    }

    public void setConceptos(List<VentasCorteDTO> conceptos) {
        Conceptos = conceptos;
    }
}
