package com.neotecknewts.sagasapp.Interactor;

public interface PuntoVentaGasListaInteractor {
    void getListaCamionetaCilindros(String token, boolean esGasLP,
                                    boolean esCilindroConGas, boolean esCilindro, int idCliente);

    void getListEstacionGas(boolean esGasLP, boolean esCilindroGas,
                            boolean esCilindro, int idCliente, String token);

    void getPrecioVenta(String token);

    void getCamionetaCilindros(boolean esGasLP, boolean esCilindroGas, boolean esCilindro,int idCliente, String token);

}
