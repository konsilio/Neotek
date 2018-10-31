package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.ReporteDto;
import com.example.neotecknewts.sagasapp.Model.UnidadesDTO;
import com.example.neotecknewts.sagasapp.Presenter.ReportePresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.Date;
import java.util.List;

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
        String url  = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<UnidadesDTO>> call = restClient.getUnidades(token,"application/json");

        call.enqueue(new Callback<List<UnidadesDTO>>() {
            @Override
            public void onResponse(Call<List<UnidadesDTO>> call,
                                   Response<List<UnidadesDTO>> response) {
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
            public void onFailure(Call<List<UnidadesDTO>> call, Throwable t) {
                presenter.onError(t.getMessage());
            }
        });

    }

    @Override
    public void Reporte(int idUnidad, Date fecha, String token) {
        String url  = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
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
