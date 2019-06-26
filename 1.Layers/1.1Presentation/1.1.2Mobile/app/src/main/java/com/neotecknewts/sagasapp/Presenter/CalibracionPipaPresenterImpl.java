package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.CalibracionPipaView;
import com.neotecknewts.sagasapp.Interactor.CalibracionPipaInteractor;
import com.neotecknewts.sagasapp.Interactor.CalibracionPipaInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

public class CalibracionPipaPresenterImpl implements CalibracionPipaPresenter {
    CalibracionPipaView View;
    CalibracionPipaInteractor interactor;
    public CalibracionPipaPresenterImpl(CalibracionPipaView view) {
        this.View = view;
        interactor = new CalibracionPipaInteractorImpl(this);
    }

    @Override
    public void getList(String token, boolean esCalibracionPipaFinal) {
        View.onShowProgress(R.string.message_cargando);
        interactor.getList(token,esCalibracionPipaFinal);
    }


    @Override
    public void onSuccess(DatosCalibracionDTO data) {
        View.onHiddenProgress();
        View.onSuccessList(data);
    }

    @Override
    public void onError(String mensajesError) {
        View.onHiddenProgress();
        View.onError(mensajesError);
    }
}
