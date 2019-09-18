package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

public class PipaDTO implements Serializable{
    @SerializedName("NombrePipa")
    private String NombrePipa;

    @SerializedName("IdPipa")
    private int IdPipa;
    //region Anticipos y cortes

    @SerializedName("IdCAlmacenGas")
    private int IdCAlmacengas;

    @SerializedName("IdAlmacenGas")
    private int IdAlmacenGas;

    @SerializedName("NombreAlmacen")
    private String NombreAlmacen;

    @SerializedName("CantidadP5000")
    private int CantidadP5000;

    @SerializedName("P5000Final")
    private int P5000Final;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("Medidor")
    private MedidorDTO medidor;

    @SerializedName("AnticiposEstacion")
    private AnticiposEstacionDTO anticiposEstacionDTO;
    //endregion

    public String getNombrePipa() {
        return NombrePipa;
    }

    public void setNombrePipa(String nombrePipa) {
        NombrePipa = nombrePipa;
    }

    public int getIdPipa() {
        return IdPipa;
    }

    public void setIdPipa(int idPipa) {
        IdPipa = idPipa;
    }

    public int getIdCAlmacengas() {
        return IdCAlmacengas;
    }

    public void setIdCAlmacengas(int idCAlmacengas) {
        IdCAlmacengas = idCAlmacengas;
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

    public int getCantidadP5000() {
        return CantidadP5000;
    }

    public void setCantidadP5000(int cantidadP5000) {
        CantidadP5000 = cantidadP5000;
    }

    public int getP5000Final() {
        return P5000Final;
    }

    public void setP5000Final(int p5000Final) {
        P5000Final = p5000Final;
    }

    public MedidorDTO getMedidor() {
        return medidor;
    }

    public void setMedidor(MedidorDTO medidor) {
        this.medidor = medidor;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public AnticiposEstacionDTO getAnticiposEstacionDTO() {
        return anticiposEstacionDTO;
    }

    public void setAnticiposEstacionDTO(AnticiposEstacionDTO anticiposEstacionDTO) {
        this.anticiposEstacionDTO = anticiposEstacionDTO;
    }
}
