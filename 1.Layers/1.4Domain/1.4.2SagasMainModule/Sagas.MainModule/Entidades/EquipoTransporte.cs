//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class EquipoTransporte
{
    public int IdEquipoTransporte { get; set; }
    public short IdEmpresa { get; set; }
    public Nullable<int> IdVehiculoUtilitario { get; set; }
    public Nullable<int> IdCamioneta { get; set; }
    public Nullable<int> IdPipa { get; set; }
    public bool Activo { get; set; }
    public System.DateTime FechaRegistro { get; set; }

    public virtual Camioneta Camionetas { get; set; }
    public virtual Pipa Pipas { get; set; }
    public virtual Empresa Empresa { get; set; }
}
