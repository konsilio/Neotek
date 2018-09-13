package com.example.neotecknewts.sagasapp.Interactor;

public interface LecturaAlmacenInteractor {
    void getMedidores(String token);

    void getAlmacenes(String token,boolean esFinalizar);
}
