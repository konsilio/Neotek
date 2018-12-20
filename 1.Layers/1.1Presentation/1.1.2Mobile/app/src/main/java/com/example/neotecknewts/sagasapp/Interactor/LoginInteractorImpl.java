package com.example.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RestClient;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;


/**
 * Created by neotecknewts on 02/08/18.
 */

public class LoginInteractorImpl implements LoginInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "LoginInteractor";
    LoginPresenter loginPresenter;

    //constructor de la clase y se inicializa el presenter
    public LoginInteractorImpl(LoginPresenter loginPresenter){
        this.loginPresenter = loginPresenter;
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene todas las empresas para login
    @Override
    public void getEmpresasLogin(){
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<List<EmpresaDTO>> call = restClient.getListEmpresas();
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<List<EmpresaDTO>>() {
            @Override
            public void onResponse(Call<List<EmpresaDTO>> call, Response<List<EmpresaDTO>> response) {
                if (response.isSuccessful()) {
                    List<EmpresaDTO> data = response.body();
                    Log.w(TAG,"Success");
                    loginPresenter.onSuccessGetEmpresas(data);
                }
                else {
                    List<EmpresaDTO>  repon = response.body();
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            loginPresenter.onError(response.message());
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            loginPresenter.onError(response.message());
                            break;
                        default:
                            Log.w(TAG, "desconocido");
                            loginPresenter.onError(response.message());
                            break;
                    }
                    loginPresenter.onError(response.message());
                }

            }

            @Override
            public void onFailure(Call<List<EmpresaDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                loginPresenter.onError(t.toString());
            }
        });
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //hace el login
    @Override
    public void postLogin(UsuarioLoginDTO usuarioLoginDTO) {
        String url = Constantes.BASE_URL;

        Gson gson = new GsonBuilder()
                .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(url)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        RestClient restClient = retrofit.create(RestClient.class);
        Call<UsuarioDTO> call = restClient.postLogin(usuarioLoginDTO,"application/json");
        Log.w(TAG,retrofit.baseUrl().toString());

        call.enqueue(new Callback<UsuarioDTO>() {
            @Override
            public void onResponse(Call<UsuarioDTO> call, Response<UsuarioDTO> response) {
                UsuarioDTO data = response.body();
                if (response.isSuccessful()) {
                    //UsuarioDTO data = response.body();
                    Log.w(TAG,"Sucess");
                    loginPresenter.onSuccessLogin(data);

                }
                else {
                    //UsuarioDTO data = response.body();
                    switch (response.code()) {
                        case 404:
                            Log.w(TAG,"not found");
                            //loginPresenter.onError(data.getMensaje());
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            //loginPresenter.onError(data.getMensaje());
                            break;
                        case 400:
                            Log.w(TAG,"Bad request");
                            break;
                        default:
                            Log.w(TAG, "desconocido: "+response.code());
                            //loginPresenter.onError(data.getMensaje());
                            break;
                    }

                    if(data!=null){
                        loginPresenter.onError(data.getMensaje());
                    }else {
                        JSONObject respuesta = null;
                        try {
                            respuesta = new JSONObject(response.errorBody().string());

                        } catch (JSONException e) {
                            e.printStackTrace();
                        } catch (IOException e) {
                            e.printStackTrace();
                        }
                        if(respuesta!=null){
                            try {
                                Log.w("Error body",respuesta.toString());
                                loginPresenter.onError(respuesta.getString("Mensaje"));

                            } catch (JSONException e) {
                                e.printStackTrace();
                            }

                        }else {
                            loginPresenter.onError(response.message());
                        }
                    }
                }

            }

            @Override
            public void onFailure(Call<UsuarioDTO> call, Throwable t) {
                Log.e("error", t.toString());
                Log.e("error", t.getLocalizedMessage());
                loginPresenter.onError(t.getMessage());
            }
        });
    }
}
