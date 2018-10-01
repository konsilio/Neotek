package com.example.neotecknewts.sagasapp.Activity;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.widget.Button;
import android.widget.LinearLayout;

import com.example.neotecknewts.sagasapp.Adapter.ClientesAdapter;
import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
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
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_buscar_cliente);
        RVBuscarClienteActivityClientes = findViewById(R.id.RVBuscarClienteActivityClientes);
        BtnBuscarClienteActivityNo = findViewById(R.id.BtnBuscarClienteActivityNo);
        BtnBuscarClienteActivitySi = findViewById(R.id.BtnBuscarClienteActivitySi);
        BtnBuscarClienteActivityNo.setOnClickListener(V->{

        });
        BtnBuscarClienteActivitySi.setOnClickListener(V->{

        });
        LinearLayoutManager linearLayout = new LinearLayoutManager(BuscarClienteActivity.this);
        RVBuscarClienteActivityClientes.setLayoutManager(linearLayout);
        RVBuscarClienteActivityClientes.setHasFixedSize(true);
        presenter = new BuscarClientePresenterImpl(this);
        session = new Session(this);
        criterio = "";
        presenter.getClientes(criterio,session.getToken());
        ClientesAdapter adapter = new ClientesAdapter(list);
    }

    @Override
    public void onShowPorgress(int mensaje) {

    }

    @Override
    public void onHiddeProgress() {

    }

    @Override
    public void onSuccessList(List<ClienteDTO> dtos) {

    }

    @Override
    public void onError(String mensaje) {

    }
}
