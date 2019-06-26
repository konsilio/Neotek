package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.neotecknewts.sagasapp.Model.MedidorDTO;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */
//interfaz que indica los metodos relacionados con la vista que el interactor usara
public interface FinalizarDescargaView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetMedidores(List<MedidorDTO> medidorDTOs);
    void onSuccessGetAlmacen(List<AlmacenDTO> almacenDTOs);

    void messageError(String mensaje);
}
