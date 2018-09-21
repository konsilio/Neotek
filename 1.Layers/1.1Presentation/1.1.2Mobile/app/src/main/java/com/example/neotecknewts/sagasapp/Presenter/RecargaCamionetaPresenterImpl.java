package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RecargaCamionetaView;
import com.example.neotecknewts.sagasapp.Interactor.RecargaCamionetaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public class RecargaCamionetaPresenterImpl implements RecargaCamionetaPresenter {
    private RecargaCamionetaView recargaCamionetaView;
    private RecargaCamionetaInteractorImpl interactor;

    public RecargaCamionetaPresenterImpl(RecargaCamionetaView recargaCamionetaActivity) {
        this.recargaCamionetaView = recargaCamionetaActivity;
        this.interactor = new RecargaCamionetaInteractorImpl(this);
    }


    @Override
    public void getCamionetas(String token) {
        recargaCamionetaView.showProgres();
        interactor.getCamionetas(token);
    }

    @Override
    public void onSuccessCamionetas(DatosTomaLecturaDto data) {
        recargaCamionetaView.hideProgress();
        recargaCamionetaView.onSuccessCamionetas(data);
    }

    @Override
    public void onError(String mensaje) {
        recargaCamionetaView.hideProgress();
        recargaCamionetaView.onError(mensaje);
    }

}
