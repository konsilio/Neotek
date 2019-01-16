package com.example.neotecknewts.sagasapp.Presenter;

import android.content.Context;

import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesPresenter {

    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token,  SAGASSql sagasSql, Context applicationContext);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO, String token, SAGASSql sagasSql);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO, String token, SAGASSql finalizarDescargaSQL);
    void onSuccessRegistrarPapeleta();
    void onSuccessRegistrarIniciarDescarga();
    void onSuccessRegistrarFinalizarDescarga();
    void onError();
    void onSuccessRegistroPapeleta();
    void onSuccessRegistroAndroid();

    void errorSolicitud(String mensaje);

    void onRegistrarIniciarDescarga();

    void registrarLecturaInicial(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO);

    void registrarLecturaFinal(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO);

    void registrarLecturaInicialPipa(SAGASSql sagasSql, String token, LecturaPipaDTO lecturaPipaDTO);

    void registrarLecturaFinalalPipa(SAGASSql sagasSql, String token, LecturaPipaDTO lecturaPipaDTO);

    void registrarLecturaInicialAlmacen(SAGASSql sagasSql, String token, LecturaAlmacenDTO lecturaAlmacenDTO);

    void registrarLecturaFinalAlmacen(SAGASSql sagasSql, String token, LecturaAlmacenDTO lecturaAlmacenDTO);

    void registrarRecargaEstacion(SAGASSql sagasSql, String token, RecargaDTO recargaDTO,boolean EsRecargaEstacionInicial);

    void onSuccessRegistroRecarga();

    void registrarRecargaPipa(SAGASSql sagasSql, String token, RecargaDTO recargaDTO, boolean esRecargaPipaFinal);

    void registrarAutoconsumoEstacion(SAGASSql sagasSql, String token, AutoconsumoDTO autoconsumoDTO, boolean esAutoconsumoEstacionFinal);

    void registrarAutoconsumoInventario(SAGASSql sagasSql, String token, AutoconsumoDTO autoconsumoDTO, boolean esAutoconsumoInventarioFinal);

    void registrarAutoconsumoPipa(SAGASSql sagasSql, String token, AutoconsumoDTO autoconsumoDTO, boolean esAutoconsumoPipaFinal);

    void registrarTraspasoEstracion(SAGASSql sagasSql, String token, TraspasoDTO traspasoDTO, boolean esTraspasoEstacionFinal);

    void registrarTraspasoEstracionPipa(SAGASSql sagasSql, String token, TraspasoDTO traspasoDTO, boolean esTraspasoPipaFinal);

    void registrarCalibracionEstacion(SAGASSql sagasSql, String token, CalibracionDTO calibracionDTO, boolean esCalibracionEstacionFinal);

    void registrarCalibracionPipa(SAGASSql sagasSql, String token, CalibracionDTO calibracionDTO, boolean esCalibracionPipaFinal);
}
