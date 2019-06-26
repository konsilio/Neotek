package com.neotecknewts.sagasapp.Util;

/**
 * Created by neotecknewts on 08/08/18.
 */

//clase de constantes, se declara la url base a donde se hacen las llamadas a web service y las rutas de cada llamada
public class Constantes {

    //public static final String BASE_URL = "http://192.168.0.12:7010/ds/api/";
    //public static final String BASE_URL = "http://pruebaswebneoteck.ddns.net:7010/ds/api/";
//    public static final String BASE_URL = "http://sagasapi.ddns.net:7012/api/"; //QA
    //public static final String BASE_URL = "http://sagasapi.ddns.net:7011/api/";//DEV
    public static final int IdClienteGeneral = 0;
    public static final String LOGIN_URL = "mobile/login";
    public static final String LISTA_EMPRESAS   = "catalogos/empresas/listaempresaslogin";
    public static final String LISTA_ORDENESCOMPRA_GAS = "mobile/lista/ordenes/compra";
    public static final String LISTA_MENU = "mobile/obtener/menu";
    public static final String LISTA_ALMACEN = "mobile/obtener/almacenes";
    public static final String LISTA_MEDIDORES = "mobile/obtener/medidores";
    public static final String VERIFICA_SERVICIO = "seguridad/servicio/disponible";
    public static final String POST_PAPELETA = "mobile/registrar/papeleta";
    public static final String POST_INICIAR_DESCARGA = "mobile/iniciar/descarga";
    public static final String POST_FINALIZAR_DESCARGA = "mobile/finalizar/descarga";
    public static final String POST_LECTURA_INICIAL = "mobile/iniciar/toma-de-lectura";
    public static final String POST_LECTURA_FINAL = "mobile/finalizar/toma-de-lectura";
    public static final String POST_LECTURA_INICIAL_CAMIONETA = "mobile/iniciar/toma-lectura-camioneta";
    public static final String POST_LECTURA_FINAL_CAMIONETA = "mobile/finalizar/toma-lectura-camioneta";
    public static final String LISTA_TIPO_ALMACEN = "mobile/catalogos/almacenes/{esEstacion}/{esPipa}/{esCamioneta}/{esFinalizar}";
    public static final String POST_RECARGA = "mobile/recarga/camioneta";
    public static final String POST_RECARGA_INCIAL = "mobile/recarga/inicial";
    public static final String POST_RECARGA_FINAL = "mobile/recarga/final";
    public static final String GET_UNIDADES = "mobile/catalogos/unidades";
    public static final String GET_CATALOGO_RECARGAS = "mobile/catalogos/recarga/{esEstacion}/{esPipa}/{esCamioneta}";
    public static final String GETCATALOGO_AUTOCONSUMO = "mobile/catalogos/autoconsumo/{esEstacion}/{esInventario}/{esPipas}/{esFinal}";
    public static final String POST_AUTOCONSUMO = "mobile/autoconsumo/{esFinal}";
    public static final String GET_CATALOGO_TRASPASO = "mobile/catalogos/traspaso/{esPipa}";
    public static final String POST_TRASPASO = "mobile/traspaso/{esFinal}";
    public static final String GET_CATALOGO_CALIBRACION = "mobile/catalogos/calibracion/{esEstacion}/{esPipa}";
    public static final String POST_CALIBRACION = "mobile/calibracion/{esFinal}";
    public static final String GET_CONFIGURACION_EMPRESA = "mobile/empresas/configuracion";
    public static final String GET_CATALOGO_RAZON = "mobile/catalogos/tipo-persona";
    public static final String POST_CLIENTE = "mobile/cliente/registrar";
    public static final String GET_LIST_CLIENTES = "mobile/cliente/lista-clientes/{criterio}";
    public static final String GET_LIST_EXISTENCIAS = "mobile/catalogos/venta-gas/{esGasLP}/{esCilindroConGas}/{esCilindro}";
    public static final String GET_CATALOGO_PRODUCTO = "mobile/catalogos/venta-gas/otros";
    public static final String POST_VENTA = "mobile/venta";
    public static final String POST_ANTICIPO = "mobile/anticipos";
    public static final String GET_CATALOGO_VENTAS_ESTACIONES = "mobile/catalogos/anticipo-y-corte/estaciones";
    public static final String GET_REPORTE = "mobile/reportes/reporte-dia/{fecha}/{idCAlmacenGas}";
    public static final String POST_CORTE = "mobile/corte-de-caja";
    public static final String GET_CATALOGOS_VENTA_GAS = "mobile/catalogos/venta-gas/{esLP}/{esCilindroConGas}/{esCilindro}";
    public static final String GET_CATALOGOS_VENTA_OTROS = "mobile/catalogos/otros";
    public static final String GET_ORDEN_REFERENCIA = "mobile/lista/ordenes/compra/{IdOrdenCompra}";
    public static final String GET_CILINDROS_VENTA = "mobile/catalogos/venta-gas/catalgocilindros";
    public static final String GET_CATALOGOS_ANTICIPOS_CORTE_LISTA = "mobile/catalogos/anticipo-y-corte/ventas/{estacion}/{esAnticipos}/{fecha}";
    public static final String GET_PRECIO_VENTA = "mobile/consulta/precioventa/vigente";
    public static final String GET_PUNTO_VENTA_ASIGNADO = "mobile/estacion/punto-venta";
    public static final String GET_USUARIOS_ANTICIPOS = "mobile/catalogos/anticipos-y-cortes/usuarios";
    public static final String GETUSUARIOS_CORTES = "mobile/catalogos/anticipos-y-cortes/usuarios-liquitar";
    public static final String GET_VENTA_EXTRAFORANEA = "mobile/venta-extraforanea/{idCliente}" ;
    public static final String GET_HAY_VENTA = "mobile/hay-corte-estacion/{fecha}";
    public static final String GET_HAY_LECTURAS = "mobile/cortes/verificar-lecturas";

    public static final String FORMATO_FECHA = "yyyy-MM-dd HH:mm:ss";
    public static final String FORMATO_FECHA_SESSION = "yyyy-MM-dd";
    public static final String FORMATO_FECHA_API = "yyyy-MM-dd'T'HH:mm:ss.SSSZ";

}
