package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;

import java.util.List;

public interface PuntoVentaGasListaView {
    void mostrarConsepto(List<ConceptoDTO> conceptoDTOS);
    void onHideProgress();
    void onShowProgress(int message_cargando);
    void onError(String mensaje);
    void onSuccessListExistencia(List<ExistenciasDTO> data);
}
