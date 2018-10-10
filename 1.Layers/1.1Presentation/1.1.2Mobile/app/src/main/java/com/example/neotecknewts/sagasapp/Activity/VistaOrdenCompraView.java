package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

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
