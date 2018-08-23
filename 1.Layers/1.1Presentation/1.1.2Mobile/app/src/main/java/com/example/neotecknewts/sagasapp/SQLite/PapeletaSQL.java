package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.DatabaseUtils;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;

import java.net.URI;
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
    private static final String TABLE_PAPELETAS = "papeletas";
    private static final String TABLE_PAPELETAS_IMAGENES = "papeletas_imagenes";
    private static final String TABLE_IMAGES_PAPELETAS = "papeletas_imagenes";
    public Integer RowId;
    //endregion

    public PapeletaSQL(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        //region Tabla Papeleta
        db.execSQL("CREATE TABLE "+TABLE_PAPELETAS+"(" +
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
                "Falta BOOLEAN DEFAULT(TRUE))");

        //endregion
        //region tabla Imagenes de la papeleta
        db.execSQL("CREATE TABLE "+TABLE_PAPELETAS_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                "Imagen TEXT,"+
                "Url TEXT,"+
                "CalveUnica TEXT,"+
                "Falta BOOLEAN DEFAULT(TRUE)"+
                ")");
        //endregion
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_PAPELETAS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_IMAGES_PAPELETAS);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }
    //region Acciónes papeleta
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

        Long id = db.insert(TABLE_PAPELETAS,null,contentValues);
        Log.v("Registro",String.valueOf(id));
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
        return db.delete(TABLE_PAPELETAS,
                "ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }
    public Integer EliminarById(String id){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_PAPELETAS,
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
        return db.rawQuery("SELECT * FROM "+TABLE_PAPELETAS+ " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * GetNumberOfRecors
     * Permie obtener el numero total de registros en la tabla
     * @return
     */
    public int GetNumberOfRecors(){
        SQLiteDatabase db = this.getReadableDatabase();
        return (int) DatabaseUtils.queryNumEntries(db, TABLE_PAPELETAS);
    }
    //endregion
    //region Acciónes Imagenes papeleta
    /**
     * InsertImagenes
     * Realiza el registro en base de datos de los datos de la imagen,
     * retornara en caso de ser correcto el id del registro en caso contrario retornara un -1
     * @param imagen String de 64 bits de la imagen
     * @param url Url en el dispositvo de la imagen
     * @param CalveUnica Clave unica de la papeleta
     * @return En caso de ser correcto el Id del registro, en caso erroneo un -1
     */
    public Long[] InsertImagenes(List<URI> imagen, List<String> url, String CalveUnica){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] inserts = new Long[imagen.size()];
        for (int x = 0;x<imagen.size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("IMAGEN",imagen.get(x).toString());
            contentValues.put("Url",url.get(x));
            contentValues.put("CalveUnica",CalveUnica);
            inserts[x] =  db.insert(TABLE_IMAGES_PAPELETAS,null,contentValues);
            Log.w("Imagenes",String.valueOf(inserts[x]));

        }
        return inserts;
    }
    /**
     * GetRecordsByCalveUnica
     * Retorna un arreglo con las imagenes de la papeleta, se tomara como parametro la
     * ClaveOperacion
     * @param ClaveOperacion Clave unica del la orden
     * @return Registro/os que retorno la consulta
     */
    public Cursor GetRecordsByCalveUnica(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_IMAGES_PAPELETAS+" WHERE CalveUnica ='"+ClaveOperacion+"'",null);
    }
    //endregion
}
