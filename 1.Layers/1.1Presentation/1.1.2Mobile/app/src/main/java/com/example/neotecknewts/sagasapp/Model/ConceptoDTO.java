package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ConceptoDTO implements Serializable {
    @SerializedName("IdEmpresa")
    private int IdEmpresa;

    @SerializedName("Year")
    private int Year;

    @SerializedName("Mes")
    private int Mes;

    @SerializedName("Dia")
    private int Dia;

    @SerializedName("IdProducto")
    private int IdProducto;

    @SerializedName("IdLinea")
    private int IdLinea;

    @SerializedName("IdCategoria")
    private int IdCategoria;

    @SerializedName("IdUnidadMedida")
    private int IdUnidadMedida;

    @SerializedName("PrecioUnitarioProducto")
    private double PrecioUnitarioProducto;

    @SerializedName("PrecioUnitarioLt")
    private double PrecioUnitarioLt;

    @SerializedName("PrecioUnitarioKg")
    private double PrecioUnitarioKg;

    @SerializedName("DescuentoUnitarioProducto")
    private double DescuentoUnitarioProducto;

    @SerializedName("DescuentoUnitarioLt")
    private double DescuentoUnitarioLt;

    @SerializedName("DescuentoUnitarioKg")
    private double DescuentoUnitarioKg;

    @SerializedName("Cantidad")
    private double Cantidad;

    @SerializedName("CantidadLt")
    private double CantidadLt;

    @SerializedName("CantidadKg")
    private double CantidadKg;

    @SerializedName("DescuentoTotal")
    private double DescuentoTotal;

    @SerializedName("Subtotal")
    private double Subtotal;

    @SerializedName("IdTipoGas")
    private int IdTipoGas;

    @SerializedName("Concepto")
    private String Concepto;

    @SerializedName("PUnitario")
    private double PUnitario;

    @SerializedName("Descuento")
    private double Descuento;

    @SerializedName("LitrosDespachados")
    private double LitrosDespachados;


    public int getIdTipoGas() {
        return IdTipoGas;
    }

    public void setIdTipoGas(int idTipoGas) {
        IdTipoGas = idTipoGas;
    }

    public double getCantidad() {
        return Cantidad;
    }

    public void setCantidad(double cantidad) {
        Cantidad = cantidad;
    }

    public String getConcepto() {
        return Concepto;
    }

    public void setConcepto(String concepto) {
        Concepto = concepto;
    }

    public double getPUnitario() {
        return PUnitario;
    }

    public void setPUnitario(double PUnitario) {
        this.PUnitario = PUnitario;
    }

    public double getDescuento() {
        return Descuento;
    }

    public void setDescuento(double descuento) {
        Descuento = descuento;
    }

    public double getSubtotal() {
        return Subtotal;
    }

    public void setSubtotal(double subtotal) {
        Subtotal = subtotal;
    }

    public int getIdCategoria() {
        return IdCategoria;
    }

    public void setIdCategoria(int idCategoria) {
        IdCategoria = idCategoria;
    }

    public int getIdLinea() {
        return IdLinea;
    }

    public void setIdLinea(int idLinea) {
        IdLinea = idLinea;
    }

    public int getIdProducto() {
        return IdProducto;
    }

    public void setIdProducto(int idProducto) {
        IdProducto = idProducto;
    }

    public double getLitrosDespachados() {
        return LitrosDespachados;
    }

    public void setLitrosDespachados(double litrosDespachados) {
        LitrosDespachados = litrosDespachados;
    }

    public int getIdEmpresa() {
        return IdEmpresa;
    }

    public void setIdEmpresa(int idEmpresa) {
        IdEmpresa = idEmpresa;
    }

    public int getYear() {
        return Year;
    }

    public void setYear(int year) {
        Year = year;
    }

    public int getMes() {
        return Mes;
    }

    public void setMes(int mes) {
        Mes = mes;
    }

    public int getDia() {
        return Dia;
    }

    public void setDia(int dia) {
        Dia = dia;
    }

    public int getIdUnidadmedida() {
        return IdUnidadMedida;
    }

    public void setIdUnidadmedida(int idUnidadmedida) {
        IdUnidadMedida = idUnidadmedida;
    }

    public double getPrecioUnitarioProducto() {
        return PrecioUnitarioProducto;
    }

    public void setPrecioUnitarioProducto(double precioUnitarioProducto) {
        PrecioUnitarioProducto = precioUnitarioProducto;
    }

    public double getPrecioUnitarioLt() {
        return PrecioUnitarioLt;
    }

    public void setPrecioUnitarioLt(double precioUnitarioLt) {
        PrecioUnitarioLt = precioUnitarioLt;
    }

    public double getPrecioUnitarioKg() {
        return PrecioUnitarioKg;
    }

    public void setPrecioUnitarioKg(double precioUnitarioKg) {
        PrecioUnitarioKg = precioUnitarioKg;
    }

    public double getDescuentoUnitarioProducto() {
        return DescuentoUnitarioProducto;
    }

    public void setDescuentoUnitarioProducto(double descuentoUnitarioProducto) {
        DescuentoUnitarioProducto = descuentoUnitarioProducto;
    }

    public double getDescuentoUnitarioLt() {
        return DescuentoUnitarioLt;
    }

    public void setDescuentoUnitarioLt(double descuentoUnitarioLt) {
        DescuentoUnitarioLt = descuentoUnitarioLt;
    }

    public double getDescuentoUnitarioKg() {
        return DescuentoUnitarioKg;
    }

    public void setDescuentoUnitarioKg(double descuentoUnitarioKg) {
        DescuentoUnitarioKg = descuentoUnitarioKg;
    }

    public double getCantidadLt() {
        return CantidadLt;
    }

    public void setCantidadLt(double cantidadLt) {
        CantidadLt = cantidadLt;
    }

    public double getCantidadKg() {
        return CantidadKg;
    }

    public void setCantidadKg(double cantidadKg) {
        CantidadKg = cantidadKg;
    }

    public double getDescuentoTotal() {
        return DescuentoTotal;
    }

    public void setDescuentoTotal(double descuentoTotal) {
        DescuentoTotal = descuentoTotal;
    }
}
