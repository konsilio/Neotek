package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class LineaDTO implements Serializable {

    @SerializedName("IdLinea")
    private int IdLinea;

    @SerializedName("Nombre")
    private String Linea;

    public int getIdLinea() {
        return IdLinea;
    }

    public void setIdLinea(int idLinea) {
        IdLinea = idLinea;
    }

    public String getLinea() {
        return Linea;
    }

    public void setLinea(String linea) {
        Linea = linea;
    }
}
