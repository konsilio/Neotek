package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RegistrarPapeletaView;
import com.example.neotecknewts.sagasapp.Interactor.RegistrarPapeletaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.RegistrarPapeletaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class RegistrarPapeletaPresenterImpl implements RegistrarPapeletaPresenter {
    //se delcaran la vista y el interactor
    RegistrarPapeletaInteractor interactor;
    RegistrarPapeletaView registrarPapeletaView;

    //se obtiene la vista al ser contruido y se inicializa el interactor
    public RegistrarPapeletaPresenterImpl(RegistrarPapeletaView view){
        this.registrarPapeletaView = view;
        this.interactor = new RegistrarPapeletaInteractorImpl(this);
    }
    //metodo para obtener ordenes de compra
    @Override
    public void getOrdenesCompraExpedidor(int IdEmpresa, String token) {
        registrarPapeletaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,true,true,false,token);
    }
    //metodo para obtener ordenes de compra
    @Override
    public void getOrdenesCompraPorteador(int IdEmpresa, String token) {
        registrarPapeletaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,false,false,true,token);
    }

    //metodo que se llama al obtener las ordenes de compra
    @Override
    public void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessGetOrdenesCompraExpedidor(respuestaOrdenesCompraDTO);

    }
    //metodo que se llama al obtener las ordenes de compra
    @Override
    public void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessGetOrdenesCompraPorteador(respuestaOrdenesCompraDTO);

    }

    //metodo para obtener medidores
    @Override
    public void getMedidores(String token) {
        registrarPapeletaView.showProgress(R.string.message_cargando);
        interactor.getMedidores(token);
    }

    //metodo que se llama al obtener los medidores
    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList) {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessGetMedidores(medidorDTOList);
    }

    //metodo de error
    @Override
    public void onError() {
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.messageError(R.string.error_conexion);

    }
}
