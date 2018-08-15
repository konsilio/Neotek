package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public interface IniciarDescargaView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetMedidores(List<MedidorDTO> medidorDTOs);
    void onSuccessGetAlmacen(List<AlmacenDTO> almacenDTOs);

}
