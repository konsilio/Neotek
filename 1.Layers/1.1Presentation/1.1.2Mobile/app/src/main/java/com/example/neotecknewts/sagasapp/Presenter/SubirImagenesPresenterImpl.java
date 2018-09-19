package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.RegistrarPapeletaView;
import com.example.neotecknewts.sagasapp.Activity.SubirImagenesView;
import com.example.neotecknewts.sagasapp.Interactor.SubirImagenesInteractor;
import com.example.neotecknewts.sagasapp.Interactor.SubirImagenesInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.FinalizarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.IniciarDescargaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaAlmacenDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaPipaDTO;
import com.example.neotecknewts.sagasapp.Model.PrecargaPapeletaDTO;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.FinalizarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.IniciarDescargaSQL;
import com.example.neotecknewts.sagasapp.SQLite.PapeletaSQL;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;

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
    public void registrarPapeleta(PrecargaPapeletaDTO precargaPapeletaDTO, String token, PapeletaSQL papeletaSQL) {
        //crear show progress en vista igual que en otras vistas
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarPapeleta(precargaPapeletaDTO,token,papeletaSQL);

    }

    @Override
    public void registrarIniciarDescarga(IniciarDescargaDTO iniciarDescargaDTO, String token, IniciarDescargaSQL iniciarDescargaSQL) {
        //crear show progress en vista igual que en otras vistas
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarIniciarDescarga(iniciarDescargaDTO,token,iniciarDescargaSQL);
    }

    @Override
    public void registrarFinalizarDescarga(FinalizarDescargaDTO finalizarDescargaDTO, String token, FinalizarDescargaSQL finalizarDescargaSQL) {
        //crear show progress en vista igual que en otras vistas
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarFinalizarDescarga(finalizarDescargaDTO,token,finalizarDescargaSQL);
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
    //region Metodos de respuesta Registro papeleta
    /**
     * onSuccessRegistroPapeleta
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
     * onSuccessRegistroAndroid
     * Metodo sobreescrito para llamar a la interfaz inplementer
     * que muestre en la activity el dialogo de que los datos
     * fueron guardados en el dispositivo
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void onSuccessRegistroAndroid(){
        subirImagenesView.onSuccessRegistroAndroid();
    }

    /**
     * errorSolicitud
     * En caso de surgir un error interno del servicio se mostrara el error
     * generado en la app como dialog y se reiniciara en una anterior
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     * @param mensaje Mensaje de error generado
     */
    @Override
    public void errorSolicitud(String mensaje) {
        subirImagenesView.showError(mensaje);
    }

    @Override
    public void onRegistrarIniciarDescarga() {
        subirImagenesView.onRegistrarIniciarDescarga();
    }

    @Override
    public void registrarLecturaInicial(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarLecturaInicial(sagasSql,token,lecturaDTO);
    }

    @Override
    public void registrarLecturaFinal(SAGASSql sagasSql, String token, LecturaDTO lecturaDTO) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarLecturaFinal(sagasSql,token,lecturaDTO);
    }

    /**
     * <h3>registrarLecturaInicialPipa</h3>
     * Permite realizar el registro de la lectura inicial de la pipa, temoara como parametros
     * un objeto de tipo {@link SAGASSql} que tiene el acceso a base de datos en local,
     * una cadena de tipo {@link String} con el token del usuario y un objeto {@link LecturaPipaDTO}
     * con los valores a registrar.
     * @param sagasSql Objeto de tipo {@link SAGASSql} que permite el acceso a base de datos local
     * @param token Cadena de tipo {@link String} que reprecenta el token de usuario
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} que tien los valores de la
     *                       lectura de la pipa
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void registrarLecturaInicialPipa(SAGASSql sagasSql, String token,
                                            LecturaPipaDTO lecturaPipaDTO) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarLecturaInicialPipa(sagasSql,token,lecturaPipaDTO);
    }

    /**
     * <h3>registrarLecturaFinalalPipa</h3>
     * Permite realizar la lectura final de la pipa, se enviaran como parametros
     * @param sagasSql Objeto de tipo {@link SAGASSql} que permite el acceso a base de datos local
     * @param token Cadena de tipo {@link String} que reprecenta el token de usuario
     * @param lecturaPipaDTO Objeto de tipo {@link LecturaPipaDTO} que tien los valores de la
     *                       lectura de la pipa
     * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
     */
    @Override
    public void registrarLecturaFinalalPipa(SAGASSql sagasSql, String token,
                                            LecturaPipaDTO lecturaPipaDTO) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarLecturaFinalizalPipa(sagasSql,token,lecturaPipaDTO);
    }

    @Override
    public void registrarLecturaInicialAlmacen(SAGASSql sagasSql, String token,
                                               LecturaAlmacenDTO lecturaAlmacenDTO) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarLecturaInicialAlmacen(sagasSql,token,lecturaAlmacenDTO);
    }

    @Override
    public void registrarLecturaFinalAlmacen(SAGASSql sagasSql, String token,
                                             LecturaAlmacenDTO lecturaAlmacenDTO) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarLecturaFinalAlmacen(sagasSql,token,lecturaAlmacenDTO);
    }

    @Override
    public void registrarRecargaEstacion(SAGASSql sagasSql, String token, RecargaDTO recargaDTO,
                                         boolean EsRecargaEstacionInicial) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarRecargaEstacion(sagasSql,token,recargaDTO,
                EsRecargaEstacionInicial
                );
    }

    @Override
    public void onSuccessRegistroRecarga() {
        subirImagenesView.hideProgress();
        subirImagenesView.onSuccessRegistroRecarga();
    }

    @Override
    public void registrarRecargaPipa(SAGASSql sagasSql, String token, RecargaDTO recargaDTO,
                                     boolean esRecargaPipaFinal) {
        subirImagenesView.showProgress(R.string.message_cargando);
        interactor.registrarRecargaPipa(sagasSql,token,recargaDTO,esRecargaPipaFinal);
    }
    //endregion
}
