package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.AutoconsumoEstacionView;
import com.neotecknewts.sagasapp.Interactor.AutoconsumoEstacionInteractor;
import com.neotecknewts.sagasapp.Interactor.AutoconsumoEstacionInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

public class AutoconsumoEstacionPresenterImpl implements AutoconsumoEstacionPresenter {
    AutoconsumoEstacionView view;
    AutoconsumoEstacionInteractor interactor;
    public AutoconsumoEstacionPresenterImpl(AutoconsumoEstacionView view){
        this.view = view;
        this.interactor = new AutoconsumoEstacionInteractorImpl(this);
    }

    @Override
    public void getList(String token,boolean esFinal) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getList(token,esFinal);
    }

    @Override
    public void onSuccessList(DatosAutoconsumoDTO data) {
        view.onHiddenProgress();
        view.onSuccessLista(data);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddenProgress();
        view.onErrorLista(mensaje);
    }
}
