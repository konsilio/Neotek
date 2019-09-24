package com.neotecknewts.sagasapp.Interactor;

import android.content.Context;

import com.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.neotecknewts.sagasapp.Model.LecturaDTO;
import com.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesInteractor {
    //void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, SAGASSql sagasSql, Context applicationContext);
    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, SAGASSql sagasSql, Context applicationContext);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO, String token, SAGASSql iniciarDescargaSQL);
    //void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO, String token, SAGASSql sagasSql);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO, String token, SAGASSql sagasSql);

    void registrarLecturaInicial(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO);

    void registrarLecturaFinal(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO);

    void registrarLecturaInicialPipa(SAGASSql sagasSql, String token, LecturaPipaDTO lecturaPipaDTO);

    void registrarLecturaFinalizalPipa(SAGASSql sagasSql, String token, LecturaPipaDTO lecturaPipaDTO);

    void registrarLecturaInicialAlmacen(SAGASSql sagasSql, String token,
                                        LecturaAlmacenDTO lecturaAlmacenDTO);

    void registrarLecturaFinalAlmacen(SAGASSql sagasSql, String token,
                                      LecturaAlmacenDTO lecturaAlmacenDTO);

    void registrarRecargaEstacion(SAGASSql sagasSql, String token, RecargaDTO recargaDTO,
                                  boolean EsRecargaEstacionInicial);

    void registrarRecargaPipa(SAGASSql sagasSql, String token, RecargaDTO recargaDTO, boolean esRecargaPipaFinal);

    void registrarAutoconsumoEstacion(SAGASSql sagasSql, String token, AutoconsumoDTO autoconsumoDTO, boolean esAutoconsumoEstacionFinal);

    void registrarAutoconsumoInventario(SAGASSql sagasSql, String token, AutoconsumoDTO autoconsumoDTO, boolean esAutoconsumoInventarioFinal);

    void registrarAutoconsumoPipa(SAGASSql sagasSql, String token, AutoconsumoDTO autoconsumoDTO, boolean esAutoconsumoPipaFinal);

    void registrarTraspasoEstacion(SAGASSql sagasSql, String token, TraspasoDTO traspasoDTO, boolean esTraspasoEstacionFinal);

    void registrarTraspasoPipa(SAGASSql sagasSql, String token, TraspasoDTO traspasoDTO, boolean esTraspasoPipaFinal);

    void registrarCalibracionEstacion(SAGASSql sagasSql, String token, CalibracionDTO calibracionDTO, boolean esCalibracionEstacionFinal);

    void registrarCalibracionPipa(SAGASSql sagasSql, String token, CalibracionDTO calibracionDTO, boolean esCalibracionPipaFinal);
}