package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

public interface AnticipoTablaPresenter {
    void Anticipo(AnticiposDTO anticiposDTO, SAGASSql sagasSql, String token);

    void onSuccess();

    void onError(String s);

    void onSuccessAndroid();

    void onError(Object ob);
}
