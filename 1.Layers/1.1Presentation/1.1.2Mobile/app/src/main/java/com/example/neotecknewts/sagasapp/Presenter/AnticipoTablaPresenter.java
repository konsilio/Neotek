package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosBusquedaCortesDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuariosCorteDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

import java.util.Date;

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
