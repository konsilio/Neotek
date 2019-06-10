package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.AutoconsumoEstacionView;
import com.example.neotecknewts.sagasapp.Interactor.AutoconsumoEstacionInteractor;
import com.example.neotecknewts.sagasapp.Interactor.AutoconsumoEstacionInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.R;

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
