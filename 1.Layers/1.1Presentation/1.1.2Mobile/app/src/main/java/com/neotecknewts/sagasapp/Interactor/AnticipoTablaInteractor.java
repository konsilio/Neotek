package com.neotecknewts.sagasapp.Interactor;

import com.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.neotecknewts.sagasapp.Model.CorteDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface AnticipoTablaInteractor {
    void Anticipo(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token);
    boolean VerificarServicio(String token);

    void Corte(CorteDTO corteDTO, SAGASSql sagasSql, String token);

    void getAnticipos(String token, int estacion, boolean esAnticipos, String fecha);

    void usuarios(String token);

    void usuariosCortes(String token);
}
