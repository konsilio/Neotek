package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaAlmacenPresenter {
    void getMedidores(String token);

    void hiddeProgress();

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();

    void getAlmacenes(String token,boolean esFinalizar);

    void onSuccessGetAlmacen(DatosTomaLecturaDto data);
}
