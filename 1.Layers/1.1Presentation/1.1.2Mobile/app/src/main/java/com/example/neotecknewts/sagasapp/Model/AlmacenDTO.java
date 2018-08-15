package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class AlmacenDTO {
    @SerializedName("NombreAlamcen")
    private String NombreAlmacen;

    @SerializedName("IdAlmacenGas")
    private int IdAlmacenGas;

    public String getNombreAlmacen() {
        return NombreAlmacen;
    }

    public void setNombreAlmacen(String nombreAlmacen) {
        NombreAlmacen = nombreAlmacen;
    }

    public int getIdAlmacenGas() {
        return IdAlmacenGas;
    }

    public void setIdAlmacenGas(int idAlmacenGas) {
        IdAlmacenGas = idAlmacenGas;
    }
}
