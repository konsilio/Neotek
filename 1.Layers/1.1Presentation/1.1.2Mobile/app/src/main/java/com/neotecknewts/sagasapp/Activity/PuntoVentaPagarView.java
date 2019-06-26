package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.neotecknewts.sagasapp.Model.RespuestaVentaExtraforaneaDTO;

import org.json.JSONObject;

import java.util.List;

public interface PuntoVentaPagarView {
    void calcula_total(List<ConceptoDTO> conceptoDTOS);
    void onShowProgress(int mensaje);
    void onHiddeProgress();
    void onError(String mensaje);

    void onError(RespuestaPuntoVenta data);

    void onSuccess(RespuestaPuntoVenta data);

    void onSuccessAndroid();

    void onSuccessPuntoVentaAsignado(PuntoVentaAsignadoDTO data);

    void onErrorPuntoVenta(String mensaje);

    void onSuccessExtraforanea(RespuestaVentaExtraforaneaDTO data);

    void onErrorInternalServer(JSONObject respuesta);
}
