package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class CamionetaDTO implements Serializable  {
    @SerializedName("IdCAlmacen")
    private int IdCAlmacen;

    @SerializedName("Numero")
    private String Numero;

    @SerializedName("PorcentajeActual")
    private double PorcentajeActual;

    @SerializedName("CantidadActual")
    private double CantidadActual;

    @SerializedName("CantidadActualLt")
    private double CantidadActualLt;

    @SerializedName("CantidadActualKg")
    private double CantidadActualKg;

    @SerializedName("Medidor")
    public MedidorDTO medidor;

    //region Anticipos y cortes
    @SerializedName("IdAlmacenGas")
    private int IdAlmacenGas;

    @SerializedName("NombreAlmacen")
    private String NombreAlmacen;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("P5000Final")
    private int P5000Final;

    @SerializedName("CantidadP5000")
    private int CantidadP5000;

    @SerializedName("AnticiposEstacion")
    private AnticiposEstacionDTO anticiposEstacionDTO;
    //endregion

    public int getIdCAlmacen() {
        return IdCAlmacen;
    }

    public void setIdCAlmacen(int idCAlmacen) {
        IdCAlmacen = idCAlmacen;
    }

    public String getNumero() {
        return Numero;
    }

    public void setNumero(String numero) {
        Numero = numero;
    }

    public double getPorcentajeActual() {
        return PorcentajeActual;
    }

    public void setPorcentajeActual(double porcentajeActual) {
        PorcentajeActual = porcentajeActual;
    }

    public double getCantidadActual() {
        return CantidadActual;
    }

    public void setCantidadActual(double cantidadActual) {
        CantidadActual = cantidadActual;
    }

    public double getCantidadActualLt() {
        return CantidadActualLt;
    }

    public void setCantidadActualLt(double cantidadActualLt) {
        CantidadActualLt = cantidadActualLt;
    }

    public double getCantidadActualKg() {
        return CantidadActualKg;
    }

    public void setCantidadActualKg(double cantidadActualKg) {
        CantidadActualKg = cantidadActualKg;
    }

    public MedidorDTO getMedidor() {
        return medidor;
    }

    public void setMedidor(MedidorDTO medidor) {
        this.medidor = medidor;
    }

    public int getIdAlmacenGas() {
        return IdAlmacenGas;
    }

    public void setIdAlmacenGas(int idAlmacenGas) {
        IdAlmacenGas = idAlmacenGas;
    }

    public String getNombreAlmacen() {
        return NombreAlmacen;
    }

    public void setNombreAlmacen(String nombreAlmacen) {
        NombreAlmacen = nombreAlmacen;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public int getP5000Final() {
        return P5000Final;
    }

    public void setP5000Final(int p5000Final) {
        P5000Final = p5000Final;
    }

    public int getCantidadP5000() {
        return CantidadP5000;
    }

    public void setCantidadP5000(int cantidadP5000) {
        CantidadP5000 = cantidadP5000;
    }

    public AnticiposEstacionDTO getAnticiposEstacionDTO() {
        return anticiposEstacionDTO;
    }

    public void setAnticiposEstacionDTO(AnticiposEstacionDTO anticiposEstacionDTO) {
        this.anticiposEstacionDTO = anticiposEstacionDTO;
    }
}
