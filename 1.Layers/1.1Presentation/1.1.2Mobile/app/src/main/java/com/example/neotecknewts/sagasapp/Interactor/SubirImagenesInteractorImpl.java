package com.example.neotecknewts.sagasapp.Interactor;

import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Presenter.SubirImagenesPresenter;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class SubirImagenesInteractorImpl implements SubirImagenesInteractor {
    //se declara el tag de la clase y el presenter correspondiente
    public static final String TAG = "SubirImagInteractor";
    SubirImagenesPresenter subirImagenesPresenter;

    //constructor de la clase y se inicializa el presenter
    public SubirImagenesInteractorImpl(SubirImagenesPresenter subirImagenesPresenter){
        this.subirImagenesPresenter = subirImagenesPresenter;
    }

    //metodos para llamada web service
    @Override
    public void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO) {

    }

    @Override
    public void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO) {

    }

    @Override
    public void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO) {

    }
}
