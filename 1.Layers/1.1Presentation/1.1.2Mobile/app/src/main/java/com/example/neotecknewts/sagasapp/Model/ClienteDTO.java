/**
 * ClienteDTO
 * Modelo dto de los datos del cliebre para el registro y listado de clientes del buscador
 * @developer Jorge Omar Tovar Martínez jorge.tovar@neoteck.com.mx
 * @company Neoteck
 * @date 28/11/2018
 * @update 28/11/2018
 *
 */
package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ClienteDTO extends RespuestaDTO implements Serializable {
    @SerializedName("IdCliente")
    private int IdCliente;

    @SerializedName("IdTipoPersona")
    private int IdTipoPersona;

    @SerializedName("IdTipoRegimen")
    private int IdTipoRegimen;

    @SerializedName("Nombre")
    private String Nombre;

    @SerializedName("Apellido1")
    private String Apellido_uno;

    @SerializedName("Apellido2")
    private String Apellido_dos;

    @SerializedName("Celular")
    private String Celular;

    @SerializedName("TelefonoFijo")
    private String Telefono_fijo;

    @SerializedName("RFC")
    private String RFC;

    @SerializedName("RazonSocial")
    private String RazonSocial;

    @SerializedName("Credito")
    private boolean Credito;

    @SerializedName("Factura")
    private boolean Factura;

    @SerializedName("LimiteCredito")
    private double LimiteCredito;

    public boolean isFactura() {
        return Factura;
    }

    public void setFactura(boolean factura) {
        Factura = factura;
    }

    public int getIdCliente() {
        return IdCliente;
    }

    public void setIdCliente(int idCliente) {
        IdCliente = idCliente;
    }

    public int getIdTipoPersona() {
        return IdTipoPersona;
    }

    public void setIdTipoPersona(int idTipoPersona) {
        IdTipoPersona = idTipoPersona;
    }

    public int getIdTipoRegimen() {
        return IdTipoRegimen;
    }

    public void setIdTipoRegimen(int idTipoRegimen) {
        IdTipoRegimen = idTipoRegimen;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public String getApellido_uno() {
        return Apellido_uno;
    }

    public void setApellido_uno(String apellido_uno) {
        Apellido_uno = apellido_uno;
    }

    public String getApellido_dos() {
        return Apellido_dos;
    }

    public void setApellido_dos(String apellido_dos) {
        Apellido_dos = apellido_dos;
    }

    public String getCelular() {
        return Celular;
    }

    public void setCelular(String celular) {
        Celular = celular;
    }

    public String getTelefono_fijo() {
        return Telefono_fijo;
    }

    public void setTelefono_fijo(String telefono_fijo) {
        Telefono_fijo = telefono_fijo;
    }

    public String getRFC() {
        return RFC;
    }

    public void setRFC(String RFC) {
        this.RFC = RFC;
    }

    public String getRazonSocial() {
        return RazonSocial;
    }

    public void setRazonSocial(String razonSocial) {
        RazonSocial = razonSocial;
    }

    public boolean isCredito() {
        return Credito;
    }

    public void setCredito(boolean credito) {
        Credito = credito;
    }

    public double getLimiteCredito() {
        return LimiteCredito;
    }

    public void setLimiteCredito(double limiteCredito) {
        LimiteCredito = limiteCredito;
    }

    @Override
    public String toString() {
        return "ClienteDTO{" +
                "IdCliente=" + IdCliente +
                ", IdTipoPersona=" + IdTipoPersona +
                ", IdTipoRegimen=" + IdTipoRegimen +
                ", Nombre='" + Nombre + '\'' +
                ", Apellido_uno='" + Apellido_uno + '\'' +
                ", Apellido_dos='" + Apellido_dos + '\'' +
                ", Celular='" + Celular + '\'' +
                ", Telefono_fijo='" + Telefono_fijo + '\'' +
                ", RFC='" + RFC + '\'' +
                ", RazonSocial='" + RazonSocial + '\'' +
                ", Credito=" + Credito +
                ", Factura=" + Factura +
                ", LimiteCredito=" + LimiteCredito +
                '}';
    }
}
