package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Build;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.widget.Button;
import android.widget.LinearLayout;

import com.example.neotecknewts.sagasapp.Adapter.ClientesAdapter;
import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosClientesDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.BuscarClientePresenter;
import com.example.neotecknewts.sagasapp.Presenter.BuscarClientePresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.List;

public class BuscarClienteActivity extends AppCompatActivity implements BuscarClienteView{
    RecyclerView RVBuscarClienteActivityClientes;
    Button BtnBuscarClienteActivityNo,BtnBuscarClienteActivitySi;
    List<ClienteDTO> list;
    BuscarClientePresenter presenter;
    String criterio;
    Session session;
    ProgressDialog progressDialog;
    VentaDTO ventaDTO;
    boolean EsVentaCarburacion,EsVentaCamioneta,EsVentaPipa;
    boolean esGasLP;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_buscar_cliente);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            criterio = extras.getString("criterio");
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaCamioneta=extras.getBoolean("EsVentaCamioneta",false);
            EsVentaPipa=extras.getBoolean("EsVentaPipa",EsVentaPipa);
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            esGasLP = extras.getBoolean("esGasLP",false);
        }
        RVBuscarClienteActivityClientes = findViewById(R.id.RVBuscarClienteActivityClientes);
        //BtnBuscarClienteActivityNo = findViewById(R.id.BtnBuscarClienteActivityNo);
        BtnBuscarClienteActivitySi = findViewById(R.id.BtnBuscarClienteActivitySi);
       /* BtnBuscarClienteActivityNo.setOnClickListener(V->{
            Intent intent = new Intent(BuscarClienteActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            startActivity(intent);
        });*/
        BtnBuscarClienteActivitySi.setOnClickListener(V->{
            Intent intent = new Intent(BuscarClienteActivity.this,
                    RegistroClienteActivity.class);
            ventaDTO.setSinNumero(true);
            ventaDTO.setRFC("");
            ventaDTO.setRazonSocial("");
            ventaDTO.setNombre("");
            ventaDTO.setCredito(false);
            ventaDTO.setFactura(false);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("esGasLP",esGasLP);
            startActivity(intent);
        });
        LinearLayoutManager linearLayout = new LinearLayoutManager(BuscarClienteActivity.this);
        RVBuscarClienteActivityClientes.setLayoutManager(linearLayout);
        RVBuscarClienteActivityClientes.setHasFixedSize(true);
        presenter = new BuscarClientePresenterImpl(this);
        session = new Session(this);
        presenter.getClientes(criterio,session.getToken());
        ClientesAdapter adapter = new ClientesAdapter(
                list,
                EsVentaCarburacion,
                EsVentaCamioneta,
                EsVentaPipa,
                ventaDTO
        );
        adapter.esGasLP = esGasLP;
        RVBuscarClienteActivityClientes.setAdapter(adapter);
    }

    @Override
    public void onShowPorgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onHiddeProgress() {
        if(progressDialog.isShowing() && progressDialog!=null){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onSuccessList(DatosClientesDTO dtos) {
        if(dtos!=null && dtos.getList().size()>0){
            list = dtos.getList();
            ClientesAdapter adapter = new ClientesAdapter(list,
                    EsVentaCarburacion,
                    EsVentaCamioneta,
                    EsVentaPipa,
                    ventaDTO);
            adapter.esGasLP = esGasLP;
            RVBuscarClienteActivityClientes.setAdapter(adapter);
        }
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,(dialog, which) -> dialog.dismiss());
        builder.create().show();
    }
}
