package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.LecturaCamionetaView;
import com.example.neotecknewts.sagasapp.Interactor.LecturaCamionetaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class LecturaCamionetaPresenterImpl implements LecturaCamionetaPresenter {
    private LecturaCamionetaView lecturaCamionetaView;
    private LecturaCamionetaInteractorImpl lecturaCamionetaInteractor;
    public LecturaCamionetaPresenterImpl ( LecturaCamionetaView lecturaCamionetaView) {
        this.lecturaCamionetaView = lecturaCamionetaView;
        this.lecturaCamionetaInteractor = new LecturaCamionetaInteractorImpl(this);
    }

    @Override
    public void GetListCamionetas(String token,boolean esFinalizar) {
        this.lecturaCamionetaView.onShowProgressDialog(R.string.message_cargando);
        lecturaCamionetaInteractor.GetListCamionetas(token,esFinalizar);
    }

    @Override
    public void onError() {
        lecturaCamionetaView.hideProgress();
        lecturaCamionetaView.onErrorCamionetas();
    }

    @Override
    public void onSuccessCamionetas(List<EstacionCarburacionDTO> data) {
        lecturaCamionetaView.onSuccessCamionetas(data);
        lecturaCamionetaView.hideProgress();
    }
}
