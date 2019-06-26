package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

public interface RecargaEstacionCarburacionPresenter {
    void getLista(String token);

    void onError(String mensaje);

    void onSuccesslista(DatosRecargaDto datosRecargaDTO);
}
