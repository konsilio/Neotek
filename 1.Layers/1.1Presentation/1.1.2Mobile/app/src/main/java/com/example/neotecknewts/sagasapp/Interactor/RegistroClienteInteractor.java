package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaClienteDTO;

public interface RegistroClienteInteractor {
    void getLista(String token);

    void registrarCliente(ClienteDTO clienteDTO,String token);
}
