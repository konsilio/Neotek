package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface EnviarDatosPresenter {
    void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                          LecturaCamionetaDTO lecturaCamionetaDTO);
    void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                        LecturaCamionetaDTO lecturaCamionetaDTO);

    void onSuccessServicio();

    void onSuccessAndroid();

    void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql);
}
