package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class TipoPersonaDTO implements Serializable {
    @SerializedName("IdTipoPersona")
    private int IdTipoPersona;

    @SerializedName("Descripcion")
    private String Descripcion;

    @SerializedName("Regimenes")
    private List<RegimenesDTO> Regimenes;

    public TipoPersonaDTO(){
        this.Regimenes = new ArrayList<>();
    }

    public int getIdTipoPersona() {
        return IdTipoPersona;
    }

    public void setIdTipoPersona(int idTipoPersona) {
        IdTipoPersona = idTipoPersona;
    }

    public String getDescripcion() {
        return Descripcion;
    }

    public void setDescripcion(String descripcion) {
        Descripcion = descripcion;
    }

    public List<RegimenesDTO> getRegimenes() {
        return Regimenes;
    }

    public void setRegimenes(List<RegimenesDTO> regimenes) {
        Regimenes = regimenes;
    }
}
