package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.MenuPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class MenuInteractorImpl implements MenuInteractor {
    public static final String TAG = "MenuInteractor";
    MenuPresenter menuPresenter;

    public MenuInteractorImpl(MenuPresenter menuPresenter){
        this.menuPresenter = menuPresenter;
    }

    @Override
    public void getMenu(String token) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<MenuDTO>> call = restClient.getMenu(token);
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<MenuDTO>>() {
            @Override
            public void onResponse(Call<List<MenuDTO>> call, Response<List<MenuDTO>> response) {
                if (response.isSuccessful()) {
                    List<MenuDTO> data = response.body();
                    Log.w(TAG,"Success");
                    menuPresenter.onSuccessGetMenu(data);
                }
                else {
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            menuPresenter.onError();
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            menuPresenter.onError();
                            break;
                        default:
                            Log.w(TAG, ""+response.code());
                            menuPresenter.onError();
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<MenuDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                menuPresenter.onError();
            }
        });
    }
}
