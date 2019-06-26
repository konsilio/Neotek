package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.IniciarDescargaView;
import com.neotecknewts.sagasapp.Interactor.IniciarDescargaInteractor;
import com.neotecknewts.sagasapp.Interactor.IniciarDescargaInteractorImpl;
import com.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.neotecknewts.sagasapp.Model.MedidorDTO;
import com.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class IniciarDescargaPresenterImpl implements IniciarDescargaPresenter {
    //se delcaran la vista y el interactor
    IniciarDescargaInteractor interactor;
    IniciarDescargaView iniciarDescargaView;

    //se obtiene la vista al ser contruido y se inicializa el interactor
    public IniciarDescargaPresenterImpl(IniciarDescargaView view){
        this.iniciarDescargaView = view;
        this.interactor = new IniciarDescargaInteractorImpl(this);
    }
    //metodo para obtener ordenes de compra
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        iniciarDescargaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,token);
    }

    //metodo que se llama al obtener las ordenes de compra
    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.onSuccessGetOrdenesCompra(respuestaOrdenesCompraDTO);

    }

    //metodo para obtener medidores
    @Override
    public void getMedidores(String token) {
        iniciarDescargaView.showProgress(R.string.message_cargando);
        interactor.getMedidores(token);
    }

    //metodo que se llama al obtener los medidores
    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList) {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.onSuccessGetMedidores(medidorDTOList);
    }
    //metodo para obtener almacenes
    @Override
    public void getAlmacenes(String token) {
        iniciarDescargaView.showProgress(R.string.message_cargando);
        interactor.getAlmacenes(token);
    }

    //metodo que se llama al obtener los almacenes
    @Override
    public void onSuccessGetAlmacenes(List<AlmacenDTO> almacenDTOs) {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.onSuccessGetAlmacen(almacenDTOs);
    }
//metodo de error
    @Override
    public void onError() {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.messageError(R.string.error_conexion);

    }
}
