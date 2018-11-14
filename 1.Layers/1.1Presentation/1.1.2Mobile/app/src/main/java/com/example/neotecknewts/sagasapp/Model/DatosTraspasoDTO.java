package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosTraspasoDTO extends RespuestaDTO implements Serializable {

    @SerializedName("predeterminada")
    private int predeterminada;

    @SerializedName("pipas")
    private List<PipasDTO> pipas;

    @SerializedName("estaciones")
    private List<EstacionesDTO> estaciones;

    @SerializedName("medidores")
    private  List<MedidorDTO> medidores;

    public DatosTraspasoDTO(){
        this.estaciones = new ArrayList<>();
        this.medidores = new ArrayList<>();
        this.pipas = new ArrayList<>();
    }

    public int getPredeterminada() {
        return predeterminada;
    }

    public void setPredeterminada(int predeterminada) {
        this.predeterminada = predeterminada;
    }

    public List<PipasDTO> getPipas() {
        return pipas;
    }

    public void setPipas(List<PipasDTO> pipas) {
        this.pipas = pipas;
    }

    public List<EstacionesDTO> getEstaciones() {
        return estaciones;
    }

    public void setEstaciones(List<EstacionesDTO> estaciones) {
        this.estaciones = estaciones;
    }

    public List<MedidorDTO> getMedidores() {
        return medidores;
    }

    public void setMedidores(List<MedidorDTO> medidores) {
        this.medidores = medidores;
    }

    //region Internal class
    public class PipasDTO extends RespuestaDTO implements Serializable {
        @SerializedName("Medidor")
        private MedidorDTO medidor;

        @SerializedName("IdAlmacenGas")
        private int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String  NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private  int IdTipoMedidor;

        @SerializedName("Cilindros")
        private  List<CilindrosDTO> cilindros;

        public PipasDTO(){
            this.cilindros = new ArrayList<>();
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

        public List<CilindrosDTO> getCilindros() {
            return cilindros;
        }

        public void setCilindros(List<CilindrosDTO> cilindros) {
            this.cilindros = cilindros;
        }
    }

    public class EstacionesDTO  extends RespuestaDTO implements Serializable {
            @SerializedName("Medidor")
            private MedidorDTO medidor;

            @SerializedName("IdAlmacenGas")
            private int IdAlmacenGas;

            @SerializedName("NombreAlmacen")
            private String NombreAlmacen;

            @SerializedName("PorcentajeMedidor")
            private double PorcentajeMedidor;

            @SerializedName("CantidadP5000")
            private int CantidadP5000;

            @SerializedName("IdTipoMedidor")
            private int IdTipoMedidor;

            @SerializedName("Cilindros")
            private List<CilindrosDTO> cilindros;

            public EstacionesDTO(){
                this.cilindros = new ArrayList<>();
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

        public List<CilindrosDTO> getCilindros() {
            return cilindros;
        }

        public void setCilindros(List<CilindrosDTO> cilindros) {
            this.cilindros = cilindros;
        }
    }
    //endregion
}
