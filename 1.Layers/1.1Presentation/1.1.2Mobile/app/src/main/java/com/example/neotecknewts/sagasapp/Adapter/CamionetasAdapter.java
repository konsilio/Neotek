package com.example.neotecknewts.sagasapp.Adapter;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Model.CilindrosDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;
import java.util.List;

public class CamionetasAdapter  extends RecyclerView.Adapter<RecyclerView.ViewHolder>{
    private List<CilindrosDTO> items;

    public CamionetasAdapter(ArrayList<CilindrosDTO> cilindrosDTOS){
        this.items = cilindrosDTOS;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
           View view = LayoutInflater.from(parent.getContext()).inflate(
                   R.layout.campos_camionetas,parent,false);
           return  new CamionetasHolder(view);
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder viewHolder, final int position){
        ((CamionetasHolder) viewHolder).ETConfiguracionCamionetasCantidad.setText(
                String.valueOf(items.get(position).getCantidad())
        );
        ((CamionetasHolder) viewHolder).TVCamposCamionetasTipoCilindro.setText(
                items.get(position).getCilindroKg()
        );
    }

    @Override
    public int getItemCount(){
        return this.items.size();
    }

    public class  CamionetasHolder extends RecyclerView.ViewHolder{
        public TextView TVCamposCamionetasTipoCilindro;
        public EditText ETConfiguracionCamionetasCantidad;
        CamionetasHolder(View v){
            super(v);
            TVCamposCamionetasTipoCilindro =  v.findViewById(R.id.TVCamposCamionetasTipoCilindro);
            ETConfiguracionCamionetasCantidad = v.findViewById(R.id.ETConfiguracionCamionetasCantidad);
        }
    }

    public CilindrosDTO getCilindro(int  position){
        return this.items.get(position);
    }
}
