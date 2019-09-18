package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.PorcentajeCalibracionCilindroView;
import com.example.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionCilindroInteractor;
import com.example.neotecknewts.sagasapp.Interactor.PorcentajeCalibracionCilindroInteractorImpl;

public class PorcentajeCalibracionCilindroPresenterImpl implements
        PorcentajeCalibracionCilindroPresenter {
    PorcentajeCalibracionCilindroView view;
    PorcentajeCalibracionCilindroInteractor interactor;
    public PorcentajeCalibracionCilindroPresenterImpl(PorcentajeCalibracionCilindroView view) {
        this.view = view;
        this.interactor = new PorcentajeCalibracionCilindroInteractorImpl(this);
    }
}
