package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 07/08/18.
 */

public interface MainPresenter {

    void getEmpresas();
    void doLogin();

    void onSuccessGetEmpresas(ArrayList<EmpresaDTO> empresaDTOs);
    void onSuccessLogin();

}