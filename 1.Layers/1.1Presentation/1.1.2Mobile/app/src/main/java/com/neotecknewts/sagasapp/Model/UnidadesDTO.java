package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class UnidadesDTO implements Serializable {

    @SerializedName("IdAlmacenGas")
    private  int IdAlmacenGas;

    @SerializedName("NombreAlmacen")
    private String NombreAlmacen;

    @SerializedName("PorcentajeMedidor")
    private double PorcentajeMedidor;

    @SerializedName("CantidadP5000")
    private double CantidadP5000;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    public int getIdAlmacenGas() {
        return IdAlmacenGas;
    }

    public void setIdAlmacenGas(int IdAlmacenGas) {
        IdAlmacenGas = IdAlmacenGas;
    }

    public String getNombre() {
        return NombreAlmacen;
    }


    public String getNombreAlmacen() {
        return NombreAlmacen;
    }

    public void setNombreAlmacen(String nombreAlmacen) {
        NombreAlmacen = nombreAlmacen;
    }

    public double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public double getCantidadP5000() {
        return CantidadP5000;
    }

    public void setCantidadP5000(double cantidadP5000) {
        CantidadP5000 = cantidadP5000;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }
}
