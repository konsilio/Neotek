package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.Date;

public class AnticiposDTO extends RespuestaDTO implements Serializable {
    @SerializedName("IdAnticipo")
    @Expose
    private int IdAnticipo;

    @SerializedName("IdEstacion")
    private int IdEstacion;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("Monto")
    private double Anticipar;

    @SerializedName("Total")
    private double Total;

    @SerializedName("Fecha")
    private String Fecha;

    @SerializedName("Hora")
    private String Hora;

    @SerializedName("NombreEstacion")
    private String NombreEstacion;

    @SerializedName("IdCAlmacenGas")
    private int IdCAlmacen;

    @SerializedName("Tiket")
    private String Tiket;

    @SerializedName("Recibe")
    private String Recibe;

    @SerializedName("FechaAnticipo")
    private String FechaAnticipo;

    public int getIdEstacion() {
        return IdEstacion;
    }

    public void setIdEstacion(int idEstacion) {
        IdEstacion = idEstacion;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public double getAnticipar() {
        return Anticipar;
    }

    public void setAnticipar(double anticipar) {
        this.Anticipar = anticipar;
    }

    public double getTotal() {
        return Total;
    }

    public void setTotal(double total) {
        this.Total = total;
    }

    public String getFecha() {
        return Fecha;
    }

    public void setFecha(String fecha) {
        this.Fecha = fecha;
    }

    public String getHora() {
        return Hora;
    }

    public void setHora(String hora) {
        Hora = hora;
    }

    public String getNombreEstacion() {
        return NombreEstacion;
    }

    public void setNombreEstacion(String nombreEstacion) {
        NombreEstacion = nombreEstacion;
    }

    public int getIdCAlmacen() {
        return IdCAlmacen;
    }

    public void setIdCAlmacen(int idCAlmacen) {
        IdCAlmacen = idCAlmacen;
    }

    public String getTiket() {
        return Tiket;
    }

    public void setTiket(String tiket) {
        Tiket = tiket;
    }

    public int getIdAnticipo() {
        return IdAnticipo;
    }

    public void setIdAnticipo(int idAnticipo) {
        IdAnticipo = idAnticipo;
    }

    public String getRecibe() {
        return Recibe;
    }

    public void setRecibe(String recibe) {
        Recibe = recibe;
    }

    public String getFechaAnticipo() {
        return FechaAnticipo;
    }

    public void setFechaAnticipo(String fechaAnticipo) {
        FechaAnticipo = fechaAnticipo;
    }
}
