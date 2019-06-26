package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

public interface RecargaCamionetaView {
    void ValidarForm();
    void GoBackWindow();

    void showProgres();

    void hideProgress();

    void onSuccessCamionetas(DatosRecargaDto data);

    void onError(String mensaje);
}
