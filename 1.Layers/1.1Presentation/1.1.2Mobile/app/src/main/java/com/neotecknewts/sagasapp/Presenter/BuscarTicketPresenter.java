package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosVentasDTO;

public interface BuscarTicketPresenter {

    void getTickets(String token);

    void onError(String mensaje);

    void onSuccess(DatosVentasDTO ventas);
}
