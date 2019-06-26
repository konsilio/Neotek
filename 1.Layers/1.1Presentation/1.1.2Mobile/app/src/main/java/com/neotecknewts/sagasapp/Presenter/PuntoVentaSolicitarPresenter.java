package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;

public interface PuntoVentaSolicitarPresenter {
    void hayCorte(String token);

    void onSuccess(RespuestaCortesAntesVentaDTO data);

    void onError(String s);
}
