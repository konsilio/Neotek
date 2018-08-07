package com.example.neotecknewts.sagas.Presenter;

import com.example.neotecknewts.sagas.Model.EmpresaDTO;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 02/08/18.
 */

public interface MainPresenter {

    void getEmpresas();
    void doLogin();

    void onSuccessGetEmpresas(ArrayList<EmpresaDTO> empresaDTOs);
    void onSuccessLogin();

}
