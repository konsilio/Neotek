package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.LecturaCamionetaView;
import com.example.neotecknewts.sagasapp.Interactor.LecturaCamionetaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.LecturaCamionetaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.R;

public class LecturaCamionetaPresenterImpl implements LecturaCamionetaPresenter {
    private LecturaCamionetaView lecturaCamionetaView;
    private LecturaCamionetaInteractor lecturaCamionetaInteractor;
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
    public void onSuccessCamionetas(DatosTomaLecturaDto data) {
        lecturaCamionetaView.hideProgress();
        lecturaCamionetaView.onSuccessCamionetas(data);
    }
}
