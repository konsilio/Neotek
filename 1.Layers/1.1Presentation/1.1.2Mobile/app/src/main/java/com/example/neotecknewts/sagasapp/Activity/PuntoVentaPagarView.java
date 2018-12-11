package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;

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
}
