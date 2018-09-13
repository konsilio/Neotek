package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.LecturaDatosView;
import com.example.neotecknewts.sagasapp.Interactor.LecturaDatosInteractor;
import com.example.neotecknewts.sagasapp.Interactor.LecturaDatosInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.R;

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
