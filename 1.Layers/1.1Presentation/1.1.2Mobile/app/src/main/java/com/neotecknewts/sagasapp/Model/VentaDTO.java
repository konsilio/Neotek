/**
 * VentaDTO
 * Permite registrar los datos de la venta
 * @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @date 28/11/2018
 * @update 28/11/2018
 */
package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class VentaDTO implements Serializable {
    @SerializedName("Concepto")
    private List<ConceptoDTO> Concepto;

    @SerializedName("IdCliente")
    private int IdCliente;

    @SerializedName("Subtotal")
    private double Subtotal;

    @SerializedName("Iva")
    private double Iva;

    @SerializedName("Total")
    private double Total;

    @SerializedName("Factura")
    private boolean Factura;

    @SerializedName("Credito")
    private boolean Credito;

    @SerializedName("Bonificación")
    private boolean Bonificación;

    @SerializedName("Efectivo")
    private double Efectivo;

    @SerializedName("Fecha")
    private String Fecha;

    @SerializedName("Hora")
    private String Hora;

    @SerializedName("Cambio")
    private double Cambio;

    @SerializedName("SinNumero")
    private boolean SinNumero;

    @SerializedName("FolioVenta")
    private String FolioVenta;

    @SerializedName("Nombre")
    private String Nombre;

    @SerializedName("RFC")
    private String RFC;

    @SerializedName("TieneCredito")
    private boolean TieneCredito;

    @SerializedName("EsVentaGas")
    private boolean EsVentaGas;

    @SerializedName("RazonSocial")
    private String RazonSocial;

    @SerializedName("VentaExtraordinaria")
    private boolean VentaExtraforanea;

    //region Atributos de tipo reporte
    private boolean EsSinNumero;

    private boolean EsBusqueda;

    private boolean EsRegistro;
    //endregion

    private String Estacion;

    private double LimiteCreditoCliente;

    public String getFecha() {
        return Fecha;
    }

    public  VentaDTO(){
        this.Concepto = new ArrayList<>();
    }

    public void setFecha(String fecha) {
        Fecha = fecha;
    }

    public String getHora() {
        return Hora;
    }

    public void setHora(String hora) {
        Hora = hora;
    }

    public double getCambio() {
        return Cambio;
    }

    public void setCambio(double cambio) {
        Cambio = cambio;
    }

    public double getSubtotal() {
        return Subtotal;
    }

    public void setSubtotal(double subtotal) {
        Subtotal = subtotal;
    }

    public double getIva() {
        return Iva;
    }

    public void setIva(double iva) {
        Iva = iva;
    }

    public double getTotal() {
        return Total;
    }

    public void setTotal(double total) {
        Total = total;
    }

    public boolean isFactura() {
        return Factura;
    }

    public void setFactura(boolean factura) {
        Factura = factura;
    }

    public boolean isCredito() {
        return Credito;
    }

    public void setCredito(boolean credito) {
        Credito = credito;
    }

    public boolean isBonificación() {
        return Bonificación;
    }

    public void setBonificación(boolean bonificación) {
        Bonificación = bonificación;
    }

    public double getEfectivo() {
        return Efectivo;
    }

    public void setEfectivo(double efectivo) {
        Efectivo = efectivo;
    }

    public List<ConceptoDTO> getConcepto() {
        return Concepto;
    }

    public void setConcepto(List<ConceptoDTO> concepto) {
        Concepto = concepto;
    }

    public int getIdCliente() {
        return IdCliente;
    }

    public void setIdCliente(int idCliente) {
        IdCliente = idCliente;
    }

    public boolean isSinNumero() {
        return SinNumero;
    }

    public void setSinNumero(boolean sinNumero) {
        SinNumero = sinNumero;
    }

    public String getFolioVenta() {
        return FolioVenta;
    }

    public void setFolioVenta(String folioVenta) {
        FolioVenta = folioVenta;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public String getRFC() {
        return RFC;
    }

    public void setRFC(String RFC) {
        this.RFC = RFC;
    }

    public boolean isTieneCredito() {
        return TieneCredito;
    }

    public void setTieneCredito(boolean tieneCredito) {
        TieneCredito = tieneCredito;
    }

    public boolean isEsVentaGas() {
        return EsVentaGas;
    }

    public void setEsVentaGas(boolean esVentaGas) {
        EsVentaGas = esVentaGas;
    }

    public String getRazonSocial() {
        return RazonSocial;
    }

    public void setRazonSocial(String razonSocial) {
        RazonSocial = razonSocial;
    }

    public boolean isEsSinNumero() {
        return EsSinNumero;
    }

    public void setEsSinNumero(boolean esSinNumero) {
        EsSinNumero = esSinNumero;
    }

    public boolean isEsBusqueda() {
        return EsBusqueda;
    }

    public void setEsBusqueda(boolean esBusqueda) {
        EsBusqueda = esBusqueda;
    }

    public boolean isEsRegistro() {
        return EsRegistro;
    }

    public void setEsRegistro(boolean esRegistro) {
        EsRegistro = esRegistro;
    }

    public String getEstacion() {
        return Estacion;
    }

    public void setEstacion(String estacion) {
        Estacion = estacion;
    }

    public double getLimiteCreditoCliente() {
        return LimiteCreditoCliente;
    }

    public void setLimiteCreditoCliente(double limiteCreditoCliente) {
        LimiteCreditoCliente = limiteCreditoCliente;
    }

    public boolean isVentaExtraforanea() {
        return VentaExtraforanea;
    }

    public void setVentaExtraforanea(boolean ventaExtraforanea) {
        VentaExtraforanea = ventaExtraforanea;
    }
}
