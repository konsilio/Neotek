package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface AnticipoTablaInteractor {
    void Anticipo(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token);
    boolean VerificarServicio(String token);

    void Corte(CorteDTO corteDTO, SAGASSql sagasSql, String token);
}
