package com.neotecknewts.sagasapp.Adapter;

import android.content.Intent;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Activity.PuntoVentaGasListaActivity;
import com.neotecknewts.sagasapp.Activity.VentaGasActivity;
import com.neotecknewts.sagasapp.Model.ClienteDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;

import java.util.List;

public class ClientesAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private List<ClienteDTO> items;
    private VentaDTO ventaDTO;
    private boolean EsVentaCarburacion, EsVentaCamioneta, EsVentaPipa;
    public boolean esGasLP;

    public ClientesAdapter(List<ClienteDTO> items, boolean EsVentaCarburacion, boolean EsVentaCamioneta, boolean EsVentaPipa, VentaDTO ventaDTO){
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
        String razon = items.get(position).getRazonSocial();
        String nombre = items.get(position).getNombre()+" "+items.get(position).getApellido_uno()
                +" "+items.get(position).getApellido_dos();
        if(razon!=null && !razon.isEmpty()){
            ((ClientesHolder)holder).TVBuscarClienteActivityNombre.setText(
                    razon
            );
        }else{
            ((ClientesHolder) holder).TVBuscarClienteActivityNombre.setText(
                    nombre
            );
        }

        ((ClientesHolder)holder).TVBuscarClienteaCTIVITYrFC.setText(
                items.get(position).getRFC()
        );
        if(!items.get(position).getTelefono_fijo().isEmpty()) {
            ((ClientesHolder) holder).TVBuscarClienteActivityTelefono.setText(
                    items.get(position).getTelefono_fijo()
            );
        }
        if(!items.get(position).getCelular().isEmpty()){
            ((ClientesHolder) holder).TVBuscarClienteActivityTelefono.setText(
                    items.get(position).getCelular()
            );
        }

        ((ClientesHolder) holder).TvBuscarClienteActivityFactura.setText(
                (items.get(position).isFactura()||items.get(position).getRazonSocial().trim().length()>0)
                        ?" Si":" No"
        );

        ((ClientesHolder) holder).CVEstacionesCarburacionItem.setOnClickListener(view -> {
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
            ventaDTO.setRazonSocial(
                    items.get(position).getRazonSocial()
            );
            ventaDTO.setSinNumero(false);
            if(items.get(position).isCredito()) {
                ventaDTO.setCredito(true);
                ventaDTO.setTieneCredito(true);
            }else{
                ventaDTO.setCredito(false);
                ventaDTO.setTieneCredito(false);
            }
            if(items.get(position).isFactura()||items.get(position).getRazonSocial().trim().length()>0){
                ventaDTO.setFactura(true);
            }else{
                ventaDTO.setFactura(false);
            }

            ventaDTO.setLimiteCreditoCliente(
              items.get(position).getLimiteCredito()
            );

            if(EsVentaCamioneta) {
                Intent intent = new Intent(view.getContext(),
                        VentaGasActivity.class);
                intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
                intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
                intent.putExtra("EsVentaPipa", EsVentaPipa);
                intent.putExtra("ventaDTO", ventaDTO);
                intent.putExtra("esGasLP",esGasLP);
                view.getContext().startActivity(intent);
            }else{
                Intent intent = new Intent(view.getContext(),
                        PuntoVentaGasListaActivity.class);
                intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
                intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
                intent.putExtra("EsVentaPipa", EsVentaPipa);
                intent.putExtra("ventaDTO", ventaDTO);
                intent.putExtra("esGasLP",esGasLP);
                view.getContext().startActivity(intent);
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
