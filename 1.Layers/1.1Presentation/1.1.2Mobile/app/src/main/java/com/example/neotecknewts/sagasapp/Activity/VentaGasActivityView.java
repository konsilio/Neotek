package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;

import java.util.List;

public interface VentaGasActivityView {
    void onSuccess();
    void onError(String mensaje);
    void onShowProgress(int mensaje);
    void onHiddeProgress();
    void mostrarConcepto(List<ConceptoDTO> list);
}
