package com.neotecknewts.sagasapp.Presenter.Rest;

import com.google.gson.FieldNamingPolicy;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class ApiClient {

    public  static final String  BASE_URL = "http://97.74.232.9:7012/api/"; //Nube
    //public static final String BASE_URL = "http://sagasapi.ddns.net:7012/api/"; // QA
    // public static final String BASE_URL = "http://sagasapi.ddns.net:7011/api/";//DEV

    private static Retrofit retrofit = null;

    public static Retrofit getClient() {
        if(retrofit == null) {

            Gson gson = new GsonBuilder()
                    .setFieldNamingPolicy(FieldNamingPolicy.LOWER_CASE_WITH_UNDERSCORES)
                    .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                    .create();

            retrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create(gson))
                    .client(new OkHttpClient())
                    .build();
        }
        return retrofit;
    }
}
