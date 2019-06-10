package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.constraint.ConstraintLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.CardView;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.text.Layout;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.GridLayout;
import android.widget.GridView;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.Switch;
import android.widget.TableLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.example.neotecknewts.sagasapp.Adapter.PuntoVentaAdapter;
import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.DatosPuntoVentaDTO;
import com.example.neotecknewts.sagasapp.Model.ExistenciasDTO;
import com.example.neotecknewts.sagasapp.Model.PrecioVentaDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaGasListaPresenter;
import com.example.neotecknewts.sagasapp.Presenter.PuntoVentaGasListaPresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

public class PuntoVentaGasListaActivity extends AppCompatActivity implements PuntoVentaGasListaView{
    RecyclerView RVPuntoVentaGasActivityListaGas;
    //TextView TVPuntoVentaGasListaActivityNombre;
    Button BtnPuntoVetaGasListActivityOpciones,BtnPuntoVentaGasListaActivityGasListaAgregar,
            BtnPuntoVentaGasListActivityPagar;
    TableLayout TLPuntoVentaGasListaActivityConcepto;
    ScrollView SVPuntoVentaGasListActivitiyConcepto;
    //Variables del formulario de venta de camioneta y pipa
    ConstraintLayout CLFormularioVentaCamionetaYPipaContenedor;
    Switch SWFormularioVentaCamionetaYPipaCredito,SWFormularioVentaCamionetaYPipaFactura;
    Button BtnFormularioVentaCamionetaYPipaCancelar,BtnFormularioVentaCamionetaYPipaPagar;
    TextView TVFormularioVentaCamionetaYPipaPrecio,TVFormularioVentaCamionetaYPipaDescuento,
            TVFormularioVentaCamionetaYPipaSubtotal,TVFormularioVentaCamionetaYPipaIva,
            TVFormularioVentaCamionetaYPipaTotal;
    //Variables del formulario de venta de camioneta y pipa
    PuntoVentaGasListaPresenter presenter;
    ProgressDialog progressDialog;
    Session session;
    List<ExistenciasDTO> data_get;

    VentaDTO ventaDTO;
    PuntoVentaAdapter adapter;
    boolean  EsVentaCamioneta,EsVentaCarburacion,EsVentaPipa;
    boolean esGasLP,esCilindroGas,esCilindro;
    Tabla tabla;
    PrecioVentaDTO precioVentaDTO;

    List<ExistenciasDTO> ExistenciasDTO;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_gas_lista);
        Bundle extras = getIntent().getExtras();
        //region Extras (Parametros entre acticitys)
        if(extras!=null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa",false);
            esGasLP = extras.getBoolean("esGasLP",false);
            esCilindroGas = extras.getBoolean("esCilindroGas");
            esCilindro = extras.getBoolean("esCilindro");
        }
        //endregion
        //region Session
        session = new Session(this);
        //endregion
        //region Formulario del layout de veta de cilindros
        BtnPuntoVetaGasListActivityOpciones = findViewById(R.id.BtnPuntoVetaGasListActivityOpciones);
        BtnPuntoVetaGasListActivityOpciones.setVisibility(EsVentaCamioneta? View.VISIBLE:View.GONE);
        BtnPuntoVentaGasListaActivityGasListaAgregar = findViewById(R.id.
                BtnPuntoVentaGasListaActivityGasListaAgregar);
        BtnPuntoVentaGasListaActivityGasListaAgregar.setVisibility(
                EsVentaCamioneta? View.VISIBLE:View.GONE);
        BtnPuntoVentaGasListActivityPagar = findViewById(R.id.BtnPuntoVentaGasListActivityPagar);
        BtnPuntoVentaGasListActivityPagar.setVisibility(
                EsVentaCamioneta? View.VISIBLE:View.GONE);
        RVPuntoVentaGasActivityListaGas = findViewById(R.id.RVPuntoVentaGasActivityListaGas);
        //TVPuntoVentaGasListaActivityNombre = findViewById(R.id.TVPuntoVentaGasListaActivityNombre);
        TLPuntoVentaGasListaActivityConcepto = findViewById(R.id.
                TLPuntoVentaGasListaActivityConcepto);
        SVPuntoVentaGasListActivitiyConcepto = findViewById(R.id.
                SVPuntoVentaGasListActivitiyConcepto);
        SVPuntoVentaGasListActivitiyConcepto.setVisibility(EsVentaCamioneta ?
                View.VISIBLE:View.GONE);
        BtnPuntoVetaGasListActivityOpciones.setOnClickListener(V->{
            Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            startActivity(intent);
        });

        BtnPuntoVentaGasListaActivityGasListaAgregar.setOnClickListener(v->{
            if(esCilindroGas) VentaCilindroGas();
            else if(esCilindro) VentaCilindro();
            else if(esGasLP) VentaGaslp();
            LimpiarLista();
        });

        BtnPuntoVentaGasListActivityPagar.setOnClickListener(v->{
            if(ventaDTO.getConcepto().size()>0) {
                Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                        PuntoVentaPagarActivity.class);
                intent.putExtra("ventaDTO", ventaDTO);
                intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
                intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
                intent.putExtra("EsVentaPipa",EsVentaPipa);
                startActivity(intent);
            }else{
                AlertDialog.Builder builder = new
                        AlertDialog.Builder(this);
                builder.setTitle(R.string.error_titulo);
                builder.setMessage(R.string.No_venta);
                builder.setPositiveButton(R.string.message_acept,
                        ((dialog, which) -> dialog.dismiss()));
                builder.create().show();
            }
        });
        //endregion
        //region Formulario de venta de pipas y estaciones
        CLFormularioVentaCamionetaYPipaContenedor = findViewById(R.id.
                CLFormularioVentaCamionetaYPipaContenedor);
        CLFormularioVentaCamionetaYPipaContenedor.setVisibility(EsVentaCamioneta? View.GONE:
                View.VISIBLE);
        SWFormularioVentaCamionetaYPipaCredito = findViewById(R.id.
                SWFormularioVentaCamionetaYPipaCredito);
        SWFormularioVentaCamionetaYPipaCredito.setVisibility(
                ventaDTO.isTieneCredito()? View.VISIBLE:View.GONE
        );
//        SWFormularioVentaCamionetaYPipaCredito.setVisibility(
//                ventaDTO.isTieneCredito()? View.VISIBLE:View.GONE
//        );

        SWFormularioVentaCamionetaYPipaCredito.setChecked(
                ventaDTO.isCredito()
        );
        SWFormularioVentaCamionetaYPipaFactura = findViewById(R.id.
                SWFormularioVentaCamionetaYPipaFactura);
        ventaDTO.setFactura(true);
        SWFormularioVentaCamionetaYPipaFactura.setChecked(
                ventaDTO.isFactura()
        );
        SWFormularioVentaCamionetaYPipaFactura.setVisibility(View.GONE);
        BtnFormularioVentaCamionetaYPipaCancelar = findViewById(R.id.
                BtnFormularioVentaCamionetaYPipaCancelar);
        BtnFormularioVentaCamionetaYPipaPagar = findViewById(R.id.
                BtnFormularioVentaCamionetaYPipaPagar);
        TVFormularioVentaCamionetaYPipaPrecio = findViewById(R.id.
                TVFormularioVentaCamionetaYPipaPrecio);
        TVFormularioVentaCamionetaYPipaDescuento = findViewById(R.id.
                TVFormularioVentaCamionetaYPipaDescuento);
        TVFormularioVentaCamionetaYPipaSubtotal = findViewById(R.id.
                TVFormularioVentaCamionetaYPipaSubtotal);
        TVFormularioVentaCamionetaYPipaIva  = findViewById(R.id.TVFormularioVentaCamionetaYPipaIva);
        TVFormularioVentaCamionetaYPipaTotal = findViewById(R.id.
                TVFormularioVentaCamionetaYPipaTotal);
        BtnFormularioVentaCamionetaYPipaCancelar.setOnClickListener(V->{
            Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                    MenuActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            startActivity(intent);
        });
        BtnFormularioVentaCamionetaYPipaPagar.setOnClickListener(V->{
            SetearLitrosDespachados();
        });
        //endregion
        //region Obtención de catalogos web api
        presenter = new PuntoVentaGasListaPresenterImpl(this);

        presenter.getPrecioVenta(session.getToken());
        if(EsVentaCamioneta) {
            Log.d("Jimmy","EsVentaCamioneta true ");
            presenter.getCamionetaCilindros(
                    esGasLP,
                    esCilindroGas,
                    esCilindro,
                    session.getToken()
            );
        }else{
            Log.d("Jimmy","EsVentaCamioneta false");
            presenter.getCamionetaCilindros(
                    esGasLP,
                    esCilindroGas,
                    esCilindro,
                    session.getToken()
            );
        }
        //endregion

        presenter.getListaCamionetaCilindros(session.getToken(),
                esGasLP, esCilindroGas, esCilindro);
        //Agrego la lista de conceptos que viene actualmente
        mostrarConsepto(ventaDTO.getConcepto());

        //Ya lo hace en un inicio
        /*SWFormularioVentaCamionetaYPipaCredito.setChecked(
                ventaDTO.isCredito()
        );
        SWFormularioVentaCamionetaYPipaFactura.setChecked(
                ventaDTO.isFactura()
        );*/
    }
    //region Metodos sobreescritos de la interfaz
    @Override
    public void mostrarConsepto(List<ConceptoDTO> list) {
        tabla = new Tabla(this,TLPuntoVentaGasListaActivityConcepto);
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

    @Override
    public void onHideProgress() {
        if(progressDialog!=null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onShowProgress(int message_cargando) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setIndeterminate(true);
        progressDialog.show();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> dialog.dismiss()));
        builder.create();
        builder.show();

    }

    @Override
    public void onSuccessListExistencia(List<ExistenciasDTO> data) {
        if(data!=null){
            data_get = data;
            /*adapter = new PuntoVentaAdapter(data,EsVentaCamioneta,this);
            if(!EsVentaCamioneta){
                adapter.esVentaGas = true;
                adapter.PrecioLitro = TVFormularioVentaCamionetaYPipaPrecio;
                adapter.Descuento = TVFormularioVentaCamionetaYPipaDescuento;
                adapter.Subtotal = TVFormularioVentaCamionetaYPipaSubtotal;
                adapter.Iva = TVFormularioVentaCamionetaYPipaIva;
                adapter.Total =TVFormularioVentaCamionetaYPipaTotal;
            }

            RVPuntoVentaGasActivityListaGas.setAdapter(adapter);*/

        }
    }

    /**
     * onSuccessPrecioVenta
     * Permite obtener el precio actual de venta de gas,
     * en caso de que el web api este disponible y el resultado en el
     * parametro de {@link PrecioVentaDTO} tenga valores lo guarda en
     * una variable de esta actividad
     * @param data Respuesta del web api al consultar el precio de venta
     */
    @Override
    public void onSuccessPrecioVenta(PrecioVentaDTO data) {
        if(data!=null){
            precioVentaDTO = data;

            /*if(esGasLP ||EsVentaPipa||EsVentaCarburacion){
                List<ExistenciasDTO> existenciasDTOS = new ArrayList<>();
                ExistenciasDTO existencia_gas = new ExistenciasDTO();
                existencia_gas.setCantidad(String.valueOf(data.getPrecioSalidaKg()));
                existencia_gas.setDescuento(0);
                existencia_gas.setExistencias(0);
                existencia_gas.setId(data.getId());
                existencia_gas.setNombre(data.getProducto());
                existencia_gas.setPrecioUnitario(data.getPrecioSalidaKg());
                existenciasDTOS.add(existencia_gas);
                adapter = new PuntoVentaAdapter(existenciasDTOS,EsVentaCamioneta,this);
                adapter.esVentaGas = true;
                adapter.PrecioLitro = TVFormularioVentaCamionetaYPipaPrecio;
                adapter.Descuento = TVFormularioVentaCamionetaYPipaDescuento;
                adapter.Subtotal = TVFormularioVentaCamionetaYPipaSubtotal;
                adapter.Iva = TVFormularioVentaCamionetaYPipaIva;
                adapter.Total =TVFormularioVentaCamionetaYPipaTotal;
                adapter.precioVentaDTO =precioVentaDTO;

                RVPuntoVentaGasActivityListaGas.setAdapter(adapter);
            }*/
        }
    }

    /**
     * onSuccessDatosCamioneta
     * Permite obtener los items de venta de gas, dependiendo del tipo de
     * venta (Veta de gas lp, venta de cilindros y venta de cilindros con gas) se
     * retornaran valores diferentes en el objeto {@link ExistenciasDTO} que
     * se tiene como parametro
     * @param respuesta Respuesta del web api a mostrar en la lista de venta de gas o cilindros
     *             segun sea el caso
     */
    @Override
    public void onSuccessDatosCamioneta(List<ExistenciasDTO> respuesta) {
        ExistenciasDTO = respuesta;
        if(ExistenciasDTO!=null){
            adapter = new PuntoVentaAdapter(ExistenciasDTO,EsVentaCamioneta,this);
            if(!EsVentaCamioneta){
                adapter.esVentaGas = true;
                adapter.PrecioLitro = TVFormularioVentaCamionetaYPipaPrecio;
                adapter.Descuento = TVFormularioVentaCamionetaYPipaDescuento;
                adapter.Subtotal = TVFormularioVentaCamionetaYPipaSubtotal;
                adapter.Iva = TVFormularioVentaCamionetaYPipaIva;
                adapter.Total =TVFormularioVentaCamionetaYPipaTotal;
                adapter.precioVentaDTO = precioVentaDTO;
            }
            if(esGasLP && EsVentaCamioneta){
                adapter.esVentaGas = true;
                adapter.PrecioLitro = TVFormularioVentaCamionetaYPipaPrecio;
                adapter.Descuento = TVFormularioVentaCamionetaYPipaDescuento;
                adapter.Subtotal = TVFormularioVentaCamionetaYPipaSubtotal;
                adapter.Iva = TVFormularioVentaCamionetaYPipaIva;
                adapter.Total =TVFormularioVentaCamionetaYPipaTotal;
                adapter.precioVentaDTO = precioVentaDTO;
            }
            if(esCilindro){
                adapter.Mostrar = false;
            }else{
                adapter.Mostrar = true;
            }
            //region Seteo de la lista del reciclerview
            LinearLayoutManager linearLayout = new LinearLayoutManager(
                    PuntoVentaGasListaActivity.this);
            RVPuntoVentaGasActivityListaGas.setLayoutManager(linearLayout);
            RVPuntoVentaGasActivityListaGas.setHasFixedSize(true);
            //endregion
            RVPuntoVentaGasActivityListaGas.setAdapter(adapter);

        }

    }
    //endregion

    //region Metodos privados de la vista
    /**
     * SetearLitrosDespachados
     * Permite colocar los datos de litros despachados en esta vista en
     * la venta {@link VentaDTO} en el apartado de detalle de venta {@link ConceptoDTO}
     */
    private void SetearLitrosDespachados() {
        Log.d("Jimmy","SetearLitrosDespachados");
        if(adapter.cantidad.getText()!=null && adapter.cantidad.getText().toString().length()>0) {
            if (Double.valueOf(adapter.cantidad.getText().toString()) > 0) {
                double total, subtotal, iva, precio, desc;
                ventaDTO.setCredito(SWFormularioVentaCamionetaYPipaCredito.isChecked());
                ventaDTO.setFactura(SWFormularioVentaCamionetaYPipaFactura.isChecked());
                ConceptoDTO conceptoDTO = new ConceptoDTO();
                conceptoDTO.setConcepto("Litros de gas");
                conceptoDTO.setCantidad(Double.valueOf(adapter.cantidad.getText().toString()));
                conceptoDTO.setPUnitario(adapter.existencia.getPrecioUnitario());
                conceptoDTO.setDescuento(adapter.existencia.getDescuento());
                conceptoDTO.setSubtotal(Double.valueOf(adapter.Subtotal.getText().toString()));
                conceptoDTO.setLitrosDespachados(Double.parseDouble(adapter.cantidad.getText().toString()));
                conceptoDTO.setCantidadLt(Double.valueOf(adapter.cantidad.getText().toString()));
                Calendar calendar = Calendar.getInstance();
                conceptoDTO.setYear(calendar.get(Calendar.YEAR));
                conceptoDTO.setMes(calendar.get(Calendar.MONTH) + 1);
                conceptoDTO.setDia(calendar.get(Calendar.DAY_OF_WEEK));
                conceptoDTO.setPrecioUnitarioProducto(adapter.existencia.getPrecioUnitario());
                conceptoDTO.setPrecioUnitarioLt(precioVentaDTO.getPrecioSalidaLt());
                conceptoDTO.setPrecioUnitarioKg(precioVentaDTO.getPrecioSalidaKg());
                conceptoDTO.setIdProducto(precioVentaDTO.getIdProducto());
                conceptoDTO.setIdLinea(precioVentaDTO.getIdProductoLinea());
                conceptoDTO.setIdCategoria(precioVentaDTO.getIdCategoria());
                conceptoDTO.setIdUnidadmedida(precioVentaDTO.getIdUnidadMedida());
                conceptoDTO.setCantidadKg(
                        precioVentaDTO.getPrecioSalidaKg() *
                                Double.parseDouble(adapter.cantidad.getText().toString())
                );
                conceptoDTO.setCantidadLt(
                        precioVentaDTO.getPrecioSalidaLt() *
                                Double.parseDouble(adapter.cantidad.getText().toString())
                );
                conceptoDTO.setLitrosDespachados(Double.parseDouble(adapter.cantidad.getText().toString()));
                conceptoDTO.setPUnitario(precioVentaDTO.getPrecioSalidaKg());
                conceptoDTO.setIdTipoGas(0);
                ventaDTO.getConcepto().add(conceptoDTO);
                Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                        PuntoVentaPagarActivity.class);
                intent.putExtra("ventaDTO", ventaDTO);
                intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
                intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
                intent.putExtra("EsVentaPipa", EsVentaPipa);
                startActivity(intent);
            }
        }
    }

    /**
     * Permite realizar la actualizacion de la tabla actual de conceptos
     * en caso de que se agregue otra venta
     * @param conceptoDTO Objeto con el cocepto de venta a agregar
     */
    private void actualizarConceptos(ConceptoDTO conceptoDTO){
        double precio = conceptoDTO.getPUnitario();
        if(conceptoDTO.getDescuento()>0){
            precio = conceptoDTO.getPUnitario() - conceptoDTO.getDescuento();
        }
        precio = precio * conceptoDTO.getCantidad();
        //conceptoDTO.setSubtotal(precio);
        ventaDTO.getConcepto().add(conceptoDTO);
        NumberFormat format = NumberFormat.getCurrencyInstance();
        TLPuntoVentaGasListaActivityConcepto.removeAllViews();
        //
        ArrayList<String[]> datos = new ArrayList<>();
        NumberFormat formato = NumberFormat.getCurrencyInstance();
        for (ConceptoDTO concepto:ventaDTO.getConcepto()){
            datos.add(new String[]{
                    concepto.getConcepto(),
                    String.valueOf(concepto.getCantidad()),
                    formato.format(concepto.getPUnitario()),
                    formato.format(concepto.getDescuento()),
                    formato.format(concepto.getSubtotal())
            });
        }
        tabla.Cabecera(R.array.condepto_venta);
        tabla.agregarFila(datos);
    }

    /**
     * VentaCilindroGas
     * Permite agregar la la tabla de la vista los detalles de la venta
     * de gas con cilindro
     */
    private void VentaCilindroGas() {
        List<ConceptoDTO> conceptos = new ArrayList<>();
        for (int x = 0; x < RVPuntoVentaGasActivityListaGas.getChildCount(); x++) {
            View view = RVPuntoVentaGasActivityListaGas.getChildAt(x);
            LinearLayout linearLayout = view.findViewById(R.id.Layout);
            CardView card = linearLayout.findViewById(R.id.CVEstacionesCarburacionItem);
            EditText editText = card.findViewById(R.id.CVEstacionesCarburacionItem).
                    findViewById(R.id.ETPuntoVentaGasListActivityCantidad);
            TextView TVTipo = card.findViewById(R.id.PuntoVentaGasListaActivityTipoGas);
            Calendar  calendar = Calendar.getInstance();
            if (editText.getText().toString().trim().length() > 0) {
                double cantidadVenta = Double.valueOf(editText.getText().toString());
                double cantidadActual = adapter.getCilindro(x).getExistencias();

                if(cantidadActual>0) {
                    if (cantidadActual>=cantidadVenta) {
                        ConceptoDTO cilindros = new ConceptoDTO();

                        cilindros.setIdEmpresa(session.getIdEmpresa());
                        cilindros.setDia(calendar.get(Calendar.DAY_OF_WEEK));
                        cilindros.setMes(calendar.get(Calendar.MONTH) + 1);
                        cilindros.setYear(calendar.get(Calendar.YEAR));
                        cilindros.setIdProducto(0);//Queda pendiente el id del producto del cilindro
                        cilindros.setIdLinea(0);//Queda pendiente el id de linea del cilindro
                        cilindros.setIdCategoria(0);//Queda pendiete el id de la categoria del cilindro
                        cilindros.setIdUnidadmedida(0);//Queda pendiente el id de unidad de medida
                        // del cilindro
                        cilindros.setPrecioUnitarioProducto(adapter.getCilindro(x)
                                .getPrecioUnitario());
                        cilindros.setPrecioUnitarioKg(0);
                        cilindros.setPrecioUnitarioLt(0);
                        cilindros.setDescuento(0);//Queda pendiente que se consulte el descuento del cliente
                        cilindros.setDescuentoUnitarioKg(0.0);
                        cilindros.setDescuentoUnitarioLt(0.0);
                        cilindros.setCantidad(cantidadVenta);
                        cilindros.setCantidadLt(0);
                        cilindros.setCantidadKg(0);
                        cilindros.setDescuentoTotal(0);//Qeuda pendiente el calculo de descuento total
                        cilindros.setSubtotal(
                                cilindros.getCantidad() * cilindros.getPrecioUnitarioProducto()
                        );
                        cilindros.setPUnitario(cilindros.getPrecioUnitarioProducto());
                        cilindros.setConcepto(TVTipo.getText().toString());
                        cilindros.setPUnitario(cilindros.getPrecioUnitarioProducto());
                        cilindros.setEsVentaCilindro(true);
                        cilindros.setIdCilindro(adapter.getCilindro(x).getId());
                        conceptos.add(cilindros);
                        //Costo del gas
                        ConceptoDTO Gas = new ConceptoDTO();
                        Gas.setIdEmpresa(session.getIdEmpresa());
                        Gas.setYear(calendar.get(Calendar.YEAR));
                        Gas.setMes(calendar.get(Calendar.MONTH) + 1);
                        Gas.setDia(calendar.get(Calendar.DAY_OF_WEEK));
                        Gas.setIdProducto(precioVentaDTO.getIdProducto());
                        Gas.setIdLinea(precioVentaDTO.getIdProductoLinea());
                        Gas.setIdCategoria(precioVentaDTO.getIdCategoria());
                        Gas.setIdUnidadmedida(precioVentaDTO.getIdUnidadMedida());
                        Gas.setPrecioUnitarioProducto(0);
                        Gas.setPrecioUnitarioLt(precioVentaDTO.getPrecioSalidaLt());
                        Gas.setPrecioUnitarioKg(precioVentaDTO.getPrecioSalidaKg());
                        Gas.setDescuento(0);
                        Gas.setDescuentoUnitarioKg(0);
                        Gas.setDescuentoUnitarioLt(0);
                        double capacidadLt = 0;
                        double capacidadKg = 0;
                        for (ExistenciasDTO item: data_get){
                            if(item.getId()==adapter.getCilindro(x).getId()) {
                                capacidadLt = item.getCapacidadLt();
                                capacidadKg = item.getCapacidadKg();
                            }
                        }
                        Gas.setCantidad(
                                /*adapter.getCilindro(x).getCapacidadLt()*/ capacidadKg *
                                        cilindros.getCantidad()
                        );
                        Gas.setCantidadKg(
                                (precioVentaDTO.getPrecioSalidaKg() * /*adapter.getCilindro(x).getCapacidadKg()*/capacidadKg) *
                                        cilindros.getCantidad()
                        );
                        Gas.setCantidadLt(
                                (precioVentaDTO.getPrecioSalidaLt() * capacidadLt /*adapter.getCilindro(x).getCapacidadLt()*/) *
                                        cilindros.getCantidad()
                        );
                        Gas.setSubtotal(Gas.getPrecioUnitarioKg() * Gas.getCantidad());
                        Gas.setConcepto("Gas");
                        Gas.setLitrosDespachados(Integer.parseInt(editText.getText().toString()));
                        Gas.setPUnitario(precioVentaDTO.getPrecioSalidaKg());
                        conceptos.add(Gas);
                    }else{
                        AlertDialog.Builder builder = new
                                AlertDialog.Builder(this,R.style.AlertDialog);
                        builder.setTitle(R.string.mensjae_error_campos);
                        builder.setMessage("No hay existencias suficientes de "+TVTipo.getText()+
                                " actualmente cuentas con "+cantidadActual

                        );
                        builder.setPositiveButton(R.string.message_acept,(dialog, which) ->
                        dialog.dismiss());
                        builder.create().show();
                        break;
                    }

                }else{
                    AlertDialog.Builder  alert =
                            new AlertDialog.Builder(this,R.style.AlertDialog);
                    alert.setTitle(R.string.title_alert_message);
                    alert.setMessage("No hay existencias suficientes del cilindro de "+
                    adapter.getCilindro(x).getNombre().replace(",0000"," KG"));
                    alert.setPositiveButton(R.string.message_acept,(dialog, which) ->{
                                editText.setText("");
                                dialog.dismiss();
                            });

                    alert.create().show();
                }
            }
        }
        for (int x=0;x<conceptos.size();x++){
            actualizarConceptos(conceptos.get(x));
        }
    }

    /**
     * VentaCilindro
     * Permite agregar la venta del cilindro vacio de
     * gas a la tabla de detalle de venta
     */
    private void VentaCilindro() {
        List<ConceptoDTO> conceptos = new ArrayList<>();

        for (int x = 0; x < adapter.getItemCount(); x++) {
            //View view = RVPuntoVentaGasActivityListaGas.getChildAt(x);
            //LinearLayout linearLayout = view.findViewById(R.id.Layout);
            //CardView card = linearLayout.findViewById(R.id.CVEstacionesCarburacionItem);
            //EditText editText = card.findViewById(R.id.CVEstacionesCarburacionItem).
            //        findViewById(R.id.ETPuntoVentaGasListActivityCantidad);
            //TextView TVTipo = card.findViewById(R.id.PuntoVentaGasListaActivityTipoGas);
            Calendar  calendar = Calendar.getInstance();
            if(adapter.getCilindro(x).getCantidad()!=null) {
                if (adapter.getCilindro(x).getCantidad().length() > 0
                        ) {
                    //if (editText.getText().toString().trim().length() > 0) {
                    int cantidad = Integer.parseInt(adapter.getCilindro(x).getCantidad());
                    if (cantidad > 0) {
                        ConceptoDTO cilindros = new ConceptoDTO();

                        cilindros.setIdEmpresa(session.getIdEmpresa());
                        cilindros.setDia(calendar.get(Calendar.DAY_OF_WEEK));
                        cilindros.setMes(calendar.get(Calendar.MONTH) + 1);
                        cilindros.setYear(calendar.get(Calendar.YEAR));
                        cilindros.setIdProducto(0);//Queda pendiente el id del producto del cilindro
                        cilindros.setIdLinea(0);//Queda pendiente el id de linea del cilindro
                        cilindros.setIdCategoria(0);//Queda pendiete el id de la categoria del cilindro
                        cilindros.setIdUnidadmedida(0);//Queda pendiente el id de unidad de medida
                        // del cilindro
                        cilindros.setPrecioUnitarioProducto(data_get.get(x).getPrecioUnitario());
                        cilindros.setPrecioUnitarioKg(0);
                        cilindros.setPrecioUnitarioLt(0);
                        cilindros.setDescuento(0);//Queda pendiente que se consulte el descuento del cliente
                        cilindros.setDescuentoUnitarioKg(0.0);
                        cilindros.setDescuentoUnitarioLt(0.0);
                        cilindros.setCantidad(cantidad);
                        cilindros.setCantidadLt(0);
                        cilindros.setCantidadKg(0);
                        cilindros.setDescuentoTotal(0);//Qeuda pendiente el calculo de descuento total
                        cilindros.setSubtotal(
                                cilindros.getCantidad() * cilindros.getPrecioUnitarioProducto()
                        );
                        cilindros.setPUnitario(cilindros.getPrecioUnitarioProducto());
                        cilindros.setConcepto(adapter.getCilindro(x).getNombre());
                        cilindros.setPUnitario(cilindros.getPrecioUnitarioProducto());
                        cilindros.setEsVentaCilindro(true);
                        cilindros.setIdCilindro(adapter.getCilindro(x).getId());
                        conceptos.add(cilindros);

                    }
                    //}
                }
            }
        }
        for (int x=0;x<conceptos.size();x++){
            actualizarConceptos(conceptos.get(x));
        }
    }

    /**
     * VentaGaslp
     * Realiza el calculo de la venta de gas LP y lo coloca en la tabla de
     * conceptos de la venta.
     */
    private void VentaGaslp() {
        List<ConceptoDTO> conceptos = new ArrayList<>();
        for (int x = 0; x < RVPuntoVentaGasActivityListaGas.getChildCount(); x++) {
            View view = RVPuntoVentaGasActivityListaGas.getChildAt(x);
            LinearLayout linearLayout = view.findViewById(R.id.Layout);
            CardView card = linearLayout.findViewById(R.id.CVEstacionesCarburacionItem);
            EditText editText = card.findViewById(R.id.CVEstacionesCarburacionItem).
                    findViewById(R.id.ETPuntoVentaGasListActivityCantidad);
            TextView TVTipo = card.findViewById(R.id.PuntoVentaGasListaActivityTipoGas);
            Calendar  calendar = Calendar.getInstance();
            if(!editText.getText().toString().isEmpty()) {
                double cantidadVenta = Double.valueOf(editText.getText().toString());
                double cantidadActual = adapter.getCilindro(x).getExistencias();
                if (editText.getText().toString().trim().length() > 0 && cantidadVenta>0 ) {
                    if(cantidadVenta<=cantidadActual) {
                        //Costo del gas
                        ConceptoDTO Gas = new ConceptoDTO();
                        Gas.setIdEmpresa(session.getIdEmpresa());
                        Gas.setYear(calendar.get(Calendar.YEAR));
                        Gas.setMes(calendar.get(Calendar.MONTH) + 1);
                        Gas.setDia(calendar.get(Calendar.DAY_OF_WEEK));
                        Gas.setIdProducto(precioVentaDTO.getIdProducto());
                        Gas.setIdLinea(precioVentaDTO.getIdProductoLinea());
                        Gas.setIdCategoria(precioVentaDTO.getIdCategoria());
                        Gas.setIdUnidadmedida(precioVentaDTO.getIdUnidadMedida());
                        Gas.setPrecioUnitarioProducto(precioVentaDTO.getPrecioSalidaKg());
                        Gas.setPrecioUnitarioLt(precioVentaDTO.getPrecioSalidaLt());
                        Gas.setPrecioUnitarioKg(precioVentaDTO.getPrecioSalidaKg());
                        Gas.setDescuento(0);
                        Gas.setDescuentoUnitarioKg(0);
                        Gas.setDescuentoUnitarioLt(0);
                        Gas.setCantidad(
                                cantidadVenta
                        );
                        if(EsVentaCamioneta && esGasLP){
                            double cap = ExistenciasDTO.get(x).getCapacidadKg();
                            Gas.setCantidadKg(
                                    cap * cantidadVenta *
                                    precioVentaDTO.getPrecioSalidaKg()

                            );
                            Gas.setCantidadLt(
                                    cap * cantidadVenta *
                                    precioVentaDTO.getPrecioSalidaLt()
                            );

                            Gas.setSubtotal(
                                    cap * cantidadVenta * precioVentaDTO.getPrecioSalidaKg()
                            );
                            Gas.setConcepto(TVTipo.getText().toString());
                            Gas.setLitrosDespachados(Integer.parseInt(editText.getText().toString()));
                            Gas.setPUnitario(precioVentaDTO.getPrecioSalidaKg());
                        }else{
                            Gas.setCantidadKg(
                                    precioVentaDTO.getPrecioSalidaKg() * cantidadVenta
                            );
                            Gas.setCantidadLt(
                                    precioVentaDTO.getPrecioSalidaLt() * cantidadVenta
                            );
                            Gas.setSubtotal(Gas.getPrecioUnitarioLt() * Gas.getCantidad());
                            Gas.setConcepto(TVTipo.getText().toString());
                            Gas.setLitrosDespachados(Integer.parseInt(editText.getText().toString()));
                            Gas.setPUnitario(precioVentaDTO.getPrecioSalidaKg());
                        }


                        conceptos.add(Gas);
                    }
                    else
                    {
                        AlertDialog.Builder builder =
                                new AlertDialog.Builder(this,R.style.AlertDialog);
                        builder.setTitle(R.string.mensjae_error_campos);
                        builder.setMessage("La cantidad sobrepasa la existencia actual que es "+
                        cantidadActual+" Lt");
                        builder.setPositiveButton(R.string.message_acept,(dialog, which) ->
                                dialog.dismiss());
                        builder.create().show();
                    }
                }
            }
        }
        for (int x=0;x<conceptos.size();x++){
            actualizarConceptos(conceptos.get(x));
        }
    }

    public void LimpiarLista(){
        for (int x = 0; x <data_get.size()-1; x++) {
            View view = RVPuntoVentaGasActivityListaGas.getLayoutManager().getChildAt(x);
            CardView CVEstacionesCarburacionItem = view.findViewById(R.id.CVEstacionesCarburacionItem);
            EditText editText = CVEstacionesCarburacionItem.findViewById(R.id.Grid).
                    findViewById(R.id.ETPuntoVentaGasListActivityCantidad);
            editText.setText("");
        }
    }
    //endregion
}
