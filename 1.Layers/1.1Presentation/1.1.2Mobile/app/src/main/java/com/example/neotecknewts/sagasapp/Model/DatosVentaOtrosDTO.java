package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosVentaOtrosDTO extends RespuestaDTO implements Serializable {
    @SerializedName("Categorias")
    private List<CategoriaDTO> Categorias;

    @SerializedName("Lientas")
    private List<LineaDTO> Lineas;

    @SerializedName("Producto")
    private List<ProductoOtrosDTO> Producto;

    public DatosVentaOtrosDTO(){
        this.Categorias = new ArrayList<>();
        this.Lineas = new ArrayList<>();
        this.Producto = new ArrayList<>();
    }

    public List<CategoriaDTO> getCategorias() {
        return Categorias;
    }

    public void setCategorias(List<CategoriaDTO> categorias) {
        Categorias = categorias;
    }

    public List<LineaDTO> getLineas() {
        return Lineas;
    }

    public void setLineas(List<LineaDTO> lineas) {
        Lineas = lineas;
    }

    public List<ProductoOtrosDTO> getProducto() {
        return Producto;
    }

    public void setProducto(List<ProductoOtrosDTO> producto) {
        Producto = producto;
    }
}
