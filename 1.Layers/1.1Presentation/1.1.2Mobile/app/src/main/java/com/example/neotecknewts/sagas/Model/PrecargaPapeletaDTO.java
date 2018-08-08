package com.example.neotecknewts.sagas.Model;

/**
 * Created by neotecknewts on 03/08/18.
 */

public class PrecargaPapeletaDTO {
    private OrdenCompraDTO ordenCompraDTO;
    private ProvedorDTO provedorDTO;
    private ProvedorDTO porteadorDTO;

    public OrdenCompraDTO getOrdenCompraDTO() {
        return ordenCompraDTO;
    }

    public void setOrdenCompraDTO(OrdenCompraDTO ordenCompraDTO) {
        this.ordenCompraDTO = ordenCompraDTO;
    }

    public ProvedorDTO getProvedorDTO() {
        return provedorDTO;
    }

    public void setProvedorDTO(ProvedorDTO provedorDTO) {
        this.provedorDTO = provedorDTO;
    }

    public ProvedorDTO getPorteadorDTO() {
        return porteadorDTO;
    }

    public void setPorteadorDTO(ProvedorDTO porteadorDTO) {
        this.porteadorDTO = porteadorDTO;
    }
}
