package com.example.neotecknewts.sagasapp.Interactor;

import java.util.Date;

public interface ReporteInteractor {
    void GetUnidades(String token);

    void Reporte(int idUnidad, String fecha, String token);

}
