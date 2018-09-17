package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.ReporteView;
import com.example.neotecknewts.sagasapp.Interactor.ReporteInteractor;
import com.example.neotecknewts.sagasapp.Interactor.ReporteInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.Date;
import java.util.List;

public class ReportePresenterImpl implements ReportePresenter {
    ReporteInteractor interactor;
    ReporteView view;
    public ReportePresenterImpl (ReporteView view){
        this.view = view;
        interactor = new ReporteInteractorImpl(this);
    }

    @Override
    public void GetUnidades(String token) {
        view.onShowProgress(R.string.message_cargando);
        interactor.GetUnidades(token);
    }

    @Override
    public void onSuccessUnidades(List<UnidadesDTO> data) {
        view.hiddeProgress();
        view.onSuccessGetUnidades(data);
    }

    @Override
    public void onError(String mensaje_error) {
        view.hiddeProgress();
        view.onErrorMessage(mensaje_error);
    }

    @Override
    public void Reporte(int idUnidad, Date fecha,String token) {
        view.onShowProgress(R.string.generando_reporte);
        interactor.Reporte(idUnidad,fecha,token);
    }

    @Override
    public void onSuccessReport(Object reporteDTO) {
        view.hiddeProgress();
        view.onSuccessReport(reporteDTO);
    }
}
