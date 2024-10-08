﻿public class Detalles
    {
        public int cTicketDetalleFolio { get; set; }
        public int cTicketDetalleTicket { get; set; }
        public int cTicketDetalleProducto { get; set; }
        public int cTicketDetalleSucursal { get; set; }
        public decimal cTicketDetallePrecio { get; set; }
        public int cTicketDetalleCantidad { get; set; }
        public decimal cTicketDetalleSubtotal { get; set; }
        public decimal cTicketDetalleTotal { get; set; }
        public decimal cTicketDetalleDescuento { get; set; }
        public decimal cTicketDetalleImpuesto1 { get; set; }
        public object cTicketDetalleComentarios { get; set; }
        public object cTicketDetalleLugarEntrega { get; set; }
        public int cTicketDetallePaquete { get; set; }
        public int cTicketDetalleContador { get; set; }
        public int cTicketDetalleCaja { get; set; }
        public decimal cTicketDetalleCosto { get; set; }
        public int cTicketDetalleProcesado { get; set; }
        public object cTicketDetalleCantidadProcesada { get; set; }
        public int cTicketDetalleCantidadPedido { get; set; }
        public object cTicketDetalleCategoriaPrecio { get; set; }
        public decimal PrecioTotalFactura { get; set; }
        public Producto producto { get; set; }
    }

    public class Producto
    {
        public int cProductoCodigo { get; set; }
        public string cProductoDescripcion { get; set; }
        public string cProductoInterCodigo { get; set; }
        public int cProductoImpuesto1 { get; set; }
        public int cProductoImpuesto2 { get; set; }
        public int cProductoImpuesto3 { get; set; }
        public int cProductoImpuesto4 { get; set; }
        public int cProductoCategoria { get; set; }
        public int cProductoSubCategoria { get; set; }
        public int cProductoUnidadMedida { get; set; }
        public object cProductoUnidadMedidaSecundaria { get; set; }
        public object cProductoClasificacion { get; set; }
        public int cProductoAutoModificador { get; set; }
        public object cProductoModificador { get; set; }
        public object cProductoModificadorCant { get; set; }
        public object cProductoCombo { get; set; }
        public object cProductoComboCat1 { get; set; }
        public object cProductoComboPorcion1 { get; set; }
        public object cProductoComboCat1Cant { get; set; }
        public object cProductoComboCat2 { get; set; }
        public object cProductoComboPorcion2 { get; set; }
        public object cProductoComboCat2Cant { get; set; }
        public object cProductoComboCat3 { get; set; }
        public object cProductoComboPorcion3 { get; set; }
        public object cProductoComboCat3Cant { get; set; }
        public object cProductoComboCat4 { get; set; }
        public object cProductoComboPorcion4 { get; set; }
        public object cProductoComboCat4Cant { get; set; }
        public object cProductoComboCat5 { get; set; }
        public object cProductoComboPorcion5 { get; set; }
        public object cProductoComboCat5Cant { get; set; }
        public object cProductoPuntos { get; set; }
        public int cProductoPais { get; set; }
        public object cProductoBolsa { get; set; }
        public object cProductoPieza { get; set; }
        public object cProductoBascula { get; set; }
        public object cProductoCodBarras { get; set; }
        public int cProductoAutorizado { get; set; }
        public int cProductoCompuesto { get; set; }
        public int cProductoRenta { get; set; }
        public object cProductoRentaDisponible { get; set; }
        public object cProductoRentaItem { get; set; }
        public object cProductoRentaDescripcionPromo { get; set; }
        public object cProductoRentaNombre { get; set; }
        public object cProductoRentaHoraInicio { get; set; }
        public object cProductoRentaRedondeo { get; set; }
        public object cProductoFamilia { get; set; }
        public object cProductoSubfamilia { get; set; }
        public int cProductoInventariable { get; set; }
        public int cProductoPOS { get; set; }
        public string cProductoPorcion { get; set; }
}