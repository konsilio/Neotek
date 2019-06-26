package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Activity.PorcentajeCalibracionCilindroView;
import com.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionCilindroInteractor;
import com.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionCilindroInteractorImpl;

public class PorcentajeCalibracionCilindroPresenterImpl implements
        PorcentajeCalibracionCilindroPresenter {
    PorcentajeCalibracionCilindroView view;
    PorcentajeCalibracionCilindroInteractor interactor;
    public PorcentajeCalibracionCilindroPresenterImpl(PorcentajeCalibracionCilindroView view) {
        this.view = view;
        this.interactor = new PorcentajeCalibracionCilindroInteractorImpl(this);
    }
}
