package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Activity.RecargaCamionetaView;
import com.neotecknewts.sagasapp.Interactor.RecargaCamionetaInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

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
    public void onSuccessCamionetas(DatosRecargaDto data) {
        recargaCamionetaView.hideProgress();
        recargaCamionetaView.onSuccessCamionetas(data);
    }

    @Override
    public void onError(String mensaje) {
        recargaCamionetaView.hideProgress();
        recargaCamionetaView.onError(mensaje);
    }

}
