package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.RegistroClienteView;
import com.neotecknewts.sagasapp.Interactor.RegistroClienteInteractor;
import com.neotecknewts.sagasapp.Interactor.RegistroClienteInteractorImpl;
import com.neotecknewts.sagasapp.Model.ClienteDTO;
import com.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaClienteDTO;

public class RegistroClientePresenterImpl implements RegistroClientePresenter {
    RegistroClienteView view;
    RegistroClienteInteractor interactor;
    public RegistroClientePresenterImpl(RegistroClienteView view) {
        this.view = view;
        this.interactor = new RegistroClienteInteractorImpl(this);
    }

    @Override
    public void getLista(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getLista(token);
    }

    @Override
    public void onSuccess(DatosTipoPersonaDTO data) {
        view.onHideProgress();
        view.onSuccessDatosFiscales(data);
    }

    @Override
    public void onError(DatosTipoPersonaDTO data) {
        view.onHideProgress();
        view.onError(data);
    }

    @Override
    public void onError(String message) {
        view.onHideProgress();
        view.onError(message);
    }

    @Override
    public void registrarCliente(ClienteDTO clienteDTO, String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.registrarCliente(clienteDTO,token);
    }

    @Override
    public void onSuccessRegistro(RespuestaClienteDTO data) {
        view.onHideProgress();
        view.setIdCliente(data);
    }

    @Override
    public void onErrorRegistro(RespuestaClienteDTO data) {
        view.onHideProgress();
        view.onErrorRegistro(data);
    }
}
