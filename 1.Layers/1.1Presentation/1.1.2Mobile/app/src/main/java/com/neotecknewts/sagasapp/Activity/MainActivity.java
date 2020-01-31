package com.neotecknewts.sagasapp.Activity;

import android.Manifest;
import android.app.Activity;
import android.app.PendingIntent;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.Settings;
import android.renderscript.RenderScript;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.view.inputmethod.EditorInfo;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import com.neotecknewts.sagasapp.Model.Cortes.UsuariosDTO;
import com.neotecknewts.sagasapp.R;
import com.google.firebase.iid.FirebaseInstanceId;
import com.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.neotecknewts.sagasapp.Model.MenuDTO;
import com.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.neotecknewts.sagasapp.Presenter.LoginPresenterImpl;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Permisos;
import com.neotecknewts.sagasapp.Util.Session;
import com.neotecknewts.sagasapp.Util.Utilidades;

import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Timer;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class MainActivity extends AppCompatActivity implements MainView {
    // variables latitud y longitud
    private Double tvLatitud, tvLongitud, tvAltura, tvPrecision;

    private LocationListener locationListener;
    private LocationManager locManager;
    private Location loc;
    public String bestProvider;
    public Criteria criteria;
    private LocationManager locationManager;
    double longitudeNetwork = 0, latitudeNetwork = 0, accuracy = 0;

    //variables relacionadas con la vista
    private EditText editTextCorreoElectronico;
    private EditText editTextContraseña;
    private Spinner spinnerGaseras;

    //variable para usuario y contraseña
    public String contraseña;
    public String usuario;

    UsuarioLoginDTO usuarioLoginDTO;

    //variable para id de empresa y la lista de empresas a seleccionar
    public int IdEmpresa;
    List<EmpresaDTO> empresaDTOs;

    //presenter, el que se encarga de interacturar entre la vista y el interactor(el que hace las llamadas al web service)
    LoginPresenter loginPresenter;

    //dialogo de progreso que se muestra al obtener datos
    ProgressDialog progressDialog;

    //clase de la session
    Session session;
    //Token que genera firebase
    String fb_token;
    SharedPreferences prefs;


    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        if (Utilidades.checkAndRequestPermissions(this).size() > 0) {
            Permisos permisos = new Permisos(this);
            permisos.permisos();
        }

        // prefs = getSharedPreferences("token", Context.MODE_PRIVATE);
        // String tokenpref = prefs.getString("fb_token", fb_token);

        SAGASSql dbHelper = new SAGASSql(this);
        SQLiteDatabase db = dbHelper.getWritableDatabase();

        if (db != null) {
            // Hacer las operaciones que queramos sobre la base de datos
            //db.execSQL("INSERT INTO comments (headerMenu, name, imageRef) VALUES (MenuDTO)");
        }
        //se inicializa la session
        session = new Session(getApplicationContext());

        ActivityCompat.requestPermissions(MainActivity.this, new String[]{Manifest.permission.ACCESS_FINE_LOCATION}, 1);

        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // hAcc= precision en metros
            // checkLocation();
           //  Log.d("localizacion", loc.toString());
            return;
        } else {
            locationManager = (LocationManager) getSystemService(Context.LOCATION_SERVICE);
            loc = locationManager.getLastKnownLocation(LocationManager.NETWORK_PROVIDER);
            // Log.d("localizacion", loc.toString());

            if (ActivityCompat.checkSelfPermission(MainActivity.this, Manifest.permission.ACCESS_FINE_LOCATION)
                    != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission
                    (MainActivity.this, Manifest.permission.ACCESS_COARSE_LOCATION)
                    != PackageManager.PERMISSION_GRANTED) {
                // usuarioLoginDTO.setCoordenadas(loc.getLatitude() + "," + loc.getLongitude());
                return;
            }

            // checkLocation();
            locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1, 1, locationListenerNetwork);
            // Se asigna a la clase LocationManager el servicio a nivel de sistema a partir del nombre.
            // Log.d("localizacion", loc.toString());
            // Log.d("accuracy", loc.getAccuracy()+"");
        }

        if (session.isLogin())
            startActivity();
        //se inicializan las variables de la vista
        editTextContraseña = (EditText) findViewById(R.id.input_password);
        editTextCorreoElectronico = (EditText) findViewById(R.id.input_username);
        spinnerGaseras = (Spinner) findViewById(R.id.spinner_gasera);
        //linearLayoutLogin = (LinearLayout) findViewById(R.id.layout_iniciar);
        //linearLayoutReintentar = (LinearLayout) findViewById(R.id.layout_reintentar);

        empresaDTOs = new ArrayList<>();

        //editTextContraseña.setText("saadmin");
        // editTextCorreoElectronico.setText(!session.getAttribute(Session.KEY_EMAIL).equals("") ?
        //        session.getAttribute(Session.KEY_EMAIL):"aaron.gallegos@neoteck.com.mx"
        //);

        //linearLayoutLogin.setVisibility(View.GONE);

        //se inicializa el presenter
        loginPresenter = new LoginPresenterImpl(this, dbHelper);

        //se obtienen las empresas para llenar el spinner
        loginPresenter.getEmpresas();

        //onclick del boton
        final Button buttonLogin = (Button) findViewById(R.id.login_button);
        buttonLogin.setOnClickListener(new View.OnClickListener() {

            // Log.d("precision", precision+"");
            public void onClick(View v) {
                // loc.setAccuracy(Criteria.ACCURACY_COARSE);
                // Float precision = loc.getAccuracy();

                onClickLogin();
                /*if (accuracy != 0) {
                    Log.d("precision", accuracy + "");
                   *//* AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this, R.style.AlertDialog);
                    builder.setMessage("La precision es de:" + accuracy);
                    builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
                        dialogInterface.dismiss();
                    });
                    builder.create().show();*//*
                    onClickLogin();
                } else {
                    buttonLogin.setEnabled(true);
                    AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this, R.style.AlertDialog);
                    builder.setMessage("No se puede establecer tu ubicación, vuelve a intentarlo" + accuracy);
                    builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
                        dialogInterface.dismiss();
                    });
                    builder.create().show();
                }*/
            }
        });

        editTextContraseña.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_DONE) {
                    buttonLogin.performClick();
                    return true;
                }
                return false;
            }
        });
       /* Date fecha = new Date();
        long millis = System.currentTimeMillis() % 1000;
        String str = ""+fecha.getDate()+""+fecha.getMonth()+""+(fecha.getYear()+1900)+""+fecha.getHours()+""+fecha.getMinutes()+""+fecha.getSeconds()+""+millis;
        Log.w("STRING",str);*/


    }

  /*  private boolean checkLocation() {
        if (!isLocationEnabled())
            showAlert();
        return isLocationEnabled();
    }*/

    private final LocationListener locationListenerNetwork = new LocationListener() {
        public void onLocationChanged(Location location) {
            longitudeNetwork = location.getLongitude();
            latitudeNetwork = location.getLatitude();
            accuracy = location.getAccuracy();
            Log.d("precision changed", accuracy + "");
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

   /* private boolean isLocationEnabled() {
        return locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER) ||
                locationManager.isProviderEnabled(LocationManager.NETWORK_PROVIDER);
    }*/

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


    //al hacer click en el boton entra en este metodo
    private void onClickLogin() {
        //se obtienen los datos de la vista
        usuario = editTextCorreoElectronico.getText().toString();
        contraseña = editTextContraseña.getText().toString();
        if (empresaDTOs.size() == 0) {
            AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
            builder.setTitle(R.string.error_titulo);
            builder.setMessage("No se ha especificado la empresa, o se perdio la conexión al " +
                    getString(R.string.error_conexion_lista_login));
            builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
                dialogInterface.dismiss();
                dialogInterface.dismiss();
            });
            builder.create().show();
        } else {
            IdEmpresa = empresaDTOs.get(spinnerGaseras.getSelectedItemPosition()).getIdEmpresa();
            //se verifica que no haya campos vacios y que sea un correo valido, y en caso contrario se muestra un mensaje
            if (TextUtils.isEmpty(usuario) || TextUtils.isEmpty(contraseña)) {
                Log.d("mensaje:", "campos vacios");
                showDialog(getResources().getString(R.string.empty_field));
            } else if (!android.util.Patterns.EMAIL_ADDRESS.matcher(usuario).matches()) {
                Log.d("mensaje:", "correo invalido");
                showDialog(getResources().getString(R.string.invalid_email));
            } else {
                //String fb_token = "";
                try {
                    //se codifica la contraseña en SHA256
                    Log.e("SAAAA", Utilidades.getHash(contraseña));
                    Log.e("SAAAA", IdEmpresa + "");
                    //showDialog( Utilidades.getHash(contraseña));
                    this.contraseña = Utilidades.getHash(contraseña);
                    fb_token = FirebaseInstanceId.getInstance().getToken();
                    Log.w("FireBaseToken", fb_token);

                } catch (NoSuchAlgorithmException ex) {
                    ex.printStackTrace();
                }
                //startActivity();
                //se completa el objeto para mandar a iniciar sesion
                UsuarioLoginDTO usuarioLoginDTO = new UsuarioLoginDTO();
                usuarioLoginDTO.setIdEmpresa(IdEmpresa);
                usuarioLoginDTO.setPassword(this.contraseña);
                usuarioLoginDTO.setUsuario(usuario);
                usuarioLoginDTO.setFbToken(fb_token);
                //Log.d("usuariodto", usuarioLoginDTO.toString());
                //usuarioLoginDTO.setCoordenadas("17.599863,-99.5208956");
                // usuarioLoginDTO.setCoordenadas(latitudeNetwork + "," + longitudeNetwork);
                //usuarioLoginDTO.setFBToken(FirebaseInstanceId.getInstance().getToken());
                //por medio del presenter se llama al web service con el objeto de usuario
                loginPresenter.doLogin(usuarioLoginDTO);
            }
        }
    }

    /// funcion que muestra el dialogo
    private void showDialog(String mensaje) {
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
        Log.d("showmessage", "alert mensaje");
        //alert11.dismiss();
    }

    //funcion que inicia el activity del menu y le envia la lista para construirlo
    public void startActivity(ArrayList<MenuDTO> menuDTOs) {
        //Cursor cursor = sagasSql.GetMenu();
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        intent.putExtra("lista", menuDTOs);
        startActivity(intent);

    }

    //funcion que inicia el activity del menu si ya tiene una sesion iniciada
    public void startActivity() {
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivity(intent);
    }


    //metodo que muestra el progreso de la obtencion/ envio de datos
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this, getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    //metodo que oculta el progreso
    @Override
    public void hideProgress() {
        Log.e("error", "ocultar");
        if (progressDialog != null) {
            progressDialog.dismiss();
        }
    }

    //metodo que envia un mensaje de error
    @Override
    public void messageError(String mensaje) {
        if (mensaje == null) {
            Log.d("mensaje:", "error conexion");
            mensaje = getResources().getString(R.string.error_conexion);
        }
        showDialog(mensaje);
    }

    //funcion que se llama al obtener todas las empresas y llena el spinner
    @Override
    public void onSuccessGetEmpresa(List<EmpresaDTO> empresaDTOs) {
        //linearLayoutLogin.setVisibility(View.GONE);
        this.empresaDTOs = empresaDTOs;
        String[] empresas = new String[empresaDTOs.size()];
        for (int i = 0; i < empresaDTOs.size(); i++) {
            empresas[i] = empresaDTOs.get(i).getNombreComercial();
        }

        spinnerGaseras.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, empresas));
        if (session != null) {
            if (session.getIdEmpresa() > 0) {
                for (EmpresaDTO empresa : empresaDTOs) {
                    if (empresa.getIdEmpresa() == session.getIdEmpresa())
                        spinnerGaseras.setSelection(empresaDTOs.indexOf(empresa));
                }
            }
        }
    }

    //funcion que se ejecuta cuando el login fue correcto
    @Override
    public void onSuccessLogin(UsuarioDTO usuarioDTO) {
        if (usuarioDTO.isExito()) {
            Log.w("TOKEN", usuarioDTO.getToken() + "");
            Log.w("USUARIO", usuarioDTO.getIdUsuario() + "");
            Log.w("LISTA", usuarioDTO.getListMenu().length + "");

            if (usuarioDTO.getListMenu().length == 0) {
                Log.d("mensaje:", "sin permisos");
                showDialog(getResources().getString(R.string.usuario_sin_permisos));
            } else {
                //showDialog(getResources().getString(R.string.login_sucess));
                Log.w("success", getResources().getString(R.string.login_sucess));
                //se crea la sesion

                String[] datos = usuarioDTO.getMensaje().trim().split("\\|");

                String nombre = datos[1];

                session.createLoginSession(contraseña, usuario, IdEmpresa, fb_token, "", nombre, usuarioDTO);

                ArrayList<MenuDTO> menuDTOs = new ArrayList<MenuDTO>(Arrays.asList(usuarioDTO.getListMenu()));
                startActivity(menuDTOs);
                finish();
            }
        } else {
            Log.d("mensaje:", "contraseña incorrecta");
            showDialog(getResources().getString(R.string.usuario_incorrecto));
        }

    }
}