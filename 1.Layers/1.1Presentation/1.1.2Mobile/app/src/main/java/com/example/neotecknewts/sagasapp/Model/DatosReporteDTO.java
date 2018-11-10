/*
 * DatosReporteDTO
 * Permite obtener el listado de estaciónes para
 * el reporte del día
 * @author Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @company Neoteck
 * @date 09/11/2018
 * @update 09/11/2018
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosReporteDTO extends RespuestaDTO implements Serializable {

    @SerializedName("Almacenes")
    private List<AlmacenesDTO> Almacenes;


    public List<AlmacenesDTO> getAlmacenes() {
        return Almacenes;
    }

    public void setAlmacenes(List<AlmacenesDTO> almacenes) {
        Almacenes = almacenes;
    }

    public DatosReporteDTO(){
        this.Almacenes = new ArrayList<>();
    }

    /**
     * AlmacenesDTO
     * Clase DTO para guardar el listado de almacenes o pipas
     */
    public class AlmacenesDTO implements Serializable{

        @SerializedName("IdAlmacenGas")
        private int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private  int IdTipoMedidor;

        @SerializedName("Clindros")
        private List<CilindrosDTO> Clindros;

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

        public double getPorcentajeMedidor() {
            return PorcentajeMedidor;
        }

        public void setPorcentajeMedidor(double porcentajeMedidor) {
            PorcentajeMedidor = porcentajeMedidor;
        }

        public int getCantidadP5000() {
            return CantidadP5000;
        }

        public void setCantidadP5000(int cantidadP5000) {
            CantidadP5000 = cantidadP5000;
        }

        public int getIdTipoMedidor() {
            return IdTipoMedidor;
        }

        public void setIdTipoMedidor(int idTipoMedidor) {
            IdTipoMedidor = idTipoMedidor;
        }

        public List<CilindrosDTO> getClindros() {
            return Clindros;
        }

        public void setClindros(List<CilindrosDTO> clindros) {
            Clindros = clindros;
        }
    }
}
