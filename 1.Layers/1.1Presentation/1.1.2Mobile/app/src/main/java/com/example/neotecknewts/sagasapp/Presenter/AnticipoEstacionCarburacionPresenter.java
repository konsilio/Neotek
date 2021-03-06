package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaVerificarLecturasDTO;

public interface AnticipoEstacionCarburacionPresenter {
    void getEstaciones(String token);

    void onError(String s);

    void onError(RespuestaEstacionesVentaDTO data);

    void onSuccess(RespuestaEstacionesVentaDTO data);

    void checkLecturas(String token);

    void onSuccessVerificarLecturas(RespuestaVerificarLecturasDTO data);

    void onErrorVerificarLecturas(String localizedMessage);
}
