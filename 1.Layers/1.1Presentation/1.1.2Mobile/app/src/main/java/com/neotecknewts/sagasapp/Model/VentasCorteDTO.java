/*
 * VentasCorteDTO
 * Permite almacenar todos las ventas que se les este realizando el corte de
 * caja para su actualización en el servicio y asignar el código de corte a la venta
 * @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @company Neoteck
 * @Date 05/12/2018
 * @Update 05/12/2018
 */
package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class VentasCorteDTO implements Serializable {
    @SerializedName("Corte")
    private String Corte;

    @SerializedName("TiketVenta")
    private String TiketVenta;

    @SerializedName("IdVenta")
    private int IdVenta;

    public String getCorte() {
        return Corte;
    }

    public void setCorte(String corte) {
        Corte = corte;
    }

    public String getTiketVenta() {
        return TiketVenta;
    }

    public void setTiketVenta(String tiketVenta) {
        TiketVenta = tiketVenta;
    }

    public int getIdVenta() {
        return IdVenta;
    }

    public void setIdVenta(int idVenta) {
        IdVenta = idVenta;
    }
}
