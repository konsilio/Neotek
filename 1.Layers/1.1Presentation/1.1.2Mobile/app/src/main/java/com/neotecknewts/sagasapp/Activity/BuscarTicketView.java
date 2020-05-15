package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosVentasDTO;

public interface BuscarTicketView {
    void onShowPorgress(int mensaje);
    void onHiddeProgress();
    void onSuccessList(DatosVentasDTO dtos);
    void onError(String mensaje);
}
