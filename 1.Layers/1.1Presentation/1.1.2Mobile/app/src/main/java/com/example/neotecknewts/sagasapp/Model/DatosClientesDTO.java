package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DatosClientesDTO extends RespuestaDTO implements Serializable {
    @SerializedName("clientes")
    private List<ClienteDTO> list;

    public  DatosClientesDTO(){
        this.list = new ArrayList<>();
    }

    public List<ClienteDTO> getList() {
        return list;
    }

    public void setList(List<ClienteDTO> list) {
        this.list = list;
    }
}
