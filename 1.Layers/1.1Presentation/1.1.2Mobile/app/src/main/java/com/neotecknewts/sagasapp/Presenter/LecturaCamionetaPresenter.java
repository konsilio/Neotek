package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface LecturaCamionetaPresenter {

    void GetListCamionetas(String token, boolean esFinalizar);

    void onError();

    void onSuccessCamionetas(DatosTomaLecturaDto data);
}
