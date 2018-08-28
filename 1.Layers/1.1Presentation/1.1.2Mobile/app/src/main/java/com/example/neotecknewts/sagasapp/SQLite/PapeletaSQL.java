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
import java.util.List;
import java.util.UUID;

/**
 * PapeletaSQL
 * Clase que extiende de {@link SQLiteOpenHelper} para realizar las transacciónales
 * en una base de datos local los datos de la papeleta y la imagenes de la papeleta
 * @author Jorge Omar Tovar Martìnez <jorge.tovar@neotheck.com.mx>
 * @date 20/08/18.
 * @lastupdate 24/08/2018
 * @company NEOTECK
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
    //region Constructor

    /**
     * Constrcutor del clase , tomara como parametro el {@link Context} o contexto
     * de la aplicación con la que se trabaja la base de datos
     * @param context Objeto de tipo {@link Context} que es la activity actual
     *                (Example:MyActivity.this)
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     */
    public PapeletaSQL(Context context) {
        super(context, DB_NAME, null, DB_VERSION);
    }
    //endregion
    //region Metodos sobreescritos

    /**
     * onCreate
     * Se llama cuando la base de datos se crea por primera vez. Aquí es donde debería ocurrir la
     * creación de tablas y la población inicial de las tablas.
     * @param db Objeto {@link SQLiteDatabase} que permite la inteacción con bases de datos locales
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     */
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
                "Falta BOOLEAN DEFAULT 1)");

        //endregion
        //region tabla Imagenes de la papeleta
        db.execSQL("CREATE TABLE "+TABLE_PAPELETAS_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                "Imagen TEXT,"+
                "Url TEXT,"+
                "CalveUnica TEXT,"+
                "Falta BOOLEAN DEFAULT 1"+
                ")");
        //endregion
    }

    /**
     * onUpgrade
     * Se llama cuando la base de datos necesita ser actualizada.La implementación debería usar este
     * método para eliminar tablas, agregar tablas o hacer cualquier otra cosa que necesite para
     * actualizar a la nueva versión de esquema. Si agrega nuevas columnas, puede usar ALTER TABLE
     * para insertarlas en una tabla activa. Si cambia el nombre o elimina las columnas, puede usar
     * ALTER TABLE para cambiar el nombre de la tabla anterior, luego crear la nueva tabla y llenar
     * la nueva tabla con el contenido de la tabla anterior.
     * Este método se ejecuta dentro de una transacción. Si se lanza una excepción, todos
     * los cambios se revertirán automáticamente.
     * @param db Objeto {@link SQLiteDatabase} que permite la inteacción con bases de datos locales
     * @param oldVersion Valor de tipo int que reprecenta la verción mas vieja de la base de datos
     * @param newVersion Valor de tipo int que reprecenta la versión nueva de la base de datos
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        /* Elimino la tabla de la base de dato */
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_PAPELETAS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_IMAGES_PAPELETAS);
        /* Invoca nuevamente el metodo para crear la tabla */
        onCreate(db);
    }
    //endregion
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

    /**
     * Permite buscar un registro por medio de su id en la base de datos local
     * retornara un objeto de tipo {@link Cursor} con los datos encontrados
     * @param id Id del registro a buscar
     * @return Objeto {@link Cursor} con los datos obtenidos
     *
     */
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
        return db.rawQuery("SELECT * FROM "+TABLE_PAPELETAS+ " WHERE ClaveOperacion = '"+
                ClaveOperacion+"'",null);
    }

    /**
     * GetNumberOfRecors
     * Prime obtener el numero total de registros en la tabla
     * @return Un valor de tipo entero que reprecenta el numero de registros en la base de datos
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
