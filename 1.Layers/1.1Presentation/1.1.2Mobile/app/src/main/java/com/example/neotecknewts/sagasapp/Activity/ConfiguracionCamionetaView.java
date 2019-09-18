package com.example.neotecknewts.sagasapp.Activity;

import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;

import java.util.ArrayList;

public interface ConfiguracionCamionetaView {
    void VerificarForm();
    void getCilindros(ArrayList<CilindrosDTO> cilindrosDTOS);
}
