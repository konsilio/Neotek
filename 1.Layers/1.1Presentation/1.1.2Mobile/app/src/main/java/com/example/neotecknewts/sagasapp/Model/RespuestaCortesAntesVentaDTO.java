package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class RespuestaCortesAntesVentaDTO extends RespuestaDTO implements Serializable {
    @SerializedName("HayCorte")
    private boolean HayCorte;

    @SerializedName("Corte")
    private CorteDTO corte;

    public boolean isHayCorte() {
        return HayCorte;
    }

    public void setHayCorte(boolean hayCorte) {
        HayCorte = hayCorte;
    }

    public CorteDTO getCorte() {
        return corte;
    }

    public void setCorte(CorteDTO corte) {
        this.corte = corte;
    }
}
