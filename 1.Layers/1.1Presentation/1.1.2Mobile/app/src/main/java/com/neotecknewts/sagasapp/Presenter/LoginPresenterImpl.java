package com.neotecknewts.sagasapp.Presenter;

import android.util.Log;

import com.neotecknewts.sagasapp.Activity.MenuView;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.MainView;
import com.neotecknewts.sagasapp.Interactor.LoginInteractor;
import com.neotecknewts.sagasapp.Interactor.LoginInteractorImpl;
import com.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */
public class LoginPresenterImpl implements LoginPresenter {
    //se delcaran la vista y el interactor
    LoginInteractor interactor;
    MainView mainView;
    MenuView menuView;

    //se obtiene la vista al ser contruido y se inicializa el interactor
    public LoginPresenterImpl(MainView view, SAGASSql sagasSql){
        this.mainView = view;
        this.interactor = new LoginInteractorImpl(this, sagasSql);
    }
    public LoginPresenterImpl(MenuView view, SAGASSql sagasSql){
        this.menuView = view;
        this.interactor = new LoginInteractorImpl(this, sagasSql);
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

    @Override
    public void doRegistrar(UsuarioLoginDTO usuarioLoginDTO, String token) {
        // menuView.showProgress(R.string.message_cargando);
        interactor.postRegistrar(usuarioLoginDTO, token);
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
        mainView.hideProgress();
        mainView.messageError(mensaje);
    }
}