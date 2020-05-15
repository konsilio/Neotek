package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Activity.BuscarTicketView;
import com.neotecknewts.sagasapp.Interactor.BuscarTicketInteractor;
import com.neotecknewts.sagasapp.Interactor.BuscarTicketInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosVentasDTO;
import com.neotecknewts.sagasapp.R;

public class BuscarTicketPresenterImpl implements BuscarTicketPresenter{

    BuscarTicketView view;
    BuscarTicketInteractor interactor;

    public BuscarTicketPresenterImpl(BuscarTicketView view) {
        this.view = view;
        this.interactor = new BuscarTicketInteractorImpl(this);
    }

    @Override
    public void getTickets(String token) {
        view.onShowPorgress(R.string.message_cargando);
        interactor.getTickets(token);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddeProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccess(DatosVentasDTO ventas) {
        view.onHiddeProgress();
        view.onSuccessList(ventas);
    }
}
