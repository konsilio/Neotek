package com.example.neotecknewts.sagasapp.Activity;



import com.example.neotecknewts.sagasapp.Model.MenuDTO;

import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface MenuView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetMenu(List<MenuDTO> menuDTOs);
}