package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesPresenter {

    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO);
    void onSuccessRegistrarPapeleta();
    void onSuccessRegistrarIniciarDescarga();
    void onSuccessRegistrarFinalizarDescarga();
    void onError();
}
