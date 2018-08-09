package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public interface MainPresenter {

    void getEmpresas();
    void doLogin(UsuarioLoginDTO usuarioLoginDTO);

    void onSuccessGetEmpresas(List<EmpresaDTO> empresaDTOs);
    void onSuccessLogin(UsuarioDTO usuarioDTO);

}