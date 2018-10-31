package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.ReporteDto;
import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;

import java.util.Date;
import java.util.List;

public interface ReportePresenter {
    void GetUnidades(String token);

    void onSuccessUnidades(List<UnidadesDTO> data) ;

    void onError(String mensaje_error);

    void Reporte(int id, Date fecha,String token);

    void onSuccessReport(ReporteDto reporteDTO);
}
