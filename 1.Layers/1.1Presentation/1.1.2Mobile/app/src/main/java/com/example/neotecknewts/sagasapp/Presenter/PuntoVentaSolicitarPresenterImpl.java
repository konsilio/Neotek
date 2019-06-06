package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.PuntoVentaSolicitarView;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaSolicitarInteractor;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaSolicitarInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;
import com.example.neotecknewts.sagasapp.R;

public class PuntoVentaSolicitarPresenterImpl implements PuntoVentaSolicitarPresenter {
    PuntoVentaSolicitarView view;
    PuntoVentaSolicitarInteractor interactor;
    public PuntoVentaSolicitarPresenterImpl(PuntoVentaSolicitarView  view) {
        this.view = view;
        this.interactor = new PuntoVentaSolicitarInteractorImpl(this);
    }

    @Override
    public void hayCorte(String token) {
        //view.onShowProgress(R.string.message_cargando);
        interactor.hayCorte(token);
    }

    @Override
    public void onSuccess(RespuestaCortesAntesVentaDTO data) {
        //view.onHiddeProgress();
        view.onResultVerificaCorte(data);
    }

    @Override
    public void onError(String s) {
        view.onError(s);
    }
}
