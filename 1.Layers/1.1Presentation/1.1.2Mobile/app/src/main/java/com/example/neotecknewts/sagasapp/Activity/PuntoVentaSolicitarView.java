package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.RespuestaCortesAntesVentaDTO;

public interface PuntoVentaSolicitarView {
    void SeguirSinNumero();
    void RegistrarCliente();
    void Buscar();
    void onShowProgress(int mensaje);
    void onError(String mensaje);
    void onHiddeProgress();

    void onResultVerificaCorte(RespuestaCortesAntesVentaDTO data);

}
