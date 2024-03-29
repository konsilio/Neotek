package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaVerificarLecturasDTO;

public interface AnticipoEstacionCarburacionView {
    void onShowProgress(int message_cargando);

    void onHiddeProgress();

    void onError(String error);

    void onError(RespuestaEstacionesVentaDTO data);

    void onSuccess(RespuestaEstacionesVentaDTO data);

    void onSuccessRespuestaLecturas(RespuestaVerificarLecturasDTO data);

    void onErrorVerificarLecturas(String mensaje);
}
