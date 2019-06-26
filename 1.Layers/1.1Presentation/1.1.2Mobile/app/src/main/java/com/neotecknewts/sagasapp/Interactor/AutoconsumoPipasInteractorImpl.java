package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.neotecknewts.sagasapp.Presenter.AutoconsumoPipaPresenter;
import com.neotecknewts.sagasapp.Presenter.AutoconsumoPipaPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.neotecknewts.sagasapp.Util.Session;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class AutoconsumoPipasInteractorImpl implements AutoconsumoPipaInteractor {
    AutoconsumoPipaPresenter presenter;
    public AutoconsumoPipasInteractorImpl(AutoconsumoPipaPresenterImpl autoconsumoPipaPresenter) {
        this.presenter = autoconsumoPipaPresenter;
    }

    @Override
    public void getList(Session session, boolean esAutoconsumoPipaFinal) {
        DatosAutoconsumoDTO dto = new DatosAutoconsumoDTO();


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosAutoconsumoDTO> call = restClient.getDatosAutoconsumo(
                false,
                false,
                true,
                esAutoconsumoPipaFinal,
                session.getToken(),
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosAutoconsumoDTO>() {
            @Override
            public void onResponse(Call<DatosAutoconsumoDTO> call, Response<DatosAutoconsumoDTO> response) {
                DatosAutoconsumoDTO data = response.body();
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
            public void onFailure(Call<DatosAutoconsumoDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }
}
