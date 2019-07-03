package com.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 09/08/18.
 */

public class UsuarioDTO {

    @SerializedName("listMenu")
    private MenuDTO[] listMenu;

    @SerializedName("IdUsuario")
    private int IdUsuario;

    @SerializedName("Exito")
    private boolean Exito;

    @SerializedName("token")
    private String token;

    @SerializedName("Mensaje")
    private String Mensaje;

    @SerializedName("IdCAlmacenGas")
    private int IdAlmacen;

    public MenuDTO[] getListMenu() {
        return listMenu;
    }

    public int getLengthListMenu() {
        return this.listMenu.length;
    }

    public void setListMenu(MenuDTO[] listMenu) {
        this.listMenu = listMenu;
    }

    public int getIdUsuario() {
        return IdUsuario;
    }

    public void setIdUsuario(int idUsuario) {
        IdUsuario = idUsuario;
    }

    public boolean isExito() {
        return Exito;
    }

    public void setExito(boolean exito) {
        Exito = exito;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public String getMensaje() {
        return Mensaje;
    }

    public void setMensaje(String mensaje) {
        Mensaje = mensaje;
    }

    @Override
    public String toString() {
        String menu="";
        for (int i= 0; i < listMenu.length; i++) {
            menu = menu + listMenu[i].toString();
        }
        return "UsuarioDTO{" +
                "IdUsuario=" + IdUsuario +
                ", Exito=" + Exito +
                ", token='" + token + '\'' +
                ", Mensaje='" + Mensaje + '\'' +
                ", IdAlmacen=" + IdAlmacen + '\'' +
                ", Lista Menu=" + menu +

                '}';
    }

    @SerializedName("IdCAlmacenGas")
    public int getIdAlmacen() {
        return IdAlmacen;
    }

    @SerializedName("IdCAlmacenGas")
    public void setIdAlmacen(int idAlmacen) {
        IdAlmacen = idAlmacen;
    }
}
