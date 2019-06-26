package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by neotecknewts on 10/08/18.
 */

public class RespuestaOrdenesCompraDTO extends RespuestaDTO {

    @SerializedName("OrdenesCompra")
    private List<OrdenCompraDTO> OrdenesCompra;

    public List<OrdenCompraDTO> getOrdenesCompra() {
        return OrdenesCompra;
    }

    public void setOrdenesCompra(List<OrdenCompraDTO> ordenesCompra) {
        OrdenesCompra = ordenesCompra;
    }
}
