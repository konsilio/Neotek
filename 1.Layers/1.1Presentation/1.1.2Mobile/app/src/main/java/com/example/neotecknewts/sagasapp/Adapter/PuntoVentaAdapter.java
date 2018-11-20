package com.example.neotecknewts.sagasapp.Adapter;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.Model.PrecioVentaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class PuntoVentaAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private final List<ExistenciasDTO> items;
    public PrecioVentaDTO precioVentaDTO;
    private boolean EsVentaCamioneta;
    private Context context;
    public TextView Total,Descuento,Subtotal,Iva,PrecioLitro;
    public boolean esVentaGas;
    public EditText cantidad;
    public ExistenciasDTO existencia;

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
                this.items.get(position).getNombre().replace(",0000","Kg.")
        );
        ((ExistenciasHolder) holder).PuntoVentaGasListActivityTituloCantidad.setText(
                this.EsVentaCamioneta ? this.context.getString(R.string.cantidad):
                        this.context.getString(R.string.litros_despachados)
        );
        EditText editText = ((ExistenciasHolder) holder).ETPuntoVentaGasListActivityCantidad;
        if(esVentaGas) {
            editText.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                    if(esVentaGas) {
                        if(!editText.getText().toString().isEmpty()) {
                            PrecioLitro.setText(String.valueOf(precioVentaDTO.getPrecioSalidaLt()));
                            Descuento.setText(String.valueOf(0));
                            double sub = precioVentaDTO.getPrecioSalidaLt() *
                                    Integer.parseInt(editText.getText().toString());
                            Subtotal.setText(String.valueOf(sub));
                            double iva = sub * 0.16;
                            Iva.setText(String.valueOf(sub * 0.16));
                            Total.setText(String.valueOf(sub + iva));
                            cantidad = editText;
                            existencia = items.get(position);
                        }
                    }
                }

                @Override
                public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                    if(esVentaGas) {
                        if(!editText.getText().toString().isEmpty()) {
                            PrecioLitro.setText(String.valueOf(precioVentaDTO.getPrecioSalidaLt()));
                            Descuento.setText(String.valueOf(0));
                            double sub = precioVentaDTO.getPrecioSalidaLt() *
                                    Integer.parseInt(editText.getText().toString());
                            Subtotal.setText(String.valueOf(sub));
                            double iva = sub * 0.16;
                            Iva.setText(String.valueOf(sub * 0.16));
                            Total.setText(String.valueOf(sub + iva));
                            cantidad = editText;
                            existencia = items.get(position);
                        }
                    }
                }

                @Override
                public void afterTextChanged(Editable editable) {
                    if(esVentaGas) {
                        if(!editText.getText().toString().isEmpty()) {
                            PrecioLitro.setText(String.valueOf(precioVentaDTO.getPrecioSalidaLt()));
                            Descuento.setText(String.valueOf(0));
                            double sub = precioVentaDTO.getPrecioSalidaLt() *
                                    Integer.parseInt(editText.getText().toString());
                            Subtotal.setText(String.valueOf(sub));
                            double iva = sub * 0.16;
                            Iva.setText(String.valueOf(sub * 0.16));
                            Total.setText(String.valueOf(sub + iva));
                            cantidad = editText;
                            existencia = items.get(position);
                        }
                    }
                }
            });
        }
    }

    @Override
    public int getItemCount() {
        return this.items.size();
    }

    public ExistenciasDTO getCilindro(int position) {
        return  this.items.get(position);
    }

    public class ExistenciasHolder extends RecyclerView.ViewHolder {
        TextView PuntoVentaGasListaActivityCantidadGas,PuntoVentaGasListaActivityTipoGas,
                PuntoVentaGasListActivityTituloCantidad;
        EditText ETPuntoVentaGasListActivityCantidad;
        ExistenciasHolder(View view) {
            super(view);
            PuntoVentaGasListaActivityCantidadGas = view.findViewById(R.id.
                    PuntoVentaGasListaActivityCantidadExistencia);
            PuntoVentaGasListaActivityTipoGas = view.findViewById(R.id.
                    PuntoVentaGasListaActivityTipoGas);
            PuntoVentaGasListActivityTituloCantidad = view.findViewById(R.id.
                    PuntoVentaGasListActivityTituloCantidad);
            ETPuntoVentaGasListActivityCantidad = view.findViewById(
                    R.id.ETPuntoVentaGasListActivityCantidad);
        }
    }
}
