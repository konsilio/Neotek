package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.ArrayList;
import java.util.List;

public interface LecturaDatosView {
    void ValidateForm();
    void DialogoCamposVacios(ArrayList<String> mensaje);
    void DialogoNoRetornar();

    void showLoadingProgress(int message_cargando);

    void hiddeLoadingProgress();

    void onSuccessMedidores(List<MedidorDTO> data);

    void ErrorMedidores();

    void onSuccessEstacionesCarburacion(DatosTomaLecturaDto data);
}
