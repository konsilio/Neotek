package com.neotecknewts.sagasapp.Activity;

import android.annotation.SuppressLint;
import android.app.DatePickerDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.neotecknewts.sagasapp.Model.CorteDTO;
import com.neotecknewts.sagasapp.Model.Cortes.UsuariosDTO;
import com.neotecknewts.sagasapp.Model.DatosBusquedaCortesDTO;
import com.neotecknewts.sagasapp.Model.RespuestaEstacionesVentaDTO;
import com.neotecknewts.sagasapp.Model.UsuariosCorteDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;
import com.neotecknewts.sagasapp.Model.VentasCorteDTO;
import com.neotecknewts.sagasapp.Presenter.AnticipoTablaPresenter;
import com.neotecknewts.sagasapp.Presenter.AnticipoTablaPresenterImpl;
import com.neotecknewts.sagasapp.SQLite.SAGASSql;
import com.neotecknewts.sagasapp.Util.Session;
import com.neotecknewts.sagasapp.Util.Tabla;

import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

public class AnticipoTablaActivity extends AppCompatActivity implements AnticipoTablaView {
    Button BtnAnticipoTablaActivityRegresar,BtnAnticipoTablaActivityHacerAnticipo;
    TableLayout TLAnticipoTablaActivityTabla,TLAnticipoTablaActivityResultadosCorteDeCaja;
    TextView TVAnticipoTablaActivityTotal,TVAnticipoTablaActivityTitulo,TVAnticipoTablaActivityP5000,
            TVAnticipoTableActivityInicial,TVAnticipoTableActivityFinal,TVAnticipoTableActivityLitros,
        TVAnticipoTableMontoDeCorte,TVAnticipoTableActivityAnticipos,
            TVAnticipoTablaActivityFecha,TVAnticipoTablaActivityTituloUsuario;
    //Spinner SPAnticipoTablaActivityFechaCorte;
    EditText SPAnticipoTablaActvityUsuario;
    TableRow TRAnticipoTablaActivityTituloAnticipo,TRAnticipoTablaActivityFormAnticipar;
    EditText ETAnticipoTablaActivityAnticipo;
    ImageButton IBAnticipotABLAactivityFecha;

    double total;
    ArrayList<String[]> elementos;
    boolean EsAnticipo,EsCorte;
    AnticiposDTO anticiposDTO;
    CorteDTO corteDTO;
    AnticipoTablaPresenter presenter;
    Session session;
    ProgressDialog progressDialog;
    SAGASSql sagasSql;
    Tabla tabla;
    RespuestaEstacionesVentaDTO datos;
    NumberFormat dformat;
    public int mYear,mMonth,mDay;
    public Date fecha;
    public boolean hasFecha;
    public UsuariosCorteDTO dataUsariosCorte;
    UsuariosDTO usuariosDTO;
    public String[] listUsuarios;
    boolean EsCamioneta,EsEstacion,EsPipa;
    DatosBusquedaCortesDTO datosBusqueda;
    NumberFormat format = NumberFormat.getCurrencyInstance();
    @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate =
            new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
    @SuppressLint("SimpleDateFormat") SimpleDateFormat sfdate =
            new SimpleDateFormat("dd/MM/yyyy");
    double totalTabla;
    double totalCortes;
    double totalAnticipos;

    public DatePickerDialog.OnDateSetListener onDateSetListener =
            (view, year, month, dayOfMonth) -> {
                mYear = year;
                mMonth = month;
                mDay = dayOfMonth;
                setFecha();
            };

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_anticipo_tabla);
        Bundle bundle = getIntent().getExtras();
        if(bundle!= null){
            EsAnticipo = bundle.getBoolean("EsAnticipo",false);
            EsCorte = bundle.getBoolean("EsCorte",false);
            anticiposDTO = (AnticiposDTO) bundle.getSerializable("anticiposDTO");
            corteDTO = (CorteDTO) bundle.getSerializable("corteDTO");
            usuariosDTO =(UsuariosDTO) bundle.getSerializable("usuariosDTO");
            EsCamioneta = bundle.getBoolean("EsCamioneta",false);
            EsEstacion = bundle.getBoolean("EsEstacion",false);
            EsPipa = bundle.getBoolean("EsPipa");
            Log.d("CorteDTO", corteDTO.toString());
        }
        Log.d("CorteDTO", corteDTO.toString());
        hasFecha = false;
        dataUsariosCorte = new UsuariosCorteDTO();
        usuariosDTO = new UsuariosDTO();
        final Calendar c = Calendar.getInstance();
        mYear = c.get(Calendar.YEAR);
        mMonth = c.get(Calendar.MONTH);
        mDay = c.get(Calendar.DAY_OF_MONTH);
        datosBusqueda = new DatosBusquedaCortesDTO();

        session = new Session(AnticipoTablaActivity.this);
        dformat  = new DecimalFormat("#.00");
        BtnAnticipoTablaActivityRegresar = findViewById(R.id.BtnAnticipoTablaActivityRegresar);
        SPAnticipoTablaActvityUsuario = (EditText) findViewById(R.id.SPAnticipoTablaActvityUsuario);
        BtnAnticipoTablaActivityHacerAnticipo = findViewById(R.id.
                BtnAnticipoTablaActivityHacerAnticipo);
        TVAnticipoTablaActivityTotal = findViewById(R.id.TVAnticipoTablaActivityTotal);
        //SPAnticipoTablaActivityFechaCorte = findViewById(R.id.SPAnticipoTablaActivityFechaCorte);
        //SPAnticipoTablaActivityFechaCorte.setVisibility((EsCorte)?View.VISIBLE:View.GONE);
        TVAnticipoTablaActivityTitulo = findViewById(R.id.TVAnticipoTablaActivityTitulo);
        TVAnticipoTablaActivityFecha = findViewById(R.id.TVAnticipoTablaActivityFecha);
        IBAnticipotABLAactivityFecha = findViewById(R.id.IBAnticipotABLAactivityFecha);
        IBAnticipotABLAactivityFecha.setOnClickListener(view -> showDialog(0));
        TVAnticipoTablaActivityP5000 = findViewById(R.id.TVAnticipoTablaActivityP5000);
        ETAnticipoTablaActivityAnticipo = findViewById(R.id.ETAnticipoTablaActivityAnticipo);
        if(EsCorte) {
            TVAnticipoTableActivityInicial = findViewById(R.id.TVAnticipoTableActivityInicial);
            TVAnticipoTableActivityInicial.setText(String.valueOf(corteDTO.getP5000Inicial()));
            TVAnticipoTableActivityFinal = findViewById(R.id.TVAnticipoTableActivityFinal);
            TVAnticipoTableActivityFinal.setText(String.valueOf(corteDTO.getP5000Final()));
            TVAnticipoTableActivityLitros = findViewById(R.id.TVAnticipoTableActivityLitros);
            String cadena = String.valueOf(corteDTO.getLitrosCorte())+" Lt.";
            TVAnticipoTableActivityLitros.setText(cadena);
            TVAnticipoTableActivityAnticipos = findViewById(R.id.TVAnticipoTableActivityAnticipos);
            TVAnticipoTableMontoDeCorte = findViewById(R.id.TVAnticipoTableMontoDeCorte);
            corteDTO.setCamioneta(EsCamioneta);
        }

        TLAnticipoTablaActivityResultadosCorteDeCaja = findViewById(R.id.
                TLAnticipoTablaActivityResultadosCorteDeCaja);
        TLAnticipoTablaActivityResultadosCorteDeCaja.setVisibility((EsCorte)?View.VISIBLE:View.GONE);
        TRAnticipoTablaActivityTituloAnticipo = findViewById(R.id.
                TRAnticipoTablaActivityTituloAnticipo);
        TRAnticipoTablaActivityTituloAnticipo.setVisibility((EsAnticipo)?View.VISIBLE:View.GONE);
        TRAnticipoTablaActivityFormAnticipar = findViewById(R.id.
                TRAnticipoTablaActivityFormAnticipar);
        TRAnticipoTablaActivityFormAnticipar.setVisibility((EsAnticipo)? View.VISIBLE:View.GONE);
        TVAnticipoTablaActivityTitulo.setText((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        setTitle((EsCorte)?getString(R.string.corte_de_caja):
                getString(R.string.Anticipo));
        BtnAnticipoTablaActivityRegresar.setOnClickListener(V->finish());
        BtnAnticipoTablaActivityHacerAnticipo.setOnClickListener(V-> VerificarCampos());
        BtnAnticipoTablaActivityHacerAnticipo.setText((EsCorte)?getString(R.string.hacer_corte):
        getString(R.string.hacer_anticipo));
        TVAnticipoTablaActivityP5000.setVisibility((EsCorte)? View.VISIBLE:View.GONE);
        TLAnticipoTablaActivityTabla = findViewById(R.id.TLAnticipoTablaActivityTabla);
        presenter = new AnticipoTablaPresenterImpl(this);
        session = new Session(this);
        /*presenter.getAnticipos(session.getToken()
                ,EsAnticipo?anticiposDTO.getIdEstacion():corteDTO.getIdEstacion(),EsAnticipo);*/
        tabla = new Tabla(this, TLAnticipoTablaActivityTabla);
        tabla.Cabecera(R.array.header_tabla_anticipo);
        elementos = new ArrayList<>();
        //**
        if(EsCorte) {
            presenter.usuariosCorte(session.getToken());
        }else if (EsAnticipo){
            presenter.usuarios(session.getToken());
        }
        TVAnticipoTablaActivityTituloUsuario = findViewById(R.id.
                TVAnticipoTablaActivityTituloUsuario);
        TVAnticipoTablaActivityTituloUsuario.setText(EsAnticipo? R.string.recibi_de:
        R.string.Recibe);
        // SPAnticipoTablaActvityUsuario.setVisibility(EsAnticipo? View.VISIBLE:View.GONE);
        // SPAnticipoTablaActvityUsuario.getText();

        UsuariosDTO usuario = usuariosDTO;
        usuario.setNombre(SPAnticipoTablaActvityUsuario.getText().toString());
        System.out.println(usuario.getNombre());


        if(EsAnticipo) {
            System.out.println("anticipo");
            usuario.setNombre(SPAnticipoTablaActvityUsuario.getText().toString());
            Log.d("AnticipoEditText", SPAnticipoTablaActvityUsuario.getText().toString());
            //anticiposDTO.setNombreEntrega(usuario.getNombre());
            //anticiposDTO.setIdEntrega(usuario.getIdUsuario());
            anticiposDTO.setIdRecibe(usuario.getIdUsuario());
            anticiposDTO.setRecibe(usuario.getNombre());
            System.out.println(usuario.getNombre());
        }else if (EsCorte){
            System.out.println("corte");
            corteDTO.setRecibe(usuario.getNombre());
            corteDTO.setIdRecibio(usuario.getIdUsuario());
            System.out.println(usuario.getNombre());
        }

               // new AdapterView.OnItemSelectedListener() {
           /* @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(position>=0) {
                    if (dataUsariosCorte.isExito()) {
                        if (dataUsariosCorte.getUsuarios().size() > 0 &&
                                !dataUsariosCorte.getUsuarios().isEmpty()) {
                            for (int x = 0; x < dataUsariosCorte.getUsuarios().size(); x++) {
                                if (dataUsariosCorte.getUsuarios().get(x).getNombre().equals(
                                        listUsuarios[position]
                                )) {
                                    // UsuariosDTO usuario = SPAnticipoTablaActvityUsuario.setText();
                                    UsuariosDTO usuario = dataUsariosCorte.getUsuarios().get(x);
                                    Log.d("usuarioanticipo",dataUsariosCorte.getUsuarios().get(x).toString());
                                    if(EsAnticipo) {
                                        //anticiposDTO.setNombreEntrega(usuario.getNombre());
                                        //anticiposDTO.setIdEntrega(usuario.getIdUsuario());
                                        anticiposDTO.setIdRecibe(usuario.getIdUsuario());
                                        anticiposDTO.setRecibe(usuario.getNombre());
                                        Log.d("getnombreanticipo", usuario.getNombre());
                                    }else if (EsCorte){
                                        corteDTO.setRecibe(usuario.getNombre());
                                        corteDTO.setIdRecibio(usuario.getIdUsuario());
                                    }
                                }
                            }
                        }
                    }
                }
            }*/

         /*   @Override
            public void onNothingSelected(AdapterView<?> parent) {
                if (EsAnticipo) {
                    anticiposDTO.setNombreEntrega("");
                    anticiposDTO.setIdEntrega(0);
                }else if(EsCorte){
                    corteDTO.setEntrega("");
                    corteDTO.setIdEntrega(0);
                }
            }
        };*/
        NumberFormat format = NumberFormat.getCurrencyInstance();
        session = new Session(this);
        sagasSql = new SAGASSql(this);
        TVAnticipoTablaActivityTotal.setText(format.format(total));
    }

    @Override
    public void VerificarCampos() {
        Log.d("CorteDTO", corteDTO.toString());
        if(hasFecha) {
            if (EsAnticipo) {
                AlertDialog.Builder builder = new AlertDialog.Builder(this, R.style.AlertDialog);
                if (ETAnticipoTablaActivityAnticipo.getText().toString().equals("")) {

                    builder.setTitle(R.string.error_titulo);
                    builder.setMessage("El total del anticipo es un valor requerido");
                    builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
                        dialog.dismiss();
                        ETAnticipoTablaActivityAnticipo.setFocusable(true);
                    }));
                    builder.create().show();
                } else {
                    String cantidad = ETAnticipoTablaActivityAnticipo.getText().toString();
                    if (Double.parseDouble(cantidad) <= 0) {
                        builder.setTitle(R.string.error_titulo);
                        builder.setMessage("El total del anticipo es un positivo requerido");
                        builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
                            dialog.dismiss();
                            ETAnticipoTablaActivityAnticipo.setFocusable(true);
                        }));
                        builder.create().show();
                    } else {
                        if(/*Double.parseDouble(cantidad)<total ||*/ Double.parseDouble(cantidad)>total) {
                            AlertDialog.Builder builderMonto = new AlertDialog.Builder(this,R.style.AlertDialog);
                            builderMonto.setCancelable(false);
                            builderMonto.setTitle(R.string.info);
                            builderMonto.setMessage("El monto ingresado debe de ser el igual al " +
                                    " monto total de las ventas no pude ser mayor");
                            builderMonto.setPositiveButton(R.string.message_acept,(dialogInterface, i) ->
                                    dialogInterface.dismiss());
                            builderMonto.create().show();
                        }else{
                            //if(datos.getAnticipos().size()>0) {
                                anticiposDTO.setAnticipar(Double.parseDouble(cantidad));
                                @SuppressLint("SimpleDateFormat") SimpleDateFormat f = new SimpleDateFormat(
                                        "yyyy-MM-dd'T'HH:mm:ss.SSSZ");
                                Date fecha = new Date();
                                anticiposDTO.setFecha(f.format(fecha));
                                anticiposDTO.setTotal(total);
                                SimpleDateFormat format = new SimpleDateFormat("HH:mm",
                                        Locale.getDefault());
                                String hour = format.format(new Date());
                                anticiposDTO.setHora(hour);
                                @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                                        new SimpleDateFormat("ddMMyyyyhhmmssS");
                                String clave_unica = "ANT" + s.format(new Date());
                                anticiposDTO.setClaveOperacion(clave_unica);
                                anticiposDTO.setTiket(clave_unica);
                                anticiposDTO.setRecibe(session.getAttribute(Session.KEY_NOMBRE));

                                //Agrego las ventas correspondientes al corte
                                presenter.Anticipo(anticiposDTO, sagasSql, session.getToken());
                            /*}else{
                                AlertDialog.Builder builderMonto = new AlertDialog.Builder(this,R.style.AlertDialog);
                                builderMonto.setCancelable(false);
                                builderMonto.setTitle(R.string.info);
                                builderMonto.setMessage("La fecha ingresada no tiene ventas para" +
                                        " realizar el anticipo");
                                builderMonto.setPositiveButton(R.string.message_acept,(dialogInterface, i) ->
                                        dialogInterface.dismiss());
                                builderMonto.create().show();
                            }*/
                        }
                    }
                }
            } else {

                    /*if(total==0) {*/
                        @SuppressLint("SimpleDateFormat") SimpleDateFormat s =
                                new SimpleDateFormat("ddMMyyyyhhmmssS");
                        SimpleDateFormat format = new SimpleDateFormat("HH:mm",
                                Locale.getDefault());
                        String hour = format.format(new Date());
                        corteDTO.setHora(hour);
                        String clave_unica = "CC" + s.format(new Date());
                        @SuppressLint("SimpleDateFormat") SimpleDateFormat f = new SimpleDateFormat(
                                "yyyy-MM-dd'T'HH:mm:ss.SSSZ");
                        corteDTO.setFecha(f.format(new Date()));
                        corteDTO.setFechaVenta(f.format(new Date()));
                        corteDTO.setClaveOperacion(clave_unica);
                        corteDTO.setTiket(clave_unica);
                        corteDTO.setEntrega(session.getAttribute(Session.KEY_NOMBRE));
                        int idSession =session.getAttributeInt(Session.KEY_ID_USUARIO);
                        corteDTO.setIdEntrega(idSession);
                        //Agrego las ventas correspondientes al corte
                        /*for (CorteDTO itemCorte : datos.getCortes()) {
                            VentasCorteDTO ventasCorteDTO = new VentasCorteDTO();
                            ventasCorteDTO.setCorte(clave_unica);
                            ventasCorteDTO.setTiketVenta(itemCorte.getTiket());
                            ventasCorteDTO.setIdVenta(itemCorte.getId());
                            corteDTO.getConceptos().add(ventasCorteDTO);
                        }*/
                        for (VentaDTO itemCorte: datosBusqueda.venta.getVentas()) {
                            VentasCorteDTO ventasCorteDTO = new VentasCorteDTO();
                            ventasCorteDTO.setCorte(clave_unica);
                            ventasCorteDTO.setTiketVenta(itemCorte.getFolioVenta());
                            ventasCorteDTO.setIdVenta(itemCorte.getIdCliente());
                            corteDTO.getConceptos().add(ventasCorteDTO);
                        }
                        //Agrego las ventas correspondientes al corte
                        if(EsCamioneta || EsPipa) {
                            presenter.Corte(corteDTO, sagasSql, session.getToken());
                        }else{
                            if(datosBusqueda.anticipo.getTotalAnticipos()<0) {
                                AlertDialog.Builder builderMonto = new AlertDialog.Builder(this, R.style.AlertDialog);
                                builderMonto.setCancelable(false);
                                builderMonto.setTitle(R.string.info);
                                builderMonto.setMessage("No se puede hacer un corte ya que faltan anticipos");
                                builderMonto.setPositiveButton(R.string.message_acept, (dialogInterface, i) ->
                                        dialogInterface.dismiss());
                                builderMonto.create().show();
                            }else{
                                presenter.Corte(corteDTO, sagasSql, session.getToken());
                            }
                        }

                    //startIntent();
                /*}else{
                    AlertDialog.Builder builderMonto = new AlertDialog.Builder(this,R.style.AlertDialog);
                    builderMonto.setCancelable(false);
                    builderMonto.setTitle(R.string.mensjae_error_campos);
                    builderMonto.setMessage("No se puede hacer un corte de esta fecha, no " +
                            "hay datos actualmente");
                    builderMonto.setPositiveButton(R.string.message_acept,(dialogInterface, i) ->
                            dialogInterface.dismiss());
                    builderMonto.create().show();
                }*/
            }
        }else{
            AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setCancelable(false);
            builder.setTitle(R.string.info);
            builder.setMessage(EsCorte?"Es necesario que especifiques la fecha de corte a realizar":
            "Es necesario que especifiques la fecha de anticipo a realizar");
            builder.setPositiveButton(R.string.message_acept,(dialogInterface, i) ->
                    dialogInterface.dismiss());
            builder.create().show();
        }
    }

    @Override
    public void onShowProgress(int message_cargando) {
        progressDialog = new ProgressDialog(this);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(message_cargando));
        progressDialog.setTitle(R.string.project_id);
        progressDialog.show();
    }

    @Override
    public void HiddeProgress() {
        if(progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onSuccess() {
        startIntent();
    }

    @Override
    public void onError(String mensaje) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(mensaje);
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
           dialogInterface.dismiss();

        });
        builder.create().show();
    }

    @Override
    public void onSuccessAndroid() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(R.string.mensaje_exito_papeleta_android);
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
            dialogInterface.dismiss();
            startIntent();
        });
        builder.create().show();
    }

    @Override
    public void onError(DatosBusquedaCortesDTO ob) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        builder.setMessage(ob.getMensajesError());
        builder.setCancelable(false);
        builder.setPositiveButton(R.string.message_acept, (dialogInterface, i) -> {
            dialogInterface.dismiss();

        });
        builder.create().show();
    }

    @SuppressLint("SetTextI18n")
    @Override
    public void onSuccessList(DatosBusquedaCortesDTO data) {
        if(data!=null){
            if(data.isExito()){
                datosBusqueda = data;
                if(data.isHayVentas()){

                    TLAnticipoTablaActivityTabla.removeAllViews();
                    elementos = new ArrayList<>();
                    DatosBusquedaCortesDTO.VentaInfoDTO venta =data.venta;
                    for (int x=0; x<venta.getVentas().size();x++){

                        try{
                            Date fecha = fdate.parse(
                                    venta.getVentas().get(x).getFecha()
                            );
                            elementos.add(new String[]{
                                    venta.getVentas().get(x).getFolioVenta(),
                                    sfdate.format(fecha),
                                    format.format(venta.getVentas().get(x).getTotal())
                            });
                        }catch (Exception ex){
                            ex.printStackTrace();
                        }
                    }
                    totalTabla = venta.getTotalVentas();
                    total = totalTabla;
                    TVAnticipoTablaActivityTotal.setText(format.format(totalTabla));
                }
                if(data.isHayCortes()){
                    totalCortes = data.corte.totalCortes;
                }
                if(data.isHayAnticipos()){
                    totalAnticipos = data.anticipo.getTotalAnticipos();
                }
                if(!EsAnticipo){//Es corte
                    if(EsPipa||EsCamioneta) {
                        TVAnticipoTableActivityAnticipos.setText("$(0.00)");
                        TVAnticipoTableMontoDeCorte.setText(
                                "$" + String.valueOf(
                                        dformat.format(
                                                totalTabla
                                        )
                                )
                        );
                        corteDTO.setMontoCorte(totalTabla);
                        corteDTO.setMonto(totalTabla);
                        corteDTO.setTotal(totalTabla);
                        corteDTO.setTotalAnticipos(0);
                        corteDTO.setAnticipos(0);
                    }else{
                        if(data.anticipo.getTotalAnticipos()>0) {
                            TVAnticipoTableActivityAnticipos.setText(
                                    "$" + String.valueOf(
                                            dformat.format(
                                                    data.anticipo.getAnticipos()
                                            )
                                    )
                            );
                        }else {
                            TVAnticipoTableActivityAnticipos.setText(
                                    "$(0.00)"
                            );
                        }
                        TVAnticipoTableMontoDeCorte.setText(
                                "$" + String.valueOf(
                                        dformat.format(
                                                totalCortes
                                        )
                                )
                        );
                        double calculoTotalMontoCorte = totalTabla - totalAnticipos;
                        corteDTO.setMontoCorte(calculoTotalMontoCorte);
                        corteDTO.setMonto(calculoTotalMontoCorte);
                        corteDTO.setTotal(totalTabla);
                        corteDTO.setTotalAnticipos(totalAnticipos);
                        corteDTO.setAnticipos(totalAnticipos);
                    }
                }
                if(!elementos.isEmpty()) {
                    tabla.agregarFila(elementos);
                }
            }else{// en caso de que no haya datos muestro lo que repsponde
                AlertDialog.Builder builder = new AlertDialog.Builder(this,
                        R.style.AlertDialog);
                builder.setTitle(R.string.error_titulo);
                builder.setMessage(data.getMensaje());
                builder.setPositiveButton(R.string.message_acept, (dialog, which) ->
                        dialog.dismiss());
                builder.create().show();
            }
        }
    }
    /*
    @Deprecated
    @Override
    public void onSuccessList(RespuestaEstacionesVentaDTO data) {
        NumberFormat format = NumberFormat.getCurrencyInstance();
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        @SuppressLint("SimpleDateFormat") SimpleDateFormat sfdate =
                new SimpleDateFormat("dd/MM/yyyy");
        datos = data;
        if(data!=null){
            TLAnticipoTablaActivityTabla.removeAllViews();
            elementos = new ArrayList<>();
            if (EsAnticipo){
                total=0;
                if(data.getAnticipos().size()>0){

                   for (int x=0;x<data.getAnticipos().size();x++){
                       try {
                           Date fecha = fdate.parse(
                                   data.getAnticipos().get(x).getFecha()
                           );
                           elementos.add(new String[]{
                                   data.getAnticipos().get(x).getTiket(), //"201809180785236"
                                   sfdate.format( fecha),//"18/09/2018"
                                   format.format(data.getAnticipos().get(x).getTotal())
                           });
                           total += data.getAnticipos().get(x).getTotal();
                       } catch (ParseException e) {
                           e.printStackTrace();
                       }
                   }
                   String valor = format.format(total);
                   TVAnticipoTablaActivityTotal.setText(valor);
                }
            }else{
                if(data.getCortes().size()>0){
                    total =0 ;
                    for (int x=0;x<data.getCortes().size();x++){
                        try {
                            Date fecha = fdate.parse(
                                    data.getCortes().get(x).getFecha()
                            );
                            elementos.add(new String[]{
                                    data.getCortes().get(x).getTiket(),//"201809180785236"
                                    sfdate.format( fecha),//"18/09/2018"
                                    format.format(data.getCortes().get(x).getTotal())//format.format(i*100.00)
                            });
                            total += data.getCortes().get(x).getTotal();
                        } catch (ParseException e) {
                            e.printStackTrace();
                        }
                    }
                    String valor = format.format(total);
                    TVAnticipoTablaActivityTotal.setText(valor);
                }
                corteDTO.setTotalAnticipos(data.getTotalAnticiposCorte());
                double montoTotal = total-corteDTO.getTotalAnticipos();
                String totalMonto = "$"+String.valueOf(dformat.format(montoTotal));
                if(EsPipa||EsCamioneta){
                    TVAnticipoTableActivityAnticipos.setText("$(0.00)");
                    TVAnticipoTableMontoDeCorte.setText(
                            "$"+String.valueOf(dformat.format(data.getTotalAnticiposCorte()))
                    );
                    corteDTO.setMontoCorte(total);
                    corteDTO.setMonto(total);
                    corteDTO.setTotal(total);
                    corteDTO.setTotalAnticipos(0);
                    corteDTO.setAnticipos(0);
                }else {
                    String totalAnticipo = "$(" + String.valueOf(
                            dformat.format(corteDTO.getTotalAnticipos())
                    ) + ")";
                    TVAnticipoTableActivityAnticipos.setText(totalAnticipo);
                    TVAnticipoTableMontoDeCorte.setText(
                            totalMonto
                    );
                    corteDTO.setMontoCorte(montoTotal);
                    corteDTO.setMonto(corteDTO.getTotalAnticipos());
                    corteDTO.setTotal(total);
                }
            }
            if(!elementos.isEmpty()) {
                tabla.agregarFila(elementos);
            }else{
                AlertDialog.Builder builder = new AlertDialog.Builder(this,
                        R.style.AlertDialog);
                builder.setTitle(R.string.title_alert_message);
                builder.setMessage("No se han encontrado información");
                builder.setPositiveButton(R.string.regresar, (dialogInterface, i) ->
                {dialogInterface.dismiss();});
                builder.create().show();
            }

        }
    }*/

    @Override
    public void onSuccessList(UsuariosCorteDTO data) {
        dataUsariosCorte = data;
        if(data!=null){
            if(data.isExito()){
                if(!data.getUsuarios().isEmpty() && data.getUsuarios().size()>0){
                    listUsuarios = new String[data.getUsuarios().size()];
                    for (int x = 0; x<data.getUsuarios().size();x++){
                        listUsuarios[x] = data.getUsuarios().get(x).getNombre();
                    }
//                    SPAnticipoTablaActvityUsuario.setAdapter(new ArrayAdapter<>(
//                            this,R.layout.custom_spinner,
//                            listUsuarios
//                    ));
                }else{
                    AlertDialog.Builder builder = new AlertDialog.Builder(this,
                            R.style.AlertDialog);
                    builder.setCancelable(false);
                    builder.setTitle(R.string.mensjae_error_campos);
                    builder.setTitle(data.getMensaje().length()>0? data.getMensaje():
                    "Se ha generado un error al obtener la lista de personas , revise su " +
                            "conexion de internet o intente nuevamente mas tarde ");
                    builder.setPositiveButton(R.string.message_acept, (dialog, which) ->
                            dialog.dismiss());
                    builder.create().show();
                }
            }
        }
    }

    private void startIntent(){
        if(EsAnticipo){
            Intent intent = new Intent(AnticipoTablaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsAnticipo",EsAnticipo);
            intent.putExtra("EsCorte",EsCorte);
            intent.putExtra("anticiposDTO",anticiposDTO);
            startActivity(intent);
        }else if(EsCorte){
            Intent intent = new Intent(AnticipoTablaActivity.this,
                    VerReporteActivity.class);
            intent.putExtra("EsAnticipo",EsAnticipo);
            intent.putExtra("EsCorte",EsCorte);
            intent.putExtra("corteDTO",corteDTO);
            startActivity(intent);
        }
    }

    public void setFecha(){
        if(fecha==null){
            fecha = new Date();
        }
        TVAnticipoTablaActivityFecha.setText(
                new StringBuilder()
                        // Month is 0 based so add 1
                        .append(mDay).append("/")
                        .append(mMonth + 1).append("/")
                        .append(mYear).append(" "));
        fecha.setDate(mDay);
        fecha.setMonth(mMonth+1);
        fecha.setYear(mYear);
        presenter.getAnticipos(
                session.getToken(),
                EsAnticipo? anticiposDTO.getIdEstacion():corteDTO.getIdEstacion(),
                EsAnticipo,
                String.valueOf(mYear)+"-"+String.valueOf(mMonth+ 1)+"-"+String.valueOf(mDay)
        );
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate =
                new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        String fecha_str = String.valueOf(mDay)+"-"+String.valueOf(mMonth+ 1)+"-"+
                String.valueOf(mYear);
        @SuppressLint("SimpleDateFormat") SimpleDateFormat dateFormat = new SimpleDateFormat("dd-MM-yyyy");
        try {
            Date d = dateFormat.parse(fecha_str);
            if(EsCorte)
                corteDTO.setFechaCorte(fdate.format(
                        d
                ));
            else
                anticiposDTO.setFechaAnticipo(fdate.format(d));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        hasFecha=true;
    }

    protected Dialog onCreateDialog(int id) {
        switch (id) {
            case 0:
                DatePickerDialog dialog = new DatePickerDialog(this,
                        R.style.datepicker,
                        onDateSetListener,
                        mYear, mMonth, mDay);
                dialog.getDatePicker().setMaxDate(System.currentTimeMillis());
                return  dialog;
        }
        return null;
    }
}
