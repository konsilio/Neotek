package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.neotecknewts.sagasapp.Model.MedidorDTO;
import com.neotecknewts.sagasapp.Presenter.LecturaAlmacenPresenter;
import com.neotecknewts.sagasapp.Presenter.LecturaAlmacenPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LecturaAlmacenInteractorImpl implements LecturaAlmacenInteractor {
    public LecturaAlmacenPresenter lecturaAlmacenPresenter;
    public LecturaAlmacenInteractorImpl(LecturaAlmacenPresenterImpl lecturaAlmacenPresenter) {
        this.lecturaAlmacenPresenter = lecturaAlmacenPresenter;
    }

    @Override
    public void getMedidores(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<MedidorDTO>> call = restClient.getMedidores(token);
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<List<MedidorDTO>>() {
            @Override
            public void onResponse(Call<List<MedidorDTO>> call, Response<List<MedidorDTO>> response) {
                if (response.isSuccessful()) {
                    List<MedidorDTO> data = response.body();
                    Log.w("Estatus","Success");
                    lecturaAlmacenPresenter.onSuccessGetMedidores(data);
                }
                else {
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
                    lecturaAlmacenPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaAlmacenPresenter.onError();
            }
        });

    }

    @Override
    public void getAlmacenes(String token,boolean esFinalizar ) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosTomaLecturaDto> call = restClient.getEstacionesCarburacion(
                false,
                false,
                false,
                esFinalizar,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosTomaLecturaDto>() {
            @Override
            public void onResponse(Call<DatosTomaLecturaDto> call, Response<DatosTomaLecturaDto> response) {
                if (response.isSuccessful()) {
                    DatosTomaLecturaDto data = response.body();
                    Log.w("Estatus","Success");
                    lecturaAlmacenPresenter.onSuccessGetAlmacen(data);
                }
                else {
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
                    lecturaAlmacenPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<DatosTomaLecturaDto> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaAlmacenPresenter.onError();
            }
        });
    }
}
