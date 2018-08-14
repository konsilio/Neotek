package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.IniciarDescargaView;
import com.example.neotecknewts.sagasapp.Interactor.IniciarDescargaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.IniciarDescargaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.R;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class IniciarDescargaPresenterImpl implements IniciarDescargaPresenter {
    IniciarDescargaInteractor interactor;
    IniciarDescargaView iniciarDescargaView;

    public IniciarDescargaPresenterImpl(IniciarDescargaView view){
        this.iniciarDescargaView = view;
        this.interactor = new IniciarDescargaInteractorImpl(this);
    }
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        iniciarDescargaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,token);
    }

    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.onSuccessGetOrdenesCompra(respuestaOrdenesCompraDTO);

    }

    @Override
    public void onError() {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.messageError(R.string.error_conexion);

    }
}
