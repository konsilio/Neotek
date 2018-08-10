package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 10/08/18.
 */

abstract class RespuestaDTO {

    @SerializedName("Exito")
    private boolean Exito;

    @SerializedName("Mensaje")
    private String Mensaje;

    public boolean isExito() {
        return Exito;
    }

    public void setExito(boolean exito) {
        Exito = exito;
    }

    public String getMensaje() {
        return Mensaje;
    }

    public void setMensaje(String mensaje) {
        Mensaje = mensaje;
    }
}
