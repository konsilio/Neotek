package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.KeyEvent;
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
import java.util.List;

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

            if(EsLecturaFinalCamioneta || EsLecturaInicialCamioneta) {
                cilindrosDTOS = (ArrayList<CilindrosDTO>) bundle.getSerializable("cilindrosDTOS");
            }
            if(EsRecargaCamioneta) {
                recargaDTO = (RecargaDTO) bundle.getSerializable("recargaDTO");
                cilindrosDTOS = (ArrayList<CilindrosDTO>) recargaDTO.getCilindros();
            }
        }

        RVConfiguracionCamionetasCilindros = findViewById(R.id.RVConfiguracionCamionetasCilindros);
        RVConfiguracionCamionetasCilindros.setHasFixedSize(true);

        Button btnCamposCamionetasFooterGuardar = findViewById(R.id.BtnCamposCamionetasFooterGuardar);
        TextView TVCamposCamionetasHeaderTitulo = findViewById(R.id.TVCamposCamionetasHeaderTitulo);
        TextView TVCamposCamionetasHeaderInstrucciones = findViewById(R.id.
                TVCamposCamionetasHeaderInstrucciones);
        TextView TVCamposCamionetaHader = findViewById(R.id.TVCamposCamionetaHader);
        TextView TVCamposCamionetasHeaderContabiliza = findViewById(R.id.
                TVCamposCamionetasHeaderContabiliza);

        if(EsLecturaInicialCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.toma_de_lectura)+" inicial");
            TVCamposCamionetasHeaderInstrucciones.setText(R.string.Registra_configuracion);
        }else if (EsLecturaFinalCamioneta){
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.toma_de_lectura)+" final");
            TVCamposCamionetasHeaderInstrucciones.setText(R.string.Registra_configuracion_final);
            TVCamposCamionetasHeaderContabiliza.setText(
                    lecturaCamionetaDTO.isEsEncargadoPuerta()?
                            "Contabiliza todos los cilindros llenos y vacios":
                            "Contabiliza todos los cilindros llenos"
            );
        }else if(EsRecargaCamioneta){
            String estacion = (recargaDTO.getNombreEstacionEntrada().isEmpty())? "":": "+
                    recargaDTO.getNombreEstacionEntrada();
            TVCamposCamionetasHeaderTitulo.setText(getString(R.string.recarga)+" - Camioneta"+estacion);
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
            //TextView textView;
            for (int x = 0; x < adapter.getItemCount(); x++) {
                View view = RVConfiguracionCamionetasCilindros.getChildAt(x);
                EditText editText = view.findViewById(R.id.ETConfiguracionCamionetasCantidad);
                //textView = view.findViewById(R.id.TVLecturaAlmacenActivityTitulo);
                if (!editText.getText().toString().equals("") && editText.getText()!=null) {
                    if (Integer.parseInt(editText.getText().toString()) <= 0) {
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
                    //View view = RVConfiguracionCamionetasCilindros.getChildAt(x);
                    View view = RVConfiguracionCamionetasCilindros.getLayoutManager().findViewByPosition(x);
                    EditText editText = view.findViewById(R.id.ETConfiguracionCamionetasCantidad);
                    TextView textView = view.findViewById(R.id.TVLecturaAlmacenActivityTitulo);
                    cilindrosDTO.setCantidad(Integer.parseInt(editText.getText().toString()));
                    lecturaCamionetaDTO.getCilindros().add(cilindrosDTO);
                    lecturaCamionetaDTO.getCilindroCantidad().add(cilindrosDTO.getCantidad());
                    lecturaCamionetaDTO.getIdCilindro().add(cilindrosDTO.getIdCilindro());
                }

            }else if(EsRecargaCamioneta){
                List<CilindrosDTO> cilindrosDTOS = new ArrayList<>();
                for (int x =0; x<adapter.getItemCount();x++){
                    View view = RVConfiguracionCamionetasCilindros.getLayoutManager().findViewByPosition(x);

                    EditText editText =  view.findViewById(R.id.ETConfiguracionCamionetasCantidad);
                    /*int[] datos = new int[2];
                    datos[0] = adapter.getCilindro(x).getIdCilindro();
                    datos[1] = Integer.parseInt(editText.getText().toString());*/
                    CilindrosDTO cilindro = new CilindrosDTO();
                    cilindro.setCantidad(Integer.parseInt(editText.getText().toString()));
                    cilindro.setCilindroKg(adapter.getCilindro(x).getCilindroKg());
                    cilindro.setIdCilindro(adapter.getCilindro(x).getIdCilindro());
                    cilindrosDTOS.add(cilindro);
                }
                recargaDTO.setCilindros(cilindrosDTOS);
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
            startActivity(intent);
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

    @Override
    public boolean onKeyDown(int keyCode, KeyEvent event) {
        if(keyCode == KeyEvent.KEYCODE_BACK) {
            android.support.v7.app.AlertDialog.Builder builder = new
                    android.support.v7.app.AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(R.string.title_alert_message);
            builder.setMessage(R.string.message_goback_diabled);
            builder.setNegativeButton(getString(R.string.label_no), (dialogInterface, i) ->
                    dialogInterface.dismiss());
            builder.setPositiveButton(getString(R.string.label_si), (dialogInterface, i) -> {
                dialogInterface.dismiss();
                Intent intent = new Intent(ConfiguracionCamionetaActivity.this,
                        MenuActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                startActivity(intent);
                finish();
            });
            builder.setCancelable(false);
            builder.show();
            return false;
        }
        return super.onKeyDown(keyCode, event);
    }

}
