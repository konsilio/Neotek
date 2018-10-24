package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.Date;

public class AnticiposDTO implements Serializable {
    @SerializedName("IdEstacion")
    private int IdEstacion;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("Anticipar")
    private double Anticipar;

    @SerializedName("Total")
    private double Total;

    @SerializedName("Fecha")
    private Date Fecha;

    @SerializedName("Hora")
    private String Hora;

    @SerializedName("NombreEstacion")
    private String NombreEstacion;

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

    public Date getFecha() {
        return Fecha;
    }

    public void setFecha(Date fecha) {
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
}
