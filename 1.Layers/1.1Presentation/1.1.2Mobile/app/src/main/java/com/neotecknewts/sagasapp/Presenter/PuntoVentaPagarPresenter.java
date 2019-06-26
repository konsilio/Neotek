package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.neotecknewts.sagasapp.Model.RespuestaVentaExtraforaneaDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

import org.json.JSONObject;

public interface PuntoVentaPagarPresenter {
    void pagar(VentaDTO ventaDTO, String token, boolean esCamioneta, boolean esEstacion,
               boolean esPipa, SAGASSql sagasSql);

    void onError(String error);

    void onError(RespuestaPuntoVenta data);

    void onSuccess(RespuestaPuntoVenta data);

    void onSuccessAndroid();

    void puntoVentaAsignado(String token);

    void onSuccessPuntoVentaAsignado(PuntoVentaAsignadoDTO data);

    void onErrorPuntoVenta(String s);

    void verificarVentaExtraforanea(int idCliente, String token);

    void onSuccessVentaExtraforanea(RespuestaVentaExtraforaneaDTO data);

    void onErrorInternalServer(JSONObject respuesta);
}
