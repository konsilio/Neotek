package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

public interface TraspasoEstacionPresenter {
    void GetList(String token, boolean esFinal);

    void onError(String mensaje);

    void onSuccess(DatosTraspasoDTO datosTraspasoDTO);
}
