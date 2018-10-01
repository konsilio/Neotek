package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.BuscarClienteView;
import com.example.neotecknewts.sagasapp.Interactor.BuscarClienteInteractor;
import com.example.neotecknewts.sagasapp.Interactor.BuscarClienteInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class BuscarClientePresenterImpl implements BuscarClientePresenter {
    BuscarClienteView view;
    BuscarClienteInteractor interactor;
    public BuscarClientePresenterImpl(BuscarClienteView view) {
        this.view = view;
        this.interactor = new BuscarClienteInteractorImpl(this);
    }

    @Override
    public void getClientes(String criterio,String token) {
        view.onShowPorgress(R.string.message_cargando);
        interactor.getClientes(criterio,token);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddeProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccess(List<ClienteDTO> clienteDTOS) {
        view.onHiddeProgress();
        view.onSuccessList(clienteDTOS);
    }
}
