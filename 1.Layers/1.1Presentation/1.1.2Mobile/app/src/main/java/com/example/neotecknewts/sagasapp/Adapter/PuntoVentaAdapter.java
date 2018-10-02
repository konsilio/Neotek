package com.example.neotecknewts.sagasapp.Adapter;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class PuntoVentaAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    List<ExistenciasDTO> items;
    public PuntoVentaAdapter(List<ExistenciasDTO>  items){
        this.items = items;
    }
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(
                R.layout.punto_venta_item,parent,false
        );
        return new ExistenciasHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        ((ExistenciasHolder)holder).PuntoVentaGasListaActivityCantidadGas.setText(
                String.valueOf(this.items.get(position).getExistencias())
        );
        ((ExistenciasHolder)holder).PuntoVentaGasListaActivityTipoGas.setText(
                this.items.get(position).getNombre()
        );
    }

    @Override
    public int getItemCount() {
        return this.items.size();
    }

    private class ExistenciasHolder extends RecyclerView.ViewHolder {
        TextView PuntoVentaGasListaActivityCantidadGas,PuntoVentaGasListaActivityTipoGas;
        ExistenciasHolder(View view) {
            super(view);
            PuntoVentaGasListaActivityCantidadGas = view.findViewById(R.id.
                    PuntoVentaGasListaActivityCantidadExistencia);
            PuntoVentaGasListaActivityTipoGas = view.findViewById(R.id.
                    PuntoVentaGasListaActivityTipoGas);
        }
    }
}
