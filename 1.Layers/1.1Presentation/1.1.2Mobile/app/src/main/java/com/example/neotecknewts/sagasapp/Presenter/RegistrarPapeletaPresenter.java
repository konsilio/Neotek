package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.MedidorDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenReferenciaDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;

import java.util.List;

/**
 * Created by neotecknewts on 13/08/18.
 */

//interfaz del presentador, este comunica la vista con el interactor que es le que se encarga de hacer la llamada a web service
//los metodos de get o do o post que son los que llaman a al interactor para llamar a su vez al web service y en estos mismos se inicia en la vista
// los metodos de mostrar el progreso.
//los metodo de success son los que se llaman desde el interacor al terminar y devolver un resultado, asi se le envia este reultado a la vista por medio
// del metodo success de la vista y se ocultan los progress,
//el metodo onError se llama desde el interactor al ocurrir un error, en la vista oculta el progress dialog y manda el mensaje de error
public interface RegistrarPapeletaPresenter {
    void getOrdenesCompraExpedidor(int IdEmpresa, String token);
    void getOrdenesCompraPorteador(int IdEmpresa, String token);
    void onSuccessGetOrdenesCompraExpedidor(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void onSuccessGetOrdenesCompraPorteador(RespuestaOrdenesCompraDTO respuestaOrdenesCompraDTO);
    void getMedidores(String token);
    void onSuccessGetMedidores(List<MedidorDTO> medidorDTOList);
    void onError();

    void onError(String mensaje);

    void getOrderReferencia(String token, int idOrdenCompra,boolean esExpedidor);

    void onSuccessReferencia(RespuestaOrdenReferenciaDTO data,boolean esExpedidor);
}
