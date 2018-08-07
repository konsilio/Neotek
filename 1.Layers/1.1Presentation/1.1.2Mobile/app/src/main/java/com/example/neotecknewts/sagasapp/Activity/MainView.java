package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 07/08/18.
 */

public interface MainView {
    void onSuccessGetEmpresa(ArrayList<EmpresaDTO> empresaDTOs);
    void onSuccessLogin();
}
