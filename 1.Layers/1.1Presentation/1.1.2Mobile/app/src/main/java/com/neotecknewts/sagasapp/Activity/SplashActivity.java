package com.neotecknewts.sagasapp.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.AppCompatActivity;

import com.neotecknewts.sagasapp.R;
import com.neotecknewts.sagasapp.Util.Session;

public class SplashActivity extends AppCompatActivity {
    Session session;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
        int secondsDelayed = 1;
        session = new Session(this);
        new Handler().postDelayed(new Runnable() {
            public void run() {
                if(session!=null) {
                    if ( !session.isExpired() && session.isLogin()) {
                        Intent intent = new Intent(SplashActivity.this, MenuActivity.class);
                        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                        startActivity(intent);
                    } else {
                        Intent intent = new Intent(SplashActivity.this, MainActivity.class);
                        intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                        startActivity(intent);
                    }
                }else{
                    Intent intent = new Intent(SplashActivity.this, MainActivity.class);
                    intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
                    startActivity(intent);
                }
                finish();

            }
        }, secondsDelayed * 3000);
    }
}
