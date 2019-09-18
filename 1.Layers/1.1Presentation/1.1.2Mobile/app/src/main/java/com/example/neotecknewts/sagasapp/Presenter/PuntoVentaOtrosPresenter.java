package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;

public interface PuntoVentaOtrosPresenter {
    void getList(String token);

    void onSuccess(DatosVentaOtrosDTO data);

    void onError(String mensaje);
}
