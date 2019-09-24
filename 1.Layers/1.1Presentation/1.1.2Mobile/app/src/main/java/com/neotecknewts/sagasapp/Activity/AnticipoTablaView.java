package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosBusquedaCortesDTO;
import com.neotecknewts.sagasapp.Model.UsuariosCorteDTO;

public interface AnticipoTablaView {
    void VerificarCampos();

    void onShowProgress(int message_cargando);

    void HiddeProgress();

    void onSuccess();

    void onError(String mensaje);

    void onSuccessAndroid();

    void onError(DatosBusquedaCortesDTO ob);

    //void onSuccessList(RespuestaEstacionesVentaDTO data);
    void onSuccessList(DatosBusquedaCortesDTO data);

    void onSuccessList(UsuariosCorteDTO data);
}