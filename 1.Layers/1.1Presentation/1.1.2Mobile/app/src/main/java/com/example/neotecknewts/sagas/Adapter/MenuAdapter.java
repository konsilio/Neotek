package com.example.neotecknewts.sagas.Adapter;

import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagas.Activity.FinalizarDescargaActivity;
import com.example.neotecknewts.sagas.Activity.IniciarDescargaActivity;
import com.example.neotecknewts.sagas.R;

import java.util.List;
import java.util.Locale;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class MenuAdapter extends RecyclerView.Adapter<MenuAdapter.ViewHolder> {

    private List<String> menuItems;
    private int[] colores;
    Context context;

    public MenuAdapter(List<String> menuItems) {
        this.menuItems = menuItems;
        colores = new int[3];
        colores[0] = R.color.colorBackgroundMenu1;
        colores[1] = R.color.colorBackgroundMenu2;
        colores[2] = R.color.colorBackgroundMenu3;
    }

    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        this.context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        View contactView = inflater.inflate(R.layout.menu_list_item, parent, false);

        ViewHolder viewHolder = new ViewHolder(contactView);
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(ViewHolder holder, final int position) {
        String menuItem = menuItems.get(position);


        // Set item views based on your views and data model
        TextView textView = holder.nameTextView;
        textView.setText(menuItem);
        LinearLayout linearLayout = holder.linearLayout;
        if (position <= 2) {
            linearLayout.setBackgroundColor(context.getResources().getColor(colores[position]));
        } else {
            linearLayout.setBackgroundColor(context.getResources().getColor(colores[position - 3]));
        }

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                if(position==0) {
                    Intent intent = new Intent(view.getContext(), IniciarDescargaActivity.class);
                    view.getContext().startActivity(intent);

                }else if(position==1){
                        Intent intent = new Intent(view.getContext() , FinalizarDescargaActivity.class);
                        view.getContext().startActivity(intent);

                }

            }
        });

    }

    @Override
    public int getItemCount() {
        return menuItems.size();
    }


    public class ViewHolder extends RecyclerView.ViewHolder{
        public TextView nameTextView;
        public ImageView imageView;
        public LinearLayout linearLayout;

        public ViewHolder(View itemView) {

            super(itemView);

            linearLayout = (LinearLayout) itemView.findViewById(R.id.linearLayoutMenu);
            nameTextView = (TextView) itemView.findViewById(R.id.menu_item_name);
            imageView = (ImageView) itemView.findViewById(R.id.image_view_menu_item);
        }

    }
}



