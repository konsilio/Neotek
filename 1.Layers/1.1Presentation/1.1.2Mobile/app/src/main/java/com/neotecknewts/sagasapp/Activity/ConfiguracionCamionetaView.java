package com.neotecknewts.sagasapp.Activity;

import com.neotecknewts.sagasapp.Model.CilindrosDTO;

import java.util.ArrayList;

public interface ConfiguracionCamionetaView {
    void VerificarForm();
    void getCilindros(ArrayList<CilindrosDTO> cilindrosDTOS);
}
