package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.RecargaEstacionCarburacionView;
import com.neotecknewts.sagasapp.Interactor.RecargaEstacionCarburacionInteractor;
import com.neotecknewts.sagasapp.Interactor.RecargaEstacionCarburacionInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

public class RecargaEstacionCarburacionPresenterImpl implements RecargaEstacionCarburacionPresenter {
    RecargaEstacionCarburacionView view;
    RecargaEstacionCarburacionInteractor interactor;
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
    public void onSuccesslista(DatosRecargaDto datosRecargaDTO) {
        view.onHiddenProgress();
        view.onSuccessLista(datosRecargaDTO);
    }
}
