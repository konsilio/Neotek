package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class VentasCorteDTO implements Serializable {
    @SerializedName("Corte")
    private String Corte;

    @SerializedName("TiketVenta")
    private String TiketVenta;

    @SerializedName("IdVenta")
    private int IdVenta;

    public String getCorte() {
        return Corte;
    }

    public void setCorte(String corte) {
        Corte = corte;
    }

    public String getTiketVenta() {
        return TiketVenta;
    }

    public void setTiketVenta(String tiketVenta) {
        TiketVenta = tiketVenta;
    }

    public int getIdVenta() {
        return IdVenta;
    }

    public void setIdVenta(int idVenta) {
        IdVenta = idVenta;
    }
}
