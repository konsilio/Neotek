package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosTomaLecturaDto extends RespuestaDTO  implements Serializable {

    @SerializedName("Almacenes")
    private List<AlmacenDTO> Almacenes;

    @SerializedName("Medidores")
    private List<MedidorDTO> Medidores;

    public DatosTomaLecturaDto() {
        Almacenes = new ArrayList<>();
        Medidores = new ArrayList<>();
    }

    public List<AlmacenDTO> getAlmacenes() {
        return Almacenes;
    }

    public void setAlmacenes(List<AlmacenDTO> almacenes) {
        Almacenes = almacenes;
    }

    public List<MedidorDTO> getMedidores() {
        return Medidores;
    }

    public void setMedidores(List<MedidorDTO> medidores) {
        Medidores = medidores;
    }
}
