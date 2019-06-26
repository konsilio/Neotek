package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosClientesDTO;

public interface BuscarClienteView {
    void onShowPorgress(int mensaje);
    void onHiddeProgress();
    void onSuccessList(DatosClientesDTO dtos);
    void onError(String mensaje);

}
