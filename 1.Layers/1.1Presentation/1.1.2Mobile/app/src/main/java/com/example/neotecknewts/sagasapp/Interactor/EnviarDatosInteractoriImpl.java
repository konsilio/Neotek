package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenterImpl;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public class EnviarDatosInteractoriImpl implements EnviarDatosInteractor {
    public EnviarDatosPresenter enviarDatosPresenter;

    public EnviarDatosInteractoriImpl(EnviarDatosPresenterImpl enviarDatosPresenter) {
        this.enviarDatosPresenter =  enviarDatosPresenter;
    }

    @Override
    public void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO) {

        enviarDatosPresenter.onSuccessServicio();
    }

    @Override
    public void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO) {
        enviarDatosPresenter.onSuccessAndroid();
    }
}
