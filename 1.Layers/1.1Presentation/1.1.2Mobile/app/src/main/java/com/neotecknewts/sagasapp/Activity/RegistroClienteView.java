package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaClienteDTO;

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

    void onErrorRegistro(RespuestaClienteDTO data);

    void setIdCliente(RespuestaClienteDTO data);
}
