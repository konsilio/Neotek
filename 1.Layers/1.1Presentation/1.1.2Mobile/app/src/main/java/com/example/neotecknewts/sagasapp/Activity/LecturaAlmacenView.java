package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.EstacionCarburacionDTO;
import com.example.neotecknewts.sagasapp.Model.MedidorDTO;

import java.util.ArrayList;
import java.util.List;

public interface LecturaAlmacenView {
    public void VerificarErrores();
    public void onSuccessMedidores(List<MedidorDTO> data);
    public void onSuccessAlmacenes(List<EstacionCarburacionDTO> data);
    public void onError();
    public void DialogoRetroceder();
    public void DialogoError(ArrayList<String> mensajes_error);

    void showProgress(int message_cargando);

    void hiddeProgress();
}
