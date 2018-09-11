package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaAlmacenPresenter {
    void getMedidores(String token);

    void hiddeProgress();

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();
}
