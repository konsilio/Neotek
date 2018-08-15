package com.example.neotecknewts.sagasapp.Interactor;

/**
 * Created by neotecknewts on 13/08/18.
 */

public interface IniciarDescargaInteractor {
    void getOrdenesCompra(int IdEmpresa, String token);
    void getMedidores(String token);
    void getAlmacenes(String token);
}
