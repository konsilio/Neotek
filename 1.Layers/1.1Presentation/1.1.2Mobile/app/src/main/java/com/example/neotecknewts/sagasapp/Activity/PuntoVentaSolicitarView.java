package com.example.neotecknewts.sagasapp.Activity;

public interface PuntoVentaSolicitarView {
    void SeguirSinNumero();
    void RegistrarCliente();
    void Buscar();
    void onShowProgress(int mensaje);
    void onError(String mensaje);
    void onHiddeProgress();
}
