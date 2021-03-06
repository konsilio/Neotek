
package com.example.neotecknewts.sagasapp.Util.Sincronizaciones;

import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaIniciarDescargaDTO;
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

public class Descarga {
    private SAGASSql db;
    private Sincronizacion sincronizacion;
    private boolean respuesta_servicio;
    private String token;
    public List<String> mensajes;

    public Descarga(SAGASSql db, Sincronizacion sincronizacion, String token) {
        this.db = db;
        this.sincronizacion = sincronizacion;
        this.token = token;
    }

    /**
     * SincronizarIniciarDescargas
     * Permite realizar el envio de los datos de la descarga inicial al api,
     * recuperara desde la base de datos sqlite los registros de las descargas
     * y lo transformara a un modelo dto de tipo {@link IniciarDescargaDTO} el cual
     * sera enviado a la api la cual repspondera un boolean de exito o fracaso , en
     * caso de exito eliminara los registros locales
     * @return boolean Bandera que determina que si hay o no registros en local
     */
    public boolean SincronizarIniciarDescargas(){
        Cursor cursor = db.GetIniciarDescargas();
        cursor.moveToFirst();
        //Verifico si hay descargas iniciales
        if(sincronizacion.servicioDisponible()) {
            if (cursor.getCount() > 0) {
                while (!cursor.isAfterLast()) {
                    IniciarDescargaDTO dto = new IniciarDescargaDTO();
                    /* Coloco los valores de la base de datos en el DTO */
                    dto.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    dto.setIdOrdenCompra(cursor.getInt(
                            cursor.getColumnIndex("IdOrdenCompra")));
                    dto.setClaveOperacion(cursor.getString(
                            cursor.getColumnIndex("ClaveOperacion")));
                    dto.setFechaDescarga(cursor.getString(
                            cursor.getColumnIndex("FechaDescarga")));
                    dto.setNombreTipoMedidorTractor(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorTractor")));
                    dto.setNombreTipoMedidorAlmacen(cursor.getString(
                            cursor.getColumnIndex("NombreTipoMedidorAlmacen")));
                    dto.setIdTipoMedidorTractor(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorTractor")));
                    dto.setIdTipoMedidorAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdTipoMedidorAlmacen")));
                    dto.setCantidadFotosAlmacen(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosAlmacen")));
                    dto.setCantidadFotosTractor(cursor.getInt(
                            cursor.getColumnIndex("CantidadFotosTractor")));
                    boolean es_prestado = cursor.getInt(
                            cursor.getColumnIndex("TanquePrestado")) > 0;
                    dto.setTanquePrestado(es_prestado);
                    dto.setPorcentajeMedidorAlmacen(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorAlmacen")));
                    dto.setPorcentajeMedidorTractor(cursor.getDouble(
                            cursor.getColumnIndex("PorcentajeMedidorTractor")));
                    dto.setIdAlmacen(cursor.getInt(
                            cursor.getColumnIndex("IdAlmacen")));

                    Cursor imagenes = db.GetImagenesDescargaByClaveUnica(dto.getClaveOperacion());
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

                    if (Registro(dto)) {
                        db.EliminarDescarga(dto.getClaveOperacion());
                        db.EliminarImagenesDescarga(dto.getClaveOperacion());
                    }
                    cursor.moveToNext();
                }
                return db.GetIniciarDescargas().getCount()==0;
            } else {
                mensajes.add("No hay papeletas pendientes");
                return true;
            }
        } else {
            mensajes.add("No se pudieron realizar los registros de descargas, el servicio no esta" +
                    "disponible");
            return false;
        }
    }

    /**
     * Registro
     * Realiza el registro de la  descarga inicial en el api, retornara un
     * valor de tipo bool dependiendo de la repsuesta de este
     * @param dto Objeto de tipo {@link IniciarDescargaDTO} con los valores almacenados
     * @return boolean con el repsutado de la solicitud
     */
    private boolean Registro(IniciarDescargaDTO dto){
        Log.w("Registro","Registrando en servicio "+dto.getClaveOperacion());

        RestClient restClient = ApiClient.getClient().create(RestClient.class);
        Call<RespuestaIniciarDescargaDTO> call = restClient.postDescarga(dto,token,
                "application/json");
        call.enqueue(new Callback<RespuestaIniciarDescargaDTO>() {
            @Override
            public void onResponse(Call<RespuestaIniciarDescargaDTO> call,
                                   Response<RespuestaIniciarDescargaDTO> response) {
                respuesta_servicio = call.isExecuted() && response.isSuccessful();
                if(respuesta_servicio)
                    Log.w("Registro descarga ini","Exito");
                else
                    Log.w("Registro descarga ini "+dto.getClaveOperacion(),"Error "+
                            response.body().getMensaje());
            }

            @Override
            public void onFailure(Call<RespuestaIniciarDescargaDTO> call, Throwable t) {
                respuesta_servicio = false;
            }
        });
        Log.w("Registro","Registro en servicio "+dto.getClaveOperacion()+": "+
                respuesta_servicio);
        return respuesta_servicio;
    }
}
