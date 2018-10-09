package com.example.neotecknewts.sagasapp.Model;

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
}
