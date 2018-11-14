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

import com.example.neotecknewts.sagasapp.Model.DatosRecargaDto;
import com.example.neotecknewts.sagasapp.Model.DatosTomaLecturaDto;
import com.example.neotecknewts.sagasapp.Model.RecargaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RecargaPipaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.RecargaPipaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;

public class RecargaPipaActivity extends AppCompatActivity implements RecargaPipaView{
    TextView TVRecargaPipaActivityTitulo,TVRecargaPipaActivityTituloAlmacen,
            TVRecargaPipaActivityTituloPipa,TVRecargaPipaActiviryTituloMedidor;
    Spinner SRecargaPipaActivityAlmacen,SRecargaPipaActivityPipa,
            SRecargaPipaActivityMedidor;
    Button BtnRecargaPipaActivityAceptar;

    boolean EsRecargaPipaInicial,EsRecargaPipaFinal;
    RecargaDTO recargaDTO;
    RecargaPipaPresenter presenter;
    Session session;
    String[] lista_almacen,lista_pipa,lista_medidor;
    ProgressDialog progressDialog;
    DatosRecargaDto datosRecargaDto;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recarga_pipa);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsRecargaPipaInicial = bundle.getBoolean("EsRecargaPipaInicial",false);
            EsRecargaPipaFinal = bundle.getBoolean("EsRecargaPipaFinal",false);

        }
        recargaDTO = new RecargaDTO();
        session = new Session(RecargaPipaActivity.this);
        presenter = new RecargaPipaPresenterImpl(this);
        TVRecargaPipaActivityTitulo = findViewById(R.id.TVRecargaPipaActivityTitulo);
        String titulo = getString(R.string.Recarga_pipa);
        titulo += (EsRecargaPipaInicial) ? " - Inicial" : " - Final";
        TVRecargaPipaActivityTitulo.setText(titulo);

        TVRecargaPipaActivityTituloAlmacen = findViewById(R.id.TVRecargaPipaActivityTituloAlmacen);
        TVRecargaPipaActivityTituloPipa = findViewById(R.id.TVRecargaPipaActivityTituloPipa);
        TVRecargaPipaActiviryTituloMedidor = findViewById(R.id.TVRecargaPipaActiviryTituloMedidor);
        SRecargaPipaActivityAlmacen = findViewById(R.id.SRecargaPipaActivityAlmacen);
        SRecargaPipaActivityPipa = findViewById(R.id.SRecargaPipaActivityPipa);
        SRecargaPipaActivityMedidor = findViewById(R.id.SRecargaPipaActivityMedidor);

        lista_almacen = new String[]{"Almacen 1","Almacen 2"};
        lista_pipa = new String[]{"Pipa No. 1","Pipa No. 2"};
        lista_medidor = new String[]{"Rotogate","Magnatel"};

        BtnRecargaPipaActivityAceptar = findViewById(R.id.BtnRecargaPipaActivityAceptar);

        presenter.getLists(session.getToken(),EsRecargaPipaFinal);

        SRecargaPipaActivityAlmacen.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(datosRecargaDto.getEstacionesDTOS().size()>0) {
                    for (int x=0;x<datosRecargaDto.getEstacionesDTOS().size();x++) {
                        if(parent.getItemAtPosition(position).toString().equals(
                                datosRecargaDto.getEstacionesDTOS().get(x).getNombreAlmacen()
                        )) {
                            recargaDTO.setIdCAlmacenGasSalida(
                                    datosRecargaDto.getEstacionesDTOS().get(x).getIdAlmacenGas()
                            );
                            recargaDTO.setP5000Salida(
                                    datosRecargaDto.getEstacionesDTOS().get(x).getCantidadP5000()
                            );
                            recargaDTO.setProcentajeSalida(
                                    datosRecargaDto.getEstacionesDTOS().get(x)
                                            .getPorcentajeMedidor()
                            );
                            recargaDTO.setNombreMedidorSalida(
                                    datosRecargaDto.getEstacionesDTOS().get(x)
                                            .getMedidor().getNombreTipoMedidor()
                            );
                            recargaDTO.setIdTipoMedidorSalida(
                                    datosRecargaDto.getEstacionesDTOS().get(x).getIdTipoMedidor()
                            );
                            recargaDTO.setCantidadFotosSalida(
                                    datosRecargaDto.getEstacionesDTOS().get(x).getMedidor()
                                            .getCantidadFotografias()
                            );
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasSalida(0);
                recargaDTO.setP5000Salida(0);
                recargaDTO.setNombreMedidorSalida("");
            }
        });

        SRecargaPipaActivityPipa.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                //recargaDTO.setIdCAlmacenGasEntrada(1);
                if(datosRecargaDto.getPipasDTOS() !=null && datosRecargaDto!= null) {
                    for (int x=0;x<datosRecargaDto.getPipasDTOS().size();x++) {
                        if(parent.getItemAtPosition(position).toString().equals(
                                datosRecargaDto.getPipasDTOS().get(x).getNombreAlmacen()
                        )) {
                            recargaDTO.setIdCAlmacenGasEntrada(
                                    datosRecargaDto.getPipasDTOS().get(x).getIdAlmacenGas()
                            );
                            recargaDTO.setP5000Entrada(
                                    datosRecargaDto.getPipasDTOS().get(x).getCantidadP5000()
                            );
                            recargaDTO.setProcentajeEntrada(
                                    datosRecargaDto.getPipasDTOS().get(x)
                                            .getPorcentajeMedidor()
                            );
                            recargaDTO.setNombreMedidorEntrada(
                                    datosRecargaDto.getPipasDTOS().get(x)
                                            .getMedidor().getNombreTipoMedidor()
                            );
                            recargaDTO.setIdTipoMedidorEntrada(
                                    datosRecargaDto.getPipasDTOS().get(x).getIdTipoMedidor()
                            );
                            recargaDTO.setCantidadFotosEntrada(
                                    datosRecargaDto.getPipasDTOS().get(x).getMedidor()
                                            .getCantidadFotografias()
                            );
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdCAlmacenGasEntrada(1);
                recargaDTO.setP5000Entrada(0);
                recargaDTO.setNombreMedidorEntrada("");
            }
        });

        SRecargaPipaActivityMedidor.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(datosRecargaDto.getMedidorDTOS().size()>0) {
                    for (int x =0; x<datosRecargaDto.getMedidorDTOS().size();x++) {
                     if(parent.getItemAtPosition(position).toString().equals(
                             datosRecargaDto.getMedidorDTOS().get(x).getNombreTipoMedidor()
                     )) {
                         recargaDTO.setIdTipoMedidorEntrada(
                                 datosRecargaDto.getMedidorDTOS().get(x).getIdTipoMedidor()
                         );
                         recargaDTO.setNombreMedidorEntrada(
                                 datosRecargaDto.getMedidorDTOS().get(x).getNombreTipoMedidor()
                         );
                         recargaDTO.setCantidadFotosEntrada(
                                 datosRecargaDto.getMedidorDTOS().get(x).getCantidadFotografias()
                         );
                     }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                recargaDTO.setIdTipoMedidorEntrada(0);
                recargaDTO.setNombreMedidorEntrada("");
                recargaDTO.setCantidadFotosEntrada(0);
            }
        });

        BtnRecargaPipaActivityAceptar.setOnClickListener(v->{
            ValidateForm();
        });

        SRecargaPipaActivityPipa.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_pipa
                )
        );
        SRecargaPipaActivityAlmacen.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_almacen
        ));
        SRecargaPipaActivityMedidor.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                lista_medidor
        ));
        if(EsRecargaPipaFinal){
            TVRecargaPipaActiviryTituloMedidor.setVisibility(View.VISIBLE);
            SRecargaPipaActivityMedidor.setVisibility(View.VISIBLE);
            SRecargaPipaActivityAlmacen.setVisibility(View.GONE);
            TVRecargaPipaActivityTituloAlmacen.setVisibility(View.GONE);
        }else{
            TVRecargaPipaActiviryTituloMedidor.setVisibility(View.GONE);
            SRecargaPipaActivityMedidor.setVisibility(View.GONE);
            SRecargaPipaActivityAlmacen.setVisibility(View.VISIBLE);
            TVRecargaPipaActivityTituloAlmacen.setVisibility(View.VISIBLE);
        }


    }

    private  void mensajeError(ArrayList<String> mensajes){
        AlertDialog.Builder builder = new AlertDialog.Builder(RecargaPipaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje: mensajes){
            stringBuilder.append(mensaje).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
    }

    @Override
    public void ValidateForm() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();
        if(SRecargaPipaActivityPipa.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("Es necesario seleccionar la pipa");
        }
        if(SRecargaPipaActivityAlmacen.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("Es necesario seleccionar el almacen");
        }
        if(error){
            mensajeError(mensajes);
        }else{
            if(EsRecargaPipaInicial) {
                Intent intent = new Intent(RecargaPipaActivity.this,
                        SubirImagenesActivity.class);
                intent.putExtra("recargaDTO", recargaDTO);
                intent.putExtra("EsRecargaPipaInicial", EsRecargaPipaInicial);
                intent.putExtra("EsRecargaPipaFinal", EsRecargaPipaFinal);
                startActivity(intent);
            }else{
                Intent intent = new Intent(RecargaPipaActivity.this,
                        CapturaPorcentajeActivity.class);
                intent.putExtra("recargaDTO", recargaDTO);
                intent.putExtra("EsRecargaPipaInicial", EsRecargaPipaInicial);
                intent.putExtra("EsRecargaPipaFinal", EsRecargaPipaFinal);
                startActivity(intent);
            }
        }
    }

    @Override
    public void onSuccessLista(DatosRecargaDto data) {
        if(data!=null){
            datosRecargaDto = data;
            lista_almacen = new String[data.getEstacionesDTOS().size()];
            lista_pipa = new String[data.getPipasDTOS().size()];
            lista_medidor = new String[data.getMedidorDTOS().size()];
            if(!data.getMedidorDTOS().isEmpty()){
                for (int x=0;x<data.getMedidorDTOS().size();x++){
                    lista_medidor[x] = data.getMedidorDTOS().get(x).getNombreTipoMedidor();
                }
                SRecargaPipaActivityMedidor.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_medidor
                ));
            }
            if(!data.getEstacionesDTOS().isEmpty()){
                for (int x=0;x<data.getEstacionesDTOS().size();x++){
                    lista_almacen[x] = data.getEstacionesDTOS().get(x).getNombreAlmacen();
                }
                SRecargaPipaActivityAlmacen.setAdapter(new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                lista_almacen
                        )
                );
            }
            if(!data.getPipasDTOS().isEmpty()){
                for (int x=0;x<data.getPipasDTOS().size();x++){
                    lista_pipa[x] = data.getPipasDTOS().get(x).getNombreAlmacen();
                }

                SRecargaPipaActivityPipa.setAdapter(new ArrayAdapter<>(
                        this,
                        R.layout.custom_spinner,
                        lista_pipa
                ));
            }
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(RecargaPipaActivity.this,
                R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create();
        builder.show();
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
    public void onHiddenProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
        }
    }
}
