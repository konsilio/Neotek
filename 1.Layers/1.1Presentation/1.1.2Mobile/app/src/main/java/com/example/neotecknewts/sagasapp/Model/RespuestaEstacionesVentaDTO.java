package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class RespuestaEstacionesVentaDTO extends RespuestaDTO implements Serializable {

    @SerializedName("estaciones")
    @Expose
    private List<DatosEstacionesDTO> estaciones;

    @SerializedName("anticipos")
    @Expose
    private List<AnticiposDTO>anticipos;

    @SerializedName("cortes")
    @Expose
    private List<CorteDTO> cortes;

    @SerializedName("fechasCorte")
    @Expose
    private List<String> fechasCorte;

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

    public List<String> getFechasCorte() {
        return fechasCorte;
    }

    public void setFechasCorte(List<String> fechasCorte) {
        this.fechasCorte = fechasCorte;
    }
}
