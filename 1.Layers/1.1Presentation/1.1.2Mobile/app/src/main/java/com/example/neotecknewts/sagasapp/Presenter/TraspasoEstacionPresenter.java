package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

public interface TraspasoEstacionPresenter {
    void GetList(String token);

    void onError(String mensaje);

    void onSuccess(DatosTraspasoDTO datosTraspasoDTO);
}
