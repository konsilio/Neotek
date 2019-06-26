package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.PorcentajeCalibracionView;
import com.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionInteractor;
import com.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;

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
