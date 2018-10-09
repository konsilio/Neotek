package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface PuntoVentaPagarPresenter {
    void pagar(VentaDTO ventaDTO, String token, boolean esCamioneta, boolean esEstacion,
               boolean esPipa, SAGASSql sagasSql);

    void onError(String error);

    void onError(RespuestaPuntoVenta data);

    void onSuccess(RespuestaPuntoVenta data);

    void onSuccessAndroid();
}
