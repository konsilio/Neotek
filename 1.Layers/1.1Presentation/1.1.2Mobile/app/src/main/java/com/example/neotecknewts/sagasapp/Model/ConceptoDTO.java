package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

public class
ConceptoDTO {
    @SerializedName("IdTipoGas")
    private int IdTipoGas;

    @SerializedName("Cantidad")
    private int Cantidad;

    @SerializedName("Concepto")
    private String Concepto;

    @SerializedName("PUnitario")
    private double PUnitario;

    @SerializedName("Descuento")
    private double Descuento;

    @SerializedName("Subtotal")
    private double Subtotal;

    public int getIdTipoGas() {
        return IdTipoGas;
    }

    public void setIdTipoGas(int idTipoGas) {
        IdTipoGas = idTipoGas;
    }

    public int getCantidad() {
        return Cantidad;
    }

    public void setCantidad(int cantidad) {
        Cantidad = cantidad;
    }

    public String getConcepto() {
        return Concepto;
    }

    public void setConcepto(String concepto) {
        Concepto = concepto;
    }

    public double getPUnitario() {
        return PUnitario;
    }

    public void setPUnitario(double PUnitario) {
        this.PUnitario = PUnitario;
    }

    public double getDescuento() {
        return Descuento;
    }

    public void setDescuento(double descuento) {
        Descuento = descuento;
    }

    public double getSubtotal() {
        return Subtotal;
    }

    public void setSubtotal(double subtotal) {
        Subtotal = subtotal;
    }
}
