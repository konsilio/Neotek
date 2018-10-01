package com.example.neotecknewts.sagasapp.Adapter;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class ClientesAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private List<ClienteDTO> items;
    public ClientesAdapter(List<ClienteDTO> items){
        this.items = items;
    }
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(
                R.layout.cliente_item,parent,false
        );
        return new ClientesHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        ((ClientesHolder)holder).TVBuscarClienteActivityNombre.setText(
                items.get(position).getNombre()
        );
        ((ClientesHolder)holder).TVBuscarClienteaCTIVITYrFC.setText(
                items.get(position).getRFC()
        );
        ((ClientesHolder) holder).TVBuscarClienteActivityTelefono.setText(
                items.get(position).getTelefono_fijo()
        );
        ((ClientesHolder) holder).TvBuscarClienteActivityFactura.setText(
                "Si"
        );
    }

    @Override
    public int getItemCount() {
        return 0;
    }

    private class ClientesHolder extends RecyclerView.ViewHolder {
        TextView TVBuscarClienteActivityTelefono,TVBuscarClienteActivityNombre,
                TVBuscarClienteaCTIVITYrFC,TvBuscarClienteActivityFactura;

        ClientesHolder(View view) {
            super(view);
            TVBuscarClienteActivityTelefono = view.findViewById(R.id.
                    TVBuscarClienteActivityTelefono);
            TVBuscarClienteActivityNombre = view.findViewById(R.id.TVBuscarClienteActivityNombre);
            TVBuscarClienteaCTIVITYrFC = view.findViewById(R.id.TVBuscarClienteaCTIVITYrFC);
            TvBuscarClienteActivityFactura = view.findViewById(R.id.TvBuscarClienteActivityFactura);
        }
    }
}
