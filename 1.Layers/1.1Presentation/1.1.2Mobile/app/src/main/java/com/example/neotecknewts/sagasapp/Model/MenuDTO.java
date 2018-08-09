package com.example.neotecknewts.sagasapp.Model;

import com.google.gson.annotations.SerializedName;

/**
 * Created by neotecknewts on 09/08/18.
 */

public class MenuDTO {
    @SerializedName("headerMenu")
    private String headerMenu;

    @SerializedName("name")
    private String name;

    @SerializedName("imageRef")
    private String imageRef;

    public String getHeaderMenu() {
        return headerMenu;
    }

    public void setHeaderMenu(String headerMenu) {
        this.headerMenu = headerMenu;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getImageRef() {
        return imageRef;
    }

    public void setImageRef(String imageRef) {
        this.imageRef = imageRef;
    }
}
