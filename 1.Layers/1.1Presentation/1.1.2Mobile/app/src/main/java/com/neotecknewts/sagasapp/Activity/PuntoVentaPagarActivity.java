package com.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Switch;
import android.widget.TableLayout;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.neotecknewts.sagasapp.Model.PuntoVentaAsignadoDTO;
import com.neotecknewts.sagasapp.Model.RespuestaPuntoVenta;
import com.neotecknewts.sagasapp.Model.RespuestaVentaExtraforaneaDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaPagarPresenter;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaPagarPresenterImpl;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Session;
import com.neotecknewts.sagasapp.Util.Tabla;

import org.json.JSONObject;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

public class PuntoVentaPagarActivity extends AppCompatActivity implements PuntoVentaPagarView {
    VentaDTO ventaDTO;
    TableLayout TLPuntoVentaPagarActivityConcepto;
    Button BtnPuntoVentaPagarActivityCancelar, BtnPuntoVentaPagarActivityConfirmar,
            BtnPuntoVentaPagarActivityOpciones;
    TextView TVPuntoVentaPagarActivitySubtotal, TVPuntoVentaPagarActivityIva,
            TVPuntoVentaActivityPagarTotal, TVPuntoVentaPagarActivityEfectivo;
    Switch SPuntoVentaPagarActivityFactura, SPuntoVentaActivityCredito, SPuntoVentaActivityBonificacion;
    EditText ETPuntoVentaPagarActivityEfectivo;
    Tabla tabla;
    boolean EsVentaCamioneta, EsVentaCarburacion, EsVentaPipa;
    ProgressDialog progressDialog;
    ArrayList<String[]> lista;
    PuntoVentaPagarPresenter presenter;
    Session session;
    SAGASSql sagasSql;
    PuntoVentaAsignadoDTO puntoVentaAsignadoDTO;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_pagar);
        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta", false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion", false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa", false);
        }
        ventaDTO.setVentaExtraforanea(false);
        presenter = new PuntoVentaPagarPresenterImpl(this);
        session = new Session(this);
        sagasSql = new SAGASSql(this);
        puntoVentaAsignadoDTO = new PuntoVentaAsignadoDTO();
        TLPuntoVentaPagarActivityConcepto = findViewById(R.id.TLPuntoVentaPagarActivityConcepto);
        BtnPuntoVentaPagarActivityCancelar = findViewById(R.id.BtnPuntoVentaPagarActivityCancelar);
        BtnPuntoVentaPagarActivityConfirmar = findViewById(R.id.BtnPuntoVentaPagarActivityConfirmar);
        BtnPuntoVentaPagarActivityOpciones = findViewById(R.id.BtnPuntoVentaPagarActivityOpciones);
        TVPuntoVentaPagarActivitySubtotal = findViewById(R.id.TVPuntoVentaPagarActivitySubtotal);
        TVPuntoVentaPagarActivityIva = findViewById(R.id.TVPuntoVentaPagarActivityIva);
        TVPuntoVentaActivityPagarTotal = findViewById(R.id.TVPuntoVentaActivityPagarTotal);
        SPuntoVentaPagarActivityFactura = findViewById(R.id.SPuntoVentaPagarActivityFactura);
        SPuntoVentaActivityCredito = findViewById(R.id.SPuntoVentaActivityCredito);
        SPuntoVentaActivityBonificacion = findViewById(R.id.SPuntoVentaActivityBonificacion);
        TVPuntoVentaPagarActivityEfectivo = findViewById(R.id.TVPuntoVentaPagarActivityEfectivo);
        ETPuntoVentaPagarActivityEfectivo = findViewById(R.id.ETPuntoVentaPagarActivityEfectivo);
        SPuntoVentaActivityCredito.setChecked(
                ventaDTO.isCredito()
        );
        /*SPuntoVentaPagarActivityFactura.setChecked(
                ventaDTO.isFactura()
        );*/
        SPuntoVentaPagarActivityFactura.setVisibility(View.GONE);
        ventaDTO.setFactura(true);

        SPuntoVentaActivityCredito.setOnCheckedChangeListener((buttonView, isChecked) -> {
            if (isChecked) {
                TVPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);
                ETPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);
                SPuntoVentaActivityBonificacion.setVisibility(View.GONE);

            } else {
                TVPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
                ETPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
                SPuntoVentaActivityBonificacion.setVisibility(View.VISIBLE);
            }
        });


        BtnPuntoVentaPagarActivityCancelar.setOnClickListener(v -> {
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
        });
        BtnPuntoVentaPagarActivityOpciones.setOnClickListener(v -> {
            //ventaDTO.setFactura();
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            startActivity(intent);
        });
        SPuntoVentaActivityCredito.setChecked(ventaDTO.isCredito());
        SPuntoVentaActivityBonificacion.setChecked(ventaDTO.isBonificacion());
        if (ventaDTO.isTieneCredito()) {

            if (ventaDTO.isCredito()) {
                SPuntoVentaActivityCredito.setVisibility(View.VISIBLE);
                TVPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);
                ETPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);
                SPuntoVentaActivityBonificacion.setVisibility(View.GONE);
            } else {

                TVPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
                ETPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
                SPuntoVentaActivityBonificacion.setVisibility(View.VISIBLE);

            }
            //SPuntoVentaActivityCredito.setVisibility(View.VISIBLE);
            //TVPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);
            //ETPuntoVentaPagarActivityEfectivo.setVisibility(View.GONE);

        } else {
            SPuntoVentaActivityCredito.setVisibility(View.GONE);
            ETPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
            TVPuntoVentaPagarActivityEfectivo.setVisibility(View.VISIBLE);
            SPuntoVentaActivityBonificacion.setVisibility(View.VISIBLE);
        }

        if (EsVentaPipa || EsVentaCarburacion) {
            BtnPuntoVentaPagarActivityOpciones.setVisibility(View.GONE);
        }
        BtnPuntoVentaPagarActivityConfirmar.setOnClickListener(v -> {
            ventaDTO.setFactura(SPuntoVentaPagarActivityFactura.isChecked());
            ventaDTO.setCredito(SPuntoVentaActivityCredito.isChecked());
            //ventaDTO.setBonificacion(SPuntoVentaActivityBonificacion.isChecked());
            boolean error = false;
            //Verifica si esta habilitado la venta extraforanea
            presenter.verificarVentaExtraforanea(ventaDTO.getIdCliente(), session.getToken());
            Log.d("VentaDTO", ventaDTO.toString());
            if (!SPuntoVentaActivityCredito.isChecked()) {
                if (ETPuntoVentaPagarActivityEfectivo.getText().toString().trim().length() > 0) {

                    double efectivio = Double.valueOf(ETPuntoVentaPagarActivityEfectivo
                            .getText().toString());
                    if (efectivio < ventaDTO.getTotal()) {
                        if (SPuntoVentaActivityBonificacion.isChecked()) {
                            ventaDTO.setBonificacion(false);
                            ventaDTO.setEfectivo(efectivio);
                            ventaDTO.setCambio(0);

                        } else {
                            error = true;
                            AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);

                            builder.setTitle(R.string.info);
                            builder.setMessage("El monto es menor al pago requerido");
                            builder.setPositiveButton(R.string.regresar, (dialogInterface, i) ->
                                    dialogInterface.dismiss());
                            builder.create().show();
                        }
                    } else {
                        ventaDTO.setEfectivo(efectivio);
                        ventaDTO.setCambio(ventaDTO.getEfectivo() - ventaDTO.getTotal());
                    }
                } else {
                    error = true;
                    AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
                    builder.setTitle(R.string.info);
                    builder.setMessage("Es necesario indicar el monto pagado");
                    builder.setPositiveButton(R.string.regresar, (dialogInterface, i) ->
                            dialogInterface.dismiss());
                    builder.create().show();
                }
            } else {
                if (!ventaDTO.isVentaExtraforanea()) {
                    if (ventaDTO.getLimiteCreditoCliente() < ventaDTO.getTotal()) {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this,
                                R.style.AlertDialog);
                        builder.setTitle(R.string.info);
                        builder.setMessage("No se puede realizar la venta, favor de comunicarse con el" +
                                "área de crédito y  cobranza, Por favor intente ingresar de nuevo y pagar en efectivo");
                        builder.setPositiveButton(R.string.message_acept, (dialog, which) ->
                                dialog.dismiss());
                        builder.create().show();
                        error = true;
                        Button BtnConfirmar = (Button) findViewById(R.id.BtnPuntoVentaPagarActivityConfirmar);
                        BtnConfirmar.setEnabled(false);

                        /*Switch credito = (Switch) findViewById(R.id.SPuntoVentaActivityCredito);
                        credito.setClickable(true);*/

                    }
                }
            }
            /**VENTAEXTRAFORANEA00 **/

            if (!error) {
                presenter.pagar(ventaDTO, session.getToken(), EsVentaCamioneta, EsVentaCarburacion,
                        EsVentaPipa, sagasSql);
            }
        });
        NumberFormat format = NumberFormat.getCurrencyInstance();
        lista = new ArrayList<>();
        for (ConceptoDTO conceptoDTO :
                ventaDTO.getConcepto()) {
            lista.add(new String[]{
                    conceptoDTO.getConcepto(),
                    String.valueOf(conceptoDTO.getCantidad()),
                    format.format(conceptoDTO.getPUnitario()),
                    format.format(conceptoDTO.getDescuento()),
                    format.format(conceptoDTO.getSubtotal())
            });
        }
        tabla = new Tabla(this, TLPuntoVentaPagarActivityConcepto);
        tabla.Cabecera(R.array.condepto_venta);
        tabla.agregarFila(lista);
        calcula_total(ventaDTO.getConcepto());
        presenter.puntoVentaAsignado(session.getToken());
        Log.d("Ventadto2", ventaDTO.toString());
    }

    @Override
    public void calcula_total(List<ConceptoDTO> conceptoDTOS) {
        double total = 0;
        for (ConceptoDTO conceptoDTO : conceptoDTOS) {
            Log.d("concepto1", conceptoDTO.getConcepto());
            if (EsVentaCamioneta){
                Log.d("substring", conceptoDTO.getConcepto().substring(6, 9));
                double cap = Double.parseDouble(conceptoDTO.getConcepto().substring(6, 9));
                total += (conceptoDTO.getPrecioUnitarioLt() - conceptoDTO.getDescuento()) * (conceptoDTO.getCantidad() * cap);
            } else {
                total += (conceptoDTO.getPrecioUnitarioLt() - conceptoDTO.getDescuento()) * conceptoDTO.getCantidad();
            }
        }

        NumberFormat format = NumberFormat.getCurrencyInstance();
        ventaDTO.setBonificacion(SPuntoVentaActivityBonificacion.isClickable());
        //double total = ventaDTO.getTotal();
        double iva = total - (total /1.16);
        ventaDTO.setSubtotal(total /1.16);
        ventaDTO.setIva(iva );
        ventaDTO.setTotal(total);
        TVPuntoVentaPagarActivitySubtotal.setText(format.format(total /1.16));
        TVPuntoVentaPagarActivityIva.setText(format.format(iva));
        TVPuntoVentaActivityPagarTotal.setText(format.format(total));
    }

    @Override
    public void onShowProgress(int mensaje) {
        //if(!progressDialog.isShowing()) {
        progressDialog = new ProgressDialog(this, R.style.AlertDialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setTitle(R.string.app_name);
        progressDialog.show();
        //}
    }

    @Override
    public void onHiddeProgress() {
        if (progressDialog != null && progressDialog.isShowing()) {
            progressDialog.hide();

            progressDialog.dismiss();
        }
    }

    @Override
    public void onError(String mensaje) {
        Log.d("ali", "errorInternal");
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        // builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();
    }

    @Override
    public void onError(RespuestaPuntoVenta data) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setTitle(R.string.info);
        builder.setMessage(data.getMensaje());
        builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();
    }

    @Override
    public void onSuccess(RespuestaPuntoVenta data) {
        if (data.isExito()) {
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    VerReporteActivity.class);

            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            startActivity(intent);
        } else {
            onError("Se ha generado un error desconocido, intente nuevamente");
        }
    }

    @Override
    public void onSuccessAndroid() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setTitle(R.string.app_name);
        builder.setMessage(R.string.mensaje_exito_papeleta_android);
        builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
            Intent intent = new Intent(PuntoVentaPagarActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            startActivity(intent);
        }));
        builder.create().show();
    }

    @Override
    public void onSuccessPuntoVentaAsignado(PuntoVentaAsignadoDTO data) {
        puntoVentaAsignadoDTO = data;
        if (data != null) {
            if (!data.getNombrePuntoVenta().isEmpty() || data.getNombrePuntoVenta().trim().length() > 0)
                ventaDTO.setEstacion(data.getNombrePuntoVenta());
            else
                ventaDTO.setEstacion("");
        } else {
            ventaDTO.setEstacion("");
        }
    }

    @Override
    public void onErrorPuntoVenta(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setCancelable(false);
        builder.setTitle("Error al identificar punto de venta");
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
            ventaDTO.setEstacion("");
            dialog.dismiss();
        }));
        builder.create().show();
    }

    @Override
    public void onSuccessExtraforanea(RespuestaVentaExtraforaneaDTO data) {
        if (data.isExito()) {
            ventaDTO.setVentaExtraforanea(data.isVentaExtraforanea());
        }
    }

    @Override
    public void onErrorInternalServer(JSONObject respuesta) {
        if (respuesta != null) {
            Log.d("ali", "errorInternal");
            try {
                Log.d("ali", "errorInternal");
                AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
                builder.setTitle(R.string.error_titulo);
                builder.setMessage(respuesta.getString("Mensaje"));
                builder.setCancelable(false);
                builder.setPositiveButton(R.string.message_acept, (dialog, which) ->
                        dialog.dismiss());
                builder.create().show();
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
    }
}
