package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

public interface CalibracionPipaPresenter {
    void getList(String token, boolean EsCalibracionPipaFinal);

    void onSuccess(DatosCalibracionDTO data);

    void onError(String mensajesError);
}
