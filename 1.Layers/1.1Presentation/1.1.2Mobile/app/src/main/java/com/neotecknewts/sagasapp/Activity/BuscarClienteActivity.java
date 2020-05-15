package com.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.widget.Button;

import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Adapter.ClientesAdapter;
import com.neotecknewts.sagasapp.Model.ClienteDTO;
import com.neotecknewts.sagasapp.Model.DatosClientesDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Presenter.BuscarClientePresenter;
import com.neotecknewts.sagasapp.Presenter.BuscarClientePresenterImpl;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Session;

import java.io.IOException;
import java.util.List;

public class BuscarClienteActivity extends AppCompatActivity implements BuscarClienteView {
    RecyclerView RVBuscarClienteActivityClientes;
    Button BtnBuscarClienteActivityNo, BtnBuscarClienteActivitySi;
    List<ClienteDTO> list;
    BuscarClientePresenter presenter;
    String criterio;
    Session session;
    ProgressDialog progressDialog;
    VentaDTO ventaDTO;
    boolean EsVentaCarburacion, EsVentaCamioneta, EsVentaPipa;
    boolean esGasLP;
    int idCliente;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_buscar_cliente);
        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            criterio = extras.getString("criterio");
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion", false);
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta", false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa", EsVentaPipa);
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            esGasLP = extras.getBoolean("esGasLP", false);
            idCliente = extras.getInt("idCliente");
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
        BtnBuscarClienteActivitySi.setOnClickListener(V -> {
            Log.d("idcliente", new ExistenciasDTO()+"");
            Intent intent = new Intent(BuscarClienteActivity.this, RegistroClienteActivity.class);
            ventaDTO.setSinNumero(true);
            ventaDTO.setRFC("");
            ventaDTO.setRazonSocial("");
            ventaDTO.setNombre("");
            ventaDTO.setCredito(false);
            ventaDTO.setBonificacion(false);
            ventaDTO.setFactura(false);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("esGasLP", esGasLP);
            intent.putExtra("idCliente", idCliente);

            startActivity(intent);
        });
        LinearLayoutManager linearLayout = new LinearLayoutManager(BuscarClienteActivity.this);
        RVBuscarClienteActivityClientes.setLayoutManager(linearLayout);
        RVBuscarClienteActivityClientes.setHasFixedSize(true);
        presenter = new BuscarClientePresenterImpl(this, BuscarClienteActivity.this);
        session = new Session(this);
        String mail = session.getEmail();
        mail =  mail.substring(0, mail.indexOf('@')).replace('.', ' ');
        setTitle("Punto de venta - " + mail);

        if (isOnline()) {
            presenter.getClientes(criterio, session.getToken());
            Log.d("BuscarClienteActivity", "isOnline");
        } else {
            Log.d("BuscarClienteActivity", "isOffline");

            SAGASSql sagasSql = new SAGASSql(this);
            DatosClientesDTO datosClientesDTO = new DatosClientesDTO();
            Log.d("BuscarClienteActivity", "Size:" +sagasSql.GetClients(criterio).size());

            datosClientesDTO.setList(sagasSql.GetClients(criterio));
            this.onSuccessList(datosClientesDTO);
        }

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
        if (progressDialog.isShowing() && progressDialog != null) {
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onSuccessList(DatosClientesDTO dtos) {
        if (dtos != null && dtos.getList().size() > 0) {
            Log.d("FerChido",dtos.toString());
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
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }

    public boolean isOnline() {
        Runtime runtime = Runtime.getRuntime();
        try {
            Process ipProcess = runtime.exec("/system/bin/ping -c 1 8.8.8.8");
            int exitValue = ipProcess.waitFor();
            return (exitValue == 0);
        } catch (IOException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        return false;
    }
}
