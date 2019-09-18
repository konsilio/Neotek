package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.AutoconsumoInventarioView;
import com.example.neotecknewts.sagasapp.Interactor.AutoconsumoInventarioInteractor;
import com.example.neotecknewts.sagasapp.Interactor.AutoconsumoInventarioInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.R;

public class AutoconsumoInventarioPresenterImpl implements AutoconsumoInventarioPresenter {
    AutoconsumoInventarioView view;
    AutoconsumoInventarioInteractor interactor;
    public AutoconsumoInventarioPresenterImpl(AutoconsumoInventarioView view) {
        this.view = view;
        this.interactor = new AutoconsumoInventarioInteractorImpl(this);
    }

    @Override
    public void getList(String token, boolean esAutoconsumoInventarioFinal) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getList(token,esAutoconsumoInventarioFinal);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddenProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccess(DatosAutoconsumoDTO dto) {
        view.onHiddenProgress();
        view.onSuccessLista(dto);
    }
}
