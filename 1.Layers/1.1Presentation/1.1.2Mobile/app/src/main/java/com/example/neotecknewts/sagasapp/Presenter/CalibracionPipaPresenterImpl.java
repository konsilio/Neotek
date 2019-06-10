package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.CalibracionPipaView;
import com.example.neotecknewts.sagasapp.Interactor.CalibracionPipaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.CalibracionPipaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.example.neotecknewts.sagasapp.R;

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
