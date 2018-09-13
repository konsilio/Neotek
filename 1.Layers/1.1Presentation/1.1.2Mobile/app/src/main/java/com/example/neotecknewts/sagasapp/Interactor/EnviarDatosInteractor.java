package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface EnviarDatosInteractor {
    public void RegistrarLecturaInicialCamioneta(SAGASSql sagasSql, String token,
                                                 LecturaCamionetaDTO lecturaCamionetaDTO);
    public void RegistrarLecturaFinalCamioneta(SAGASSql sagasSql,String token,
                                               LecturaCamionetaDTO lecturaCamionetaDTO);

    void RegistrarRecargaCamioneta(RecargaDTO recargaDTO, String token, SAGASSql sagasSql);
}
