package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

public class DatosCalibracionDTO extends RespuestaDTO implements Serializable {
    @SerializedName("estaciones")
    public List<EstacionDTO> estaciones;

    @SerializedName("medidores")
    public List<MedidorDTO> medidores;

    public List<EstacionDTO> getEstaciones() {
        return estaciones;
    }

    public void setEstaciones(List<EstacionDTO> estaciones) {
        this.estaciones = estaciones;
    }

    public List<MedidorDTO> getMedidores() {
        return medidores;
    }

    public void setMedidores(List<MedidorDTO> medidores) {
        this.medidores = medidores;
    }

    public class EstacionDTO  implements Serializable{
        @SerializedName("IdAlmacenGas")
        private int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private float PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private int IdTipoMedidor;

        @SerializedName("Medidor")
        private MedidorDTO medidor;

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

        public float getPorcentajeMedidor() {
            return PorcentajeMedidor;
        }

        public void setPorcentajeMedidor(float porcentajeMedidor) {
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

        public MedidorDTO getMedidor() {
            return medidor;
        }

        public void setMedidor(MedidorDTO medidor) {
            this.medidor = medidor;
        }
    }
}
