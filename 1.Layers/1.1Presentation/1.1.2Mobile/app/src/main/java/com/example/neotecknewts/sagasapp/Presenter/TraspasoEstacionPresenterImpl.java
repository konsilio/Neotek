package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.TraspasoEstacionView;
import com.example.neotecknewts.sagasapp.Interactor.TraspasoEstacionInteractor;
import com.example.neotecknewts.sagasapp.Interactor.TraspasoEstacionInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.R;

public class TraspasoEstacionPresenterImpl implements TraspasoEstacionPresenter {
    TraspasoEstacionView view;
    TraspasoEstacionInteractor interactor;
    public TraspasoEstacionPresenterImpl(TraspasoEstacionView view) {
        this.view = view;
        interactor = new TraspasoEstacionInteractorImpl(this);
    }

    @Override
    public void GetList(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.GetList(token);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddeProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccess(DatosTraspasoDTO datosTraspasoDTO) {
        view.onHiddeProgress();
        view.onSuccessList(datosTraspasoDTO);
    }
}
