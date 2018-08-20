package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

//interfaz que define los metodos que hacen llamada a web service
public interface FinalizarDescargaInteractor {
    void getOrdenesCompra(int IdEmpresa, String token);
    void getMedidores(String token);
    void getAlmacenes(String token);
}
