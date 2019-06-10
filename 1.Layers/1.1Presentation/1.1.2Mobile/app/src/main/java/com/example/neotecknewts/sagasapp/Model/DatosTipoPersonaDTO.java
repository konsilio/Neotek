package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.List;

public class DatosTipoPersonaDTO extends RespuestaDTO {
    @SerializedName("tipoPersona")
    private List<TipoPersonaDTO> tipoPersona;

    public DatosTipoPersonaDTO(){
        this.tipoPersona = new ArrayList<>();
    }

    public List<TipoPersonaDTO> getTipoPersona() {
        return tipoPersona;
    }

    public void setTipoPersona(List<TipoPersonaDTO> tipoPersona) {
        this.tipoPersona = tipoPersona;
    }
}
