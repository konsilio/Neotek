package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

/**
 * Created by neotecknewts on 02/08/18.
 */
//interfaz que define los metodos que hacen llamada a web service
public interface LoginInteractor {
    void getEmpresasLogin();
    void postLogin(UsuarioLoginDTO usuarioLoginDTO);

}
