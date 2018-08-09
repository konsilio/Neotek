package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

/**
 * Created by neotecknewts on 02/08/18.
 */

public interface GeneralInteractor {
    void getEmpresasLogin();
    void postLogin(UsuarioLoginDTO usuarioLoginDTO);

}
