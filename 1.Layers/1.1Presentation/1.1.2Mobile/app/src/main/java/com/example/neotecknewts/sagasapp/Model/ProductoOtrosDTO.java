package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ProductoOtrosDTO implements Serializable {

    @SerializedName("IdProducto")
    private int IdProducto;

    @SerializedName("Nombre")
    private String Producto;

    @SerializedName("IdCategoria")
    private int IdCategoria;

    @SerializedName("IdLinea")
    private int IdLinea;

    public int getIdProducto() {
        return IdProducto;
    }

    public void setIdProducto(int idProducto) {
        IdProducto = idProducto;
    }

    public String getProducto() {
        return Producto;
    }

    public void setProducto(String producto) {
        Producto = producto;
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
}
