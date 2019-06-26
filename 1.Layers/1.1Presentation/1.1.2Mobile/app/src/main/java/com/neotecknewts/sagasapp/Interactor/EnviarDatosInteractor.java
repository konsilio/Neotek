package com.neotecknewts.sagasapp.Interactor;

import com.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface EnviarDatosInteractor {
    void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                          LecturaCamionetaDTO lecturaCamionetaDTO);
    void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql, String token,
                                        LecturaCamionetaDTO lecturaCamionetaDTO);

    void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql);
}
