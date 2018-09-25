package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;

public interface TraspasoPipaPresenter {
    void GetList(String token);

    void onSuccess(DatosTraspasoDTO data);

    void onError(String message);
}
