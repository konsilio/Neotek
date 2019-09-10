package com.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TextView;

import com.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.neotecknewts.sagasapp.Model.DatosVentaOtrosDTO;
import com.neotecknewts.sagasapp.Model.ProductoOtrosDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaOtrosPresenter;
import com.neotecknewts.sagasapp.Presenter.PuntoVentaOtrosPresenterImpl;
import com.neotecknewts.sagasapp.Util.Session;
import com.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.List;

public class PuntoVentaOtrosActivity extends AppCompatActivity implements PuntoVentaOtrosView {


    Spinner SPuntoVentaOtrosCategoria,SPuntoVentaOtrosLinea,SPuntoVentaOtrosActivityProducto;
    EditText ETProductoVentaOtrosConcepto,ETPuntoVentaOtrosActivityPrecio,
            ETPuntoVentaOtrosActivityCantidad, ETPuntoVentaOtrosActivityPrecioporLitro;
    Button BtnPuntoVentaOtrosActivityOpciones,BtnPuntoVentaOtrosActivityAgregar,
            BtnPuntoVentaOtrosActivityPagar;
    TableLayout TLPuntoVentaOtrosActivityConcepto;

    PuntoVentaOtrosPresenter presenter;
    ProgressDialog progressDialog;
    String[] list_categoria,list_linea,list_producto;
    String[] list_categorias_original,list_lineas_original,list_producto_original;
    DatosVentaOtrosDTO otrosDTO;
    VentaDTO ventaDTO;
    ExistenciasDTO existenciasDTO;
    boolean EsVentaCamioneta,EsVentaCarburacion,EsVentaPipa;
    int idCategoria,idLinea,idProducto;

    List<com.neotecknewts.sagasapp.Model.ExistenciasDTO> ExistenciasDTO;

    Session session;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        Log.d("oncreate","si entra");
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
        ETPuntoVentaOtrosActivityPrecioporLitro = findViewById(R.id.ETPuntoVentaGasListActivityPrecioporLitro);
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
            ExistenciasDTO existenciasDTO = new ExistenciasDTO();
            int cantidad = Integer.parseInt(ETPuntoVentaOtrosActivityCantidad.getText().toString());
            //int precioLtr= Integer.parseInt(ETPuntoVentaOtrosActivityPrecioporLitro.getText().toString());
            double punitario = Double.parseDouble(ETPuntoVentaOtrosActivityPrecio.getText().toString());
//Aqui agregar descuento
            double descuento = existenciasDTO.getDescuento();
            conceptoDTO.setCantidad(cantidad);
            //conceptoDTO.setPUnitario(precioLtr);
            conceptoDTO.setSubtotal(cantidad*punitario);
            //conceptoDTO.setPUnitario(punitario);
            conceptoDTO.setDescuento(descuento);
            conceptoDTO.setIdTipoGas(0);
            conceptoDTO.setConcepto(ETProductoVentaOtrosConcepto.getText().toString());
            conceptoDTO.setIdCategoria(idCategoria);
            conceptoDTO.setIdLinea(idLinea);
            conceptoDTO.setIdProducto(idProducto);
            conceptoDTO.setLitrosDespachados(0);


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
                cambioCategoria(i);
                    /*if(adapterView.getItemAtPosition(i).toString().equals("Otro"))
                        idCategoria = -1;
                    if(otrosDTO!= null && otrosDTO.getCategorias()!=null){
                        for(CategoriaDTO categoriaDTO : otrosDTO.getCategorias()){
                            if(categoriaDTO.getCategoria().equals(
                                    adapterView.getItemAtPosition(i).toString()
                            )){
                                idCategoria = categoriaDTO.getIdCategoria();
                            }
                        }
                    }*/
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {
                idCategoria = 0;
            }
        });
        SPuntoVentaOtrosLinea.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                cambioLinea(i);
                /*if(adapterView.getItemAtPosition(i).toString().equals("Otro"))
                    idLinea = -1;
                if(otrosDTO!= null && otrosDTO.getLineas()!=null){
                    for(LineaDTO lineaDTO : otrosDTO.getLineas()){
                        if(lineaDTO.getLinea().equals(
                                adapterView.getItemAtPosition(i).toString()
                        )){
                            idCategoria = lineaDTO.getIdLinea();
                        }
                    }
                }*/
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

    /**
     * cambioCategoria
     * Permite que al momento de registrar la seleccionar la categoria , guardar el valor
     * e identificar si es un campo de otro, se toma como parametro la posicion seleccionada
     * del select de categorias para extraer su nombre y buscar en el objeto
     * el id que le corresponde y gudardarlo en una variable global.
     * @param posicion {@link } que reprecenta la posicion del select a obtener
     *                                el nombre de al categoria
     */
    private void cambioCategoria(int posicion) {
         Object item = SPuntoVentaOtrosCategoria.getItemAtPosition(posicion);
         if(otrosDTO!=null){
             if(!otrosDTO.getCategorias().isEmpty() && otrosDTO.getCategorias().size()>0 &&
                     otrosDTO.getCategorias()!=null){
                 Log.w("Categoria",item.toString());
                 if(list_categoria[posicion].equals("Otros")){
                  idCategoria = -1;
                 }else {
                     for (int x = 0; x < otrosDTO.getCategorias().size(); x++) {

                         if (otrosDTO.getCategorias().get(x).getCategoria().equals(
                                 list_categoria[posicion]
                         )) {
                             idCategoria = otrosDTO.getCategorias().get(x).getIdCategoria();
                         }
                     }
                 }
             }
         }
    }

    /**
     * cambioLinea
     * Permite  realizar la consulta de la posicion actual del input
     * para buscar el id de la linea que le corresponde, despues actualizara con id de la categoria
     * el select de productos
     *
     * @param posicion {@link} que reprecenta la posicion del select a obtener el nombre de
     *                                linea
     */
    private void cambioLinea(int posicion){
        Object item = SPuntoVentaOtrosLinea.getItemAtPosition(posicion);
        ArrayList<String> data = new ArrayList<>();
        if(item.toString().equals("Otros")){
            idLinea = -1;
        }else{
            if(otrosDTO!=null && !otrosDTO.getLineas().isEmpty() && otrosDTO.getLineas()!=null){
                for(int x=0; x< otrosDTO.getLineas().size();x++){
                    if(otrosDTO.getLineas().get(x).getLinea().equals(
                            item.toString()
                    )){
                        idCategoria = otrosDTO.getLineas().get(x).getIdLinea();
                        for (ProductoOtrosDTO productoDTO: otrosDTO.getProducto()){
                            if(productoDTO.getIdCategoria()==idCategoria &&
                                    productoDTO.getIdLinea()==idLinea){
                                data.add(productoDTO.getProducto());
                            }
                        }
                        //if(data.size()>0){
                            SPuntoVentaOtrosActivityProducto.setAdapter(
                                    new ArrayAdapter<>(
                                            this,
                                            R.layout.custom_spinner,
                                            data
                                    )
                            );
                        //}
                    }
                }
            }
        }
    }

    @Override
    public void onSuccess(DatosVentaOtrosDTO dtos) {
        otrosDTO = dtos;
        if(dtos!=null){
            list_categoria = new String[dtos.getCategorias().size()+1];
            list_linea = new String[dtos.getLineas().size()+1];
            list_producto = new String[dtos.getProducto().size()+1];

            list_categorias_original= new String[dtos.getCategorias().size()+1];
            list_lineas_original = new String[dtos.getLineas().size()+1];
            list_producto_original = new String[dtos.getProducto().size()+1];

            list_categoria[0] = "Otros";
            list_linea[0] = "Otros";
            list_producto[0]= "Otros";

            for (int x =0 ;x<dtos.getCategorias().size();x++) {
                list_categoria[x+1] = dtos.getCategorias().get(x).getCategoria();
            }
            for (int x =0 ;x<dtos.getLineas().size();x++) {
                list_linea[x+1] = dtos.getLineas().get(x).getLinea();
            }
            for (int x =0 ;x<dtos.getProducto().size();x++) {
                list_producto[x+1] = dtos.getProducto().get(x).getProducto();
            }
            //region Guardo los datos original para utilizarlos cada ves que se cambie
            for (int x =0 ;x<dtos.getCategorias().size();x++) {
                list_categorias_original[x+1] = dtos.getCategorias().get(x).getCategoria();
            }
            for (int x =0 ;x<dtos.getLineas().size();x++) {
                list_lineas_original[x+1] = dtos.getLineas().get(x).getLinea();
            }
            for (int x =0 ;x<dtos.getProducto().size();x++) {
                list_producto_original[x+1] = dtos.getProducto().get(x).getProducto();
            }
            //endregion

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
        Log.d("mostrarconcepto","entro");
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
                        format.format(concepto.getDescuentoUnitarioLt()),
                        format.format(concepto.getDescuento()),
                        format.format(concepto.getSubtotal())

                });
                Log.d("Preciounitario",concepto.getCantidad()+"");
            }
            tabla.agregarFila(datos);
        }else{
            tabla.Cabecera(R.array.condepto_venta);
        }
    }
}
