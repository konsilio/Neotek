package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

public interface AutoconsumoInventarioView {
    void VerificarCampos();
    void onError(String mensaje);
    void onSuccessLista(DatosAutoconsumoDTO data);
    void onShowProgress(int mensaje);
    void onHiddenProgress();
}
