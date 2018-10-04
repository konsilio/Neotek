package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Adapter.PuntoVentaAdapter;
import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaGasListaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaGasListaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

public class PuntoVentaGasListaActivity extends AppCompatActivity implements PuntoVentaGasListaView{
    RecyclerView RVPuntoVentaGasActivityListaGas;
    TextView TVPuntoVentaGasListaActivityNombre;
    Button BtnPuntoVetaGasListActivityOpciones,BtnPuntoVentaGasListaActivityGasListaAgregar,
            BtnPuntoVentaGasListActivityPagar;
    TableLayout TLPuntoVentaGasListaActivityConcepto;
    PuntoVentaGasListaPresenter presenter;
    ProgressDialog progressDialog;
    Session session;

    VentaDTO ventaDTO;
    PuntoVentaAdapter adapter;
    boolean  EsVentaCamioneta,EsVentaCarburacion,EsVentaPipa;
    boolean esGasLP,esCilindroGas,esCilindro;
    Tabla tabla;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_gas_lista);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa",false);
            esGasLP = extras.getBoolean("esGasLP",false);
            esCilindroGas = extras.getBoolean("esCilindroGas");
            esCilindro = extras.getBoolean("esCilindro");
        }
        BtnPuntoVetaGasListActivityOpciones = findViewById(R.id.BtnPuntoVetaGasListActivityOpciones);
        BtnPuntoVentaGasListaActivityGasListaAgregar = findViewById(R.id.
                BtnPuntoVentaGasListaActivityGasListaAgregar);
        BtnPuntoVentaGasListActivityPagar = findViewById(R.id.BtnPuntoVentaGasListActivityPagar);
        RVPuntoVentaGasActivityListaGas = findViewById(R.id.RVPuntoVentaGasActivityListaGas);
        TVPuntoVentaGasListaActivityNombre = findViewById(R.id.TVPuntoVentaGasListaActivityNombre);
        TLPuntoVentaGasListaActivityConcepto = findViewById(R.id.
                TLPuntoVentaGasListaActivityConcepto);
        BtnPuntoVetaGasListActivityOpciones.setOnClickListener(V->{
            Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            startActivity(intent);
        });

        BtnPuntoVentaGasListaActivityGasListaAgregar.setOnClickListener(v->{
            ConceptoDTO conceptoDTO = new ConceptoDTO();
            conceptoDTO.setCantidad(30);
            conceptoDTO.setConcepto("GAS LP");
            conceptoDTO.setPUnitario(10.00);
            conceptoDTO.setDescuento(0.10);
            actualizarConceptos(conceptoDTO);
        });
        session = new Session(this);
        BtnPuntoVentaGasListActivityPagar.setOnClickListener(v->{
            if(ventaDTO.getConcepto().size()>0) {
                Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                        PuntoVentaPagarActivity.class);
                intent.putExtra("ventaDTO", ventaDTO);
                startActivity(intent);
            }else{
                AlertDialog.Builder builder = new
                        AlertDialog.Builder(this);
                builder.setTitle(R.string.error_titulo);
                builder.setMessage(R.string.No_venta);
                builder.setPositiveButton(R.string.message_acept,
                        ((dialog, which) -> dialog.dismiss()));
                builder.create().show();
            }
        });
        LinearLayoutManager linearLayout = new LinearLayoutManager(
                PuntoVentaGasListaActivity.this);
        RVPuntoVentaGasActivityListaGas.setLayoutManager(linearLayout);
        RVPuntoVentaGasActivityListaGas.setHasFixedSize(true);
        presenter = new PuntoVentaGasListaPresenterImpl(this);
        //if(EsVentaCamioneta) {
            presenter.getListaCamionetaCilindros(session.getToken(),
                    esGasLP,esCilindroGas,esCilindro);
            mostrarConsepto(ventaDTO.getConcepto());
        //}
    }

    @Override
    public void mostrarConsepto(List<ConceptoDTO> list) {
        tabla = new Tabla(this,TLPuntoVentaGasListaActivityConcepto);
        if(list!=null && list.size()>0){
            tabla.Cabecera(R.array.condepto_venta);
            ArrayList<String[]> datos = new ArrayList<>();
            NumberFormat format = NumberFormat.getCurrencyInstance();
            for (ConceptoDTO concepto:list){
                datos.add(new String[]{
                        concepto.getConcepto(),
                        String.valueOf(concepto.getCantidad()),
                        format.format(concepto.getPUnitario()),
                        format.format(concepto.getDescuento()),
                        format.format(concepto.getSubtotal())
                });
            }
            tabla.agregarFila(datos);

        }else{
            tabla.Cabecera(R.array.condepto_venta);
        }
    }

    @Override
    public void onHideProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onShowProgress(int message_cargando) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
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
    public void onSuccessListExistencia(DatosPuntoVentaDTO data) {
        if(data.isExito()){
            adapter = new PuntoVentaAdapter(data.getList());
            RVPuntoVentaGasActivityListaGas.setAdapter(adapter);
        }else{
            adapter = new PuntoVentaAdapter(data.getList());
            RVPuntoVentaGasActivityListaGas.setAdapter(adapter);
        }
    }

    public void actualizarConceptos(ConceptoDTO conceptoDTO){
        double precio = conceptoDTO.getPUnitario();
        if(conceptoDTO.getDescuento()>0){
            precio = conceptoDTO.getPUnitario() - conceptoDTO.getDescuento();
        }
        precio = precio * conceptoDTO.getCantidad();
        conceptoDTO.setSubtotal(precio);
        ventaDTO.getConcepto().add(conceptoDTO);
        NumberFormat format = NumberFormat.getCurrencyInstance();
        TLPuntoVentaGasListaActivityConcepto.removeAllViews();
        //
        ArrayList<String[]> datos = new ArrayList<>();
        NumberFormat formato = NumberFormat.getCurrencyInstance();
        for (ConceptoDTO concepto:ventaDTO.getConcepto()){
            datos.add(new String[]{
                    concepto.getConcepto(),
                    String.valueOf(concepto.getCantidad()),
                    formato.format(concepto.getPUnitario()),
                    formato.format(concepto.getDescuento()),
                    formato.format(concepto.getSubtotal())
            });
        }
        tabla.Cabecera(R.array.condepto_venta);
        tabla.agregarFila(datos);
    }
}
