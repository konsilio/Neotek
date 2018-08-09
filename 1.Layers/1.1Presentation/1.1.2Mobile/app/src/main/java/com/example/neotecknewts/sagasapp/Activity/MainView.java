package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public interface MainView {
    void onSuccessGetEmpresa(List<EmpresaDTO> empresaDTOs);
    void onSuccessLogin();
}
