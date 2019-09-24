/*
 *  ReporteDto
 *  Modelo DTO para almacenar los datos retornados por el servicio del reporte del día
 *  @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 *  @company Neoteck
 *  @date   29/10/2018 18:32
 *  @update 26/11/2018 9:36
 */
package com.neotecknewts.sagasapp.Model;

import android.util.Log;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ReporteDto extends RespuestaDTO implements Serializable {

    @SerializedName("IdCAlmacenGas")
    private int IdCAlmacenGas;

    @SerializedName("NombreCAlmacen")
    private String NombreCAlmacen;

    @SerializedName("Estacion")
    private String Estacion;

    @SerializedName("Medidor")
    private MedidorDTO Medidor;

    @SerializedName("ClaveReporte")
    private String ClaveOperacion;

    @SerializedName("Fecha")
    private Date Fecha;

    @SerializedName("Hora")
    private String Hora;

    @SerializedName("PorcentajeSalida")
    private double PorcentajeSalida;

    @SerializedName("PorcentajeRegreso")
    private double PorcentajeRegreso;

    @SerializedName("LecturaInicial")
    private LecturaDTO LecturaInicial;

    @SerializedName("LecturaFinal")
    private LecturaDTO LecturaFinal;

    @SerializedName("LitrosVenta")
    private int LitrosVenta;

    @SerializedName("Precio")
    private double Precio;

    @SerializedName("Importe")
    private double ImporteContado;

    @SerializedName("ImporteCredito")
    private  double ImporteCredito;

    @SerializedName("Tanques")
    private List<TanquesDto> tanques;

    @SerializedName("OtrasVentas")
    private  List<OtrasVentasDTO> otrasVentas;

    @SerializedName("Carburacion")
    private double Carburacion;

    @SerializedName("KilosDeVenta")
    private double KilosVenta;

    @SerializedName("OtrasVentasTotal")
    private double TOtrasVentas;

    @SerializedName("EsCamioneta")
    private boolean EsCamioneta;

    @SerializedName("Bonificacion")
    private double Bonificacion;

    public ReporteDto(){
        this.tanques = new ArrayList<>();
        this.otrasVentas = new ArrayList<>();
    }

    public String getClaveOperacion() {
        //return ClaveOperacion;}
        Log.d("Claveoperacion",ClaveOperacion+"");
         return ClaveOperacion!=null?ClaveOperacion:"";
    }

    public void setNombreCAlmacen(String nombreCAlmacen) {
        NombreCAlmacen = nombreCAlmacen;
    }

    public String getNombreCAlmacen() {
        return NombreCAlmacen!=null?NombreCAlmacen:"";
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public String getEstacion() {
        return Estacion!=null?Estacion:"";
    }

    public void setEstacion(String estacion) {
        Estacion = estacion;
    }

    public Date getFecha() {
        return Fecha;
    }

    public void setFecha(Date fecha) {
        Fecha = fecha;
    }

    public String getHora() {
        return Hora;
    }

    public void setHora(String hora) {
        Hora = hora;
    }

    public double getPorcentajeSalida() {
        return PorcentajeSalida;
    }

    public void setPorcentajeSalida(double porcentajeSalida) {
        PorcentajeSalida = porcentajeSalida;
        Log.d("",porcentajeSalida+"");
    }

    public double getPorcentajeRegreso() {
        return PorcentajeRegreso;
    }

    public void setPorcentajeRegreso(double porcentajeRegreso) {
        PorcentajeRegreso = porcentajeRegreso;
    }

    public LecturaDTO getLecturaInicial() {
        return LecturaInicial;
    }

    public void setLecturaInicial(LecturaDTO lecturaInicial) {
        LecturaInicial = lecturaInicial;
    }

    public int getLitrosVenta() {
        return LitrosVenta;
    }

    public void setLitrosVenta(int litrosVenta) {
        LitrosVenta = litrosVenta;
    }

    public double getPrecio() {
        return Precio;
    }

    public void setPrecio(double precio) {
        Precio = precio;
    }

    public double getImporteContado() {
        return ImporteContado;
    }

    public void setImporteContado(double importeContado) {
        ImporteContado = importeContado;
    }

    public double getImporteCredito() {
        return ImporteCredito;
    }

    public void setImporteCredito(double importeCredito) {
        ImporteCredito = importeCredito;
    }

    public double getCarburacion() {
        return Carburacion;
    }

    public void setCarburacion(double carburacion) {
        Carburacion = carburacion;
    }

    public double getKilosVenta() {
        return KilosVenta;
    }

    public void setKilosVenta(double kilosVenta) {
        KilosVenta = kilosVenta;
    }

    public double getTOtrasVentas() {
        return TOtrasVentas;
    }

    public void setTOtrasVentas(double TOtrasVentas) {
        this.TOtrasVentas = TOtrasVentas;
    }

    public List<TanquesDto> getTanques() {
        return tanques;
    }

    public void setTanques(List<TanquesDto> tanques) {
        this.tanques = tanques;
    }

    public List<OtrasVentasDTO> getOtrasVentas() {
        return otrasVentas;
    }

    public void setOtrasVentas(List<OtrasVentasDTO> otrasVentas) {
        this.otrasVentas = otrasVentas;
    }

    public int getIdCAlmacenGas() {
        return IdCAlmacenGas;
    }

    public void setIdCAlmacenGas(int idCAlmacenGas) {
        IdCAlmacenGas = idCAlmacenGas;
    }

    public MedidorDTO getMedidor() {
        return Medidor;
    }

    public void setMedidor(MedidorDTO medidor) {
        Medidor = medidor;
    }

    public LecturaDTO getLecturaFinal() {
        return LecturaFinal;
    }

    public void setLecturaFinal(LecturaDTO lecturaFinal) {
        LecturaFinal = lecturaFinal;
    }

    public boolean isEsCamioneta() {
        return EsCamioneta;
    }

    public double isBonificacion() {
        return Bonificacion;
    }

    public void setBonificacion(double bonificacion) {
        Bonificacion = bonificacion;
    }

    public void setEsCamioneta(boolean esCamioneta) {
        EsCamioneta = esCamioneta;
    }

    @Override
    public String toString() {
        return "ReporteDto{" +
                "IdCAlmacenGas=" + IdCAlmacenGas +
                ", Estacion='" + Estacion + '\'' +
                ", Medidor=" + Medidor +
                ", ClaveOperacion='" + ClaveOperacion + '\'' +
                ", Fecha=" + Fecha +
                ", Hora='" + Hora + '\'' +
                ", PorcentajeSalida=" + PorcentajeSalida +
                ", PorcentajeRegreso=" + PorcentajeRegreso +
                ", LecturaInicial=" + LecturaInicial +
                ", LecturaFinal=" + LecturaFinal +
                ", LitrosVenta=" + LitrosVenta +
                ", Precio=" + Precio +
                ", ImporteContado=" + ImporteContado +
                ", ImporteCredito=" + ImporteCredito +
                ", tanques=" + tanques +
                ", otrasVentas=" + otrasVentas +
                ", Carburacion=" + Carburacion +
                ", KilosVenta=" + KilosVenta +
                ", TOtrasVentas=" + TOtrasVentas +
                ", EsCamioneta=" + EsCamioneta +
                '}';
    }

    public class TanquesDto {
        @SerializedName("Tanques")
        private String Tanques;

        @SerializedName("Normal")
        private  int Normal;

        @SerializedName("Venta")
        private int Venta;

        public String getTanques() {
            return Tanques;
        }

        public void setTanques(String tanques) {
            Tanques = tanques;
        }

        public int getNormal() {
            return Normal;
        }

        public void setNormal(int normal) {
            Normal = normal;
        }

        public int getVenta() {
            return Venta;
        }

        public void setVenta(int venta) {
            Venta = venta;
        }
    }

    public class OtrasVentasDTO {

        @SerializedName("Producto")
        private String Producto;

        @SerializedName("Cantidad")
        private int Cantidad;

        public String getProducto() {
            return Producto;
        }

        public void setProducto(String producto) {
            Producto = producto;
        }

        public int getCantidad() {
            return Cantidad;
        }

        public void setCantidad(int cantidad) {
            Cantidad = cantidad;
        }
    }
}
