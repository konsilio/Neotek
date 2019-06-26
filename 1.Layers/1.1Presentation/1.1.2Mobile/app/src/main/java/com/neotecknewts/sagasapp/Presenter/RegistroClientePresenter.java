package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.ClienteDTO;
import com.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.neotecknewts.sagasapp.Model.RespuestaClienteDTO;

public interface RegistroClientePresenter {
    void getLista(String token);

    void onSuccess(DatosTipoPersonaDTO data);

    void onError(DatosTipoPersonaDTO data);

    void onError(String message);

    void registrarCliente(ClienteDTO clienteDTO, String token);

    void onSuccessRegistro(RespuestaClienteDTO data);

    void onErrorRegistro(RespuestaClienteDTO data);
}
