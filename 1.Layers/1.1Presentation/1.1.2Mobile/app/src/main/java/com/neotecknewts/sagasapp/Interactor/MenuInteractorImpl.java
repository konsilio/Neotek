package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.MenuDTO;
import com.neotecknewts.sagasapp.Presenter.MenuPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class MenuInteractorImpl implements MenuInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "MenuInteractor";
    MenuPresenter menuPresenter;

    //constructor de la clase y se inicializa el presenter
    public MenuInteractorImpl(MenuPresenter menuPresenter){
        this.menuPresenter = menuPresenter;
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene la lista de menu
    @Override
    public void getMenu(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<MenuDTO>> call = restClient.getMenu(token);
        Log.w(TAG, ApiClient.BASE_URL);

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
