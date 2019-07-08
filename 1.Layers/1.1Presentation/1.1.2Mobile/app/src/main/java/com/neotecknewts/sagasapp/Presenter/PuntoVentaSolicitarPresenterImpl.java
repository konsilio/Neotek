package com.neotecknewts.sagasapp.Presenter;

import android.content.Context;

import com.neotecknewts.sagasapp.Activity.PuntoVentaSolicitarView;
import com.neotecknewts.sagasapp.Interactor.PuntoVentaSolicitarInteractor;
import com.neotecknewts.sagasapp.Interactor.PuntoVentaSolicitarInteractorImpl;
import com.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;

public class PuntoVentaSolicitarPresenterImpl implements PuntoVentaSolicitarPresenter {
    PuntoVentaSolicitarView view;
    PuntoVentaSolicitarInteractor interactor;
    public PuntoVentaSolicitarPresenterImpl(PuntoVentaSolicitarView view, Context context) {
        this.view = view;
        this.interactor = new PuntoVentaSolicitarInteractorImpl(this, context);
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
