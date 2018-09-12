package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class EstacionCarburacionDTO implements Serializable {
    @SerializedName("IdEstacionCarburacion")
    protected int IdEstacionCarburacion;

    @SerializedName("NombreEstacionCarburacion")
    protected String NombreEstacionCarburacion;

    public int getIdEstacionCarburacion() {
        return IdEstacionCarburacion;
    }

    public void setIdEstacionCarburacion(int idEstacionCarburacion) {
        IdEstacionCarburacion = idEstacionCarburacion;
    }

    public String getNombreEstacionCarburacion() {
        return NombreEstacionCarburacion;
    }

    public void setNombreEstacionCarburacion(String nombreEstacionCarburacion) {
        NombreEstacionCarburacion = nombreEstacionCarburacion;
    }
}
