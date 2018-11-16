package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ExistenciasDTO implements Serializable {
    @SerializedName("Id")
    private int Id;

    @SerializedName("Existencias")
    private int Existencias;

    @SerializedName("PrecioUnitario")
    private double PrecioUnitario;

    @SerializedName("Descuento")
    private double Descuento;

    @SerializedName("Nombre")
    private String Nombre;

    @SerializedName("Cantidad")
    private String cantidad;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int getExistencias() {
        return Existencias;
    }

    public void setExistencias(int existencias) {
        Existencias = existencias;
    }

    public double getPrecioUnitario() {
        return PrecioUnitario;
    }

    public void setPrecioUnitario(double precioUnitario) {
        PrecioUnitario = precioUnitario;
    }

    public double getDescuento() {
        return Descuento;
    }

    public void setDescuento(double descuento) {
        Descuento = descuento;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public void setCantidad(String cantidad) {
        this.cantidad= cantidad;
    }
    public String getCantidad(){
        return  this.cantidad;
    }
}
