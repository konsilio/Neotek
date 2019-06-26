package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosClientesDTO;

public interface BuscarClientePresenter {
    void getClientes(String criterio, String token);

    void onError(String mensaje);

    void onSuccess(DatosClientesDTO clienteDTOS);
}
