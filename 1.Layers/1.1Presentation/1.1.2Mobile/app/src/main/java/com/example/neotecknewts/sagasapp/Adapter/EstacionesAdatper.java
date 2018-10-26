/**
 * EstacionesAdatper
 * Clase adapter para el listado de estaciónes
 * @author Jorge Omar Tovar Martínez <jorge.tovar@neoteck.com.mx/>
 * @date 27/09/2018
 * @update 27/09/2018
 */
package com.example.neotecknewts.sagasapp.Adapter;


import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Activity.AnticipoTablaActivity;
import com.example.neotecknewts.sagasapp.Model.AnticiposDTO;
import com.example.neotecknewts.sagasapp.Model.CorteDTO;
import com.example.neotecknewts.sagasapp.Model.DatosEstacionesDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.List;

public class EstacionesAdatper extends RecyclerView.Adapter<RecyclerView.ViewHolder> {
    private static final int TYPE_HEADER = 0;
    private static final int TYPE_ITEM = 1;
    private List<DatosEstacionesDTO> itemObjects;
    private Context context;
    private boolean EsAnticipo,EsCorte;
    private AnticiposDTO anticiposDTO;
    private CorteDTO corteDTO;

    public EstacionesAdatper(Context activity,List<DatosEstacionesDTO> itemObjects,
                             boolean EsAnticipo,boolean EsCorte){
        this.itemObjects = itemObjects;
        this.context = activity;
        this.EsAnticipo = EsAnticipo;
        this.EsCorte = EsCorte;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        if(viewType == TYPE_HEADER){
            View  hlayoutView = LayoutInflater.from(parent.getContext()).
                    inflate(R.layout.header_estacion,parent,false);
            return new HeaderViewHolder(hlayoutView);
        }else if(viewType == TYPE_ITEM){
            View  hlayoutView = LayoutInflater.from(parent.getContext()).
                    inflate(R.layout.item_estacion,parent,false);
            return new ItemViewHolder(hlayoutView);
        }
        throw new RuntimeException("No hay items "+viewType+".");
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, int position) {
        final DatosEstacionesDTO mObject = getItem(position);
        anticiposDTO = new AnticiposDTO();
        corteDTO = new CorteDTO();
        if(EsAnticipo) {

            anticiposDTO.setIdEstacion(mObject.getIdCAlmacenGas());
            anticiposDTO.setNombreEstacion(mObject.getNombreCAlmacen());
        }else if(EsCorte){
            corteDTO.setIdEstacion(mObject.getIdCAlmacenGas());
            corteDTO.setNombreEstacion(mObject.getNombreCAlmacen());
        }

        if(holder instanceof HeaderViewHolder){
            ((HeaderViewHolder)holder).header.setText(mObject.getNombreCAlmacen());
            ((HeaderViewHolder) holder).title.setText((EsCorte) ?
                    context.getString(R.string.corte_de_caja):
                    context.getString(R.string.Anticipo));
        }else if (holder instanceof ItemViewHolder){
            ((ItemViewHolder)holder).item.setText(mObject.getNombreCAlmacen());
            ((ItemViewHolder)holder).cardView.setOnClickListener(v -> {
                Intent intent = new Intent(v.getContext(),AnticipoTablaActivity.class);
                //intent.putExtra("IdCAlmacenGas",mObject.getIdCAlmacenGas());
                intent.putExtra("EsAnticipo",EsAnticipo);
                intent.putExtra("EsCorte",EsCorte);
                intent.putExtra("anticiposDTO",anticiposDTO);
                intent.putExtra("corteDTO",corteDTO);
                v.getContext().startActivity(intent);
            });
        }
    }

    private DatosEstacionesDTO getItem(int position){
        return itemObjects.get(position);
    }

    @Override
    public int getItemCount() {
        return itemObjects.size();
    }
    public int getItemViewType(int position){
        if(isPositionHeader(position))
            return TYPE_HEADER;
        return TYPE_ITEM;
    }
    private boolean isPositionHeader(int position){
        return position == 0;
    }

    private class HeaderViewHolder extends RecyclerView.ViewHolder {
        TextView header,title;
        HeaderViewHolder(View view) {
            super(view);
            title = itemView.findViewById(R.id.EstacionesCarburacionHeaderTitle);
            header = itemView.findViewById(R.id.EstacionesCarburacionHeaderInstructions);
        }
    }
    private class ItemViewHolder extends RecyclerView.ViewHolder {
        TextView item;
        CardView cardView;
        ItemViewHolder(View view) {
            super(view);
            item = itemView.findViewById(R.id.TVEstacionesCarburacionItem);
            cardView = itemView.findViewById(R.id.CVEstacionesCarburacionItem);
        }
    }
}
