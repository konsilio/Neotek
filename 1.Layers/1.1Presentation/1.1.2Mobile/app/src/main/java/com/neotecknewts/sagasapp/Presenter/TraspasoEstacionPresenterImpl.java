package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.TraspasoEstacionView;
import com.neotecknewts.sagasapp.Interactor.TraspasoEstacionInteractor;
import com.neotecknewts.sagasapp.Interactor.TraspasoEstacionInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

public class TraspasoEstacionPresenterImpl implements TraspasoEstacionPresenter {
    TraspasoEstacionView view;
    TraspasoEstacionInteractor interactor;
    public TraspasoEstacionPresenterImpl(TraspasoEstacionView view) {
        this.view = view;
        interactor = new TraspasoEstacionInteractorImpl(this);
    }

    @Override
    public void GetList(String token,boolean esFinal) {
        view.onShowProgress(R.string.message_cargando);
        interactor.GetList(token,esFinal);
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
