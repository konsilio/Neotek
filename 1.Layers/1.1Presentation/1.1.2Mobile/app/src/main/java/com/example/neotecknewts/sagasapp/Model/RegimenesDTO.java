package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class RegimenesDTO extends RespuestaDTO implements Serializable {
    @SerializedName("IdRegimenFiscal")
    private int IdRegimenFiscal;

    @SerializedName("IdTipoPersona")
    private int IdTipoPersona;

    @SerializedName("c_RegimenFiscal")
    private String c_RegimenFiscal;

    @SerializedName("Descripcion")
    private String Descripcion;

    @SerializedName("AplicaPersonaFisica")
    private boolean AplicaPersonaFisica;

    @SerializedName("AplicaPersonaMoral")
    private boolean AplicaPersonaMoral;

    public int getIdRegimenFiscal() {
        return IdRegimenFiscal;
    }

    public void setIdRegimenFiscal(int idRegimenFiscal) {
        IdRegimenFiscal = idRegimenFiscal;
    }

    public int getIdTipoPersona() {
        return IdTipoPersona;
    }

    public void setIdTipoPersona(int idTipoPersona) {
        IdTipoPersona = idTipoPersona;
    }

    public String getC_RegimenFiscal() {
        return c_RegimenFiscal;
    }

    public void setC_RegimenFiscal(String c_RegimenFiscal) {
        this.c_RegimenFiscal = c_RegimenFiscal;
    }

    public String getDescripcion() {
        return Descripcion;
    }

    public void setDescripcion(String descripcion) {
        Descripcion = descripcion;
    }

    public boolean isAplicaPersonaFisica() {
        return AplicaPersonaFisica;
    }

    public void setAplicaPersonaFisica(boolean aplicaPersonaFisica) {
        AplicaPersonaFisica = aplicaPersonaFisica;
    }

    public boolean isAplicaPersonaMoral() {
        return AplicaPersonaMoral;
    }

    public void setAplicaPersonaMoral(boolean aplicaPersonaMoral) {
        AplicaPersonaMoral = aplicaPersonaMoral;
    }
}
