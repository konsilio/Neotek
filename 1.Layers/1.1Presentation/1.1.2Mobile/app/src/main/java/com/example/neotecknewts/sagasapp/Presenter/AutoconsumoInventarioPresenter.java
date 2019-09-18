package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

public interface AutoconsumoInventarioPresenter {
    void getList(String token, boolean esAutoconsumoInventarioFinal);

    void onError(String mensaje);

    void onSuccess(DatosAutoconsumoDTO dto);
}
