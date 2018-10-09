package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ProductoOtrosDTO implements Serializable {

    @SerializedName("IdProducto")
    private int IdProducto;

    @SerializedName("Producto")
    private String Producto;

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
}
