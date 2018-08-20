package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class MedidorDTO {
    @SerializedName("NombreTipoMedidor")
    private String NombreTipoMedidor;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("CantidadFotografias")
    private int CantidadFotografias;

    public String getNombreTipoMedidor() {
        return NombreTipoMedidor;
    }

    public void setNombreTipoMedidor(String nombreTipoMedidor) {
        NombreTipoMedidor = nombreTipoMedidor;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public int getCantidadFotografias() {
        return CantidadFotografias;
    }

    public void setCantidadFotografias(int cantidadFotografias) {
        CantidadFotografias = cantidadFotografias;
    }
}
