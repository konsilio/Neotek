/*
 *  UsuariosCorteDTO
 *  Modelo para el listado de usuario para el apartado de anticipos para
 *  especificar quien entrega, este permitira verificar si hay datos y controlar los errores
 *  @developer Jorge Omar Tovar martínez jorge.tovar@neoteck.com.mx
 *  @company Neoteck
 *  @date 12/11/2018
 *  @update 12/11/2018
 */
package com.example.neotecknewts.sagasapp.Model;

import com.example.neotecknewts.sagasapp.Model.Cortes.UsuariosDTO;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class UsuariosCorteDTO extends RespuestaDTO implements Serializable {
    /**
     * Id de la empresa
     */
    @SerializedName("IdEmpresa")
    private int IdEmpresa;
    /**
     * Lista de usuarios
     */
    @SerializedName("Usuarios")
    private List<UsuariosDTO> usuarios;

    /**
     * Constructor de clase
     */
    public UsuariosCorteDTO(){
        this.usuarios = new ArrayList<>();
    }

    public int getIdEmpresa() {
        return IdEmpresa;
    }

    public void setIdEmpresa(int idEmpresa) {
        IdEmpresa = idEmpresa;
    }

    public List<UsuariosDTO> getUsuarios() {
        return usuarios;
    }

    public void setUsuarios(List<UsuariosDTO> usuarios) {
        this.usuarios = usuarios;
    }
}
