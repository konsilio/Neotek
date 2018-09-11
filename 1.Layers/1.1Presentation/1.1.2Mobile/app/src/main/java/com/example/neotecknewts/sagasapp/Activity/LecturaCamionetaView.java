package com.example.neotecknewts.sagasapp.Activity;

import java.util.List;

public interface LecturaCamionetaView {
    void verificarForm();
    void getCamionetas();
    void onSuccessCamionetas();
    void onErrorCamionetas();
    void mensajeError(List<String>mensajes_error);
    void dialogoRetornar();
}
