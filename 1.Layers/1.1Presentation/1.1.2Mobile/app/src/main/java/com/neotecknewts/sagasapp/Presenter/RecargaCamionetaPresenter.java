package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

public interface RecargaCamionetaPresenter {
    void getCamionetas(String token);

    void onSuccessCamionetas(DatosRecargaDto data);

    void onError(String mensaje);
}
