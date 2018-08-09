package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.MainView;
import com.example.neotecknewts.sagasapp.Interactor.GeneralInteractor;
import com.example.neotecknewts.sagasapp.Interactor.GeneralInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

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
        interactor.getEmpresasLogin();
    }

    @Override
    public void doLogin(UsuarioLoginDTO usuarioLoginDTO) {
        interactor.postLogin(usuarioLoginDTO);

    }

    @Override
    public void onSuccessGetEmpresas(List<EmpresaDTO> empresaDTOs) {
        mainView.onSuccessGetEmpresa(empresaDTOs);
    }

    @Override
    public void onSuccessLogin(UsuarioDTO usuarioDTO) {
        mainView.onSuccessLogin(usuarioDTO);
    }


}