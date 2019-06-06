package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

public interface CalibracionEstacionPresenter {
    void getList(String token, boolean esCalibracionEstacionFinal);

    void onSuccess(DatosCalibracionDTO data);

    void onError(String message);
}
