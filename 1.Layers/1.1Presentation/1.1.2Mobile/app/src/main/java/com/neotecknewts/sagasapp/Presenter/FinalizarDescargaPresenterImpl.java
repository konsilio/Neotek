package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.FinalizarDescargaView;
import com.neotecknewts.sagasapp.Interactor.FinalizarDescargaInteractor;
import com.neotecknewts.sagasapp.Interactor.FinalizarDescargaInteractorImpl;
import com.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.neotecknewts.sagasapp.Model.MedidorDTO;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class FinalizarDescargaPresenterImpl implements FinalizarDescargaPresenter {
    //se delcaran la vista y el interactor
    FinalizarDescargaInteractor interactor;
    FinalizarDescargaView finalizarDescargaView;

    //se obtiene la vista al ser contruido y se inicializa el interactor
    public FinalizarDescargaPresenterImpl(FinalizarDescargaView view){
        this.finalizarDescargaView = view;
        this.interactor = new FinalizarDescargaInteractorImpl(this);
    }

    //metodo para obtener ordenes de compra
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        finalizarDescargaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,token);
    }

    //metodo que se llama al obtener las ordenes de compra
    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.onSuccessGetOrdenesCompra(respuestaOrdenesCompraDTO);

    }
    //metodo para obtener medidores

    @Override
    public void getMedidores(String token) {
        finalizarDescargaView.showProgress(R.string.message_cargando);
        interactor.getMedidores(token);
    }

    //metodo que se llama al obtener los medidores
    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.onSuccessGetMedidores(medidorDTOList);
    }

    //metodo para obtener alamcenes
    @Override
    public void getAlmacenes(String token) {
        finalizarDescargaView.showProgress(R.string.message_cargando);
        interactor.getAlmacenes(token);
    }

    //metodo que se llama al obtener los almacenes
    @Override
    public void onSuccessGetAlmacenes(List<AlmacenDTO> almacenDTOs) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.onSuccessGetAlmacen(almacenDTOs);
    }

//metodo de error
    @Override
    public void onError() {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.messageError(R.string.error_conexion);

    }

    @Override
    public void onError(String mensaje) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.messageError(mensaje);
    }
}
