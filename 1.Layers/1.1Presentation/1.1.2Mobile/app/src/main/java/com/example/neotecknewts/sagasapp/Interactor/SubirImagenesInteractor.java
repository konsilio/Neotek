package com.example.neotecknewts.sagasapp.Interactor;

import android.content.Context;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesInteractor {
    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL, Context context);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO);
}
