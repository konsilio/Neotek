package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosEmpresaConfiguracionDTO;

public interface PorcentajeCalibracionView {
    void VerificarPorcentaje();
    void onSuccessPorcentaje(DatosEmpresaConfiguracionDTO dto);
    void onErrorPorcentaje(String mensaje);
    void confirmar();
    void continuar();
    void onShowProgress(int mensaje);
    void onHiddeProgress();
}
