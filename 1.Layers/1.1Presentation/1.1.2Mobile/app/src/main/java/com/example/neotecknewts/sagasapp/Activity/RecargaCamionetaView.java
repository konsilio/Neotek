package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;

import java.util.List;

public interface RecargaCamionetaView {
    void ValidarForm();
    void GoBackWindow();

    void showProgres();

    void hideProgress();

    void onSuccessCamionetas(List<EstacionCarburacionDTO> data);

    void onError();
}
