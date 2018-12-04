package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class VentasCorteDTO implements Serializable {
    @SerializedName("ClaveVenta")
    private String ClaveVenta;

    @SerializedName("ClaveCorte")
    private String ClaveCorte;

    public String getClaveVenta() {
        return ClaveVenta;
    }

    public void setClaveVenta(String claveVenta) {
        ClaveVenta = claveVenta;
    }

    public String getClaveCorte() {
        return ClaveCorte;
    }

    public void setClaveCorte(String claveCorte) {
        ClaveCorte = claveCorte;
    }
}
