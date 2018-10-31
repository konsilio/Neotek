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
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;

public class ConfiguracionCamionetaActivity extends AppCompatActivity implements
        ConfiguracionCamionetaView{
    private RecyclerView RVConfiguracionCamionetasCilindros;
    private CamionetasAdapter adapter;
    private boolean EsLecturaInicialCamioneta,EsLecturaFinalCamioneta,EsRecargaCamioneta;
    private LecturaCamionetaDTO lecturaCamionetaDTO;
    public RecargaDTO recargaDTO;

    ArrayList<String> lista_errores;
    ArrayList<CilindrosDTO> cilindrosDTOS;
    @SuppressLint("SetTextI18n")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_configuracion_camioneta);
        Bundle bundle = getIntent().getExtras();
        cilindrosDTOS = new ArrayList<>();
        if(bundle!=null){
            EsLecturaInicialCamioneta = (boolean)bundle.get("EsLecturaInicialCamioneta");
            EsLecturaFinalCamioneta = (boolean) bundle.get("EsLecturaFinalCamioneta");
            EsRecargaCamioneta = bundle.getBoolean("EsRecargaCamioneta",false);
            lecturaCamionetaDTO = (LecturaCamionetaDTO) bundle.
                    getSerializable("lecturaCamionetaDTO");
            recargaDTO = (RecargaDTO) bundle.getSerializable("recargaDTO");
            cilindrosDTOS = (ArrayList<CilindrosDTO>) bundle.getSerializable("cilindrosDTOS");
        }
        /*CilindrosDTO valor = new CilindrosDTO();
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
        valor.setIdCilindro(3);*/

        RVConfiguracionCamionetasCilindros = findViewById(R.id.RVConfiguracionCamionetasCilindros);
        RVConfiguracionCamionetasCilindros.setHasFixedSize(true);

        Button btnCamposCamionetasFooterGuardar = findViewById(R.id.BtnCamposCamionetasFooterGuardar);
        TextView TVCamposCamionetasHeaderTitulo = findViewById(R.id.TVCamposCamionetasHeaderTitulo);
        TextView TVCamposCamionetasHeaderInstrucciones = findViewById(R.id.
                TVCamposCamionetasHeaderInstrucciones);
        TextView TVCamposCamionetaHader = findViewById(R.id.TVCamposCamionetaHader);

        if(EsLecturaInicialCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.toma_de_lectura)+" inicial");
        }else if (EsLecturaFinalCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.toma_de_lectura)+" final");
        }else if(EsRecargaCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(R.string.recarga+" - Camioneta");
            TVCamposCamionetasHeaderInstrucciones.setText(R.string.registra_configuracion_recarga);
            TVCamposCamionetaHader.setVisibility(View.GONE);
        }

        btnCamposCamionetasFooterGuardar.setOnClickListener(v -> {
            VerificarForm();

        });

        RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(this);
        RVConfiguracionCamionetasCilindros.setLayoutManager(layoutManager);

        //cilindrosDTOS.add(valor);
        //cilindrosDTOS.add(valor2);
        //cilindrosDTOS.add(valor3);

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
            if(EsLecturaInicialCamioneta || EsLecturaFinalCamioneta) {
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

            }else if(EsRecargaCamioneta){
                for (int x =0; x<adapter.getItemCount();x++){
                    View view = RVConfiguracionCamionetasCilindros.getChildAt(x);
                    EditText editText = view.findViewById(R.id.ETConfiguracionCamionetasCantidad);
                    int[] datos = new int[2];
                    datos[0] = adapter.getCilindro(x).getIdCilindro();
                    datos[1] = Integer.parseInt(editText.getText().toString());
                    recargaDTO.getCilindros().add(datos);
                }
            }
            Registrar();
        }
    }

    public void Registrar(){
        if(EsLecturaInicialCamioneta || EsLecturaFinalCamioneta) {
            Intent intent = new Intent(ConfiguracionCamionetaActivity.this,
                    EnviarDartosActivity.class);
            intent.putExtra("EsLecturaInicialCamioneta", EsLecturaInicialCamioneta);
            intent.putExtra("EsLecturaFinalCamioneta", EsLecturaFinalCamioneta);
            intent.putExtra("lecturaCamionetaDTO", lecturaCamionetaDTO);
            startActivity(intent);
        }else if (EsRecargaCamioneta){
            Intent intent = new Intent(ConfiguracionCamionetaActivity.this,
                    EnviarDartosActivity.class);
            intent.putExtra("EsLecturaInicialCamioneta", EsLecturaInicialCamioneta);
            intent.putExtra("EsLecturaFinalCamioneta", EsLecturaFinalCamioneta);
            intent.putExtra("EsRecargaCamioneta",EsRecargaCamioneta);
            intent.putExtra("recargaDTO", recargaDTO);
        }
    }

    public void DialogoError(ArrayList<String> mensajes){
        AlertDialog.Builder builder = new AlertDialog.Builder(
                ConfiguracionCamionetaActivity.this,R.style.AlertDialog);
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
