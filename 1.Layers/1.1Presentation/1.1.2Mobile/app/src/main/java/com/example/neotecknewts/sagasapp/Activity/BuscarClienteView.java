package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;

import java.util.List;

public interface BuscarClienteView {
    void onShowPorgress(int mensaje);
    void onHiddeProgress();
    void onSuccessList(List<ClienteDTO> dtos);
    void onError(String mensaje);

}
