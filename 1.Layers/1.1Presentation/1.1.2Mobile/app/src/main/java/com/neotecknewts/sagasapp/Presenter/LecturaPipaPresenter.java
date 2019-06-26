package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaPipaPresenter {
    void getMedidores(String token);

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();

    void getPipas(String token, boolean EsFinal);

    void onSuccessGetPipas(DatosTomaLecturaDto data);
}
