package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

public interface RecargaPipaPresenter {
    void getLists(String token, boolean EsRecargaPipaFinal);

    void onSuccessList(DatosRecargaDto data);

    void onError(String mensaje);
}
