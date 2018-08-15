package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Model.MenuDTO;

import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public interface MenuPresenter {
    void getMenu(String token);
    void onSuccessGetMenu(List<MenuDTO> menuDTOList);
    void onError();
}
