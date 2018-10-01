package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Presenter.BuscarClientePresenter;

import java.util.List;

public class BuscarClienteInteractorImpl implements BuscarClienteInteractor {
    BuscarClientePresenter presenter;
    public BuscarClienteInteractorImpl(BuscarClientePresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getClientes(String criterio, String token) {
        List<ClienteDTO> clienteDTOS = null;
        String mensaje = "";
        presenter.onSuccess(clienteDTOS);
        presenter.onError(mensaje);
    }
}
