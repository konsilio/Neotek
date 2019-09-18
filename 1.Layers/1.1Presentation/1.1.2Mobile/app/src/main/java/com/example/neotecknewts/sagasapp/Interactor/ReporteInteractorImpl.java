package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosReporteDTO;
import com.example.neotecknewts.sagasapp.Model.ReporteDto;
import com.example.neotecknewts.sagasapp.Presenter.ReportePresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

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
                if(response.isSuccessful()){
                    Log.w("Success","Se han cargado ");

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
