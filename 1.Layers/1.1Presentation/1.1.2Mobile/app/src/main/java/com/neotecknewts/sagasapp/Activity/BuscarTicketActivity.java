package com.neotecknewts.sagasapp.Activity;

import android.support.v7.app.AppCompatActivity;
import android.app.ProgressDialog;
import android.app.AlertDialog;
import android.os.Bundle;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;

import com.neotecknewts.sagasapp.Adapter.TicketsAdapter;
import com.neotecknewts.sagasapp.Model.DatosVentasDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Presenter.BuscarTicketPresenter;
import com.neotecknewts.sagasapp.Presenter.BuscarTicketPresenterImpl;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;


public class BuscarTicketActivity extends AppCompatActivity implements BuscarTicketView {

    RecyclerView RVListadoTickets;
    ProgressDialog progressDialog;
    List<VentaDTO> list;

    BuscarTicketPresenter presenter;
    Session session;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_buscar_ticket);

        list = new ArrayList<VentaDTO>();

        RVListadoTickets = findViewById(R.id.RVListadoTickets);
        LinearLayoutManager linearLayout = new LinearLayoutManager(BuscarTicketActivity.this);
        RVListadoTickets.setLayoutManager(linearLayout);
        RVListadoTickets.setHasFixedSize(true);

        presenter = new BuscarTicketPresenterImpl(this);
        session = new Session(this);

        presenter.getTickets(session.getToken());
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
    public void onSuccessList(DatosVentasDTO dtos) {
        String nombre = session.getAttribute(Session.KEY_NOMBRE)==null?"":session.getAttribute(Session.KEY_NOMBRE);
        list = dtos.getList();
        TicketsAdapter adapter = new TicketsAdapter(list, nombre);
        RVListadoTickets.setAdapter(adapter);
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) -> dialog.dismiss());
        builder.create().show();
    }
}
