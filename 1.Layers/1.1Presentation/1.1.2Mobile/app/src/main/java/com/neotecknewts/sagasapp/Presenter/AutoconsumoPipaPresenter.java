package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.neotecknewts.sagasapp.Util.Session;

public interface AutoconsumoPipaPresenter {
    void getList(Session session, boolean esAutoconsumoPipaFinal);

    void onSuccess(DatosAutoconsumoDTO dto);

    void onError(String mensaje);
}
