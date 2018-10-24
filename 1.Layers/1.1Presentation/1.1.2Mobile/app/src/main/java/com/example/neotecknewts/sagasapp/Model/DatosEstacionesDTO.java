package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class DatosEstacionesDTO implements Serializable {

    @SerializedName("IdAlmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("NombreCAlmacen")
    private String NombreCAlmacen;

    @SerializedName("Icono")
    private String Icono;

    public int getIdCAlmacenGas() {
        return IdCAlmacenGas;
    }

    public void setIdCAlmacenGas(int idCAlmacenGas) {
        IdCAlmacenGas = idCAlmacenGas;
    }

    public String getNombreCAlmacen() {
        return NombreCAlmacen;
    }

    public void setNombreCAlmacen(String nombreCAlmacen) {
        NombreCAlmacen = nombreCAlmacen;
    }

    public String getIcono() {
        return Icono;
    }

    public void setIcono(String icono) {
        Icono = icono;
    }
}
