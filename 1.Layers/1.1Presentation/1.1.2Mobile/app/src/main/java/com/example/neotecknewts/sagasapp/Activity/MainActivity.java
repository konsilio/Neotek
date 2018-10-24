package com.example.neotecknewts.sagasapp.Activity;

import android.app.ActivityOptions;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
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

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LoginPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Utilidades;
import com.google.firebase.iid.FirebaseInstanceId;

import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class MainActivity extends AppCompatActivity implements MainView{

    //variables relacionadas con la vista
    private EditText editTextCorreoElectronico;
    private EditText editTextContraseña;
    private Spinner spinnerGaseras;

    //variable para usuario y contraseña
    public String contraseña;
    public String usuario;

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

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //se inicializa la session
        session = new Session(getApplicationContext());

        //se inicializan las variables de la vista
        editTextContraseña = (EditText) findViewById(R.id.input_password);
        editTextCorreoElectronico = (EditText) findViewById(R.id.input_username);
        spinnerGaseras = (Spinner) findViewById(R.id.spinner_gasera);
        //linearLayoutLogin = (LinearLayout) findViewById(R.id.layout_iniciar);
        //linearLayoutReintentar = (LinearLayout) findViewById(R.id.layout_reintentar);


        empresaDTOs = new ArrayList<>();


        editTextContraseña.setText("saadmin");
        editTextCorreoElectronico.setText("sa@k.com");


        //linearLayoutLogin.setVisibility(View.GONE);

        //se inicializa el presenter
        loginPresenter = new LoginPresenterImpl(this);

        //se obtienen las empresas para llenar el spinner
        loginPresenter.getEmpresas();

        //onclick del boton
        final Button buttonLogin = (Button) findViewById(R.id.login_button);
        buttonLogin.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickLogin();
            }
        });
        editTextContraseña.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if(actionId == EditorInfo.IME_ACTION_DONE) {
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
//al hacer click en el boton entra en este metodo
    private void onClickLogin(){
        //se obtienen los datos de la vista
        usuario = editTextCorreoElectronico.getText().toString();
        contraseña = editTextContraseña.getText().toString();
        IdEmpresa = empresaDTOs.get(spinnerGaseras.getSelectedItemPosition()).getIdEmpresa();

        //se verifica que no haya campos vacios y que sea un correo valido, y en caso contrario se muestra un mensaje
        if(TextUtils.isEmpty(usuario) || TextUtils.isEmpty(contraseña)){
            showDialog(getResources().getString(R.string.empty_field));
        }else if(!android.util.Patterns.EMAIL_ADDRESS.matcher(usuario).matches()) {
            showDialog(getResources().getString(R.string.invalid_email));
        }else{
            //String fb_token = "";
            try{
                //se codifica la contraseña en SHA256
                Log.e("SAAAA", Utilidades.getHash(contraseña));
                Log.e("SAAAA", IdEmpresa+"");
                //showDialog( Utilidades.getHash(contraseña));
                this.contraseña = Utilidades.getHash(contraseña);
                fb_token =FirebaseInstanceId.getInstance().getToken();
                Log.w("FireBaseToken",fb_token );

            }catch (NoSuchAlgorithmException ex){
                ex.printStackTrace();
            }
            //startActivity();
            //se completa el objeto para mandar a iniciar sesion
            UsuarioLoginDTO usuarioLoginDTO = new UsuarioLoginDTO();
            usuarioLoginDTO.setIdEmpresa(IdEmpresa);
            usuarioLoginDTO.setPassword(this.contraseña);
            usuarioLoginDTO.setUsuario(usuario);
            usuarioLoginDTO.setFbToken(fb_token);
            //usuarioLoginDTO.setFBToken(FirebaseInstanceId.getInstance().getToken());
            //por medio del presenter se llama al web service con el objeto de usuario
            loginPresenter.doLogin(usuarioLoginDTO);
        }


    }


/// funcion que muestra el dialogo
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

    //funcion que inicia el activity del menu y le envia la lista para construirlo
    public void startActivity(ArrayList<MenuDTO> menuDTOs){
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        intent.putExtra("lista",menuDTOs);
        startActivity(intent);
    }


    //metodo que muestra el progreso de la obtencion/ envio de datos
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    //metodo que oculta el progreso
    @Override
    public void hideProgress() {
        Log.e("error", "ocultar");
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    //metodo que envia un mensaje de error
    @Override
    public void messageError(String mensaje) {
        if(mensaje==null) {
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
        for (int i =0; i<empresaDTOs.size(); i++){
            empresas[i]=empresaDTOs.get(i).getNombreComercial();
        }

        spinnerGaseras.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, empresas));

    }
    

    //funcion que se ejecuta cuando el login fue correcto
    @Override
    public void onSuccessLogin(UsuarioDTO usuarioDTO) {
        if(usuarioDTO.isExito()){
            Log.w("TOKEN",usuarioDTO.getToken()+"");
            Log.w("USUARIO",usuarioDTO.getIdUsuario()+"");
            Log.w("LISTA",usuarioDTO.getListMenu().length+"");
            if(usuarioDTO.getListMenu().length==0){
                showDialog(getResources().getString(R.string.usuario_sin_permisos));
            }else{
                //showDialog(getResources().getString(R.string.login_sucess));
                Log.w("success",getResources().getString(R.string.login_sucess));
                //se crea la sesion
                session.createLoginSession(contraseña,usuario,usuarioDTO.getToken(),IdEmpresa,
                        fb_token,"");
                ArrayList<MenuDTO> menuDTOs = new ArrayList<MenuDTO>(Arrays.asList(usuarioDTO.getListMenu()));
                startActivity(menuDTOs);
            }
        }else{
            showDialog(getResources().getString(R.string.usuario_incorrecto));
        }

    }

}
