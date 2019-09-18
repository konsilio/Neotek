package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;

public interface RecargaPipaView {
    void ValidateForm();
    void onSuccessLista(DatosRecargaDto data);
    void onError(String mensaje);
    void onShowProgress(int mensaje);
    void onHiddenProgress();
}
