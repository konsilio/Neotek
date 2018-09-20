package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AutoconsumoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosAutoconsumoDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

public class AutoconsumoInventarioActivity extends AppCompatActivity implements
        AutoconsumoInventarioView{
    TextView TVAutoconsumoInventarioTitulo,TVAutoconsumoInventarioActivitySubtitulo,
            TVAutoconsumoInventarioActivityUnidadEntrada;
    Spinner SAutoconsumoInvetarioActivityInventario;
    Button BtnAutoconsumoInventarioActivityGuardar;

    boolean EsAutoconsumoInvetarioInicial, EsAutoConsumoInventarioFinal;
    AutoconsumoDTO autoconsumoDTO;
    DatosAutoconsumoDTO datosAutoconsumoDTO;
    Session session;
    ProgressDialog progressDialog;
    String[] list_unidad;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_autoconsumo_inventario);
        Bundle bundle = getIntent().getExtras();
        if (bundle!= null){
            EsAutoconsumoInvetarioInicial = bundle.getBoolean("EsAutoconsumoInvetarioInicial",
                    false);
            EsAutoConsumoInventarioFinal = bundle.getBoolean("EsAutoConsumoInventarioFinal",
                    false);

        }
        session = new Session(this);
        autoconsumoDTO = new AutoconsumoDTO();
        list_unidad = new String[]{"Unidad 1","Unidad 2"};

         TVAutoconsumoInventarioTitulo = findViewById(R.id.TVAutoconsumoInventarioTitulo);
         TVAutoconsumoInventarioActivitySubtitulo = findViewById(R.id.
                 TVAutoconsumoInventarioActivitySubtitulo);
         TVAutoconsumoInventarioActivityUnidadEntrada = findViewById(R.id.
                 TVAutoconsumoInventarioActivityUnidadEntrada);
         SAutoconsumoInvetarioActivityInventario = findViewById(R.id.
                 SAutoconsumoInvetarioActivityInventario);
         SAutoconsumoInvetarioActivityInventario.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                 autoconsumoDTO.setIdCAlmacenGasSalida(1);
             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                autoconsumoDTO.setIdCAlmacenGasSalida(0);
             }
         });
         BtnAutoconsumoInventarioActivityGuardar = findViewById(R.id.
                 BtnAutoconsumoInventarioActivityGuardar);
        BtnAutoconsumoInventarioActivityGuardar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                VerificarCampos();
            }
        });

    }

    @Override
    public void VerificarCampos() {
        if(SAutoconsumoInvetarioActivityInventario.getSelectedItemPosition()<0){
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle(R.string.error_titulo);
            builder.setMessage(getString(R.string.mensjae_error_campos)+
                    "\nLa unidad de entrada es un valor requerido");
            builder.setPositiveButton(R.string.message_acept, new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                }
            });
            builder.create().show();
        }else{
            Intent intent = new Intent(AutoconsumoInventarioActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("autoconsumoDTO",autoconsumoDTO);
            intent.putExtra("EsAutoconsumoInvetarioInicial",EsAutoconsumoInvetarioInicial);
            intent.putExtra("EsAutoConsumoInventarioFinal",EsAutoConsumoInventarioFinal);
            startActivity(intent);
        }
    }

    @Override
    public void onSuccessLista(DatosAutoconsumoDTO data) {

        if(data!=null){
            list_unidad = new String[2];
            datosAutoconsumoDTO = data;
        }
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setMessage(getString(R.string.message_cargando));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onHiddenProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }

    @Override
    public void onError(String mensaje) {

    }
}
