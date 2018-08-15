package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public interface FinalizarDescargaPresenter {
    void getOrdenesCompra(int IdEmpresa, String token);
    void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void getMedidores(String token);
    void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList);
    void getAlmacenes(String token);
    void onSuccessGetAlmacenes(List<AlmacenDTO> almacenDTOs);
    void onError();
}
