package com.neotecknewts.sagasapp.Interactor;

import android.database.Cursor;
import android.provider.ContactsContract;
import android.support.v7.view.menu.ListMenuPresenter;
import android.support.v7.view.menu.MenuView;
import android.util.Log;

import com.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.neotecknewts.sagasapp.Model.MenuDTO;
import com.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.neotecknewts.sagasapp.Presenter.MenuPresenterImpl;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;

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
    private SAGASSql sagasSql;
    private String token;

    //constructor de la clase y se inicializa el presenter
    public LoginInteractorImpl(LoginPresenter loginPresenter, SAGASSql sagasSql) {
        this.loginPresenter = loginPresenter;
        this.sagasSql = sagasSql;
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //obtiene todas las empresas para login
    @Override
    public void getEmpresasLogin() {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<List<EmpresaDTO>> call = restClient.getListEmpresas();

        call.enqueue(new Callback<List<EmpresaDTO>>() {
            @Override
            public void onResponse(Call<List<EmpresaDTO>> call, Response<List<EmpresaDTO>> response) {
                if (response.isSuccessful()) {
                    Log.d("entro mensaje:", "Si entró");
                    List<EmpresaDTO> data = response.body();
                    Log.w(TAG, "Success");
                    loginPresenter.onSuccessGetEmpresas(data);
                    Log.d("responsebody1", response.code() + "");
                } else {
                    List<EmpresaDTO> repon = response.body();
                    Log.d("responsebody2", response.code() + "");
                    switch (response.code()) {
                        case 400:
                            Log.w(TAG, "sin lectura");
                            loginPresenter.onError("es probable");
                            Log.d("messageerror", response.code() + "");
                            break;
                        case 404:
                            Log.w(TAG, "not found");
                            loginPresenter.onError(response.message());
                            Log.d("messageerror", response.code() + "");
                            break;
                        case 500:
                            Log.w(TAG, "server broken");
                            loginPresenter.onError(response.message());
                            Log.d("messageerror", response.code() + "");
                            break;
                        default:
                            Log.w(TAG, "desconocido");
                            loginPresenter.onError(response.message());
                            Log.d("messageerrordef", response.message());
                            break;
                    }
                    loginPresenter.onError(response.message());
                    Log.d("messageerror", response.code() + "");
                }

                Log.d("responsebody", response.code() + "");
            }

            @Override
            public void onFailure(Call<List<EmpresaDTO>> call, Throwable t) {
                Log.e("error", t.toString());
                loginPresenter.onError(t.toString());
            }
        });
    }

    public LoginInteractorImpl(SAGASSql sagasSql, String token) {
        this.sagasSql = sagasSql;
        this.token = token;
    }

    //funcion que hace el llamado al web service por el metodo indicado en la interfaz de restclient y con los parametros indicados
    //hace el login

    @Override
    public void postLogin(UsuarioLoginDTO usuarioLoginDTO) {
        Log.d("entro mensaje:", "Si entró");
        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<UsuarioDTO> call = restClient.postLogin(usuarioLoginDTO, "application/json");
        Log.w(TAG, ApiClient.BASE_URL.toString());

        call.enqueue(new Callback<UsuarioDTO>() {
            @Override
            public void onResponse(Call<UsuarioDTO> call, Response<UsuarioDTO> response) {
                try {
                    UsuarioDTO data = response.body();
                    Log.d("jimmycode", response.code() + "");
                    Log.d(".", response.isSuccessful() + "");
                    //loginPresenter.onSuccessLogin(UsuarioDTO);
                    if (response.isSuccessful()) {
                        //Log.d("Jimy", data.toString());
                        //Log.w(TAG,"Sucess");
                        if (data.getIdUsuario() != 0) {
                            if (data.getLengthListMenu() > 0) {
                                sagasSql.InsertMenuDTO(data.getListMenu());
                                // Debería de guardar en bd
                                // Log.d("Ali",data.getListMenu());
                                loginPresenter.onSuccessLogin(data);
                            } else {
                                loginPresenter.onError(data.getMensaje());
                            }
                        } else {
                            loginPresenter.onError(data.getMensaje());
                        }
                    } else {
                        //Log.w(TAG, response.errorBody().toString());
                        //Log.d("message", response.message());
                        if (data != null) {
                            loginPresenter.onError(data.getMensaje());
                        } else {
                            Log.d("else", "");
                            JSONObject respuesta = null;
                            try {
                                respuesta = new JSONObject(response.errorBody().string());
                                Log.d("try", respuesta + "");
                                Log.d("errorbody", response.errorBody() + "");
                            } catch (JSONException e) {
                                e.printStackTrace();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                            if (respuesta != null) {
                                try {
                                    Log.w("Error body", respuesta.toString());
                                    loginPresenter.onError(respuesta.getString("Mensaje"));
                                } catch (JSONException e) {
                                    Log.d("catch", "");
                                    e.printStackTrace();
                                }

                            } else {
                                loginPresenter.onError(response.message());
                                Log.d("elseerror", response.message());
                            }
                        }
                    }
                } catch (Exception error) {
                    loginPresenter.onError("Usuario y/o contraseña incorrectos - try");
                }

            }

            @Override
            public void onFailure(Call<UsuarioDTO> call, Throwable t) {
                Log.d("entro mensaje:", "Si entró");
                Log.e("error", t.toString());
                Log.e("error", t.getLocalizedMessage());
                loginPresenter.onError(t.getMessage());
            }
        });
    }

    public void postRegistrar(UsuarioLoginDTO usuarioLoginDTO, String token) {
        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<UsuarioDTO> call = restClient.postRegistrar(usuarioLoginDTO, token, "application/json");

        call.enqueue(new Callback<UsuarioDTO>() {
            @Override
            public void onResponse(Call<UsuarioDTO> call, Response<UsuarioDTO> response) {
                Log.d("ali",  "Si entro");
                if(response.isSuccessful()){
                    UsuarioDTO data = response.body();
                    Log.d("ali111", data.toString());
                    loginPresenter.onError(data.getMensaje(), "");
                }
                /*try {
                    UsuarioDTO data = response.body();
                    Log.d("respbodyRegistrologin", response.body() + "");
                    //loginPresenter.onSuccessLogin(UsuarioDTO);
                    if (response.isSuccessful()) {
                        Log.d("postRegistrologin", data.toString());
                        //Log.w(TAG,"Sucess");
                        if (data.getIdUsuario() != 0) {
                            if (data.getLengthListMenu() > 0) {
                                sagasSql.InsertMenuDTO(data.getListMenu());
                                // Debería de guardar en bd
                                // Log.d("Ali",data.getListMenu());
                                loginPresenter.onSuccessLogin(data);
                            } else {
                                loginPresenter.onError("No te encuentras en el area de login", "");
                            }
                        } else {
                            loginPresenter.onError("No te encuentras en el area de login", "");
                        }
                    } else {
                        Log.w(TAG, response.errorBody().toString());
                        //Log.d("message", response.message());
                        if (data != null) {
                            loginPresenter.onError("No te encuentras en el area de login", "");
                        } else {
                            Log.d("else", "");
                            JSONObject respuesta = null;
                            try {
                                respuesta = new JSONObject(response.errorBody().string());
                                Log.d("try", respuesta + "");
                                Log.d("errorbodyregistrarLogin", response.errorBody() + "");
                            } catch (JSONException e) {
                                e.printStackTrace();
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                            if (respuesta != null) {

                                    Log.w("Error body", respuesta.toString());
                                    loginPresenter.onError("No te encuentras en el area de login", "");


                            } else {
                                loginPresenter.onError("No te encuentras en el area de login", "");
                                Log.d("elseerror", response.message());
                            }
                        }
                    }
                } catch (Exception error) {
                    Log.d("catch registrarlog", "");
                    loginPresenter.onError("No te encuentras en el area de login", "");
                }*/

            }

            @Override
            public void onFailure(Call<UsuarioDTO> call, Throwable t) {
                Log.d("entro mensaje:", "Si entró");
                Log.e("error", t.toString());
                Log.e("error", t.getLocalizedMessage());
                loginPresenter.onError("No te encuentras en el area de login", "");
            }
        });
    }

}
