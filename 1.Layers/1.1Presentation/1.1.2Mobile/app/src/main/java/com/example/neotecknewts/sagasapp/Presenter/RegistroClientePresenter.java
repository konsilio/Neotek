package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;

public interface RegistroClientePresenter {
    void getLista(String token);

    void onSuccess(DatosTipoPersonaDTO data);

    void onError(DatosTipoPersonaDTO data);

    void onError(String message);
}
