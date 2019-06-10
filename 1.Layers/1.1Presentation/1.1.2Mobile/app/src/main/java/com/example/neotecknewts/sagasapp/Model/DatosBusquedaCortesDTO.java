/*
 * DatosBusquedaCortesDTO
 * Clase para el buscador por fecha de cortes y anticipos
 * en el se puede obtener, las ventas, los cortes , los anticipos
 * y los datos de la estación en la que se realizan estas acciónes
 * @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @company Neoteck
 * @date 26/12/2018 5:55 p.m.
 * @update 26/12/2018 5:55 p.m.
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosBusquedaCortesDTO extends RespuestaDTO implements Serializable {

    //region Banderas de datos
    /**
     * Determina si se encontraron anticipos
     */
    @SerializedName("hayAnticipos")
    private boolean hayAnticipos;

    /**
     * Determina si se encontraron cortes
     */
    @SerializedName("hayCortes")
    private boolean hayCortes;

    /**
     * Determina si se encontraron ventas
     */
    @SerializedName("hayVentas")
    private boolean hayVentas;
    //endregion
    //region Clases internas
    /**
     * Datos de los anticipos encontrados en la fecha especificada y en la estación
     * que se tiene en session
     */
    @SerializedName("anticipo")
    public AnticipoInfoDTO anticipo;
    /**
     * Datos de los cortes encontrados en la fecha especificada y en la estación
     * que se tiene en session
     */
    @SerializedName("corte")
    public CorteInfoDTO corte;

    /**
     * Estación de la session actual
     */
    @SerializedName("estacion")
    public EstacionDTO estacion;

    /**
     * Ventas encontradas en la fecha especificada y en la estación
     * que se tiene en session
     */
    @SerializedName("venta")
    public  VentaInfoDTO venta;

    //endregion

    //region Gets y sets
    public boolean isHayAnticipos() {
        return hayAnticipos;
    }

    public void setHayAnticipos(boolean hayAnticipos) {
        this.hayAnticipos = hayAnticipos;
    }

    public boolean isHayCortes() {
        return hayCortes;
    }

    public void setHayCortes(boolean hayCortes) {
        this.hayCortes = hayCortes;
    }

    public boolean isHayVentas() {
        return hayVentas;
    }

    public void setHayVentas(boolean hayVentas) {
        this.hayVentas = hayVentas;
    }

    public AnticipoInfoDTO getAnticipo() {
        return anticipo;
    }

    public void setAnticipo(AnticipoInfoDTO anticipo) {
        this.anticipo = anticipo;
    }

    public CorteInfoDTO getCorte() {
        return corte;
    }

    public void setCorte(CorteInfoDTO corte) {
        this.corte = corte;
    }

    public EstacionDTO getEstacion() {
        return estacion;
    }

    public void setEstacion(EstacionDTO estacion) {
        this.estacion = estacion;
    }

    public VentaInfoDTO getVenta() {
        return venta;
    }

    public void setVenta(VentaInfoDTO venta) {
        this.venta = venta;
    }
    //endregion

    //region Clases internas

    public class CorteInfoDTO implements Serializable {

        /**
         * Contiene el total de los cortes realizados en la fecha y estación
         */
        @SerializedName("totalCortes")
        public double totalCortes;

        /**
         * Contiene el listado de cortes realizados en la fecha y estación
         */
        @SerializedName("cortes")
        public List<CorteDTO> cortes;

        /**
         * Constructor de clase para inicializar la lista de cortes
         */
        public CorteInfoDTO(){
            this.cortes = new ArrayList<>();
        }
    }

    public class AnticipoInfoDTO implements Serializable {
        /**
         * Contiene el total actual de anticipos en esta fecha
         */
        @SerializedName("totalAnticipos")
        private double totalAnticipos;

        /**
         * Listado de anticipos registrados con esa fecha
         */
        @SerializedName("anticipos")
        private List<AnticiposDTO> anticipos;

        /**
         * Constructor de clase para inicializar la lista de
         * anticipos
         */
        public AnticipoInfoDTO(){
            this.anticipos = new ArrayList<>();
        }

        public double getTotalAnticipos() {
            return totalAnticipos;
        }

        public void setTotalAnticipos(double totalAnticipos) {
            this.totalAnticipos = totalAnticipos;
        }

        public List<AnticiposDTO> getAnticipos() {
            return anticipos;
        }

        public void setAnticipos(List<AnticiposDTO> anticipos) {
            this.anticipos = anticipos;
        }
    }

    public class EstacionDTO implements Serializable{
        /**
         * Reprecenta el Id de el CAlmacenGas
         */
        @SerializedName("IdAlmacenGas")
        private int IdCAlmacenGas;

        /**
         * Reprecenta el nombre de la estación,pipa o camioneta
         */
        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        /**
         * Reprecenta el porcentaje actual del medidor
         */
        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        public int getIdCAlmacenGas() {
            return IdCAlmacenGas;
        }

        public void setIdCAlmacenGas(int idCAlmacenGas) {
            IdCAlmacenGas = idCAlmacenGas;
        }

        public String getNombreAlmacen() {
            return NombreAlmacen;
        }

        public void setNombreAlmacen(String nombreAlmacen) {
            NombreAlmacen = nombreAlmacen;
        }

        public double getPorcentajeMedidor() {
            return PorcentajeMedidor;
        }

        public void setPorcentajeMedidor(double porcentajeMedidor) {
            PorcentajeMedidor = porcentajeMedidor;
        }
    }

    public class VentaInfoDTO implements Serializable{
        /**
         * Reprecenta la suma total de las ventas en esa fecha y en la estación
         */
        @SerializedName("totalVentas")
        private double totalVentas;

        /**
         * Reprecenta el listado de ventas realizadas en esa fecha y en la estación
         */
        @SerializedName("ventas")
        private List<VentaDTO> ventas;

        /**
         * Constructor de clase para incializar la lista de ventas
         */
        public VentaInfoDTO(){
            this.ventas = new ArrayList<>();
        }

        public double getTotalVentas() {
            return totalVentas;
        }

        public void setTotalVentas(double totalVentas) {
            this.totalVentas = totalVentas;
        }

        public List<VentaDTO> getVentas() {
            return ventas;
        }

        public void setVentas(List<VentaDTO> ventas) {
            this.ventas = ventas;
        }
    }


    //endregion
}
