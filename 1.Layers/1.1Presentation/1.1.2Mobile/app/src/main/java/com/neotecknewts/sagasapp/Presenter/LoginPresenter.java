package com.neotecknewts.sagasapp.Presenter;

import android.support.v7.view.menu.ListMenuPresenter;

import com.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.neotecknewts.sagasapp.Model.UsuarioLoginDTO;

import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

//interfaz del presentador, este comunica la vista con el interactor que es le que se encarga de hacer la llamada a web service
//los metodos de get o do o post que son los que llaman a al interactor para llamar a su vez al web service y en estos mismos se inicia en la vista
// los metodos de mostrar el progreso.
//los metodo de success son los que se llaman desde el interacor al terminar y devolver un resultado, asi se le envia este reultado a la vista por medio
// del metodo success de la vista y se ocultan los progress,
//el metodo onError se llama desde el interactor al ocurrir un error, en la vista oculta el progress dialog y manda el mensaje de error
public interface LoginPresenter {
    void getEmpresas();
    void doLogin(UsuarioLoginDTO usuarioLoginDTO);
    void doRegistrar(UsuarioLoginDTO usuarioLoginDTO, String token);
    void onSuccessGetEmpresas(List<EmpresaDTO> empresaDTOs);
    void onSuccessLogin(UsuarioDTO usuarioDTO);
    void onError(String mensaje);

}