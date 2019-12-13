/*
 *  UsuariosDTO
 *  Modelo para el listado de usuario para el apartado de anticipos para
 *  especificar quien entrega
 *  @developer Jorge Omar Tovar martínez jorge.tovar@neoteck.com.mx
 *  @company Neoteck
 *  @date 12/11/2018
 *  @update 12/11/2018
 */
package com.neotecknewts.sagasapp.Model.Cortes;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class UsuariosDTO implements Serializable {
    /**
     * Id del usuario
     */
    @SerializedName("IdUsuario")
    private int IdUsuario;

    /**
     * Nombre del usuario que entrega
     */
    @SerializedName("Nombre")
    private String Nombre;

    /**
     * Id de la empresa
     */
    @SerializedName("IdEmpresa")
    private int IdEmpresa;

    public int getIdUsuario() {
        return IdUsuario;
    }

    public void setIdUsuario(int idUsuario) {
        IdUsuario = idUsuario;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public int getIdEmpresa() {
        return IdEmpresa;
    }

    public void setIdEmpresa(int idEmpresa) {
        IdEmpresa = idEmpresa;
    }
}
