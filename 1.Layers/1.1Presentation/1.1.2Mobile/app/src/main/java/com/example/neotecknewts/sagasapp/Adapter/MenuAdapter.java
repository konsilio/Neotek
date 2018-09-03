package com.example.neotecknewts.sagasapp.Adapter;

import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.example.neotecknewts.sagasapp.Activity.FinalizarDescargaActivity;
import com.example.neotecknewts.sagasapp.Activity.IniciarDescargaActivity;
import com.example.neotecknewts.sagasapp.Activity.LecturaDatosActivity;
import com.example.neotecknewts.sagasapp.Activity.LecturaPipaActivity;
import com.example.neotecknewts.sagasapp.Activity.RegistrarPapeletaActivity;
import com.example.neotecknewts.sagasapp.Activity.VistaOrdenCompraActivity;
import com.example.neotecknewts.sagasapp.Model.MenuDTO;
import com.example.neotecknewts.sagasapp.R;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class MenuAdapter extends RecyclerView.Adapter<MenuAdapter.ViewHolder> {

    //lista con la que se llenara la vista
    private ArrayList<MenuDTO> menuItems;
    private int[] colores;
    //contexto de la aplicacipon
    Context context;

    //constructor que recibe la lista y la asinga a la de esta clase
    public MenuAdapter(ArrayList<MenuDTO> menuItems) {
        this.menuItems = menuItems;
        colores = new int[3];
        colores[0] = R.color.colorBackgroundMenu1;
        colores[1] = R.color.colorBackgroundMenu2;
        colores[2] = R.color.colorBackgroundMenu3;
    }

    //metodo que infla la vista, se seleciona que layout sera el que se repetira por cada elemento de la vista
    @Override
    public ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        this.context = parent.getContext();
        LayoutInflater inflater = LayoutInflater.from(context);
        View contactView = inflater.inflate(R.layout.menu_list_item, parent, false);

        ViewHolder viewHolder = new ViewHolder(contactView);
        return viewHolder;
    }

    //por cada elemento de la lista la vista se llenara de la siguiente manera
    @Override
    public void onBindViewHolder(ViewHolder holder, final int position) {
        final MenuDTO menuItem = menuItems.get(position);

//se cargan los datos del item de la lista a la vista
        TextView textView = holder.nameTextView;
        textView.setText(menuItem.getName());
        TextView textViewruta = holder.textViewRuta;
        textViewruta.setText(menuItem.getHeaderMenu());
        LinearLayout linearLayout = holder.linearLayout;

        //se declara el comportamiento al recibir el click y las vistas que se deben iniciar
        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(menuItem.getName().equals("Iniciar Descarga")) {
                    Intent intent = new Intent(view.getContext(), IniciarDescargaActivity.class);
                    view.getContext().startActivity(intent);

                }else if(menuItem.getName().equals("Finalizar Descarga")){
                    Intent intent = new Intent(view.getContext() , FinalizarDescargaActivity.class);
                    view.getContext().startActivity(intent);

                }else if(menuItem.getName().equals("Registrar Papeleta")){
                    Intent intent = new Intent(view.getContext() , RegistrarPapeletaActivity.class);
                    view.getContext().startActivity(intent);
                }
                else if(menuItem.getName().equals("Ordenes de compra")){
                    Intent intent = new Intent(view.getContext() , VistaOrdenCompraActivity.class);
                    view.getContext().startActivity(intent);
                }
                else if(menuItem.getName().equals("Estación de carb. (Inicial)")){
                    Intent intent = new Intent(view.getContext() , LecturaDatosActivity.class);
                    intent.putExtra("EsLecturaInicial",true);
                    intent.putExtra("EsLecturaFinal",false);
                    view.getContext().startActivity(intent);
                }
                else if(menuItem.getName().equals("Estación de carb. (Final)")){
                    Intent intent = new Intent(view.getContext() , LecturaDatosActivity.class);
                    intent.putExtra("EsLecturaInicial",false);
                    intent.putExtra("EsLecturaFinal",true);
                    view.getContext().startActivity(intent);
                }else if(menuItem.getName().equals("Pipa (Inicial)")){
                    Intent intent = new Intent(view.getContext(),LecturaPipaActivity.class);
                    intent.putExtra("EsLecturaInicialPipa",true);
                    intent.putExtra("EsLecturaFinalPipa",false);
                    view.getContext().startActivity(intent);
                }else if (menuItem.getName().equals("Pipa (Final)")){
                    Intent intent = new Intent(view.getContext(),LecturaPipaActivity.class);
                    intent.putExtra("EsLecturaInicialPipa",false);
                    intent.putExtra("EsLecturaFinalPipa",true);
                    view.getContext().startActivity(intent);
                }

            }
        });

    }

    @Override
    public int getItemCount() {
        return menuItems.size();
    }


    //se declaran las vistas que se tienen en cada elemento a repetir y se obtienen de la vista
    public class ViewHolder extends RecyclerView.ViewHolder{
        public TextView nameTextView;
        public ImageView imageView;
        public LinearLayout linearLayout;
        public TextView textViewRuta;

        public ViewHolder(View itemView) {

            super(itemView);

            textViewRuta = (TextView) itemView.findViewById(R.id.textViewRuta);
            linearLayout = (LinearLayout) itemView.findViewById(R.id.linearLayoutMenu);
            nameTextView = (TextView) itemView.findViewById(R.id.menu_item_name);
            imageView = (ImageView) itemView.findViewById(R.id.image_view_menu_item);
        }

    }
}



