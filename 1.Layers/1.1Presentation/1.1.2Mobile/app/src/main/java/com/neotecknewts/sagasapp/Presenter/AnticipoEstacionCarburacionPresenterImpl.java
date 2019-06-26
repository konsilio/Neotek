package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.AnticipoEstacionCarburacionView;
import com.neotecknewts.sagasapp.Interactor.AnticipoEstacionCarburacionInteractor;
import com.neotecknewts.sagasapp.Interactor.AnticipoEstacionCarburacionInteractorImpl;
import com.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaVerificarLecturasDTO;

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

    @Override
    public void checkLecturas(String token) {
        //view.onShowProgress(R.string.message_cargando);
        interactor.checkLecturas(token);
    }

    @Override
    public void onSuccessVerificarLecturas(RespuestaVerificarLecturasDTO data) {
        //view.onHiddeProgress();
        view.onSuccessRespuestaLecturas(data);
    }

    @Override
    public void onErrorVerificarLecturas(String mensaje) {
        //view.onHiddeProgress();
        view.onErrorVerificarLecturas(mensaje);
    }
}
