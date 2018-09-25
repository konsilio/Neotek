package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface RecargaPipaPresenter {
    void getLists(String token,boolean EsRecargaPipaFinal);

    void onSuccessList(DatosTomaLecturaDto data);

    void onError(String mensaje);
}
