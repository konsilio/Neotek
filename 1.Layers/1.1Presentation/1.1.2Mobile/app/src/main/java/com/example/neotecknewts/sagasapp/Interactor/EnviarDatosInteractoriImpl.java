package com.example.neotecknewts.sagasapp.Interactor;

import android.annotation.SuppressLint;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.EnviarDatosPresenterImpl;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

import java.text.SimpleDateFormat;
import java.util.Date;

public class EnviarDatosInteractoriImpl implements EnviarDatosInteractor {
    public EnviarDatosPresenter enviarDatosPresenter;

    public EnviarDatosInteractoriImpl(EnviarDatosPresenterImpl enviarDatosPresenter) {
        this.enviarDatosPresenter =  enviarDatosPresenter;
    }

    @Override
    public void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LIC"+s.format(new Date());
        lecturaCamionetaDTO.setClaveOperacion(clave_unica);
       /* sagasSql.InsertLecturaInicialCamioneta(lecturaCamionetaDTO);
        sagasSql.InsertCilindrosLecturaInicialCamioneta(lecturaCamionetaDTO);
        enviarDatosPresenter.onSuccessServicio();*/
    }

    @Override
    public void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO) {
        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                new SimpleDateFormat("ddMMyyyyhhmmssS");
        String clave_unica = "LFC"+s.format(new Date());
        lecturaCamionetaDTO.setClaveOperacion(clave_unica);
        /*sagasSql.InsertLecturaFinalCamioneta(lecturaCamionetaDTO);
        sagasSql.InsertCilindrosLecturaFinalCamioneta(lecturaCamionetaDTO);
        enviarDatosPresenter.onSuccessAndroid();*/
    }
}
