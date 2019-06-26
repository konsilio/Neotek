package com.neotecknewts.sagasapp.Interactor;

public interface PuntoVentaGasListaInteractor {
    void getListaCamionetaCilindros(String token, boolean esGasLP,
                                    boolean esCilindroConGas, boolean esCilindro);

    void getListEstacionGas(String token, boolean esGasLP, boolean esCilindroGas,
                            boolean esCilindro);

    void getPrecioVenta(String token);

    void getCamionetaCilindros(boolean esGasLP, boolean esCilindroGas, boolean esCilindro,
                               String token);

}
