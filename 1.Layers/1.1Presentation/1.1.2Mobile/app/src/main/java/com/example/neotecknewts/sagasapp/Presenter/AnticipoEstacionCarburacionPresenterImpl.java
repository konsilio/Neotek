package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.AnticipoEstacionCarburacionView;
import com.example.neotecknewts.sagasapp.Interactor.AnticipoEstacionCarburacionInteractor;
import com.example.neotecknewts.sagasapp.Interactor.AnticipoEstacionCarburacionInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.R;

public class AnticipoEstacionCarburacionPresenterImpl implements AnticipoEstacionCarburacionPresenter {
    AnticipoEstacionCarburacionView view;
    AnticipoEstacionCarburacionInteractor interactor;
    public AnticipoEstacionCarburacionPresenterImpl(AnticipoEstacionCarburacionView view) {
        this.view = view;
        this.interactor = new AnticipoEstacionCarburacionInteractorImpl(this);
    }

    @Override
    public void getEstaciones(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getEstaciones(token);
    }

    @Override
    public void onError(String error) {
        view.onHiddeProgress();
        view.onError(error);
    }

    @Override
    public void onError(RespuestaEstacionesVentaDTO data) {
        view.onHiddeProgress();
        view.onError(data);
    }

    @Override
    public void onSuccess(RespuestaEstacionesVentaDTO data) {
        view.onHiddeProgress();
        view.onSuccess(data);
    }
}
