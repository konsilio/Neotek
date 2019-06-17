package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 10/08/18.
 */

public class ProductoDTO {

    @SerializedName("IdOrdenCompra")
    private int IdOrdenCompra;

    @SerializedName("Producto")
    private String Producto;

    @SerializedName("UnidadMedida")
    private String UnidadMedida;

    @SerializedName("Cantidad")
    private int Cantidad;

    @SerializedName("Precio")
    private Double Precio;

    @SerializedName("Descuento")
    private Double Descuento;

    @SerializedName("IVA")
    private Double IVA;

    @SerializedName("IEPS")
    private Double IEPS;

    @SerializedName("Importe")
    private Double Importe;

    public int getIdOrdenCompra() {
        return IdOrdenCompra;
    }

    public void setIdOrdenCompra(int idOrdenCompra) {
        IdOrdenCompra = idOrdenCompra;
    }

    public String getProducto() {
        return Producto;
    }

    public void setProducto(String producto) {
        Producto = producto;
    }

    public String getUnidadMedida() {
        return UnidadMedida;
    }

    public void setUnidadMedida(String unidadMedida) {
        UnidadMedida = unidadMedida;
    }

    public int getCantidad() {
        return Cantidad;
    }

    public void setCantidad(int cantidad) {
        Cantidad = cantidad;
    }

    public Double getPrecio() {
        return Precio;
    }

    public void setPrecio(Double precio) {
        Precio = precio;
    }

    public Double getDescuento() {
        return Descuento;
    }

    public void setDescuento(Double descuento) {
        Descuento = descuento;
    }

    public Double getIVA() {
        return IVA;
    }

    public void setIVA(Double IVA) {
        this.IVA = IVA;
    }

    public Double getIEPS() {
        return IEPS;
    }

    public void setIEPS(Double IEPS) {
        this.IEPS = IEPS;
    }

    public Double getImporte() {
        return Importe;
    }

    public void setImporte(Double importe) {
        Importe = importe;
    }
}

