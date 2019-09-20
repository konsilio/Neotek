package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosRecargaDto extends RespuestaDTO implements Serializable {


    @SerializedName("AlmacenesAlternos")
    private List<AlmacenesDTO> almacenesDTO;

    @SerializedName("Camionetas")
    private List<CamionetasDTO> camionetasDTOS;

    @SerializedName("Pipas")
    private List<PipasDTO> pipasDTOS;

    @SerializedName("Estaciones")
    private List<EstacionesDTO> estacionesDTOS;

    @SerializedName("Medidores")
    private List<MedidorDTO> medidorDTOS;

    public DatosRecargaDto() {
        this.almacenesDTO = new ArrayList<>();
        this.camionetasDTOS = new ArrayList<>();
        this.pipasDTOS = new ArrayList<>();
        this.estacionesDTOS = new ArrayList<>();
        this.medidorDTOS = new ArrayList<>();
    }

    public List<AlmacenesDTO> getAlmaceneDTO() {
        return almacenesDTO;
    }

    public void setAlmacenesDTO(List<AlmacenesDTO> almacenesDTO) {
        this.almacenesDTO = almacenesDTO;
    }

    public List<CamionetasDTO> getCamionetasDTOS() {
        return camionetasDTOS;
    }

    public void setCamionetasDTOS(List<CamionetasDTO> camionetasDTOS) {
        this.camionetasDTOS = camionetasDTOS;
    }

    public List<PipasDTO> getPipasDTOS() {
        return pipasDTOS;
    }

    public void setPipasDTOS(List<PipasDTO> pipasDTOS) {
        this.pipasDTOS = pipasDTOS;
    }

    public List<EstacionesDTO> getEstacionesDTOS() {
        return estacionesDTOS;
    }

    public void setEstacionesDTOS(List<EstacionesDTO> estacionesDTOS) {
        this.estacionesDTOS = estacionesDTOS;
    }

    public List<MedidorDTO> getMedidorDTOS() {
        return medidorDTOS;
    }

    public void setMedidorDTOS(List<MedidorDTO> medidorDTOS) {
        this.medidorDTOS = medidorDTOS;
    }

    public class AlmacenesDTO extends RespuestaDTO implements Serializable {
        @SerializedName("Medidor")
        private MedidorDTO Medidor;

        @SerializedName("IdCAlmacen")
        private int IdAlmacenGas;

        @SerializedName("NombreAlmacen")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private int IdTipoMedidor;

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

        @Override
        public String toString() {
            return "AlmacenesDTO{" +
                    "Medidor=" + Medidor +
                    ", IdAlmacenGas=" + IdAlmacenGas +
                    ", NombreAlmacen='" + NombreAlmacen + '\'' +
                    ", PorcentajeMedidor=" + PorcentajeMedidor +
                    ", CantidadP5000=" + CantidadP5000 +
                    ", IdTipoMedidor=" + IdTipoMedidor +
                    '}';
        }
    }

    public class CamionetasDTO extends RespuestaDTO implements Serializable{
        @SerializedName("Medidor")
        private MedidorDTO Medidor;

        @SerializedName("IdCAlmacen")
        private int IdAlmacenGas;

        @SerializedName("Numero")
        private String NombreAlmacen;

        @SerializedName("PorcentajeMedidor")
        private double PorcentajeMedidor;

        @SerializedName("CantidadP5000")
        private int CantidadP5000;

        @SerializedName("IdTipoMedidor")
        private int IdTipoMedidor;

        @SerializedName("Cilindros")
        private List<CilindrosDTO> Cilindros;

        public CamionetasDTO(){
            this.Cilindros = new ArrayList<>();
        }

        public List<CilindrosDTO> getCilindros() {
            return Cilindros;
        }

        public void setCilindros(List<CilindrosDTO> cilindros) {
            Cilindros = cilindros;
        }

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
    }

    public class PipasDTO extends RespuestaDTO implements Serializable{
        @SerializedName("Medidor")
        private MedidorDTO Medidor;

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

        @Override
        public String toString() {
            return "PipasDTO{" +
                    "Medidor=" + Medidor +
                    ", IdAlmacenGas=" + IdAlmacenGas +
                    ", NombreAlmacen='" + NombreAlmacen + '\'' +
                    ", PorcentajeMedidor=" + PorcentajeMedidor +
                    ", CantidadP5000=" + CantidadP5000 +
                    ", IdTipoMedidor=" + IdTipoMedidor +
                    '}';
        }
    }

    public class EstacionesDTO extends RespuestaDTO implements Serializable{
        @SerializedName("Medidor")
        private MedidorDTO Medidor;

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

        @Override
        public String toString() {
            return "EstacionesDTO{" +
                    "Medidor=" + Medidor +
                    ", IdAlmacenGas=" + IdAlmacenGas +
                    ", NombreAlmacen='" + NombreAlmacen + '\'' +
                    ", PorcentajeMedidor=" + PorcentajeMedidor +
                    ", CantidadP5000=" + CantidadP5000 +
                    ", IdTipoMedidor=" + IdTipoMedidor +
                    '}';
        }
    }
}
