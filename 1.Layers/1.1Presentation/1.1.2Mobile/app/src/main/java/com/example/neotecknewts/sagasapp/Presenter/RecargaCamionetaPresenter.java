package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;

import java.util.List;

public interface RecargaCamionetaPresenter {
    void getCamionetas(String token);

    void onSuccessCamionetas(List<EstacionCarburacionDTO> data);

    void onError();
}
