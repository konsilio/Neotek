package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

public interface AutoconsumoEstacionPresenter {
    void getList(String token, boolean esFinal);

    void onSuccessList(DatosAutoconsumoDTO data);

    void onError(String mensaje);
}
