package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface PuntoVentaPagarInteractor {
    void pagar(VentaDTO ventaDTO, String token, boolean esCamioneta, boolean esEstacion,
               boolean esPipa, SAGASSql sagasSql);

    void puntoVentaAsignado(String token);

    void verificarVentaExtraforanea(int idCliente, String token);
}
