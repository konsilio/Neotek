package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.Switch;
import android.widget.TableLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaPagarPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaPagarPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

public class PuntoVentaPagarActivity extends AppCompatActivity implements PuntoVentaPagarView{
    VentaDTO ventaDTO;
    TableLayout TLPuntoVentaPagarActivityConcepto;
    Button BtnPuntoVentaPagarActivityCancelar,BtnPuntoVentaPagarActivityConfirmar,
            BtnPuntoVentaPagarActivityOpciones;
    TextView TVPuntoVentaPagarActivitySubtotal,TVPuntoVentaPagarActivityIva,
            TVPuntoVentaActivityPagarTotal;
    Switch SPuntoVentaPagarActivityFactura,SPuntoVentaActivityCredito;
    EditText ETPuntoVentaPagarActivityEfectivo;
    Tabla tabla;
    boolean  EsVentaCamioneta,EsVentaCarburacion,EsVentaPipa;
    ProgressDialog progressDialog;
    ArrayList<String[]> lista;
    PuntoVentaPagarPresenter presenter;
    Session session;
    SAGASSql sagasSql;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_pagar);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa",false);
        }
        presenter = new PuntoVentaPagarPresenterImpl(this);
        session = new Session(this);
        sagasSql = new SAGASSql(this);
        TLPuntoVentaPagarActivityConcepto = findViewById(R.id.TLPuntoVentaPagarActivityConcepto);
        BtnPuntoVentaPagarActivityCancelar = findViewById(R.id.BtnPuntoVentaPagarActivityCancelar);
        BtnPuntoVentaPagarActivityConfirmar = findViewById(R.id.BtnPuntoVentaPagarActivityConfirmar);
        BtnPuntoVentaPagarActivityOpciones = findViewById(R.id.BtnPuntoVentaPagarActivityOpciones);
        TVPuntoVentaPagarActivitySubtotal = findViewById(R.id.TVPuntoVentaPagarActivitySubtotal);
        TVPuntoVentaPagarActivityIva = findViewById(R.id.TVPuntoVentaPagarActivityIva);
        TVPuntoVentaActivityPagarTotal = findViewById(R.id.TVPuntoVentaActivityPagarTotal);
        SPuntoVentaPagarActivityFactura = findViewById(R.id.SPuntoVentaPagarActivityFactura);
        SPuntoVentaActivityCredito = findViewById(R.id.SPuntoVentaActivityCredito);
        SPuntoVentaActivityCredito.setOnCheckedChangeListener((buttonView, isChecked) -> {
            if(isChecked)
                ETPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);
            else
                ETPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
        });
        ETPuntoVentaPagarActivityEfectivo = findViewById(R.id.ETPuntoVentaPagarActivityEfectivo);

        BtnPuntoVentaPagarActivityCancelar.setOnClickListener(v->{
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
        });
        BtnPuntoVentaPagarActivityOpciones.setOnClickListener(v->{
            //ventaDTO.setFactura();
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            startActivity(intent);
        });
        BtnPuntoVentaPagarActivityConfirmar.setOnClickListener(v->{
            ventaDTO.setFactura(SPuntoVentaPagarActivityFactura.isChecked());
            ventaDTO.setCredito(SPuntoVentaActivityCredito.isChecked());
            if(!SPuntoVentaActivityCredito.isChecked()) {
                double efectivio = Double.valueOf(ETPuntoVentaPagarActivityEfectivo
                        .getText().toString());
                ventaDTO.setEfectivo(efectivio);
            }
            presenter.pagar(ventaDTO,session.getToken(),EsVentaCamioneta,EsVentaCarburacion,
                    EsVentaPipa,sagasSql);
        });
        NumberFormat format = NumberFormat.getCurrencyInstance();
        lista = new ArrayList<>();
        for (ConceptoDTO conceptoDTO:
                ventaDTO.getConcepto()) {
            lista.add(new String[]{
                    conceptoDTO.getConcepto(),
                    String.valueOf(conceptoDTO.getCantidad()),
                    format.format(conceptoDTO.getPUnitario()),
                    format.format(conceptoDTO.getDescuento()),
                    format.format(conceptoDTO.getSubtotal())
            });
        }
        tabla = new Tabla(this,TLPuntoVentaPagarActivityConcepto);
        tabla.Cabecera(R.array.condepto_venta);
        tabla.agregarFila(lista);
        calcula_total(ventaDTO.getConcepto());

    }
    @Override
    public void calcula_total(List<ConceptoDTO> conceptoDTOS){
        double subtotal = 0;
        for (ConceptoDTO conceptoDTO :conceptoDTOS){
            subtotal += conceptoDTO.getSubtotal();
        }
        NumberFormat format = NumberFormat.getCurrencyInstance();
        TVPuntoVentaPagarActivitySubtotal.setText(format.format(subtotal));
        double iva = subtotal * 0.16;
        TVPuntoVentaPagarActivityIva.setText(format.format(iva));
        double total = subtotal + iva;
        TVPuntoVentaActivityPagarTotal.setText(format.format(total));
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setTitle(R.string.app_name);
        progressDialog.show();
    }

    @Override
    public void onHiddeProgress() {
        if (progressDialog!=null && progressDialog.isShowing()) {
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();
    }

    @Override
    public void onError(RespuestaPuntoVenta data) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(data.getMensaje());
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();
    }

    @Override
    public void onSuccess(RespuestaPuntoVenta data) {
        if (data.isExito()){
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    VerReporteActivity.class);

            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            startActivity(intent);
        }else {
            onError("Se ha generado un error desconocido, intente nuevamente");
        }
    }

    @Override
    public void onSuccessAndroid() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.app_name);
        builder.setMessage(R.string.mensaje_exito_papeleta_android);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            startActivity(intent);
        }));
        builder.create().show();
    }
}
