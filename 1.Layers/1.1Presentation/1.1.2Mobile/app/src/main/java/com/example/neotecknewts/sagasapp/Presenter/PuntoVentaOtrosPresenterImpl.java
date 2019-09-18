package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.PuntoVentaOtrosView;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaOtrosInteractor;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaOtrosInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;
import com.example.neotecknewts.sagasapp.R;

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
