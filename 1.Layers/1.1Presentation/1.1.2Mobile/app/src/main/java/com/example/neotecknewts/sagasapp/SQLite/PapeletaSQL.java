package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.DatabaseUtils;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;

import java.util.Iterator;
import java.util.List;
import java.util.UUID;

/**
 * Created by neotecknewts on 20/08/18.
 */

public class PapeletaSQL extends SQLiteOpenHelper {
    //region Constantes
    private static final String DB_NAME = "sagas_db";
    private static final int DB_VERSION = 1;
    private static final String TABLE_NAME = "papeletas";
    private static final String TABLE_IMAGES_PAPELETAS = "papeletas_imagenes";
    public Integer RowId;
    //endregion

    public PapeletaSQL(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL("CREATE TABLE "+TABLE_NAME+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                "ClaveOperacion TEXT," +
                "IdOrdenCompraExpedidor INTEGER,"+
                "IdOrdenCompraPorteador INTEGER,"+
                "IdProveedorPorteador INTEGER,"+
                "IdProveedorExpedidor INTEGER," +
                "Fecha DATE,"+
                "FechaEmbarque DATE,"+
                "NumeroEmbarque TEXT,"+
                "PlacasTractor TEXT,"+
                "NombreOperador TEXT,"+
                "Producto TEXT,"+
                "NumeroTanque TEXT,"+
                "PresionTanque DECIMAL(10,2),"+
                "CapacidadTanque DECIMAL(10,2),"+
                "PorcentajeTanque DECIMAL(10,2),"+
                "Masa DECIMAL(10,2),"+
                "Sello TEXT,"+
                "ValorCarga DECIMAL(10,2),"+
                "NombreResponsable TEXT,"+
                "PorcentajeMedidor TEXT,"+
                "NombreTipoMedidorTractor TEXT,"+
                "IdTipoMedidorTractor INTEGER,"+
                "CantidadFotosTractor INTEGER,"+
                "Falta BOOLEAN DEFAULT TRUE,"+
                ")");
        /*db.execSQL("CREATE TABLE "+TABLE_IMAGES_PAPELETAS+"(" +
                "IMAGEN TEXT,"+
                "IMAGEN_URL TEXT,"+
                "Uuid_ TEXT"+
                ")");*/
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF EXIST "+TABLE_NAME);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }

    /**
     * Permite hacer el registro en local de los datos de la
     * papeleta, retornara un string del objeto {@link UUID}
     * como referencia del registro
     * @param papeletaDTO Modelo de {@link PrecargaPapeletaDTO} con los datos a registra
     * @return boolean Resultado del registro en base de datos
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public boolean Insert(PrecargaPapeletaDTO papeletaDTO,String clave_operacion){

        SQLiteDatabase db = this.getReadableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",clave_operacion);
        contentValues.put("IdOrdenCompraExpedidor",papeletaDTO.getIdOrdenCompraExpedidor());
        contentValues.put("IdOrdenCompraPorteador",papeletaDTO.getIdOrdenCompraPorteador());
        contentValues.put("IdProveedorPorteador",papeletaDTO.getIdProveedorPorteador());
        contentValues.put("IdProveedorExpedidor",papeletaDTO.getIdProveedorExpedidor());
        contentValues.put("Fecha",papeletaDTO.getFecha().toString());
        contentValues.put("FechaEmbarque",papeletaDTO.getFechaEmbarque().toString());
        contentValues.put("NumeroEmbarque",papeletaDTO.getNumeroEmbarque());
        contentValues.put("PlacasTractor",papeletaDTO.getPlacasTractor());
        contentValues.put("NombreOperador",papeletaDTO.getNombreOperador());
        contentValues.put("Producto",papeletaDTO.getProducto());
        contentValues.put("NumeroTanque",papeletaDTO.getNumeroTanque());
        contentValues.put("PresionTanque",papeletaDTO.getPresionTanque());
        contentValues.put("CapacidadTanque",papeletaDTO.getPresionTanque());
        contentValues.put("PorcentajeTanque",papeletaDTO.getPorcentajeTanque());
        contentValues.put("Masa",papeletaDTO.getMasa());
        contentValues.put("Sello",papeletaDTO.getSello());
        contentValues.put("ValorCarga",papeletaDTO.getValorCarga());
        contentValues.put("NombreResponsable",papeletaDTO.getValorCarga());
        contentValues.put("PorcentajeMedidor",papeletaDTO.getPorcentajeMedidor());
        contentValues.put("NombreTipoMedidorTractor",papeletaDTO.getNombreTipoMedidorTractor());
        contentValues.put("IdTipoMedidorTractor",papeletaDTO.getIdTipoMedidorTractor());
        contentValues.put("CantidadFotosTractor",papeletaDTO.getImagenes().size());
        contentValues.put("Falta",true);
        db.insert(TABLE_NAME,null,contentValues);
        //Guardar imagenes
        /*ContentValues contentValues1 = new ContentValues();

        for (Iterator<String> i = papeletaDTO.getImagenes().iterator(); i.hasNext(); ) {
            String object = i.next();
            contentValues1.put("");
            // do something with object
        }*/
        return true;
    }

    /**
     * Eliminar
     * Permite realizar el borrado del registro especificado por
     * medio de su {@link UUID} generado
     * @param ClaveOperacion String generado a partir la clave unica
     * @return Numero de registro eliminados
     */
    public Integer Eliminar(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_NAME,
                "ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }
    public Integer EliminarById(String id){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_NAME,
                "Id = "+id,
                null);
    }
    /**
     * GetRecordByUuid
     * Permite realizar la busqueda de un registro por medio del uuid generado
     * @param ClaveOperacion String de la clave unica generada
     * @return Objeto con los registros de la consulta
     */
    public Cursor GetRecordByCalveUnica(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_NAME+ " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * GetNumberOfRecors
     * Permie obtener el numero total de registros en la tabla
     * @return
     */
    public int GetNumberOfRecors(){
        SQLiteDatabase db = this.getReadableDatabase();
        return (int) DatabaseUtils.queryNumEntries(db, TABLE_NAME);
    }
}
