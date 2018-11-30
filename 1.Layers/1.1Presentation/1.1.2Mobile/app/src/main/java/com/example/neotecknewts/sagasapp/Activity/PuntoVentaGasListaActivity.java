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
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_punto_venta_gas_lista);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa",false);
            esGasLP = extras.getBoolean("esGasLP",false);
            esCilindroGas = extras.getBoolean("esCilindroGas");
            esCilindro = extras.getBoolean("esCilindro");
        }
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
            if(esCilindroGas ||esCilindro) {
                int size = data_get.size();
                List<ConceptoDTO> conceptoDTOS = new ArrayList<>();
                for (int x = 0; x < RVPuntoVentaGasActivityListaGas.getChildCount(); x++) {
                    View view = RVPuntoVentaGasActivityListaGas.getChildAt(x);
                    LinearLayout linearLayout = view.findViewById(R.id.Layout);
                    CardView card = linearLayout.findViewById(R.id.CVEstacionesCarburacionItem);
                    EditText editText = card.findViewById(R.id.CVEstacionesCarburacionItem).
                            findViewById(R.id.ETPuntoVentaGasListActivityCantidad);
                    TextView TVTipo = card.findViewById(R.id.PuntoVentaGasListaActivityTipoGas);
                    if (editText.getText().toString().trim().length() > 0) {
                        ConceptoDTO conceptoDTO = new ConceptoDTO();
                        conceptoDTO.setCantidad(Integer.parseInt(editText.getText().toString()));
                        conceptoDTO.setConcepto(TVTipo.getText().toString());
                        conceptoDTO.setPUnitario(data_get.get(x).getPrecioUnitario());
                        conceptoDTO.setDescuento(data_get.get(x).getDescuento());
                        conceptoDTOS.add(conceptoDTO);
                        conceptoDTO.setIdTipoGas(data_get.get(x).getId());
                        if (esCilindro || esCilindroGas) {
                            //Falta agregar los valores de los cilindros
                            ConceptoDTO conceptoDTOcilindro = new ConceptoDTO();
                            conceptoDTOcilindro.setCantidad(Integer.parseInt(editText.getText().toString()));
                            conceptoDTOcilindro.setConcepto(precioVentaDTO.getProducto());
                            conceptoDTOcilindro.setPUnitario(precioVentaDTO.getPrecioSalidaKg());
                            conceptoDTOcilindro.setDescuento(0);
                            double sub = Integer.parseInt(editText.getText().toString()) *
                                    precioVentaDTO.getPrecioSalidaKg();
                            conceptoDTO.setSubtotal(sub);
                            conceptoDTOS.add(conceptoDTOcilindro);
                        }
                    }
                }

                for (int x=0;x<conceptoDTOS.size();x++){
                    actualizarConceptos(conceptoDTOS.get(x));
                }
            }else{
                List<ConceptoDTO> conceptoDTOS = new ArrayList<>();
                ConceptoDTO conceptoDTO = new ConceptoDTO();
                conceptoDTO.setCantidad(Integer.parseInt(adapter.cantidad.getText().toString()));
                conceptoDTO.setConcepto(precioVentaDTO.getProducto());
                conceptoDTO.setPUnitario(precioVentaDTO.getPrecioSalidaKg());
                conceptoDTO.setDescuento(0);
                conceptoDTO.setIdTipoGas(precioVentaDTO.getIdProductoLinea());
                double sub = Integer.parseInt(adapter.cantidad.getText().toString()) *
                        precioVentaDTO.getPrecioSalidaKg();
                conceptoDTO.setSubtotal(sub);
                conceptoDTOS.add(conceptoDTO);
                for (int x=0;x<conceptoDTOS.size();x++){
                    actualizarConceptos(conceptoDTOS.get(x));
                }
            }
        });
        session = new Session(this);
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
        LinearLayoutManager linearLayout = new LinearLayoutManager(
                PuntoVentaGasListaActivity.this);
        RVPuntoVentaGasActivityListaGas.setLayoutManager(linearLayout);
        RVPuntoVentaGasActivityListaGas.setHasFixedSize(true);
        presenter = new PuntoVentaGasListaPresenterImpl(this);
        //Formulario de venta de pipas y estaciones
        CLFormularioVentaCamionetaYPipaContenedor = findViewById(R.id.
                CLFormularioVentaCamionetaYPipaContenedor);
        CLFormularioVentaCamionetaYPipaContenedor.setVisibility(EsVentaCamioneta? View.GONE:
                View.VISIBLE);
        SWFormularioVentaCamionetaYPipaCredito = findViewById(R.id.
                SWFormularioVentaCamionetaYPipaCredito);
        SWFormularioVentaCamionetaYPipaCredito.setChecked(
                ventaDTO.isCredito()
        );
        SWFormularioVentaCamionetaYPipaFactura = findViewById(R.id.
                SWFormularioVentaCamionetaYPipaFactura);
        SWFormularioVentaCamionetaYPipaFactura.setChecked(
                ventaDTO.isFactura()
        );
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
        //Formulario de venta de pipas y estaciones
        if(EsVentaCamioneta){
            if(esCilindro ||esCilindroGas) {
                presenter.getListaCamionetaCilindros(session.getToken(),
                        esGasLP, esCilindroGas, esCilindro);
                presenter.getPrecioVenta(session.getToken());
            }else{
                presenter.getPrecioVenta(session.getToken());
            }
        }else{
            presenter.getPrecioVenta(session.getToken());
            /*presenter.getListaVenta(session.getToken(),
                    esGasLP,esCilindroGas,esCilindro);
            presenter.getPrecioVenta(session.getToken());*/
        }
        SWFormularioVentaCamionetaYPipaCredito.setChecked(
                ventaDTO.isCredito()
        );
        SWFormularioVentaCamionetaYPipaFactura.setChecked(
                ventaDTO.isFactura()
        );
        mostrarConsepto(ventaDTO.getConcepto());
    }

    private void SetearLitrosDespachados() {
        double total,subtotal, iva,precio,desc;
        ventaDTO.setCredito(SWFormularioVentaCamionetaYPipaCredito.isChecked());
        ventaDTO.setFactura(SWFormularioVentaCamionetaYPipaFactura.isChecked());
        ConceptoDTO conceptoDTO = new ConceptoDTO();
        conceptoDTO.setConcepto("Litros de gas");
        conceptoDTO.setCantidad(Integer.valueOf(adapter.cantidad.getText().toString()));
        conceptoDTO.setPUnitario(adapter.existencia.getPrecioUnitario());
        conceptoDTO.setDescuento(adapter.existencia.getDescuento());
        conceptoDTO.setSubtotal(Double.valueOf(adapter.Subtotal.getText().toString()));
        conceptoDTO.setLitrosDespachados(Double.parseDouble(adapter.cantidad.getText().toString()));
        ventaDTO.getConcepto().add(conceptoDTO);
        Intent intent = new Intent(PuntoVentaGasListaActivity.this,
                PuntoVentaPagarActivity.class);
        intent.putExtra("ventaDTO",ventaDTO);
        intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
        intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
        intent.putExtra("EsVentaPipa",EsVentaPipa);
        startActivity(intent);
    }

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
            adapter = new PuntoVentaAdapter(data,EsVentaCamioneta,this);
            if(!EsVentaCamioneta){
                adapter.esVentaGas = true;
                adapter.PrecioLitro = TVFormularioVentaCamionetaYPipaPrecio;
                adapter.Descuento = TVFormularioVentaCamionetaYPipaDescuento;
                adapter.Subtotal = TVFormularioVentaCamionetaYPipaSubtotal;
                adapter.Iva = TVFormularioVentaCamionetaYPipaIva;
                adapter.Total =TVFormularioVentaCamionetaYPipaTotal;
            }

            RVPuntoVentaGasActivityListaGas.setAdapter(adapter);

        }
    }

    public void actualizarConceptos(ConceptoDTO conceptoDTO){
        double precio = conceptoDTO.getPUnitario();
        if(conceptoDTO.getDescuento()>0){
            precio = conceptoDTO.getPUnitario() - conceptoDTO.getDescuento();
        }
        precio = precio * conceptoDTO.getCantidad();
        conceptoDTO.setSubtotal(precio);
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

    @Override
    public void onSuccessPrecioVenta(PrecioVentaDTO data) {
        if(data!=null){
            precioVentaDTO = data;

            if(esGasLP ||EsVentaPipa||EsVentaCarburacion){
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
            }
        }
    }
}
