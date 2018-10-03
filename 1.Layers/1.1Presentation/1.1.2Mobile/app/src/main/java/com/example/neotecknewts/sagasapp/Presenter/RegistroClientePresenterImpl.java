package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RegistroClienteView;
import com.example.neotecknewts.sagasapp.Interactor.RegistroClienteInteractor;
import com.example.neotecknewts.sagasapp.Interactor.RegistroClienteInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.example.neotecknewts.sagasapp.R;

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
}
