package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.Model.PrecioVentaDTO;

import java.util.List;

public interface PuntoVentaGasListaPresenter {
    void getListaCamionetaCilindros(boolean esGasLP, boolean esCilindroConGas, boolean esCilindro, int idCliente, String token);

    void onError(String s);

    void onSuccess(List<ExistenciasDTO> data);

    void getListaVenta(boolean esGasLP, boolean esCilindroGas, boolean esCilindro, int idCliente, String token );

    void getPrecioVenta(String token);

    void onSuccessPrecioVenta(PrecioVentaDTO data);

    void getCamionetaCilindros(boolean esGasLP, boolean esCilindroGas, boolean esCilindro,int idCliente, String token);

    void onSuccessDatosCamioneta(List<ExistenciasDTO> data);
}
