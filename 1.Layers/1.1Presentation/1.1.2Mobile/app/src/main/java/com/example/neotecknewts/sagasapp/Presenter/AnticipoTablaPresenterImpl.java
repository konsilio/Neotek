package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.AnticipoTablaView;
import com.example.neotecknewts.sagasapp.Interactor.AnticipoTablaInteractor;
import com.example.neotecknewts.sagasapp.Interactor.AnticipoTablaInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
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
}
