package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.CalibracionEstacionView;
import com.example.neotecknewts.sagasapp.Interactor.CalibracionEstacionInteractor;
import com.example.neotecknewts.sagasapp.Interactor.CalibracionEstacionInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.example.neotecknewts.sagasapp.R;

public class CalibracionEstacionPresenterImpl implements CalibracionEstacionPresenter {
    CalibracionEstacionView view;
    CalibracionEstacionInteractor interactor;
    public CalibracionEstacionPresenterImpl(CalibracionEstacionView view) {
        this.view = view;
        this.interactor = new CalibracionEstacionInteractorImpl(this);
    }

    @Override
    public void getList(String token, boolean esCalibracionEstacionFinal) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getList(token,esCalibracionEstacionFinal);
    }

    @Override
    public void onSuccess(DatosCalibracionDTO data) {
        view.onHiddeProgress();
        view.onSuccessList(data);
    }

    @Override
    public void onError(String message) {
        view.onHiddeProgress();
        view.onError(message);
    }
}
