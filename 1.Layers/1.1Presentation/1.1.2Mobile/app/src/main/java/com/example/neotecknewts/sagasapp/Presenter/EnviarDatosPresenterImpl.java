package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.EnviarDatosView;
import com.example.neotecknewts.sagasapp.Interactor.EnviarDatosInteractor;
import com.example.neotecknewts.sagasapp.Interactor.EnviarDatosInteractoriImpl;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

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
