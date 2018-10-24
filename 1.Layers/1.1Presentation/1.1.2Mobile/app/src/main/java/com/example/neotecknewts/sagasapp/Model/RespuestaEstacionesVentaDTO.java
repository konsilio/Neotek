package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class RespuestaEstacionesVentaDTO extends RespuestaDTO implements Serializable {

    @SerializedName("estaciones")
    private List<DatosEstacionesDTO> estaciones;

    @SerializedName("anticipos")
    private List<AnticiposDTO>anticipos;

    @SerializedName("cortes")
    private List<CorteDTO> cortes;

    public  RespuestaEstacionesVentaDTO(){
        this.estaciones = new ArrayList<>();
        this.anticipos = new ArrayList<>();
        this.cortes = new ArrayList<>();
    }

    public List<DatosEstacionesDTO> getEstaciones() {
        return estaciones;
    }

    public void setEstaciones(List<DatosEstacionesDTO> estaciones) {
        this.estaciones = estaciones;
    }

    public List<AnticiposDTO> getAnticipos() {
        return anticipos;
    }

    public void setAnticipos(List<AnticiposDTO> anticipos) {
        this.anticipos = anticipos;
    }

    public List<CorteDTO> getCortes() {
        return cortes;
    }

    public void setCortes(List<CorteDTO> cortes) {
        this.cortes = cortes;
    }
}
