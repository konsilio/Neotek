package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.neotecknewts.sagasapp.Model.ReporteDto;
import com.neotecknewts.sagasapp.Presenter.ReportePresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ReporteInteractorImpl implements ReporteInteractor {
    ReportePresenterImpl presenter;
    public ReporteInteractorImpl(ReportePresenterImpl reportePresenter) {
        this.presenter = reportePresenter;
    }

    @Override
    public void GetUnidades(String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosReporteDTO> call = restClient.getUnidades(token,"application/json");

        call.enqueue(new Callback<DatosReporteDTO>() {
            @Override
            public void onResponse(Call<DatosReporteDTO> call,
                                   Response<DatosReporteDTO> response) {
                if(response.isSuccessful()){
                    Log.w("Success","Se han cargado ");
                    presenter.onSuccessUnidades(response.body());

                }else{
                    switch (response.code()) {
                        case 404:
                            Log.w("Error","not found");


                            break;
                        case 500:
                            Log.w("Error", "server broken");

                            break;
                        default:
                            Log.w("Error", "Error desconocido: "+response.code());

                            break;
                    }
                    presenter.onError("Error no. "+response.code()+":"+response.message());

                }
            }

            @Override
            public void onFailure(Call<DatosReporteDTO> call, Throwable t) {
                presenter.onError(t.getMessage());
            }
        });

    }

    @Override
    public void Reporte(int idUnidad, String fecha, String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<ReporteDto> call = restClient.getReporte(
                idUnidad,
                fecha,
                token,
                "application/json"

        );
        call.enqueue(new Callback<ReporteDto>() {
            @Override
            public void onResponse(Call<ReporteDto> call,
                                   Response<ReporteDto> response) {
                ReporteDto reporteDTO  = response.body();

                Log.w("responsebody",response.body().toString()+"");
                Log.w("response",response.code()+"");
                if(response.isSuccessful()){

                    Log.w("Success","Se han cargado ");
                    reporteDTO.setExito(true);
                    presenter.onSuccessReport(reporteDTO);
                }else{
                    switch (response.code()) {
                        case 404:
                            Log.w("Error","not found");

                            break;
                        case 500:
                            Log.w("Error", "server broken");

                            break;
                        default:
                            Log.w("Error", "Error desconocido: "+response.code());

                            break;
                    }
                    if(reporteDTO!=null)
                        presenter.onError(reporteDTO);
                    else
                        presenter.onError("Error no. "+response.code()+":"+response.message());
                }

            }


            @Override
            public void onFailure(Call<ReporteDto> call, Throwable t) {
                presenter.onError(t.getMessage());
            }
        });

    }

}
