package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.TraspasoPipaView;
import com.example.neotecknewts.sagasapp.Interactor.TraspasoPipaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.TraspasoPipaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.R;

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
