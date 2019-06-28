package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.PuntoVentaGasListaView;
import com.neotecknewts.sagasapp.Interactor.PuntoVentaGasListaInteractor;
import com.neotecknewts.sagasapp.Interactor.PuntoVentaGasListaInteractorImpl;
import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.Model.PrecioVentaDTO;

import java.util.List;

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
        //view.onShowProgress(R.string.message_cargando);
        interactor.getListaCamionetaCilindros(token, esGasLP,
         esCilindroConGas, esCilindro);
    }

    @Override
    public void onError(String s) {
        view.onHideProgress();
        view.onError(s);
    }

    @Override
    public void onSuccess(List<ExistenciasDTO> data) {
        //view.onHideProgress();
        view.onSuccessListExistencia(data);
    }

    @Override
    public void getListaVenta(String token, boolean esGasLP, boolean esCilindroGas, boolean esCilindro) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getListEstacionGas(
                 token,  esGasLP,  esCilindroGas,  esCilindro
        );
    }

    @Override
    public void getPrecioVenta(String token) {
        //view.onShowProgress(R.string.message_cargando);
        interactor.getPrecioVenta(token);
    }

    @Override
    public void onSuccessPrecioVenta(PrecioVentaDTO data) {
        //view.onHideProgress();
        view.onSuccessPrecioVenta(data);

    }

    @Override
    public void getCamionetaCilindros(boolean esGasLP, boolean esCilindroGas, boolean esCilindro,
                                      String token) {
        //view.onShowProgress(R.string.message_cargando);
        interactor.getCamionetaCilindros(esGasLP,esCilindroGas,esCilindro,token);
    }

    @Override
    public void onSuccessDatosCamioneta(List<ExistenciasDTO> data) {
        view.onHideProgress();
        view.onSuccessDatosCamioneta(data);
    }
}