package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;

import java.util.List;

public interface LecturaCamionetaPresenter {

    void GetListCamionetas(String token);

    void onError();

    void onSuccessCamionetas(List<EstacionCarburacionDTO> data);
}
