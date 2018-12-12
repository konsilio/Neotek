package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuariosCorteDTO;

public interface AnticipoTablaView {
    void VerificarCampos();

    void onShowProgress(int message_cargando);

    void HiddeProgress();

    void onSuccess();

    void onError(String mensaje);

    void onSuccessAndroid();

    void onError(Object ob);

    void onSuccessList(RespuestaEstacionesVentaDTO data);

    void onSuccessList(UsuariosCorteDTO data);
}
