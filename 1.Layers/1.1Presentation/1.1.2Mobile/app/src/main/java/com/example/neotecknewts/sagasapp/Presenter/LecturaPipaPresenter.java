package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.PipaDTO;

import java.util.List;

public interface LecturaPipaPresenter {
    void getMedidores(String token);

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();

    void getPipas(String token);

    void onSuccessGetPipas(List<PipaDTO> pipaDTOList);
}
