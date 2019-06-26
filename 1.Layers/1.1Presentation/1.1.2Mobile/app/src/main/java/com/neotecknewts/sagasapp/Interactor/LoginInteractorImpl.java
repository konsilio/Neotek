package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;


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

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<EmpresaDTO>> call = restClient.getListEmpresas();

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

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<UsuarioDTO> call = restClient.postLogin(usuarioLoginDTO,"application/json");
        Log.w(TAG, ApiClient.BASE_URL.toString());

        call.enqueue(new Callback<UsuarioDTO>() {
            @Override
            public void onResponse(Call<UsuarioDTO> call, Response<UsuarioDTO> response) {
                try{
                    UsuarioDTO data = response.body();
                    //Log.d("Jimy", response.code()+"");
                    //Log.d("Jimy", response.isSuccessful()+"");
                    //loginPresenter.onSuccessLogin(UsuarioDTO);

                    if (response.isSuccessful()) {
                        //Log.d("Jimy", data.toString());
                        //Log.w(TAG,"Sucess");
                        if(data.getIdUsuario()!= 0){
                            if(data.getLengthListMenu()  > 0){
                                // Debería de guardar en bd
                                loginPresenter.onSuccessLogin(data);

                            } else {
                                loginPresenter.onError(data.getMensaje());
                            }
                        }else {
                            loginPresenter.onError(data.getMensaje());
                        }
                    }
                    else {
                        //UsuarioDTO data = response.body();
                        Log.w(TAG,response.errorBody().string());

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
                }catch(Exception error) {
                    loginPresenter.onError("Usuario y/o contraseña incorrectos - try");
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
