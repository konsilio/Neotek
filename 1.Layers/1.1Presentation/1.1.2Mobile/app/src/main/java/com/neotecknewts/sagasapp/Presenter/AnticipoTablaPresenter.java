package com.neotecknewts.sagasapp.Presenter;

import com.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.neotecknewts.sagasapp.Model.CorteDTO;
import com.neotecknewts.sagasapp.Model.DatosBusquedaCortesDTO;
import com.neotecknewts.sagasapp.Model.UsuariosCorteDTO;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface AnticipoTablaPresenter {
    void Anticipo(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token);

    void onSuccess();

    void onError(String s);

    void onSuccessAndroid();

    void onError(DatosBusquedaCortesDTO ob);

    void Corte(CorteDTO corteDTO, SAGASSql sagasSql, String token);

    void getAnticipos(String token, int IdEstacion, boolean esAnticipos, String fecha);

    //void onSuccessList(RespuestaEstacionesVentaDTO data);
    void onSuccessList(DatosBusquedaCortesDTO data);

    void usuarios(String token);

    void onSuccessList(UsuariosCorteDTO data);

    void usuariosCorte(String token);
}
