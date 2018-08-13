package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.Spinner;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Presenter.LoginPresenter;
import com.example.neotecknewts.sagasapp.Presenter.LoginPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class MainActivity extends AppCompatActivity implements MainView{
    private EditText editTextCorreoElectronico;
    private EditText editTextContraseña;
    private Spinner spinnerGaseras;

    private LinearLayout linearLayoutLogin;
    private LinearLayout linearLayoutReintentar;

    public String contraseña;
    public String usuario;

    public int IdEmpresa;
    List<EmpresaDTO> empresaDTOs;

    LoginPresenter loginPresenter;

    ProgressDialog progressDialog;

    Session session;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        session = new Session(getApplicationContext());

        editTextContraseña = (EditText) findViewById(R.id.input_password);
        editTextCorreoElectronico = (EditText) findViewById(R.id.input_username);
        spinnerGaseras = (Spinner) findViewById(R.id.spinner_gasera);
        //linearLayoutLogin = (LinearLayout) findViewById(R.id.layout_iniciar);
        //linearLayoutReintentar = (LinearLayout) findViewById(R.id.layout_reintentar);

        empresaDTOs = new ArrayList<>();



        editTextContraseña.setText("saadmin");
        editTextCorreoElectronico.setText("sa@k.com");

        //linearLayoutLogin.setVisibility(View.GONE);

        loginPresenter = new LoginPresenterImpl(this);

        loginPresenter.getEmpresas();

        final Button buttonLogin = (Button) findViewById(R.id.login_button);
        buttonLogin.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickLogin();
            }
        });




    }

    private void onClickLogin(){
        usuario = editTextCorreoElectronico.getText().toString();
        contraseña = editTextContraseña.getText().toString();
        IdEmpresa = empresaDTOs.get(spinnerGaseras.getSelectedItemPosition()).getIdEmpresa();

        if(TextUtils.isEmpty(usuario) || TextUtils.isEmpty(contraseña)){
            showDialog(getResources().getString(R.string.empty_field));
        }else if(!android.util.Patterns.EMAIL_ADDRESS.matcher(usuario).matches()) {
            showDialog(getResources().getString(R.string.invalid_email));
        }else{
            try{
                Log.e("SAAAA", Utilidades.getHash(contraseña));
                Log.e("SAAAA", IdEmpresa+"");
                //showDialog( Utilidades.getHash(contraseña));
                this.contraseña = Utilidades.getHash(contraseña);

            }catch (NoSuchAlgorithmException ex){
                ex.printStackTrace();
            }
            //startActivity();
            UsuarioLoginDTO usuarioLoginDTO = new UsuarioLoginDTO();
            usuarioLoginDTO.setIdEmpresa(IdEmpresa);
            usuarioLoginDTO.setPassword(this.contraseña);
            usuarioLoginDTO.setUsuario(usuario);
            loginPresenter.doLogin(usuarioLoginDTO);
        }


    }



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

    public void startActivity(ArrayList<MenuDTO> menuDTOs){
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        intent.putExtra("lista",menuDTOs);
        startActivity(intent);
    }


    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    @Override
    public void hideProgress() {
        Log.e("error", "ocultar");
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    @Override
    public void messageError(int mensaje) {
        showDialog(getResources().getString(mensaje));
    }

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
    

    @Override
    public void onSuccessLogin(UsuarioDTO usuarioDTO) {
        if(usuarioDTO.isExito()){
            Log.w("TOKEN",usuarioDTO.getToken()+"");
            Log.w("USUARIO",usuarioDTO.getIdUsuario()+"");
            Log.w("LISTA",usuarioDTO.getListMenu().length+"");
            if(usuarioDTO.getListMenu().length==0){
                showDialog(getResources().getString(R.string.usuario_sin_permisos));
            }else{
                showDialog(getResources().getString(R.string.login_sucess));
                session.createLoginSession(contraseña,usuario,usuarioDTO.getToken(),IdEmpresa);
                ArrayList<MenuDTO> menuDTOs = new ArrayList<MenuDTO>(Arrays.asList(usuarioDTO.getListMenu()));
                startActivity(menuDTOs);
            }
        }else{
            showDialog(getResources().getString(R.string.usuario_incorrecto));
        }

    }
}
