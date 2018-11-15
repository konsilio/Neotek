package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class DatosEstacionesDTO implements Serializable {

    @SerializedName("IdAlmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("NombreAlmacen")
    private String NombreCAlmacen;

    @SerializedName("Icono")
    private int Icono;

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

    public int getIcono() {
        return Icono;
    }

    public void setIcono(int icono) {
        Icono = icono;
    }
}
