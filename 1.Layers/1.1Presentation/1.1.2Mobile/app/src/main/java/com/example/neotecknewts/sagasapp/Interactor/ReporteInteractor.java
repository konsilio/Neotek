package com.example.neotecknewts.sagasapp.Interactor;

import java.util.Date;

public interface ReporteInteractor {
    void GetUnidades(String token);

    void Reporte(int idUnidad, Date fecha, String token);

}
