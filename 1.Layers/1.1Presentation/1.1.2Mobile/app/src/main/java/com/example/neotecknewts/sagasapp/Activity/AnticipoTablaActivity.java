package com.example.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoTablaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.AnticipoTablaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

public class AnticipoTablaActivity extends AppCompatActivity implements AnticipoTablaView{
    Button BtnAnticipoTablaActivityRegresar,BtnAnticipoTablaActivityHacerAnticipo;
    TableLayout TLAnticipoTablaActivityTabla,TLAnticipoTablaActivityResultadosCorteDeCaja;
    TextView TVAnticipoTablaActivityTotal,TVAnticipoTablaActivityTitulo,TVAnticipoTablaActivityP5000;
    Spinner SPAnticipoTablaActivityFechaCorte;
    TableRow TRAnticipoTablaActivityTituloAnticipo,TRAnticipoTablaActivityFormAnticipar;
    EditText ETAnticipoTablaActivityAnticipo;

    float total;
    ArrayList<String[]> elementos;
    boolean EsAnticipo,EsCorte;
    AnticiposDTO anticiposDTO;
    CorteDTO corteDTO;
    AnticipoTablaPresenter presenter;
    Session session;
    ProgressDialog progressDialog;
    SAGASSql sagasSql;
    Tabla tabla;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_anticipo_tabla);
        Bundle bundle = getIntent().getExtras();
        if(bundle!= null){
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);
            anticiposDTO = (AnticiposDTO) bundle.getSerializable("anticiposDTO");
            corteDTO = (CorteDTO) bundle.getSerializable("corteDTO");
        }
        BtnAnticipoTablaActivityRegresar = findViewById(R.id.BtnAnticipoTablaActivityRegresar);
        BtnAnticipoTablaActivityHacerAnticipo = findViewById(R.id.
                BtnAnticipoTablaActivityHacerAnticipo);
        TVAnticipoTablaActivityTotal = findViewById(R.id.TVAnticipoTablaActivityTotal);
        SPAnticipoTablaActivityFechaCorte = findViewById(R.id.SPAnticipoTablaActivityFechaCorte);
        SPAnticipoTablaActivityFechaCorte.setVisibility((EsCorte)?View.VISIBLE:View.GONE);
        TVAnticipoTablaActivityTitulo = findViewById(R.id.TVAnticipoTablaActivityTitulo);
        TVAnticipoTablaActivityP5000 = findViewById(R.id.TVAnticipoTablaActivityP5000);
        ETAnticipoTablaActivityAnticipo = findViewById(R.id.ETAnticipoTablaActivityAnticipo);
        TLAnticipoTablaActivityResultadosCorteDeCaja = findViewById(R.id.
                TLAnticipoTablaActivityResultadosCorteDeCaja);
        TLAnticipoTablaActivityResultadosCorteDeCaja.setVisibility((EsCorte)?View.VISIBLE:View.GONE);
        TRAnticipoTablaActivityTituloAnticipo = findViewById(R.id.
                TRAnticipoTablaActivityTituloAnticipo);
        TRAnticipoTablaActivityTituloAnticipo.setVisibility((EsAnticipo)?View.VISIBLE:View.GONE);
        TRAnticipoTablaActivityFormAnticipar = findViewById(R.id.
                TRAnticipoTablaActivityFormAnticipar);
        TRAnticipoTablaActivityFormAnticipar.setVisibility((EsAnticipo)? View.VISIBLE:View.GONE);
        TVAnticipoTablaActivityTitulo.setText((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        setTitle((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        BtnAnticipoTablaActivityRegresar.setOnClickListener(V->finish());
        BtnAnticipoTablaActivityHacerAnticipo.setOnClickListener(V->{
            VerificarCampos();
        });
        BtnAnticipoTablaActivityHacerAnticipo.setText((EsCorte)?getString(R.string.hacer_corte):
        getString(R.string.hacer_anticipo));
        TVAnticipoTablaActivityP5000.setVisibility((EsCorte)? View.VISIBLE:View.GONE);
        TLAnticipoTablaActivityTabla = findViewById(R.id.TLAnticipoTablaActivityTabla);
        presenter = new AnticipoTablaPresenterImpl(this);
        session = new Session(this);
        presenter.getAnticipos(session.getToken()
                ,EsAnticipo?anticiposDTO.getIdEstacion():corteDTO.getIdEstacion(),EsAnticipo);
        tabla = new Tabla(this, TLAnticipoTablaActivityTabla);
        tabla.Cabecera(R.array.header_tabla_anticipo);
        elementos = new ArrayList<>();
        SPAnticipoTablaActivityFechaCorte.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                corteDTO.setFecha(new Date(adapterView.getItemAtPosition(i).toString()));
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                //corteDTO.setFecha(null);
            }
        });

        NumberFormat format = NumberFormat.getCurrencyInstance();
       /* for(int i = 0; i < 15; i++)
        {
            elementos.add(new String[]{"201809180785236","18/09/2018",
                    format.format(i*100.00)});
            total += i*100;
        }
        tabla.agregarFila(elementos);*/
        session = new Session(this);
        sagasSql = new SAGASSql(this);
        TVAnticipoTablaActivityTotal.setText(format.format(total));
    }

    @Override
    public void VerificarCampos() {
        if(EsAnticipo){
            AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
            if(ETAnticipoTablaActivityAnticipo.getText().toString().equals("")){

                builder.setTitle(R.string.error_titulo);
                builder.setMessage("El total del anticipo es un valor requerido");
                builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
                    dialog.dismiss();
                    ETAnticipoTablaActivityAnticipo.setFocusable(true);
                }));
                builder.create().show();
            }else{
                String cantidad = ETAnticipoTablaActivityAnticipo.getText().toString();
                if(Double.parseDouble(cantidad)<=0){
                    builder.setTitle(R.string.error_titulo);
                    builder.setMessage("El total del anticipo es un positivo requerido");
                    builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
                        dialog.dismiss();
                        ETAnticipoTablaActivityAnticipo.setFocusable(true);
                    }));
                    builder.create().show();
                }else{
                    anticiposDTO.setAnticipar(Double.parseDouble(cantidad));
                    anticiposDTO.setFecha(new Date());
                    anticiposDTO.setTotal(total);
                    SimpleDateFormat format = new SimpleDateFormat("HH:mm",
                            Locale.getDefault());
                    String hour = format.format(new Date());
                    anticiposDTO.setHora(format.format(new Date()));
                    @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                            new SimpleDateFormat("ddMMyyyyhhmmssS");
                    String clave_unica = "ANT"+s.format(new Date());
                    anticiposDTO.setClaveOperacion(clave_unica);
                    anticiposDTO.setTiket(clave_unica);
                    presenter.Anticipo(anticiposDTO,sagasSql,session.getToken());
                }
            }
        }
        else{
            corteDTO.setP5000Final(0);
            corteDTO.setP5000Inicial(0);
            corteDTO.setLitrosCorte(0);
            corteDTO.setAnticipos(0);
            corteDTO.setMontoCorte(0);
            @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                    new SimpleDateFormat("ddMMyyyyhhmmssS");
            String fecha = "";/*SPAnticipoTablaActivityFechaCorte.getSelectedItem().toString();*/
            /*if (fecha.equals("")){*/
                corteDTO.setFecha(new Date());
            /*}else{*/
                //corteDTO.setFecha(new Date(fecha));
            /*}*/
            String clave_unica = "CC"+s.format(new Date());
            corteDTO.setClaveOperacion(clave_unica);
            presenter.Corte(corteDTO,sagasSql,session.getToken());
            startIntent();
        }
    }

    @Override
    public void onShowProgress(int message_cargando) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setTitle(R.string.project_id);
        progressDialog.show();
    }

    @Override
    public void HiddeProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onSuccess() {
        startIntent();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
           dialogInterface.dismiss();

        });
        builder.create().show();
    }

    @Override
    public void onSuccessAndroid() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(R.string.mensaje_exito_papeleta_android);
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
            dialogInterface.dismiss();
            startIntent();
        });
        builder.create().show();
    }

    @Override
    public void onError(Object ob) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage("");
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
            dialogInterface.dismiss();

        });
        builder.create().show();
    }

    @Override
    public void onSuccessList(RespuestaEstacionesVentaDTO data) {
        NumberFormat format = NumberFormat.getCurrencyInstance();
        if(data!=null){
            if (EsAnticipo){
                if(data.getAnticipos().size()>0){
                   for (int x=0;x<data.getAnticipos().size();x++){
                       elementos.add(new String[]{
                               data.getAnticipos().get(x).getTiket() /*"201809180785236"*/,
                               data.getAnticipos().get(x).getFecha().toString()/*"18/09/2018"*/,
                               format.format(data.getAnticipos().get(x).getTotal())
                       });
                       total += data.getAnticipos().get(x).getTotal();
                   }
                }
            }else{
                if(data.getCortes().size()>0){
                    for (int x=0;x<data.getCortes().size();x++){
                        elementos.add(new String[]{
                                data.getCortes().get(x).getTiket()/*"201809180785236"*/,
                                data.getAnticipos().get(x).getFecha().toString()/*"18/09/2018"*/,
                                format.format(data.getAnticipos().get(x).getTotal())/*format.format(i*100.00)*/
                        });
                        total += data.getCortes().get(x).getTotal();
                    }
                }
                if(data.getFechasCorte()!=null){
                    if(data.getFechasCorte().size()>0){
                        SPAnticipoTablaActivityFechaCorte.setAdapter(new ArrayAdapter<>(
                                this,
                                R.layout.custom_spinner,
                                data.getFechasCorte().toArray()
                        ));
                    }
                }
            }
            if(!elementos.isEmpty()) {
                tabla.agregarFila(elementos);
            }else{
                AlertDialog.Builder builder = new AlertDialog.Builder(this,
                        R.style.AlertDialog);
                builder.setTitle(R.string.title_alert_message);
                builder.setMessage("No se han encontrado información");
                builder.setPositiveButton(R.string.regresar, (dialogInterface, i) ->
                {dialogInterface.dismiss();finish();});
                builder.create().show();
            }
        }
    }

    private void startIntent(){
        if(EsAnticipo){
            Intent intent = new Intent(AnticipoTablaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsAnticipo",EsAnticipo);
            intent.putExtra("EsCorte",EsCorte);
            intent.putExtra("anticiposDTO",anticiposDTO);
            startActivity(intent);
        }else if(EsCorte){
            Intent intent = new Intent(AnticipoTablaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsAnticipo",EsAnticipo);
            intent.putExtra("EsCorte",EsCorte);
            intent.putExtra("corteDTO",corteDTO);
            startActivity(intent);
        }
    }
}
