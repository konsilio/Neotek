package com.neotecknewts.sagasapp.Interactor;

import com.neotecknewts.sagasapp.Model.ClienteDTO;

public interface RegistroClienteInteractor {
    void getLista(String token);

    void registrarCliente(ClienteDTO clienteDTO, String token);
}
