package com.example.neotecknewts.sagasapp.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;


import com.example.neotecknewts.sagasapp.Model.EmpresaDTO;
import com.example.neotecknewts.sagasapp.Model.OrdenCompraDTO;
import com.example.neotecknewts.sagasapp.Model.ProductoDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaOrdenesCompraDTO;
import com.example.neotecknewts.sagasapp.Presenter.VistaOrdenesCompraPresenter;
import com.example.neotecknewts.sagasapp.Presenter.VistaOrdenesCompraPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by neotecknewts on 07/08/18.
 */

public class VistaOrdenCompraActivity extends AppCompatActivity implements VistaOrdenCompraView{

    //variables de la vista
    public TextView textViewProveedor;
    public TableLayout tableLayout;
    public Spinner spinnerOrdenCompra;
    public TextView textViewFecha;
    public TextView textViewsubtotal;
    public TextView textViewIVA;
    public TextView textViewIPS;
    public TextView textViewTotal;
    ProgressDialog progressDialog;

    //clase de la sesion
    public Session session;

    //objeto de las ordenes de compra disponibles y la seleccionada para mostrar sus datos
    public OrdenCompraDTO ordenCompraDTO;
    List<OrdenCompraDTO> ordenesCompraDTO;


    public VistaOrdenesCompraPresenter vistaOrdenesCompraPresenter;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_vista_orden_compra);

        //se inicializa la session
        session = new Session(getApplicationContext());

        //se obtienen los objetos de la vista
        textViewProveedor = (TextView) findViewById(R.id.textViewProovedor);
        tableLayout = (TableLayout) findViewById(R.id.tableProductos2);
        spinnerOrdenCompra = (Spinner) findViewById(R.id.spinner_orden_compra);
        textViewFecha = (TextView) findViewById(R.id.textFecha);
        textViewsubtotal = (TextView) findViewById(R.id.textViewSubtotal);
        textViewIVA = (TextView) findViewById(R.id.textViewIva);
        textViewIPS = (TextView) findViewById(R.id.textViewIps);
        textViewTotal = (TextView) findViewById(R.id.textViewTotal);

        // se inicializa el presenter
        vistaOrdenesCompraPresenter = new VistaOrdenesCompraPresenterImpl(this);

        final String[] ordenes = {"OC1", "OC2"};

        ordenesCompraDTO = new ArrayList<>();

        //se asingna el adapter a los spinners
        spinnerOrdenCompra.setAdapter(new ArrayAdapter<String>(this, R.layout.custom_spinner, ordenes));
        textViewProveedor.setMovementMethod(new ScrollingMovementMethod());
        textViewProveedor.setText("Proveedor S.A. de C.V. \nAv. Universidad #435 Col. Lazaro Cardenas \nChilpancingo Guerrero, Mex. \n" +
                "Cp. 55472 \n \nTels. 546-56-56");

        //metodo que cuando se selcciona una orden de compra crga sus datos
        spinnerOrdenCompra.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                if (ordenesCompraDTO.size()!=0){
                    Log.w("Selected",""+position);
                    ordenCompraDTO = ordenesCompraDTO.get(spinnerOrdenCompra.getSelectedItemPosition());
                    spinnerOrdenCompra.getSelectedItemPosition();
                    fillView();
                 }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {

            }

        });

        //se llama al presenter para obtener las ordenes de compra
        vistaOrdenesCompraPresenter.getOrdenesCompra(session.getIdEmpresa(),session.getTokenWithBearer());

    }

    //metodo que muestra algun mensaje
    private void showDialog(String mensaje){
        AlertDialog.Builder builder1 = new AlertDialog.Builder(this);
        builder1.setMessage(mensaje);
        builder1.setCancelable(true);

        builder1.setNegativeButton(
                R.string.message_acept,
                new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        dialog.cancel();
                    }
                });

        AlertDialog alert11 = builder1.create();
        alert11.show();
    }

    //metodo que muestra el progreso de la obtencion de datos
    @Override
    public void showProgress(int mensaje) {
        progressDialog = ProgressDialog.show(this,getResources().getString(R.string.app_name),
                getResources().getString(mensaje), true);
    }

    //metodo que oculta el progreso
    @Override
    public void hideProgress() {
        if(progressDialog != null){
            progressDialog.dismiss();
        }
    }

    //metodo que muestra un mensaje de error
    @Override
    public void messageError(int mensaje) {
        showDialog(getResources().getString(mensaje));
    }

    //metodo que se llama cuando se obtuvieron todas las ordenes de compra y se asigna el arreglo de los nombres al adapter del spinner
    @Override
    public void onSuccessGetOrdenesCompra(RespuestaOrdenesCompraDTO respuesta) {
        Log.w("VIEW", respuesta.getOrdenesCompra().size()+"");
        this.ordenesCompraDTO = respuesta.getOrdenesCompra();
        String[] ordenes = new String[ordenesCompraDTO.size()];
        for (int i =0; i<ordenes.length; i++){
            ordenes[i]=ordenesCompraDTO.get(i).getNumOrdenCompra();
        }

        spinnerOrdenCompra.setAdapter(new ArrayAdapter<>(this, R.layout.custom_spinner, ordenes));
    }

    @Override
    public void messageError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }


    //metodo que llena la vista con los datos de la orden de compra seleccionada
    public void fillView(){
        if(ordenCompraDTO!=null) {
            textViewFecha.setText(String.valueOf(ordenCompraDTO.getFechaRequisicion().toString()));
            String proveedor = ordenCompraDTO.getProveedorNombreComercial() + "\n" +
                    ordenCompraDTO.getProveedorCalle() + " #" + ordenCompraDTO.getProveedorNumExt() + " Int." +
                    ordenCompraDTO.getProveedorNumInt() + "\n" + ordenCompraDTO.getProveedorColonia() + "\n" +
                    ordenCompraDTO.getProveedorMunicipio() + " " + ordenCompraDTO.getProveedorEstadoProvincia() + "\n C.P." +
                    ordenCompraDTO.getProveedorCodigoPostal() + "\n \nTels: " +
                    ordenCompraDTO.getProveedorTelefono1() + "," + ordenCompraDTO.getProveedorTelefono2() + " y " +
                    ordenCompraDTO.getProveedorTelefono3() + "\nCels:" +
                    ordenCompraDTO.getProveedorCelular1() + "," + ordenCompraDTO.getProveedorCelular2() + " y " +
                    ordenCompraDTO.getProveedorCelular3();
            textViewProveedor.setText(proveedor);

            tableLayout.removeAllViews();

            for (ProductoDTO producto : ordenCompraDTO.getProductos()) {
                TableRow row = new TableRow(this);
                TextView tv = new TextView(this);
                TextView tv1 = new TextView(this);
                TextView tv2 = new TextView(this);
                TextView tv3 = new TextView(this);
                TextView tv4 = new TextView(this);
                TextView tv5 = new TextView(this);
                tv.setText(producto.getProducto());
                tv1.setText(String.valueOf(producto.getCantidad()));
                tv1.setGravity(Gravity.RIGHT);
                tv1.setPadding(0, 0, 10, 0);
                tv2.setText(String.valueOf(producto.getUnidadMedida()));
                tv3.setText(String.valueOf("$" + producto.getPrecio()));
                tv3.setGravity(Gravity.RIGHT);
                tv4.setText(String.valueOf(producto.getDescuento() + "%"));
                tv4.setGravity(Gravity.CENTER);
                tv5.setText(String.valueOf("$" + producto.getImporte()));
                tv5.setGravity(Gravity.RIGHT);
                tv.setTextSize(20);
                tv1.setTextSize(20);
                tv2.setTextSize(20);
                tv3.setTextSize(20);
                tv4.setTextSize(20);
                tv5.setTextSize(20);
                tv.setTextColor(getResources().getColor(R.color.colorTextLogin));
                tv1.setTextColor(getResources().getColor(R.color.colorTextLogin));
                tv2.setTextColor(getResources().getColor(R.color.colorTextLogin));
                tv3.setTextColor(getResources().getColor(R.color.colorTextLogin));
                tv4.setTextColor(getResources().getColor(R.color.colorTextLogin));
                tv5.setTextColor(getResources().getColor(R.color.colorTextLogin));
                row.addView(tv);
                row.addView(tv1);
                row.addView(tv2);
                row.addView(tv3);
                row.addView(tv4);
                row.addView(tv5);
                tableLayout.addView(row);
            }

            textViewsubtotal.setText(String.valueOf(ordenCompraDTO.getSubtotalSinIva()));
            textViewIVA.setText(String.valueOf(ordenCompraDTO.getIva()));
            textViewIPS.setText(String.valueOf(ordenCompraDTO.getIeps()));
            textViewTotal.setText(String.valueOf(ordenCompraDTO.getTotal()));
        }
    }
}
