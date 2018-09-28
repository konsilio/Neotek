package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

public interface CalibracionPipaPresenter {
    void getList(String token,boolean EsCalibracionPipaFinal);

    void onSuccess(DatosCalibracionDTO data);

    void onError(String mensajesError);
}
