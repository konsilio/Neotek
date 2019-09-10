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
    public EditText Litro;
    private double precioSalidaLt;
    public ExistenciasDTO existencia;
    public boolean Mostrar;
    AlertDialog.Builder builder;

    public PuntoVentaAdapter(List<ExistenciasDTO> items, boolean EsVentaCamioneta, Context context) {

        //ExistenciaDTObien
        Log.d("Item: ", items + "");
        Log.d("Existencias: ", new ExistenciasDTO()+"");
        this.items = items;
        this.EsVentaCamioneta = EsVentaCamioneta;
        this.context = context;
        this.builder = new AlertDialog.Builder(context,R.style.AlertDialog );


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

    public void MostrarAlert() {
        Log.d("PrecioSalidaDTO", precioSalidaLt + "");
        builder.setTitle("El precio es mayor al precio establecido");
        builder.setMessage("Intente con una cantidad menor");
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
        Log.d("Existenciaholder", editTextPrecioLitro.toString());

        if (precioVentaDTO != null) {
            precioSalidaLt = precioVentaDTO.getPrecioActual();
            if (EsVentaCamioneta) {
                //Existenciadto vacio
                Log.d("ExistenciaDTO", (new ExistenciasDTO().toString()));
                Log.d("Existencia adapter", existencia + "");
                editTextPrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaKg()));
            } else {
                editTextPrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));

            }
        }

        if (esVentaGas) {

            boolean isDirty;
            editTextPrecioLitro.addTextChangedListener(new TextWatcher() {
                @Override
                public void beforeTextChanged(CharSequence charSequence, int start, int count, int after) {

                    Log.d("Precio", charSequence.toString());

                }

                @Override
                public void onTextChanged(CharSequence s, int start, int before, int count) {

                }


                @Override
                public void afterTextChanged(Editable s) {

                    if (!editTextCantidad.getText().toString().isEmpty() && editTextCantidad.getText() != null) {
                        double precioPorLitro;
                        if (!editTextPrecioLitro.getText().toString().isEmpty()) {
                            precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                            PrecioLitro.setText(new DecimalFormat("#.##").format(Double.parseDouble(editTextPrecioLitro.getText().toString())));
                            if (precioPorLitro > precioSalidaLt) {
                                MostrarAlert();
                                editTextPrecioLitro.setText(precioSalidaLt+"");
                                System.out.println("Ingrese una cantidad menor");
                            }
                        } else {

                            precioPorLitro = precioVentaDTO.getPrecioSalidaKg();
                            precioPorLitro = precioPorLitro / 1.16;
                            PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                        }

                        Log.d("precioltr", precioPorLitro + "");
                        Log.d("existenciadescuento2",  existencia.getDescuento()+"");
                        //Descuento.setText(String.valueOf(existencia.getDescuento()));
                        Descuento.setText(String.valueOf(items.get(position).getDescuento()));
                        double cantidadSeleccionada = Double.parseDouble(editTextCantidad.getText().toString());
                        Log.d("Cntidadselec", cantidadSeleccionada + "");
                        double sub = precioPorLitro * cantidadSeleccionada;
                        Subtotal.setText(new DecimalFormat("#.##").format(sub));
                        double iva = sub * 0.16;
                        Iva.setText(new DecimalFormat("#.##").format(iva));
                        Total.setText(new DecimalFormat("#.##").format(sub));
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
                            if (!editTextPrecioLitro.getText().toString().isEmpty()) {
                                precioPorLitro = Double.parseDouble(editTextPrecioLitro.getText().toString());
                                PrecioLitro.setText(new DecimalFormat("#.##").format(Double.parseDouble(editTextPrecioLitro.getText().toString())));
                                Log.d("precioedit", precioPorLitro + "");
                                Log.d("precioactual", precioVentaDTO.getPrecioActual() + "");

                            } else {

                                precioPorLitro = precioVentaDTO.getPrecioSalidaLt();
                                PrecioLitro.setText(new DecimalFormat("#.##").format(precioVentaDTO.getPrecioSalidaLt()));
                            }
                            Log.d("precioedit", precioPorLitro + "");
                            Log.d("existenciadescuento", precioVentaDTO.getPrecioSalidaLt() + "");
                            System.out.println(items.get(position).getDescuento());
                            Descuento.setText(String.valueOf( items.get(position).getDescuento()));
                            double cantidadSeleccionada = Double.parseDouble(editTextCantidad.getText().toString());
                            Log.d("Cntidadselec", cantidadSeleccionada + "");
                            double sub = precioPorLitro * cantidadSeleccionada;
                            Subtotal.setText(new DecimalFormat("#.##").format(sub));
                            double iva = sub * 0.16;
                            Iva.setText(new DecimalFormat("#.##").format(iva));
                            Total.setText(new DecimalFormat("#.##").format(sub));
                            precioVentaDTO.setPrecioSalidaLt(Double.parseDouble(PrecioLitro.getText().toString()));
                            //Log.d("Precionulo",PrecioLitro+"");
                            cantidad = editTextCantidad;
                            Litro = editTextPrecioLitro;
                            existencia = items.get(position);
                            Log.d("cantidad", cantidad + "");
                            Log.d("preciolitro", precioPorLitro + "");
                            Log.d("preciosalida", precioVentaDTO + "");
                            Log.d("cantidad", editTextCantidad + "");
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


        }

    }
}
