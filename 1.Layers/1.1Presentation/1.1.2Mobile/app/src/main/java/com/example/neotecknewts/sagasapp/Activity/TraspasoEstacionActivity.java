package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.DatosTraspasoDTO;
import com.example.neotecknewts.sagasapp.Model.TraspasoDTO;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoEstacionPresenter;
import com.example.neotecknewts.sagasapp.Presenter.TraspasoEstacionPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

public class TraspasoEstacionActivity extends AppCompatActivity implements TraspasoEstacionView {
    boolean EsTraspasoEstacionInicial,EsTraspasoEstacionFinal,EsPrimeraParteTraspaso;
    Session session;
    String[] lista_estacion_salida,lista_medidor,lista_pipa_entrada;
    TraspasoDTO traspasoDTO;
    ProgressDialog progressDialog;
    DatosTraspasoDTO datosTraspasoDTO;

    TextView TVTraspasoEstacionActivityTitulo,TVTraspasoEstacionActivityEstacionSalidaTitulo,
            TVTraspasoEstacionActivityMedidor,TVTraspasoEstacionActivityPipaDeEntrada;
    Spinner STraspasoEstacionActivityEstacionSalida,STraspasoEstacionActivityMedidor,
            STraspasoEstacionActivityPipaEntrada;
    Button BtnTraspasoEstacionActivityAceptar;
    TraspasoEstacionPresenter presenter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_traspaso_estacion);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsTraspasoEstacionInicial = bundle.getBoolean("EsTraspasoEstacionInicial",
                    false);
            EsTraspasoEstacionFinal = bundle.getBoolean("EsTraspasoEstacionFinal",
                    false);
            EsPrimeraParteTraspaso = bundle.getBoolean("EsPrimeraParteTraspaso",
                    true);
        }
        session = new Session(this);
        traspasoDTO = new TraspasoDTO();
        presenter = new TraspasoEstacionPresenterImpl(this);

        TVTraspasoEstacionActivityTitulo = findViewById(R.id.TVTraspasoEstacionActivityTitulo);
        TVTraspasoEstacionActivityEstacionSalidaTitulo = findViewById(R.id.
                 TVTraspasoEstacionActivityEstacionSalidaTitulo);
        TVTraspasoEstacionActivityMedidor = findViewById(R.id.TVTraspasoEstacionActivityMedidor);
        TVTraspasoEstacionActivityPipaDeEntrada = findViewById(R.id.
                 TVTraspasoEstacionActivityPipaDeEntrada);
        STraspasoEstacionActivityEstacionSalida = findViewById(R.id.
                 STraspasoEstacionActivityEstacionSalida);
        STraspasoEstacionActivityMedidor = findViewById(R.id.STraspasoEstacionActivityMedidor);
        STraspasoEstacionActivityPipaEntrada = findViewById(R.id.
                 STraspasoEstacionActivityPipaEntrada);
        BtnTraspasoEstacionActivityAceptar = findViewById(R.id.BtnTraspasoEstacionActivityAceptar);

        STraspasoEstacionActivityEstacionSalida.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                 if (datosTraspasoDTO != null) {
                     for (int x=0;x<datosTraspasoDTO.getEstaciones().size();x++) {
                         if(parent.getItemAtPosition(position).toString().equals(
                                 datosTraspasoDTO.getEstaciones().get(x).getNombreAlmacen()
                         )) {
                             traspasoDTO.setIdCAlmacenGasSalida(
                                     datosTraspasoDTO.getEstaciones().get(x).getIdAlmacenGas()
                             );
                             traspasoDTO.setPorcentajeSalida(
                                     datosTraspasoDTO.getEstaciones().get(x).getPorcentajeMedidor()
                             );
                             traspasoDTO.setP5000Salida(
                                     datosTraspasoDTO.getEstaciones().get(x).getCantidadP5000()
                             );
                             traspasoDTO.setP5000SalidaInicial(
                                     datosTraspasoDTO.getEstaciones().get(x).getCantidadP5000()
                             );
                             traspasoDTO.setIdTipoMedidorSalida(
                                     datosTraspasoDTO.getEstaciones().get(x).getIdTipoMedidor()
                             );
                             traspasoDTO.setPorcentajeInicial(
                                     datosTraspasoDTO.getEstaciones().get(x).getIdTipoMedidor()
                             );
                             traspasoDTO.setNombreEstacionTraspaso(
                                     datosTraspasoDTO.getEstaciones().get(x).getNombreAlmacen()
                             );
                             for (int y=0;y<datosTraspasoDTO.getMedidores().size();y++) {
                                 if(datosTraspasoDTO.getEstaciones().get(x).getIdTipoMedidor() ==
                                         datosTraspasoDTO.getMedidores().get(y).getIdTipoMedidor()
                                 ) {
                                     STraspasoEstacionActivityMedidor.setSelection(y);
                                 }
                             }
                         }
                     }

                 }
             }
             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                 traspasoDTO.setIdCAlmacenGasSalida(0);
                 traspasoDTO.setPorcentajeSalida(0);
                 traspasoDTO.setP5000Entrada(0);
                 traspasoDTO.setIdTipoMedidorSalida(0);
             }
         });

         STraspasoEstacionActivityMedidor.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                 if(datosTraspasoDTO!=null) {
                     for (int x=0;x<datosTraspasoDTO.getMedidores().size();x++) {
                      if(parent.getItemAtPosition(position).toString().equals(
                              datosTraspasoDTO.getMedidores().get(x).getNombreTipoMedidor()
                      )) {
                          traspasoDTO.setIdTipoMedidorSalida(
                                  datosTraspasoDTO.getMedidores().get(x).getIdTipoMedidor()
                          );
                          traspasoDTO.setNombreMedidor(
                                  datosTraspasoDTO.getMedidores().get(x).getNombreTipoMedidor()
                          );
                          traspasoDTO.setCantidadDeFotos(
                                  datosTraspasoDTO.getMedidores().get(x).getCantidadFotografias()
                          );
                      }
                     }
                 }
             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                traspasoDTO.setIdTipoMedidorSalida(0);
                traspasoDTO.setNombreMedidor("");
                traspasoDTO.setCantidadDeFotos(0);
             }
         });

         STraspasoEstacionActivityPipaEntrada.setOnItemSelectedListener(
                 new AdapterView.OnItemSelectedListener() {
             @Override
             public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                 if(datosTraspasoDTO!=null) {
                     for (int x = 0; x < datosTraspasoDTO.getPipas().size(); x++) {
                         traspasoDTO.setIdCAlmacenGasEntrada(
                                 datosTraspasoDTO.getPipas().get(x).getIdAlmacenGas()
                         );
                         traspasoDTO.setNombreEstacionEntrada(
                                 datosTraspasoDTO.getPipas().get(x).getNombreAlmacen()
                         );
                         traspasoDTO.setP5000EntradaInicial(
                                 datosTraspasoDTO.getPipas().get(x).getCantidadP5000()
                         );
                         traspasoDTO.setP5000Entrada(
                                 datosTraspasoDTO.getPipas().get(x).getCantidadP5000()
                         );
                     }
                 }
             }

             @Override
             public void onNothingSelected(AdapterView<?> parent) {
                 traspasoDTO.setIdCAlmacenGasEntrada(0);
                 traspasoDTO.setP5000Entrada(0);
             }
         });

         /*lista_estacion_salida = new String[]{"Estación No.1","Estación No.2"};
         lista_medidor = new String[]{"Magnatel","Rotogate"};
         lista_pipa_entrada = new String[]{"Pipa No, 1","Pipa No. 2"};*/
         /*STraspasoEstacionActivityEstacionSalida.setAdapter(new ArrayAdapter<>(
                 this,
                 R.layout.custom_spinner,
                 lista_estacion_salida
         ));
         STraspasoEstacionActivityMedidor.setAdapter(new ArrayAdapter<>(
                 this,
                 R.layout.custom_spinner,
                 lista_medidor
         ));
         STraspasoEstacionActivityPipaEntrada.setAdapter(new ArrayAdapter<>(
                 this,
                 R.layout.custom_spinner,
                 lista_estacion_salida
         ));*/
         presenter.GetList(session.getToken(),EsTraspasoEstacionFinal);

         BtnTraspasoEstacionActivityAceptar.setOnClickListener(v -> {
             ValidarForm();
         });
    }

    @Override
    public void onSuccessList(DatosTraspasoDTO dto) {
        datosTraspasoDTO = dto;
        DatosTraspasoDTO.EstacionesDTO predeterminada = null;
        int pos_predeterminda = 0;
        if(datosTraspasoDTO!=null){
            if(datosTraspasoDTO.getEstaciones().size()>0){
                for (int x=0;x<datosTraspasoDTO.getEstaciones().size();x++){
                    lista_estacion_salida[x] = datosTraspasoDTO.getEstaciones()
                            .get(x).getNombreAlmacen();
                    if((datosTraspasoDTO.getEstaciones().get(x).getIdAlmacenGas())==
                            datosTraspasoDTO.getPredeterminada()) {
                        predeterminada = datosTraspasoDTO.getEstaciones().get(x);
                        pos_predeterminda = x;
                    }
                }
                STraspasoEstacionActivityEstacionSalida.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_estacion_salida
                        )
                );
                STraspasoEstacionActivityEstacionSalida.setSelection(pos_predeterminda);
            }

            if(datosTraspasoDTO.getMedidores().size()>0){
                for (int x=0;x<datosTraspasoDTO.getMedidores().size();x++){
                    lista_medidor[x] = datosTraspasoDTO.getMedidores().get(x)
                            .getNombreTipoMedidor();
                }
                STraspasoEstacionActivityMedidor.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_medidor
                        )
                );
                if (predeterminada!=null){
                    for (int x=0;x<datosTraspasoDTO.getMedidores().size();x++){
                       if(predeterminada.getIdAlmacenGas()==
                               datosTraspasoDTO.getMedidores().get(x).getIdTipoMedidor()){
                           STraspasoEstacionActivityMedidor.setSelection(x);
                       }
                    }
                }
            }
            if(datosTraspasoDTO.getPipas().size()>0){
                for (int x=0;x<datosTraspasoDTO.getPipas().size();x++){
                    lista_pipa_entrada[x]= datosTraspasoDTO.getPipas().get(x).getNombreAlmacen();
                }
                STraspasoEstacionActivityPipaEntrada.setAdapter(
                        new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_pipa_entrada
                        )
                );
            }
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,(dialog,which)->{dialog.dismiss();});
        builder.create().show();
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onHiddeProgress() {
        if(progressDialog!=null && progressDialog.isShowing()) {
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void ValidarForm() {
        boolean error = false;
        List<String> mensajes = new ArrayList<>();
        if(STraspasoEstacionActivityEstacionSalida.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La estación de salida es un valor requerido");
        }
        if(STraspasoEstacionActivityMedidor.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El medidor de la estación de salida es un valor requerido");
        }
        if(STraspasoEstacionActivityPipaEntrada.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("La pipa de entrada es un valor requerido");
        }
        if(error){
            MostrarErrores(mensajes);
        }else{
            DialogoBack();
        }
    }

    @Override
    public void MostrarErrores(List<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje : mensajes){
            stringBuilder.append(mensaje).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    @Override
    public void DialogoBack() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.title_alert_message);
        builder.setMessage(R.string.message_continuar);
        builder.setNegativeButton(R.string.message_cancel, (dialog, which) -> dialog.dismiss());
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            Intent intent = new Intent(TraspasoEstacionActivity.this,
                    LecturaP5000Activity.class);
            intent.putExtra("EsTraspasoEstacionInicial",EsTraspasoEstacionInicial);
            intent.putExtra("EsTraspasoEstacionFinal",EsTraspasoEstacionFinal);
            intent.putExtra("EsPrimeraParteTraspaso",EsPrimeraParteTraspaso);
            intent.putExtra("traspasoDTO",traspasoDTO);
            startActivity(intent);
        }));
        builder.create().show();
    }
}
