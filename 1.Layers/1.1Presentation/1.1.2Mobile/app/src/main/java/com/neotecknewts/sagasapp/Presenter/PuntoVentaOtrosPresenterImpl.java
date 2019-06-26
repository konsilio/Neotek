package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.PuntoVentaOtrosView;
import com.neotecknewts.sagasapp.Interactor.PuntoVentaOtrosInteractor;
import com.neotecknewts.sagasapp.Interactor.PuntoVentaOtrosInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;

public class PuntoVentaOtrosPresenterImpl implements PuntoVentaOtrosPresenter {
    PuntoVentaOtrosView view;
    PuntoVentaOtrosInteractor interactor;
    public PuntoVentaOtrosPresenterImpl(PuntoVentaOtrosView view) {
        this.view = view;
        this.interactor = new PuntoVentaOtrosInteractorImpl(this);
    }

    @Override
    public void getList(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getList(token);
    }

    @Override
    public void onSuccess(DatosVentaOtrosDTO data) {
        view.onHiddenProgress();
        view.onSuccess(data);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddenProgress();
        view.onError(mensaje);
    }
}
