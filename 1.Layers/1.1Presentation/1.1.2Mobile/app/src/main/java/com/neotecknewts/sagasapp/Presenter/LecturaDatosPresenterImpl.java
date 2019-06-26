package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.LecturaDatosView;
import com.neotecknewts.sagasapp.Interactor.LecturaDatosInteractor;
import com.neotecknewts.sagasapp.Interactor.LecturaDatosInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public class LecturaDatosPresenterImpl implements LecturaDatosPresenter {
    private LecturaDatosView lecturaDatosView;
    private LecturaDatosInteractor lecturaDatosInteractor;
    public LecturaDatosPresenterImpl(LecturaDatosView lecturaDatosView){
        this.lecturaDatosView = lecturaDatosView;
        lecturaDatosInteractor = new LecturaDatosInteractorImpl(this);
    }
    @Override
    public void getMedidores(String token) {
        lecturaDatosView.showLoadingProgress(R.string.message_cargando);
        lecturaDatosInteractor.getMedidores(token);
    }

    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> data) {
        lecturaDatosView.hiddeLoadingProgress();
        lecturaDatosView.onSuccessMedidores(data);
    }

    @Override
    public void onError() {
        lecturaDatosView.hiddeLoadingProgress();
        lecturaDatosView.ErrorMedidores();
    }

    @Override
    public void getEstacionesCarburacion(String token,boolean esFinalizar) {
        lecturaDatosView.showLoadingProgress(R.string.message_cargando);
        lecturaDatosInteractor.getEstacionesCarburacion(token,esFinalizar);
    }

    @Override
    public void onSuccessGetEstacionesCarburacion(DatosTomaLecturaDto data) {
        lecturaDatosView.onSuccessEstacionesCarburacion(data);
        lecturaDatosView.onSuccessMedidores(data.getMedidores());
        lecturaDatosView.hiddeLoadingProgress();
    }
}
