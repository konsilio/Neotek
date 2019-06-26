package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.RecargaPipaView;
import com.neotecknewts.sagasapp.Interactor.RecargaPipaInteractor;
import com.neotecknewts.sagasapp.Interactor.RecargaPipaInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

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
