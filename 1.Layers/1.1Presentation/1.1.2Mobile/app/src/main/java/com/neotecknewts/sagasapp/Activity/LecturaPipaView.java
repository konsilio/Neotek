package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.ArrayList;
import java.util.List;

public interface LecturaPipaView {
    void ValidateForm();
    void DialogoCamposVacios(ArrayList<String> mensaje);
    void DialogoRetornar();
    void showLoadingProgress(int message_cargando);
    void hiddeLoadingProgress();
    void onSuccessMedidores(List<MedidorDTO> data);
    void ErrorMedidores();

    void onError();

    void onSuccessPipas(DatosTomaLecturaDto data);
}
