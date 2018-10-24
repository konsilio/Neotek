package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;

public interface AnticipoEstacionCarburacionPresenter {
    void getEstaciones(String token);

    void onError(String s);

    void onError(RespuestaEstacionesVentaDTO data);

    void onSuccess(RespuestaEstacionesVentaDTO data);
}
