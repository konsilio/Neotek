package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;

public interface PorcentajeCalibracionPresenter {
    void getPorcentaje(String token);

    void onError(String s);

    void onSuccessPorcentaje(DatosEmpresaConfiguracionDTO data);
}
