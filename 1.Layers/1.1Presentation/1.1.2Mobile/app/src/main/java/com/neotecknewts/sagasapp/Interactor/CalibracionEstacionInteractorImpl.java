package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosCalibracionDTO;
import com.neotecknewts.sagasapp.Presenter.CalibracionEstacionPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CalibracionEstacionInteractorImpl implements CalibracionEstacionInteractor {
    CalibracionEstacionPresenter presenter;
    public CalibracionEstacionInteractorImpl(CalibracionEstacionPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getList(String token, boolean esCalibracionEstacionFinal) {
        

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosCalibracionDTO> call = restClient.getDatosCalibracion(
                true,
                false,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosCalibracionDTO>() {
            @Override
            public void onResponse(Call<DatosCalibracionDTO> call, Response<DatosCalibracionDTO> response) {
                DatosCalibracionDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    presenter.onSuccess(data);
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
                    if(data!= null){
                        presenter.onError(data.getMensajesError());
                    }else{
                        presenter.onError(response.message());
                    }
                }

            }

            @Override
            public void onFailure(Call<DatosCalibracionDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }
}
