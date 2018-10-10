package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;

import java.util.List;

public interface PuntoVentaGasListaView {
    void mostrarConsepto(List<ConceptoDTO> conceptoDTOS);
    void onHideProgress();
    void onShowProgress(int message_cargando);
    void onError(String mensaje);
    void onSuccessListExistencia(DatosPuntoVentaDTO data);
}
