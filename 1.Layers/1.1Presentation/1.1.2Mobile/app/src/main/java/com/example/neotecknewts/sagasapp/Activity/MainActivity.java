package com.example.neotecknewts.sagasapp.Activity;

import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.TextUtils;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Presenter.MainPresenter;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class MainActivity extends AppCompatActivity implements MainView{
    private EditText editTextCorreoElectronico;
    private EditText editTextContraseña;
    private Spinner spinnerGaseras;

    ArrayList<EmpresaDTO> empresaDTOs;

    MainPresenter mainPresenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        editTextContraseña = (EditText) findViewById(R.id.input_password);
        editTextCorreoElectronico = (EditText) findViewById(R.id.input_username);
        spinnerGaseras = (Spinner) findViewById(R.id.spinner_gasera);

        editTextContraseña.setText("contraeña");
        editTextCorreoElectronico.setText("correo@gmail.com");

        //mainPresenter = new MainPresenterImpl(this);

        //mainPresenter.getEmpresas();

        String[] gaseras = {"Gasera 1", "Gasera 2"}; /*new String[empresaDTOs.size()];
       for (int i =0; i<empresaDTOs.size(); i++){
            gaseras[i]=empresaDTOs.get(i).getNombreComercial();
        }
*/

        spinnerGaseras.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, gaseras));

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
        //short idEmpresa = empresaDTOs.get(spinnerGaseras.getSelectedItemPosition()).getIdEmpresa();

        if(TextUtils.isEmpty(correoElectronico) || TextUtils.isEmpty(contraseña)){
            showDialog(getResources().getString(R.string.empty_field));
        }else if(!android.util.Patterns.EMAIL_ADDRESS.matcher(correoElectronico).matches()) {
            showDialog(getResources().getString(R.string.invalid_email));
        }else{
            showDialog("Correcto");
            startActivity();
            //mainPresenter.doLogin();
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
        Intent intent = new Intent(getApplicationContext(), VistaOrdenCompraActivity.class);
        startActivity(intent);
    }


    @Override
    public void onSuccessGetEmpresa(ArrayList<EmpresaDTO> empresaDTOs) {
        this.empresaDTOs = empresaDTOs;
    }

    @Override
    public void onSuccessLogin() {

    }
}
