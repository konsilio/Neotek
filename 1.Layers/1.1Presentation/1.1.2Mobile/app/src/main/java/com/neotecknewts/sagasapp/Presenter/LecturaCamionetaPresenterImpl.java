package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.LecturaCamionetaView;
import com.neotecknewts.sagasapp.Interactor.LecturaCamionetaInteractor;
import com.neotecknewts.sagasapp.Interactor.LecturaCamionetaInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

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
