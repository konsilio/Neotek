package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.PuntoVentaPagarView;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaPagarInteractor;
import com.example.neotecknewts.sagasapp.Interactor.PuntoVentaPagarInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public class PuntoVentaPagarPresenterImpl implements PuntoVentaPagarPresenter {
    PuntoVentaPagarView view;
    PuntoVentaPagarInteractor interactor;
    public PuntoVentaPagarPresenterImpl(PuntoVentaPagarView view) {
        this.view = view;
        this.interactor = new PuntoVentaPagarInteractorImpl(this);
    }

    @Override
    public void pagar(VentaDTO ventaDTO, String token, boolean esCamioneta, boolean esEstacion,
                      boolean esPipa, SAGASSql sagasSql) {
        view.onShowProgress(R.string.message_cargando);
        interactor.pagar(ventaDTO,token,esCamioneta,esEstacion,esPipa,sagasSql);
    }

    @Override
    public void onError(String error) {
        view.onHiddeProgress();
        view.onError(error);
    }

    @Override
    public void onError(RespuestaPuntoVenta data) {
        view.onHiddeProgress();
        view.onError(data);
    }

    @Override
    public void onSuccess(RespuestaPuntoVenta data) {
        view.onHiddeProgress();
        view.onSuccess(data);
    }

    @Override
    public void onSuccessAndroid() {
        view.onHiddeProgress();
        view.onSuccessAndroid();
    }

    @Override
    public void puntoVentaAsignado(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.puntoVentaAsignado(token);
    }

    @Override
    public void onSuccessPuntoVentaAsignado(PuntoVentaAsignadoDTO data) {
        view.onHiddeProgress();
        view.onSuccessPuntoVentaAsignado(data);
    }

    @Override
    public void onErrorPuntoVenta(String mensaje) {
        view.onHiddeProgress();
        view.onErrorPuntoVenta(mensaje);
    }
}
