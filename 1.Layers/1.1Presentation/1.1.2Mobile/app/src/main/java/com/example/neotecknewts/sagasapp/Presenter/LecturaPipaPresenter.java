package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

public interface LecturaPipaPresenter {
    void getMedidores(String token);

    void onSuccessGetMedidores(List<MedidorDTO> data);

    void onError();

    void getPipas(String token,boolean EsFinal);

    void onSuccessGetPipas(List<EstacionCarburacionDTO> data);
}
