package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;

import java.util.List;

public interface LecturaCamionetaView {
    void verificarForm();
    void onSuccessCamionetas(List<EstacionCarburacionDTO> data);
    void onErrorCamionetas();
    void mensajeError(List<String>mensajes_error);
    void dialogoRetornar();

    void onShowProgressDialog(int message_cargando);

    void hideProgress();
}
