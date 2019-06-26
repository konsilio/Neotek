package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.neotecknewts.sagasapp.Presenter.RecargaEstacionCarburacionPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RecargaEstacionCarburacionInteractorImpl
        implements RecargaEstacionCarburacionInteractor {
    RecargaEstacionCarburacionPresenter presenter;
    public RecargaEstacionCarburacionInteractorImpl(
            RecargaEstacionCarburacionPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void getLista(String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosRecargaDto> call = restClient.getCatalogsRecarga(
                true,
                false,
                false,
                /*false,*/
                token,
                "application/json"
        );

        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<DatosRecargaDto>() {
            @Override
            public void onResponse(Call<DatosRecargaDto> call, Response<DatosRecargaDto> response) {
                if(response.isSuccessful()){
                    presenter.onSuccesslista(response.body());
                }else{
                    String mensaje = "";
                    switch (response.code()){
                        case 404:
                            mensaje = "Error "+String.valueOf(response.code())+
                                    "\n Se ha generado un error al enviar la solicitud.";
                            Log.e("Error "+String.valueOf(response.code()),
                                    response.message());
                            break;
                        case 500:
                            mensaje = "Error "+String.valueOf(response.code())+
                                    "\n Se ha generado un error al cargar la lista, el servidor " +
                                    "no esta disponible, verifique su conexión";
                            Log.e("Error "+String.valueOf(response.code()),
                                    response.message());
                            break;
                        case 401:
                            mensaje = "Error "+String.valueOf(response.code())+
                                    "\n Se ha generado un error inesperado, " +
                                    "intente nuevamente mas tarde";
                            Log.e("Error "+String.valueOf(response.code()),
                                    response.message());
                            break;
                            default:
                                mensaje = "Error "+String.valueOf(response.code())+
                                        "\n Se ha generado un error inesperado, " +
                                        "intente nuevamente mas tarde";
                                Log.e("Error "+String.valueOf(response.code()),
                                        response.message());
                                break;

                    }
                    presenter.onError(mensaje);
                }
            }

            @Override
            public void onFailure(Call<DatosRecargaDto> call, Throwable t) {
                Log.e("Error",t.getLocalizedMessage()+"\n"+t.getMessage() +
                        "\n"+ t.getCause());
                presenter.onError("Se ha generado un error inesperado, intente nuevamente");
            }
        });
    }
}
