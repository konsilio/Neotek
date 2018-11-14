package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;

public interface RecargaEstacionCarburacionPresenter {
    void getLista(String token);

    void onError(String mensaje);

    void onSuccesslista(DatosRecargaDto datosRecargaDTO);
}
