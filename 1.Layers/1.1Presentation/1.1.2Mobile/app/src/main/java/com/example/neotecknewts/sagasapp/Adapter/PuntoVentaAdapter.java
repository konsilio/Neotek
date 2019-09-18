package com.example.neotecknewts.sagasapp.Adapter;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.text.Editable;
import android.text.InputType;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.Model.PrecioVentaDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;

import java.text.DecimalFormat;
import java.util.List;

public class PuntoVentaAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private final List<ExistenciasDTO> items;
    public PrecioVentaDTO precioVentaDTO;
    private boolean EsVentaCamioneta;
    private Context context;
    public TextView Total, Descuento, Subtotal, Iva, PrecioLitro;
    public boolean esVentaGas;
    public EditText cantidad;
    public EditText PrecioporLitro;
    public ExistenciasDTO existencia;
    public boolean Mostrar;

    public PuntoVentaAdapter(List<ExistenciasDTO> items, boolean EsVentaCamioneta, Context context) {
        this.items = items;
        this.EsVentaCamioneta = EsVentaCamioneta;
        this.context = context;
    }

    public EditText ETPuntoVentaGasListActivityPrecioporLitro;


    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {


        View view = LayoutInflater.from(parent.getContext()).inflate(
                R.layout.punto_venta_item, parent, false
        );
        ExistenciasHolder holder = new ExistenciasHolder(view);
        return holder;


    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder holder, @SuppressLint("RecyclerView") int position) {
        ((ExistenciasHolder) holder).PuntoVentaGasListaActivityCantidadGas.setText(
                String.valueOf(items.get(position).getExistencias())
        );
        ((ExistenciasHolder) holder).PuntoVentaGasListaActivityCantidadGas.setVisibility(
                Mostrar ? View.VISIBLE : View.GONE
        );
        ((ExistenciasHolder) holder).PuntoVentaGasListActivityExistencia.setVisibility(
                Mostrar ? View.VISIBLE : View.GONE
        );
        ((ExistenciasHolder) holder).PuntoVentaGasListaActivityTipoGas.setText(
                items.get(position).getNombre().replace(",0000", "Kg.")
        );
        ((ExistenciasHolder) holder).PuntoVentaGasListActivityTituloCantidad.setText(
                EsVentaCamioneta ? this.context.getString(R.string.cantidad) :
                        this.context.getString(R.string.litros_despachados)
        );


        EditText editText = ((ExistenciasHolder) holder).ETPuntoVentaGasListActivityCantidad;
        EditText PrecioLitro = ((ExistenciasHolder) holder).ETPuntoVentaGasListActivityPrecioporLitro;

        if (esVentaGas) {
            editText.addTextChangedListener(new TextWatcher() {

                public void operaciones() {
                    if (esVentaGas) {
                        if (!editText.getText().toString().isEmpty() && editText.getText() != null) {
                            PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                            Descuento.setText(String.valueOf(0));
                            double sub = precioVentaDTO.getPrecioSalidaLt() *
                                    Double.parseDouble(editText.getText().toString());
                            Subtotal.setText(new DecimalFormat("#.##").format(sub));
                            double iva = sub * 0.16;
                            Iva.setText(new DecimalFormat("#.##").format(sub * 0.16));
                            Total.setText(new DecimalFormat("#.##").format(sub + iva));
                            cantidad = editText;
                            PrecioporLitro = PrecioLitro;
                            existencia = items.get(position);
                        }
                    }
                }

                @Override
                public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
//                    if(esVentaGas) {
//                        if(!editText.getText().toString().isEmpty() && editText.getText()!=null) {
//
//                            Descuento.setText(
//                                    String.valueOf(0)
//                                    );
//
//                            double sub = precioVentaDTO.getPrecioSalidaLt() *
//                                    Double.parseDouble(editText.getText().toString());
//                            Subtotal.setText(new DecimalFormat("#.##").format(sub));
//                            double iva = sub * 0.16;
//                            Iva.setText(new DecimalFormat("#.##").format(sub * 0.16));
//                            Total.setText(new DecimalFormat("#.##").format(sub + iva));
//                            cantidad = editText;
//                            existencia = items.get(position);
//                        }
//                    }
                }

                @Override
                public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
//                    if(esVentaGas) {
//                        if(!editText.getText().toString().isEmpty() && editText.getText()!=null) {
//                            PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
//                            Descuento.setText(String.valueOf(0));
//                            double sub = precioVentaDTO.getPrecioSalidaLt() *
//                                    Double.parseDouble(editText.getText().toString());
//                            Subtotal.setText(new DecimalFormat("#.##").format(sub));
//                            double iva = sub * 0.16;
//                            Iva.setText(new DecimalFormat("#.##").format(sub * 0.16));
//                            Total.setText(new DecimalFormat("#.##").format(sub + iva));
//                            cantidad = editText;
//                            existencia = items.get(position);
//                        }
//                    }
                }


                @Override
                public void afterTextChanged(Editable editable) {
                    this.operaciones();

                }

            });
        } else {
            editText.setInputType(InputType.TYPE_CLASS_NUMBER);
            editText.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {
                    items.get(position).setCantidad(editText.getText().toString());
                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {
                    items.get(position).setCantidad(editText.getText().toString());
                }

                @Override
                public void afterTextChanged(Editable s) {
                    items.get(position).setCantidad(editText.getText().toString());
                }


            });
        }
    }

    @Override
    public int getItemCount() {
        return items.size();
    }

    public ExistenciasDTO getCilindro(int position) {
        return items.get(position);
    }

    public class ExistenciasHolder extends RecyclerView.ViewHolder {
        TextView PuntoVentaGasListaActivityCantidadGas, PuntoVentaGasListaActivityTipoGas,
                PuntoVentaGasListActivityTituloCantidad, PuntoVentaGasListActivityExistencia;
        public EditText ETPuntoVentaGasListActivityCantidad, ETPuntoVentaGasListActivityPrecioporLitro;

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
            PuntoVentaGasListActivityExistencia = view.findViewById(
                    R.id.PuntoVentaGasListActivityExistencia);
            ETPuntoVentaGasListActivityPrecioporLitro = view.findViewById(
                    R.id.ETPuntoVentaGasListActivityPrecioporLitro);

            ETPuntoVentaGasListActivityPrecioporLitro.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

                }

                @Override
                public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

                }

                @Override
                public void afterTextChanged(Editable editable) {

                    if (!ETPuntoVentaGasListActivityPrecioporLitro.getText().toString().isEmpty() && ETPuntoVentaGasListActivityPrecioporLitro.getText() != null) {

                        double precioPorLitro = Double.parseDouble(ETPuntoVentaGasListActivityPrecioporLitro.getText().toString());

                        double cantidadSeleccionada = Double.parseDouble(ETPuntoVentaGasListActivityCantidad.getText().toString());
                        double sub = precioPorLitro * cantidadSeleccionada;
                        double iva = sub * 0.16;

                        PrecioLitro.setText(new DecimalFormat("#.##").format(precioPorLitro));
                        Descuento.setText(String.valueOf(0));
                        Subtotal.setText(new DecimalFormat("#.##").format(sub));
                        Iva.setText(new DecimalFormat("#.##").format(sub * 0.16));
                        Total.setText(new DecimalFormat("#.##").format(sub + iva));
                        precioVentaDTO.setPrecioSalidaLt(Double.parseDouble(ETPuntoVentaGasListActivityPrecioporLitro.getText().toString()));
                        //cantidad = ETPuntoVentaGasListActivityCantidad;
                        Log.d("precio", "preciolitro");
                        if (true) {

                        }
                    }


                }
            });
        }

    }
}
