package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

public class DatosEmpresaConfiguracionDTO extends RespuestaDTO{
    @SerializedName("FactorLitrosAKilos")
    private double FactorLitrosAKilos;

    @SerializedName("FactorGalonALitros")
    private double FactorGalonALitros;

    @SerializedName("FactorCompraLitroAKilos")
    private double FactorCompraLitroAKilos;

    @SerializedName("FactorFleteGas")
    private double FactorFleteGas;

    @SerializedName("CierreInventario")
    private Date CierreInventario;

    @SerializedName("InventarioSano")
    private int InventarioSano;

    @SerializedName("InventarioCritico")
    private int InventarioCritico;

    @SerializedName("MaxRemaGaseraMensual")
    private double MaxRemaGaseraMensual;

    public double getFactorLitrosAKilos() {
        return FactorLitrosAKilos;
    }

    public void setFactorLitrosAKilos(double factorLitrosAKilos) {
        FactorLitrosAKilos = factorLitrosAKilos;
    }

    public double getFactorGalonALitros() {
        return FactorGalonALitros;
    }

    public void setFactorGalonALitros(double factorGalonALitros) {
        FactorGalonALitros = factorGalonALitros;
    }

    public double getFactorCompraLitroAKilos() {
        return FactorCompraLitroAKilos;
    }

    public void setFactorCompraLitroAKilos(double factorCompraLitroAKilos) {
        FactorCompraLitroAKilos = factorCompraLitroAKilos;
    }

    public double getFactorFleteGas() {
        return FactorFleteGas;
    }

    public void setFactorFleteGas(double factorFleteGas) {
        FactorFleteGas = factorFleteGas;
    }

    public Date getCierreInventario() {
        return CierreInventario;
    }

    public void setCierreInventario(Date cierreInventario) {
        CierreInventario = cierreInventario;
    }

    public int getInventarioSano() {
        return InventarioSano;
    }

    public void setInventarioSano(int inventarioSano) {
        InventarioSano = inventarioSano;
    }

    public int getInventarioCritico() {
        return InventarioCritico;
    }

    public void setInventarioCritico(int inventarioCritico) {
        InventarioCritico = inventarioCritico;
    }

    public double getMaxRemaGaseraMensual() {
        return MaxRemaGaseraMensual;
    }

    public void setMaxRemaGaseraMensual(double maxRemaGaseraMensual) {
        MaxRemaGaseraMensual = maxRemaGaseraMensual;
    }
}
