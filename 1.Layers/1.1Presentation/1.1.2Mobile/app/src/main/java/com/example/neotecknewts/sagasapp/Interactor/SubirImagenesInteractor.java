package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface SubirImagenesInteractor {
    void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO,String token);
    void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO);
    void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO);
}
