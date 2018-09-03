package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.net.URI;
import java.util.ArrayList;
import java.util.List;

/**
 * <h3>LecturaPipaDTO</h3>
 * Clase modelo DTO para el apartado de lectura de pipa
 * tendra todos los valores para el apartado de pipas
 * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx>
 * @company Neoteck
 * @date 03/09/2018
 * @updated 03/09/2018
 */
public class LecturaPipaDTO implements Serializable {

    @SerializedName("IdPipa")
    private int IdPipa;

    @SerializedName("ClaveProceso")
    private String ClaveProceso;

    @SerializedName("NombrePipa")
    private String NombrePipa;

    @SerializedName("IdTipoMedidor")
    private int IdTipoMedidor;

    @SerializedName("TipoMedidor")
    private String TipoMedidor;

    @SerializedName("CantidadFotografias")
    private int CantidadFotografias;

    @SerializedName("CantidadP5000")
    private int CantidadP5000;

    @SerializedName("ImagenP5000")
    private String ImagenP5000;

    @SerializedName("ImagenP5000URI")
    private URI ImagenP5000URI;

    @SerializedName("PorcentajeMedidor")
    private Double PorcentajeMedidor;

    @SerializedName("Imagenes")
    private List<String> Imagenes;

    @SerializedName("ImagenesURL")
    private List<URI> ImagenesURI;

    //region Constructores
    public LecturaPipaDTO() {
        Imagenes = new ArrayList<>();
        ImagenesURI = new ArrayList<>();
    }
    //endregion

    public int getIdPipa() {
        return IdPipa;
    }

    public void setIdPipa(int idPipa) {
        IdPipa = idPipa;
    }

    public String getClaveProceso() {
        return ClaveProceso;
    }

    public void setClaveProceso(String claveProceso) {
        ClaveProceso = claveProceso;
    }

    public String getNombrePipa() {
        return NombrePipa;
    }

    public void setNombrePipa(String nombrePipa) {
        NombrePipa = nombrePipa;
    }

    public int getIdTipoMedidor() {
        return IdTipoMedidor;
    }

    public void setIdTipoMedidor(int idTipoMedidor) {
        IdTipoMedidor = idTipoMedidor;
    }

    public String getTipoMedidor() {
        return TipoMedidor;
    }

    public void setTipoMedidor(String tipoMedidor) {
        TipoMedidor = tipoMedidor;
    }

    public int getCantidadFotografias() {
        return CantidadFotografias;
    }

    public void setCantidadFotografias(int cantidadFotografias) {
        CantidadFotografias = cantidadFotografias;
    }

    public int getCantidadP5000() {
        return CantidadP5000;
    }

    public void setCantidadP5000(int cantidadP5000) {
        CantidadP5000 = cantidadP5000;
    }

    public String getImagenP5000() {
        return ImagenP5000;
    }

    public void setImagenP5000(String imagenP5000) {
        ImagenP5000 = imagenP5000;
    }

    public URI getImagenP5000URI() {
        return ImagenP5000URI;
    }

    public void setImagenP5000URI(URI imagenP5000URI) {
        ImagenP5000URI = imagenP5000URI;
    }

    public Double getPorcentajeMedidor() {
        return PorcentajeMedidor;
    }

    public void setPorcentajeMedidor(Double porcentajeMedidor) {
        PorcentajeMedidor = porcentajeMedidor;
    }

    public List<String> getImagenes() {
        return Imagenes;
    }

    public void setImagenes(List<String> imagenes) {
        Imagenes = imagenes;
    }

    public List<URI> getImagenesURI() {
        return ImagenesURI;
    }

    public void setImagenesURI(List<URI> imagenesURI) {
        ImagenesURI = imagenesURI;
    }
}
