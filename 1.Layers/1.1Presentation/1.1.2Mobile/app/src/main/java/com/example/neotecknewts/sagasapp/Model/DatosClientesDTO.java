package com.example.neotecknewts.sagasapp.Model;

import java.util.ArrayList;
import java.util.List;

public class DatosClientesDTO extends RespuestaDTO{

    private List<DatosClientesDTO> list;

    public  DatosClientesDTO(){
        this.list = new ArrayList<>();
    }

    public List<DatosClientesDTO> getList() {
        return list;
    }

    public void setList(List<DatosClientesDTO> list) {
        this.list = list;
    }
}
