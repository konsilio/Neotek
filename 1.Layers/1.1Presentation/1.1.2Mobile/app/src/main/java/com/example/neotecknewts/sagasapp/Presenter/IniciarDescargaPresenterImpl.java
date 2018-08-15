package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.IniciarDescargaView;
import com.example.neotecknewts.sagasapp.Interactor.IniciarDescargaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.IniciarDescargaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

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
    public void getMedidores(String token) {
        iniciarDescargaView.showProgress(R.string.message_cargando);
        interactor.getMedidores(token);
    }

    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList) {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.onSuccessGetMedidores(medidorDTOList);
    }

    @Override
    public void getAlmacenes(String token) {
        iniciarDescargaView.showProgress(R.string.message_cargando);
        interactor.getAlmacenes(token);
    }

    @Override
    public void onSuccessGetAlmacenes(List<AlmacenDTO> almacenDTOs) {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.onSuccessGetAlmacen(almacenDTOs);
    }

    @Override
    public void onError() {
        iniciarDescargaView.hideProgress();
        iniciarDescargaView.messageError(R.string.error_conexion);

    }
}
