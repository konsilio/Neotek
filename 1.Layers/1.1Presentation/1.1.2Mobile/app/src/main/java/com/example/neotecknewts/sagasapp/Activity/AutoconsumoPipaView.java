package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;

import java.util.ArrayList;

public interface AutoconsumoPipaView {
    void VerificarCampos();
    void onShowprogress(int mensaje);
    void onHiddeprogress();
    void mostrarErrores(ArrayList<String> mensajes);
    void onError(String mensaje);
    void onSuccessList(DatosAutoconsumoDTO dto);
}
