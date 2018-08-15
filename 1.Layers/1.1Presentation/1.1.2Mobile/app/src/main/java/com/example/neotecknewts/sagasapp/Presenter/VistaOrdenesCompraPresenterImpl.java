package com.example.neotecknewts.sagasapp.Presenter;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Activity.MainView;
import com.example.neotecknewts.sagasapp.Activity.VistaOrdenCompraView;
import com.example.neotecknewts.sagasapp.Interactor.LoginInteractor;
import com.example.neotecknewts.sagasapp.Interactor.LoginInteractorImpl;
import com.example.neotecknewts.sagasapp.Interactor.VistaOrdenesCompraInteractor;
import com.example.neotecknewts.sagasapp.Interactor.VistaOrdenesCompraInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

/**
 * Created by neotecknewts on 10/08/18.
 */

public class VistaOrdenesCompraPresenterImpl implements VistaOrdenesCompraPresenter {
    VistaOrdenesCompraInteractor interactor;
    VistaOrdenCompraView vistaOrdenCompraView;

    public VistaOrdenesCompraPresenterImpl(VistaOrdenCompraView view){
        this.vistaOrdenCompraView = view;
        this.interactor = new VistaOrdenesCompraInteractorImpl(this);
    }
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        vistaOrdenCompraView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa, token);
    }

    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        vistaOrdenCompraView.hideProgress();
        vistaOrdenCompraView.onSuccessGetOrdenesCompra(respuestaOrdenesCompraDTO);
    }

    @Override
    public void onError() {
        Log.e("error", "onerror");
        vistaOrdenCompraView.hideProgress();
        vistaOrdenCompraView.messageError(R.string.error_conexion);
    }
}
