package com.neotecknewts.sagasapp.Interactor;

import android.content.Context;
import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosClientesDTO;
import com.neotecknewts.sagasapp.Presenter.BuscarClientePresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class BuscarClienteInteractorImpl implements BuscarClienteInteractor {
    BuscarClientePresenter presenter;
    private SAGASSql sagasSql;

    public BuscarClienteInteractorImpl(BuscarClientePresenter presenter, Context context) {
        this.presenter = presenter;
        this.sagasSql = new SAGASSql(context);
    }

    @Override
    public void getClientes(String criterio, String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosClientesDTO> call = restClient.getListaClientes(
                criterio,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);


        call.enqueue(new Callback<DatosClientesDTO>() {
            @Override
            public void onResponse(Call<DatosClientesDTO> call, Response<DatosClientesDTO> response) {
                DatosClientesDTO data = response.body();

                if (response.isSuccessful()) {
                    Log.w("Estatus","Success");
                    sagasSql.InsertClients(data.getList());
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
                    presenter.onError(response.message());

                }

            }

            @Override
            public void onFailure(Call<DatosClientesDTO> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }
}
