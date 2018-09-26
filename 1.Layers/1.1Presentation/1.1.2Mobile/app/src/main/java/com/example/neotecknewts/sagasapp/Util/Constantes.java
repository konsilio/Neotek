package com.example.neotecknewts.sagasapp.Util;

/**
 * Created by neotecknewts on 08/08/18.
 */

//clase de constantes, se declara la url base a donde se hacen las llamadas a web service y las rutas de cada llamada
public class Constantes {

    public static final String BASE_URL = "http://192.168.0.12:7010/ds/api/";
    //public static final String BASE_URL = "http://pruebaswebneoteck.ddns.net:7010/ds/api/";
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
    public static final String POST_LECTURA_FINAL = "mobile/final/toma-de-lectura";
    public static final String POST_LECTURA_INICIAL_CAMIONETA = "mobile/iniciar/toma-lectura-camioneta";
    public static final String POST_LECTURA_FINAL_CAMIONETA = "mobile/final/toma-lectura-camioneta";
    public static final String LISTA_TIPO_ALMACEN = "mobile/catalogos/almacenes/{esEstacion}/{esPipa}/{esCamioneta}/{esFinalizar}";
    public static final String POST_RECARGA = "mobile/recarga/camioneta";
    public static final String POST_RECARGA_INCIAL = "mobile/recarga/inicial";
    public static final String POST_RECARGA_FINAL = "mobile/recarga/final";
    public static final String GET_UNIDADES = "mobile/catalogo/unidades";
    public static final String GET_CATALOGO_RECARGAS = "mobile/catalogos/recarga/{esEstacion}/{esPipa}/{esCamioneta}/{esFinalizar}";
    public static final String GETCATALOGO_AUTOCONSUMO = "mobile/caatalogos/autoconsumo/{esEstacion}/{esInventario}/{esPipa}/{esFinalizar}";
    public static final String POST_AUTOCONSUMO = "mobile/autoconsumo/{esEstacion}/{esIventario}/{esPipa}/{esFinal}";
    public static final String GET_CATALOGO_TRASPASO = "mobile/catalogos/traspaso/{esTraspaso}/{esPipa}";
    public static final String POST_TRASPASO = "mobile/traspaso/{esEstacion}/{esPipa}/{esFinal}";
    public static final String GET_CATALOGO_CALIBRACION = "mobile/catalogos/calibracion/{esEstacion}/{esPipa}/{esFinal}";
    public static final String POST_CALIBRACION = "mobile/calibracion/{esEstacion}/{esPipa}/{esFinal}";
    public static final String GET_CONFIGURACION_EMPRESA = "mobile/empresas/configuracion";
}
