package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.PuntoVentaGasListaView;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaGasListaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaGasListaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.R;

public class PuntoVentaGasListaPresenterImpl implements PuntoVentaGasListaPresenter {
    PuntoVentaGasListaView view;
    PuntoVentaGasListaInteractor interactor;
    public PuntoVentaGasListaPresenterImpl(PuntoVentaGasListaView view) {
        this.view = view;
        this.interactor = new PuntoVentaGasListaInteractorImpl(this);
    }

    @Override
    public void getListaCamionetaCilindros(String token, boolean esGasLP,
                                           boolean esCilindroConGas, boolean esCilindro) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getListaCamionetaCilindros(token, esGasLP,
         esCilindroConGas, esCilindro);
    }

    @Override
    public void onError(String s) {
        view.onHideProgress();
        view.onError(s);
    }

    @Override
    public void onSuccess(DatosPuntoVentaDTO data) {
        view.onHideProgress();
        view.onSuccessListExistencia(data);
    }
}