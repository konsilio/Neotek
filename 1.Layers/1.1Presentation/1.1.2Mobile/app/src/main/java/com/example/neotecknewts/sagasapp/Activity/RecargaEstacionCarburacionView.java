package com.example.neotecknewts.sagasapp.Activity;

import java.util.ArrayList;

public interface RecargaEstacionCarburacionView {
    void VerificarFormulario();
    void EnviarDatos();
    void MostrarErrores(ArrayList<String> mensaje);
    void onShowProgress(int mensaje);
    void onHiddenProgress();
    void onError(String mensaje);
    void onSuccessLista(Object datosRecargasDTO);
}
