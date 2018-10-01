package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;

import java.util.List;

public interface BuscarClientePresenter {
    void getClientes(String criterio,String token);

    void onError(String mensaje);

    void onSuccess(List<ClienteDTO> clienteDTOS);
}
