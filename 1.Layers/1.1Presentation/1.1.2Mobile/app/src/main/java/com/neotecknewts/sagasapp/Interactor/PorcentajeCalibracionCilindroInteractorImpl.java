package com.neotecknewts.sagasapp.Interactor;

import com.neotecknewts.sagasapp.Presenter.PorcentajeCalibracionCilindroPresenter;

public class PorcentajeCalibracionCilindroInteractorImpl implements PorcentajeCalibracionCilindroInteractor {
    PorcentajeCalibracionCilindroPresenter presenter;
    public PorcentajeCalibracionCilindroInteractorImpl(
            PorcentajeCalibracionCilindroPresenter presenter) {
        this.presenter = presenter;
    }
}
