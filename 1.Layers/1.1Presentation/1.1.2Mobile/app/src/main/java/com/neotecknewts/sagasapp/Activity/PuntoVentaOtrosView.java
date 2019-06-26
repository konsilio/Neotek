package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;

import java.util.List;

public interface PuntoVentaOtrosView {
    void onSuccess(DatosVentaOtrosDTO dtos);
    void onError(DatosVentaOtrosDTO datos);
    void onError(String mensaje);
    void onShowProgress(int mensaje);
    void onHiddenProgress();
    void mostrarConcepto(List<ConceptoDTO> list);
}
