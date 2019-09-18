package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Util.Session;

public interface AutoconsumoPipaPresenter {
    void getList(Session session, boolean esAutoconsumoPipaFinal);

    void onSuccess(DatosAutoconsumoDTO dto);

    void onError(String mensaje);
}
