package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.Date;

public class CorteDTO extends RespuestaDTO implements Serializable {
    @SerializedName("Id")
    private int Id;

    @SerializedName("Tiket")
    private String Tiket;

    @SerializedName("Fecha")
    private Date Fecha;

    @SerializedName("Monto")
    private double Monto;

    @SerializedName("IdEstacion")
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

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getTiket() {
        return Tiket;
    }

    public void setTiket(String tiket) {
        Tiket = tiket;
    }

    public Date getFecha() {
        return Fecha;
    }

    public void setFecha(Date fecha) {
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
}
