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
    //se delcaran la vista y el interactor
    LoginInteractor interactor;
    MainView mainView;

    //se obtiene la vista al ser contruido y se inicializa el interactor
    public LoginPresenterImpl(MainView view){
        this.mainView = view;
        this.interactor = new LoginInteractorImpl(this);
    }
    //metodo para obtener empresas
    @Override
    public void getEmpresas() {
        mainView.showProgress(R.string.message_cargando);
        interactor.getEmpresasLogin();
    }

    //metodo que hace el login
    @Override
    public void doLogin(UsuarioLoginDTO usuarioLoginDTO) {
        mainView.showProgress(R.string.message_cargando);
        interactor.postLogin(usuarioLoginDTO);

    }

    //metodo que se llama al obtener empresas
    @Override
    public void onSuccessGetEmpresas(List<EmpresaDTO> empresaDTOs) {
        mainView.hideProgress();
        mainView.onSuccessGetEmpresa(empresaDTOs);
    }

    //metodo que se llama hacer el login
    @Override
    public void onSuccessLogin(UsuarioDTO usuarioDTO) {
        mainView.hideProgress();
        mainView.onSuccessLogin(usuarioDTO);
    }

    //metodo de error
    @Override
    public void onError(String mensaje) {
        Log.e("error", "onerror");
        mainView.hideProgress();
        mainView.messageError(mensaje);

    }


}