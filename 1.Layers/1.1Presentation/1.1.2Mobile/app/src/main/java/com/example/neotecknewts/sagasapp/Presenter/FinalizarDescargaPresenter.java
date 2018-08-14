package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

/**
 * Created by neotecknewts on 13/08/18.
 */

public interface FinalizarDescargaPresenter {
    void getOrdenesCompra(int IdEmpresa, String token);
    void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onError();
}
