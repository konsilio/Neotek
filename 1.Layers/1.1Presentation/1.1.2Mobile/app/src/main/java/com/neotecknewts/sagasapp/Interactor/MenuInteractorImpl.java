package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.MenuDTO;
import com.neotecknewts.sagasapp.Presenter.MenuPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import java.io.IOException;
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
                    menuPresenter.onSuccessGetMenu(data);
                }
                else {
                    switch (response.code()) {
                        case 401:
                            menuPresenter.onError("Inicia sesión para continuar", false);
                            break;
                        case 500:
                            menuPresenter.onError("Error interno del servidor", false);
                            break;
                        default:
                            menuPresenter.onError("Ocurrió un error", true);
                            break;
                    }
                }

            }

            @Override
            public void onFailure(Call<List<MenuDTO>> call, Throwable t) {
                menuPresenter.onError("Sin conexión a internet", true);
            }
        });
    }
}
