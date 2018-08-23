package com.example.neotecknewts.sagasapp.Presenter;

import android.content.Context;

import com.example.neotecknewts.sagasapp.Activity.RegistrarPapeletaView;
import com.example.neotecknewts.sagasapp.Activity.SubirImagenesView;
import com.example.neotecknewts.sagasapp.Interactor.SubirImagenesInteractor;
import com.example.neotecknewts.sagasapp.Interactor.SubirImagenesInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class SubirImagenesPresenterImpl implements SubirImagenesPresenter {
    //se delcaran la vista y el interactor
    SubirImagenesInteractor interactor;
    SubirImagenesView subirImagenesView;
    RegistrarPapeletaView registrarPapeletaView;


    //se obtiene la vista al ser contruido y se inicializa el interactor
    public SubirImagenesPresenterImpl(SubirImagenesView view){
        this.subirImagenesView = view;
        this.interactor = new SubirImagenesInteractorImpl(this);
    }

    @Override
    public void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL, Context context) {
        //crear show progress en vista igual que en otras vistas
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarPapeleta(precargaPapeletaDTO,token,papeletaSQL,context);

    }

    @Override
    public void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO) {
        //crear show progress en vista igual que en otras vistas
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarIniciarDescarga(iniciarDescargaDTO);
    }

    @Override
    public void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO) {
        //crear show progress en vista igual que en otras vistas
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarFinalizarDescarga(finalizarDescargaDTO);
    }

    @Override
    public void onSuccessRegistrarPapeleta() {
        //crear hide progress en vista igual que en otras vistas
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessRegistrarPapeleta();
    }

    @Override
    public void onSuccessRegistrarIniciarDescarga() {
        //crear hide progress en vista igual que en otras vistas
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessRegistrarIniciarDescarga();
    }

    @Override
    public void onSuccessRegistrarFinalizarDescarga() {
        //crear hide progress en vista igual que en otras vistas
        registrarPapeletaView.hideProgress();
        registrarPapeletaView.onSuccessRegistrarIniciarDescarga();

    }

    @Override
    public void onError() {
        //crear hide progress en vista igual que en otras vistas
        //registrarPapeletaView.hideProgress();
        //registrarPapeletaView.showMessageError();

    }

    /**
     * Metodo sobreescrito para llamar a la interfaz implementer
     * que muestre en la activity el dialogo de sucess en caso de
     * subir la papeleta.
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onSuccessRegistroPapeleta() {
        subirImagenesView.onSuccessRegistroPapeleta();
    }

    /**
     * Metodo sobreescrito para llamar a la interfaz inplementer
     * que muestre en la activity el dialogo de que los datos
     * fueron guardados en el dispositivo
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onSuccessRegistroAndroid(){
        subirImagenesView.onSuccessRegistroAndroid();
    }
}
