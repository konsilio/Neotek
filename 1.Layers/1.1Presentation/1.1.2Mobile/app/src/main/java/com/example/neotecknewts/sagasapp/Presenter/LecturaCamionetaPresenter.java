package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface LecturaCamionetaPresenter {

    void GetListCamionetas(String token,boolean esFinalizar);

    void onError();

    void onSuccessCamionetas(DatosTomaLecturaDto data);
}
