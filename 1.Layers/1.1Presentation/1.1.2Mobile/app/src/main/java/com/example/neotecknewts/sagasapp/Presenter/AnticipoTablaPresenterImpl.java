package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.AnticipoTablaView;
import com.example.neotecknewts.sagasapp.Interactor.AnticipoTablaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.AnticipoTablaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuariosCorteDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

import java.util.Date;

public class AnticipoTablaPresenterImpl implements AnticipoTablaPresenter {
    AnticipoTablaView view;
    AnticipoTablaInteractor interactor;
    public AnticipoTablaPresenterImpl(AnticipoTablaView view){
        this.view = view;
        this.interactor = new AnticipoTablaInteractorImpl(this);
    }
    @Override
    public void Anticipo(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.Anticipo(anticiposDTO,sagasSql,token);
    }

    @Override
    public void onSuccess() {
        view.HiddeProgress();
        view.onSuccess();
    }

    @Override
    public void onError(String mensaje) {
        view.HiddeProgress();
        view.onError(mensaje);
    }

    @Override
    public void onSuccessAndroid() {
        view.HiddeProgress();
        view.onSuccessAndroid();
    }

    @Override
    public void onError(Object ob) {
        view.HiddeProgress();
        view.onError(ob);
    }

    @Override
    public void Corte(CorteDTO corteDTO, SAGASSql sagasSql, String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.Corte(corteDTO,sagasSql,token);
    }

    @Override
    public void getAnticipos(String token,int idEstacion,boolean esAnticipos,String fecha) {
        view.onShowProgress(R.string.message_cargando);
        interactor.getAnticipos(token,idEstacion,esAnticipos,fecha);
    }

    @Override
    public void onSuccessList(RespuestaEstacionesVentaDTO data) {
        view.HiddeProgress();
        view.onSuccessList(data);
    }

    /**
     * usuarios
     * Permite invocar el servicio para obtener el listado de usuarios
     * mostrara un dialogo de progress y llama el servicio
     * @param token Token de usuario
     */
    @Override
    public void usuarios(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.usuarios(token);
    }

    /**
     * onSuccessList
     * Metodo para en caso de exito de obtener el listado de usuario para el apartado de
     * anticipos y determinar de quien se cobra dicho anticipo
     * @param data Modelo de tipo {@link UsuariosCorteDTO} en el que se contiene la lista de usuarios
     */
    @Override
    public void onSuccessList(UsuariosCorteDTO data) {
        view.HiddeProgress();
        view.onSuccessList(data);
    }

    /**
     * usuariosCorte
     * Permite obtener el listado de usuarios para el corte, obtendra del serivicio un objeto
     * de tipo {@link UsuariosCorteDTO} con la repsuesta
     * @param token String que reprecenta el token del usuario
     */
    @Override
    public void usuariosCorte(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.usuariosCortes(token);
    }
}
