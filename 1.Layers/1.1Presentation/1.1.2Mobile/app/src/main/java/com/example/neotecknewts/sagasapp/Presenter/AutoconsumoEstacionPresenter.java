package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

public interface AutoconsumoEstacionPresenter {
    void getList(String token,boolean esFinal);

    void onSuccessList(DatosAutoconsumoDTO data);

    void onError(String mensaje);
}
