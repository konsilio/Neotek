package com.neotecknewts.sagasapp.Activity;



import com.neotecknewts.sagasapp.Model.MenuDTO;

import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */
//interfaz que indica los metodos relacionados con la vista que el interactor usara
public interface MenuView {
    void showProgress(int mensaje);
    void hideProgress();
    void messageError(int mensaje);
    void onSuccessGetMenu(List<MenuDTO> menuDTOs);
}
