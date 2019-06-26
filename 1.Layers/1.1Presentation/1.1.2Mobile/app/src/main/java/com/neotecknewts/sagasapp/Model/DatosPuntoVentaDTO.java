package com.neotecknewts.sagasapp.Model;

import java.util.List;

public class DatosPuntoVentaDTO extends RespuestaDTO {
    private List<ExistenciasDTO> list;

    public List<ExistenciasDTO> getList() {
        return list;
    }

    public void setList(List<ExistenciasDTO> list) {
        this.list = list;
    }
}
