package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface RecargaCamionetaView {
    void ValidarForm();
    void GoBackWindow();

    void showProgres();

    void hideProgress();

    void onSuccessCamionetas(DatosRecargaDto data);

    void onError(String mensaje);
}
