package com.neotecknewts.sagasapp.Model;

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

    @SerializedName("Coordenadas")
    private String Coordenadas;

    @SerializedName("Version")
    private String Version;

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

    public String getCoordenadas() {
        return Coordenadas;
    }

    public void setCoordenadas(String coordenadas) {
        Coordenadas = coordenadas;
    }

    public String getVersion() {
        return Version;
    }

    public void setVersion(String version) {
        Version = version;
    }

    @Override
    public String toString() {
        return "UsuarioLoginDTO{" +
                "IdEmpresa=" + IdEmpresa +
                ", Usuario='" + Usuario + '\'' +
                ", Password='" + Password + '\'' +
                ", FbToken='" + FbToken + '\'' +
                ", Coordenadas='" + Coordenadas + '\'' +
                ", Version='" + Version + '\'' +
                '}';
    }
}