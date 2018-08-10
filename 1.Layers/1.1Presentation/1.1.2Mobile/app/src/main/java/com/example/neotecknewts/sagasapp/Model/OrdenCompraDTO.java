package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.util.Date;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class OrdenCompraDTO {
    @SerializedName("IdOrdenCompra")
    private int IdOrdenCompra;

    @SerializedName("ProveedorNombreComercial")
    private String ProveedorNombreComercial;

    @SerializedName("ProveedorEstadoProvincia")
    private String ProveedorEstadoProvincia;

    @SerializedName("ProveedorMunicipio")
    private String ProveedorMunicipio;

    @SerializedName("ProveedorCodigoPostal")
    private String ProveedorCodigoPostal;

    @SerializedName("ProveedorColonia")
    private String ProveedorColonia;

    @SerializedName("ProveedorCalle")
    private String ProveedorCalle;

    @SerializedName("ProveedorNumExt")
    private String ProveedorNumExt;

    @SerializedName("ProveedorNumInt")
    private String ProveedorNumInt;

    @SerializedName("ProveedorRfc")
    private String ProveedorRfc;

    @SerializedName("ProveedorTelefono1")
    private String ProveedorTelefono1;

    @SerializedName("ProveedorTelefono2")
    private String ProveedorTelefono2;

    @SerializedName("ProveedorTelefono3")
    private String ProveedorTelefono3;

    @SerializedName("ProveedorCelular1")
    private String ProveedorCelular1;

    @SerializedName("ProveedorCelular2")
    private String ProveedorCelular2;

    @SerializedName("ProveedorCelular3")
    private String ProveedorCelular3
            ;
    @SerializedName("NumOrdenCompra")
    private String NumOrdenCompra;

    @SerializedName("FechaRequisicion")
    private Date FechaRequisicion;

    @SerializedName("SubtotalSinIva")
    private Double SubtotalSinIva;

    @SerializedName("Iva")
    private Double Iva;

    @SerializedName("Ieps")
    private Double Ieps;

    @SerializedName("Total")
    private Double Total;

    @SerializedName("Productos")
    private List<ProductoDTO> Productos;

    public int getIdOrdenCompra() {
        return IdOrdenCompra;
    }

    public void setIdOrdenCompra(int idOrdenCompra) {
        IdOrdenCompra = idOrdenCompra;
    }

    public String getProveedorNombreComercial() {
        return ProveedorNombreComercial;
    }

    public void setProveedorNombreComercial(String proveedorNombreComercial) {
        ProveedorNombreComercial = proveedorNombreComercial;
    }

    public String getProveedorEstadoProvincia() {
        return ProveedorEstadoProvincia;
    }

    public void setProveedorEstadoProvincia(String proveedorEstadoProvincia) {
        ProveedorEstadoProvincia = proveedorEstadoProvincia;
    }

    public String getProveedorMunicipio() {
        return ProveedorMunicipio;
    }

    public void setProveedorMunicipio(String proveedorMunicipio) {
        ProveedorMunicipio = proveedorMunicipio;
    }

    public String getProveedorCodigoPostal() {
        return ProveedorCodigoPostal;
    }

    public void setProveedorCodigoPostal(String proveedorCodigoPostal) {
        ProveedorCodigoPostal = proveedorCodigoPostal;
    }

    public String getProveedorColonia() {
        return ProveedorColonia;
    }

    public void setProveedorColonia(String proveedorColonia) {
        ProveedorColonia = proveedorColonia;
    }

    public String getProveedorCalle() {
        return ProveedorCalle;
    }

    public void setProveedorCalle(String proveedorCalle) {
        ProveedorCalle = proveedorCalle;
    }

    public String getProveedorNumExt() {
        return ProveedorNumExt;
    }

    public void setProveedorNumExt(String proveedorNumExt) {
        ProveedorNumExt = proveedorNumExt;
    }

    public String getProveedorNumInt() {
        return ProveedorNumInt;
    }

    public void setProveedorNumInt(String proveedorNumInt) {
        ProveedorNumInt = proveedorNumInt;
    }

    public String getProveedorRfc() {
        return ProveedorRfc;
    }

    public void setProveedorRfc(String proveedorRfc) {
        ProveedorRfc = proveedorRfc;
    }

    public String getProveedorTelefono1() {
        return ProveedorTelefono1;
    }

    public void setProveedorTelefono1(String proveedorTelefono1) {
        ProveedorTelefono1 = proveedorTelefono1;
    }

    public String getProveedorTelefono2() {
        return ProveedorTelefono2;
    }

    public void setProveedorTelefono2(String proveedorTelefono2) {
        ProveedorTelefono2 = proveedorTelefono2;
    }

    public String getProveedorCelular3() {
        return ProveedorCelular3;
    }

    public void setProveedorCelular3(String proveedorCelular3) {
        ProveedorCelular3 = proveedorCelular3;
    }

    public String getProveedorTelefono3() {
        return ProveedorTelefono3;
    }

    public void setProveedorTelefono3(String proveedorTelefono3) {
        ProveedorTelefono3 = proveedorTelefono3;
    }

    public String getProveedorCelular1() {
        return ProveedorCelular1;
    }

    public void setProveedorCelular1(String proveedorCelular1) {
        ProveedorCelular1 = proveedorCelular1;
    }

    public String getProveedorCelular2() {
        return ProveedorCelular2;
    }

    public void setProveedorCelular2(String proveedorCelular2) {
        ProveedorCelular2 = proveedorCelular2;
    }

    public String getNumOrdenCompra() {
        return NumOrdenCompra;
    }

    public void setNumOrdenCompra(String numOrdenCompra) {
        NumOrdenCompra = numOrdenCompra;
    }

    public Date getFechaRequisicion() {
        return FechaRequisicion;
    }

    public void setFechaRequisicion(Date fechaRequisicion) {
        FechaRequisicion = fechaRequisicion;
    }

    public Double getSubtotalSinIva() {
        return SubtotalSinIva;
    }

    public void setSubtotalSinIva(Double subtotalSinIva) {
        SubtotalSinIva = subtotalSinIva;
    }

    public Double getIva() {
        return Iva;
    }

    public void setIva(Double iva) {
        Iva = iva;
    }

    public Double getIeps() {
        return Ieps;
    }

    public void setIeps(Double ieps) {
        Ieps = ieps;
    }

    public Double getTotal() {
        return Total;
    }

    public void setTotal(Double total) {
        Total = total;
    }

    public List<ProductoDTO> getProductos() {
        return Productos;
    }

    public void setProductos(List<ProductoDTO> productos) {
        Productos = productos;
    }
}
