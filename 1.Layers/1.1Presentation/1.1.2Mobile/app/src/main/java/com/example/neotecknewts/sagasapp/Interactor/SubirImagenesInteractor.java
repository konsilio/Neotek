package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.SQLite.FinalizarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesInteractor {
    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO, String token, IniciarDescargaSQL iniciarDescargaSQL);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO, String token, FinalizarDescargaSQL finalizarDescargaSQL);

    void registrarLecturaInicial(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO);

    void registrarLecturaFinal(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO);

    void registrarLecturaInicialPipa(SAGASSql sagasSql, String token, LecturaPipaDTO lecturaPipaDTO);

    void registrarLecturaFinalizalPipa(SAGASSql sagasSql, String token, LecturaPipaDTO lecturaPipaDTO);
}
