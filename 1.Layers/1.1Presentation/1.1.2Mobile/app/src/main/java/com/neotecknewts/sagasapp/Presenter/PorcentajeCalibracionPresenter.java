package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;

public interface PorcentajeCalibracionPresenter {
    void getPorcentaje(String token);

    void onError(String s);

    void onSuccessPorcentaje(DatosEmpresaConfiguracionDTO data);
}
