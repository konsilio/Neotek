package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.FinalizarDescargaView;
import com.example.neotecknewts.sagasapp.Interactor.FinalizarDescargaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.FinalizarDescargaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.AlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

public class FinalizarDescargaPresenterImpl implements FinalizarDescargaPresenter {
    FinalizarDescargaInteractor interactor;
    FinalizarDescargaView finalizarDescargaView;

    public FinalizarDescargaPresenterImpl(FinalizarDescargaView view){
        this.finalizarDescargaView = view;
        this.interactor = new FinalizarDescargaInteractorImpl(this);
    }
    @Override
    public void getOrdenesCompra(int IdEmpresa, String token) {
        finalizarDescargaView.showProgress(R.string.message_cargando);
        interactor.getOrdenesCompra(IdEmpresa,token);
    }

    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.onSuccessGetOrdenesCompra(respuestaOrdenesCompraDTO);

    }

    @Override
    public void getMedidores(String token) {
        finalizarDescargaView.showProgress(R.string.message_cargando);
        interactor.getMedidores(token);
    }

    @Override
    public void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.onSuccessGetMedidores(medidorDTOList);
    }

    @Override
    public void getAlmacenes(String token) {
        finalizarDescargaView.showProgress(R.string.message_cargando);
        interactor.getAlmacenes(token);
    }

    @Override
    public void onSuccessGetAlmacenes(List<AlmacenDTO> almacenDTOs) {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.onSuccessGetAlmacen(almacenDTOs);
    }

    @Override
    public void onError() {
        finalizarDescargaView.hideProgress();
        finalizarDescargaView.messageError(R.string.error_conexion);

    }
}
