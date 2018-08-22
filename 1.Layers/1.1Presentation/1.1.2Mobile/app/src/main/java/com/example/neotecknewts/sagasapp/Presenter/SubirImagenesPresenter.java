package com.example.neotecknewts.sagasapp.Presenter;

import android.content.Context;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletasImagenesSQL;

import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesPresenter {

    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL, Context context);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO);
    void onSuccessRegistrarPapeleta();
    void onSuccessRegistrarIniciarDescarga();
    void onSuccessRegistrarFinalizarDescarga();
    void onError();
}
