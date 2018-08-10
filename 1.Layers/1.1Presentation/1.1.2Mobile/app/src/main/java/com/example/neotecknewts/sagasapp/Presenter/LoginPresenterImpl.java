package com.example.neotecknewts.sagasapp.Presenter;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Activity.MainView;
import com.example.neotecknewts.sagasapp.Interactor.LoginInteractor;
import com.example.neotecknewts.sagasapp.Interactor.LoginInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */
public class LoginPresenterImpl implements LoginPresenter {

    LoginInteractor interactor;
    MainView mainView;

    public LoginPresenterImpl(MainView view){
        this.mainView = view;
        this.interactor = new LoginInteractorImpl(this);
    }
    @Override
    public void getEmpresas() {
        mainView.showProgress(R.string.message_cargando);
        interactor.getEmpresasLogin();
    }

    @Override
    public void doLogin(UsuarioLoginDTO usuarioLoginDTO) {
        mainView.showProgress(R.string.message_cargando);
        interactor.postLogin(usuarioLoginDTO);

    }


    @Override
    public void onSuccessGetEmpresas(List<EmpresaDTO> empresaDTOs) {
        mainView.hideProgress();
        mainView.onSuccessGetEmpresa(empresaDTOs);
    }

    @Override
    public void onSuccessLogin(UsuarioDTO usuarioDTO) {
        mainView.hideProgress();
        mainView.onSuccessLogin(usuarioDTO);
    }

    @Override
    public void onError() {
        Log.e("error", "onerror");
        mainView.hideProgress();
        mainView.messageError(R.string.error_conexion);
    }


}