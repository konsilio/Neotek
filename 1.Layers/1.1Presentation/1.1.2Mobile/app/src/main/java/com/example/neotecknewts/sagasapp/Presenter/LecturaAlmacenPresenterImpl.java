package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.LecturaAlmacenView;
import com.example.neotecknewts.sagasapp.Interactor.LecturaAlmacenInteractor;
import com.example.neotecknewts.sagasapp.Interactor.LecturaAlmacenInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class LecturaAlmacenPresenterImpl implements LecturaAlmacenPresenter {
    private LecturaAlmacenView lecturaAlmacenView;
    private LecturaAlmacenInteractor lecturaAlmacenInteractor;
    public LecturaAlmacenPresenterImpl(LecturaAlmacenView lecturaAlmacenView) {
        this.lecturaAlmacenView = lecturaAlmacenView;
        lecturaAlmacenInteractor = new LecturaAlmacenInteractorImpl(this);
    }

    @Override
    public void getMedidores(String token){
        lecturaAlmacenView.showProgress(R.string.message_cargando);
        lecturaAlmacenInteractor.getMedidores(token);
    }

    @Override
    public void hiddeProgress() {
        lecturaAlmacenView.hiddeProgress();
    }

    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> data) {
        lecturaAlmacenView.hiddeProgress();
        lecturaAlmacenView.onSuccessMedidores(data);
    }

    @Override
    public void onError() {
        lecturaAlmacenView.hiddeProgress();
        lecturaAlmacenView.onError();
    }
}
