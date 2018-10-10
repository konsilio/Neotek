package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;

public interface PuntoVentaGasListaPresenter {
    void getListaCamionetaCilindros(String token, boolean esGasLP,
                                    boolean esCilindroConGas, boolean esCilindro);

    void onError(String s);

    void onSuccess(DatosPuntoVentaDTO data);
}
