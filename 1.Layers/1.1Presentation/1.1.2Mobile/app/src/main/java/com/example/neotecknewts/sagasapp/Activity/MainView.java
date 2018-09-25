package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;

import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */
//interfaz que indica los metodos relacionados con la vista que el interactor usara
public interface MainView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(String mensaje);
    void onSuccessGetEmpresa(List<EmpresaDTO> empresaDTOs);
    void onSuccessLogin(UsuarioDTO usuarioDTO);
}
