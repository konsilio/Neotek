package com.example.neotecknewts.sagasapp.Adapter;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class PuntoVentaAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private List<ExistenciasDTO> items;
    private boolean EsVentaCamioneta;
    private Context context;
    public PuntoVentaAdapter(List<ExistenciasDTO>  items,boolean EsVentaCamioneta,Context context){
        this.items = items;
        this.EsVentaCamioneta = EsVentaCamioneta;
        this.context = context;
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
        ((ExistenciasHolder) holder).PuntoVentaGasListActivityTituloCantidad.setText(
                this.EsVentaCamioneta ? this.context.getString(R.string.cantidad):
                        this.context.getString(R.string.litros_despachados)
        );
    }

    @Override
    public int getItemCount() {
        return this.items.size();
    }

    private class ExistenciasHolder extends RecyclerView.ViewHolder {
        TextView PuntoVentaGasListaActivityCantidadGas,PuntoVentaGasListaActivityTipoGas,
                PuntoVentaGasListActivityTituloCantidad;
        ExistenciasHolder(View view) {
            super(view);
            PuntoVentaGasListaActivityCantidadGas = view.findViewById(R.id.
                    PuntoVentaGasListaActivityCantidadExistencia);
            PuntoVentaGasListaActivityTipoGas = view.findViewById(R.id.
                    PuntoVentaGasListaActivityTipoGas);
            PuntoVentaGasListActivityTituloCantidad = view.findViewById(R.id.
                    PuntoVentaGasListActivityTituloCantidad);
        }
    }
}
