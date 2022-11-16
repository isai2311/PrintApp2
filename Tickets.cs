
using System;
using System.Collections.Generic;

public class Tickets
{
    public int cTicketFolio { get; set; }
    public int cTicketFactura { get; set; }
    public int cTicketCliente { get; set; }
    public object cTicketClienteID { get; set; }
    public string cTicketNombre { get; set; }
    public object cTicketCorreo { get; set; }
    public object cTicketTelefono { get; set; }
    public String cTicketFecha { get; set; }
    public String responsable { get; set; }
    public int cTicketEstatus { get; set; }
    public decimal cTicketTotal { get; set; }
    public decimal cTicketSubtotal { get; set; }
    public decimal cTicketImpuesto1 { get; set; }
    public int cTicketDescuento { get; set; }
    public int cTicketDescuentoPorcentaje { get; set; }
    public object cTicketFee { get; set; }
    public int cTicketSucursal { get; set; }
    public object cTicketPlataforma { get; set; }
    public object cTicketHoraRecoge { get; set; }
    public int cTicketTipo { get; set; }
    public int cTicketTracking { get; set; }
    public int cTicketImprime { get; set; }
    public object cTicketNota { get; set; }
    public object cTicketUbicacion { get; set; }
    public object cTicketRuta { get; set; }
    public object cTicketRepartidor { get; set; }
    public int cTicketAplicado { get; set; }
    public int cTicketSincro { get; set; }
    public int cTicketPagado { get; set; }
    public int cTicketIIF { get; set; }
    public string DateFormateado { get; set; }
    public decimal Saldo { get; set; }
    public decimal TotalInvoice { get; set; }
    public decimal SubTotalInvoice { get; set; }
    public decimal ImpuestoInvoice { get; set; }
    public decimal SaldoInvoice { get; set; }
    public List<Detalles> details { get; set; }
    public DateTime created_at { get; set; }

}
