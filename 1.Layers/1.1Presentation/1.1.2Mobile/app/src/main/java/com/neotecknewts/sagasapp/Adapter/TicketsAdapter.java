package com.neotecknewts.sagasapp.Adapter;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.neotecknewts.sagasapp.Activity.VerReporteActivity;
import com.neotecknewts.sagasapp.Model.ConceptoDTO;
import com.neotecknewts.sagasapp.Model.VentaDTO;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import com.neotecknewts.sagasapp.R;

public class TicketsAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

    private List<VentaDTO> items;
    private String nombre;

    public TicketsAdapter(List<VentaDTO> items, String nombre) {
        this.items = items;
        this.nombre = nombre;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.ticket_item,parent,false);
        return new TicketsHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerView.ViewHolder holder, int position) {
        ((TicketsHolder) holder).TVHora.setText(items.get(position).getHora());
        ((TicketsHolder) holder).TVFolio.setText(items.get(position).getFolioVenta());
        ((TicketsHolder) holder).TVCliente.setText(items.get(position).getCliente());
        ((TicketsHolder) holder).TVVenta.setText(items.get(position).getTotal()+"");

        if (items.get(position).isCredito()) {
            ((TicketsHolder) holder).TVTipo.setText("A crédito");
        } else if (items.get(position).isBonificacion()){
            ((TicketsHolder) holder).TVTipo.setText("Bonificación");
        } else {
            ((TicketsHolder) holder).TVTipo.setText("De contado");
        }


        ((TicketsHolder) holder).CVTickets.setOnClickListener(view -> {
            imprimirTicket(view, items.get(position));
        });
    }

    @Override
    public int getItemCount() {
        return this.items!=null ? this.items.size():0;
    }

    private void imprimirTicket(View view, VentaDTO venta) {
        Log.d("FerChido", venta.toString());
        String stringReporte, htmlReporte;
        StringBuilder conceptoString = new StringBuilder();
        StringBuilder conceptoHTML = new StringBuilder();

        @SuppressLint("SimpleDateFormat") SimpleDateFormat timeStamp= new SimpleDateFormat("yyyy-MM-dd'T'HH:mm");
        @SuppressLint("SimpleDateFormat") SimpleDateFormat fdate= new SimpleDateFormat("dd/MM/yyyy");
        @SuppressLint("SimpleDateFormat") SimpleDateFormat tdate = new SimpleDateFormat("HH:mm");
        Date date = new Date();
        try {
            date = timeStamp.parse(venta.getFecha());
        } catch (ParseException e) {
            e.printStackTrace();
        }

        for (ConceptoDTO conceptoDTO: venta.getConcepto()){

            conceptoString.append(conceptoDTO.getConcepto())
                    .append("|").append(conceptoDTO.getCantidad())
                    .append("|$ ").append(conceptoDTO.getPUnitario())
                    .append("|$ ").append(conceptoDTO.getDescuento())
                    .append("|$ ").append(conceptoDTO.getSubtotal()).append("\n");

            conceptoHTML.append("<tr><td>").append(conceptoDTO.getConcepto())
                    .append("</td><td>").append(conceptoDTO.getCantidad())
                    .append("</td><td>$ ").append(conceptoDTO.getPUnitario())
                    .append("</td><td>$ ").append(conceptoDTO.getDescuento())
                    .append("</td><td>$ ").append(conceptoDTO.getSubtotal())
                    .append("</tr>");
        }
        stringReporte = "\tTiket de venta\n" +
                "Tiket: \t" + venta.getFolioVenta() + "\n"+
                "Fecha: \t" + fdate.format(date) + "\n"+
                "Hora: \t" + tdate.format(date) + "\n" +
                "Surtido: \t" + venta.getEstacion() + "\n" +
                "--------------------------------\n" +
                "\tCliente\n" +
                "No.Cliente: " + venta.getIdCliente() + "\n" +
                "Nombre: " + venta.getCliente() + "\n" +
                "RFC: "+ venta.getRFC() +"\n" +
                "--------------------------------\n" +
                "\tDetalles\n" +
                "Concepto|Cant|P Uni|Desc|Sub\n" +
                conceptoString +
                "--------------------------------\n" +
                "\tPago\n" +
                "I.V.A. (16%): $ " + venta.getIva() + "\n"+
                "Total: $ " + venta.getTotal() + "\n" +
                "Descuento: $ " + venta.getDescuentoTotal() + "\n";
        htmlReporte = "<body>" +
                "<h3><u>Nota de venta</u></h3>" +
                "<table>" +
                "<tr>" +
                "<td>Folio Venta: </td><td>" + venta.getFolioVenta() + "</td>"+
                "</tr>"+
                "<tr>" +
                "<td>Fecha: </td><td>" + fdate.format(date) + "</td>"+
                "</tr>"+
                "<tr>" +
                "<td>Hora: </td><td>" + tdate.format(date) + "</td>"+
                "</tr>" +
                "<tr>" +
                "<td>Surtido: </td><td>" + venta.getEstacion() + "</td>"+
                "</tr>" +
                "</table>"+
                "<hr>" +
                "<h3>Cliente</h3>" +
                "<table>" +
                "<tr>"+
                "<td>Numero: </td><td>" + venta.getIdCliente() + "</td>"+
                "</tr>"+
                "<tr>" +
                "<td>Nombre: </td><td>" + venta.getCliente() + "</td>" +
                "</tr>" +
                "<tr>" +
                "<td>RFC: </td><td>"+ venta.getRFC() +"</td>" +
                "</tr>" +
                "</table>" +
                "<hr>" +
                "<h3>Detalles</h3>" +
                "<table>" +
                "<tr>" +
                "<td>Concepto</td><td>Cant.</td><td>P.Uni.</td><td>Desc</td><td>Subt.</td>"+
                "</tr>" +
                conceptoHTML +
                "</table>" +
                "<hr>" +
                "<h3>Pago</h3>" +
                "<table>" +
                "<tr>" +
                "<td>I.V.A. (16%): </td><td>" + venta.getIva() + "</td>"+
                "</tr>"+
                "<tr>"+
                "<td>Total: </td><td>" + venta.getTotal() + "</td>"+
                "</tr>" +
                "<tr>"+
                "<td>Descuento: </td><td>" + venta.getDescuentoTotal() + "</td>"+
                "</tr>";
        double cambio;
        String tipoVenta;
        if (venta.isCredito()) {
            tipoVenta = "A credito";
            cambio = 0;
        } else if (venta.isBonificacion()){
            tipoVenta = "Bonificacion";
            cambio = 0;
        } else {
            tipoVenta = "De contado";
            cambio = Math.abs(venta.getCambio());
        }
        stringReporte += "Efectivo recibido: \t$ " + venta.getEfectivo()+ "\n" +
                "Cambio: $ " + cambio + "\n" +
                "Venta: " + tipoVenta +  " \n";
        htmlReporte += "<tr>" +
                "<td>Efectivo recibido: </td><td>" + venta.getEfectivo()+ "</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Cambio: </td><td>" + cambio + "</td>" +
                "</tr>" +
                "<tr>" +
                "<td>Venta: </td><td>" + tipoVenta +  "</td>" +
                "</tr>";
        stringReporte += "--------------------------------\n" +
                venta.getNombreGasera() + "\n" +
                "RFC: "+ venta.getRFCGasera() + "\n" +
                "Av. Principal No. 5477 C.P. 56789\n"+
                "www.gasmundialdeguerrero.com.mx\n"+
                "Le atendio: " + nombre + "\n\n" +
                "Facturacion electronica en:\n"+
                "www.gasmundialdeguerrero.com.mx/\n"+
                "facturacion\n\n"+
                "Folio Factura: " + venta.getFolioVenta() + "\n\n"+
                "Gracias por su confianza,vuelva\n" +
                "pronto!";
        htmlReporte+="</table></body>";
        Intent intent = new Intent(view.getContext(), VerReporteActivity.class);
        intent.putExtra("EsReimpresionTicket", true);
        intent.putExtra("StringReporte", stringReporte);
        intent.putExtra("HtmlReporte", htmlReporte);
        view.getContext().startActivity(intent);


    }

    private class TicketsHolder extends RecyclerView.ViewHolder {
        TextView TVHora, TVFolio, TVCliente, TVVenta, TVTipo;
        CardView CVTickets;
        TicketsHolder(View view) {
            super(view);
            TVHora = view.findViewById(R.id.TVHora);
            TVFolio = view.findViewById(R.id.TVFolio);
            TVCliente = view.findViewById(R.id.TVCliente);
            TVVenta = view.findViewById(R.id.TVVenta);
            TVTipo = view.findViewById(R.id.TVTipo);
            CVTickets = view.findViewById(R.id.CVTickets);
        }
    }
}
