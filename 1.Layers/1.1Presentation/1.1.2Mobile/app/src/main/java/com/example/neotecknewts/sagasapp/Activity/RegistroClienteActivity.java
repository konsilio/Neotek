package com.example.neotecknewts.sagasapp.Activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;

import com.example.neotecknewts.sagasapp.Model.ClienteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosTipoPersonaDTO;
import com.example.neotecknewts.sagasapp.Model.RegimenesDTO;
import com.example.neotecknewts.sagasapp.Model.RespuestaClienteDTO;
import com.example.neotecknewts.sagasapp.Model.TipoPersonaDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.Presenter.RegistroClientePresenter;
import com.example.neotecknewts.sagasapp.Presenter.RegistroClientePresenterImpl;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Session;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

public class RegistroClienteActivity extends AppCompatActivity implements RegistroClienteView{
    Spinner SRegistroClienteActivityTipoPersona,SRegistroClienteActivityRegimenFiscal;
    EditText ETRegistroClienteActivityNombre,ETRegistroClienteActivityApellidoPaterno,
            ETRegistroClienteActivityApellidoMaterno,ETRegistroClienteActivityCelular,
            ETRegistroClienteActivityTelefonoFijo,ETRegistroClienteActivityRazonSocial;
    Button BtnRegistroClienteActivityRegistrarCliente,BtnRegistroClienteActivityRegresar;
    EditText ETRegistroClienteRfc;
    boolean EsVentaCarburacion,EsVentaCamioneta,EsVentaPipa;
    boolean esGasLP;
    ProgressDialog progressDialog;
    RegistroClientePresenter presenter;
    Session session;
    DatosTipoPersonaDTO datos;
    ClienteDTO clienteDTO;
    VentaDTO ventaDTO;
    String[] lista_tipo_persona,lista_regimen_fiscal;
    TipoPersonaDTO tipoPersonaDTO_Seleccionada;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registro_cliente);
        Bundle bundle = getIntent().getExtras();
        if(bundle!=null){
            EsVentaCarburacion = bundle.getBoolean("EsVentaCarburacion",false);
            EsVentaCamioneta = bundle.getBoolean("EsVentaCamioneta",false);
            EsVentaPipa = bundle.getBoolean("EsVentaPipa",false);
            ventaDTO = (VentaDTO) bundle.getSerializable("ventaDTO");
            esGasLP = bundle.getBoolean("esGasLP",false);
        }
        session = new Session(this);
        clienteDTO = new ClienteDTO();
        SRegistroClienteActivityTipoPersona = findViewById(R.id.
                SRegistroClienteActivityTipoPersona);
        SRegistroClienteActivityRegimenFiscal = findViewById(R.id.
                SRegistroClienteActivityRegimenFiscal);
        ETRegistroClienteActivityNombre = findViewById(R.id.ETRegistroClienteActivityNombre);
        ETRegistroClienteActivityApellidoPaterno = findViewById(R.id.
                ETRegistroClienteActivityApellidoPaterno);
        ETRegistroClienteActivityApellidoMaterno = findViewById(R.id.
                ETRegistroClienteActivityApellidoMaterno);
        ETRegistroClienteActivityCelular = findViewById(R.id.ETRegistroClienteActivityCelular);
        ETRegistroClienteActivityTelefonoFijo = findViewById(R.id.
                ETRegistroClienteActivityTelefonoFijo);
        ETRegistroClienteActivityRazonSocial = findViewById(R.id.
                ETRegistroClienteActivityRazonSocial);
        BtnRegistroClienteActivityRegistrarCliente = findViewById(R.id.
                BtnRegistroClienteActivityRegistrarCliente);
        BtnRegistroClienteActivityRegresar = findViewById(R.id.BtnRegistroClienteActivityRegresar);
        ETRegistroClienteRfc = findViewById(R.id.ETRegistroClienteRfc);

        SRegistroClienteActivityTipoPersona.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                getResources().getStringArray(R.array.Tipo_persona)
        ));

        SRegistroClienteActivityRegimenFiscal.setAdapter(new ArrayAdapter<>(
                this,
                R.layout.custom_spinner,
                getResources().getStringArray(R.array.Regimen_fiscal)
        ));

        SRegistroClienteActivityTipoPersona.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(datos!=null) {

                    Log.w("Persona", parent.getItemAtPosition(position).toString());
                    ETRegistroClienteActivityRazonSocial.setVisibility(
                            parent.getItemAtPosition(position).toString().equals("Moral") ?
                                    View.VISIBLE : View.GONE
                    );
                    for (TipoPersonaDTO tipo:
                         datos.getTipoPersona()) {
                        if(tipo.getDescripcion().equals(
                                parent.getItemAtPosition(position).toString()
                        )){
                            clienteDTO.setIdTipoPersona(tipo.getIdTipoPersona());
                            tipoPersonaDTO_Seleccionada = tipo;
                            colocarRegimen(tipo,parent.getItemAtPosition(position).toString().equals("Moral"));
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                clienteDTO.setIdTipoPersona(0);
            }
        });
        SRegistroClienteActivityRegimenFiscal.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if(datos!=null) {
                    if(position>=0) {
                        if(tipoPersonaDTO_Seleccionada.getRegimenes().size()>0
                                ) {
                            for (RegimenesDTO regimenDTO : tipoPersonaDTO_Seleccionada.getRegimenes()) {
                                if (regimenDTO.getDescripcion().equals(parent.getItemAtPosition(position).toString())) {
                                    clienteDTO.setIdTipoRegimen(regimenDTO.getIdRegimenFiscal());
                                }
                            }
                        }
                    }
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                clienteDTO.setIdTipoRegimen(0);
            }
        });
        presenter = new RegistroClientePresenterImpl(this);
        presenter.getLista(session.getToken());
        BtnRegistroClienteActivityRegistrarCliente.setOnClickListener(v -> verificarForm());
        BtnRegistroClienteActivityRegresar.setOnClickListener(v -> {
            Intent intent =  new Intent(RegistroClienteActivity.this,PuntoVentaSolicitarActivity.class);
            intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("esGasLP",esGasLP);
            startActivity(intent);
            finish();
        });
    }

    private void colocarRegimen(TipoPersonaDTO tipo,boolean esMoral) {
        if(tipo.getRegimenes().size()>0 && esMoral){
            lista_regimen_fiscal = new String[tipo.getRegimenes().size()];
            for (int x = 0;x<tipo.getRegimenes().size();x++) {
                lista_regimen_fiscal[x] = tipo.getRegimenes().get(x).getDescripcion();
            }
            SRegistroClienteActivityRegimenFiscal.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_regimen_fiscal
            ));
        }
        if(!esMoral){
            lista_regimen_fiscal = new String[tipo.getRegimenes().size()];
            for (int x = 0;x<tipo.getRegimenes().size();x++) {
                lista_regimen_fiscal[x] = tipo.getRegimenes().get(x).getDescripcion();
            }
            SRegistroClienteActivityRegimenFiscal.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_regimen_fiscal
            ));
        }
    }

    @Override
    public void registrarCliente() {
        clienteDTO.setNombre(ETRegistroClienteActivityNombre.getText().toString());
        clienteDTO.setApellido_uno(ETRegistroClienteActivityApellidoPaterno.getText().toString());
        clienteDTO.setApellido_dos(ETRegistroClienteActivityApellidoMaterno.getText().toString());
        clienteDTO.setCelular(ETRegistroClienteActivityCelular.getText().toString());
        clienteDTO.setTelefono_fijo(ETRegistroClienteActivityTelefonoFijo.getText().toString());
        clienteDTO.setRazonSocial(ETRegistroClienteActivityRazonSocial.getText().toString());
        clienteDTO.setRFC(ETRegistroClienteRfc.getText().toString());
        presenter.registrarCliente(clienteDTO,session.getToken());
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
    public void onHideProgress() {
        if (progressDialog!= null && progressDialog.isShowing()){
            progressDialog.hide();
            progressDialog.dismiss();
        }
    }

    @Override
    public void onError(DatosTipoPersonaDTO dto) {
        datos = dto;
        String mensaje  = "";
        if(dto!=null) {
            if (datos.getMensajesError().length() > 0) {
                mensaje = datos.getMensaje();
            }
            if (datos.getMensajesError().length() > 0) {
                mensaje = datos.getMensaje();
            }
            AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(getString(R.string.error_titulo));
            builder.setMessage(mensaje);
            builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
                dialog.dismiss();
            }));
            builder.create();
            builder.show();
        }else{
            AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
            builder.setTitle(getString(R.string.error_titulo));
            builder.setMessage("No se ha obtenido respuesta con el servidor, verifique su conexión");
            builder.setPositiveButton(R.string.message_acept, ((dialog, which) -> {
                dialog.dismiss();
            }));
            builder.create();
            builder.show();
        }
    }

    @Override
    public void onSuccessDatosFiscales(DatosTipoPersonaDTO dto) {
        datos = dto;

        if(datos.getTipoPersona().size()>0){
            int size = datos.getTipoPersona().size();
            lista_tipo_persona = new String[size];
            for (int x= 0;x<datos.getTipoPersona().size();x++){
                lista_tipo_persona[x] = datos.getTipoPersona().get(x).getDescripcion();
            }
            SRegistroClienteActivityTipoPersona.setAdapter(new ArrayAdapter<>(
                    this,
                    R.layout.custom_spinner,
                    lista_tipo_persona
            ));
        }
    }

    @Override
    public void verificarForm() {
        boolean error = false;
        ArrayList<String> mensajes = new ArrayList<>();

        if(SRegistroClienteActivityTipoPersona.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El tipo de persona es un valor requerido");
        }

        if(SRegistroClienteActivityRegimenFiscal.getSelectedItemPosition()<0){
            error = true;
            mensajes.add("El regiment fiscal es un valor requerido");
        }

        if(SRegistroClienteActivityTipoPersona.getSelectedItemPosition()==1){
            if(ETRegistroClienteActivityRazonSocial.getText().length()<=0 ||
                    ETRegistroClienteActivityRazonSocial.getText().toString().isEmpty()){
                error = true;
                mensajes.add("Es necesario especificar la razon social");
            }
        }

        if(ETRegistroClienteActivityNombre.getText().toString().isEmpty() ||
                ETRegistroClienteActivityNombre.getText().length()<=0){
            error = true;
            mensajes.add("El nombre de la persona es un valor requerido");
        }

        if(ETRegistroClienteActivityApellidoPaterno.getText().toString().isEmpty()
            ||ETRegistroClienteActivityApellidoPaterno.getText().length()<=0){
            error = true;
            mensajes.add("El primer apellido es un valor requerido");
        }

        if(ETRegistroClienteActivityCelular.getText().toString().isEmpty()||
                ETRegistroClienteActivityCelular.getText().length()<=0){
            error = true;
            mensajes.add("Es necesario un teléfono de celular");
        }

        if(ETRegistroClienteActivityTelefonoFijo.getText().toString().isEmpty()||
                ETRegistroClienteActivityTelefonoFijo.getText().length()<=0){
            error = true;
            mensajes.add("Es necesario un número de teléfono fijo");
        }
        
        if(error){
            mostrarDialogoErrores(mensajes);
        }else{
            registrarCliente();
        }
    }

    @Override
    public void mostrarDialogoErrores(ArrayList<String> mensajes) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(R.string.error_titulo);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append(getString(R.string.mensjae_error_campos)).append("\n");
        for (String mensaje : mensajes) {
            stringBuilder.append(mensaje).append("\n");
        }
        builder.setMessage(stringBuilder);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
            SRegistroClienteActivityRegimenFiscal.setFocusable(true);
        }));
        builder.create().show();
    }

    @Override
    public void onError(String message) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(getString(R.string.error_titulo));
        builder.setMessage(message);
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
        }));
        builder.create();
        builder.show();
    }

    @Override
    public void onErrorRegistro(RespuestaClienteDTO data) {
        AlertDialog.Builder builder = new AlertDialog.Builder(this,R.style.AlertDialog);
        builder.setTitle(getString(R.string.error_titulo));
        if(data!=null) {
            builder.setMessage(data.getMensaje());
        }else{
            builder.setMessage("No se ha podido realizar el registro,intente nuevamente");
        }
        builder.setPositiveButton(R.string.message_acept,((dialog, which) -> {
            dialog.dismiss();
        }));
        builder.create();
        builder.show();
    }

    @Override
    public void setIdCliente(RespuestaClienteDTO data) {
        clienteDTO.setIdCliente(data.getId());
        ventaDTO.setIdCliente(data.getId());
        ventaDTO.setNombre(clienteDTO.getNombre()+" "+clienteDTO.getApellido_uno()+
                ""+clienteDTO.getApellido_dos());
        ventaDTO.setCredito(false);
        ventaDTO.setRFC(clienteDTO.getRFC());
        ventaDTO.setSinNumero(false);
        ventaDTO.setRazonSocial(clienteDTO.getRazonSocial());
        if(EsVentaCamioneta) {
            Intent intent = new Intent(RegistroClienteActivity.this,
                    VentaGasActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("esGasLP",esGasLP);
            startActivity(intent);
        }else if(EsVentaCarburacion){
            Intent intent = new Intent(RegistroClienteActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("esGasLP",esGasLP);
            startActivity(intent);

        }else  if (EsVentaPipa){
            Intent intent = new Intent(RegistroClienteActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion", EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta", EsVentaCamioneta);
            intent.putExtra("EsVentaPipa", EsVentaPipa);
            intent.putExtra("ventaDTO", ventaDTO);
            intent.putExtra("esGasLP",esGasLP);
            startActivity(intent);
        }
    }
}
