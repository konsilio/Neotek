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

    public  VentaDTO(){
        this.Concepto = new ArrayList<>();
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
}
