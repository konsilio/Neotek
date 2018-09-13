package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class AlmacenDTO {
    @SerializedName("NombreAlmacen")
    private String NombreAlmacen;

    @SerializedName("IdAlmacenGas")
    private int IdAlmacenGas;

    @SerializedName("PorcentajeMedidor")
    private double PorcentajeMedidor;

    @SerializedName("CantidadP5000")
    private double CantidadP5000;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("Cilindros")
    private List<CilindrosDTO> Cilindros;

    public AlmacenDTO (){
        Cilindros = new ArrayList<>();
    }

    public String getNombreAlmacen() {
        return NombreAlmacen;
    }

    public void setNombreAlmacen(String nombreAlmacen) {
        NombreAlmacen = nombreAlmacen;
    }

    public int getIdAlmacenGas() {
        return IdAlmacenGas;
    }

    public void setIdAlmacenGas(int idAlmacenGas) {
        IdAlmacenGas = idAlmacenGas;
    }

    public double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public double getCantidadP5000() {
        return CantidadP5000;
    }

    public void setCantidadP5000(double cantidadP5000) {
        CantidadP5000 = cantidadP5000;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public List<CilindrosDTO> getCilindros() {
        return Cilindros;
    }

    public void setCilindros(List<CilindrosDTO> cilindros) {
        Cilindros = cilindros;
    }
}

