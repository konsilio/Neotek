package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RegistrarPapeletaView;
import com.example.neotecknewts.sagasapp.Interactor.RegistrarPapeletaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.RegistrarPapeletaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.R;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class RegistrarPapeletaPresenterImpl implements RegistrarPapeletaPresenter {
    RegistrarPapeletaInteractor interactor;
    RegistrarPapeletaView registrarPapeletaView;

    public RegistrarPapeletaPresenterImpl(RegistrarPapeletaView view){
        this.registrarPapeletaView = view;
        this.interactor = new RegistrarPapeletaInteractorImpl(this);
    }
    @Override
    public void getOrdenesCompraExpedidor(int IdEmpresa, String token) {
        registrarPapeletaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,true,true,false,token);
    }
    @Override
    public void getOrdenesCompraPorteador(int IdEmpresa, String token) {
        registrarPapeletaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,false,false,true,token);
    }

    @Override
    public void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessGetOrdenesCompraExpedidor(respuestaOrdenesCompraDTO);

    }

    @Override
    public void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessGetOrdenesCompraPorteador(respuestaOrdenesCompraDTO);

    }

    @Override
    public void onError() {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.messageError(R.string.error_conexion);

    }
}
