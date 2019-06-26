package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

import java.util.List;

public interface LecturaCamionetaView {
    void verificarForm();
    void onSuccessCamionetas(DatosTomaLecturaDto data);
    void onErrorCamionetas();
    void mensajeError(List<String> mensajes_error);
    void dialogoRetornar();

    void onShowProgressDialog(int message_cargando);

    void hideProgress();
}
