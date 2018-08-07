package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.MainView;
import com.example.neotecknewts.sagasapp.Interactor.GeneralInteractor;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 07/08/18.
 */
public class MainPresenterImpl implements MainPresenter {

    GeneralInteractor interactor;
    MainView mainView;

    public MainPresenterImpl(MainView view){
        this.mainView = view;
    }
    @Override
    public void getEmpresas() {
        interactor.webServiceCall();
    }

    @Override
    public void doLogin() {

    }

    @Override
    public void onSuccessGetEmpresas(ArrayList<EmpresaDTO> empresaDTOs) {
        mainView.onSuccessGetEmpresa(empresaDTOs);
    }

    @Override
    public void onSuccessLogin() {
        mainView.onSuccessLogin();
    }


}