package com.example.neotecknewts.sagasapp.Interactor;

/**
 * Created by neotecknewts on 10/08/18.
 */
//interfaz que define los metodos que hacen llamada a web service
public interface VistaOrdenesCompraInteractor {
    void getOrdenesCompra(int IdEmpresa, String token);
}
