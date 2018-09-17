package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;
import com.example.neotecknewts.sagasapp.Presenter.ReportePresenterImpl;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ReporteInteractorImpl implements ReporteInteractor {
    ReportePresenterImpl presenter;
    public ReporteInteractorImpl(ReportePresenterImpl reportePresenter) {
        this.presenter = reportePresenter;
    }

    @Override
    public void GetUnidades(String token) {
        String mensaje_error = "";
        List<UnidadesDTO> data = new ArrayList<>();
        presenter.onSuccessUnidades(data);
        presenter.onError(mensaje_error);
    }

    @Override
    public void Reporte(int idUnidad, Date fecha, String token) {
        String mensaje_error = "";
        Object reporteDTO = null;
        presenter.onSuccessReport(reporteDTO);
        presenter.onError(mensaje_error);
    }

}
