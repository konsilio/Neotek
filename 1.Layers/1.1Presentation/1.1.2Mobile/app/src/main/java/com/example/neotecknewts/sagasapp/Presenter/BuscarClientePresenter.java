package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosClientesDTO;

public interface BuscarClientePresenter {
    void getClientes(String criterio,String token);

    void onError(String mensaje);

    void onSuccess(DatosClientesDTO clienteDTOS);
}
