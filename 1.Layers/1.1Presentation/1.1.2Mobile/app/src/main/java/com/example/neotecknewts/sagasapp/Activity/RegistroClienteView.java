package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;

import java.util.ArrayList;

public interface RegistroClienteView {
    void registrarCliente();
    void onShowProgress(int mensaje);
    void onHideProgress();
    void onError(DatosTipoPersonaDTO dto);
    void onSuccessDatosFiscales(DatosTipoPersonaDTO dto);
    void verificarForm();
    void mostrarDialogoErrores(ArrayList<String> mensajes);

    void onError(String message);
}
