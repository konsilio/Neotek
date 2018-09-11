package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Adapter.CamionetasAdapter;
import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.LecturaCamionetaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;

public class ConfiguracionCamionetaActivity extends AppCompatActivity implements
        ConfiguracionCamionetaView{
    private RecyclerView RVConfiguracionCamionetasCilindros;
    private CamionetasAdapter adapter;
    private boolean EsLecturaInicialCamioneta,EsLecturaFinalCamioneta;
    private LecturaCamionetaDTO lecturaCamionetaDTO;

    ArrayList<String> lista_errores;
    ArrayList<CilindrosDTO> cilindrosDTOS;
    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_configuracion_camioneta);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsLecturaInicialCamioneta = (boolean)bundle.get("EsLecturaInicialCamioneta");
            EsLecturaFinalCamioneta = (boolean) bundle.get("EsLecturaFinalCamioneta");
            lecturaCamionetaDTO = (LecturaCamionetaDTO) bundle.
                    getSerializable("lecturaCamionetaDTO");
        }
        cilindrosDTOS = new ArrayList<>();
        CilindrosDTO valor = new CilindrosDTO();
        valor.setCilindroKg("20kg.");
        valor.setCantidad(4);
        valor.setIdCilindro(1);
        CilindrosDTO valor2 = new CilindrosDTO();
        valor2.setCilindroKg("30kg.");
        valor2.setCantidad(24);
        valor.setIdCilindro(2);
        CilindrosDTO valor3 = new CilindrosDTO();
        valor3.setCilindroKg("45kg.");
        valor3.setCantidad(99);
        valor.setIdCilindro(3);

        RVConfiguracionCamionetasCilindros = findViewById(R.id.RVConfiguracionCamionetasCilindros);
        RVConfiguracionCamionetasCilindros.setHasFixedSize(true);

        Button btnCamposCamionetasFooterGuardar = findViewById(R.id.BtnCamposCamionetasFooterGuardar);
        TextView TVCamposCamionetasHeaderTitulo = findViewById(R.id.TVCamposCamionetasHeaderTitulo);

        if(EsLecturaInicialCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.toma_de_lectura)+" inicial");
        }else if (EsLecturaFinalCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.toma_de_lectura)+" final");
        }

        btnCamposCamionetasFooterGuardar.setOnClickListener(v -> {
            VerificarForm();

        });

        RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this);
        RVConfiguracionCamionetasCilindros.setLayoutManager(layoutManager);

        cilindrosDTOS.add(valor);
        cilindrosDTOS.add(valor2);
        cilindrosDTOS.add(valor3);

        adapter = new CamionetasAdapter(cilindrosDTOS);
        RVConfiguracionCamionetasCilindros.setAdapter(adapter);

    }

    @Override
    public void VerificarForm() {
        boolean error = false;
        lista_errores = new ArrayList<>();
        if(EsLecturaInicialCamioneta) {
            TextView textView;
            for (int x = 0; x < adapter.getItemCount(); x++) {
                View view = RVConfiguracionCamionetasCilindros.getChildAt(x);
                EditText editText = view.findViewById(R.id.ETConfiguracionCamionetasCantidad);
                textView = view.findViewById(R.id.TVLecturaAlmacenActivityTitulo);
                if (!editText.getText().toString().equals("")) {
                    if (Integer.parseInt(editText.getText().toString()) < 0) {
                        lista_errores.add("El valor para el cilindo del renglon "+
                                String.valueOf(x+1)+" es requerido");
                        error = true;
                        break;
                    }
                } else {
                    lista_errores.add("El valor para el cilindo del renglon " +
                            String.valueOf(x+1) + " es requerido");
                    error = true;
                    break;
                }
            }
        }
        if (error) {
            DialogoError(lista_errores);
        } else {
            for (int x = 0; x < adapter.getItemCount(); x++) {
                CilindrosDTO cilindrosDTO = adapter.getCilindro(x);
                View view = RVConfiguracionCamionetasCilindros.getChildAt(x);
                EditText editText = view.findViewById(R.id.ETConfiguracionCamionetasCantidad);
                TextView textView = view.findViewById(R.id.TVLecturaAlmacenActivityTitulo);
                cilindrosDTO.setCantidad(Integer.parseInt(editText.getText().toString()));
                lecturaCamionetaDTO.getCilindros().add(cilindrosDTO);
                lecturaCamionetaDTO.getCilindroCantidad().add(cilindrosDTO.getCantidad());
                lecturaCamionetaDTO.getIdCilindro().add(cilindrosDTO.getIdCilindro());
            }
            RegistrarLectura();
        }
    }

    public void RegistrarLectura(){
        Intent intent = new Intent(ConfiguracionCamionetaActivity.this,
                EnviarDartosActivity.class);
        intent.putExtra("EsLecturaInicialCamioneta",EsLecturaInicialCamioneta);
        intent.putExtra("EsLecturaFinalCamioneta",EsLecturaFinalCamioneta);
        intent.putExtra("lecturaCamionetaDTO",lecturaCamionetaDTO);
        startActivity(intent);
    }

    public void DialogoError(ArrayList<String> mensajes){
        AlertDialog.Builder builder = new AlertDialog.Builder(
                ConfiguracionCamionetaActivity.this);
        builder.setTitle(R.string.error_titulo);
        StringBuilder mensaje = new StringBuilder(getString(R.string.mensjae_error_campos)+"\n");
        for (String men:mensajes){
            mensaje.append(men).append("\n");
        }
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
    }

    @Override
    public void getCilindros(ArrayList<CilindrosDTO> cilindrosDTOS) {

    }

}
