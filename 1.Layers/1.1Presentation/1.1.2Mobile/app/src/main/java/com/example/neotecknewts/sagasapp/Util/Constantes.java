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
}
