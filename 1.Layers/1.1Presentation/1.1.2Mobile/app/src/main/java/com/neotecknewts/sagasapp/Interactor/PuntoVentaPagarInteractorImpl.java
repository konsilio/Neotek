package com.neotecknewts.sagasapp.Interactor;

import android.util.Log;

import com.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.neotecknewts.sagasapp.Model.RespuestaVentaExtraforaneaDTO;
import com.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaPagarPresenter;
import com.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Lisener;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class PuntoVentaPagarInteractorImpl implements PuntoVentaPagarInteractor {
    PuntoVentaPagarPresenter presenter;
    private boolean registro_local = false;

    public PuntoVentaPagarInteractorImpl(PuntoVentaPagarPresenter presenter) {
        this.presenter = presenter;
    }

    @Override
    public void pagar(VentaDTO ventaDTO, String token, boolean esCamioneta, boolean esEstacion,
                      boolean esPipa, SAGASSql sagasSql) {
        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        ventaDTO.setFecha(ventaDTO.getFecha());
        Log.d("ali", ventaDTO.toString());
        Call<RespuestaPuntoVenta> call = restClient.pagar(ventaDTO, token, "application/json");
        Log.d("Ventadto", ventaDTO.toString());
        Log.w("Url base", ApiClient.BASE_URL);
        call.enqueue(new Callback<RespuestaPuntoVenta>() {
            @Override
            public void onResponse(Call<RespuestaPuntoVenta> call, Response<RespuestaPuntoVenta> response) {
                RespuestaPuntoVenta data = response.body();
                Log.d("FerChido", ventaDTO.toString());
                if (response.isSuccessful()) {
                    Log.d("ali",  "successful");
                    presenter.onSuccess(data);
                    registro_local = false;
                    RespuestaPuntoVenta dataresponse = response.body();
                    presenter.onError(dataresponse.getMensaje());
                }else{
                    Log.d("FerChido",  "code:" + response.code());
                    RespuestaPuntoVenta dataresponse = response.body();
                    if (dataresponse != null && dataresponse.getMensaje() != null) {
                        Log.d("FerChido",  dataresponse.toString());
                        presenter.onError(dataresponse.getMensaje());
                    } else {
                        presenter.onError("Algo salió mal. Intenta de nuevo");
                    }
                }
            }

            @Override
            public void onFailure(Call<RespuestaPuntoVenta> call, Throwable t) {
                /*if(registro_local) {
                    local(sagasSql, ventaDTO,esCamioneta,esEstacion,esPipa);
                    presenter.onSuccessAndroid();
                    Lisener lisener = new Lisener(sagasSql,token);
                    lisener.CrearRunable(Lisener.Proceso.VENTA);
                }*/
                presenter.onError("Error en el servidor");
            }
        });
    }

    @Override
    public void puntoVentaAsignado(String token) {


        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<PuntoVentaAsignadoDTO> call = restClient.getPuntoVentaAsigando(
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<PuntoVentaAsignadoDTO>() {
            @Override
            public void onResponse(Call<PuntoVentaAsignadoDTO> call, Response<PuntoVentaAsignadoDTO> response) {
                PuntoVentaAsignadoDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus", "Success");
                    presenter.onSuccessPuntoVentaAsignado(data);
                    registro_local = false;
                } else {
                    String mensaje = "";
                    switch (response.code()) {
                        case 404:
                            Log.w("Error", "not found");

                            break;
                        case 500:
                            Log.w("Error", "server broken");

                            break;
                        default:
                            Log.w("Error", "Error desconocido: " + response.code());

                            break;
                    }
                    presenter.onErrorPuntoVenta("No se ha podido identificar el punto de venta");
                    Log.d("Proceso", "sincronizarpuntoventa");
                }

            }

            @Override
            public void onFailure(Call<PuntoVentaAsignadoDTO> call, Throwable t) {
                presenter.onErrorPuntoVenta("No se ha podido identificar el punto de venta");
            }

        });
    }

    @Override
    public void verificarVentaExtraforanea(int idCliente, String token) {

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaVentaExtraforaneaDTO> call = restClient.getTieneVentaExtraforanea(
                idCliente,
                token,
                "application/json"
        );
        Log.w("Url base", ApiClient.BASE_URL);

        call.enqueue(new Callback<RespuestaVentaExtraforaneaDTO>() {
            @Override
            public void onResponse(Call<RespuestaVentaExtraforaneaDTO> call,
                                   Response<RespuestaVentaExtraforaneaDTO> response) {

                RespuestaVentaExtraforaneaDTO data = response.body();
                if (response.isSuccessful()) {
                    Log.w("Estatus", "Success");
                    presenter.onSuccessVentaExtraforanea(data);
                    registro_local = false;
                } else {
                    JSONObject respuesta = null;
                    try {
                        respuesta = new JSONObject(response.errorBody().string());

                    } catch (JSONException e) {
                        e.printStackTrace();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    switch (response.code()) {
                        case 404:
                            Log.w("Error", "not found");

                            break;
                        case 500:
                            Log.w("Error", "server broken");

                            break;
                        default:
                            Log.w("Error", "Error desconocido: " + response.code());

                            break;
                    }
                    if (respuesta != null) {
                        try {
                            Log.w("Error body", respuesta.toString());
                            presenter.onError(respuesta.getString("Mensaje"));

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    } else {
                        presenter.onError(response.message());
                    }

                }

            }

            @Override
            public void onFailure(Call<RespuestaVentaExtraforaneaDTO> call, Throwable t) {
                presenter.onErrorPuntoVenta("No se ha podido identificar el punto de venta");
            }

        });
    }

    private void local(SAGASSql sagasSql, VentaDTO ventaDTO, boolean esCamioneta,
                       boolean esEstacion, boolean esPipa) {
        try {
            if (sagasSql.GetVenta(ventaDTO.getFolioVenta()).getCount() == 0) {
                sagasSql.InsertarVenta(ventaDTO, esCamioneta, esEstacion, esPipa);
                if (sagasSql.GetVentaConcepto(ventaDTO.getFolioVenta()).getCount() == 0) {
                    sagasSql.InsertarConcepto(ventaDTO);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            Log.e("Mensaje excepcion", ex.getMessage());
        }

    }
}
