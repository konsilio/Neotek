package com.example.neotecknewts.sagasapp.Util;

import android.content.Context;

import com.example.neotecknewts.sagasapp.SQLite.FinalizarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public class Semaforo {
    SAGASSql sagasSql;
    PapeletaSQL papeletaSQL;
    FinalizarDescargaSQL finalizarDescargaSQL;
    IniciarDescargaSQL iniciarDescargaSQL;
    public Semaforo(Context context){
        sagasSql = new SAGASSql(context);
        papeletaSQL = new PapeletaSQL(context);
        finalizarDescargaSQL = new FinalizarDescargaSQL(context);
        iniciarDescargaSQL = new IniciarDescargaSQL(context);
    }
    public boolean VerificarEstatus(){
        boolean ban = false;
        if(papeletaSQL.GetPapeletas().getCount()>0){
            ban = true;
        }
        if(sagasSql.GetAutoconsumos().getCount()>0)
            ban = true;
        if(sagasSql.GetCalibraciones().getCount()>0)
            ban = true;
        if(sagasSql.GetLecturasIniciales().getCount()>0 )
            ban = true;

        return ban;
    }
}
