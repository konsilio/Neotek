/*
 *  AnticiposEstacionDTO
 *  Permite que se almacene desde el serivicio todos los anticipos
 *  correspondientes a la venta para realizar el corte de caja
 *  @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 *  @company Neoteck
 *  @date 22/11/2018 5:21 p.m.
 *  @update 22/11/2018 5:21 p.m.
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class AnticiposEstacionDTO extends RespuestaDTO implements Serializable {
    @SerializedName("IdAnticipo")
    private int IdAntiAnticipo;

    @SerializedName("Tiket")
    private String Tiket;

    @SerializedName("Fecha")
    private String Fecha;

    @SerializedName("Monto")
    private double Monto;

    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("IdCAlmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("Total")
    private double Total;

    @SerializedName("Anticipos")
    private List<AnticiposDTO> Anticipos;

    public AnticiposEstacionDTO(){
        this.Anticipos = new ArrayList<>();
    }

    public int getIdAntiAnticipo() {
        return IdAntiAnticipo;
    }

    public void setIdAntiAnticipo(int idAntiAnticipo) {
        IdAntiAnticipo = idAntiAnticipo;
    }

    public String getTiket() {
        return Tiket;
    }

    public void setTiket(String tiket) {
        Tiket = tiket;
    }

    public String getFecha() {
        return Fecha;
    }

    public void setFecha(String fecha) {
        Fecha = fecha;
    }

    public double getMonto() {
        return Monto;
    }

    public void setMonto(double monto) {
        Monto = monto;
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public int getIdCAlmacenGas() {
        return IdCAlmacenGas;
    }

    public void setIdCAlmacenGas(int idCAlmacenGas) {
        IdCAlmacenGas = idCAlmacenGas;
    }

    public double getTotal() {
        return Total;
    }

    public void setTotal(double total) {
        Total = total;
    }

    public List<AnticiposDTO> getAnticipos() {
        return Anticipos;
    }

    public void setAnticipos(List<AnticiposDTO> anticipos) {
        Anticipos = anticipos;
    }
}

