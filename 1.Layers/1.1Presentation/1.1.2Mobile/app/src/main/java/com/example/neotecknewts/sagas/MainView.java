package com.example.neotecknewts.sagas;

import com.example.neotecknewts.sagas.Model.EmpresaDTO;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 02/08/18.
 */

public interface MainView {

    void onSuccessGetEmpresa(ArrayList<EmpresaDTO> empresaDTOs);
    void onSuccessLogin();
}
