package com.example.neotecknewts.sagasapp.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.widget.Button;
import android.widget.TableLayout;

import com.example.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.example.neotecknewts.sagasapp.Model.VentaDTO;
import com.example.neotecknewts.sagasapp.R;
import com.example.neotecknewts.sagasapp.Util.Tabla;

import java.text.NumberFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class VentaGasActivity extends AppCompatActivity implements VentaGasActivityView{
    Button BtnVentaGasActivityGasLp,BtnVentaGasActivtiyCilindroConGas,
            BtnVentaGasActivityCilindro,BtnVentaGasActivityOtros,BtnVentagGasActivtyPagar;
    VentaDTO ventaDTO;
    TableLayout TBVentaGasActivtyTabla;
    boolean EsVentaCarburacion,EsVentaCamioneta,EsVentaPipa;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_venta_gas);
        Bundle extras = getIntent().getExtras();
        if(extras!=null){
            ventaDTO = (VentaDTO) extras.getSerializable("ventaDTO");
            EsVentaCamioneta = extras.getBoolean("EsVentaCamioneta",false);
            EsVentaCarburacion = extras.getBoolean("EsVentaCarburacion",false);
            EsVentaPipa = extras.getBoolean("EsVentaPipa",false);
            ventaDTO.setFecha(new Date().toString());
        }

        BtnVentaGasActivityGasLp = findViewById(R.id.BtnVentaGasActivityGasLp);
        BtnVentaGasActivtiyCilindroConGas = findViewById(R.id.BtnVentaGasActivtiyCilindroConGas);
        BtnVentaGasActivityCilindro = findViewById(R.id.BtnVentaGasActivityCilindro);
        BtnVentaGasActivityOtros = findViewById(R.id.BtnVentaGasActivityOtros);
        BtnVentagGasActivtyPagar = findViewById(R.id.BtnVentagGasActivtyPagar);
        TBVentaGasActivtyTabla = findViewById(R.id.TBVentaGasActivtyTabla);

        BtnVentaGasActivityGasLp.setOnClickListener(v->{
            Intent intent = new Intent(VentaGasActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("esGasLP",true);
            intent.putExtra("esCilindroGas",false);
            intent.putExtra("esCilindro",false);
            startActivity(intent);
            //intent.putExtra()
        });
        BtnVentaGasActivtiyCilindroConGas.setOnClickListener(v->{
            Intent intent = new Intent(VentaGasActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("esGasLP",false);
            intent.putExtra("esCilindroGas",true);
            intent.putExtra("esCilindro",false);
            startActivity(intent);
        });
        BtnVentaGasActivityCilindro.setOnClickListener(v->{
            Intent intent = new Intent(VentaGasActivity.this,
                    PuntoVentaGasListaActivity.class);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            intent.putExtra("esGasLP",false);
            intent.putExtra("esCilindroGas",false);
            intent.putExtra("esCilindro",true);
            startActivity(intent);
        });

        BtnVentaGasActivityOtros.setOnClickListener(v->{
            Intent intent = new Intent(VentaGasActivity.this,
                    PuntoVentaOtrosActivity.class);
            intent.putExtra("EsVentaCarburacion",EsVentaCarburacion);
            intent.putExtra("EsVentaCamioneta",EsVentaCamioneta);
            intent.putExtra("EsVentaPipa",EsVentaPipa);
            intent.putExtra("ventaDTO",ventaDTO);
            startActivity(intent);
        });
        BtnVentagGasActivtyPagar.setOnClickListener(v->{
            if(ventaDTO.getConcepto().size()>0) {
                Intent intent = new Intent(VentaGasActivity.this,
                        PuntoVentaPagarActivity.class);
                intent.putExtra("ventaDTO", ventaDTO);
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
        mostrarConcepto(ventaDTO.getConcepto());
    }

    @Override
    public void onSuccess() {

    }

    @Override
    public void onError(String mensaje) {

    }

    @Override
    public void onShowProgress(int mensaje) {

    }

    @Override
    public void onHiddeProgress() {

    }

    @Override
    public void mostrarConcepto(List<ConceptoDTO> list) {
        Tabla tabla = new Tabla(this,TBVentaGasActivtyTabla);
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
