package com.example.neotecknewts.sagasapp.Adapter;

import android.content.Intent;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Activity.PuntoVentaGasListaActivity;
import com.example.neotecknewts.sagasapp.Activity.VentaGasActivity;
import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class ClientesAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private List<ClienteDTO> items;
    private VentaDTO ventaDTO;
    private boolean EsVentaCarburacion,
            EsVentaCamioneta,
            EsVentaPipa;
    public ClientesAdapter(List<ClienteDTO> items,boolean EsVentaCarburacion,
                           boolean EsVentaCamioneta,
                           boolean EsVentaPipa,VentaDTO ventaDTO){
        this.items = items;
        this.ventaDTO = ventaDTO;
        this.EsVentaCamioneta = EsVentaCamioneta;
        this.EsVentaPipa = EsVentaPipa;
        this.EsVentaCarburacion = EsVentaCarburacion;
    }
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(
                R.layout.cliente_item,parent,false
        );
        return new ClientesHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, final int position) {
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
        ((ClientesHolder) holder).CVEstacionesCarburacionItem.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ventaDTO.setIdCliente(
                        items.get(position).getIdCliente()
                );
                ventaDTO.setNombre(
                        items.get(position).getNombre()
                        +" "+items.get(position).getApellido_uno()
                        +" "+items.get(position).getApellido_dos()
                );
                ventaDTO.setRFC(
                        items.get(position).getRFC()
                );
                if(items.get(position).isCredito()) {
                    ventaDTO.setCredito(true);
                    ventaDTO.setTieneCredito(true);
                }else{
                    ventaDTO.setCredito(false);
                    ventaDTO.setTieneCredito(false);
                }
                if(EsVentaCamioneta) {
                    Intent intent = new Intent(view.getContext(),
                            VentaGasActivity.class);
                    intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
                    intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
                    intent.putExtra("EsVentaPipa", EsVentaPipa);
                    intent.putExtra("ventaDTO", ventaDTO);
                    view.getContext().startActivity(intent);
                }else{
                    Intent intent = new Intent(view.getContext(),
                            PuntoVentaGasListaActivity.class);
                    intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
                    intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
                    intent.putExtra("EsVentaPipa", EsVentaPipa);
                    intent.putExtra("ventaDTO", ventaDTO);
                    view.getContext().startActivity(intent);
                }
            }
        });

    }

    @Override
    public int getItemCount() {
        return this.items!=null ? this.items.size():0;
    }

    private class ClientesHolder extends RecyclerView.ViewHolder {
        TextView TVBuscarClienteActivityTelefono,TVBuscarClienteActivityNombre,
                TVBuscarClienteaCTIVITYrFC,TvBuscarClienteActivityFactura;
        CardView CVEstacionesCarburacionItem ;
        ClientesHolder(View view) {
            super(view);
            TVBuscarClienteActivityTelefono = view.findViewById(R.id.
                    TVBuscarClienteActivityTelefono);
            TVBuscarClienteActivityNombre = view.findViewById(R.id.TVBuscarClienteActivityNombre);
            TVBuscarClienteaCTIVITYrFC = view.findViewById(R.id.TVBuscarClienteaCTIVITYrFC);
            TvBuscarClienteActivityFactura = view.findViewById(R.id.TvBuscarClienteActivityFactura);
            CVEstacionesCarburacionItem = view.findViewById(R.id.CVEstacionesCarburacionItem);
        }
    }
}
