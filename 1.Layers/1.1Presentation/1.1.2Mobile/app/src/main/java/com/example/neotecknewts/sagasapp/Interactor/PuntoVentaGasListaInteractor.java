package com.example.neotecknewts.sagasapp.Interactor;

public interface PuntoVentaGasListaInteractor {
    void getListaCamionetaCilindros(String token, boolean esGasLP,
                                    boolean esCilindroConGas, boolean esCilindro);

    void getListEstacionGas(String token, boolean esGasLP, boolean esCilindroGas, boolean esCilindro);
}
