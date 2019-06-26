package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;

public interface RecargaPipaView {
    void ValidateForm();
    void onSuccessLista(DatosRecargaDto data);
    void onError(String mensaje);
    void onShowProgress(int mensaje);
    void onHiddenProgress();
}
