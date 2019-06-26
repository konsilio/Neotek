package com.neotecknewts.sagasapp.Interactor;

/**
 * Created by neotecknewts on 13/08/18.
 */
//interfaz que define los metodos que hacen llamada a web service
public interface IniciarDescargaInteractor {
    void getOrdenesCompra(int IdEmpresa, String token);
    void getMedidores(String token);
    void getAlmacenes(String token);
}
