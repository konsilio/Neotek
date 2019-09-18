package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface EnviarDatosPresenter {
    void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO);
    void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql,String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO);

    void onSuccessServicio();

    void onSuccessAndroid();

    void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql);
}
