package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ExistenciasDTO implements Serializable {
    @SerializedName("Id")
    private int Id;

    @SerializedName("Existencia")
    private double Existencias;

    @SerializedName("PrecioUnitario")
    private double PrecioUnitario;

    @Override
    public String toString() {
        return "ExistenciasDTO{" +
                "Id=" + Id +
                ", Existencias=" + Existencias +
                ", PrecioUnitario=" + PrecioUnitario +
                ", Descuento=" + Descuento +
                ", Nombre='" + Nombre + '\'' +
                ", cantidad='" + cantidad + '\'' +
                ", CapacidadLt=" + CapacidadLt +
                ", CapacidadKg=" + CapacidadKg +
                '}';
    }

    @SerializedName("Descuento")
    private double Descuento;

    @SerializedName("Nombre")
    private String Nombre;

    @SerializedName("Cantidad")
    private String cantidad;

    @SerializedName("CapacidadLt")
    private double CapacidadLt;

    @SerializedName("CapacidadKg")
    private double CapacidadKg;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public double getExistencias() {
        return Existencias;
    }

    public void setExistencias(double existencias) {
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

    public double getCapacidadLt() {
        return CapacidadLt;
    }

    public void setCapacidadLt(double capacidadLt) {
        CapacidadLt = capacidadLt;
    }

    public double getCapacidadKg() {
        return CapacidadKg;
    }

    public void setCapacidadKg(double capacidadKg) {
        CapacidadKg = capacidadKg;
    }
}
