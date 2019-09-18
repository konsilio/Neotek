/*
 *  PuntoVentaAsignadoDTO
 *  Modelo DTO que permite extraer desde el api
 *  los datos del punto de venta para la generación del reporte
 *  @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 *  @date 10/12/2018
 *  @update 10/12/2018
 *  @company Neoteck
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class PuntoVentaAsignadoDTO extends RespuestaDTO implements Serializable {

    @SerializedName("IdEstacion")
    private int IdEstacion;

    @SerializedName("IdCAlmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("IdPuntoVenta")
    private int IdPuntoVenta;

    @SerializedName("NombrePuntoVenta")
    private String NombrePuntoVenta;

    @SerializedName("NombreOperador")
    private String NombreOperador;

    @SerializedName("IdOperadorChofer")
    private int IdOperadorChofer;

    @SerializedName("IdUsuario")
    private int IdUsuario;

    public int getIdEstacion() {
        return IdEstacion;
    }

    public void setIdEstacion(int idEstacion) {
        IdEstacion = idEstacion;
    }

    public int getIdCAlmacenGas() {
        return IdCAlmacenGas;
    }

    public void setIdCAlmacenGas(int idCAlmacenGas) {
        IdCAlmacenGas = idCAlmacenGas;
    }

    public int getIdPuntoVenta() {
        return IdPuntoVenta;
    }

    public void setIdPuntoVenta(int idPuntoVenta) {
        IdPuntoVenta = idPuntoVenta;
    }

    public String getNombrePuntoVenta() {
        return NombrePuntoVenta;
    }

    public void setNombrePuntoVenta(String nombrePuntoVenta) {
        NombrePuntoVenta = nombrePuntoVenta;
    }

    public String getNombreOperador() {
        return NombreOperador;
    }

    public void setNombreOperador(String nombreOperador) {
        NombreOperador = nombreOperador;
    }

    public int getIdOperadorChofer() {
        return IdOperadorChofer;
    }

    public void setIdOperadorChofer(int idOperadorChofer) {
        IdOperadorChofer = idOperadorChofer;
    }

    public int getIdUsuario() {
        return IdUsuario;
    }

    public void setIdUsuario(int idUsuario) {
        IdUsuario = idUsuario;
    }
}
