package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Activity.EnviarDatosView;
import com.neotecknewts.sagasapp.Interactor.EnviarDatosInteractor;
import com.neotecknewts.sagasapp.Interactor.EnviarDatosInteractoriImpl;
import com.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

public class EnviarDatosPresenterImpl implements EnviarDatosPresenter {
    EnviarDatosView enviarDatosView;
    EnviarDatosInteractor enviarDatosInteractor;
    public EnviarDatosPresenterImpl(EnviarDatosView enviarDatosView){
        this.enviarDatosView = enviarDatosView;
        this.enviarDatosInteractor= new EnviarDatosInteractoriImpl(this);
    }
    @Override
    public void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO) {
        enviarDatosView.showProgressDialog();
        enviarDatosInteractor.RegistrarLecturaInicialCamioneta(sagasSql,token,lecturaCamionetaDTO);
    }

    @Override
    public void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO) {
        enviarDatosView.showProgressDialog();
        enviarDatosInteractor.RegistrarLecturaFinalCamioneta(sagasSql,token,lecturaCamionetaDTO);
    }

    @Override
    public void onSuccessServicio() {
        enviarDatosView.hiddenProgressDialog();
        enviarDatosView.onSuccessEnvio();
    }

    @Override
    public void onSuccessAndroid() {
        enviarDatosView.hiddenProgressDialog();
        enviarDatosView.onSuccessAndroid();
    }

    @Override
    public void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql) {
        enviarDatosView.showProgressDialog();
        enviarDatosInteractor.RegistrarRecargaCamioneta(recargaDTO, token, sagasSql);
    }
}
