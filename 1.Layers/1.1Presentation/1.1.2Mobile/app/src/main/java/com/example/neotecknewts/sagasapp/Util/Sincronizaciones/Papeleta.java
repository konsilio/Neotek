package com.example.neotecknewts.sagasapp.Util.Sincronizaciones;

import android.content.Context;
import android.database.Cursor;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Presenter.Rest.ApiClient;
import com.example.neotecknewts.sagasapp.Presenter.Rest.RestClient;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Constantes;
import com.example.neotecknewts.sagasapp.Util.Sincronizacion;
import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class Papeleta {
    private SAGASSql db;
    private Sincronizacion sincronizacion;
    private String token;
    private boolean respuesta_servicio;
    public List<String> mensajes;


    /**
     * Permite realizar la sincronización de los datos de la
     * papeleta
     * @param context Contexto de la aplicación
     * @param db Base de datos en SQLITE
     */
    public Papeleta(Context context, SAGASSql db, Sincronizacion sincronizacion,String token){
        this.db = db;
        this.sincronizacion = sincronizacion;
        this.token = token;
    }

    /**
     * SincronizarPapeletas
     * Realiza la adaptación de los datos en sqlite a un modelo dto
     * para su envio a la api, retornara un boolean para determinar
     * si los registros fueron completados
     * @return boolean con el resultado de que si el envio fue correcto
     */
    public boolean SincronizarPapeletas(){
        Log.w("Consulta papeletas","Consulta de papeletas: "+new Date());
        Cursor papeletas = this.db.GetPapeletas();
        papeletas.moveToFirst();
        Log.w("Total papeletas",String.valueOf(papeletas.getCount()));
        //Verifico si es necesario registrar las papeletas
        if (papeletas.getCount()>0) {
            while (!papeletas.isAfterLast()) {
                PrecargaPapeletaDTO dto = new PrecargaPapeletaDTO();
                dto.setClaveOperacion(
                        papeletas.getString(
                                papeletas.getColumnIndex("ClaveOperacion")
                        )
                );
                /* Coloco los valores de la base de datos en el DTO */
                dto.setClaveOperacion(papeletas.getString(
                        papeletas.getColumnIndex("ClaveOperacion")));
                dto.setIdOrdenCompraExpedidor(papeletas.getInt(
                        papeletas.getColumnIndex("IdOrdenCompraExpedidor")));
                dto.setIdOrdenCompraPorteador(papeletas.getInt(
                        papeletas.getColumnIndex("IdOrdenCompraPorteador")));
                dto.setIdProveedorPorteador(papeletas.getInt(
                        papeletas.getColumnIndex("IdProveedorPorteador")));
                dto.setIdProveedorExpedidor(papeletas.getInt(
                        papeletas.getColumnIndex("IdProveedorExpedidor")));
                dto.setFecha(papeletas.getString(
                        papeletas.getColumnIndex("Fecha")));
                dto.setFechaEmbarque(papeletas.getString(
                        papeletas.getColumnIndex("FechaEmbarque")));
                dto.setNumeroEmbarque(papeletas.getString(
                        papeletas.getColumnIndex("NumeroEmbarque")));
                dto.setPlacasTractor(papeletas.getString(
                        papeletas.getColumnIndex("PlacasTractor")));
                dto.setNombreOperador(papeletas.getString(
                        papeletas.getColumnIndex("NombreOperador")));
                dto.setProducto(papeletas.getString(
                        papeletas.getColumnIndex("Producto")));
                dto.setNumeroTanque(papeletas.getString(
                        papeletas.getColumnIndex("NumeroTanque")));
                dto.setPresionTanque(papeletas.getDouble(
                        papeletas.getColumnIndex("PresionTanque")));
                dto.setCapacidadTanque(papeletas.getDouble(
                        papeletas.getColumnIndex("CapacidadTanque")));
                dto.setPorcentajeTanque(papeletas.getDouble(
                        papeletas.getColumnIndex("PorcentajeTanque")));
                dto.setMasa(papeletas.getDouble(
                        papeletas.getColumnIndex("Masa")));
                dto.setSello(papeletas.getString(
                        papeletas.getColumnIndex("Sello")));
                dto.setValorCarga(papeletas.getDouble(
                        papeletas.getColumnIndex("ValorCarga")));
                dto.setNombreResponsable(papeletas.getString(
                        papeletas.getColumnIndex("NombreResponsable")));
                dto.setPorcentajeMedidor(papeletas.getDouble(
                        papeletas.getColumnIndex("PorcentajeMedidor")));
                dto.setNombreTipoMedidorTractor(papeletas.getString(
                        papeletas.getColumnIndex("NombreTipoMedidorTractor")));
                dto.setIdTipoMedidorTractor(papeletas.getInt(
                        papeletas.getColumnIndex("IdTipoMedidorTractor")));
                dto.setCantidadFotosTractor(papeletas.getInt(
                        papeletas.getColumnIndex("CantidadFotosTractor")));
                papeletas.moveToNext();
                Cursor imagenes = db.GetRecordsByCalveUnica(dto.getClaveOperacion());
                //Obtener imagenes papeleta
                while (!imagenes.isAfterLast()) {
                    dto.getImagenes().add(
                            imagenes.getString(imagenes.getColumnIndex("Url"))
                    );
                    imagenes.moveToNext();
                }
                if (Registro(dto)) {
                    this.db.Eliminar(dto.getClaveOperacion());
                    this.db.EliminarImagenes(dto.getClaveOperacion());
                }
            }
            int total = db.GetPapeletas().getCount();
            return total == 0;
        }
        //En caso de que no existan registros le digo que este no tiene nada y todo correcto
        else
            return true;

    }

    /**
     * Registro
     * Permite realizar el registro de la papeleta en el api para la sincronización de datos
     * @param dto Objeto {@link PrecargaPapeletaDTO} con los datos transformados
     * @return boolean con el resultado del registro
     */
    private boolean Registro(PrecargaPapeletaDTO dto) {
        Log.w("Registro papeleta ",dto.getClaveOperacion());
        if(sincronizacion.servicioDisponible()){

            RestClient restClient = ApiClient.getClient().create(RestClient.class);
            Call<RespuestaPapeletaDTO> call = restClient.postPapeleta(dto,this.token,
                    "application/json");

            call.enqueue(new Callback<RespuestaPapeletaDTO>() {
                @Override
                public void onResponse(Call<RespuestaPapeletaDTO> call,
                                       Response<RespuestaPapeletaDTO> response) {
                    respuesta_servicio = response.isSuccessful() && response.body().isExito();
                    if(respuesta_servicio)
                        Log.w("Registro papeleta","Exito");
                    else
                        Log.w("Registro papeleta"+dto.getClaveOperacion(),"Error "+
                                response.body().getMensaje());
                }

                @Override
                public void onFailure(Call<RespuestaPapeletaDTO> call, Throwable t) {
                    respuesta_servicio= false;
                    Log.w("Registro papeleta"+dto.getClaveOperacion(),"Error "+
                            t.getMessage());
                }
            });

            if(!respuesta_servicio)
                mensajes.add("No se pudo registrar la papeleta "+dto.getClaveOperacion());
        }
        return respuesta_servicio;
    }

}
