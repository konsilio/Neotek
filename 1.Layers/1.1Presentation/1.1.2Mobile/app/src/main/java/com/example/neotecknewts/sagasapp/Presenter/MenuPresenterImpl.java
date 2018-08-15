package com.example.neotecknewts.sagasapp.Presenter;

import com.example.neotecknewts.sagasapp.Activity.MenuView;
import com.example.neotecknewts.sagasapp.Interactor.MenuInteractor;
import com.example.neotecknewts.sagasapp.Interactor.MenuInteractorImpl;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

/**
 * Created by neotecknewts on 15/08/18.
 */

public class MenuPresenterImpl implements MenuPresenter {
    MenuInteractor interactor;
    MenuView menuView;

    public MenuPresenterImpl(MenuView view){
        this.menuView = view;
        this.interactor = new MenuInteractorImpl(this);
    }

    @Override
    public void getMenu(String token) {
        menuView.showProgress(R.string.message_cargando);
        interactor.getMenu(token);
    }

    @Override
    public void onSuccessGetMenu(List<MenuDTO> menuDTOList) {
        menuView.hideProgress();
        menuView.onSuccessGetMenu(menuDTOList);
    }

    @Override
    public void onError() {
        menuView.hideProgress();
        menuView.messageError(R.string.error_conexion);
    }
}
