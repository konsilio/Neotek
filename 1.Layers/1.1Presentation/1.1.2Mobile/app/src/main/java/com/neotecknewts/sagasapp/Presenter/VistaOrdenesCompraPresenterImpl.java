package com.neotecknewts.sagasapp.Presenter;

import android.util.Log;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.VistaOrdenCompraView;
import com.neotecknewts.sagasapp.Interactor.VistaOrdenesCompraInteractor;
import com.neotecknewts.sagasapp.Interactor.VistaOrdenesCompraInteractorImpl;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

/**
 * Created by neotecknewts on 10/08/18.
 */

public class VistaOrdenesCompraPresenterImpl implements VistaOrdenesCompraPresenter {
    //se delcaran la vista y el interactor
    VistaOrdenesCompraInteractor interactor;
    VistaOrdenCompraView vistaOrdenCompraView;

    //se obtiene la vista al ser contruido y se inicializa el interactor
    public VistaOrdenesCompraPresenterImpl(VistaOrdenCompraView view){
        this.vistaOrdenCompraView = view;
        this.interactor = new VistaOrdenesCompraInteractorImpl(this);
    }
    //metodo para obtener ordenes de compra
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        vistaOrdenCompraView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa, token);
    }

    //metodo que se llama al obtner las ordenes de compra
    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        vistaOrdenCompraView.hideProgress();
        vistaOrdenCompraView.onSuccessGetOrdenesCompra(respuestaOrdenesCompraDTO);
    }

    //metodo de error
    @Override
    public void onError() {
        Log.e("error", "onerror");
        vistaOrdenCompraView.hideProgress();
        vistaOrdenCompraView.messageError(R.string.error_conexion);
    }

    @Override
    public void onError(String mensaje) {
        vistaOrdenCompraView.hideProgress();
        vistaOrdenCompraView.messageError(mensaje);
    }
}
