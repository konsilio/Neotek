package com.neotecknewts.sagasapp.Adapter;

import android.annotation.SuppressLint;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
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

import com.neotecknewts.sagasapp.Activity.PuntoVentaGasListaActivity;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.Model.PrecioVentaDTO;

import java.text.DecimalFormat;
import java.util.List;

import android.os.Bundle;
import android.app.ProgressDialog;
import android.widget.Toast;

public class PuntoVentaAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private final List<ExistenciasDTO> items;
    public PrecioVentaDTO precioVentaDTO;
    private boolean EsVentaCamioneta;
    private Context context;
    public TextView Total, Descuento, Subtotal, Iva, PrecioLitro;
    public boolean esVentaGas;
    public EditText cantidad;
    public EditText descuento;
    public EditText Litro;
    private double precioSalidaLt;
    public ExistenciasDTO existencia;
    public boolean Mostrar;
    AlertDialog.Builder builder;
    private boolean flag = true;
    public EditText ETPuntoVentaGasListActivityPrecioporLitro;

    public PuntoVentaAdapter(List<ExistenciasDTO> items, boolean EsVentaCamioneta, Context context) {
        this.items = items;
        this.EsVentaCamioneta = EsVentaCamioneta;
        this.context = context;
        this.builder = new AlertDialog.Builder(context,R.style.AlertDialog );
    }

    @NonNull
    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(
                R.layout.punto_venta_item, parent, false
        );

        ExistenciasHolder holder = new ExistenciasHolder(view);
        return holder;

    }

    public void MostrarAlert(String title, String msg) {
        Log.d("PrecioSalidaDTO", precioSalidaLt + "");
        builder.setTitle(title);
        builder.setMessage(msg);
        builder.setPositiveButton(R.string.message_acept, (dialog, which) ->
                dialog.dismiss());
        builder.create().show();

        return;
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

        if (precioVentaDTO != null) {
            precioSalidaLt = precioVentaDTO.getPrecioActual();
            if (EsVentaCamioneta) {
                editTextPrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaKg()));
            } else {
                double descuento = items.get(0).getDescuento();
                editTextPrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt() - descuento));

            }
        }

        if (esVentaGas) {

            boolean isDirty;
            editTextPrecioLitro.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence charSequence, int start, int count, int after) {
                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {
                }

                @Override
                public void afterTextChanged(Editable s) {
                    if (!editTextCantidad.getText().toString().isEmpty() && editTextCantidad.getText() != null) {
                        Log.d("FerChido","editTextPrecioLitro");
                        double precioPorLitro;
                        double desc = 0;
                        if (!editTextPrecioLitro.getText().toString().isEmpty()) {
                            desc = Double.parseDouble(editTextPrecioLitro.getText().toString());
                            precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                            PrecioLitro.setText(new DecimalFormat("#.##").format(Double.parseDouble(editTextPrecioLitro.getText().toString())));
                            if (precioPorLitro > precioSalidaLt) {
                                MostrarAlert("El precio es mayor al precio establecido", "Intente con una cantidad menor");
                                desc = items.get(position).getDescuento();
                                editTextPrecioLitro.setText(precioSalidaLt+"");
                                precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                            } else if(precioPorLitro == 0) {
                                MostrarAlert("El precio debe ser mayor a 0","Intente con una cantidad mayor");
                            }
                        } else {
                            precioPorLitro = precioVentaDTO.getPrecioSalidaKg();
                            PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                        }
                        Log.d("FerChido", "ET: " + editTextPrecioLitro.getText().toString());
                        Log.d("FerChido", "TV: " + PrecioLitro.getText().toString());
                        double descuento = 0;
                        if(desc > 0) {
                            descuento = precioSalidaLt - desc;
                            Descuento.setText(new DecimalFormat("#.##").format(descuento));
                        }
                        Log.d("FerChido","descuento: " + descuento);

                        double cantidadSeleccionada = Double.parseDouble(editTextCantidad.getText().toString());
                        double sub = (precioSalidaLt - descuento ) * cantidadSeleccionada;
                        Subtotal.setText(new DecimalFormat("#.##").format(sub));
                        double iva = sub * 0.16;
                        Iva.setText(new DecimalFormat("#.##").format(iva));
                        Total.setText(new DecimalFormat("#.##").format(sub));
                        precioVentaDTO.setPrecioSalidaLt(Double.parseDouble(PrecioLitro.getText().toString()));
                        cantidad = editTextCantidad;
                        Litro = editTextPrecioLitro;
                        existencia = items.get(position);
                    }
                    if (editTextPrecioLitro.getText().toString().isEmpty() && flag == true) {
                        flag = false;
                        editTextPrecioLitro.setText("");
                        PrecioLitro.setText(new DecimalFormat("#.##").format(0));
                        Subtotal.setText(new DecimalFormat("#.##").format(0));
                        Iva.setText(new DecimalFormat("#.##").format(0));
                        Total.setText(new DecimalFormat("#.##").format(0));
                    } else {
                        flag = true;
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
                            Log.d("FerChido","editTextCantidad");
                            double precioPorLitro;
                            double desc = 0;
                            if (!editTextPrecioLitro.getText().toString().isEmpty()) {
                                desc = Double.parseDouble(editTextPrecioLitro.getText().toString());
                                precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                                PrecioLitro.setText(new DecimalFormat("#.##").format(Double.parseDouble(editTextPrecioLitro.getText().toString())));
                            } else {
                                precioPorLitro = precioVentaDTO.getPrecioSalidaLt();
                                PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                            }
                            Log.d("FerChido", "ET: " + editTextPrecioLitro.getText().toString());
                            Log.d("FerChido", "TV: " + PrecioLitro.getText().toString());
                            double descuento = 0;
                            if(desc > 0) {
                                descuento = precioSalidaLt - desc;
                                Descuento.setText(new DecimalFormat("#.##").format(descuento));
                            }

                            double cantidadSeleccionada = Double.parseDouble(editTextCantidad.getText().toString());
                            double sub = ((precioSalidaLt - descuento) * cantidadSeleccionada );
                            double subtotal =  sub / 1.16 ;
                            Subtotal.setText(new DecimalFormat("#.##").format(subtotal));
                            double iva = sub - (sub / 1.16 );
                            double total = (precioPorLitro * cantidadSeleccionada) + iva ;

                            Iva.setText(new DecimalFormat("#.##").format(iva));
                            Total.setText(new DecimalFormat("#.##").format(sub));
                            precioVentaDTO.setPrecioSalidaLt(Double.parseDouble(PrecioLitro.getText().toString()));
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
                public void beforeTextChanged(CharSequence s, int start, int count, int after) {}

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {}

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
        }

    }
}
