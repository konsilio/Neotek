package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface RecargaCamionetaPresenter {
    void getCamionetas(String token);

    void onSuccessCamionetas(DatosRecargaDto data);

    void onError(String mensaje);
}
