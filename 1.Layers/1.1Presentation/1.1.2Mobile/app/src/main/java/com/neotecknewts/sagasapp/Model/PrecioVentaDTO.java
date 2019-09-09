/*
 * PrecioVentaDTO
 * Clase modelo para extraer los valores de venta de gas actuales desde el servici
 * @author Jorge Omar Tovar Martínez
 * @company Neotrck
 * @date 20/11/2018
 * @update 20/11/2018
 */
package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.Date;

public class PrecioVentaDTO extends RespuestaDTO implements Serializable {
    @SerializedName("IdPrecioVenta")
    private int IdPrecioVenta;

    @SerializedName("IdEmpresa")
    private int IdEmpresa;

    @SerializedName("IdPrecioVentaEstatus")
    private int IdPrecioVentaEstatus;

    @SerializedName("IdCategoria")
    private int IdCategoria;

    @SerializedName("IdProductoLinea")
    private int IdProductoLinea;

    @SerializedName("IdProducto")
    private int IdProducto;

    @SerializedName("Categoria")
    private String Categoria;

    @SerializedName("Linea")
    private String Linea;

    @SerializedName("Producto")
    private String Producto;

    @SerializedName("PrecioActual")
    private double PrecioActual ;

    @SerializedName("PrecioPemexKg")
    private double PrecioPemexKg;

    @SerializedName("PrecioPemexLt")
    private double PrecioPemexLt;

    @SerializedName("UtilidadEsperadaKg")
    private double UtilidadEsperadaKg;

    @SerializedName("UtilidadEsperadaLt")
    private double UtilidadEsperadaLt;

    @SerializedName("PrecioSalida")
    private double PrecioSalida;

    @SerializedName("PrecioSalidaKg")
    private double PrecioSalidaKg;

    @SerializedName("PrecioSalidaLt")
    private double PrecioSalidaLt;

    @SerializedName("EsGas")
    private boolean EsGas;

    @SerializedName("FechaProgramada")
    private Date FechaProgramada;

    @SerializedName("FechaRegistro")
    private Date FechaRegistro;

    @SerializedName("FechaVencimiento")
    private Date FechaVencimiento;

    @SerializedName("Activo")
    private boolean Activo;

    @SerializedName("PrecioVentaEstatus")
    private String PrecioVentaEstatus;

    @SerializedName("CategoriaProducto")
    private String CategoriaProducto;

    @SerializedName("LineaProducto")
    private String LineaProducto;

    @SerializedName("Empresa")
    private String Empresa;

    @SerializedName("IdUnidadMedida")
    private int IdUnidadMedida;

    @SerializedName("UnidadMedida")
    private String UnidadMedida;

    public int getIdPrecioVenta() {
        return IdPrecioVenta;
    }

    public void setIdPrecioVenta(int idPrecioVenta) {
        IdPrecioVenta = idPrecioVenta;
    }

    public int getIdEmpresa() {
        return IdEmpresa;
    }

    public void setIdEmpresa(int idEmpresa) {
        IdEmpresa = idEmpresa;
    }

    public int getIdPrecioVentaEstatus() {
        return IdPrecioVentaEstatus;
    }

    public void setIdPrecioVentaEstatus(int idPrecioVentaEstatus) {
        IdPrecioVentaEstatus = idPrecioVentaEstatus;
    }

    public int getIdCategoria() {
        return IdCategoria;
    }

    public void setIdCategoria(int idCategoria) {
        IdCategoria = idCategoria;
    }

    public int getIdProductoLinea() {
        return IdProductoLinea;
    }

    public void setIdProductoLinea(int idProductoLinea) {
        IdProductoLinea = idProductoLinea;
    }

    public int getIdProducto() {
        return IdProducto;
    }

    public void setIdProducto(int idProducto) {
        IdProducto = idProducto;
    }

    public String getCategoria() {
        return Categoria;
    }

    public void setCategoria(String categoria) {
        Categoria = categoria;
    }

    public String getLinea() {
        return Linea;
    }

    public void setLinea(String linea) {
        Linea = linea;
    }

    public String getProducto() {
        return Producto;
    }

    public void setProducto(String producto) {
        Producto = producto;
    }

    public double getPrecioActual() {
        return PrecioActual = 8.12;
    }

    public void setPrecioActual(double precioActual) {
        PrecioActual = precioActual;
    }

    public double getPrecioPemexKg() {
        return PrecioPemexKg;
    }

    public void setPrecioPemexKg(double precioPemexKg) {
        PrecioPemexKg = precioPemexKg;
    }

    public double getPrecioPemexLt() {
        return PrecioPemexLt;
    }

    public void setPrecioPemexLt(double precioPemexLt) {
        PrecioPemexLt = precioPemexLt;
    }

    public double getUtilidadEsperadaKg() {
        return UtilidadEsperadaKg;
    }

    public void setUtilidadEsperadaKg(double utilidadEsperadaKg) {
        UtilidadEsperadaKg = utilidadEsperadaKg;
    }

    public double getUtilidadEsperadaLt() {
        return UtilidadEsperadaLt;
    }

    public void setUtilidadEsperadaLt(double utilidadEsperadaLt) {
        UtilidadEsperadaLt = utilidadEsperadaLt;
    }

    public double getPrecioSalida() {
        return PrecioSalida;
    }

    public void setPrecioSalida(double precioSalida) {
        PrecioSalida = precioSalida;
    }

    public double getPrecioSalidaKg() {
        return PrecioSalidaKg;
    }

    public void setPrecioSalidaKg(double precioSalidaKg) {
        PrecioSalidaKg = precioSalidaKg;
    }

    public double getPrecioSalidaLt() {
        return PrecioSalidaLt;
    }

    public void setPrecioSalidaLt(double precioSalidaLt) {
        PrecioSalidaLt = precioSalidaLt;
    }

    public boolean isEsGas() {
        return EsGas;
    }

    public void setEsGas(boolean esGas) {
        EsGas = esGas;
    }

    public Date getFechaProgramada() {
        return FechaProgramada;
    }

    public void setFechaProgramada(Date fechaProgramada) {
        FechaProgramada = fechaProgramada;
    }

    public Date getFechaRegistro() {
        return FechaRegistro;
    }

    public void setFechaRegistro(Date fechaRegistro) {
        FechaRegistro = fechaRegistro;
    }

    public Date getFechaVencimiento() {
        return FechaVencimiento;
    }

    public void setFechaVencimiento(Date fechaVencimiento) {
        FechaVencimiento = fechaVencimiento;
    }

    public boolean isActivo() {
        return Activo;
    }

    public void setActivo(boolean activo) {
        Activo = activo;
    }

    public String getPrecioVentaEstatus() {
        return PrecioVentaEstatus;
    }

    public void setPrecioVentaEstatus(String precioVentaEstatus) {
        PrecioVentaEstatus = precioVentaEstatus;
    }

    public String getCategoriaProducto() {
        return CategoriaProducto;
    }

    public void setCategoriaProducto(String categoriaProducto) {
        CategoriaProducto = categoriaProducto;
    }

    public String getLineaProducto() {
        return LineaProducto;
    }

    public void setLineaProducto(String lineaProducto) {
        LineaProducto = lineaProducto;
    }

    public String getEmpresa() {
        return Empresa;
    }

    public void setEmpresa(String empresa) {
        Empresa = empresa;
    }

    public int getIdUnidadMedida() {
        return IdUnidadMedida;
    }

    public void setIdUnidadMedida(int idUnidadMedida) {
        IdUnidadMedida = idUnidadMedida;
    }

    public String getUnidadMedida() {
        return UnidadMedida;
    }

    public void setUnidadMedida(String unidadMedida) {
        UnidadMedida = unidadMedida;
    }

    @Override
    public String toString() {
        return "PrecioVentaDTO{" +
                "IdPrecioVenta=" + IdPrecioVenta +
                ", IdEmpresa=" + IdEmpresa +
                ", IdPrecioVentaEstatus=" + IdPrecioVentaEstatus +
                ", IdCategoria=" + IdCategoria +
                ", IdProductoLinea=" + IdProductoLinea +
                ", IdProducto=" + IdProducto +
                ", Categoria='" + Categoria + '\'' +
                ", Linea='" + Linea + '\'' +
                ", Producto='" + Producto + '\'' +
                ", PrecioActual=" + PrecioActual +
                ", PrecioPemexKg=" + PrecioPemexKg +
                ", PrecioPemexLt=" + PrecioPemexLt +
                ", UtilidadEsperadaKg=" + UtilidadEsperadaKg +
                ", UtilidadEsperadaLt=" + UtilidadEsperadaLt +
                ", PrecioSalida=" + PrecioSalida +
                ", PrecioSalidaKg=" + PrecioSalidaKg +
                ", PrecioSalidaLt=" + PrecioSalidaLt +
                ", EsGas=" + EsGas +
                ", FechaProgramada=" + FechaProgramada +
                ", FechaRegistro=" + FechaRegistro +
                ", FechaVencimiento=" + FechaVencimiento +
                ", Activo=" + Activo +
                ", PrecioVentaEstatus='" + PrecioVentaEstatus + '\'' +
                ", CategoriaProducto='" + CategoriaProducto + '\'' +
                ", LineaProducto='" + LineaProducto + '\'' +
                ", Empresa='" + Empresa + '\'' +
                ", IdUnidadMedida=" + IdUnidadMedida +
                ", UnidadMedida='" + UnidadMedida + '\'' +
                '}';
    }
}
