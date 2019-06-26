package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

import java.util.ArrayList;

public interface AutoconsumoPipaView {
    void VerificarCampos();
    void onShowprogress(int mensaje);
    void onHiddeprogress();
    void mostrarErrores(ArrayList<String> mensajes);
    void onError(String mensaje);
    void onSuccessList(DatosAutoconsumoDTO dto);
}
