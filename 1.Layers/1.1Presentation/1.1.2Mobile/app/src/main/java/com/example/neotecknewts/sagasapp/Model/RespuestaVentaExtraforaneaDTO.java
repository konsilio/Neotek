package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class RespuestaVentaExtraforaneaDTO extends RespuestaDTO implements Serializable {
    @SerializedName("VentaExtraforanea")
    private boolean VentaExtraforanea;

    public boolean isVentaExtraforanea() {
        return VentaExtraforanea;
    }

    public void setVentaExtraforanea(boolean ventaExtraforanea) {
        VentaExtraforanea = ventaExtraforanea;
    }
}
