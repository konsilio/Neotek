package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaDatosPresenter {
    void getMedidores(String token);

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();

    void getEstacionesCarburacion(String token, boolean esFinalizar);

    void onSuccessGetEstacionesCarburacion(DatosTomaLecturaDto data);
}
