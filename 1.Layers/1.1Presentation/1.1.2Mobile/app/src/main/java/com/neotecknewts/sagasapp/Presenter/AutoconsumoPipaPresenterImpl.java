package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.AutoconsumoPipaView;
import com.neotecknewts.sagasapp.Interactor.AutoconsumoPipaInteractor;
import com.neotecknewts.sagasapp.Interactor.AutoconsumoPipasInteractorImpl;
import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.neotecknewts.sagasapp.Util.Session;

public class AutoconsumoPipaPresenterImpl implements AutoconsumoPipaPresenter {
    AutoconsumoPipaView view;
    AutoconsumoPipaInteractor interactor;
    public AutoconsumoPipaPresenterImpl(AutoconsumoPipaView view) {
        this.view = view;
        this.interactor = new AutoconsumoPipasInteractorImpl(this);
    }

    @Override
    public void getList(Session session, boolean esAutoconsumoPipaFinal) {
        view.onShowprogress(R.string.message_cargando);
        interactor.getList(session,esAutoconsumoPipaFinal);
    }

    @Override
    public void onSuccess(DatosAutoconsumoDTO dto) {
        view.onHiddeprogress();
        view.onSuccessList(dto);
    }

    @Override
    public void onError(String mensaje) {
        view.onHiddeprogress();
        view.onError(mensaje);
    }
}
