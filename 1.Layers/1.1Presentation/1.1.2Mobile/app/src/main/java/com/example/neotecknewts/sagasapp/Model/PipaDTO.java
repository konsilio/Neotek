package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class PipaDTO implements Serializable{
    @SerializedName("NombrePipa")
    private String NombrePipa;

    @SerializedName("IdPipa")
    private int IdPipa;

    public String getNombrePipa() {
        return NombrePipa;
    }

    public void setNombrePipa(String nombrePipa) {
        NombrePipa = nombrePipa;
    }

    public int getIdPipa() {
        return IdPipa;
    }

    public void setIdPipa(int idPipa) {
        IdPipa = idPipa;
    }
}
