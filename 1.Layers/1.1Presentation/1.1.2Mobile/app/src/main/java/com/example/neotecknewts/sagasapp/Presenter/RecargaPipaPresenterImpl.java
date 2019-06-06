package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RecargaPipaView;
import com.example.neotecknewts.sagasapp.Interactor.RecargaPipaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.RecargaPipaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.R;

public class RecargaPipaPresenterImpl implements RecargaPipaPresenter {
    RecargaPipaView view;
    RecargaPipaInteractor interactor;
    public RecargaPipaPresenterImpl(RecargaPipaView view){
        this.view = view;
        this.interactor = new RecargaPipaInteractorImpl(this);
    }
    @Override
    public void getLists(String token, boolean EsRecargaPipaFinal) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getList(token,EsRecargaPipaFinal);
    }

    @Override
    public void onSuccessList(DatosRecargaDto data) {
        view.onHiddenProgress();
        view.onSuccessLista(data);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddenProgress();
        view.onError(mensaje);
    }
}
