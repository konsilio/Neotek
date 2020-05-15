package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosClientesDTO;
import com.neotecknewts.sagasapp.Model.DatosVentasDTO;
import com.neotecknewts.sagasapp.Presenter.BuscarTicketPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class BuscarTicketInteractorImpl implements BuscarTicketInteractor {

    BuscarTicketPresenter presenter;

    public BuscarTicketInteractorImpl(BuscarTicketPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getTickets(String token) {
        Log.d("FerChido",token);
        RestClient restClient = ApiClient.getClient().create(RestClient.class);

        Call<DatosVentasDTO> call = restClient.getTickets(token, "application/json");
        call.enqueue(new Callback<DatosVentasDTO>() {
            @Override
            public void onResponse(Call<DatosVentasDTO> call, Response<DatosVentasDTO> response) {
                if (response.isSuccessful()) {
                    DatosVentasDTO data = response.body();
                    presenter.onSuccess(data);
                } else {
                    Log.d("FerChido","response code: " + response.code());
                    if (response.message() == null) {
                        presenter.onError("Error del servidor");
                    } else {
                        presenter.onError(response.message());
                    }
                }
            }

            @Override
            public void onFailure(Call<DatosVentasDTO> call, Throwable t) {
                Log.d("FerChido", "Error desconocido: "+t.toString());
                presenter.onError(t.getLocalizedMessage());
            }
        });
    }
}
