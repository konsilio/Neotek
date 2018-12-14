package com.example.neotecknewts.sagasapp.SQLite;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.CalibracionDTO;
import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Model.VentasCorteDTO;

/**
 * Clase SAGASSql para el manejo de base de datos local
 * @author Jorge Omar Tovar Martínez <jorge.tovar@neothec.com.mx>
 * @companny Neoteck
 * @date 28/08/2018
 * @updated 06/09/2018
 */
public class SAGASSql extends SQLiteOpenHelper {
    //region Variables estaticas
    private static final String DB_NAME = "sagas_db";
    private static final int DB_VERSION = 1;
    private static final String TABLE_LECTURA_INICIAL = "lectura_inicial";
    private static final String TABLE_LECTURA_INICIAL_P5000 = "lectura_inicial_p5000";
    private static final String TABLE_LECTURA_INICIAL_IMAGENES = "lectura_inicial_imagenes";
    private static final String TABLE_LECTURA_FINALIZAR = "lectura_finalizar";
    private static final String TABLE_LECTURA_FINALIZAR_P5000 = "lectura_finalizar_p5000";
    private static final String TABLE_LECTURA_FINALIZAR_IAMGENES = "lectura_finalizar_imagenes";
    private static final String TABLE_LECTURA_INICIAL_PIPA = "lectura_inicial_pipa";
    private static final String TABLE_LECTURA_INICIAL_PIPA_P5000 = "lectura_inicial_pipa_p5000";
    private static final String TABLE_LECTURA_INICIAL_PIPA_IMAGENES =
            "lectura_inicial_pipa_imagenes";
    private static final String TABLE_LECTURA_FINAL_PIPA = "lectura_final_pipa";
    private static final String TABLE_LECTURA_FINAL_PIPA_P5000 = "lectura_finalal_pipa_p5000";
    private static final String TABLE_LECTURA_FINAL_PIPA_IMAGENES = "lectura_final_pipa_imagenes";
    private static final String TABLE_LECTURA_INICIAL_ALMACEN = "lectura_incial_almancen";
    private static final String TABLE_LECTURA_INICIAL_ALMACEN_IMAGENES =
            "lectura_inicial_almacen_imagenes";
    private static final String TABLE_LECTURA_FINAL_ALMACEN = "lectura_final_almacen";
    private static final String TABLE_LECTURA_FINAL_ALMACEN_IMAGENES =
            "lectura_final_almacen_imagenes";
    private static final String TABLE_LECTURA_INICIAL_CAMIONETA ="lectura_inicial_camioneta";
    private static final String TABLE_LECTURA_INICIAL_CAMIONETA_CILINDROS =
            "lectura_inicial_camioneta_cilindros";
    private static final String TABLE_LECTURA_FINAL_CAMIONETA = "lectura_final_camioneta";
    private static final String TABLE_LECTURA_FINAL_CAMIONETA_CILINDROS =
            "lectura_final_camiones_cilindros";
    private static final String TABLE_RECARGAS = "recargas";
    private static final String TABLE_RECARGAS_IMAGENES = "recargas_imagenes";
    private static final String TABLE_RECARGAS_CILINDROS = "recargas_images_cilindros";
    private static final String TABLE_REPORTES = "reportes";
    private static final String TABLE_VENTAS = "ventas";
    private static final String TABLE_VENTAS_CONCEPTO = "ventas_conceptos";
    private static final String TABLE_AUTOCONSUMO = "autoconsumos";
    private static final String TABLE_AUTOCONSUMO_IMAGENES = "autoconsumos_imagenes";
    private static final String TABLE_CALIBRACION = "calibracion";
    private static final String TABLE_CALIBRACION_IMAGENES = "calibracion_imagenes";
    private static final String TABLE_TRASPASOS = "traspasos";
    private static final String TABLE_TRASPASOS_IMAGENES = "traspasos_imagenes";
    private static final String TABLE_ANTICIPOS = "anticipos";
    private static final String TABLE_CORTES ="cortes";
    private static final String TABLE_CORTES_VENTAS ="cortes_ventas";

    public static final String TIPO_RECARGA_CAMIONETA = "C";
    public static final String TIPO_RECARGA_ESTACION_CARBURACION =  "EC";
    public static final String TIPO_RECARGA_PIPA = "P";
    public static final String TIPO_AUTOCONSUMO_ESTACION_CARBURACION = "ACEC";
    public static final String TIPO_AUTOCONSUMO_INVENTARIO_GENERAL = "ACIG";
    public static final String TIPO_AUTOCONSUMO_PIPAS = "ACP";
    public static final String TIPO_CALIBRACION_PIPA = "CALP";
    public static final String TIPO_CALIBRACION_ESTACION = "CALES";
    public static final String TIPO_TRASPASO_ESTACION = "TEC";
    public static final String TIPO_TRASPASO_PIPA = "TP";

    //endregion

    //region Constructor de clase
    /**
     * Constructor de clase , se tomara como parametro un objeto de tipo {@link Context }
     * que reprecenta la venta o activity actual con la que se invoca la base de datos
     * @param context Objeto de tipo {@link Context} que reprecenta la Activity actual
     */
    public SAGASSql(Context context) {
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
        //region Tabla lectura_inicial Calibacion
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "IdTipoMedidor INTEGER," +
                "NombreTipoMedidor TEXT," +
                "CantidadFotografiasMedidor TEXT," +
                "NombreEstacionCarburacion TEXT," +
                "IdEstacionCarburacion INTEGER," +
                "CantidadP5000 INTEGER," +
                "PorcentajeMedidor DOUBLE," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_p5000
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_P5000+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla lectura_finalizar Calibacion
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINALIZAR+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "IdTipoMedidor INTEGER," +
                "NombreTipoMedidor TEXT," +
                "CantidadFotografiasMedidor TEXT," +
                "NombreEstacionCarburacion TEXT," +
                "IdEstacionCarburacion INTEGER," +
                "CantidadP5000 INTEGER," +
                "PorcentajeMedidor DOUBLE," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_finalizar_p5000
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINALIZAR_P5000+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_finalizar_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINALIZAR_IAMGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveProceso TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla lectura_inicial_pipa
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_PIPA+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "IdPipa INTEGER," +
                "ClaveOperacion TEXT," +
                "NombrePipa TEXT," +
                "IdTipoMedidor INTEGER," +
                "TipoMedidor TEXT," +
                "CantidadFotografias INTEGER," +
                "CantidadP5000 INTEGER," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_pipa_p5000
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_PIPA_P5000+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_pipa_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_PIPA_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1"+
                ")");
        //endregion

        //region Tabla lectura_final_pipa
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_PIPA+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "IdPipa INTEGER," +
                "ClaveOperacion TEXT," +
                "NombrePipa TEXT," +
                "IdTipoMedidor INTEGER," +
                "TipoMedidor TEXT," +
                "CantidadFotografias INTEGER," +
                "CantidadP5000 INTEGER," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_pipa_p5000
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_PIPA_P5000+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_pipa_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_PIPA_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1"+
                ")");
        //endregion

        //region Tabla lectura_inicial_almacen
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_ALMACEN+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "IdAlmacen INTEGER," +
                "NombreAlmacen TEXT," +
                "IdTipoMedidor INTEGER," +
                "NombreTipoMedidor TEXT," +
                "CantidadFotografias TEXT," +
                "PorcentajeMedidor DOUBLE," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_inicial_almacen_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_ALMACEN_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla lectura_final_almacen
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_ALMACEN+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "IdAlmacen INTEGER," +
                "NombreAlmacen TEXT," +
                "IdTipoMedidor INTEGER," +
                "NombreTipoMedidor TEXT," +
                "CantidadFotografias TEXT," +
                "PorcentajeMedidor DOUBLE," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla lectura_final_almacen_imagenes
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_ALMACEN_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla lectura_incial_camioneta
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_CAMIONETA+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "IdCamioneta INTEGER," +
                "NombreCamioneta TEXT," +
                "Falta BOOLEAN DEFAULT 1,"+
                "FechaAplicacion TEXT"+
                ")");
        //endregion
        //region Tabla lectura_inicial_camioneta_cilindros
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_INICIAL_CAMIONETA_CILINDROS+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "IdCilindro INTEGER," +
                "CilindroKg TEXT," +
                "Cantidad INTEGER," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla lectura_final_camioneta
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_CAMIONETA+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "IdCamioneta INTEGER," +
                "NombreCamioneta TEXT," +
                "EsEncargadoPuerta BOOLEAN DEFAULT 1,"+
                "Falta BOOLEAN DEFAULT 1,"+
                "FechaAplicacion TEXT"+
                ")");
        //endregion
        //region Tabla lectura_final_camioneta_cilindros
        db.execSQL("CREATE TABLE "+TABLE_LECTURA_FINAL_CAMIONETA_CILINDROS+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT," +
                "IdCilindro INTEGER," +
                "CilindroKg TEXT," +
                "Cantidad INTEGER," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla de recargas
        db.execSQL("CREATE TABLE "+TABLE_RECARGAS+" (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "IdCAlmacenGasSalida INTEGER," +
                "IdCAlmacenGasEntrada INTEGER," +
                "IdTipoMedidorSalida INTEGER," +
                "IdTipoMedidorEntrada INTEGER," +
                "IdTipoEvento INTEGER," +
                "P5000Salida DOUBLE," +
                "P5000Entrada DOUBLE," +
                "ClaveOperacion TEXT," +
                "Falta BOOLEAN DEFAULT 1," +
                "EsTipo TEXT,"+
                "FechaAplicacion TEXT,"+
                "EsInicial BOOLEAN DEFAULT 1"+
                ")");
        //endregion
        //region Tabla de recargas_imagenes
        db.execSQL("CREATE TABLE "+TABLE_RECARGAS_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Imagen TEXT," +
                "Url TEXT," +
                "ClaveOperacion TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla de recargas_cilindros
        db.execSQL("CREATE TABLE "+TABLE_RECARGAS_CILINDROS+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "IdCilindro INTEGER," +
                "Cantidad INTEGER," +
                "ClaveOperacion TEXT," +
                "CilindroKg TEXT,"+
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Tabla de reportes
        db.execSQL("CREATE TABLE "+ TABLE_REPORTES+ "(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Fecha TEXT," +
                "IdCalmacen INTEGER," +
                "Html TEXT," +
                "Texto TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla de ventas
        db.execSQL("CREATE TABLE "+TABLE_VENTAS+" (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "FolioVenta TEXT,"+
                "IdCliente INTEGER," +
                "Subtotal DOUBLE," +
                "Iva DOUBLE," +
                "Total DOUBLE," +
                "Factura BOOLEAN DEFAULT 1 ," +
                "Credito BOOLEAN DEFAULT 0 ," +
                "Efectivo DOUBLE," +
                "Fecha TEXT," +
                "Hora TEXT," +
                "Cambio DOUBLE," +
                "SinNumero BOOLEAN DEFAULT 0," +
                "Falta BOOLEAN DEFAULT 1,"+
                "EsCamioneta BOOLEAN,"+
                "EsEstacion BOOLEAN,"+
                "EsPipa BOOLEAN"+
                ")");
        //endregion

        //region Tabla de concepto de venta
        db.execSQL("CREATE TABLE "+TABLE_VENTAS_CONCEPTO+" (" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "FolioVenta TEXT," +
                "IdTipoGas INTEGER," +
                "Cantidad INTEGER," +
                "Concepto TEXT," +
                "PUnitario DOUBLE," +
                "Descuento DOUBLE," +
                "Subtotal DOUBLE," +
                "IdCategoria INTEGER," +
                "IdLinea INTEGER," +
                "IdProducto INTEGER,"+
                "Falta BOOLEAN DEFAULT 1,"+
                "Year INTEGER,"+
                "Mes INTEGER,"+
                "Dia INTEGER,"+
                "PrecioUnitarioProducto DOUBLE,"+
                "PrecioUnitarioLt DOUBLE,"+
                "PrecioUnitatioKg DOUBLE,"+
                "DescuentoUnitarioProducto DOUBLE,"+
                "DescuentoUnitarioLt DOUBLE,"+
                "DescuentoUnitarioKg DOUBLE,"+
                "CantidadLt DOUBLE,"+
                "CantidadKg DOUBLE,"+
                "DescuentoTotal DOUBLE,"+
                "IdEmpresa INTEGER"+
                ")");
        //endregion

        //region Tabla de Autoconsumos
        db.execSQL("CREATE TABLE "+TABLE_AUTOCONSUMO+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "IdCAlmacenGasSalida INTEGER," +
                "IdCAlmacenGasEntrada INTEGER," +
                "P5000Salida INTEGER," +
                "NombreTipoMedidor TEXT,"+
                "ClaveOperacion TEXT," +
                "CantidadFotos INTEGER," +
                "PorcentajeMedidor DOUBLE," +
                "Tipo TEXT,"+
                "EsFinal BOOLEAN DEFAULT 1,"+
                "Falta BOOLEAN DEFAULT 1," +
                "FechaAplicacion TEXT,"+
                "IdTipoMedidor integer"+
                ")");
        //endregion
        //region Imagenes Autoconsumos
        db.execSQL("CREATE TABLE "+TABLE_AUTOCONSUMO_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "Url TEXT," +
                "Imagen TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Calibración
        db.execSQL("CREATE TABLE "+ TABLE_CALIBRACION+"(" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT,"+
                        "ClaveOperacion TEXT,"+
                        "IdCAlmacenGas INTEGER,"+
                        "IdTipoMedidor INTEGER,"+
                        "NombreCAlmacenGas TEXT,"+
                        "NombreMedidor TEXT,"+
                        "PorcentajeCalibracion DOUBLE,"+
                        "IdDestinoCalibracion INTEGER,"+
                        "P5000 INTEGER,"+
                        "Porcentaje DOUBLE,"+
                        "CantidadFotografias INTEGER,"+
                        "Tipo TEXT,"+
                        "Fecha DATETIME,"+
                        "Falta BOOLEAN DEFAULT 1," +
                        "FechaAplicacion DATETIME,"+
                        "PorcentajeMedidor2 DOUBLE,"+
                        "EsFinal BOOLEAN DEFAULT 1" +
                ")");
        //endregion
        //region Tabla de Imagenes Calibración
        db.execSQL("CREATE TABLE "+TABLE_CALIBRACION_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "Url TEXT," +
                "Imagen TEXT," +
                "Falta BOOLEAN DEFAULT 1" +
                ")");
        //endregion

        //region Traspasos
        db.execSQL("CREATE TABLE "+TABLE_TRASPASOS+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "CantidadFotos INTEGER,"+
                "IdCAlmacenGasEntrada INTEGER,"+
                "IdCAlmacenGasSalida INTEGER,"+
                "IdTipoMedidorSalida INTEGER,"+
                "NombreMedidor TEXT,"+
                "P5000Entrada INTEGER,"+
                "P5000Salida INTEGER,"+
                "PorcentajeSalida DOUBLE,"+
                "Tipo TEXT,"+
                "Fecha TEXT,"+
                "Falta BOOLEAN DEFAULT 1,"+
                "EsFinal BOOLEAN,"+
                "FechaAplicacion TEXT"+
                ")");
        //endregion
        //region Tabla de Imagenes traspasos
        db.execSQL("CREATE TABLE "+TABLE_TRASPASOS_IMAGENES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Imagen TEXT,"+
                "Url TEXT,"+
                "ClaveOperacion TEXT,"+
                "Falta BOOLEAN DEFAULT 1"+
                ")");

        //endregion

        //region Tabla de Anticipos
        db.execSQL("CREATE TABLE "+TABLE_ANTICIPOS+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "IdEstacion INTEGER," +
                "NombreEstacion TEXT,"+
                "Anticipo DOUBLE," +
                "Fecha DATETAIME," +
                "Total DOUBLE,"+
                "Hora TEXT,"+
                "IdCAlmacen INTEGER,"+
                "FechaAnticipo TEXT,"+
                "Falta BOOLEAN DEFAULT 1"+
                ")");
        //endregion

        //region Tabla de Cortes
        db.execSQL("CREATE TABLE "+TABLE_CORTES+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "ClaveOperacion TEXT,"+
                "Fecha DATE,"+
                "IdEstacion INTEGER," +
                "FechaCorte TEXT," +
                "Hora TEXT,"+
                "IdCAlmacen INTEGER,"+
                "Falta BOOLEAN DEFAULT 1"+
                ")");
        //endregion

        //region Tabla de ventas de corte
        db.execSQL("CREATE TABLE "+TABLE_CORTES_VENTAS+"(" +
                "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "Corte TEXT," +
                "TiketVenta TEXT," +
                "IdVenta INTEGER" +
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
     * @date 30/08/2018
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_P5000);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINALIZAR);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINALIZAR_P5000);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINALIZAR_IAMGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_PIPA);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_PIPA_P5000);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_PIPA_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_PIPA);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_PIPA_P5000);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_PIPA_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_ALMACEN);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_ALMACEN_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_ALMACEN);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_ALMACEN_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_CAMIONETA);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_INICIAL_CAMIONETA_CILINDROS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_CAMIONETA);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_LECTURA_FINAL_CAMIONETA_CILINDROS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_RECARGAS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_RECARGAS_CILINDROS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_RECARGAS_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_VENTAS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_VENTAS_CONCEPTO);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_AUTOCONSUMO);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_AUTOCONSUMO_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_CALIBRACION);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_CALIBRACION_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_TRASPASOS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_TRASPASOS_IMAGENES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_ANTICIPOS);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_CORTES);
        db.execSQL("DROP TABLE IF EXISTS "+TABLE_CORTES_VENTAS);
        onCreate(db);
    }
    //endregion

    //region Metodos para lectura inicial Calibacion

    /**
     * <h3>InsertLecturaInicial</h3>
     * Permite realizar el registro de la lectura inicial, se enviaran como parametros ,
     * un objeto de tipo {@link LecturaDTO} con los valores a registrar en local y al final
     * se retorna un valor de tipo {@link Long} que reprecenta el número de registro en la
     * base de datos local, en caso de ser menor o igual a -1 no se registro correctamente.
     * @param lecturaDTO Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Valor de tipo {@link Long} que tiene el id registrado en base de datos
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Long InsertLecturaInicial(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
        contentValues.put("IdTipoMedidor",lecturaDTO.getIdTipoMedidor());
        contentValues.put("NombreTipoMedidor",lecturaDTO.getNombreTipoMedidor());
        contentValues.put("CantidadFotografiasMedidor",lecturaDTO.getCantidadFotografias());
        contentValues.put("NombreEstacionCarburacion",lecturaDTO.getNombreEstacionCarburacion());
        contentValues.put("IdEstacionCarburacion",lecturaDTO.getIdEstacionCarburacion());
        contentValues.put("CantidadP5000",lecturaDTO.getCantidadP5000());
        contentValues.put("PorcentajeMedidor",lecturaDTO.getPorcentajeMedidor());

        return db.insert(TABLE_LECTURA_INICIAL,null,contentValues);
    }

    /**
     * <h3>GetLecturaByClaveUnica</h3>
     * Permite consultar un registro de la lectura inicial por medio de la clave ùnica, se envia de
     * parametro un {@link String} que reprecenta dicha clave y retornara un objeto de tipo
     * {@link Cursor} con los valores de la consulta en caso de existir.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Cursor GetLecturaByClaveUnica(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL+" WHERE ClaveProceso = '"
                +ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLectura</h3>
     * Permite realizar la eliminación de un registro de la lectura inicial, se envia de parametro
     * una cadena de tipo {@link String} que reprecenta la clave única de proceso y en caso de
     * que se elimine retornara el numero de registro eliminados en caso contrario, retornara
     * un -1.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Un valor de tipo {@link Integer} que reprecenta el numero de registro eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Integer EliminarLectura(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL,"ClaveProceso = '"+
                ClaveProceso+"'",null);
    }

    /**
     * <h3>GetLecturasIniciales</h3>
     * Permite retornar todas la lecturas iniciales que se almacenaron en base de datos,
     * retornara un objeto de tipo {@link Cursor} con el resultado de la consulta
     * @return Objeto de tipo {@link Cursor} con los resultados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 31/08/2018
     */
    public Cursor GetLecturasIniciales() {
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL,null);
    }
    //endregion

    //region Metodos para la lectura del P5000

    /**
     * <h3>InsertLecturaP5000</h3>
     * Permite realizar el registro de la imagen del P5000, tomara como paramreto un
     * objeto de tipo {@link LecturaDTO} que contiene la información de la imagen,
     * al final retornara un valor de tipo {@link Long} con el resultado del registro.
     * @param lecturaDTO  Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Variable de tipo {@link Long} con el id del registro , en caso de ser -1
     *          el regitro no fue correcto
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Long InsertLecturaP5000(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
        contentValues.put("Imagen",lecturaDTO.getImagenP5000());
        contentValues.put("Url",lecturaDTO.getImagenP5000URI().toString());
        return db.insert(TABLE_LECTURA_INICIAL_P5000,null,contentValues);
    }

    /**
     * <h3>GetLecturaP500ByClaveUnica</h3>
     * Permite consultar un registro de las imagenes DEL P5000 de lectura inicial por medio de la
     * clave ùnica, se envia de parametro un {@link String} que reprecenta dicha clave y retornara
     * un objeto de tipo {@link Cursor} con los valores de la consulta en caso de existir.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Cursor GetLecturaP5000ByClaveUnica(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_P5000+" WHERE ClaveProceso = '"
                +ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLecturaP5000</h3>
     * Permite realizar la eliminación de un registro de las imagenes de P5000
     * lectura inicial, se envia de parametro una cadena de tipo {@link String} que reprecenta
     * la clave única de proceso y en caso de que se elimine retornara el numero de registro
     * eliminados en caso contrario,  retornara un -1.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Un valor de tipo {@link Integer} que reprecenta el numero de registro eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Integer EliminarLecturaP5000(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_P5000,"ClaveProceso = '"+
                ClaveProceso+"'",null);
    }
    //endregion

    //region Metodos para las imagenes de la lectura inicial
    /**
     * <h3>InsertLecturaImagenes</h3>
     * Permite realizar el registro de las imagenes, tomara como paramreto un
     * objeto de tipo {@link LecturaDTO} que contiene la información de la imagen,
     * al final retornara un valor de tipo {@link Long} con el resultado del registro.
     * @param lecturaDTO  Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Variable de tipo {@link Long} con los ids del registros , en caso de ser -1
     *          el regitro no fue correcto
     * @author Jorge Omar Tovar Martìnez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Long[] InsertLecturaImagenes(LecturaDTO lecturaDTO){
        Long[] inserts = new Long[lecturaDTO.getImagenesURI().size()];
        SQLiteDatabase db = this.getWritableDatabase();
        for (int x = 0; x<lecturaDTO.getImagenesURI().size();x++) {
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveProceso", lecturaDTO.getClaveProceso());
            contentValues.put("Imagen", lecturaDTO.getImagenes().toString());
            contentValues.put("Url", lecturaDTO.getImagenesURI().toString());
            inserts[x] = db.insert(TABLE_LECTURA_INICIAL_IMAGENES,null,contentValues);
        }
        return inserts;
    }

    /**
     * <h3>GetLecturaImagenesByClaveUnica</h3>
     * Permite consultar  las imagenes de lectura inicial por medio de la
     * clave ùnica, se envia de parametro un {@link String} que reprecenta dicha clave y retornara
     * un objeto de tipo {@link Cursor} con los valores de la consulta en caso de existir.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Cursor GetLecturaImagenesByClaveUnica(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_IMAGENES+
                " WHERE ClaveProceso = '"+ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLecturaImagenes</h3>
     * Permite realizar la eliminación de las imagenes para la lectura inicial,
     * se envia de parametro una cadena de tipo {@link String} que reprecenta
     * la clave única de proceso y en caso de que se elimine retornara el número de registros
     * eliminados en caso contrario,  retornara un -1.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Un valor de tipo {@link Integer} que reprecenta el numero de registro eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 30/08/2018
     */
    public Integer EliminarLecturaImagenes(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_IMAGENES,"ClaveProceso = '"+
                ClaveProceso+"'",null);
    }
    ///endregion

    //region Metodos para lectura final Calibacion

    /**
     * IncertarLecturaFinal
     * Permite realizar el registro de la lectura final , se enviara como parametro un objeto de
     * tipo {@link LecturaDTO} el cual contienen los datos a registrar en local, al finalizar
     * se retornara un valor de tipo {@link Long} en caso de ser mayor a -1 quiere decir que
     * ser registro correctamente
     * @param lecturaDTO Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Variable tipo {@link Long} con el id de registro
     */
    public Long IncertarLecturaFinal(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
        contentValues.put("IdTipoMedidor",lecturaDTO.getIdTipoMedidor());
        contentValues.put("NombreTipoMedidor",lecturaDTO.getNombreTipoMedidor());
        contentValues.put("CantidadFotografiasMedidor",lecturaDTO.getCantidadFotografias());
        contentValues.put("NombreEstacionCarburacion",lecturaDTO.getNombreEstacionCarburacion());
        contentValues.put("IdEstacionCarburacion",lecturaDTO.getIdEstacionCarburacion());
        contentValues.put("PorcentajeMedidor",lecturaDTO.getPorcentajeMedidor());
        contentValues.put("CantidadP5000",lecturaDTO.getCantidadP5000());
        return db.insert(TABLE_LECTURA_FINALIZAR,null,contentValues);
    }

    /**
     * <h3>GetLecturaFinalByClaveProceso</h3>
     * Permite obtener la lectura final de la estación de carburación, se enviara como parametro
     * una cadena de tipo {@link String} que reprecenta la clave de operación y se retornara un
     * objeto de tipo {@link Cursor} con el resultado de la consulta
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     */
    public Cursor GetLecturaFinalByClaveProceso(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINALIZAR+" WHERE ClaveProceso = '"
                +ClaveProceso+"'",null);
    }

    /**
     * <h3>EliminarLecturaFinal</h3>
     * Permite eliminar el registro de la lectura final de la base de datos, se envia como parametro
     * un objeto de tipo {@link String} que reprecentan la clave de operación , al final se
     * retornara un valor de tipo {@link Integer} que reprecenta el numero de registro que se
     * eliminaron.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} que reprecenta el número de registros eliminados
     */
    public Integer EliminarLecturaFinal(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_FINALIZAR,
                " WHERE ClaveProceso = '"+ClaveProceso+"'",null);
    }

    /**
     * <h3>GetLecturasIniciales</h3>
     * Permite retornar todas la lecturas iniciales que se almacenaron en base de datos,
     * retornara un objeto de tipo {@link Cursor} con el resultado de la consulta
     * @return Objeto de tipo {@link Cursor} con los resultados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @date 31/08/2018
     */
    public Cursor GetLecturasFinales() {
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINALIZAR,null);
    }
    //endregion

    //region Metodos para lectura final P5000

    /**
     * <h3>InsertImagenLecturaFinalP5000</h3>
     * Permite realizar el registro de la imagen del P5000 para la lectura final,
     * se tomara como valor un objeto de tipo {@link LecturaDTO} y tras finalizar el registro
     * si es correcto retornara un valor de tipo {@link Long} con el id del registro realizado.
     * @param lecturaDTO  Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Valor de tipo {@link Long} que reprecenta el id del registro
     */
    public Long InsertImagenLecturaFinalP5000(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getReadableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
        contentValues.put("Imagen",lecturaDTO.getImagenP5000());
        contentValues.put("Url",lecturaDTO.getImagenP5000URI().toString());
        return db.insert(TABLE_LECTURA_FINALIZAR_P5000,null,contentValues);
    }

    /**
     * <h3>GetImagenLecturaFinalP5000ByClaveProceso</h3>
     * Permite retornar un objeto de tipo {@link Cursor} con el registro de la imagen
     * del P5000 en la lectura final, se tomara como parametro una cadena de tipo
     * {@link String} que reprecenta la clave unica de proceso
     * @param ClaveProceso {@link String} que reprecenta la clave de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     */
    public Cursor GetImagenLecturaFinalP5000ByClaveProceso(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINALIZAR_P5000+
                " WHERE ClaveProceso = "+ClaveProceso,null);
    }

    /**
     * <h3>EliminarImagenLecturaFinalP5000</h3>
     * Permite realizar la elimicación del registro de la imagen del P5000
     * se requerira como parametro un {@link String} que reprecenta la clave de
     * operación y en caso de que el eliminado sea correcto se retornara una variable
     * de tipo {@link Integer} con el número de registros eliminados.
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} que reprecenta el numero de registros eliminados.
     */
    public Integer EliminarImagenLecturaFinalP5000(String ClaveProceso){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_FINALIZAR_P5000,
                "ClaveProceso = '"+ClaveProceso+"'",null);
    }
    //endregion

    //region Metodos para imagenes lectura final

    /**
     * <h3>IncertImagenesLecturaFinal</h3>
     * Permite realizar el registro de las imagenes para la lectura final de estación
     * se envia como parametro un objeto de tipo {@link LecturaDTO} con los datos, finalmente
     * se retornara un Array de tipo {@link Long} con los ids de los valores registrados
     * @param lecturaDTO Objeto de tipo {@link LecturaDTO} con los valores a registrar
     * @return Array de tipo {@link Long} con los id de los registros ,en caso de ser menor a
     *         cero no se registro
     */
    public Long[] IncertImagenesLecturaFinal(LecturaDTO lecturaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] incerts = new Long[lecturaDTO.getImagenes().size()];
        for (int x = 0;x<lecturaDTO.getImagenes().size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveProceso",lecturaDTO.getClaveProceso());
            contentValues.put("Imagen",lecturaDTO.getImagenes().get(x).toString());
            contentValues.put("Url",lecturaDTO.getImagenesURI().toString());
            incerts[x] = db.insert(TABLE_LECTURA_FINALIZAR_IAMGENES,
                    null,contentValues);
        }
        return incerts;
    }

    /**
     * <h3>GetImagenesLecturaFinalByClaveOperacion</h3>
     * Permite obtener los registros de las imagenes de la lectura final, se envia como parametro
     * un {@link String} con la clave de operación y el metodo retornara un objeto de tipo
     * {@link Cursor} con los valores de la consulta
     * @param ClaveProceso Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     */
    public Cursor GetImagenesLecturaFinalByClaveOperacion(String ClaveProceso){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINALIZAR_IAMGENES+
                " WHERE ClaveProceso = "+ClaveProceso,null);
    }

    /**
     * <h3><EliminarImagenesLecturaFinal</h3>
     * Permite eliminar los registros de las imagenes de la lectura final, se envia como parametro
     * un {@link String} con la clave de operación y al finalizar se retorna un valor de tipo
     * {@link Integer} con el numero de registros eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Integer} que reprecenta el número de registros eliminados
     */
    public Integer EliminarImagenesLecturaFinal(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_FINALIZAR_IAMGENES,
                "ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para la lectura inicial de pipas

    /**
     * <h3>InsertLecturaInicialPipas</h3>
     * Permite realizar el registro en la base de datos de los valores de la lectura inicial de la
     * pipa, se envia de parametro un objeto de tipo {@link LecturaPipaDTO} con los valores a
     * registrar.
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los valores a registrar
     * @return Variable de tipo {@link Long} con el id del registro , en caso de ser -1 quiere decir
     * que no se registro
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long InsertLecturaInicialPipas(LecturaPipaDTO lecturaPipaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("IdPipa",lecturaPipaDTO.getIdPipa());
        contentValues.put("ClaveOperacion",lecturaPipaDTO.getClaveProceso());
        contentValues.put("NombrePipa",lecturaPipaDTO.getNombrePipa());
        contentValues.put("IdTipoMedidor",lecturaPipaDTO.getIdTipoMedidor());
        contentValues.put("CantidadFotografias",lecturaPipaDTO.getCantidadFotografias());
        contentValues.put("CantidadP5000",lecturaPipaDTO.getCantidadP5000());
        contentValues.put("TipoMedidor",lecturaPipaDTO.getTipoMedidor());
        return db.insert(TABLE_LECTURA_INICIAL_PIPA,null,contentValues);
    }

    /**
     * <h3>GetLecturaIncialPipasByClaveOperacion</h3>
     * Permite realizar la biusqueda del registro de una lectrua inicial de pipa por medio
     * de su Clave de operación se nevia de parametro una cadena de {@link String} con dicha
     * clave y retornara un objeto de tipo {@link Cursor} con el resultado de la misma.
     * @param ClaveOperacion Cadena de Cadena de tipo {@link String} que reprecenta la
     *                       clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetLecturaIncialPipasByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_PIPA+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarLecturaInicialPipa</h3>
     * Permite realizar la eliminación de un registro de lectura incial de pipa,
     * se enviara como parametro un {@link String} con la clave de operación y
     * el metodo retornara un valor de tipo {@link Integer} que reprecenta el nùmero de registros
     * eliminados.
     * @param ClaveOperacion Cadena de Cadena de tipo {@link String} que reprecenta la
     *                       clave unica de proceso
     * @return Valor de tipo {@link Integer} que reprecenta el numero de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarLecturaInicialPipa(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_PIPA,
                "ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * GetLecturasIncialesPipas
     * Retorna todos los registros en local de las lecturas iniciales de pipa, se retorna
     * un objeto de tipo {@link Cursor} con los resultados
     * @return Objeto {@link Cursor} con los resultados de la consulta
     */
    public Cursor GetLecturasIncialesPipas(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_PIPA,
                null);
    }
    //endregion

    //region Metodos para las imagenes de la lectura inicial de pipas

    /**
     * <h3>InsertImagenesLecturaInicialPipa</h3>
     * Permite el registro de las imagenes de la lectura incial de la pipa, se envia como parametro
     * un objeto de tipo {@link LecturaPipaDTO} con los datos de la lectura y al final se retorna un
     * array de tipo {@link Long} con los ids registrados.
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los valores a registrar
     * @return Array de tipo {@link Long} con los valores registrados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long[] InsertImagenesLecturaInicialPipa(LecturaPipaDTO lecturaPipaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] inserts = new Long[lecturaPipaDTO.getImagenes().size()];
        for (int x = 0; x<lecturaPipaDTO.getImagenes().size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion",lecturaPipaDTO.getClaveProceso());
            contentValues.put("Imagen", lecturaPipaDTO.getImagenes().get(x));
            contentValues.put("Url",lecturaPipaDTO.getImagenesURI().get(x).toString());
            inserts[x] = db.insert(TABLE_LECTURA_INICIAL_PIPA_IMAGENES,null,contentValues);
        }
        return inserts;
    }

    /**
     * <h3>GetImagenesLecturaInicialPipaByClaveOperacion</h3>
     * Permite retornar un objeto de tipo {@link Cursor} con las imagenes de la lectura
     * inicial de la pipa, se tomara como parametro un {@link String} con la clave de operación.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con los resultados de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetImagenesLecturaInicialPipaByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_PIPA_IMAGENES+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarImagenesLecturaInicialPipas</h3>
     * Permite eliminar los registros de las imagenes de lectura incial de pipas, se enviara una
     * cadena de tipo {@link String} que reprecenta la clave de proceso y al finalizar retornara
     * un valor de tipo {@link Integer} que reprecenta el numero de registros eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} con el total de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarImagenesLecturaInicialPipas(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_PIPA_IMAGENES,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para la lectura inicial del P5000 de las pipas

    /**
     * <H3>InsertLecturaInicialPipaP5000</H3>
     * Permite registrar las imagenes de la lectura incial del P5000 de la pipa, se toma como
     * parametro un {@link String} que reprecenta la clave de operación y retornara un valor
     * de tipo {@link Long} con el id que re registro
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los valores a registrar
     * @return Valor de tipo {@link Long} que reprecenta el id registrado en la base de datos
     *          en caso de ser -1 es que no se pudo registrar
     * @author Jorge Omar Tovart Martínez <jorge.tovar@neoteck.com.mx>
     *
     */
    public Long InsertLecturaInicialPipaP5000(LecturaPipaDTO lecturaPipaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",lecturaPipaDTO.getClaveProceso());
        contentValues.put("Imagen",lecturaPipaDTO.getImagenP5000());
        contentValues.put("Url",lecturaPipaDTO.getImagenP5000URI().toString());
        return db.insert(TABLE_LECTURA_INICIAL_PIPA_P5000,null,contentValues);
    }

    /**
     * <h3>GetLecturaInicialPipaP5000ByClaveOperacion</h3>
     * Permite realizar la consulta de la imagen del P5000 para la lectura inicial de la
     * pipa, se enviara como parametro un {@link String} con la clave de operación y se
     * retornara un objeto de tipo {@link Cursor } con el resultado de la consulta
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     */
    public Cursor GetLecturaInicialPipaP5000ByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+ TABLE_LECTURA_INICIAL_PIPA_P5000+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarLecturaInicialPipaP500</h3>
     * Permite eliminar un registro de la lectura del P5000 de la pipa, se tomara como parametro
     * un {@link String} que reprecenta la clave de operación y al final se retornara un valor
     * de tipo {@link Integer} con el número de registros eliminados
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} con el total de registros eliminados
     */
    public Integer EliminarLecturaInicialPipaP500(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.delete(TABLE_LECTURA_INICIAL_PIPA_P5000," WHERE ClaveOperacion = '"
                +ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para la lectura final de pipas
    /**
     * <h3>InsertLecturaFinalPipas</h3>
     * Permite realizar el registro en la base de datos de los valores de la lectura final de la
     * pipa, se envia de parametro un objeto de tipo {@link LecturaPipaDTO} con los valores a
     * registrar.
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los valores a registrar
     * @return Variable de tipo {@link Long} con el id del registro , en caso de ser -1 quiere decir
     * que no se registro
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long InsertLecturaFinalPipas(LecturaPipaDTO lecturaPipaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("IdPipa",lecturaPipaDTO.getIdPipa());
        contentValues.put("ClaveOperacion",lecturaPipaDTO.getClaveProceso());
        contentValues.put("NombrePipa",lecturaPipaDTO.getNombrePipa());
        contentValues.put("IdTipoMedidor",lecturaPipaDTO.getIdTipoMedidor());
        contentValues.put("CantidadFotografias",lecturaPipaDTO.getCantidadFotografias());
        contentValues.put("CantidadP5000",lecturaPipaDTO.getCantidadP5000());
        contentValues.put("TipoMedidor",lecturaPipaDTO.getTipoMedidor());
        return db.insert(TABLE_LECTURA_FINAL_PIPA,null,contentValues);
    }

    /**
     * <h3>GetLecturaFinalPipasByClaveOperacion</h3>
     * Permite realizar la biusqueda del registro de una lectrua final de pipa por medio
     * de su Clave de operación se nevia de parametro una cadena de {@link String} con dicha
     * clave y retornara un objeto de tipo {@link Cursor} con el resultado de la misma.
     * @param ClaveOperacion Cadena de Cadena de tipo {@link String} que reprecenta la
     *                       clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetLecturaFinalPipasByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINAL_PIPA+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarLecturaFinalPipa</h3>
     * Permite realizar la eliminación de un registro de lectura final de pipa,
     * se enviara como parametro un {@link String} con la clave de operación y
     * el metodo retornara un valor de tipo {@link Integer} que reprecenta el nùmero de registros
     * eliminados.
     * @param ClaveOperacion Cadena de Cadena de tipo {@link String} que reprecenta la
     *                       clave unica de proceso
     * @return Valor de tipo {@link Integer} que reprecenta el numero de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarLecturaFinalPipa(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_FINAL_PIPA,
                "ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * Obtiene todos los registros de las lecturas finales de pipa, retornara un objeto
     * de tipo {@link Cursor} con los datos resultantes
     * @return Objeto {@link Cursor} con el resultado de la consulta
     */
    public Cursor GetLecturasFinaesPipas(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_LECTURA_FINAL_PIPA,
                null);
    }
    //endregion

    //region Metodos para las imagenes de la lectura final de pipas
    /**
     * <h3>InsertImagenerLecturaInicialPipa</h3>
     * Permite el registro de las imagenes de la lectura incial de la pipa, se envia como parametro
     * un objeto de tipo {@link LecturaPipaDTO} con los datos de la lectura y al final se retorna un
     * array de tipo {@link Long} con los ids registrados.
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} con los valores a registrar
     * @return Array de tipo {@link Long} con los valores registrados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long[] InsertImagenesLecturaFinalPipa(LecturaPipaDTO lecturaPipaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] inserts = new Long[lecturaPipaDTO.getImagenes().size()];
        for (int x = 0; x<lecturaPipaDTO.getImagenes().size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion",lecturaPipaDTO.getClaveProceso());
            contentValues.put("Imagen", lecturaPipaDTO.getImagenes().get(x));
            contentValues.put("Url",lecturaPipaDTO.getImagenesURI().get(x).toString());
            inserts[x] = db.insert(TABLE_LECTURA_FINAL_PIPA_IMAGENES,null,contentValues);
        }
        return inserts;
    }

    /**
     * <h3>GetImagenesLecturaInicialPipaByClaveOperacion</h3>
     * Permite retornar un objeto de tipo {@link Cursor} con las imagenes de la lectura
     * inicial de la pipa, se tomara como parametro un {@link String} con la clave de operación.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con los resultados de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetImagenesLecturaFinalPipaByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINAL_PIPA_IMAGENES+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarImagenesLecturaInicialPipas</h3>
     * Permite eliminar los registros de las imagenes de lectura incial de pipas, se enviara una
     * cadena de tipo {@link String} que reprecenta la clave de proceso y al finalizar retornara
     * un valor de tipo {@link Integer} que reprecenta el numero de registros eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} con el total de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarImagenesLecturaFinalPipas(String ClaveOperacion){
        SQLiteDatabase db = this.getWritableDatabase();
        return db.delete(TABLE_LECTURA_FINAL_PIPA_IMAGENES,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para las images del P5000 de la lectura final
    public Long InsertLecturaFinalPipaP5000(LecturaPipaDTO lecturaPipaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",lecturaPipaDTO.getClaveProceso());
        contentValues.put("Imagen",lecturaPipaDTO.getImagenP5000());
        contentValues.put("Url",lecturaPipaDTO.getImagenP5000URI().toString());
        return db.insert(TABLE_LECTURA_FINAL_PIPA_P5000,null,contentValues);
    }

    /**
     * <h3>GetLecturaInicialPipaP5000ByClaveOperacion</h3>
     * Permite realizar la consulta de la imagen del P5000 para la lectura inicial de la
     * pipa, se enviara como parametro un {@link String} con la clave de operación y se
     * retornara un objeto de tipo {@link Cursor } con el resultado de la consulta
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     */
    public Cursor GetLecturaFinalPipaP5000ByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+ TABLE_LECTURA_FINAL_PIPA_P5000+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarLecturaInicialPipaP500</h3>
     * Permite eliminar un registro de la lectura del P5000 de la pipa, se tomara como parametro
     * un {@link String} que reprecenta la clave de operación y al final se retornara un valor
     * de tipo {@link Integer} con el número de registros eliminados
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} con el total de registros eliminados
     */
    public Integer EliminarLecturaFinalPipaP500(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.delete(TABLE_LECTURA_FINAL_PIPA_P5000," WHERE ClaveOperacion = '"
                +ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para la lectura incial de almacen

    /**
     * <h3>InsertLecturaInicialAlmacen</h3>
     * Permite realizar el registro de una nueva lectura inicial para el almacen en la base de
     * datos, se envia como parametro un objeto de tipo {@link LecturaAlmacenDTO} con los datos
     * a registrar tras terminar, el metodo retornara un valor de tipo {@link Long} en caso de
     * que este se registre correctamente se retornara el id de insercion, en caso de que no
     * retornara un -1
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los valores a registrar
     * @return Un valor de tipo {@link Long} que reprecenta el id del registro, en caso de que no
     *          retornara un -1
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long InsertLecturaInicialAlmacen(LecturaAlmacenDTO lecturaAlmacenDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",lecturaAlmacenDTO.getClaveOperacion());
        contentValues.put("IdAlmacen",lecturaAlmacenDTO.getIdAlmacen());
        contentValues.put("NombreAlmacen",lecturaAlmacenDTO.getNombreAlmacen());
        contentValues.put("IdTipoMedidor",lecturaAlmacenDTO.getIdTipoMedior());
        contentValues.put("NombreTipoMedidor",lecturaAlmacenDTO.getNombreTipoMedidor());
        contentValues.put("CantidadFotografias",lecturaAlmacenDTO.getCantidadFotografias());
        contentValues.put("PorcentajeMedidor",lecturaAlmacenDTO.getPorcentajeMedidor());
        return  db.insert(TABLE_LECTURA_INICIAL_ALMACEN,null,contentValues);
    }

    /**
     * <h3>GetLecturaInicialAlmacenByClaveOperacion</h3>
     * Permite obtener un registro de la lectura inicial del almacen, se tomara como parametro un
     * {@link String} que reprecenta la clave de operación y tras consultar se retornara un objeto
     * de tipo {@link Cursor} con el resultado de la consulta.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetLecturaInicialAlmacenByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_INICIAL_ALMACEN+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarLecturaIncialAlmacen</h3>
     * Permite eliminar un registro de la lectura inicial del almacen , se envia como parametro
     * un {@link String} con la clave de operación , tras finalizar se retornara un objeto de
     * tipo {@link Integer} con el total de registros eliminados
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} con el total de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarLecturaIncialAlmacen(String ClaveOperacion){
        return this.getReadableDatabase().delete(TABLE_LECTURA_INICIAL_ALMACEN,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * Permite obtener todas las lecturas iniciales de almacenses, retornara
     * un objeto de tipo {@link Cursor} con los datos obtenidos
     * @return Objeto de tipo {@link Cursor} con los datos
     */
    public Cursor GetLecturasIncialesAlmacen(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_LECTURA_INICIAL_ALMACEN,null);
    }
    //endregion

    //region Metodos para las imagenes de la lectura inicial de almacen

    /**
     * <h3>InsertImagenesLecturaInicialAlamacen</h3>
     * Permite realizar el registro en la base de datos de las imagenes que se registran en la
     * lectura , se toma como parametro un objeto de tipo {@link LecturaAlmacenDTO} con los datos
     * a registrar y en caso de que se realizen correctamente los incerts por cada imagen
     * se retornara un array de tipo {@link Long} con los id de dicho registros
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los valores a registrar
     * @return Array de tipo {@link Long} con los ids registrados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long[] InsertImagenesLecturaInicialAlamacen(LecturaAlmacenDTO lecturaAlmacenDTO){
        SQLiteDatabase db =this.getWritableDatabase();
        Long[] _inserts = new Long[lecturaAlmacenDTO.getCantidadFotografias()];
        for (int x=0; x<lecturaAlmacenDTO.getImagenes().size();x++) {
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion", lecturaAlmacenDTO.getClaveOperacion());
            contentValues.put("Imagen", lecturaAlmacenDTO.getImagenes().get(x));
            contentValues.put("Url", lecturaAlmacenDTO.getImagenesURI().get(x).toString());
            _inserts[x] = db.insert(TABLE_LECTURA_INICIAL_ALMACEN_IMAGENES,
                    null,contentValues);
        }
        return _inserts;
    }

    /**
     * <h3>GetImagenesLecturaInicialAlmacenByClaveOperacion</h3>
     * Permite obtener los registros de las imagenes de la lectura incial del almacen
     * se tomara como parametro un {@link String} con la clave de operación y se retornara
     * un objeto de tipo {@link Cursor} con el resultado de la consulta
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetImagenesLecturaInicialAlmacenByClaveOperacion(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_LECTURA_FINAL_ALMACEN_IMAGENES+" WHERE ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }

    /**
     * <h3>EliminarImagenesLecturaInicialAlmacen</h3>
     * Permite realizar la eliminacion de los registros de la lectura inicial del almacen,
     * se tomara como parametro un {@link String} con la clave de operación y se retornara un
     * valor de tipo {@link Integer} con el total de registros eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Long} con el total de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarImagenesLecturaInicialAlmacen(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_LECTURA_INICIAL_ALMACEN_IMAGENES,
                " ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para la lectura final de almacen
    /**
     * <h3>InsertLecturFinalAlmacen</h3>
     * Permite realizar el registro de una nueva lectura inicial para el almacen en la base de
     * datos, se envia como parametro un objeto de tipo {@link LecturaAlmacenDTO} con los datos
     * a registrar tras terminar, el metodo retornara un valor de tipo {@link Long} en caso de
     * que este se registre correctamente se retornara el id de insercion, en caso de que no
     * retornara un -1
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los valores a registrar
     * @return Un valor de tipo {@link Long} que reprecenta el id del registro, en caso de que no
     *          retornara un -1
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long InsertLecturaFinalAlmacen(LecturaAlmacenDTO lecturaAlmacenDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",lecturaAlmacenDTO.getClaveOperacion());
        contentValues.put("IdAlmacen",lecturaAlmacenDTO.getIdAlmacen());
        contentValues.put("NombreAlmacen",lecturaAlmacenDTO.getNombreAlmacen());
        contentValues.put("IdTipoMedidor",lecturaAlmacenDTO.getIdTipoMedior());
        contentValues.put("NombreTipoMedidor",lecturaAlmacenDTO.getNombreTipoMedidor());
        contentValues.put("CantidadFotografias",lecturaAlmacenDTO.getCantidadFotografias());
        contentValues.put("PorcentajeMedidor",lecturaAlmacenDTO.getPorcentajeMedidor());
        return  db.insert(TABLE_LECTURA_FINAL_ALMACEN,null,contentValues);
    }

    /**
     * <h3>GetLecturaFinalAlmacenByClaveOperacion</h3>
     * Permite obtener un registro de la lectura inicial del almacen, se tomara como parametro un
     * {@link String} que reprecenta la clave de operación y tras consultar se retornara un objeto
     * de tipo {@link Cursor} con el resultado de la consulta.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetLecturaFinalAlmacenByClaveOperacion(String ClaveOperacion){
        SQLiteDatabase db = this.getReadableDatabase();
        return db.rawQuery("SELECT * FROM "+TABLE_LECTURA_FINAL_ALMACEN+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * <h3>EliminarLecturaFinalAlmacen</h3>
     * Permite eliminar un registro de la lectura inicial del almacen , se envia como parametro
     * un {@link String} con la clave de operación , tras finalizar se retornara un objeto de
     * tipo {@link Integer} con el total de registros eliminados
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} con el total de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarLecturaFinalAlmacen(String ClaveOperacion){
        return this.getReadableDatabase().delete(TABLE_LECTURA_FINAL_ALMACEN,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * Permite consultar todos los resultados de las lecturas finales de almacen, se
     * retornara un objeto de tipo {@link Cursor} con los datos consultados
     * @return Objeto {@link Cursor} con los resultados de la consulta
     */
    public Cursor GetLecturasFinalesAlmacen(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_LECTURA_FINAL_ALMACEN,null);
    }
    //endregion

    //region Metodos para las imagenes de la lectura final de almacen
    /**
     * <h3>InsertImagenesLecturaFinalAlamacen</h3>
     * Permite realizar el registro en la base de datos de las imagenes que se registran en la
     * lectura  , se toma como parametro un objeto de tipo {@link LecturaAlmacenDTO} con los datos
     * a registrar y en caso de que se realizen correctamente los incerts por cada imagen
     * se retornara un array de tipo {@link Long} con los id de dicho registros
     * @param lecturaAlmacenDTO Objeto de tipo {@link LecturaAlmacenDTO} con los valores a registrar
     * @return Array de tipo {@link Long} con los ids registrados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Long[] InsertImagenesLecturaFinalAlamacen(LecturaAlmacenDTO lecturaAlmacenDTO){
        SQLiteDatabase db =this.getWritableDatabase();
        Long[] _inserts = new Long[lecturaAlmacenDTO.getCantidadFotografias()];
        for (int x=0; x<lecturaAlmacenDTO.getImagenes().size();x++) {
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion", lecturaAlmacenDTO.getClaveOperacion());
            contentValues.put("Imagen", lecturaAlmacenDTO.getImagenes().get(x));
            contentValues.put("Url", lecturaAlmacenDTO.getImagenesURI().get(x).toString());
            _inserts[x] = db.insert(TABLE_LECTURA_FINAL_ALMACEN_IMAGENES,
                    null,contentValues);
        }
        return _inserts;
    }

    /**
     * <h3>GetImagenesLecturaFinalAlmacenByClaveOperacion</h3>
     * Permite obtener los registros de las imagenes de la lectura final del almacen
     * se tomara como parametro un {@link String} con la clave de operación y se retornara
     * un objeto de tipo {@link Cursor} con el resultado de la consulta
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Cursor GetImagenesLecturaFinalAlmacenByClaveOperacion(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                        TABLE_LECTURA_FINAL_ALMACEN_IMAGENES+" WHERE ClaveOperacion = '"
                        +ClaveOperacion+"'",
                null);
    }

    /**
     * <h3>EliminarImagenesLecturaFinalAlmacen</h3>
     * Permite realizar la eliminacion de los registros de la lectura final del almacen,
     * se tomara como parametro un {@link String} con la clave de operación y se retornara un
     * valor de tipo {@link Integer} con el total de registros eliminados.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Long} con el total de registros eliminados
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    public Integer EliminarImagenesLecturaFinalAlmacen(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_LECTURA_INICIAL_ALMACEN_IMAGENES,
                " ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para el registro de la lectura inicial de la camioneta
    public Long InsertLecturaInicialCamioneta(LecturaCamionetaDTO lecturaCamionetaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",lecturaCamionetaDTO.getClaveOperacion());
        contentValues.put("IdCamioneta",lecturaCamionetaDTO.getIdCamioneta());
        contentValues.put("NombreCamioneta",lecturaCamionetaDTO.getNombreCamioneta());
        contentValues.put("FechaAplicacion",lecturaCamionetaDTO.getFechaAplicacion());
        return db.insert(TABLE_LECTURA_INICIAL_CAMIONETA,null,contentValues);
    }

    public Cursor GetLecturaInicialCamionetaByClaveOperacion(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery(
                "SELECT * FROM "+TABLE_LECTURA_INICIAL_CAMIONETA+
        " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    public Integer EliminarLecturaInicialCamioneta(String ClaveOperacion){
        return this.getWritableDatabase().delete(
                TABLE_LECTURA_INICIAL_CAMIONETA,
                " ClaveOperacion = '"+ClaveOperacion+"'",
                null
                );
    }

    /**
     * Permite obtener todas las lecturas iniciales del la camioneta , se retornara
     * un objeto de tipo {@link Cursor} con los datos resultantes de la consulta
     * @return Objeto {@link Cursor} con los datos de la consulta
     */
    public Cursor GetLecturasIncialesCamioneta(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_LECTURA_INICIAL_CAMIONETA,null);
    }
    //endregion

    //region Metodos para el registro de los cilindros para la lectura inicial de la camioneta
    public Long[] InsertCilindrosLecturaInicialCamioneta(LecturaCamionetaDTO lecturaCamionetaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] inserts = new Long[lecturaCamionetaDTO.getCilindros().size()];
        for (int x=0; x<lecturaCamionetaDTO.getCilindros().size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion",lecturaCamionetaDTO.getClaveOperacion());
            contentValues.put("IdCilindro",lecturaCamionetaDTO.getCilindros().get(x)
                    .getIdCilindro());
            contentValues.put("CilindroKg",lecturaCamionetaDTO.getCilindros().get(x)
                    .getCilindroKg());
            contentValues.put("Cantidad",lecturaCamionetaDTO.getCilindros().get(x).getCantidad());
            inserts[x] = db.insert(TABLE_LECTURA_INICIAL_CAMIONETA_CILINDROS,
                    null,contentValues);
        }
        return inserts;
    }

    public Cursor GetCilindrosLecturaInicialCamioneta(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_LECTURA_INICIAL_CAMIONETA_CILINDROS+" WHERE ClaveOperacion = '"+ClaveOperacion
                        +"'",
                null);
    }

    public Integer EliminarCilindrosLecturaInicialCamioneta(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_LECTURA_INICIAL_CAMIONETA_CILINDROS,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para el registro de la lectura final de la camioneta
    public Long InsertLecturaFinalCamioneta(LecturaCamionetaDTO lecturaCamionetaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues contentValues = new ContentValues();
        contentValues.put("ClaveOperacion",lecturaCamionetaDTO.getClaveOperacion());
        contentValues.put("IdCamioneta",lecturaCamionetaDTO.getIdCamioneta());
        contentValues.put("EsEncargadoPuerta",lecturaCamionetaDTO.isEsEncargadoPuerta()?1:0);
        contentValues.put("NombreCamioneta",lecturaCamionetaDTO.getNombreCamioneta());
        return db.insert(TABLE_LECTURA_FINAL_CAMIONETA,null,contentValues);
    }

    public Cursor GetLecturaFinalCamionetaByClaveOperacion(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery(
                "SELECT * FROM "+TABLE_LECTURA_FINAL_CAMIONETA+
                        " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    public Integer EliminarLecturaFinalCamioneta(String ClaveOperacion){
        return this.getWritableDatabase().delete(
                TABLE_LECTURA_FINAL_CAMIONETA,
                " ClaveOperacion = '"+ClaveOperacion+"'",
                null
        );
    }

    /**
     * Permite obtener las lecturas finales de las camionetas , se retornara un
     * objeto de tipo {@link Cursor} con los resultados del la consulta
     * @return Objeto de tipo {@link Cursor} con los datos
     */
    public Cursor GetLecturaFinalCamionetas(){
        return  this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_LECTURA_FINAL_CAMIONETA,null);
    }
    //endregion

    //region Metodos para el registro de los cilindros para la lectura final de la camioneta
    public Long[] InsertCilindrosLecturaFinalCamioneta(LecturaCamionetaDTO lecturaCamionetaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] inserts = new Long[lecturaCamionetaDTO.getCilindros().size()];
        for (int x=0; x<lecturaCamionetaDTO.getCilindros().size();x++){
            ContentValues contentValues = new ContentValues();
            contentValues.put("ClaveOperacion",lecturaCamionetaDTO.getClaveOperacion());
            contentValues.put("IdCilindro",lecturaCamionetaDTO.getCilindros().get(x)
                    .getIdCilindro());
            contentValues.put("CilindroKg",lecturaCamionetaDTO.getCilindros().get(x)
                    .getCilindroKg());
            contentValues.put("Cantidad",lecturaCamionetaDTO.getCilindros().get(x).getCantidad());
            inserts[x] = db.insert(TABLE_LECTURA_FINAL_CAMIONETA_CILINDROS,
                    null,contentValues);
        }
        return inserts;
    }

    public Cursor GetCilindrosLecturaFinalCamioneta(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                        TABLE_LECTURA_FINAL_CAMIONETA_CILINDROS+" WHERE ClaveOperacion = '"+
                        ClaveOperacion+"'",
                null);
    }

    public Integer EliminarCilindrosLecturaFinalCamioneta(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_LECTURA_FINAL_CAMIONETA_CILINDROS,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para el registro de la recarga

    /**
     * <h3>InsertRecarga</h3>
     * <i>Permite realizar el registro de una recarga , tomara como
     * parametro un objeto de tipo {@link RecargaDTO} el cual contendra los datos de dicha
     * recarga cabe destacar, que algunos de los datos puede que no este al ser de camioneta, pipa
     * o estación de carburación.</i>
     * <p>Ejemplo:</p>
     * <pre>
     *     Long IdRecarga = SAGASsql.InsertRecarga(recargaDTO);
     * </pre>
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los valores a registrar
     * @param tipo String que reprecenta el tipo de recarga
     * @param EsRecargaEstacionInicial De termina si es una recarga inicial
     * @return Valor de tipo {@link Long} que reprecenta el id en la base de datos
     *
     */
    public Long InsertRecarga(RecargaDTO recargaDTO,String tipo, boolean EsRecargaEstacionInicial){
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("IdCAlmacenGasSalida",recargaDTO.getIdCAlmacenGasSalida());
        values.put("IdCAlmacenGasEntrada",recargaDTO.getIdCAlmacenGasEntrada());
        values.put("IdTipoMedidorSalida",recargaDTO.getIdTipoMedidorSalida());
        values.put("IdTipoMedidorEntrada",recargaDTO.getIdTipoMedidorEntrada());
        values.put("IdTipoEvento",recargaDTO.getIdTipoEvento());
        values.put("P5000Salida",recargaDTO.getP5000Salida());
        values.put("P5000Entrada",recargaDTO.getP5000Entrada());
        values.put("ClaveOperacion",recargaDTO.getClaveOperacion());
        values.put("EsTipo",tipo);
        values.put("FechaAplicacion",recargaDTO.getFechaApliacacion());
        values.put("EsInicial",EsRecargaEstacionInicial);
        return db.insert(TABLE_RECARGAS,null,values);
    }

    /**
     * <h3>GetRecargaByClaveOperacion</h3>
     * Permite realizar la consulta de la recarga por medio de una clave única la cual
     * se enviara como parametro de tipo {@link String} , al final el metodo retornara un
     * objeto de tipo {@link Cursor} con los resultados de la consulta.
     * <p>Ejemplo:</p>
     * <pre>
     *     Cursor cursor = SAGASsql.GetRecargaByClaveOperacion("RC20180911083602");
     * </pre>
     * @param ClaveOperacion  Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Cursor} con la consulta resultante 
     */
    public Cursor GetRecargaByClaveOperacion(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_RECARGAS+
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     * Permite realizar la elimicación de una recarga, se envia como parametro
     * una cadena {@link String} con la clave de operacion, al final se retorna
     * un objeto de tipo {@link Integer} con el total de registros afectados.
     *
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Objeto de tipo {@link Integer} con el total de registros eliminados
     */
    public Integer EliminarRecarga(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_RECARGAS,
                "ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     *
     * @return
     */
    public Cursor GetRecargas(String tipo) {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_RECARGAS+
                " WHERE EsTipo = '"+tipo+"'",null);
    }
    //endregion

    //region Metodos para el registro de las imagenes de la recarga

    /**
     * <h3>InsertarImagesRecarga<h3/>
     * Permite el registro de la recarga, se envia como parametro
     * un objeto de tipo {@link RecargaDTO} con los valores a registrar
     * y tras finalizar retornara un array de tipo {@link Long} con los
     * ids registrados.
     * @param recargaDTO  Objeto de tipo {@link RecargaDTO} con los valores a registrar
     * @return Array de tipo {@link Long} con los id registrados, en caso de ser menor
     *          no se pudo registrar
     */
    public Long[] InsertarImagesRecarga(RecargaDTO recargaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] _inserts = new Long[recargaDTO.getImagenes().size()];
        for (int x =0; x<recargaDTO.getImagenes().size();x++){
            ContentValues values = new ContentValues();
            values.put("ClaveOperacion",recargaDTO.getClaveOperacion());
            values.put("Imagen",recargaDTO.getImagenes().get(x));
            values.put("Url",recargaDTO.getImagenesUri().get(x).toString());
            _inserts[x] = db.insert(TABLE_RECARGAS_IMAGENES,null,values);
        }
        return _inserts;
    }

    /**
     * <h3>GetImagenesRecarga</h3>
     * Permite obtener las imagenes de la recarga
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Retorna un objeto de tipo {@link Cursor} con la consulta
     */
    public Cursor GetImagenesRecarga(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_RECARGAS_IMAGENES+" WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    /**
     *<h3>EliminarImagenesRecarga</h3>
     * Permite eliminar una imagen de recarga , se envia de parametro la clave
     * de operación.
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Variable de tipo {@link Integer} con el resultado de los registros eliminados
     */
    public Integer EliminarImagenesRecarga(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_RECARGAS_IMAGENES,
                " WHERE ClaveOperacion = '"+ClaveOperacion+"'",null);
    }
    //endregion

    //region Metodos para el registro de los cilindros de la recarga

    /**
     * <h3>InsertarCilindrosRecarga</h3>
     * Permite registrar los cilindros de una recarga , se enviara
     * como parametro un objeto {@link RecargaDTO} con los valores de la recarga
     * @param recargaDTO Objeto de tipo {@link RecargaDTO} con los valores a registrar
     * @return Array de tipo {@link Integer} con los id registrados
     */
    public Long[] InsertarCilindrosRecarga(RecargaDTO recargaDTO){
        SQLiteDatabase db = this.getWritableDatabase();
        Long[] _inserts = new Long[recargaDTO.getCilindros().size()];
        for (int x =0; x<recargaDTO.getCilindros().size();x++){
            ContentValues values = new ContentValues();
            values.put("ClaveOperacion",recargaDTO.getClaveOperacion());
            values.put("IdCilindro",recargaDTO.getCilindros().get(x).getIdCilindro());
            values.put("Cantidad",recargaDTO.getCilindros().get(x).getCantidad());
            values.put("CilindroKg",recargaDTO.getCilindros().get(x).getCilindroKg());
            _inserts[x] = db.insert(TABLE_RECARGAS_CILINDROS,null,values);
        }
        return _inserts;
    }

    /**
     * <h3>GetCilindrosRecarga</h3>
     * Permite obtener el listado de cilindros, se enviara de parametro un {@link String}
     * con la clave de operación
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Retorna un objeto de tipo {@link Cursor} con los cilindros
     */
    public Cursor GetCilindrosRecarga(String ClaveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "
                +TABLE_RECARGAS_CILINDROS+" WHERE  ClaveOperacion = '"+ClaveOperacion+"'",
                null);
    }

    /**
     * <h3>EliminarCilindrosRecarga</h3>
     * Permite eliminar los registros de cilindros por medio de la clave de operación
     * @param ClaveOperacion Cadena de tipo {@link String} que reprecenta la clave unica de proceso
     * @return Valor de tipo {@link Integer} que reprecenta el total de registros eliminados
     */
    public Integer EliminarCilindrosRecarga(String ClaveOperacion){
        return this.getWritableDatabase().delete(TABLE_RECARGAS_CILINDROS,
                " WHERE  ClaveOperacion = '"+ClaveOperacion+"'",null);
    }

    public Long InsertarVenta(VentaDTO ventaDTO, boolean esCamioneta, boolean esEstacion,
                              boolean esPipa) {

        ContentValues values = new ContentValues();
        values.put("FolioVenta",ventaDTO.getFolioVenta());
        values.put("IdCliente",ventaDTO.getIdCliente());
        values.put("Subtotal",ventaDTO.getTotal());
        values.put("Iva",ventaDTO.getIva());
        values.put("Total",ventaDTO.getTotal());
        values.put("Factura",ventaDTO.isFactura());
        values.put("Credito",ventaDTO.isCredito());
        values.put("Efectivo",ventaDTO.getEfectivo());
        values.put("Fecha",ventaDTO.getFecha());
        values.put("Hora",ventaDTO.getHora());
        values.put("Cambio",ventaDTO.getCambio());
        values.put("SinNumero",ventaDTO.isCredito());
        values.put("EsCamioneta",esCamioneta);
        values.put("EsEstacion",esEstacion);
        values.put("EsPipa",esPipa);
        return this.getWritableDatabase().insert(
                TABLE_VENTAS,
                null,
                values
        );
    }

    public Long[] InsertarConcepto(VentaDTO ventaDTO) {
        int size = ventaDTO.getConcepto().size();
        Long[] _result = new Long[size];
        try {
            for (int x = 0; x < size; x++) {
                ContentValues values = new ContentValues();
                ConceptoDTO conceptoDTO = ventaDTO.getConcepto().get(x);
                values.put("FolioVenta", ventaDTO.getFolioVenta());
                values.put("IdTipoGas", conceptoDTO.getIdTipoGas());
                values.put("Cantidad", conceptoDTO.getCantidad());
                values.put("PUnitario", conceptoDTO.getPUnitario());
                values.put("Descuento", conceptoDTO.getDescuento());
                values.put("Subtotal", conceptoDTO.getSubtotal());
                values.put("IdCategoria", conceptoDTO.getIdCategoria());
                values.put("IdLinea", conceptoDTO.getIdLinea());
                values.put("IdProducto", conceptoDTO.getIdProducto());
                values.put("Concepto", conceptoDTO.getConcepto());

                values.put("Year", conceptoDTO.getYear());
                values.put("Mes", conceptoDTO.getMes());
                values.put("Dia", conceptoDTO.getDia());
                values.put("IdProducto", conceptoDTO.getIdProducto());
                values.put("IdLinea", conceptoDTO.getIdLinea());
                values.put("IdCategoria", conceptoDTO.getIdCategoria());
                values.put("IdUnidadMedida", conceptoDTO.getIdUnidadmedida());
                values.put("PrecioUnitarioProducto", conceptoDTO.getPrecioUnitarioProducto());
                values.put("PrecioUnitarioLt", conceptoDTO.getPrecioUnitarioLt());
                values.put("PrecioUnitatioKg", conceptoDTO.getPrecioUnitarioKg());
                values.put("DescuentoUnitarioProducto", conceptoDTO.getDescuentoUnitarioProducto());
                values.put("DescuentoUnitarioLt", conceptoDTO.getDescuentoUnitarioLt());
                values.put("DescuentoUnitarioKg", conceptoDTO.getDescuentoUnitarioKg());
                values.put("Cantidad", conceptoDTO.getCantidad());
                values.put("CantidadLt", conceptoDTO.getCantidadLt());
                values.put("CantidadKg", conceptoDTO.getCantidadKg());
                values.put("DescuentoTotal", conceptoDTO.getDescuentoTotal());
                values.put("IdEmpresa", conceptoDTO.getIdEmpresa());
                _result[x] = this.getWritableDatabase().insert(TABLE_VENTAS_CONCEPTO,
                        null, values);
            }
        }catch (Exception ex){
            Log.e("Error sql",ex.getMessage());
            ex.printStackTrace();
        }
        return _result;
    }

    public Cursor GetVentas() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_VENTAS,
                null);
    }

    public Cursor GetVentaConcepto(String folioVenta) {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_VENTAS_CONCEPTO+
                " WHERE FolioVenta = '"+folioVenta+"'",null);
    }

    public Integer EliminarVenta(String folioVenta) {
        return this.getWritableDatabase().delete(
                TABLE_VENTAS,
                " FolioVenta = '"+folioVenta+"'",
                null
        );
    }

    public Integer EliminarVentaConcepto(String folioVenta) {
        return this.getWritableDatabase().delete(
                TABLE_VENTAS_CONCEPTO,
                " FolioVenta = '"+folioVenta+"'",
                null
        );
    }

    public Cursor GetVenta(String folioVenta) {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_VENTAS+
                        " WHERE FolioVenta = '"+folioVenta+"'",
                null);
    }

    //endregion

    //region Metodos para el registro del reporte
    /*public Long InsertReporte (String ){

    }*/
    //endregion

    //region Metodos para el autoconsumo

    /**
     * <h3>GetAutoconsumo</h3>
     * Permite retornar un autoconsumoque coincida con la clave de operación
     * que es enviada como parametro, retornara un objeto de tipo {@link Cursor}
     * con el resultado obtenido.
     * @param claveOperacion {@link String} que reprecenta la clave de operacion de autoconsumo
     * @return Objeto de tipo {@link Cursor} con el resultado de la consulta
     * @author Jorge Omar Tovar Martínez (jorge.tovar@neoteck.com.mx)
     */
    public Cursor GetAutoconsumo(String claveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM " +TABLE_AUTOCONSUMO+
                        " WHERE ClaveOperacion = '"+claveOperacion+"'",
                null);
    }

    /**
     * <h3>EliminarAutoconsumo</h3>
     * Permite eliminar un autoconsumo por medio de la clave de operación , retornara un
     * valor de tipo int con el numero total de registros eliminados
     * @param claveOperacion {@link String} que reprecenta la clave de operacion de autoconsumo
     * @return Número total de registros eliminados
     * @author Jorge Omar Tovar Martínez (jorge.tovar@neoteck.com.mx)
     */
    public int EliminarAutoconsumo(String claveOperacion){
        return this.getWritableDatabase().delete(TABLE_AUTOCONSUMO,
                " ClaveOperacion = '"+claveOperacion+"'",null);
    }

    /**
     * <h3>InsertarAutoconsumo</h3>
     * Permite realizar el registro de un autoconsumo, tomara como parametro un
     * objeto de la clase {@link AutoconsumoDTO} con los datos a registrar,
     * retornara un valor de tipo long que repreceta el id del registro en la base de datos
     *
     * @param autoconsumoDTO Objeto DTO con los valores a registrar
     * @return Id del registro creado , en caso de que de -1 no se registro
     */
    public Long InsertarAutoconsumo(AutoconsumoDTO autoconsumoDTO,String tipo,boolean esFinal){
        ContentValues values = new ContentValues();
        values.put("ClaveOperacion",autoconsumoDTO.getClaveOperacion());
        values.put("CantidadFotos",autoconsumoDTO.getCantidadFotos());
        values.put("IdCAlmacenGasSalida",autoconsumoDTO.getIdCAlmacenGasSalida());
        values.put("IdCAlmacenGasEntrada",autoconsumoDTO.getIdCAlmacenGasEntrada());
        values.put("NombreTipoMedidor",autoconsumoDTO.getNombreTipoMedidor());
        values.put("P5000Salida",autoconsumoDTO.getP5000Salida());
        values.put("PorcentajeMedidor",autoconsumoDTO.getPorcentajeMedidor());
        values.put("Tipo",tipo);
        values.put("EsFinal",esFinal);
        values.put("IdTipoMedidor",autoconsumoDTO.getIdTipoMedidor());
        values.put("FechaAplicacion",autoconsumoDTO.getFechaAplicacion());
        return this.getWritableDatabase().insert(TABLE_AUTOCONSUMO,null,values);
    }

    /**
     * <h3>GetImagenesAutoconsumo</h3>
     * Permite retornar las imagenes que se tomaron en el autoconsumo,
     * retornara un objeto de tipo {@link Cursor} con esta información
     * @param claveOperacion {@link String} que reprecenta la clave de operacion de autoconsumo
     * @return Objeto de tipo {@link Cursor} con las imagenes
     */
    public Cursor GetImagenesAutoconsumo(String claveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+
                TABLE_AUTOCONSUMO_IMAGENES+" WHERE ClaveOperacion = '"+claveOperacion+"'",
                null);
    }

    /**
     * <h3>EliminarImagenesAutoconsumo</h3>
     * Permite realizar la eliminación de los registros de las imagenes del autoconsumo
     * por medio de la clave de operacion que es enviada como parametro, el metodo retornara
     * un valor int con el numero de regisatros eliminados.
     *
     * @param claveOperacion {@link String} que reprecenta la clave de operacion de autoconsumo
     * @return Número de registos eliminados
     * @author Jorge Omar Tovar Martínez (jorge.tovar@neoteck.com.mx)
     */
    public int EliminarImagenesAutoconsumo(String claveOperacion){
        //this.getWritableDatabase().execSQL("VACUUM;");
        return this.getWritableDatabase().delete(TABLE_AUTOCONSUMO_IMAGENES,
                " ClaveOperacion = '"+claveOperacion+"'",null);
    }

    /**
     * <h3>InsertarImagenesAutoconsumo</h3>
     * Permite realizar el registro de las imagenes de autoconsumo, retornara un
     * array de tipo {@link Long} con los id de los registros en caso de ser exitosos.
     * @param autoconsumoDTO Objeto DTO con los valores a registrar
     * @return Array de tipo {@link Long} con los id registrados
     */
    public Long[] InsertarImagenesAutoconsumo(AutoconsumoDTO autoconsumoDTO){
        int fotos = autoconsumoDTO.getImagenes().size();
        Long[] inserts = new Long[fotos];
        for(int x = 0; x<fotos;x++){
            ContentValues values = new ContentValues();

            values.put("ClaveOperacion",autoconsumoDTO.getClaveOperacion());
            values.put("Url",autoconsumoDTO.getImagenesURI().get(x).toString());
            values.put("Imagen",autoconsumoDTO.getImagenes().get(x));
            inserts[x] = this.getWritableDatabase().insert(
                    TABLE_AUTOCONSUMO_IMAGENES,
                    null,
                    values
            );
        }
        return inserts;
    }

    /**
     * Permite realizar la consulta de todos los registros de autoconsumo,
     * retornara un objeto de tipo {@link Cursor} con el resultado de la consulta
     * @return Retornara todos los registros encontrados
     */
    public Cursor GetAutoconsumos(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_AUTOCONSUMO,
                null);
    }

    public Cursor GetImagenesAutoconsumo(){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_AUTOCONSUMO_IMAGENES,
                null);
    }

    public Cursor GetCalibraciones() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_CALIBRACION,
                null);
    }

    public Cursor GetCalibracionByClaveOperacion(String claveOperacion) {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_CALIBRACION+
        " WHERE ClaveOperacion = '"+claveOperacion+"'",null);
    }

    public Long InsertarCalibracion(CalibracionDTO calibracionDTO, String tipoCalibracion,
                                    boolean esFinal) {
        ContentValues values = new ContentValues();
        values.put("Tipo",tipoCalibracion);
        values.put("ClaveOperacion",calibracionDTO.getClaveOperacion());
        values.put("IdCAlmacenGas",calibracionDTO.getIdCAlmacenGas());
        values.put("IdTipoMedidor",calibracionDTO.getIdTipoMedidor());
        values.put("NombreCAlmacenGas",calibracionDTO.getNombreCAlmacenGas());
        values.put("NombreMedidor",calibracionDTO.getNombreMedidor());
        values.put("PorcentajeCalibracion",calibracionDTO.getPorcentajeCalibracion());
        values.put("IdDestinoCalibracion",calibracionDTO.getIdDestinoCalibracion());
        values.put("IdDestinoCalibracion",calibracionDTO.getIdDestinoCalibracion());
        values.put("P5000",calibracionDTO.getP5000());
        values.put("Porcentaje",calibracionDTO.getPorcentaje());
        values.put("CantidadFotografias",calibracionDTO.getCantidadFotografias());
        values.put("PorcentajeMedidor2",calibracionDTO.getPorcentajeMedidor2());
        values.put("FechaAplicacion",calibracionDTO.getFechaAplicacion().toString());
        values.put("Fecha",calibracionDTO.getFechaRegistro().toString());
        values.put("EsFinal",esFinal);

        return this.getReadableDatabase().insert(TABLE_CALIBRACION,null,values);
    }

    public Cursor GetFotografiasCalibracion(String claveOperacion) {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_CALIBRACION_IMAGENES+
        " WHERE ClaveOperacion = '"+claveOperacion+"'",null);
    }

    public Long[] InsertarImagenesCalibracion(CalibracionDTO calibracionDTO) {
        Long[] inserts = new Long[calibracionDTO.getImagenes().size()];
        for (int x = 0;x<calibracionDTO.getImagenes().size();x++) {
            ContentValues values = new ContentValues();
            values.put("ClaveOperacion",calibracionDTO.getClaveOperacion());
            values.put("Url",calibracionDTO.getImagenesUri().get(x).toString());
            values.put("Imagen",calibracionDTO.getImagenes().get(x));
            inserts[x] = this.getWritableDatabase().insert(TABLE_CALIBRACION_IMAGENES,
                    null,values);
        }
        return inserts;
    }

    public Cursor GetRecargas() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_RECARGAS,
                null);
    }

    public Cursor GetTraspasos() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_TRASPASOS,
                null);
    }

    public  Long InsertTraspaso(TraspasoDTO traspasoDTO,boolean esFinal,String tipo){
        ContentValues values = new ContentValues();
        values.put("ClaveOperacion",traspasoDTO.getClaveOperacion());
        values.put("CantidadFotos",traspasoDTO.getCantidadDeFotos());
        values.put("IdCAlmacenGasEntrada",traspasoDTO.getIdCAlmacenGasEntrada());
        values.put("IdCAlmacenGasSalida",traspasoDTO.getIdCAlmacenGasSalida());
        values.put("IdTipoMedidorSalida",traspasoDTO.getIdTipoMedidorSalida());
        values.put("NombreMedidor",traspasoDTO.getNombreMedidor());
        values.put("P5000Entrada",traspasoDTO.getP5000Entrada());
        values.put("P5000Salida",traspasoDTO.getP5000Salida());
        values.put("PorcentajeSalida",traspasoDTO.getPorcentajeSalida());
        values.put("Fecha",traspasoDTO.getFecha().toString());
        values.put("EsFinal",esFinal);
        values.put("Tipo",tipo);
        values.put("FechaAplicacion",traspasoDTO.getFechaAplicacion());
        return this.getWritableDatabase().insert(
                TABLE_TRASPASOS,
                null,
                values
        );
    }

    public Cursor GetTraspasoByClaveOperacion(String claveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_TRASPASOS+
                " WHERE ClaveOperacion = '"+claveOperacion+"'",null);
    }

    public Integer EliminarTraspasos(String claveOperacion){
        return this.getWritableDatabase().delete(
                TABLE_TRASPASOS,
                "WHERE ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }

    public  Integer EliminarImagenesTraspasos(String claveOperacion){
        return this.getWritableDatabase().delete(
                TABLE_TRASPASOS_IMAGENES,
                " WHERE ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }

    public Long[] InsertImagenesTraspaso(TraspasoDTO traspasoDTO) {
        Long[] registros = new Long[traspasoDTO.getImagenes().size()];
        for(int x = 0; x<traspasoDTO.getImagenes().size();x++){
            ContentValues values = new ContentValues();
            values.put("Imagen",traspasoDTO.getImagenes().get(x));
            values.put("Url",traspasoDTO.getImagenesUri().get(x).toString());
            values.put("ClaveOperacion",traspasoDTO.getClaveOperacion());
            registros[x] = this.getWritableDatabase().insert(
                    TABLE_TRASPASOS_IMAGENES,
                    null,
                    values
            );
        }
        return registros;
    }

    public Cursor GetImagenesTraspaso(String claveOperacion){
        return this.getReadableDatabase().rawQuery(
                "SELECT * FROM "+TABLE_TRASPASOS_IMAGENES+" WHERE ClaveOperacion = '"+
                        claveOperacion+"'",null
        );
    }

    public Cursor GetAnticipos() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_ANTICIPOS,
                null);
    }

    public Cursor GetAnticipoByClaveOperacion(String claveOperacion){
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_ANTICIPOS+
                " WHERE ClaveOperacion = '"+claveOperacion+"'",null);
    }

    public Integer EliminarAnticipo(String claveOperacion){
        return this.getWritableDatabase().delete(
                TABLE_ANTICIPOS,
                " WHERE ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }

    public Cursor GetCortes() {
        return this.getReadableDatabase().rawQuery("SELECT * FROM "+TABLE_CORTES,
                null);
    }

    public Cursor GetCorte(String claveOperacion){
        return  this.getReadableDatabase().rawQuery(
                "SELECT * FROM "+TABLE_CORTES+
                        " WHERE ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }
    public Long InserCorte(CorteDTO corteDTO) {
        ContentValues values = new ContentValues();
        values.put("ClaveOperacion",corteDTO.getClaveOperacion());
        values.put("Fecha",corteDTO.getFecha());
        values.put("IdCAlmacen",corteDTO.getIdEstacion());
        values.put("IdEstacion",corteDTO.getIdEstacion());
        values.put("FechaCorte",corteDTO.getFecha());
        values.put("Hora",corteDTO.getHora());
        return this.getWritableDatabase().insert(
                TABLE_CORTES,
                null,
                values
        );
    }
    public Integer EliminarCorte(String claveOperacion){
        return this.getWritableDatabase().delete(
                TABLE_CORTES,
                " ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }

    public Long InsertAnticipo(AnticiposDTO dto) {
        ContentValues values = new ContentValues();
        values.put("ClaveOperacion",dto.getClaveOperacion());
        values.put("Anticipo",dto.getAnticipar());
        values.put("Hora",dto.getHora());
        values.put("IdCAlmacen",dto.getIdCAlmacen());
        values.put("IdEstacion",dto.getIdEstacion());
        values.put("NombreEstacion",dto.getNombreEstacion());
        values.put("Total",dto.getTotal());
        values.put("Fecha",dto.getFecha());
        values.put("FechaAnticipo",dto.getFechaAnticipo());

        return this.getWritableDatabase().insert(
          TABLE_ANTICIPOS,
          null,
          values
        );
    }

    /**
     * EliminarCalibracion
     * Permite realizar el borrado de una calibración por medio del la clave de operación
     * que se envia como parametro, retornara un valor de tipo {@link Integer} con el numero total
     * de registros eliminados
     * @param claveOperacion {@link String} que reprecenta la clave de operacion de la calibración
     */
    public Integer EliminarCalibracion(String claveOperacion) {
        return this.getWritableDatabase().delete(
                TABLE_CALIBRACION,
                " WHERE ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }

    /**
     * EliminarImagenesCalibracion
     * Permite realizar la eliminación de las imagenes de la calibración, retornara un valor
     * de tipo {@link Integer} que reprecenta el numero de registros eliminados
     * @param claveOperacion {@link String} que reprecenta la clave de operacion de la calibración
     * @return El número de registros eliminados
     */
    public Integer EliminarImagenesCalibracion(String claveOperacion) {
        return this.getWritableDatabase().delete(
                TABLE_CALIBRACION_IMAGENES,
                " WHERE ClaveOperacion = '"+claveOperacion+"'",
                null
        );
    }


    //endregion

    //region Metodos para las ventas del corte

    /**
     * InsertVentasCorte
     * Inserta los registros de las ventas del corte, retornara un array con
     * los id que ha sido registrados en local
     * @param dto Objeto {@link CorteDTO} del que se extrae la lista {@link VentasCorteDTO}
     *            con los datos a registrar
     * @return Array de tipo {@link Long} con los id de los registros
     */
    public Long[] InsertVentasCorte(CorteDTO dto){
        Long[] inserts = new Long[dto.getConceptos().size()];
        for (VentasCorteDTO ventasCorteDTO:
             dto.getConceptos()) {
            ContentValues values = new ContentValues();
            values.put("Corte",ventasCorteDTO.getCorte());
            values.put("TiketVenta",ventasCorteDTO.getTiketVenta());
            values.put("IdVenta",ventasCorteDTO.getIdVenta());
            this.getWritableDatabase().insert(TABLE_CORTES_VENTAS,null,values);
        }

        return inserts;
    }

    /**
     * GetVentasCorte
     * Permite extraer las ventas ligadas al corte
     * @param corte String con la clave del corte
     * @return Objeto tipo {@link Cursor} con los datos de las ventas del corte
     */
    public Cursor GetVentasCorte(String corte){
        return this.getReadableDatabase().rawQuery(
                "SELECT * FROM "+TABLE_CORTES_VENTAS+" WHERE Corte = '"+corte+"'",
                null
        );
    }

    /**
     * EliminarVentasCorte
     * Permite hacer la eliminación de los registros de la base de datos de las ventas
     * del corte , retorna un valor tipo {@link Integer} con el numero de registros eliminados
     * @param corte String con la clave del corte
     * @return Integer que reprecenta el número de registros eliminados
     */
    public Integer EliminarVentasCorte(String corte){
        return this.getWritableDatabase().delete(TABLE_CORTES_VENTAS,
                "Corte = '"+corte+"'",null);
    }
    //endregion
}
