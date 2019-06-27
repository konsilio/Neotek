package com.neotecknewts.sagasapp.Adapter;

import android.annotation.SuppressLint;
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

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.Model.PrecioVentaDTO;

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
    public EditText Litro;
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


        EditText editTextCantidad = ((ExistenciasHolder) holder).ETPuntoVentaGasListActivityCantidad;
        EditText editTextPrecioLitro = ((ExistenciasHolder) holder).ETPuntoVentaGasListActivityPrecioporLitro;

        if(precioVentaDTO != null) {
            if (EsVentaCamioneta){
                editTextPrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaKg()));
            }else {
                editTextPrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));

            }
        }

        if (esVentaGas) {
            editTextPrecioLitro.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }

                @Override
                public void afterTextChanged(Editable s) {

                    if (!editTextCantidad.getText().toString().isEmpty() && editTextCantidad.getText() != null) {
                        double precioPorLitro;
                        if(!editTextPrecioLitro.getText().toString().isEmpty() ) {
                            precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                            PrecioLitro.setText(new DecimalFormat("#.##").format(Double.parseDouble(editTextPrecioLitro.getText().toString())));
                        }
                        else {
                            precioPorLitro = precioVentaDTO.getPrecioSalidaKg();
                            precioPorLitro = precioPorLitro / 1.16 ;
                            PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                        }
                        Log.d("precioltr", precioPorLitro+"");


                        Descuento.setText(String.valueOf(0));
                        double cantidadSeleccionada = Double.parseDouble(editTextCantidad.getText().toString());
                        Log.d("Cntidadselec", cantidadSeleccionada+"");
                        double sub = precioPorLitro * cantidadSeleccionada ;
                        Subtotal.setText(new DecimalFormat("#.##").format(sub));
                        double iva = sub * 0.16;
                        Iva.setText(new DecimalFormat("#.##").format(iva));
                        Total.setText(new DecimalFormat("#.##").format(sub + iva));
                        precioVentaDTO.setPrecioSalidaLt(Double.parseDouble(PrecioLitro.getText().toString()));
                        cantidad = editTextCantidad;
                        Litro = editTextPrecioLitro;
                        existencia = items.get(position);
                    }

                }
            });
            editTextCantidad.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                }
                @Override
                public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                }
                @Override
                public void afterTextChanged(Editable editable) {
                    if (esVentaGas) {
                        if (!editTextCantidad.getText().toString().isEmpty() && editTextCantidad.getText() != null) {
                            double precioPorLitro;
                            if(!editTextPrecioLitro.getText().toString().isEmpty() ) {
                                precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                                PrecioLitro.setText(new DecimalFormat("#.##").format(Double.parseDouble(editTextPrecioLitro.getText().toString())));
                            }
                            else {
                                precioPorLitro = precioVentaDTO.getPrecioSalidaLt();
                                PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                            }
                            Log.d("precioltr", precioPorLitro+"");


                            Descuento.setText(String.valueOf(0));
                            double cantidadSeleccionada = Double.parseDouble(editTextCantidad.getText().toString());
                            Log.d("Cntidadselec", cantidadSeleccionada+"");
                            double sub = precioPorLitro * cantidadSeleccionada;
                            Subtotal.setText(new DecimalFormat("#.##").format(sub));
                            double iva = sub * 0.16;
                            Iva.setText(new DecimalFormat("#.##").format(iva));
                            Total.setText(new DecimalFormat("#.##").format(sub + iva));
                            precioVentaDTO.setPrecioSalidaLt(Double.parseDouble(PrecioLitro.getText().toString()));
                            //Log.d("Precionulo",PrecioLitro+"");
                            cantidad = editTextCantidad;
                            Litro = editTextPrecioLitro;
                            existencia = items.get(position);
                        }
                    }
                }
            });
        } else {
            editTextCantidad.setInputType(InputType.TYPE_CLASS_NUMBER);
            editTextCantidad.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {
//                    items.get(position).setCantidad(editText.getText().toString());
                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {
//                    items.get(position).setCantidad(editText.getText().toString());
                }

                @Override
                public void afterTextChanged(Editable s) {
                    items.get(position).setCantidad(editTextCantidad.getText().toString());
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

            /*ETPuntoVentaGasListActivityPrecioporLitro.addTextChangedListener(new TextWatcher() {
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
                        cantidad = ETPuntoVentaGasListActivityCantidad;
                        Log.d("precio", "preciolitro");
                    }
                }
            })*/;
        }

    }
}
