package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 *<h3>LecturaCamionetaDTO</h3>
 * Modelo para el almacenamiento de datos de la lectura de camioneta
 * @author Jorge Omar Tovar Martínez
 */
public class LecturaCamionetaDTO implements Serializable {
    @SerializedName("IdCamioneta")
    private int IdCamioneta;

    @SerializedName("NombreCamioneta")
    private String NombreCamioneta;

    @SerializedName("EsEncargadoPuerta")
    private boolean EsEncargadoPuerta;

    @SerializedName("Cilindro")
    private List<String> Cilindro;

    @SerializedName("CilindroCantidad")
    private List<Integer> CilindroCantidad;

    @SerializedName("IdCilindro")
    private List<Integer> IdCilindro;

    @SerializedName("Cilindros")
    private List<CilindrosDTO> Cilindros;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    public LecturaCamionetaDTO(){
        this.Cilindro = new ArrayList<>();
        this.CilindroCantidad = new ArrayList<>();
        this.Cilindros = new ArrayList<>();
    }

    public int getIdCamioneta() {
        return IdCamioneta;
    }

    public void setIdCamioneta(int idCamioneta) {
        IdCamioneta = idCamioneta;
    }

    public String getNombreCamioneta() {
        return NombreCamioneta;
    }

    public void setNombreCamioneta(String nombreCamioneta) {
        NombreCamioneta = nombreCamioneta;
    }

    public boolean isEsEncargadoPuerta() {
        return EsEncargadoPuerta;
    }

    public void setEsEncargadoPuerta(boolean esEncargadoPuerta) {
        EsEncargadoPuerta = esEncargadoPuerta;
    }

    public List<String> getCilindro() {
        return Cilindro;
    }

    public void setCilindro(List<String> cilindro) {
        Cilindro = cilindro;
    }

    public List<Integer> getCilindroCantidad() {
        return CilindroCantidad;
    }

    public void setCilindroCantidad(List<Integer> cilindroCantidad) {
        CilindroCantidad = cilindroCantidad;
    }

    public List<Integer> getIdCilindro() {
        return IdCilindro;
    }

    public void setIdCilindro(List<Integer> idCilindro) {
        IdCilindro = idCilindro;
    }

    public List<CilindrosDTO> getCilindros() {
        return Cilindros;
    }

    public void setCilindros(List<CilindrosDTO> cilindros) {
        Cilindros = cilindros;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }
}
