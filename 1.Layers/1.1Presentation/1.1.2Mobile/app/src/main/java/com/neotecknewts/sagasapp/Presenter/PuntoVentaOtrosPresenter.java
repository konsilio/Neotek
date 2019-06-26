package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;

public interface PuntoVentaOtrosPresenter {
    void getList(String token);

    void onSuccess(DatosVentaOtrosDTO data);

    void onError(String mensaje);
}
