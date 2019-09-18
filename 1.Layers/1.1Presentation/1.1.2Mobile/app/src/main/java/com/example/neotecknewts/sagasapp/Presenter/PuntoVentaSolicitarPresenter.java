package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;

public interface PuntoVentaSolicitarPresenter {
    void hayCorte(String token);

    void onSuccess(RespuestaCortesAntesVentaDTO data);

    void onError(String s);
}
