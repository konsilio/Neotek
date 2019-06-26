package com.neotecknewts.sagasapp.Interactor;

/**
 * Created by neotecknewts on 13/08/18.
 */
//interfaz que define los metodos que hacen llamada a web service
public interface RegistrarPapeletaInteractor {
    void getOrdenesCompra(int IdEmpresa, boolean EsGas, boolean EsActivoVenta, boolean EsTransporteGas, String token);
    void getMedidores(String token);

    void getOrderReferencia(String token, int idOrdenCompra, boolean esExpedidor);
}
