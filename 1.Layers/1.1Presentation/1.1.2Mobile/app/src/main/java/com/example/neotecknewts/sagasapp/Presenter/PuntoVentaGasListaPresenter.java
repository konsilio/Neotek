package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.Model.PrecioVentaDTO;

import java.util.List;

public interface PuntoVentaGasListaPresenter {
    void getListaCamionetaCilindros(String token, boolean esGasLP,
                                    boolean esCilindroConGas, boolean esCilindro);

    void onError(String s);

    void onSuccess(List<ExistenciasDTO> data);

    void getListaVenta(String token, boolean esGasLP, boolean esCilindroGas, boolean esCilindro);

    void getPrecioVenta(String token);

    void onSuccessPrecioVenta(PrecioVentaDTO data);

    void getCamionetaCilindros(boolean esGasLP, boolean esCilindroGas, boolean esCilindro,
                               String token);

    void onSuccessDatosCamioneta(List<ExistenciasDTO> data);
}
