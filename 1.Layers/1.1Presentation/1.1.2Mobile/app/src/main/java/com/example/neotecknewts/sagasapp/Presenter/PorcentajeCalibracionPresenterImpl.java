package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.PorcentajeCalibracionView;
import com.example.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionInteractor;
import com.example.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;
import com.example.neotecknewts.sagasapp.R;

public class PorcentajeCalibracionPresenterImpl implements PorcentajeCalibracionPresenter {
    PorcentajeCalibracionView view;
    PorcentajeCalibracionInteractor interactor;
    public PorcentajeCalibracionPresenterImpl(PorcentajeCalibracionView view) {
        this.view = view;
        this.interactor = new PorcentajeCalibracionInteractorImpl(this);
    }

    @Override
    public void getPorcentaje(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getPorcentaje(token);
    }

    @Override
    public void onError(String s) {
        view.onHiddeProgress();
        view.onErrorPorcentaje(s);
    }

    @Override
    public void onSuccessPorcentaje(DatosEmpresaConfiguracionDTO data) {
        view.onHiddeProgress();
        view.onSuccessPorcentaje(data);
    }
}
