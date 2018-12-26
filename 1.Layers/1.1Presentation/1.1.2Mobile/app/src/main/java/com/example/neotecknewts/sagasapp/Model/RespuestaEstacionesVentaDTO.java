package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class RespuestaEstacionesVentaDTO extends RespuestaDTO implements Serializable {

    @SerializedName("estaciones")
    @Expose
    private DatosEstacionesDTO estaciones;

    @SerializedName("pipa")
    private PipaDTO pipaDTO;

    @SerializedName("camioneta")
    private CamionetaDTO camionetaDTO;

    @SerializedName("anticipos")
    @Expose
    private List<AnticiposDTO>anticipos;

    @SerializedName("cortes")
    @Expose
    private List<CorteDTO> cortes;

    @SerializedName("fechasCorte")
    @Expose
    private List<String> fechasCorte;

    @SerializedName("TotalAnticiposCorte")
    private double TotalAnticiposCorte;

    @SerializedName("EsCamioneta")
    private boolean EsCamioneta;

    @SerializedName("EsPipa")
    private boolean EsPipa;

    @SerializedName("EsEstacion")
    private boolean EsEstacion;

    public  RespuestaEstacionesVentaDTO(){
        //this.estaciones = new ArrayList<>();
        this.anticipos = new ArrayList<>();
        this.cortes = new ArrayList<>();
    }

    public DatosEstacionesDTO getEstaciones() {
        return estaciones;
    }

    public void setEstaciones(DatosEstacionesDTO estaciones) {
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

    public double getTotalAnticiposCorte() {
        return TotalAnticiposCorte;
    }

    public void setTotalAnticiposCorte(double totalAnticiposCorte) {
        TotalAnticiposCorte = totalAnticiposCorte;
    }

    public PipaDTO getPipaDTO() {
        return pipaDTO;
    }

    public void setPipaDTO(PipaDTO pipaDTO) {
        this.pipaDTO = pipaDTO;
    }

    public CamionetaDTO getCamionetaDTO() {
        return camionetaDTO;
    }

    public void setCamionetaDTO(CamionetaDTO camionetaDTO) {
        this.camionetaDTO = camionetaDTO;
    }

    public boolean isEsCamioneta() {
        return EsCamioneta;
    }

    public void setEsCamioneta(boolean esCamioneta) {
        EsCamioneta = esCamioneta;
    }

    public boolean isEsPipa() {
        return EsPipa;
    }

    public void setEsPipa(boolean esPipa) {
        EsPipa = esPipa;
    }

    public boolean isEsEstacion() {
        return EsEstacion;
    }

    public void setEsEstacion(boolean esEstacion) {
        EsEstacion = esEstacion;
    }
}
