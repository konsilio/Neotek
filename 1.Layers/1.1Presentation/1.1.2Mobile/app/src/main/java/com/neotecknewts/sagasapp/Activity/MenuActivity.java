package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Point;
import android.graphics.Typeface;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.Display;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Adapter.MenuAdapter;
import com.neotecknewts.sagasapp.Model.MenuDTO;
import com.neotecknewts.sagasapp.Presenter.MenuPresenter;
import com.neotecknewts.sagasapp.Presenter.MenuPresenterImpl;
import com.neotecknewts.sagasapp.Util.Semaforo;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

import it.sephiroth.android.library.tooltip.Tooltip;

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

    Semaforo semaforo;
    Point size;
    Context context;
    ProgressDialog progressSincronizar;
    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_menu);

        //se inicializa menu y presenter
        session = new Session(getApplicationContext());
        presenter = new MenuPresenterImpl(this);

        //de la vista se obtiene el recylcer view
        recyclerView = findViewById(R.id.recyclerView);
        recyclerView.setHasFixedSize(true);

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
        /*MenuDTO mA = new MenuDTO();
        mA.setHeaderMenu("Disposición de efectivo - Anticipo");
        mA.setName("Estación Carburación");
        mA.setImageRef("ic_anticipo");
        menu.add(mA);
        MenuDTO mAC = new MenuDTO();
        mAC.setHeaderMenu("Disposición de efectivo - Corte de caja");
        mAC.setName("Estación Carburación");
        mAC.setImageRef("ic_corte_caja");
        menu.add(mAC);
        MenuDTO mCPV = new MenuDTO();
        mCPV.setHeaderMenu("Camioneta de cilindros");
        mCPV.setName("Punto de Venta");
        mCPV.setImageRef("ic_punto_venta");
        menu.add(mCPV);
        MenuDTO mECV = new MenuDTO();
        mECV.setHeaderMenu("Estación de Carburación");
        mECV.setName("Punto de Venta");
        mECV.setImageRef("ic_punto_venta");
        menu.add(mECV);
        MenuDTO mPV = new MenuDTO();
        mPV.setHeaderMenu("Pipa");
        mPV.setName("Punto de Venta");
        mPV.setImageRef("ic_punto_venta");
        menu.add(mPV);*/
        //se agrega la lista al adapter y se agrega el adapter al recylcer view
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new MenuAdapter(menu);
        recyclerView.setAdapter(adapter);
        //DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),DividerItemDecoration.HORIZONTAL);
        DividerItemDecoration decoration = new DividerItemDecoration(recyclerView.getContext(), DividerItemDecoration.HORIZONTAL                                                                                                                           );
        recyclerView.addItemDecoration(decoration);
        semaforo = new Semaforo(this);
        Display display = getWindowManager().getDefaultDisplay();
        size = new Point();
        display.getSize(size);
        context =this;
        enviarDatos();
    }

    private void enviarDatos() {
        List<String> mensajes = semaforo.obtenerCantidadesRestantes();
        AlertDialog.Builder alertDialog = new AlertDialog.Builder(this,R.style.AlertDialog);
        alertDialog.setTitle("Pendietes");
        String men ="";
        for (String mensaje:mensajes) {
            men+=mensaje+"\n";
        }
        if(mensajes.size()>0) {
            alertDialog.setMessage(men);
            alertDialog.setPositiveButton("Sincronizar", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            progressSincronizar = new ProgressDialog(MenuActivity.this,R.style.AlertDialog);
                            progressSincronizar.setIndeterminate(true);
                            progressSincronizar.setMessage(getString( R.string.message_cargando));
                            progressSincronizar.setTitle(R.string.app_name);
                            progressSincronizar.show();
                            //semaforo.sincronizar(session.getToken(),progressSincronizar);
                            semaforo.sincronizar(session.getToken());
                            dialogInterface.dismiss();
                        }
                    }
            );
            alertDialog.create().show();
        }
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                this.finish();
                return true;
            case R.id.pendientes:
                /*for (String mensaje:semaforo.obtenerCantidadesRestantes()){
                    Log.w("Mensaje",mensaje);
                }*/
                enviarDatos();
                Tooltip.make(this,
                        new Tooltip.Builder(101)
                                .withStyleId(R.style.TooltipError)
                                .anchor(new Point(size.x - 120,55), Tooltip.Gravity.BOTTOM)
                                .closePolicy(new Tooltip.ClosePolicy()
                                        .insidePolicy(true, false)
                                        .outsidePolicy(true, false), 3000)
                                .activateDelay(800)
                                .showDelay(300)
                                .text("Existen datos pendientes en el dispositivo")
                                .maxWidth(500)
                                .withArrow(true)
                                .withOverlay(true)
                                .typeface(Typeface.DEFAULT)
                                .floatingAnimation(Tooltip.AnimationBuilder.DEFAULT)
                                .build()
                ).show();
                //enviarDatos();
                return true;
            case R.id.libres:
                enviarDatos();
                Tooltip.make(this,
                        new Tooltip.Builder(101)
                                .withStyleId(R.style.TooltipGood)
                                .anchor(new Point(size.x - 120,55), Tooltip.Gravity.BOTTOM)
                                .closePolicy(new Tooltip.ClosePolicy()
                                        .insidePolicy(true, false)
                                        .outsidePolicy(true, false), 3000)
                                .activateDelay(800)
                                .showDelay(300)
                                .text("No tienes datos pendientes de enviar")
                                .maxWidth(500)
                                .withArrow(true)
                                .withOverlay(true)
                                .typeface(Typeface.DEFAULT)
                                .floatingAnimation(Tooltip.AnimationBuilder.DEFAULT)
                                .build()
                ).show();
                return true;
            case R.id.salir:
                session.logOut();
                Intent intent = new Intent(MenuActivity.this, SplashActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
                return true;

            default:
                return super.onOptionsItemSelected(item);
        }

    }
    //metodo que muestra algun mensaje
    private void showDialog(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this,R.style.AlertDialog);
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
    @SuppressLint("NewApi")
    @Override
    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.main,menu);

        MenuItem pendientes = menu.findItem(R.id.pendientes);
        MenuItem libres = menu.findItem(R.id.libres);
        //semaforo.sincronizar(session.getToken(),progressDialog);
        if(semaforo.VerificarEstatus()) {
           /* for (String mensaje:semaforo.obtenerCantidadesRestantes()){
                Log.w("Mensaje",mensaje);
            }*/
            //enviarDatos();
            pendientes.setVisible(true);
            Tooltip.make(this,
                    new Tooltip.Builder(101)
                            .withStyleId(R.style.TooltipError)
                            .anchor(new Point(size.x - 120,55), Tooltip.Gravity.BOTTOM)
                            .closePolicy(new Tooltip.ClosePolicy()
                                    .insidePolicy(true, false)
                                    .outsidePolicy(true, false), 3000)
                            .activateDelay(800)
                            .showDelay(300)
                            .text("Existen datos pendientes en el dispositivo")
                            .maxWidth(500)
                            .withArrow(true)
                            .withOverlay(true)
                            .typeface(Typeface.DEFAULT)
                            .floatingAnimation(Tooltip.AnimationBuilder.DEFAULT)
                            .build()
            ).show();
        }else {
            libres.setVisible(true);

            Tooltip.make(this,
                    new Tooltip.Builder(101)
                            .withStyleId(R.style.TooltipGood)
                            .anchor(new Point(size.x - 120,55), Tooltip.Gravity.BOTTOM)
                            .closePolicy(new Tooltip.ClosePolicy()
                                    .insidePolicy(true, false)
                                    .outsidePolicy(true, false), 3000)
                            .activateDelay(800)
                            .showDelay(300)
                            .text("No tienes datos pendientes de enviar")
                            .maxWidth(500)
                            .withArrow(true)
                            .withOverlay(true)
                            .typeface(Typeface.DEFAULT)
                            .floatingAnimation(Tooltip.AnimationBuilder.DEFAULT)
                            .build()
            ).show();
        }
        return true;
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
        ArrayList<MenuDTO> menus = new ArrayList<>(menuDTOs.size());
        menus.addAll(menuDTOs);
       menu.clear();
        menu.addAll(menuDTOs);
        adapter.notifyDataSetChanged();
    }

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if (keyCode == KeyEvent.KEYCODE_BACK) {
            //finish();
            moveTaskToBack(true);
        }
        return false;
    }
}
