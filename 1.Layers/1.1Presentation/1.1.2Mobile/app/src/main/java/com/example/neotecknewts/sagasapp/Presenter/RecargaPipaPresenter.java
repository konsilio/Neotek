package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface RecargaPipaPresenter {
    void getLists(String token,boolean EsRecargaPipaFinal);

    void onSuccessList(DatosRecargaDto data);

    void onError(String mensaje);
}
