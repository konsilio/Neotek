/*
 * RespuestaOrdenReferenciaDTO
 * Permite obtener la orden de referencia del la orden
 * que tiene de refencia la orden de expedido con el
 * porteador y viceversa
 * @author Jorge Omar Tovar Martínez
 * @company Neoteck
 * @date 09/11/2018
 * @update 09/11/2018
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

public class RespuestaOrdenReferenciaDTO extends RespuestaDTO {

    @SerializedName("Id")
    private int Id;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }
}
