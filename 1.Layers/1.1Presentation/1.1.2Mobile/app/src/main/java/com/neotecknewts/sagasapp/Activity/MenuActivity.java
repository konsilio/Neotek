package com.neotecknewts.sagasapp.Activity;

import android.Manifest;
import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.graphics.Point;
import android.graphics.Typeface;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.Display;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;

import com.google.firebase.iid.FirebaseInstanceId;
import com.neotecknewts.sagasapp.Model.RecargaDTO;
import com.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.neotecknewts.sagasapp.Presenter.LoginPresenterImpl;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Adapter.MenuAdapter;
import com.neotecknewts.sagasapp.Model.MenuDTO;
import com.neotecknewts.sagasapp.Presenter.MenuPresenter;
import com.neotecknewts.sagasapp.Presenter.MenuPresenterImpl;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Semaforo;
import com.neotecknewts.sagasapp.Util.Session;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import it.sephiroth.android.library.tooltip.Tooltip;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class MenuActivity extends AppCompatActivity implements MenuView {

    String fb_token;
    private Location loc;
    private LocationManager locationManager;
    double longitudeNetwork = 0, latitudeNetwork = 0, accuracy = 0;

    private SAGASSql sagasSql;

    //lista que se usa para llenar el recycler view que crea el menu
    ArrayList<MenuDTO> menu;

    //clase de la session
    Session session;

    //variable para usuario y contraseña y empresa
    public String contraseña;
    public String usuario;
    public int IdEmpresa;

    //objeto del recycler view
    RecyclerView recyclerView;

    //mainActivity
    MainActivity mainActivity;

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

    LoginPresenter loginPresenter;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_menu);

        SAGASSql dbHelper = new SAGASSql(this);
        loginPresenter = new LoginPresenterImpl(this, dbHelper);

        //se inicializa menu y presenter
        session = new Session(getApplicationContext());
        presenter = new MenuPresenterImpl(this);

        //de la vista se obtiene el recylcer view
        recyclerView = findViewById(R.id.recyclerView);
        recyclerView.setHasFixedSize(true);

        this.sagasSql = new SAGASSql(this);

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // hAcc= precision en metros
            checkLocation();
            Log.d("localizacion", loc.toString());
            return;
        } else {
            locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
            loc = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
            Log.d("localizacion", loc.toString());

            if (ActivityCompat.checkSelfPermission(MenuActivity.this, Manifest.permission.ACCESS_FINE_LOCATION)
                    != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission
                    (MenuActivity.this, Manifest.permission.ACCESS_COARSE_LOCATION)
                    != PackageManager.PERMISSION_GRANTED) {
                // usuarioLoginDTO.setCoordenadas(loc.getLatitude() + "," + loc.getLongitude());
                return;
            }

            checkLocation();
            locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1, 1, locationListenerNetwork);
            // Se asigna a la clase LocationManager el servicio a nivel de sistema a partir del nombre.
            // Log.d("localizacion", loc.toString());
            // Log.d("accuracy", loc.getAccuracy()+"");
        }


        menu = new ArrayList<>();

        //se obtiene el menu del login, en caso de que se llegue del login se hace un llamado a web service
        Bundle extras = getIntent().getExtras();

        if (extras != null) {
            menu = (ArrayList<MenuDTO>) extras.getSerializable("lista");
            menu.get(0).getName();
            // and get whatever type user account id is
        } else {
            if (session.isLogin()) {
                if (isOnline()) {
                    presenter.getMenu(session.getTokenWithBearer());
                } else {
                    Cursor cursor = sagasSql.getMenuDTO();
                    Log.d("Cursor", cursor.toString());
                    if (cursor.moveToFirst()) {
                        Log.d("ali", cursor.moveToFirst()+"" );
                        menu = new ArrayList<MenuDTO>();
                        for (int i=0; i<cursor.getCount(); i++){
                            cursor.moveToPosition(i);
                            Log.d("getstring", cursor.getString(cursor.getColumnIndex("headerMenu")));
                            menu.add(new MenuDTO(
                                    cursor.getString(cursor.getColumnIndex("headerMenu")),
                                    cursor.getString(cursor.getColumnIndex("name")),
                                    cursor.getString(cursor.getColumnIndex("imageRef"))));
                        }
                        Log.d("getcount", cursor.getCount()+"");
/*
                        while (cursor.moveToNext() || cursor.isLast()) {
                            Log.d("islast", cursor.isLast()+"");
                            Log.d("afterlast", cursor.isAfterLast()+"");
                            Log.d("beforefirst", cursor.isBeforeFirst()+"");
                            menu.add(new MenuDTO(
                                    cursor.getString(cursor.getColumnIndex("headerMenu")),
                                    cursor.getString(cursor.getColumnIndex("name")),
                                    cursor.getString(cursor.getColumnIndex("imageRef"))));
                        }
*/
                    }
                }
            } else {

            }

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
        DividerItemDecoration decoration = new DividerItemDecoration(recyclerView.getContext(), DividerItemDecoration.HORIZONTAL);
        recyclerView.addItemDecoration(decoration);
        semaforo = new Semaforo(this);
        Display display = getWindowManager().getDefaultDisplay();
        size = new Point();
        display.getSize(size);
        context = this;
        enviarDatos();
    }

    private boolean isLocationEnabled() {
        return locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER) ||
                locationManager.isProviderEnabled(LocationManager.NETWORK_PROVIDER);
    }

    private void showAlert() {
        final AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle("Localizacion desactivada")
                .setMessage("Su ubicación esta desactivada.\nPor favor active su ubicación " +
                        "vaya a configuración")
                .setPositiveButton("Configuración de ubicación", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface paramDialogInterface, int paramInt) {
                        Intent myIntent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                        startActivity(myIntent);
                    }
                })
                .setNegativeButton("Cancelar", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface paramDialogInterface, int paramInt) {
                    }
                });
        dialog.show();
    }

    private boolean checkLocation() {
        if (!isLocationEnabled())
            showAlert();
        return isLocationEnabled();
    }

    private final LocationListener locationListenerNetwork = new LocationListener() {
        public void onLocationChanged(Location location) {
            longitudeNetwork = location.getLongitude();
            latitudeNetwork = location.getLatitude();
            accuracy = location.getAccuracy();
            Log.d("precision changed", accuracy + "");
            Log.d("coordenadas: ", longitudeNetwork + "," + latitudeNetwork + "");
        }

        @Override
        public void onStatusChanged(String s, int i, Bundle bundle) {

        }

        @Override
        public void onProviderEnabled(String s) {

        }

        @Override
        public void onProviderDisabled(String s) {

        }
    };


    public boolean isOnline() {
        Runtime runtime = Runtime.getRuntime();
        try {
            Process ipProcess = runtime.exec("/system/bin/ping -c 1 8.8.8.8");
            int exitValue = ipProcess.waitFor();
            return (exitValue == 0);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }

    private void enviarDatos() {
        List<String> mensajes = semaforo.obtenerCantidadesRestantes();
        AlertDialog.Builder alertDialog = new AlertDialog.Builder(this, R.style.AlertDialog);
        alertDialog.setTitle("Pendietes");
        String men = "";
        for (String mensaje : mensajes) {
            men += mensaje + "\n";
        }
        if (mensajes.size() > 0) {
            Log.d("sincronizando", "sincronizando");
            alertDialog.setMessage(men);
            alertDialog.setPositiveButton("Sincronizar", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            dialogInterface.dismiss();
                            progressSincronizar = new ProgressDialog(MenuActivity.this, R.style.AlertDialog);
                            progressSincronizar.setIndeterminate(true);
                            progressSincronizar.setMessage(getString(R.string.message_cargando));
                            progressSincronizar.setTitle(R.string.app_name);
                            progressSincronizar.show();
                            //semaforo.sincronizar(session.getToken(),progressSincronizar);
                            semaforo.sincronizar(session.getToken());
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
            case R.id.RegistrarEntrada:
                UsuarioLoginDTO usuarioLoginDTO = new UsuarioLoginDTO();
                fb_token = FirebaseInstanceId.getInstance().getToken();
                usuarioLoginDTO.setFbToken(fb_token);
                Log.d("token menu", fb_token);
                usuarioLoginDTO.setPassword("1A6FF8F796ED193A72C5D8A3F8A4E173CED67372FD4A282F6F5C38C1DA8AF010");
                usuarioLoginDTO.setIdEmpresa(2);
                usuarioLoginDTO.setUsuario("planta.anden@gmail.com");
                usuarioLoginDTO.setCoordenadas(latitudeNetwork + "," + longitudeNetwork);
                Log.d("usuariodto", usuarioLoginDTO.toString());

                loginPresenter.doRegistrar(usuarioLoginDTO, session.getToken());
                AlertDialog.Builder builder = new AlertDialog.Builder(MenuActivity.this, R.style.AlertDialog);
                //builder.setMessage("Se ha registado su ubicación: " + latitudeNetwork + "," + longitudeNetwork);
                builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
                    dialogInterface.dismiss();
                });
                builder.create().show();
                return true;
            case R.id.pendientes:
                /*for (String mensaje:semaforo.obtenerCantidadesRestantes()){
                    Log.w("Mensaje",mensaje);
                }*/
                enviarDatos();
                Tooltip.make(this,
                        new Tooltip.Builder(101)
                                .withStyleId(R.style.TooltipError)
                                .anchor(new Point(size.x - 120, 55), Tooltip.Gravity.BOTTOM)
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
                                .anchor(new Point(size.x - 120, 55), Tooltip.Gravity.BOTTOM)
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
    private void showDialog(String mensaje) {
        Log.d("FerChido", mensaje);
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this, R.style.AlertDialog);
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
        progressDialog = ProgressDialog.show(this, getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    @SuppressLint("NewApi")
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.main, menu);

        MenuItem pendientes = menu.findItem(R.id.pendientes);
        MenuItem libres = menu.findItem(R.id.libres);
        //semaforo.sincronizar(session.getToken(),progressDialog);
        if (semaforo.VerificarEstatus()) {
           /* for (String mensaje:semaforo.obtenerCantidadesRestantes()){
                Log.w("Mensaje",mensaje);
            }*/
            //enviarDatos();
            pendientes.setVisible(true);
            Tooltip.make(this,
                    new Tooltip.Builder(101)
                            .withStyleId(R.style.TooltipError)
                            .anchor(new Point(size.x - 120, 55), Tooltip.Gravity.BOTTOM)
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
        } else {
            Log.w("procesoSinc", semaforo.VerificarEstatus() + "");
            libres.setVisible(true);

            Tooltip.make(this,
                    new Tooltip.Builder(101)
                            .withStyleId(R.style.TooltipGood)
                            .anchor(new Point(size.x - 120, 55), Tooltip.Gravity.BOTTOM)
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
        if (progressDialog != null) {
            progressDialog.dismiss();
        }
    }

    //metodo que muestra mensaje de error
    @Override
    public void messageError(int mensaje) {
        showDialog(getResources().getString(mensaje));
    }

    @Override
    public void messageError(String mensaje) {
        showDialog(mensaje);
    }

    //metodo que se llama al obtener el menu desde web service
    @Override
    public void onSuccessGetMenu(List<MenuDTO> menuDTOs) {
        ArrayList<MenuDTO> menus = new ArrayList<>(menuDTOs.size());
        boolean hayPuntoDeVenta = false;

        for(int x = 0; x < menuDTOs.size(); x++){
            if (menuDTOs.get(x).getName().equals("Punto de Venta"))
                hayPuntoDeVenta = true;
        }
        if(hayPuntoDeVenta)
            menuDTOs.add(new MenuDTO("Tickets del día", "Ventas realizadas", "ic_papeleta"));

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
