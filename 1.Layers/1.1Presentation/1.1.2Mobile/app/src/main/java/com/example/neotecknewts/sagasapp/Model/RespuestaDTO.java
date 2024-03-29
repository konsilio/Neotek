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

    @SerializedName("MensajesError")
    private String MensajesError;

    @SerializedName("Id")
    private int Id;

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

    public String getMensajesError() {
        return MensajesError;
    }

    public void setMensajesError(String mensajesError) {
        MensajesError = mensajesError;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }
}
