package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RecargaEstacionCarburacionView;
import com.example.neotecknewts.sagasapp.Interactor.RecargaEstacionCarburacionInteractorImpl;
import com.example.neotecknewts.sagasapp.R;

public class RecargaEstacionCarburacionPresenterImpl implements RecargaEstacionCarburacionPresenter {
    RecargaEstacionCarburacionView view;
    RecargaEstacionCarburacionInteractorImpl interactor;
    public RecargaEstacionCarburacionPresenterImpl(RecargaEstacionCarburacionView view){
        this.view = view;
        this.interactor = new RecargaEstacionCarburacionInteractorImpl(this);
    }

    @Override
    public void getLista(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getLista(token);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddenProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccesslista(Object datosRecargaDTO) {
        view.onHiddenProgress();
        view.onSuccessLista(datosRecargaDTO);
    }
}
