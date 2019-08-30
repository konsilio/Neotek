package com.neotecknewts.sagasapp.Presenter;

import android.content.Context;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.BuscarClienteView;
import com.neotecknewts.sagasapp.Interactor.BuscarClienteInteractor;
import com.neotecknewts.sagasapp.Interactor.BuscarClienteInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosClientesDTO;

public class BuscarClientePresenterImpl implements BuscarClientePresenter {
    BuscarClienteView view;
    BuscarClienteInteractor interactor;
    public BuscarClientePresenterImpl(BuscarClienteView view, Context context) {
        this.view = view;
        this.interactor = new BuscarClienteInteractorImpl(this, context);}

    @Override
    public void getClientes(String criterio,String token) {
        view.onShowPorgress(R.string.message_cargando);
        interactor.getClientes(criterio,token);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddeProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccess(DatosClientesDTO clienteDTOS) {
        view.onHiddeProgress();
        view.onSuccessList(clienteDTOS);
    }
}
