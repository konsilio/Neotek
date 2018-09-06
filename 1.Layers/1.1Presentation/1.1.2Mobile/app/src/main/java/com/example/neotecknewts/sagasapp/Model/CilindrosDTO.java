package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class CilindrosDTO implements Serializable {

    @SerializedName("IdCilindro")
    private int IdCilindro;

    @SerializedName("CilindroKg")
    private String CilindroKg;

    @SerializedName("Cantidad")
    private int Cantidad;

    public String getCilindroKg() {
        return CilindroKg;
    }

    public void setCilindroKg(String cilindroKg) {
        CilindroKg = cilindroKg;
    }

    public int getCantidad() {
        return Cantidad;
    }

    public void setCantidad(int cantidad) {
        Cantidad = cantidad;
    }

    public int getIdCilindro() {
        return IdCilindro;
    }

    public void setIdCilindro(int idCilindro) {
        IdCilindro = idCilindro;
    }
}
