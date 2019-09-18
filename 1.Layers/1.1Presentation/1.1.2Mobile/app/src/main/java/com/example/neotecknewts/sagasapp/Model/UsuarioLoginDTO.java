package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 09/08/18.
 */

public class UsuarioLoginDTO {
    @SerializedName("IdEmpresa")
    private int IdEmpresa;

    @SerializedName("Usuario")
    private String Usuario;

    @SerializedName("Password")
    private String Password;

    @SerializedName("FbToken")
    private String FbToken;

    public int getIdEmpresa() {
        return IdEmpresa;
    }

    public void setIdEmpresa(int idEmpresa) {
        IdEmpresa = idEmpresa;
    }

    public String getUsuario() {
        return Usuario;
    }

    public void setUsuario(String usuario) {
        Usuario = usuario;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getFbToken() {
        return FbToken;
    }

    public void setFbToken(String fbToken) {
        FbToken = fbToken;
    }
}
