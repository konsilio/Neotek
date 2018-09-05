package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.MenuItem;

import com.example.neotecknewts.sagasapp.Adapter.MenuAdapter;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Presenter.MenuPresenter;
import com.example.neotecknewts.sagasapp.Presenter.MenuPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class MenuActivity extends AppCompatActivity implements MenuView {

    //lista que se usa para llenar el recycler view que crea el menu
    ArrayList<MenuDTO> menu;

    //clase de la session
    Session session;

    //objeto del recycler view
    RecyclerView recyclerView;

    //adapter para el reclycler view
    MenuAdapter adapter;

    //presenter
    MenuPresenter presenter;

    //cuadro de progreso
    ProgressDialog progressDialog;
    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_menu);

        //se inicializa menu y presenter
        session = new Session(getApplicationContext());
        presenter = new MenuPresenterImpl(this);

        //de la vista se obtiene el recylcer view
        recyclerView = findViewById(R.id.recyclerView);

        menu = new ArrayList<>();
/*        menu.add("Iniciar Descarga");
        menu.add("Finalizar Descarga");
        menu.add("Inventario General");
        menu.add("Ordenes de compra");*/

        //se obtiene el menu del login, en caso de que se llegue del login se hace un llamado a web service
        Bundle extras = getIntent().getExtras();


        if (extras != null) {
            menu =  (ArrayList<MenuDTO>) extras.getSerializable("lista");
            menu.get(0).getName();
            // and get whatever type user account id is
        }else{
            presenter.getMenu(session.getTokenWithBearer());
        }
        MenuDTO m = new MenuDTO();
        m.setHeaderMenu("Toma de lectura");
        m.setName("Estación de carb. (Inicial)");
        MenuDTO m2 = new MenuDTO();
        m2.setHeaderMenu("Toma de lectura");
        m2.setName("Estación de carb. (Final)");
        MenuDTO m3 = new MenuDTO();
        m3.setHeaderMenu("Recarga - Toma de lectura");
        m3.setName("Pipa (Inicial)");
        MenuDTO m4 = new MenuDTO();
        m4.setHeaderMenu("Recarga - Toma de lectura");
        m4.setName("Pipa (Final)");
        MenuDTO m5 = new MenuDTO();
        m5.setHeaderMenu("Toma de lectura");
        m5.setName("Almacén Pral. (Inicial)");
        MenuDTO m6 = new MenuDTO();
        m6.setHeaderMenu("Toma de lectura");
        m6.setName("Almacén Pral. (Final)");
        MenuDTO m7 = new MenuDTO();
        m7.setHeaderMenu("Toma de lectura");
        m7.setName("Camioneta (Inicial)");
        MenuDTO m8 = new MenuDTO();
        m8.setHeaderMenu("Toma de lectura");
        m8.setName("Camioneta (Final)");
        //m.setImageRef(R.g);
        menu.add(m);
        menu.add(m2);
        menu.add(m3);
        menu.add(m4);
        menu.add(m5);
        menu.add(m6);
        menu.add(m7);
        menu.add(m8);

        //se agrega la lista al adapter y se agrega el adapter al recylcer view
        adapter = new MenuAdapter(menu);
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        //DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),DividerItemDecoration.HORIZONTAL);
        DividerItemDecoration decoration = new DividerItemDecoration(recyclerView.getContext(), DividerItemDecoration.HORIZONTAL                                                                                                                           );
        recyclerView.addItemDecoration(decoration);



    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                this.finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }

    }
    //metodo que muestra algun mensaje
    private void showDialog(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    //metodo que muestra el progreso de la obtencion de datos
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    //metodo que oculta el progreso
    @Override
    public void hideProgress() {
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    //metodo que muestra mensaje de error
    @Override
    public void messageError(int mensaje) {
        showDialog(getResources().getString(mensaje));
    }

    //metodo que se llama al obtener el menu desde web service
    @Override
    public void onSuccessGetMenu(List<MenuDTO> menuDTOs) {
        Log.w("OnSuccesView",""+menuDTOs.size());
        ArrayList<MenuDTO> menus = new ArrayList<>(menuDTOs.size());
        menus.addAll(menuDTOs);
       menu.clear();
        menu.addAll(menuDTOs);
        adapter.notifyDataSetChanged();
    }
}
