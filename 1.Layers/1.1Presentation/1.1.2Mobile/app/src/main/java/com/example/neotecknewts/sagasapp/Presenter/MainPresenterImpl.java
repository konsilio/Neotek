package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.MainView;
import com.example.neotecknewts.sagasapp.Interactor.GeneralInteractor;
import com.example.neotecknewts.sagasapp.Interactor.GeneralInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */
public class MainPresenterImpl implements MainPresenter {

    GeneralInteractor interactor;
    MainView mainView;

    public MainPresenterImpl(MainView view){
        this.mainView = view;
        this.interactor = new GeneralInteractorImpl(this);
    }
    @Override
    public void getEmpresas() {
        interactor.loadJSON();
    }

    @Override
    public void doLogin() {
        interactor.webServiceCall();

    }

    @Override
    public void onSuccessGetEmpresas(List<EmpresaDTO> empresaDTOs) {
        mainView.onSuccessGetEmpresa(empresaDTOs);
    }

    @Override
    public void onSuccessLogin() {
        mainView.onSuccessLogin();
    }


}