package com.neotecknewts.sagasapp;

/**
 * Created by neotecknewts on 08/08/18.
 */

import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.graphics.BitmapFactory;
import android.media.RingtoneManager;
import android.net.Uri;
import android.support.v4.app.NotificationCompat;
import android.util.Log;

import com.neotecknewts.sagasapp.R;
import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;
import com.neotecknewts.sagasapp.Activity.VistaOrdenCompraActivity;


public class MyFirebaseMessagingService extends FirebaseMessagingService {

    private static final String TAG = "MyFirebaseMsgService";

    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {
        //Displaying data in log
        //It is optional
        Log.w(TAG, "From: " + remoteMessage.getFrom());
        Log.w(TAG, "Notification Message Body: " + remoteMessage.getNotification().getBody());
        String tipo = remoteMessage.getData().get("Tipo");
        Log.w("Tipo",remoteMessage.getData().toString());
        //Calling method to generate notification
        if(tipo.equals("O")) {
            sendNotificationOrden(remoteMessage);
        }
        else {
            sendNotification(remoteMessage.getNotification().getBody());
        }
    }

    //This method is only generating push notification
    //It is same as we did in earlier posts
    private void sendNotification(String messageBody) {

        Log.w(TAG, "PRUEBA");
        Intent intent = new Intent(this, VistaOrdenCompraActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        PendingIntent pendingIntent = PendingIntent.getActivity(this, 0, intent,
                PendingIntent.FLAG_ONE_SHOT);

        Uri defaultSoundUri= RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
        NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this)
                .setSmallIcon(R.mipmap.ic_launcher)
                .setContentTitle("Firebase Push Notification")
                .setContentText(messageBody)
                .setAutoCancel(true)
                .setSound(defaultSoundUri)
                .setContentIntent(pendingIntent);

        NotificationManager notificationManager =
                (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);

        notificationManager.notify(0, notificationBuilder.build());
    }

    /**
     * sendNotificationOrden
     * Envia la notificación push al telefono en caso de
     * que el mensaje sea de contenido de orden, se tomara como
     * parametro el objeto {@link RemoteMessage} de la notificación
     * enviada por Firebase del cual, se tomaran los parametros extra
     * del mismo.
     * @param remoteMessage Objeto de tipo {@link RemoteMessage} con los datos de la
     *                      notificacion push en Firebase
     * @author Jorge Omar Tovar Martínez    <jorge.tovar@neoteck.com.mx>
     * @date 08/08/18
     * @updated 08/08/18
     *
     */
    private void sendNotificationOrden(RemoteMessage remoteMessage) {
        Intent intent = new Intent(this, VistaOrdenCompraActivity.class);
        intent.addFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);
        intent.putExtra("OrderId",remoteMessage.getData().get("OrderId"));
        PendingIntent pendingIntent = PendingIntent.getActivity(this,0, intent,PendingIntent.FLAG_ONE_SHOT);
        Uri defaultSoundUri = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
        NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                .setSmallIcon(R.drawable.logo)
                .setLargeIcon(BitmapFactory.decodeResource(this.getResources(), R.drawable.logo))
                .setContentTitle("Orden de compra")
                .setContentText(remoteMessage.getData().get("OrderNo"))
                .setAutoCancel(true)
                .setSound(defaultSoundUri)
                .setContentIntent(pendingIntent);

        NotificationManager notificationManager =
                (NotificationManager) getSystemService(Context.NOTIFICATION_SERVICE);
        notificationManager.notify(1,builder.build());
    }

}
