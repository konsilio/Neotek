package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.TraspasoPipaView;
import com.neotecknewts.sagasapp.Interactor.TraspasoPipaInteractor;
import com.neotecknewts.sagasapp.Interactor.TraspasoPipaInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

public class TraspasoPipaPresenterImpl implements TraspasoPipaPresenter {
    TraspasoPipaView view;
    TraspasoPipaInteractor interactor;
    public TraspasoPipaPresenterImpl(TraspasoPipaView view) {
        this.view = view;
        this.interactor = new TraspasoPipaInteractorImpl(this);
    }

    @Override
    public void GetList(String token) {
        this.view.onShowProgress(R.string.message_cargando);
        interactor.GetList(token);
    }

    @Override
    public void onSuccess(DatosTraspasoDTO data) {
        view.onHiddeProgress();
        view.onSuccessLista(data);
    }

    @Override
    public void onError(String message) {
        view.onHiddeProgress();
        view.onError(message);
    }
}
