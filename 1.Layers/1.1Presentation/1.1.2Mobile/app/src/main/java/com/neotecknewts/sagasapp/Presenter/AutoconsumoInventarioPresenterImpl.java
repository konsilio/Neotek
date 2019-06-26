package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.AutoconsumoInventarioView;
import com.neotecknewts.sagasapp.Interactor.AutoconsumoInventarioInteractor;
import com.neotecknewts.sagasapp.Interactor.AutoconsumoInventarioInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

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
