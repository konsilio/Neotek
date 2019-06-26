package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

import java.util.ArrayList;

public interface AutoconsumoEstacionView {
    void onSuccessLista(DatosAutoconsumoDTO dto);
    void onErrorLista(String mensaje);
    void verificarErrores();
    void onShowProgress(int mensaje);
    void onHiddenProgress();
    void mostrarError(ArrayList<String> mensajes);
}
