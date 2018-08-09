package com.example.neotecknewts.sagasapp.Activity;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TableRow;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioDTO;
import com.example.neotecknewts.sagasapp.Model.UsuarioLoginDTO;
import com.example.neotecknewts.sagasapp.Presenter.MainPresenter;
import com.example.neotecknewts.sagasapp.Presenter.MainPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Utilidades;

import java.math.BigInteger;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class MainActivity extends AppCompatActivity implements MainView{
    private EditText editTextCorreoElectronico;
    private EditText editTextContraseña;
    private Spinner spinnerGaseras;

    List<EmpresaDTO> empresaDTOs;

    MainPresenter mainPresenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editTextContraseña = (EditText) findViewById(R.id.input_password);
        editTextCorreoElectronico = (EditText) findViewById(R.id.input_username);
        spinnerGaseras = (Spinner) findViewById(R.id.spinner_gasera);
        empresaDTOs = new ArrayList<>();

        editTextContraseña.setText("saadmin");
        editTextCorreoElectronico.setText("correo@gmail.com");

        mainPresenter = new MainPresenterImpl(this);

        mainPresenter.getEmpresas();

        final Button buttonLogin = (Button) findViewById(R.id.login_button);
        buttonLogin.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                onClickLogin();
            }
        });

    }

    private void onClickLogin(){
        String correoElectronico = editTextCorreoElectronico.getText().toString();
        String contraseña = editTextContraseña.getText().toString();
        short idEmpresa = empresaDTOs.get(spinnerGaseras.getSelectedItemPosition()).getIdEmpresa();

        if(TextUtils.isEmpty(correoElectronico) || TextUtils.isEmpty(contraseña)){
            showDialog(getResources().getString(R.string.empty_field));
        }else if(!android.util.Patterns.EMAIL_ADDRESS.matcher(correoElectronico).matches()) {
            showDialog(getResources().getString(R.string.invalid_email));
        }else{
            try{
                Log.e("SAAAA", Utilidades.getHash(contraseña));
                Log.e("SAAAA", idEmpresa+"");
                showDialog( Utilidades.getHash(contraseña));
                contraseña = Utilidades.getHash(contraseña);
            }catch (NoSuchAlgorithmException ex){
                ex.printStackTrace();
            }
            //startActivity();
            UsuarioLoginDTO usuarioLoginDTO = new UsuarioLoginDTO();
            usuarioLoginDTO.setIdEmpresa(idEmpresa);
            usuarioLoginDTO.setPassword(contraseña);
            usuarioLoginDTO.setUsuario("sa");
            mainPresenter.doLogin(usuarioLoginDTO);
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

    public void startActivity(){
        Intent intent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivity(intent);
    }


    @Override
    public void onSuccessGetEmpresa(List<EmpresaDTO> empresaDTOs) {
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
            showDialog(getResources().getString(R.string.login_sucess));
        }else{
            showDialog(getResources().getString(R.string.usuario_incorrecto));
        }

    }
}
