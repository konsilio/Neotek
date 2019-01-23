package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.PipaDTO;
import com.example.neotecknewts.sagasapp.Presenter.LecturaPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class LecturaPipaInteractorImpl implements LecturaPipaInteractor {
    private LecturaPipaPresenterImpl lecturaPipaPresenter;

    public LecturaPipaInteractorImpl(LecturaPipaPresenterImpl lecturaPipaPresenter) {
        this.lecturaPipaPresenter = lecturaPipaPresenter;
    }

    /**
     * <h3>getMedidores</h3>
     * Permite obtener del servicio el listado de medidores, se tomara como parametro una cadena
     * de tipo {@link String} que reprecenta el token del usuario, al finalizar en caso de ser
     * exitoso retornara una lista de tipo {@link java.util.ArrayList} que contiene
     * objetos de tipo {@link MedidorDTO} que son los modelos de los medidores
     * @param token Cadena de tipo {@link String} con el token del usuario logeado
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com>
     * @date 02/09/2018
     */
    @Override
    public void getMedidores(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<MedidorDTO>> call = restClient.getMedidores(token);
        Log.w("Url base",ApiClient.BASE_URL);

        call.enqueue(new Callback<List<MedidorDTO>>() {
            @Override
            public void onResponse(Call<List<MedidorDTO>> call, Response<List<MedidorDTO>> response) {
                if (response.isSuccessful()) {
                    List<MedidorDTO> data = response.body();
                    Log.w("Estatus","Success");
                    lecturaPipaPresenter.onSuccessGetMedidores(data);
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
                    lecturaPipaPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<List<MedidorDTO>> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaPipaPresenter.onError();
            }
        });
    }

    /**
     * <h3>getPipas</h3>
     * Permite obtener el listado de las pipas para la lista de toma de lectura, se enviara de
     * parametro una cadena de tipo {@link String} que reprecenta el token del usuario al finalizar,
     * el metodo retornara una lista de tipo {@link java.util.ArrayList} que contiene objetos de
     * tipo {@link PipaDTO} que son las pipas disponibles o asignadas al usuario.
     * @param token Cadena de tipo {@link String} con el token del usuario logeado.
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com>
     * @date 02/09/2018
     */
    @Override
    public void getPipas(String token,boolean esFinalizar) {
        List< PipaDTO> PipaDTOList = new ArrayList<>();

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<DatosTomaLecturaDto> call = restClient.getEstacionesCarburacion(
                false,
                true,
                false,
                esFinalizar,
                token,
                "application/json"
                );
        Log.w("Url base",ApiClient.BASE_URL.toString());

        call.enqueue(new Callback<DatosTomaLecturaDto>() {
            @Override
            public void onResponse(Call<DatosTomaLecturaDto> call, Response<DatosTomaLecturaDto> response) {
                if (response.isSuccessful()) {
                    DatosTomaLecturaDto data = response.body();
                    Log.w("Estatus","Success");
                    lecturaPipaPresenter.onSuccessGetPipas(data);
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
                    Log.w("Error",response.message());
                    lecturaPipaPresenter.onError();
                }

            }

            @Override
            public void onFailure(Call<DatosTomaLecturaDto> call, Throwable t) {
                Log.e("error", "Error desconocido: "+t.toString());
                lecturaPipaPresenter.onError();
            }
        });


    }
}
