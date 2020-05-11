package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosAutoconsumoDTO extends RespuestaDTO implements Serializable {

    public DatosAutoconsumoDTO(){
        this.estacionEntradaDTOList = new ArrayList<>();
        this.estacionSalidaDTOList = new ArrayList<>();
    }

    @SerializedName("EstacionEntrada")
    private List<EstacionEntradaDTO> estacionEntradaDTOList;

    @SerializedName("EstacionSalida")
    private List<EstacionSalidaDTO> estacionSalidaDTOList;

    @SerializedName("Predeterminada")
    private  PredeterminadaDTO predeterminadaDTO;

    public List<EstacionEntradaDTO> getEstacionEntradaDTOList() {
        return estacionEntradaDTOList;
    }

    public void setEstacionEntradaDTOList(List<EstacionEntradaDTO> estacionEntradaDTOList) {
        this.estacionEntradaDTOList = estacionEntradaDTOList;
    }

    public List<EstacionSalidaDTO> getEstacionSalidaDTOList() {
        return estacionSalidaDTOList;
    }

    public void setEstacionSalidaDTOList(List<EstacionSalidaDTO> estacionSalidaDTOList) {
        this.estacionSalidaDTOList = estacionSalidaDTOList;
    }

    public PredeterminadaDTO getPredeterminadaDTO() {
        return predeterminadaDTO;
    }

    public void setPredeterminadaDTO(PredeterminadaDTO predeterminadaDTO) {
        this.predeterminadaDTO = predeterminadaDTO;
    }

    public class EstacionEntradaDTO  extends RespuestaDTO implements Serializable{

        public EstacionEntradaDTO(){
            this.Cilindros = new ArrayList<>();
        }

        @SerializedName("Medidor")
        private MedidorDTO Medidor;

        @SerializedName("IdAlmacenGas")
        private  int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private int IdTipoMedidor;

        @SerializedName("Cilindros")
        private List<CilindrosDTO> Cilindros;

        public MedidorDTO getMedidor() {
            return Medidor;
        }

        public void setMedidor(MedidorDTO medidor) {
            Medidor = medidor;
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
            return Cilindros;
        }

        public void setCilindros(List<CilindrosDTO> cilindros) {
            Cilindros = cilindros;
        }

        @Override
        public String toString() {
            return "EstacionEntradaDTO{" +
                    "Medidor=" + Medidor +
                    ", IdAlmacenGas=" + IdAlmacenGas +
                    ", NombreAlmacen='" + NombreAlmacen + '\'' +
                    ", PorcentajeMedidor=" + PorcentajeMedidor +
                    ", CantidadP5000=" + CantidadP5000 +
                    ", IdTipoMedidor=" + IdTipoMedidor +
                    ", Cilindros=" + Cilindros +
                    '}';
        }
    }

    public class EstacionSalidaDTO  extends RespuestaDTO implements Serializable{
        public  EstacionSalidaDTO(){
            this.Cilindros = new ArrayList<>();
        }
        @SerializedName("Medidor")
        private MedidorDTO Medidor;

        @SerializedName("IdAlmacenGas")
        private  int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private int IdTipoMedidor;

        @SerializedName("Cilindros")
        private List<CilindrosDTO> Cilindros;

        public MedidorDTO getMedidor() {
            return Medidor;
        }

        public void setMedidor(MedidorDTO medidor) {
            Medidor = medidor;
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
            return Cilindros;
        }

        public void setCilindros(List<CilindrosDTO> cilindros) {
            Cilindros = cilindros;
        }

        @Override
        public String toString() {
            return "EstacionSalidaDTO{" +
                    "Medidor=" + Medidor +
                    ", IdAlmacenGas=" + IdAlmacenGas +
                    ", NombreAlmacen='" + NombreAlmacen + '\'' +
                    ", PorcentajeMedidor=" + PorcentajeMedidor +
                    ", CantidadP5000=" + CantidadP5000 +
                    ", IdTipoMedidor=" + IdTipoMedidor +
                    ", Cilindros=" + Cilindros +
                    '}';
        }
    }

    public class PredeterminadaDTO  extends RespuestaDTO implements Serializable{
        public PredeterminadaDTO(){
            this.Cilindros = new ArrayList<>();
        }
        @SerializedName("Medidor")
        private MedidorDTO Medidor;

        @SerializedName("IdAlmacenGas")
        private  int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private int IdTipoMedidor;

        @SerializedName("Cilindros")
        private List<CilindrosDTO> Cilindros;

        public MedidorDTO getMedidor() {
            return Medidor;
        }

        public void setMedidor(MedidorDTO medidor) {
            Medidor = medidor;
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
            return Cilindros;
        }

        public void setCilindros(List<CilindrosDTO> cilindros) {
            Cilindros = cilindros;
        }

        @Override
        public String toString() {
            return "PredeterminadaDTO{" +
                    "Medidor=" + Medidor +
                    ", IdAlmacenGas=" + IdAlmacenGas +
                    ", NombreAlmacen='" + NombreAlmacen + '\'' +
                    ", PorcentajeMedidor=" + PorcentajeMedidor +
                    ", CantidadP5000=" + CantidadP5000 +
                    ", IdTipoMedidor=" + IdTipoMedidor +
                    ", Cilindros=" + Cilindros +
                    '}';
        }
    }
}
