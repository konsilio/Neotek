package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ReporteDto extends RespuestaDTO implements Serializable {
    @SerializedName("ClaveOperacion")
    private String ClaveOperacion;

    @SerializedName("Estacion")
    private String Estacion;

    @SerializedName("Fecha")
    private Date Fecha;

    @SerializedName("Hora")
    private String Hora;

    @SerializedName("PorcentajeSalida")
    private double PorcentajeSalida;

    @SerializedName("PorcentajeRegreso")
    private double PorcentajeRegreso;

    @SerializedName("LecturaInicial")
    private int LecturaInicial;

    @SerializedName("LitrosVenta")
    private int LitrosVenta;

    @SerializedName("Precio")
    private double Precio;

    @SerializedName("ImporteContado")
    private double ImporteContado;

    @SerializedName("ImporteCredito")
    private  double ImporteCredito;

    @SerializedName("Tanques")
    private List<TanquesDto> tanques;

    @SerializedName("OtrasVentas")
    private  List<OtrasVentasDTO> otrasVentas;

    @SerializedName("Carburacion")
    private double Carburacion;

    @SerializedName("KilosVenta")
    private double KilosVenta;

    @SerializedName("TOtrasVentas")
    private double TOtrasVentas;

    @SerializedName("IdCAlmacenGas")
    private int IdCAlmacenGas;

    public ReporteDto(){
        this.tanques = new ArrayList<>();
        this.otrasVentas = new ArrayList<>();
    }

    public String getClaveOperacion() {
        return ClaveOperacion;
    }

    public void setClaveOperacion(String claveOperacion) {
        ClaveOperacion = claveOperacion;
    }

    public String getEstacion() {
        return Estacion;
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
    }

    public double getPorcentajeRegreso() {
        return PorcentajeRegreso;
    }

    public void setPorcentajeRegreso(double porcentajeRegreso) {
        PorcentajeRegreso = porcentajeRegreso;
    }

    public int getLecturaInicial() {
        return LecturaInicial;
    }

    public void setLecturaInicial(int lecturaInicial) {
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
