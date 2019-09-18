package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

public interface AutoconsumoInventarioView {
    void VerificarCampos();
    void onError(String mensaje);
    void onSuccessLista(DatosAutoconsumoDTO data);
    void onShowProgress(int mensaje);
    void onHiddenProgress();
}
