package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class AlmacenDTO extends RespuestaDTO implements Serializable {
    @SerializedName("NombreAlmacen")
    private String NombreAlmacen;

    @SerializedName("IdAlmacenGas")
    private int IdAlmacenGas;

    @SerializedName("PorcentajeMedidor")
    private double PorcentajeMedidor;

    @SerializedName("CantidadP5000")
    private int CantidadP5000;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("Cilindros")
    private List<CilindrosDTO> Cilindros;

    @SerializedName("Capacidad")
    private double Capacidad;

    public AlmacenDTO (){
        Cilindros = new ArrayList<>();
}

    public String getNombreAlmacen() {
        return NombreAlmacen;
    }

    public void setNombreAlmacen(String nombreAlmacen) {
        NombreAlmacen = nombreAlmacen;
    }

    public int getIdAlmacenGas() {
        return IdAlmacenGas;
    }

    public void setIdAlmacenGas(int idAlmacenGas) {
        IdAlmacenGas = idAlmacenGas;
    }

    public double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public int getCantidadP5000() {
        return CantidadP5000;
    }

    public void setCantidadP5000(int cantidadP5000) {
        CantidadP5000 = cantidadP5000;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public List<CilindrosDTO> getCilindros() {
        return Cilindros;
    }

    public void setCilindros(List<CilindrosDTO> cilindros) {
        Cilindros = new ArrayList<>();
    }

    public double getCapacidad() {
        return Capacidad;
    }

    public void setCapacidad(double capacidad) {
        Capacidad = capacidad;
    }
}

