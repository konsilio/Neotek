package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaDatosPresenter {
    void getMedidores(String token);

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();
}
