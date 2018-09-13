package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.LecturaPipaView;
import com.example.neotecknewts.sagasapp.Interactor.LecturaPipaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.LecturaPipaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.PipaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class LecturaPipaPresenterImpl implements LecturaPipaPresenter {

    LecturaPipaView lecturaPipaView;
    LecturaPipaInteractor lecturaPipaInteractor;

    public LecturaPipaPresenterImpl(LecturaPipaView lecturaPipaView) {
        this.lecturaPipaView = lecturaPipaView;
        lecturaPipaInteractor = new LecturaPipaInteractorImpl(this);
    }

    @Override
    public void getMedidores(String token) {
        lecturaPipaView.showLoadingProgress(R.string.message_cargando);
        lecturaPipaInteractor.getMedidores(token);
    }

    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> data) {
        lecturaPipaView.onSuccessMedidores(data);
    }

    @Override
    public void onError() {
        lecturaPipaView.hiddeLoadingProgress();
        lecturaPipaView.onError();
    }

    @Override
    public void getPipas(String token,boolean EsFinal) {
        lecturaPipaView.showLoadingProgress(R.string.message_cargando);
        lecturaPipaInteractor.getPipas(token,EsFinal);
    }

    @Override
    public void onSuccessGetPipas(List<EstacionCarburacionDTO> data) {
        lecturaPipaView.onSuccessPipas(data);
    }
}
