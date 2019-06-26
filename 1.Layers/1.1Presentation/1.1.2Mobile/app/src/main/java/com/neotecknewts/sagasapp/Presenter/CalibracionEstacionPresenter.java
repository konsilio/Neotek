package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosCalibracionDTO;

public interface CalibracionEstacionPresenter {
    void getList(String token, boolean esCalibracionEstacionFinal);

    void onSuccess(DatosCalibracionDTO data);

    void onError(String message);
}
