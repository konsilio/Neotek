package com.example.neotecknewts.sagasapp.Presenter;

import android.content.Context;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesPresenter {

    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO, String token, IniciarDescargaSQL iniciarDescargaSQL);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO);
    void onSuccessRegistrarPapeleta();
    void onSuccessRegistrarIniciarDescarga();
    void onSuccessRegistrarFinalizarDescarga();
    void onError();
    void onSuccessRegistroPapeleta();
    void onSuccessRegistroAndroid();

    void errorSolicitud(String mensaje);

    void onRegistrarIniciarDescarga();
}
