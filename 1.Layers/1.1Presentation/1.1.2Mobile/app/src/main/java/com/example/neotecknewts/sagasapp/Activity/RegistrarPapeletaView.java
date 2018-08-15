package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */
//interfaz que indica los metodos relacionados con la vista que el interactor usara
public interface RegistrarPapeletaView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetMedidores(List<MedidorDTO> medidorDTOs);
}
