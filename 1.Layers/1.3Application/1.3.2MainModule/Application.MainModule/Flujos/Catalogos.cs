using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacen;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MainModule.Flujos
{
    public class Catalogos
    {

        #region Paises
        public List<PaisDTO> ListaPaises()
        {
            return PaisServicio.ListaPaises().ToList();
        }
        #endregion

        #region Estados Rep
        public List<EstadosRepDTO> ListaEstados()
        {
            return EstadosrepServicio.ListaEstadosR().ToList();
        }
        #endregion

        #region Empresas
        public List<EmpresaDTO> ListaEmpresasLogin()
        {
            return EmpresaServicio.BuscarEmpresasLogin();
        }
        public List<EmpresaDTO> ListaEmpresas()
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return EmpresaServicio.BuscarEmpresas();
            else
                return EmpresaServicio.BuscarEmpresas().ToList().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public List<EmpresaDTO> ListaEmpresas(bool conAC)
        {
            return EmpresaServicio.BuscarEmpresas(conAC);
        }

        public RespuestaDto RegistraEmpresa(EmpresaCrearDTO empDto)
        {
            var resp = PermisosServicio.PuedeRegistrarEmpresa();
            if (!resp.Exito) return resp;

            return EmpresaServicio.RegistrarEmpresa(EmpresaAdapter.FromDto(empDto));
        }

        public RespuestaDto ModificaEmpresa(EmpresaDTO empDto)
        {
            var resp = PermisosServicio.PuedeModificarEmpresa();
            if (!resp.Exito) return resp;

            var empresas = EmpresaServicio.Obtener(empDto.IdEmpresa);
            if (empresas == null) return EmpresaServicio.NoExiste();

            var emp = EmpresaAdapter.FromDTOEditar(empDto, empresas);
            emp.FechaRegistro = emp.FechaRegistro;
            return EmpresaServicio.ModificarEmpresa(emp);
        }

        public RespuestaDto EliminaEmpresa(short id)
        {
            var resp = PermisosServicio.PuedeEliminarEmpresa();
            if (!resp.Exito) return resp;

            var empresas = EmpresaServicio.Obtener(id);
            if (empresas == null) return EmpresaServicio.NoExiste();

            empresas = EmpresaAdapter.FromEntity(empresas);
            empresas.Activo = false;
            return EmpresaServicio.ModificarEmpresa(empresas);
        }

        public RespuestaDto ActualizaEmpresaConfig(EmpresaModificaConfig empDto)
        {
            var resp = PermisosServicio.PuedeModificarEmpresa();
            if (!resp.Exito) return resp;

            var empresaaMod = EmpresaServicio.Obtener(empDto.IdEmpresa);
            if (empresaaMod == null) return EmpresaServicio.NoExiste();

            var emp = EmpresaAdapter.FromDtoConfig(empDto, empresaaMod);
            return EmpresaServicio.ModificarEmpresa(emp);
        }

        #endregion

        #region Clientes
        public List<TipoPersonaDTO> TiposPersona()
        {
            return TipoPersonaServicio.ListaTipoPersona().ToList();
        }
        public List<RegimenDTO> RegimenFiscal()
        {
            return RegimenServicio.ListaRegimen().ToList();
        }
        public List<ClientesDto> ListaClientes(short idEmpresa)
        {
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return ClienteServicio.ListaClientes().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
            else
                return ClienteServicio.ListaClientes().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public List<ClientesDto> ListaClientes()
        {
            return ClienteServicio.ListaClientes().ToList();
        }

        public List<ClienteLocacionDTO> ListaLocaciones(int id)
        {
            return ClienteServicio.ObtenerLoc(id).ToList();
        }

        public RespuestaDto RegistraCliente(ClienteCrearDto cteDto)
        {
            var resp = PermisosServicio.PuedeRegistrarCliente();
            if (!resp.Exito) return resp;

            var cliente = ClientesAdapter.FromDtoMod(cteDto);

            if (!TokenServicio.EsSuperUsuario() && !TokenServicio.ObtenerEsAdministracionCentral())
                cliente.IdEmpresa = TokenServicio.ObtenerIdEmpresa();

            return ClienteServicio.AltaCliente(cliente);
        }

        public RespuestaDto ModificaCliente(ClienteCrearDto cteDto)
        {
            var resp = PermisosServicio.PuedeModificarCliente();
            if (!resp.Exito) return resp;

            var clientes = ClienteServicio.Obtener(cteDto.IdCliente);
            if (clientes == null) return ClienteServicio.NoExiste();

            var cte = ClientesAdapter.FromDtoEditar(cteDto, clientes);
            cte.FechaRegistro = cte.FechaRegistro;
            return ClienteServicio.Modificar(cte);
        }

        public RespuestaDto EliminaCliente(int id)
        {
            var resp = PermisosServicio.PuedeEliminarCliente();
            if (!resp.Exito) return resp;

            var clientes = ClienteServicio.Obtener(id);
            if (clientes == null) return ClienteServicio.NoExiste();

            clientes = ClientesAdapter.FromEntity(clientes);
            clientes.Activo = false;
            return ClienteServicio.Modificar(clientes);
        }

        public RespuestaDto RegistraClienteLocacion(ClienteLocacionDTO cteDto)
        {
            var resp = PermisosServicio.PuedeRegistrarCliente();
            if (!resp.Exito) return resp;

            var cliente = ClientesAdapter.FromDtox(cteDto);
                      

            return ClienteServicio.AltaClienteL(cliente);
        }

        public RespuestaDto ActualizaClienteLocacion(ClienteLocacionDTO cteDto)
        {
            var resp = PermisosServicio.PuedeModificarCliente();
            if (!resp.Exito) return resp;

            var clientes = ClienteServicio.ObtenerCL(cteDto.IdCliente, cteDto.Orden);
            if (clientes == null) return ClienteServicio.NoExiste();

            var cte = ClientesAdapter.FromDto(cteDto, clientes);           
            return ClienteServicio.ModificarCL(cte);
        }

        public RespuestaDto EliminaClienteLocacion(ClienteLocacionDTO cteDto)
        {
            var resp = PermisosServicio.PuedeEliminarCliente();
            if (!resp.Exito) return resp;

            //var clientes = ClienteServicio.Obtener(cteDto.Orden);
            //if (clientes == null) return ClienteServicio.NoExiste();

           var clientes = ClientesAdapter.FromDtocteLoc(cteDto);           
            return ClienteServicio.Eliminar(clientes);
        }
        #endregion

        #region Productos

        #region Categoria Productos
        public RespuestaDto RegistraCategoriaProducto(CategoriaProductoCrearDto cpDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.CategoriaProducto(cpDto);
            if (!resp.Exito) return resp;

            return ProductoServicios.RegistrarCategoriaProducto(ProductoAdapter.CategoriaProducto(cpDto));
        }

        public RespuestaDto ModificaCategoriaProducto(CategoriaProductoModificarDto cpDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.CategoriaProducto(cpDto);
            if (!resp.Exito) return resp;

            var catProd = ProductoServicios.ObtenerCategoria(cpDto.IdCategoria);
            if (catProd == null) return ProductoServicios.NoExiste("La categoría de producto");

            var categoria = ProductoAdapter.FromDto(cpDto, catProd);
            return ProductoServicios.ModificarCategoriaProducto(categoria);
        }

        public RespuestaDto EliminaCategoriaProducto(CategoriaProductoEliminarDto cpDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var catPro = ProductoServicios.ObtenerCategoria(cpDto.IdCategoria);
            if (catPro == null) return ProductoServicios.NoExiste("La categoría de producto");

            catPro = ProductoAdapter.FromEntity(catPro);
            catPro.Activo = false;
            return ProductoServicios.ModificarCategoriaProducto(catPro);
        }
        public List<CategoriaProductoDto> ListaCategoriasProducto()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerCategorias());
        }
        public CategoriaProductoDto ConsultaCategoriaProducto(short idCategoria)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerCategoria(idCategoria));
        }
        #endregion Categoria Productos

        #region Linea Productos
        public RespuestaDto RegistraLineaProducto(LineaProductoCrearDto cpDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.LineaProducto(cpDto);
            if (!resp.Exito) return resp;

            return ProductoServicios.RegistrarLineaProducto(ProductoAdapter.FromDto(cpDto));
        }

        public RespuestaDto ModificaLineaProducto(LineaProductoModificarDto lpDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.LineaProducto(lpDto, true);
            if (!resp.Exito) return resp;

            var linProd = ProductoServicios.ObtenerLineaProducto(lpDto.IdProductoLinea);
            if (linProd == null) return ProductoServicios.NoExiste("La línea del producto");

            var Linea = ProductoAdapter.FromDto(lpDto, linProd);
            return ProductoServicios.ModificarLineaProducto(Linea);
        }

        public RespuestaDto EliminaLineaProducto(LineaProductoEliminarDto cpDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var catPro = ProductoServicios.ObtenerLineaProducto(cpDto.IdProductoLinea);
            if (catPro == null) return ProductoServicios.NoExiste("La línea del producto");

            catPro = ProductoAdapter.FromEntity(catPro);
            catPro.Activo = false;
            return ProductoServicios.ModificarLineaProducto(catPro);
        }
        public List<LineaProductoDto> ListaLineasProducto()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerLineasProducto());
        }
        public LineaProductoDto ConsultaLineaProducto(short idLinea)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerLineaProducto(idLinea));
        }
        #endregion Linea Productos        

        #region Unidad de medida
        public RespuestaDto RegistraUnidadMedida(UnidadMedidaCrearDto uMDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.UnidadMedida(uMDto);
            if (!resp.Exito) return resp;

            return ProductoServicios.RegistrarUnidadMedida(ProductoAdapter.FromDto(uMDto));
        }

        public RespuestaDto ModificaUnidadMedida(UnidadMedidaModificarDto uMDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.UnidadMedida(uMDto, true);
            if (!resp.Exito) return resp;

            var uM = ProductoServicios.ObtenerUnidadMedida(uMDto.IdUnidadMedida);
            if (uM == null) return ProductoServicios.NoExiste("La unidad de medida");

            var uMedida = ProductoAdapter.FromDto(uMDto, uM);
            return ProductoServicios.ModificarUnidadMedida(uMedida);
        }

        public RespuestaDto EliminaUnidadMedida(UnidadMedidaEliminarDto uMDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var catPro = ProductoServicios.ObtenerUnidadMedida(uMDto.IdUnidadMedida);
            if (catPro == null) return ProductoServicios.NoExiste("La unidad de medida");

            catPro = ProductoAdapter.FromEntity(catPro);
            catPro.Activo = false;
            return ProductoServicios.ModificarUnidadMedida(catPro);
        }
        public List<UnidadMedidaDto> ListaUnidadesMedida()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerUnidadesMedida());
        }
        public UnidadMedidaDto ConsultaUnidadMedida(short idLinea)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerUnidadMedida(idLinea));
        }
        #endregion    

        #region Producto
        public RespuestaDto RegistraProducto(ProductoCrearDto pDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.Producto(pDto);
            if (!resp.Exito) return resp;

            return ProductoServicios.RegistrarProducto(ProductoAdapter.FromDto(pDto));
        }

        public RespuestaDto ModificaProducto(ProductoModificarDto pDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.Producto(pDto, true);
            if (!resp.Exito) return resp;

            var prod = ProductoServicios.ObtenerProducto(pDto.IdProducto);
            if (prod == null) return ProductoServicios.NoExiste("El producto");

            var producto = ProductoAdapter.FromDto(pDto, prod);
            return ProductoServicios.ModificarProducto(producto);
        }

        public RespuestaDto EliminaProducto(ProductoEliminarDto pDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var pro = ProductoServicios.ObtenerProducto(pDto.IdProducto);
            if (pro == null) return ProductoServicios.NoExiste("El producto");

            pro = ProductoAdapter.FromEntity(pro);
            pro.Activo = false;
            return ProductoServicios.ModificarProducto(pro);
        }
        public List<ProductoDTO> ListasProducto()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ListaProductos());
        }
        public ProductoDTO ConsultaProducto(short id)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicios.ObtenerProducto(id));
        }
        public List<ProductoDTO> ListaProductos()
        {
            return ProductoAdapter.ToDTO(ProductoServicios.ListaProductos());
        }
        public List<ProductoDTO> ListaProductosAsociados(int idProducto)
        {
            var proAsociados = ProductoServicios.ListaProductoAsociados(idProducto);
            var productosAsociados = ProductoServicios.ListaProductoAsociados(proAsociados);

            return ProductoAdapter.ToDTO(productosAsociados);
        }
        #endregion

        #endregion

        #region Centro de Costo
        public RespuestaDto RegistraCentroCosto(CentroCostoCrearDto ccDto)
        {
            var resp = PermisosServicio.PuedeRegistrarCentroCosto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.CentroCosto(ccDto);
            if (!resp.Exito) return resp;

            return CentroCostoServicio.RegistrarCentroCosto(CentroCostoAdapter.FromDto(ccDto));
        }

        public RespuestaDto ModificaCentroCosto(CentroCostoModificarDto ccDto)
        {
            var resp = PermisosServicio.PuedeModificarCentroCosto();
            if (!resp.Exito) return resp;

            //resp = ValidarCatalogoServicio.CentroCosto(ccDto);
            //if (!resp.Exito) return resp;

            var centro = CentroCostoServicio.Obtener(ccDto.IdCentroCosto);
            if (centro == null) return CentroCostoServicio.NoExiste();

            var CentroCosto = CentroCostoAdapter.FromDto(ccDto, centro);
            return CentroCostoServicio.ModificarCentroCosto(CentroCosto);
        }

        public RespuestaDto EliminaCentroCosto(CentroCostoEliminarDto ccDto)
        {
            var resp = PermisosServicio.PuedeEliminarCentroCosto();
            if (!resp.Exito) return resp;

            var centro = CentroCostoServicio.Obtener(ccDto.IdCentroCosto);
            if (centro == null) return CentroCostoServicio.NoExiste();

            centro = CentroCostoAdapter.FromEntity(centro);
            centro.Activo = false;
            return CentroCostoServicio.ModificarCentroCosto(centro);
        }

        public List<CentroCostoDTO> ListaCentrosCostos()
        {
            return CentroCostoAdapter.ToDTO(CentroCostoServicio.Obtener());
        }
        public CentroCostoDTO ConsultaCentroCosto(int idCentroCosto)
        {
            var resp = PermisosServicio.PuedeConsultarCentroCosto();
            if (!resp.Exito) return null;

            return CentroCostoAdapter.ToDTO(CentroCostoServicio.Obtener(idCentroCosto));
        }

        public List<TipoCentroCostoDto> ListaTipoCentroCosto()
        {
            return TipoCentroCostoAdapter.ToDTO(CentroCostoServicio.ListaTipoCentroCostos());
        }
        #endregion

        #region Proveedor
        public RespuestaDto RegistraProveedor(ProveedorCrearDto provDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProveedor();
            if (!resp.Exito) return resp;

            return ProveedorServicio.RegistrarProveedor(ProveedorAdapter.FromDto(provDto));
        }

        public RespuestaDto ModificaProveedor(ProveedorModificarDto provDto)
        {
            var resp = PermisosServicio.PuedeModificarProveedor();
            if (!resp.Exito) return resp;

            var provee = ProveedorServicio.Obtener(provDto.IdProveedor);
            if (provee == null) return ProveedorServicio.NoExiste();

            var proveedor = ProveedorAdapter.FromDto(provDto);
            proveedor.FechaRegistro = provee.FechaRegistro;
            return ProveedorServicio.ModificarProveedor(proveedor);
        }

        public RespuestaDto EliminaProveedor(ProveedorEliminarDto provDto)
        {
            var resp = PermisosServicio.PuedeEliminarProveedor();
            if (!resp.Exito) return resp;

            var provee = ProveedorServicio.Obtener(provDto.IdProveedor);
            if (provee == null) return ProveedorServicio.NoExiste();

            provee = ProveedorAdapter.FromEntity(provee);
            provee.Activo = false;
            return ProveedorServicio.ModificarProveedor(provee);
        }

        public List<ProveedorDto> ConsultaProveedores()
        {
            var resp = PermisosServicio.PuedeConsultarProveedor();
            if (!resp.Exito) return new List<ProveedorDto>();

            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener());
        }

        public ProveedorDto ConsultaProveedor(int idProveedor)
        {
            var resp = PermisosServicio.PuedeConsultarProveedor();
            if (!resp.Exito) return null;

            return ProveedorAdapter.ToDto(ProveedorServicio.Obtener(idProveedor));
        }
        #endregion

        #region Cuentas Contables
        public RespuestaDto RegistraCuentaContable(CuentaContableCrearDto ccDto)
        {
            var resp = PermisosServicio.PuedeRegistrarCuentaContable();
            if (!resp.Exito) return resp;

            return CuentaContableServicio.RegistrarCuentaContable(CuentaContableAdapter.FromDto(ccDto));
        }
        public RespuestaDto ModificaCuentaContable(CuentaContableModificarDto ccDto)
        {
            var resp = PermisosServicio.PuedeModificarCuentaContable();
            if (!resp.Exito) return resp;

            var ctactble = CuentaContableServicio.Obtener(ccDto.IdCuentaContable);
            if (ctactble == null) return CuentaContableServicio.NoExiste();

            //var CuentaContable = CuentaContableAdapter.FromDto(ccDto);
            var ctactbleEmptity = CuentaContableAdapter.FromEntity(ctactble);
            ctactbleEmptity.Numero = ccDto.Numero;
            ctactbleEmptity.Descripcion = ccDto.Descripcion;
            return CuentaContableServicio.ModificarCuentaContable(ctactbleEmptity);
        }
        public RespuestaDto EliminaCuentaContable(CuentaContableEliminarDto ccDto)
        {
            var resp = PermisosServicio.PuedeEliminarCuentaContable();
            if (!resp.Exito) return resp;

            var ctactble = CuentaContableServicio.Obtener(ccDto.IdCuenta);
            if (ctactble == null) return CuentaContableServicio.NoExiste();

            var ctactbleEmptity = CuentaContableAdapter.FromEmtyte(ctactble);
            ctactbleEmptity.Activo = false;
            return CuentaContableServicio.ModificarCuentaContable(ctactbleEmptity);
        }
        public List<CuentaContableDto> ConsultaCuentasContables(short idEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarCuentaContable();
            if (!resp.Exito) return new List<CuentaContableDto>();

            return CuentaContableAdapter.ToDto(CuentaContableServicio.Obtener().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList());
        }
        public CuentaContableDto ConsultaCuentaContable(int idCuentaContable)
        {
            var resp = PermisosServicio.PuedeConsultarCuentaContable();
            if (!resp.Exito) return null;

            return CuentaContableAdapter.ToDto(CuentaContableServicio.Obtener(idCuentaContable));
        }
        #endregion

        #region Estación de carburación
        public List<EstacionCarburacionDTO> ListaEstacionesCarburacion()
        {
            return EstacionCarburacionAdapter.toDTO(EstacionCarburacionServicio.ObtenerTodas());
        }
        public List<EstacionCarburacionDTO> ListaEstacionesCarburacion(short idEmpresa)
        {
            return EstacionCarburacionAdapter.toDTO(EstacionCarburacionServicio.ObtenerTodas(idEmpresa));
        }
        #endregion

        #region Unidad Almacen Gas
        //public List<UnidadAlmacenGasDTO> ListaEstacionesCarburacion()
        //{
        //    return UnidadAlmacenAdapter.ToDTO(AlmacenGasServicio.ObtenerAlmacenGeneral());
        //}
        public List<UnidadAlmacenGasDTO> ListaUnidadAlmacenGas(short idEmpresa)
        {
            return UnidadAlmacenAdapter.ToDTO(AlmacenGasServicio.ObtenerAlmacenGeneral(idEmpresa));
        }
        #endregion

        #region Equipo Transporte
        public List<EquipoTransporteDTO> ListaEquipoTrasnporte()
        {
            return EquipoTransporteAdapter.toDTO(EquipoTransporteServicio.BuscarEquipoTransporte());
        }
        #endregion
    }
}
