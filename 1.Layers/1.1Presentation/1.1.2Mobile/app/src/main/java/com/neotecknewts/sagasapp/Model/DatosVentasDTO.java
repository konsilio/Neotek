package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosVentasDTO extends RespuestaDTO implements Serializable {

    @SerializedName("ListaVentaDTO")
    private List<VentaDTO> list;

    public  DatosVentasDTO(){
        this.list = new ArrayList<>();
    }

    public List<VentaDTO> getList() {
        return list;
    }

    public void setList(List<VentaDTO> list) {
        this.list = list;
    }
}
