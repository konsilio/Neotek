package com.example.neotecknewts.sagas.Activity;


import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.MenuItem;

import com.example.neotecknewts.sagas.Adapter.MenuAdapter;
import com.example.neotecknewts.sagas.R;

import java.util.ArrayList;

/**
 * Created by neotecknewts on 02/08/18.
 */

public class MenuActivity extends AppCompatActivity {

    ArrayList<String> menu;

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main_menu);

        RecyclerView recyclerView = findViewById(R.id.recyclerView);

        menu = new ArrayList<>();
        menu.add("Iniciar Descarga");
        menu.add("Finalizar Descarga");



        MenuAdapter adapter = new MenuAdapter(menu);
        recyclerView.setAdapter(adapter);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),DividerItemDecoration.HORIZONTAL);
        recyclerView.addItemDecoration(dividerItemDecoration);




    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                this.finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }

    }
}
