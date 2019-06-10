package com.example.neotecknewts.sagasapp.Util.Sincronizaciones;

import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaFinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Sincronizacion;

import java.net.URI;
import java.net.URISyntaxException;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class FinalizarDescarga {
    private SAGASSql db;
    private Sincronizacion sincronizacion;
    private boolean respuesta_servicio;
    private String token;
    public List<String> mensajes;

    public FinalizarDescarga (SAGASSql db , Sincronizacion sincronizacion, String token){
        this.db =db;
        this.sincronizacion = sincronizacion;
        this.token = token;
    }

    /**
     * SincronizarFinalizarDescargas
     * Permite tomar los valores almacenados en la base de datos local y
     * trasformarlos en un objeto de tipo {@link FinalizarDescargaDTO} para su envio al
     * api
     * @return boolean que reprecenta si se guardaron con exito
     */
    public boolean SincronizarFinalizarDescargas(){
        Cursor cursor = db.GetFinalizarDescargas();
        if(sincronizacion.servicioDisponible()) {
            if(cursor.getCount()>0) {
                cursor.moveToFirst();
                while (!cursor.isAfterLast()) {
                    FinalizarDescargaDTO dto = new FinalizarDescargaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    dto.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    dto.setIdOrdenCompra(cursor.getInt(
                            cursor.getColumnIndex("IdOrdenCompra")));
                    dto.setFechaDescarga(cursor.getString(
                            cursor.getColumnIndex("FechaDescarga")));
                    dto.setIdTipoMedidorTractor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorTractor")));
                    dto.setIdTipoMedidorAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorAlmacen")));

                    boolean es_prestado = cursor.getInt(
                            cursor.getColumnIndex("TanquePrestado")) > 0;
                    dto.setTanquePrestado(es_prestado);
                    dto.setPorcentajeMedidorAlmacen(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedirorAlmacen")));
                    dto.setPorcentajeMedidorTractor(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorTractor")));
                    dto.setIdAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdAlmacen")));

                    Cursor imagenes = db.GetImagenesFinalizarDescargaByClaveOperacion(
                            dto.getClaveOperacion());
                    imagenes.moveToFirst();
                    while (!imagenes.isAfterLast()) {
                        String iuri = imagenes.getString(imagenes.getColumnIndex("Url"));
                        try {
                            dto.getImagenesURI().add(new URI(iuri));
                            dto.getImagenes().add(
                                    imagenes.getString(imagenes.getColumnIndex("Imagen"))
                            );
                        } catch (URISyntaxException e) {
                            e.printStackTrace();
                        }
                        imagenes.moveToNext();
                    }

                    Log.w("ClaveProceso", dto.getClaveOperacion());
                    boolean registrado = Registrar(dto);
                    if (registrado){
                        db.EliminarFinalizarDescarga(dto.getClaveOperacion());
                        db.EliminarImagenes(dto.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
                    return db.GetFinalizarDescargas().getCount() == 0;
            }else{
                mensajes.add("No hay finalizaciónes de descarga pendientes");
                return true;
            }
        }
        else {
            mensajes.add("No se pudieron realizar los registros de descargas, el servicio no esta" +
                    "disponible");
            return false;
        }
    }

    /**
     * Registrar
     * Realiza la acción de registrar los datos del modelo al api
     * retornara un valor boolean dependiendo de la respuesta del registro
     * @param dto Objeto de tipo {@link FinalizarDescargaDTO} con los valores a registrar
     * @return boolean que reprecenta la repsuesta del servidor
     */
    private boolean Registrar(FinalizarDescargaDTO dto) {
        Log.w("Registro","Registrando en servicio "+dto.getClaveOperacion());

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaFinalizarDescargaDTO> call = restClient.postFinalizarDescarga(dto,token,
                "application/json");
        call.enqueue(new Callback<RespuestaFinalizarDescargaDTO>() {
            @Override
            public void onResponse(Call<RespuestaFinalizarDescargaDTO> call,
                                   Response<RespuestaFinalizarDescargaDTO> response) {
                respuesta_servicio = call.isExecuted() && response.isSuccessful();
                if(respuesta_servicio)
                    Log.e("Registro fdescarga "+dto.getClaveOperacion(),
                            String.valueOf(response.isSuccessful()));
                else
                    Log.w("Registro f descarga "+dto.getClaveOperacion(),"Error "+
                            response.body().getMensaje());
            }

            @Override
            public void onFailure(Call<RespuestaFinalizarDescargaDTO> call, Throwable t) {
                respuesta_servicio = false;
            }
        });
        Log.w("Registro","Registro en servicio "+dto.getClaveOperacion()+": "+
                respuesta_servicio);
        return respuesta_servicio;
    }
}
