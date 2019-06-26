package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.LecturaAlmacenView;
import com.neotecknewts.sagasapp.Interactor.LecturaAlmacenInteractor;
import com.neotecknewts.sagasapp.Interactor.LecturaAlmacenInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Model.MedidorDTO;

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

    @Override
    public void getAlmacenes(String token,boolean esFinalizar) {
        lecturaAlmacenView.showProgress(R.string.message_cargando);
        lecturaAlmacenInteractor.getAlmacenes(token,esFinalizar);
    }

    @Override
    public void onSuccessGetAlmacen(DatosTomaLecturaDto data) {
        lecturaAlmacenView.onSuccessAlmacenes(data);
        lecturaAlmacenView.onSuccessMedidores(data.getMedidores());
        lecturaAlmacenView.hiddeProgress();
    }
}

