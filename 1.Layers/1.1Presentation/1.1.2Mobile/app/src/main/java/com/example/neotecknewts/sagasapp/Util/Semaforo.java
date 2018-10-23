/*
 * Clase semaforo
 * Con esta permitira realizar la consulta general a las base de datos para
 * verificar si no existen registros pendientes en la base de datos local
 * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx/>
 * @company Neoteck
 * @date 22/10/2018
 * @update 22/10/2018
 */
package com.example.neotecknewts.sagasapp.Util;

import android.content.Context;

import com.example.neotecknewts.sagasapp.SQLite.FinalizarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

import java.util.ArrayList;
import java.util.List;

public class Semaforo {
    //region Variables privadas
    SAGASSql sagasSql;
    PapeletaSQL papeletaSQL;
    FinalizarDescargaSQL finalizarDescargaSQL;
    IniciarDescargaSQL iniciarDescargaSQL;
    //endregion

    //region Constructor de clase

    /**
     * Constructor de clas, tomara de parametro
     * el contexto de la actividad que se consulta
     * @param context {@link Context} Contecto de la aplicación actual
     */
    public Semaforo(Context context){
        sagasSql = new SAGASSql(context);
        papeletaSQL = new PapeletaSQL(context);
        finalizarDescargaSQL = new FinalizarDescargaSQL(context);
        iniciarDescargaSQL = new IniciarDescargaSQL(context);
    }
    //endregion

    //region Metodos publicos
    /**
     * <h3>VerificarEstatus</h3>
     * Permite realizar la verificación en la base de datos local de todas
     * las tablas para confirmar si hay registros pendientes que deben de ser
     * registrados en el servicio, retornara un valor de true/false en caso de existir
     * @return boolean Regresa **true** en caso de existir , en caso contrario retorna false
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx/>
     * @date 22/10/2018
     * @update 22/10/2018
     */
    public boolean VerificarEstatus(){
        boolean ban = false;
        if(papeletaSQL.GetPapeletas().getCount()>0)
            ban = true;
        if(iniciarDescargaSQL.GetIniciarDescargas().getCount()>0)
            ban = true;
        if(finalizarDescargaSQL.GetFinalizarDescargas().getCount()>0)
            ban = true;
        if(sagasSql.GetAutoconsumos().getCount()>0)
            ban = true;
        if(sagasSql.GetCalibraciones().getCount()>0)
            ban = true;
        if(sagasSql.GetLecturasIniciales().getCount()>0 )
            ban = true;
        if(sagasSql.GetLecturasFinales().getCount()>0)
            ban = true;
        if(sagasSql.GetLecturasIncialesPipas().getCount()>0)
            ban = true;
        if(sagasSql.GetLecturasFinaesPipas().getCount()>0)
            ban = true;
        if(sagasSql.GetLecturasFinalesAlmacen().getCount()>0)
            ban = true;
        if(sagasSql.GetLecturasIncialesCamioneta().getCount()>0)
            ban = true;
        if (sagasSql.GetRecargas().getCount()>0)
            ban = true;
        if(sagasSql.GetTraspasos().getCount()>0)
            ban = true;
        if(sagasSql.GetAnticipos().getCount()>0)
            ban = true;
        if(sagasSql.GetCortes().getCount()>0){
            ban = true;
        }
        if(sagasSql.GetVentas().getCount()>0){
            ban = true;
        }

        return ban;
    }

    /**
     * <h3>obtenerCantidadesRestantes</h3>
     * Permite retornar una lista con los totaldes de datos
     * pendientes en la base de datos local.
     * @return List de tipo string con los mensajes a mostrar
     * @author Jorge Omar Tovar Martínez
     * @date 22/10/2018
     * @update 22/10/2018
     */
    public List<String> obtenerCantidadesRestantes(){
        List<String> mensajes = new ArrayList<>();
        int lecturas_iniciales = 0;
        int lecturas_finales = 0;
        if(papeletaSQL.GetPapeletas().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(papeletaSQL.GetPapeletas().getCount())
            +" papeletas pendientes");
        if(iniciarDescargaSQL.GetIniciarDescargas().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(iniciarDescargaSQL.GetIniciarDescargas().getCount())
                    +" inicios de descarga pendientes");
        if(finalizarDescargaSQL.GetFinalizarDescargas().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(finalizarDescargaSQL.GetFinalizarDescargas().getCount())
                    +" finalización de  pendientes");
        if(sagasSql.GetAutoconsumos().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetAutoconsumos().getCount())
                    +" autoconsumos de  pendientes");
        if(sagasSql.GetCalibraciones().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetCalibraciones().getCount())
                    +" calibraciones de  pendientes");
        if(sagasSql.GetLecturasIniciales().getCount()>0 )
           lecturas_iniciales = sagasSql.GetLecturasIniciales().getCount();
        if(sagasSql.GetLecturasIncialesPipas().getCount()>0)
            lecturas_iniciales = lecturas_iniciales +sagasSql.GetLecturasIniciales().getCount();
        if(sagasSql.GetLecturasIncialesCamioneta().getCount()>0)
            lecturas_iniciales = lecturas_iniciales +sagasSql.GetLecturasIncialesCamioneta().getCount();
        if(sagasSql.GetLecturasFinales().getCount()>0)
            lecturas_finales = sagasSql.GetLecturasFinales().getCount();
        if(sagasSql.GetLecturasFinaesPipas().getCount()>0)
            lecturas_finales = sagasSql.GetLecturasFinaesPipas().getCount();
        if(sagasSql.GetLecturasFinalesAlmacen().getCount()>0)
            lecturas_finales = sagasSql.GetLecturasFinalesAlmacen().getCount();
        if(lecturas_iniciales>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(lecturas_iniciales)
                    +" lecturas iniciales pendientes");
        if(lecturas_finales>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(lecturas_iniciales)
                    +" lecturas finales pendientes");
        if (sagasSql.GetRecargas().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetRecargas().getCount())
                    +" recargas pendientes");
        if(sagasSql.GetTraspasos().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetTraspasos().getCount())
                    +" traspasos pendientes");
        if(sagasSql.GetAnticipos().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetAnticipos().getCount())
                    +" anticipos pendientes");
        if(sagasSql.GetCortes().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetCortes().getCount())
                    +" cortes pendientes");
        if(sagasSql.GetVentas().getCount()>0)
            mensajes.add("Existen un total de "+
                    String.valueOf(sagasSql.GetVentas().getCount())
                    +" ventas pendientes");
        return mensajes;
    }
    //endregion
}
