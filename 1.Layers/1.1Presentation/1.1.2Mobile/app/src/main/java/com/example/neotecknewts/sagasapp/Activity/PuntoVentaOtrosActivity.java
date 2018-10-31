package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.InputFilter;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.NumberPicker;
import android.widget.Spinner;
import android.widget.TableLayout;

import com.example.neotecknewts.sagasapp.Model.CategoriaDTO;
import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;
import com.example.neotecknewts.sagasapp.Model.LineaDTO;
import com.example.neotecknewts.sagasapp.Model.ProductoDTO;
import com.example.neotecknewts.sagasapp.Model.ProductoOtrosDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaOtrosPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaOtrosPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.lang.reflect.Array;
import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

public class PuntoVentaOtrosActivity extends AppCompatActivity implements PuntoVentaOtrosView{
    Spinner SPuntoVentaOtrosCategoria,SPuntoVentaOtrosLinea,SPuntoVentaOtrosActivityProducto;
    EditText ETProductoVentaOtrosConcepto,ETPuntoVentaOtrosActivityPrecio,
            ETPuntoVentaOtrosActivityCantidad;
    Button BtnPuntoVentaOtrosActivityOpciones,BtnPuntoVentaOtrosActivityAgregar,
            BtnPuntoVentaOtrosActivityPagar;
    TableLayout TLPuntoVentaOtrosActivityConcepto;

    PuntoVentaOtrosPresenter presenter;
    ProgressDialog progressDialog;
    String[] list_categoria,list_linea,list_producto;
    DatosVentaOtrosDTO otrosDTO;
    VentaDTO ventaDTO;
    boolean EsVentaCamioneta,EsVentaCarburacion,EsVentaPipa;
    int idCategoria,idLinea,idProducto;

    Session session;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_otros);
        Bundle extras = getIntent().getExtras();
        if(extras!= null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa",false);
        }

        SPuntoVentaOtrosCategoria = findViewById(R.id.SPuntoVentaOtrosCategoria);
        SPuntoVentaOtrosLinea = findViewById(R.id.SPuntoVentaOtrosLinea);
        SPuntoVentaOtrosActivityProducto = findViewById(R.id.SPuntoVentaOtrosActivityProducto);
        ETProductoVentaOtrosConcepto = findViewById(R.id.ETProductoVentaOtrosConcepto);
        ETPuntoVentaOtrosActivityPrecio = findViewById(R.id.ETPuntoVentaOtrosActivityPrecio);
        ETPuntoVentaOtrosActivityCantidad = findViewById(R.id.ETPuntoVentaOtrosActivityCantidad);
        BtnPuntoVentaOtrosActivityOpciones = findViewById(R.id.BtnPuntoVentaOtrosActivityOpciones);
        BtnPuntoVentaOtrosActivityAgregar = findViewById(R.id.BtnPuntoVentaOtrosActivityAgregar);
        BtnPuntoVentaOtrosActivityPagar = findViewById(R.id.BtnPuntoVentaOtrosActivityPagar);
        TLPuntoVentaOtrosActivityConcepto = findViewById(R.id.TLPuntoVentaOtrosActivityConcepto);

        BtnPuntoVentaOtrosActivityOpciones.setOnClickListener(v->{
            Intent intent = new Intent(PuntoVentaOtrosActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("esGasLP",false);
            intent.putExtra("esCilindroGas",false);
            intent.putExtra("esCilindro",false);
            startActivity(intent);
        });
        BtnPuntoVentaOtrosActivityAgregar.setOnClickListener(v->{
            ConceptoDTO conceptoDTO = new ConceptoDTO();
            int cantidad = Integer.parseInt(ETPuntoVentaOtrosActivityCantidad.getText().toString());
            double punitario = Double.parseDouble(ETPuntoVentaOtrosActivityPrecio.getText().toString());

            conceptoDTO.setCantidad(cantidad);
            conceptoDTO.setSubtotal(cantidad*punitario);
            conceptoDTO.setPUnitario(punitario);
            conceptoDTO.setDescuento(0);
            conceptoDTO.setIdTipoGas(0);
            conceptoDTO.setConcepto(ETProductoVentaOtrosConcepto.getText().toString());
            conceptoDTO.setIdCategoria(idCategoria);
            conceptoDTO.setIdLinea(idLinea);
            conceptoDTO.setIdProducto(idProducto);


            ventaDTO.getConcepto().add(conceptoDTO);
            TLPuntoVentaOtrosActivityConcepto.removeAllViews();
            mostrarConcepto(ventaDTO.getConcepto());
        });
        BtnPuntoVentaOtrosActivityPagar.setOnClickListener(v->{
            if(ventaDTO.getConcepto().size()>0) {
                Intent intent = new Intent(PuntoVentaOtrosActivity.this,
                        PuntoVentaPagarActivity.class);
                intent.putExtra("ventaDTO", ventaDTO);
                intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
                intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
                intent.putExtra("EsVentaPipa",EsVentaPipa);
                startActivity(intent);
            }else{
                AlertDialog.Builder builder = new AlertDialog.Builder(this,
                        R.style.AlertDialog);
                builder.setTitle(R.string.error_titulo);
                builder.setMessage(R.string.No_venta);
                builder.setPositiveButton(R.string.message_acept,
                        ((dialog, which) -> dialog.dismiss()));
                builder.create().show();
            }
        });

        session = new Session(this);
        presenter = new PuntoVentaOtrosPresenterImpl(this);

        list_categoria = new String[]{"Categoria 1","Otro"};
        list_linea = new String[]{"Linea 1","Otro"};
        list_producto = new String[]{"Lámparas","Otro"};

        SPuntoVentaOtrosCategoria.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_categoria
        ));
        SPuntoVentaOtrosLinea.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_linea
        ));
        SPuntoVentaOtrosActivityProducto.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                list_producto
        ));
        SPuntoVentaOtrosCategoria.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                    if(adapterView.getItemAtPosition(i).toString().equals("Otro"))
                        idCategoria = -1;
                    if(otrosDTO!= null && otrosDTO.getCategorias()!=null){
                        for(CategoriaDTO categoriaDTO : otrosDTO.getCategorias()){
                            if(categoriaDTO.getCategoria().equals(
                                    adapterView.getItemAtPosition(i).toString()
                            )){
                                idCategoria = categoriaDTO.getIdCategoria();
                            }
                        }
                    }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                idCategoria = 0;
            }
        });
        SPuntoVentaOtrosLinea.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                if(adapterView.getItemAtPosition(i).toString().equals("Otro"))
                    idLinea = -1;
                if(otrosDTO!= null && otrosDTO.getLineas()!=null){
                    for(LineaDTO lineaDTO : otrosDTO.getLineas()){
                        if(lineaDTO.getLinea().equals(
                                adapterView.getItemAtPosition(i).toString()
                        )){
                            idCategoria = lineaDTO.getIdLinea();
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                idLinea = 0;
            }
        });
        SPuntoVentaOtrosActivityProducto.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                if(adapterView.getItemAtPosition(i).toString().equals("Otro"))
                    idProducto = -1;
                if(otrosDTO!= null && otrosDTO.getProducto()!=null){
                    for(ProductoOtrosDTO productoDTO : otrosDTO.getProducto()){
                        if(productoDTO.getProducto().equals(
                                adapterView.getItemAtPosition(i).toString()
                        )){
                            idCategoria = productoDTO.getIdProducto();
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                idCategoria = 0;
            }
        });
        presenter.getList(session.getToken());
        mostrarConcepto(ventaDTO.getConcepto());

    }

    @Override
    public void onSuccess(DatosVentaOtrosDTO dtos) {
        otrosDTO = dtos;
        if(dtos!=null){
            list_categoria = new String[dtos.getCategorias().size()+1];
            list_linea = new String[dtos.getLineas().size()+1];
            list_producto = new String[dtos.getProducto().size()+1];

            for (int x =0 ;x<dtos.getCategorias().size();x++) {
                list_categoria[x] = dtos.getCategorias().get(x).getCategoria();
            }
            for (int x =0 ;x<dtos.getLineas().size();x++) {
                list_linea[x] = dtos.getLineas().get(x).getLinea();
            }
            for (int x =0 ;x<dtos.getProducto().size();x++) {
                list_producto[x] = dtos.getProducto().get(x).getProducto();
            }
            list_categoria[dtos.getCategorias().size()+1] = "Otros";
            list_linea[dtos.getLineas().size()+1] = "Otros";
            list_producto[dtos.getProducto().size()+1]= "Otros";

            SPuntoVentaOtrosCategoria.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    list_categoria
            ));
            SPuntoVentaOtrosLinea.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    list_categoria
            ));
            SPuntoVentaOtrosActivityProducto.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    list_producto
            ));
        }else{
            AlertDialog.Builder builder = new AlertDialog.Builder(this,
                    R.style.AlertDialog);
            builder.setTitle(R.string.error_titulo);
            builder.setMessage("Ha surgido unn error desconocido");
            builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
            builder.create().show();
        }
    }

    @Override
    public void onError(DatosVentaOtrosDTO dtos) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(dtos.getMensajesError());
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create().show();
    }

    @Override
    public void onShowProgress(int mensaje) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setTitle(R.string.app_name);
        progressDialog.setMessage(getString(mensaje));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onHiddenProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void mostrarConcepto(List<ConceptoDTO> list) {
        Tabla tabla = new Tabla(this,TLPuntoVentaOtrosActivityConcepto);
        if(list!=null && list.size()>0){
            tabla.Cabecera(R.array.condepto_venta);
            ArrayList<String[]> datos = new ArrayList<>();
            NumberFormat format = NumberFormat.getCurrencyInstance();
            for (ConceptoDTO concepto:list){
                datos.add(new String[]{
                        concepto.getConcepto(),
                        String.valueOf(concepto.getCantidad()),
                        format.format(concepto.getPUnitario()),
                        format.format(concepto.getDescuento()),
                        format.format(concepto.getSubtotal())
                });
            }
            tabla.agregarFila(datos);
        }else{
            tabla.Cabecera(R.array.condepto_venta);
        }
    }
}
