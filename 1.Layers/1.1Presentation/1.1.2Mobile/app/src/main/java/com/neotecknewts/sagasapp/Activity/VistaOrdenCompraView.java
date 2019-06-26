package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

/**
 * Created by neotecknewts on 10/08/18.
 */
//interfaz que indica los metodos relacionados con la vista que el interactor usara
public interface VistaOrdenCompraView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);

    void messageError(String mensaje);
}
