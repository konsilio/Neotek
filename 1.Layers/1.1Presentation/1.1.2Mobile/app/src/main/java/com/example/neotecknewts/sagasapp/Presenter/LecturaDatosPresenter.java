package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaDatosPresenter {
    void getMedidores(String token);

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();

    void getEstacionesCarburacion(String token,boolean esFinalizar);

    void onSuccessGetEstacionesCarburacion(List<EstacionCarburacionDTO> data);
}
