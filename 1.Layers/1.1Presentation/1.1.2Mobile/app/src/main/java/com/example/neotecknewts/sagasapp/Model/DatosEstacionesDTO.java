/*
 * DatosEstacionesDTO
 * Clase modelo de tipo DTO en el cual se obtienen las estaciónes de carburación
 * para el corte de cajas y anticipos
 * @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @company Neoteck
 * @date   27/09/2018 18:40 am
 * @update 21/11/2018 6:22 pm
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class DatosEstacionesDTO implements Serializable {

    @SerializedName("IdAlmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("NombreAlmacen")
    private String NombreCAlmacen;

    @SerializedName("Icono")
    private int Icono;

    @SerializedName("P5000Inicial")
    private int P5000Inicial;

    @SerializedName("P5000Final")
    private int P5000Final;

    @SerializedName("TotalAnticipos")
    private double TotalAnticipos;

    @SerializedName("AnticiposEstacion")
    private AnticiposEstacionDTO anticiposEstacion;

    public int getIdCAlmacenGas() {
        return IdCAlmacenGas;
    }

    public void setIdCAlmacenGas(int idCAlmacenGas) {
        IdCAlmacenGas = idCAlmacenGas;
    }

    public String getNombreCAlmacen() {
        return NombreCAlmacen;
    }

    public void setNombreCAlmacen(String nombreCAlmacen) {
        NombreCAlmacen = nombreCAlmacen;
    }

    public int getIcono() {
        return Icono;
    }

    public void setIcono(int icono) {
        Icono = icono;
    }

    public int getP5000Inicial() {
        return P5000Inicial;
    }

    public void setP5000Inicial(int p5000Inicial) {
        P5000Inicial = p5000Inicial;
    }

    public int getP5000Final() {
        return P5000Final;
    }

    public void setP5000Final(int p5000Final) {
        P5000Final = p5000Final;
    }

    public double getTotalAnticipos() {
        return TotalAnticipos;
    }

    public void setTotalAnticipos(double totalAnticipos) {
        TotalAnticipos = totalAnticipos;
    }

    public AnticiposEstacionDTO getAnticiposEstacion() {
        return anticiposEstacion;
    }

    public void setAnticiposEstacion(AnticiposEstacionDTO anticiposEstacion) {
        this.anticiposEstacion = anticiposEstacion;
    }
}
