using Application.MainModule.AdaptadoresDTO.Catalogo;
using Application.MainModule.DTOs;
using Application.MainModule.AdaptadoresDTO.Seguridad;
using Application.MainModule.DTOs.Catalogo;
using Application.MainModule.DTOs.Respuesta;
using Application.MainModule.Servicios.AccesoADatos;
using Application.MainModule.Servicios.Almacenes;
using Application.MainModule.Servicios.Catalogos;
using Application.MainModule.Servicios.Seguridad;
using Sagas.MainModule.Entidades;
using Sagas.MainModule.ObjetosValor.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.MainModule.Servicios.Ventas;
using Application.MainModule.DTOs.Transporte;
using Application.MainModule.Servicios.Pedidos;
using Application.MainModule.Servicios;
using Application.MainModule.AdaptadoresDTO;
using Application.MainModule.Servicios.Facturacion;
using Application.MainModule.AdaptadoresDTO.IngresoEgreso;
using Application.MainModule.Servicios.IngresoGasto;

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
        public ClientesDto ObtenerCliente(int id)
        {
            var _c = ClienteServicio.Obtener(id);
            return ClientesAdapter.ToDTO(_c);
        }
        public List<ClientesDto> ListaClientes()
        {
            var resp = PermisosServicio.PuedeConsultarCliente();
            if (!resp.Exito) return null;
            if (TokenServicio.ObtenerEsAdministracionCentral())
                return ClienteServicio.ListaClientes().ToList();
            else
                return ClienteServicio.ListaClientes().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }

        public List<ClienteLocacionDTO> ListaLocaciones(int id)
        {
            var resp = PermisosServicio.PuedeConsultarCliente();
            if (!resp.Exito) return null;
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
        public RespuestaDto RegistraClienteAutoConsumo(ClienteCrearDto cteDto)
        {
            //var resp = PermisosServicio.PuedeRegistrarCliente();
            //if (!resp.Exito) return resp;

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
        public RespuestaDto ModificaClienteAutoServicio(ClienteCrearDto cteDto)
        {
            //var resp = PermisosServicio.PuedeModificarCliente();
            //if (!resp.Exito) return resp;

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

        #region PuntosdeVenta
        public List<PuntoVentaDTO> ListaPuntosVenta()
        {
            var resp = PermisosServicio.PuedeConsultarPuntoVenta();
            if (!resp.Exito) return null;

            if (TokenServicio.EsSuperUsuario())
                return PuntoVentaServicio.Obtener().ToList();
            else
                return PuntoVentaServicio.Obtener().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();

        }

        public List<PuntoVentaDTO> PuntosVentaIdEmpresa(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarPuntoVenta();
            if (!resp.Exito) return null;
            return PuntoVentaServicio.Obtener().ToList();
        }
        public RespuestaDto EliminaPuntosVenta(PuntoVentaDTO cteDto)
        {
            var resp = PermisosServicio.PuedeEliminarPuntoVenta();
            if (!resp.Exito) return resp;

            var puntoV = PuntoVentaAdapter.FromDto(cteDto);
            puntoV.Activo = false;
            return PuntoVentaServicio.Eliminar(puntoV);
        }

        public OperadorChoferDTO GetOperador(int idUsuario)
        {
            return PuntoVentaServicio.ObtenerOperador(idUsuario);
        }

        public List<OperadorChoferDTO> GetUsuariosIdEmpesa(short idEmpresa)
        {
            return PuntoVentaServicio.ObtenerUsuariosOperador(idEmpresa);
        }
        public RespuestaDto ModificaOperador(PuntoVentaDTO pvDto)
        {
            var resp = PermisosServicio.PuedeConsultarPuntoVenta();
            if (!resp.Exito) return resp;

            var clientes = PuntoVentaServicio.Obtener(pvDto.IdPuntoVenta);
            if (clientes == null) return PuntoVentaServicio.NoExiste();

            var cte = PuntoVentaAdapter.FromDtoEditar(pvDto, clientes);
            cte.FechaRegistro = cte.FechaRegistro;
            return PuntoVentaServicio.Modificar(cte);
        }
        #endregion

        #region Precio de venta
        public void UpdateStatus(short idEmpresa)
        {
            List<PrecioVentaDTO> _lstCompleta = ListaPreciosVenta();
            PrecioVentaDTO entity = new PrecioVentaDTO();

            DateTime CurrentDate = DateTime.Now;
            string CurrentDateS = DateTime.Now.ToString("dd/MM/yyyy");
            //  var _lstPorEmpresa = ListaPreciosVenta().GroupBy(x => x.IdEmpresa).ToList();
            if (idEmpresa != 0)
            {
                List<PrecioVentaDTO> _lstEmpresaId = LstPreciosVentaIdEmpresa(idEmpresa);//.Where(x => x.IdPrecioVentaEstatus.Equals(2)).ToList();
                foreach (var item in _lstEmpresaId)
                {
                    string FechaProgString = item.FechaProgramada.ToString("dd/MM/yyyy");
                    if (FechaProgString == CurrentDateS)
                    {
                        //entity.IdPrecioVenta = item.IdPrecioVenta;
                        //entity.FechaVencimiento = CurrentDate;

                        /**Actualizar Estatus de Programado a Vigente***/
                        if (item.IdPrecioVentaEstatus == 1)//1-Programado, 2-Vigente, 3-Vencido
                        {
                            entity.IdEmpresa = item.IdEmpresa;
                            entity.IdPrecioVentaEstatus = 2;//////2-Vigente
                            entity.IdPrecioVenta = item.IdPrecioVenta;
                            ModificaPrecioVentaGas(entity);
                        }

                        /****Actualizar Estatus de Vigente a Vencido*****/
                        List<PrecioVentaDTO> _lstEmpresa = LstPreciosVentaIdEmpresa(item.IdEmpresa).Where(x => x.IdPrecioVentaEstatus.Equals(2)).ToList();
                        if (_lstEmpresa.Count() > 1)
                        {
                            var x = (from z in _lstEmpresa orderby z.FechaRegistro ascending select z).FirstOrDefault();
                            entity.FechaVencimiento = CurrentDate;
                            entity.IdPrecioVenta = x.IdPrecioVenta;
                            entity.IdPrecioVentaEstatus = 3;  //3-Vencido
                            ModificaPrecioVentaGas(entity);
                        }
                    }
                    else
                    {
                        DateTime FechaProgDate = item.FechaProgramada;
                        DateTime currentDateTime = DateTime.Now;

                        if (item.IdPrecioVentaEstatus == 1)
                        {       //20-10/2018----22/10/2018
                            if (FechaProgDate < currentDateTime)
                            {
                                entity.IdEmpresa = item.IdEmpresa;
                                entity.IdPrecioVentaEstatus = 2;//////2-Vigente
                                entity.IdPrecioVenta = item.IdPrecioVenta;
                                ModificaPrecioVentaGas(entity);

                                /****Actualizar Estatus de Vigente a Vencido*****/
                                List<PrecioVentaDTO> _lstEmpresa = LstPreciosVentaIdEmpresa(item.IdEmpresa).Where(x => x.IdPrecioVentaEstatus.Equals(2)).ToList();
                                if (_lstEmpresa.Count() > 1)
                                {
                                    var x = (from z in _lstEmpresa orderby z.FechaRegistro ascending select z).FirstOrDefault();
                                    entity.FechaVencimiento = CurrentDate;
                                    entity.IdPrecioVenta = x.IdPrecioVenta;
                                    entity.IdPrecioVentaEstatus = 3;  //3-Vencido
                                    ModificaPrecioVentaGas(entity);
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                foreach (var item in _lstCompleta)
                {
                    string FechaProgString = item.FechaProgramada.ToString("dd/MM/yyyy");
                    if (FechaProgString == CurrentDateS)
                    {
                        /**Actualizar Estatus de Programado a Vigente***/
                        if (item.IdPrecioVentaEstatus == 1)//1-Programado, 2-Vigente, 3-Vencido
                        {
                            entity.IdEmpresa = item.IdEmpresa;
                            entity.IdPrecioVentaEstatus = 2;//////2-Vigente
                            entity.IdPrecioVenta = item.IdPrecioVenta;
                            ModificaPrecioVentaGas(entity);
                        }

                        /****Actualizar Estatus de Vigente a Vencido*****/
                        List<PrecioVentaDTO> _lstEmpresa = LstPreciosVentaIdEmpresa(item.IdEmpresa).Where(x => x.IdPrecioVentaEstatus.Equals(2)).ToList();
                        if (_lstEmpresa.Count() > 1)
                        {
                            var x = (from z in _lstEmpresa orderby z.FechaRegistro ascending select z).FirstOrDefault();
                            entity.FechaVencimiento = CurrentDate;
                            entity.IdPrecioVenta = x.IdPrecioVenta;
                            entity.IdPrecioVentaEstatus = 3;  //3-Vencido
                            ModificaPrecioVentaGas(entity);
                        }

                    }
                    else
                    {

                        if (item.IdPrecioVentaEstatus == 1)
                        {
                            DateTime FechaProgDate = item.FechaProgramada;
                            DateTime currentDateTime = DateTime.Now;
                            //20-10/2018----22/10/2018
                            if (FechaProgDate < currentDateTime)
                            {
                                entity.IdEmpresa = item.IdEmpresa;
                                entity.IdPrecioVentaEstatus = 2;//////2-Vigente
                                entity.IdPrecioVenta = item.IdPrecioVenta;
                                ModificaPrecioVentaGas(entity);

                                /****Actualizar Estatus de Vigente a Vencido*****/
                                List<PrecioVentaDTO> _lstEmpresa = LstPreciosVentaIdEmpresa(item.IdEmpresa).Where(x => x.IdPrecioVentaEstatus.Equals(2)).ToList();
                                if (_lstEmpresa.Count() > 1)
                                {
                                    var x = (from z in _lstEmpresa orderby z.FechaRegistro ascending select z).FirstOrDefault();
                                    entity.FechaVencimiento = CurrentDate;
                                    entity.IdPrecioVenta = x.IdPrecioVenta;
                                    entity.IdPrecioVentaEstatus = 3;  //3-Vencido
                                    ModificaPrecioVentaGas(entity);
                                }
                            }
                        }
                    }

                }
            }
        }
        public List<PrecioVentaDTO> ListaPreciosVenta()
        {
            var resp = PermisosServicio.PuedeConsultarPrecioVentaGas();
            if (!resp.Exito) return null;

            if (TokenServicio.EsSuperUsuario())
                return PrecioVentaGasServicio.Obtener().ToList();
            else
                return PrecioVentaGasServicio.Obtener().Where(x => x.IdEmpresa.Equals(TokenServicio.ObtenerIdEmpresa())).ToList();
        }
        public List<PrecioVentaDTO> LstPreciosVentaIdEmpresa(short idEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarPrecioVentaGas();
            if (!resp.Exito) return null;

            return PrecioVentaGasServicio.Obtener().Where(x => x.IdEmpresa.Equals(idEmpresa)).ToList();
        }
        public List<PrecioVentaEstatusDTO> TipoFecha()
        {
            var resp = PermisosServicio.PuedeConsultarPrecioVentaGas();
            if (!resp.Exito) return null;

            return PrecioVentaGasServicio.ObtenerListEstatus().ToList();
        }
        public List<PrecioVentaDTO> PreciosVentaIdEmpresa(short IdEmpresa)
        {
            var resp = PermisosServicio.PuedeConsultarPrecioVentaGas();
            if (!resp.Exito) return null;

            return PrecioVentaGasServicio.ObtenerPreciosVentaIdEmp(IdEmpresa).ToList();
        }
        public PrecioVentaDTO ObtenerPrecioVentaVigente()
        {
            var pv = PrecioVentaGasServicio.ObtenerPrecioVigente(TokenServicio.ObtenerIdEmpresa());
            var producto = ProductoServicio.ObtenerProducto(pv.IdProducto);
            //var pv = PrecioVentaGasServicio.ObtenerPrecioVigente((short)2); (Test)
            return PrecioVentaGasAdapter.ToDTO(pv, producto);
        }
        public RespuestaDto EliminaPreciosVenta(PrecioVentaDTO cteDto)
        {
            var resp = PermisosServicio.PuedeEliminarPrecioVentaGas();
            if (!resp.Exito) return resp;

            var precioventa = PrecioVentaGasServicio.Obtener(cteDto.IdPrecioVenta);
            if (precioventa == null) return PrecioVentaGasServicio.NoExiste();

            var precioV = PrecioVentaGasAdapter.FromTo(cteDto);
            return PrecioVentaGasServicio.Eliminar(precioV);
        }
        public RespuestaDto RegistraPrecioVentaGas(PrecioVentaDTO cteDto)
        {
            var resp = PermisosServicio.PuedeRegistrarPrecioVentaGas();
            if (!resp.Exito) return resp;
            //  var precio = PrecioVentaGasAdapter.FromTo(cteDto);          
            var precio = PrecioVentaGasAdapter.FromDTO(cteDto);
            return PrecioVentaGasServicio.AltaPrecioVentaGas(precio);
        }
        public RespuestaDto ModificaPrecioVentaGas(PrecioVentaDTO cteDto)
        {
            var resp = PermisosServicio.PuedeModificarPrecioVentaGas();
            if (!resp.Exito) return resp;

            var precioventa = PrecioVentaGasServicio.Obtener(cteDto.IdPrecioVenta);
            if (precioventa == null) return PrecioVentaGasServicio.NoExiste();

            var cte = PrecioVentaGasAdapter.FromDtoEditar(cteDto, precioventa);
            cte.FechaRegistro = cte.FechaRegistro;
            return PrecioVentaGasServicio.Modificar(cte);
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

            return ProductoServicio.RegistrarCategoriaProducto(ProductoAdapter.CategoriaProducto(cpDto));
        }

        public RespuestaDto ModificaCategoriaProducto(CategoriaProductoModificarDto cpDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.CategoriaProducto(cpDto);
            if (!resp.Exito) return resp;

            var catProd = ProductoServicio.ObtenerCategoria(cpDto.IdCategoria);
            if (catProd == null) return ProductoServicio.NoExiste("La categoría de producto");

            var categoria = ProductoAdapter.FromDto(cpDto, catProd);
            return ProductoServicio.ModificarCategoriaProducto(categoria);
        }

        public RespuestaDto EliminaCategoriaProducto(CategoriaProductoEliminarDto cpDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var catPro = ProductoServicio.ObtenerCategoria(cpDto.IdCategoria);
            if (catPro == null) return ProductoServicio.NoExiste("La categoría de producto");

            catPro = ProductoAdapter.FromEntity(catPro);
            catPro.Activo = false;
            return ProductoServicio.ModificarCategoriaProducto(catPro);
        }
        public List<CategoriaProductoDto> ListaCategoriasProducto()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerCategorias());
        }
        public CategoriaProductoDto ConsultaCategoriaProducto(short idCategoria)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerCategoria(idCategoria));
        }
        #endregion Categoria Productos

        #region Linea Productos
        public RespuestaDto RegistraLineaProducto(LineaProductoCrearDto cpDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.LineaProducto(cpDto);
            if (!resp.Exito) return resp;

            return ProductoServicio.RegistrarLineaProducto(ProductoAdapter.FromDto(cpDto));
        }

        public RespuestaDto ModificaLineaProducto(LineaProductoModificarDto lpDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.LineaProducto(lpDto, true);
            if (!resp.Exito) return resp;

            var linProd = ProductoServicio.ObtenerLineaProducto(lpDto.IdProductoLinea);
            if (linProd == null) return ProductoServicio.NoExiste("La línea del producto");

            var Linea = ProductoAdapter.FromDto(lpDto, linProd);
            return ProductoServicio.ModificarLineaProducto(Linea);
        }

        public RespuestaDto EliminaLineaProducto(LineaProductoEliminarDto cpDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var catPro = ProductoServicio.ObtenerLineaProducto(cpDto.IdProductoLinea);
            if (catPro == null) return ProductoServicio.NoExiste("La línea del producto");

            catPro = ProductoAdapter.FromEntity(catPro);
            catPro.Activo = false;
            return ProductoServicio.ModificarLineaProducto(catPro);
        }
        public List<LineaProductoDto> ListaLineasProducto()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerLineasProducto());
        }
        public LineaProductoDto ConsultaLineaProducto(short idLinea)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerLineaProducto(idLinea));
        }
        #endregion Linea Productos        

        #region Unidad de medida
        public RespuestaDto RegistraUnidadMedida(UnidadMedidaCrearDto uMDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.UnidadMedida(uMDto);
            if (!resp.Exito) return resp;

            return ProductoServicio.RegistrarUnidadMedida(ProductoAdapter.FromDto(uMDto));
        }

        public RespuestaDto ModificaUnidadMedida(UnidadMedidaModificarDto uMDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.UnidadMedida(uMDto, true);
            if (!resp.Exito) return resp;

            var uM = ProductoServicio.ObtenerUnidadMedida(uMDto.IdUnidadMedida);
            if (uM == null) return ProductoServicio.NoExiste("La unidad de medida");

            var uMedida = ProductoAdapter.FromDto(uMDto, uM);
            return ProductoServicio.ModificarUnidadMedida(uMedida);
        }

        public RespuestaDto EliminaUnidadMedida(UnidadMedidaEliminarDto uMDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var catPro = ProductoServicio.ObtenerUnidadMedida(uMDto.IdUnidadMedida);
            if (catPro == null) return ProductoServicio.NoExiste("La unidad de medida");

            catPro = ProductoAdapter.FromEntity(catPro);
            catPro.Activo = false;
            return ProductoServicio.ModificarUnidadMedida(catPro);
        }
        public List<UnidadMedidaDto> ListaUnidadesMedida()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerUnidadesMedida());
        }
        public UnidadMedidaDto ConsultaUnidadMedida(short idLinea)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerUnidadMedida(idLinea));
        }
        #endregion    

        #region Producto
        public RespuestaDto RegistraProducto(ProductoCrearDto pDto)
        {
            var resp = PermisosServicio.PuedeRegistrarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.Producto(pDto);
            if (!resp.Exito) return resp;

            return ProductoServicio.RegistrarProducto(ProductoAdapter.FromDto(pDto));
        }
        public RespuestaDto ModificaProducto(ProductoModificarDto pDto)
        {
            var resp = PermisosServicio.PuedeModificarProducto();
            if (!resp.Exito) return resp;

            resp = ValidarCatalogoServicio.Producto(pDto, true);
            if (!resp.Exito) return resp;

            var prod = ProductoServicio.ObtenerProducto(pDto.IdProducto);
            if (prod == null) return ProductoServicio.NoExiste("El producto");

            var producto = ProductoAdapter.FromDto(pDto, prod);
            return ProductoServicio.ModificarProducto(producto);
        }
        public RespuestaDto EliminaProducto(ProductoEliminarDto pDto)
        {
            var resp = PermisosServicio.PuedeEliminarProducto();
            if (!resp.Exito) return resp;

            var pro = ProductoServicio.ObtenerProducto(pDto.IdProducto);
            if (pro == null) return ProductoServicio.NoExiste("El producto");

            pro = ProductoAdapter.FromEntity(pro);
            pro.Activo = false;
            return ProductoServicio.ModificarProducto(pro);
        }
        public List<ProductoDTO> ListasProducto()
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ListaProductos());
        }
        public ProductoDTO ConsultaProducto(short id)
        {
            var resp = PermisosServicio.PuedeConsultarProducto();
            if (!resp.Exito) return null;

            return ProductoAdapter.ToDTO(ProductoServicio.ObtenerProducto(id));
        }
        public List<ProductoDTO> ListaProductos()
        {
            return ProductoAdapter.ToDTO(ProductoServicio.ListaProductos());
        }
        public List<ProductoDTO> ListaProductosAsociados(int idProducto)
        {
            var proAsociados = ProductoServicio.ListaProductoAsociados(idProducto);
            var productosAsociados = ProductoServicio.ListaProductoAsociados(proAsociados);

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
            //CentroCosto = CentroCostoAdapter.FromEntity(CentroCosto);

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
        public List<CuentaContableDto> ConsultaCuentasContables()
        {
            var resp = PermisosServicio.PuedeConsultarCuentaContable();
            if (!resp.Exito) return new List<CuentaContableDto>();

            return CuentaContableAdapter.ToDto(CuentaContableServicio.Obtener());
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
        public List<EquipoTransporteDTO> ListaEquiposdeTransporte(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            return EquipoTransporteServicio.Obtener(idempresa).ToList();
        }
        public EquipoTransporteDTO VehiculoId(int idVehiculo)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            return EquipoTransporteServicio.Obtener(idVehiculo);
        }
        public RespuestaDto RegistraEquipoTransporte(EquipoTransporteDTO vehiculoDto)
        {
            var resp = PermisosServicio.PuedeRegistrarPedido();
            if (!resp.Exito) return resp;

            var vehiculo = EquipoTransporteAdapter.FromDTO(vehiculoDto);

            vehiculo.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
            if (vehiculoDto.IdTipoUnidad == TipoUnidadEqTransporteEnum.Camioneta)//IdCamioneta
            {
                var camioneta = CamionetaAdapter.FromDTO(vehiculoDto);
                var cam = CamionetaServicio.Registrar(camioneta);
                if (!cam.Exito) return cam;
                else
                {
                    vehiculo.IdCamioneta = cam.Id;
                    vehiculoDto.IdCamioneta = cam.Id;
                    var almacen = AlmacenGasServicio.RegistraAlmacen(vehiculoDto);
                    if (!almacen.Exito)
                        return CamionetaServicio.Borrar(cam.Id);
                }
            }
            if (vehiculoDto.IdTipoUnidad == TipoUnidadEqTransporteEnum.Pipa)//IdPipa
            {
                var Pipa = PipaAdapter.FromDTO(vehiculoDto);
                var p = PipaServicio.Registrar(Pipa);
                if (!p.Exito) return p;
                else
                {
                    vehiculo.IdPipa = p.Id;
                    vehiculoDto.IdPipa = p.Id;
                    var almacen = AlmacenGasServicio.RegistraAlmacen(vehiculoDto);
                    if (!almacen.Exito)
                        return PipaServicio.Borrar(p.Id);
                }
            }
            if (vehiculoDto.IdTipoUnidad == TipoUnidadEqTransporteEnum.Utilitario)//IdVehiculoUtilitario
            {
                var utilitario = VehiculoUtilitarioAdapter.FromDTO(vehiculoDto);
                var vu = VehiculoUtilitarioServicio.Registrar(utilitario);
                vehiculo.IdUtilitario = vu.Id;
            }
            return EquipoTransporteServicio.Alta(vehiculo);
        }

        public RespuestaDto ModificaEquipoTrasnporte(EquipoTransporteDTO vehiculoDto)
        {
            var resp = PermisosServicio.PuedeModificarPedido();
            if (!resp.Exito) return resp;

            var vehiculo = new EquipoTransporteDataAccess().Buscar(vehiculoDto.IdEquipoTransporte);//EquipoTransporteServicio.Obtener(vehiculoDto.IdEquipoTransporte);
            if (vehiculo == null) return EquipoTransporteServicio.NoExiste();

            vehiculoDto.IdPipa = null;
            vehiculoDto.IdCamioneta = null;
            vehiculoDto.IdVehiculoUtilitario = null;

            RespuestaDto RespuestaGeneral = new RespuestaDto();
            CDetalleEquipoTransporte editarVehiculo = new CDetalleEquipoTransporte();
            if (vehiculoDto.IdTipoUnidad.Equals(TipoUnidadEqTransporteEnum.Camioneta))
            {
                if (vehiculo.IdCamioneta != null)
                {
                    var Camioneta = CamionetaServicio.Obtener(vehiculo.IdCamioneta.Value, false);
                    if (Camioneta == null) return CamionetaServicio.NoExiste();

                    var camionetaEntity = CamionetaAdapter.FromEntity(Camioneta);
                    camionetaEntity.Nombre = string.Format("Camioneta No. {0}", vehiculoDto.DescVehiculo);
                    camionetaEntity.Numero = vehiculoDto.DescVehiculo;
                    camionetaEntity.Activo = vehiculoDto.Activo;
                    editarVehiculo.CCamioneta = camionetaEntity;
                    vehiculoDto.IdCamioneta = Camioneta.IdCamioneta;

                    var Dtovehiculo = EquipoTransporteAdapter.FromEntity(EquipoTransporteAdapter.FromDTO(vehiculoDto));
                    RespuestaGeneral = EquipoTransporteServicio.Modificar(Dtovehiculo, editarVehiculo);
                }
                else
                {
                    var camioneta = CamionetaAdapter.FromDTO(vehiculoDto);
                    var cam = CamionetaServicio.Registrar(camioneta);
                    if (!cam.Exito) return cam;
                    else
                    {
                        vehiculoDto.IdCamioneta = cam.Id;
                        var almacen = AlmacenGasServicio.RegistraAlmacen(vehiculoDto);
                        if (almacen.Exito)
                        {
                            var Dtovehiculo = EquipoTransporteAdapter.FromEntity(EquipoTransporteAdapter.FromDTO(vehiculoDto));
                            var Mod = EquipoTransporteServicio.Modificar(Dtovehiculo);
                            if (Mod.Exito)
                                return EquipoTransporteServicio.BorrarVehiculoPorEdicion(vehiculo);
                            else
                                return CamionetaServicio.Borrar(cam.Id);
                        }
                        else
                            return CamionetaServicio.Borrar(cam.Id);
                    }
                }
            }
            if (vehiculoDto.IdTipoUnidad == TipoUnidadEqTransporteEnum.Pipa)
            {
                if (vehiculo.IdPipa != null)
                {
                    var _Pipa = PipaServicio.Obtener(vehiculo.IdPipa.Value, false);
                    if (_Pipa == null) return PipaServicio.NoExiste();

                    var pipaEntity = PipaAdapter.FromEntity(_Pipa);
                    pipaEntity.Nombre = string.Format("Pipa No. {0}", vehiculoDto.DescVehiculo);
                    pipaEntity.Numero = vehiculoDto.DescVehiculo;
                    pipaEntity.Activo = vehiculoDto.Activo;
                    editarVehiculo.CPipa = pipaEntity;
                    vehiculoDto.IdPipa = _Pipa.IdPipa;
                    editarVehiculo.CPipa.IdPipa = _Pipa.IdPipa;

                    var Dtovehiculo = EquipoTransporteAdapter.FromEntity(EquipoTransporteAdapter.FromDTO(vehiculoDto));
                    RespuestaGeneral = EquipoTransporteServicio.Modificar(Dtovehiculo, editarVehiculo);
                }
                else
                {
                    var _pipa = PipaAdapter.FromDTO(vehiculoDto);
                    var p = PipaServicio.Registrar(_pipa);
                    if (!p.Exito) return p;
                    else
                    {
                        vehiculoDto.IdPipa = p.Id;
                        var almacen = AlmacenGasServicio.RegistraAlmacen(vehiculoDto);
                        if (almacen.Exito)
                        {
                            var Dtovehiculo = EquipoTransporteAdapter.FromEntity(EquipoTransporteAdapter.FromDTO(vehiculoDto));
                            var Mod = EquipoTransporteServicio.Modificar(Dtovehiculo);
                            if (Mod.Exito)
                                return EquipoTransporteServicio.BorrarVehiculoPorEdicion(vehiculo);
                            else
                                return PipaServicio.Borrar(p.Id);
                        }
                        else
                            return PipaServicio.Borrar(p.Id);
                    }
                }
            }
            if (vehiculoDto.IdTipoUnidad == TipoUnidadEqTransporteEnum.Utilitario)
            {
                if (vehiculo.IdUtilitario != null)
                {
                    //  var _utilitario = VehiculoUtilitarioServicio.Obtener(vehiculo.IdPipa.Value, false);
                    var _utilitario = VehiculoUtilitarioServicio.Obtener(vehiculo.IdUtilitario.Value, false);
                    if (_utilitario == null) return VehiculoUtilitarioServicio.NoExiste();

                    var utilitarioEntity = VehiculoUtilitarioAdapter.FromEntity(_utilitario);
                    utilitarioEntity.Nombre = string.Format("Utilitario No. {0}", vehiculoDto.DescVehiculo);
                    utilitarioEntity.Numero = vehiculoDto.DescVehiculo;
                    utilitarioEntity.Activo = vehiculoDto.Activo;
                    editarVehiculo.CUtilitario = utilitarioEntity;
                    vehiculoDto.IdVehiculoUtilitario = _utilitario.IdUtilitario;

                    var Dtovehiculo = EquipoTransporteAdapter.FromEntity(EquipoTransporteAdapter.FromDTO(vehiculoDto));
                    RespuestaGeneral = EquipoTransporteServicio.Modificar(Dtovehiculo, editarVehiculo);
                }
                else
                {
                    var utili = VehiculoUtilitarioAdapter.FromDTO(vehiculoDto);
                    var u = VehiculoUtilitarioServicio.Registrar(utili);
                    if (!u.Exito) return u;
                    else
                    {
                        vehiculoDto.IdVehiculoUtilitario = u.Id;
                        var Dtovehiculo = EquipoTransporteAdapter.FromEntity(EquipoTransporteAdapter.FromDTO(vehiculoDto));
                        var Mod = EquipoTransporteServicio.Modificar(Dtovehiculo);

                        if (Mod.Exito)
                            return EquipoTransporteServicio.BorrarVehiculoPorEdicion(vehiculo);
                        else
                            return VehiculoUtilitarioServicio.Borrar(u.Id);
                    }
                }
            }
            return RespuestaGeneral;
        }
        public RespuestaDto EliminaEquipoTransporte(int id)
        {
            var resp = PermisosServicio.PuedeEliminarPedido();
            if (!resp.Exito) return resp;

            var vehiculo = new EquipoTransporteDataAccess().Buscar(id);
            if (vehiculo == null) return EquipoTransporteServicio.NoExiste();

            RespuestaDto respuesta = new RespuestaDto();
            if (vehiculo.CCamioneta != null)
            {
                var cam = CamionetaAdapter.FromEntity(vehiculo.CCamioneta);
                cam.Activo = false;
                respuesta = CamionetaServicio.Modificar(cam);
            }
            if (vehiculo.CPipa != null)
            {
                var pip = PipaAdapter.FromEntity(vehiculo.CPipa);
                pip.Activo = false;
                respuesta = PipaServicio.Modificar(pip);
            }
            if (vehiculo.CUtilitario != null)
            {
                var uti = VehiculoUtilitarioAdapter.FromEntity(vehiculo.CUtilitario);
                uti.Activo = false;
                respuesta = VehiculoUtilitarioServicio.Modificar(uti);
            }
            return respuesta;
        }
        #region Asignaciones 
        public List<TransporteDTO> BuscarAsignaciones()
        {
            var pv = new PuntoVentaDataAccess().BuscarTodos().Where(x => x.UnidadesAlmacen.IdEstacionCarburacion == null).ToList();
            var asigutil = AsignacionUtilitarioServicio.Buscar();
            var asignaciones = TransporteAdapter.ToDTO(pv, asigutil);

            return asignaciones;
        }
        public RespuestaDto RegistraAsignacion(TransporteDTO vehiculoDto)
        {
            var resp = PermisosServicio.PuedeAsignarVehiculo();
            if (!resp.Exito) return resp;
            var idUtil = ListaEquiposdeTransporte(TokenServicio.ObtenerIdEmpresa()).SingleOrDefault(x => x.IdEquipoTransporte.Equals(vehiculoDto.IdVehiculo)).IdVehiculoUtilitario;
            var asig = AsignacionUtilitarioServicio.Buscar(vehiculoDto);
            if (asig != null) return AsignacionUtilitarioServicio.Existe();

            if (vehiculoDto.TipoVehiculo.Equals((short)TipoUnidadEqTransporteEnum.Utilitario))
            {
                var chof = OperadorChoferServicio.ObtenerPorUsuario(vehiculoDto.IdChofer);
                if (chof == null)
                {
                    var newChof = OperadorChoferServicio.CrearParaUtilitario();
                    newChof.IdUsuario = vehiculoDto.IdChofer;
                    var respChof = OperadorChoferServicio.Insertar(newChof);
                    if (!resp.Exito) return respChof;
                    else
                        vehiculoDto.IdChofer = respChof.Id;
                }
                else
                    vehiculoDto.IdChofer = chof.IdOperadorChofer;
                vehiculoDto.IdVehiculo = Convert.ToInt16(idUtil);
                var asignacion = EquipoTransporteServicio.GenerarAsignacion(vehiculoDto);
                return AsignacionUtilitarioServicio.Crear(asignacion);
            }
            else
            {
                RespuestaDto _respuesta = new RespuestaDto();
                if (vehiculoDto.TipoVehiculo.Equals((short)TipoUnidadEqTransporteEnum.Camioneta))
                {
                    var idCam = ListaEquiposdeTransporte(TokenServicio.ObtenerIdEmpresa()).SingleOrDefault(x => x.IdEquipoTransporte.Equals(vehiculoDto.IdVehiculo)).IdCamioneta;
                    var almacen = AlmacenGasServicio.ObtenerPorCamioneta(idCam ?? 0);
                    var chof = OperadorChoferServicio.ObtenerPorUsuario(vehiculoDto.IdChofer);
                    if (chof == null)
                    {
                        var newChof = OperadorChoferServicio.CrearParaCamioneta();
                        newChof.IdUsuario = vehiculoDto.IdChofer;
                        var respChof = OperadorChoferServicio.Insertar(newChof);
                        if (!resp.Exito) return respChof;
                        else
                            vehiculoDto.IdChofer = respChof.Id;
                    }
                    else
                        vehiculoDto.IdChofer = chof.IdOperadorChofer;
                    var npv = EquipoTransporteServicio.GenerarAsignacion(almacen, vehiculoDto);
                    _respuesta = PuntoVentaServicio.Insertar(npv);
                }
                if (vehiculoDto.TipoVehiculo.Equals((short)TipoUnidadEqTransporteEnum.Pipa))
                {
                    var idPip = ListaEquiposdeTransporte(TokenServicio.ObtenerIdEmpresa()).FirstOrDefault(x => x.IdEquipoTransporte.Equals(vehiculoDto.IdVehiculo)).IdPipa;
                    var almacen = AlmacenGasServicio.ObtenerPorPipa(idPip ?? 0);
                    var chof = OperadorChoferServicio.ObtenerPorUsuario(vehiculoDto.IdChofer);
                    if (chof == null)
                    {
                        var newChof = OperadorChoferServicio.CrearParaPipa();
                        newChof.IdUsuario = vehiculoDto.IdChofer;
                        var respChof = OperadorChoferServicio.Insertar(newChof);
                        if (!resp.Exito) return respChof;
                        else
                            vehiculoDto.IdChofer = respChof.Id;
                    }
                    else
                        vehiculoDto.IdChofer = chof.IdOperadorChofer;
                    var npv = EquipoTransporteServicio.GenerarAsignacion(almacen, vehiculoDto);
                    _respuesta = PuntoVentaServicio.Insertar(npv);
                }
                return _respuesta;
            }
        }
        public RespuestaDto EliminarAsignacion(TransporteDTO vehiculoDto)
        {
            var resp = PermisosServicio.PuedeBorrarAsignacionVehicular();
            if (!resp.Exito) return resp;


            if (vehiculoDto.TipoVehiculo == TipoUnidadEqTransporteEnum.Utilitario)
            {
                vehiculoDto.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
               // var IdEmp = ListaEquiposdeTransporte(TokenServicio.ObtenerIdEmpresa()).FirstOrDefault(x => (x.IdVehiculoUtilitario==vehiculoDto.IdVehiculo)).IdEmpresa;
                var asignacion = AsignacionUtilitarioServicio.Buscar(vehiculoDto);
                asignacion.Activo = false;

                return AsignacionUtilitarioServicio.Actualizar(AsignacionUtilitarioAdapter.FormEmtity(asignacion));
            }
            else
            {
                var almacen = AlmacenGasServicio.ObtenerUnidadAlamcenGas(vehiculoDto.IdVehiculo);
                var pv = PuntoVentaServicio.Obtener(almacen);
                var Entity = PuntoVentaAdapter.FromEntity(pv);
                Entity.Activo = false;
                return PuntoVentaServicio.Modificar(Entity);
            }
        }
        #endregion
        #endregion

        #region Tipo proveedor
        public List<TipoProveedorDTO> ListaTipoProveedor()
        {
            //var resp = PermisosServicio.PuedeConsultarTipoProveedor();
            //if (!resp.Exito) return null;

            return TipoProveedorAdapter.ToDTO(TipoProveedorServicio.Obtener());
        }
        #endregion

        #region Combustible
        public List<CombustibleDTO> ListaCombustible()
        {
            return CombustibleAdapter.toDTO(CombustibleServicio.Obtener());
        }
        public List<CombustibleDTO> ListaCombustible(short idempresa)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            return CombustibleAdapter.toDTO(CombustibleServicio.Obtener(idempresa));
        }
        public List<CombustibleDTO> ListaCombustible(short idEmpresa, string desc)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            return CombustibleAdapter.toDTO(CombustibleServicio.Obtener(idEmpresa, desc));
        }
        public CombustibleDTO CombustibleId(int idCombustible)
        {
            var resp = PermisosServicio.PuedeConsultarParqueVehicular();
            if (!resp.Exito) return null;

            return CombustibleAdapter.toDTO(CombustibleServicio.Obtener(idCombustible));
        }
        public RespuestaDto Registra(CombustibleDTO entidadDto)
        {
            var resp = PermisosServicio.PuedeRegistrarPedido();
            if (!resp.Exito) return resp;

            var ent = CombustibleAdapter.FromDTO(entidadDto);
            ent.Activo = true;
            if (!TokenServicio.EsSuperUsuario() && !TokenServicio.ObtenerEsAdministracionCentral())
                ent.Id_Empresa = TokenServicio.ObtenerIdEmpresa();

            return CombustibleServicio.Registrar(ent);
        }

        public RespuestaDto Modifica(CombustibleDTO vehiculoDto)
        {
            var resp = PermisosServicio.PuedeModificarPedido();
            if (!resp.Exito) return resp;

            var combustible = CombustibleServicio.Obtener(vehiculoDto.Id_Combustible);
            combustible.Activo = true;
            if (combustible == null) return CombustibleServicio.NoExiste();

            var Dtovehiculo = CombustibleAdapter.FromDto(vehiculoDto, combustible);
            return CombustibleServicio.Modificar(Dtovehiculo);
        }
        public RespuestaDto Eliminar(int id)
        {
            var resp = PermisosServicio.PuedeEliminarPedido();
            if (!resp.Exito) return resp;

            var combustible = CombustibleServicio.Obtener(id);
            if (combustible == null) return CombustibleServicio.NoExiste();

            var Dtovehiculo = CombustibleAdapter.FromEntity(combustible);

            return CombustibleServicio.Eliminar(Dtovehiculo);
        }

        #endregion

        #region TipoUnidad       
        public List<TipoUnidadDTO> ListaTipoUnidad()
        {
            List<TipoUnidadDTO> lst = new List<TipoUnidadDTO>();
            TipoUnidadDTO entidad = new TipoUnidadDTO();
            entidad.IdTipoUnidad = TipoUnidadEqTransporteEnum.Camioneta;
            entidad.TipoUnidad = STipoUnidad.Camioneta.ToString();
            TipoUnidadDTO entidad1 = new TipoUnidadDTO();
            entidad.IdTipoUnidad = TipoUnidadEqTransporteEnum.Camioneta;
            entidad.TipoUnidad = STipoUnidad.Camioneta.ToString();
            TipoUnidadDTO entidad2 = new TipoUnidadDTO();
            entidad.IdTipoUnidad = TipoUnidadEqTransporteEnum.Camioneta;
            entidad.TipoUnidad = STipoUnidad.Camioneta.ToString();

            lst.Add(entidad);
            lst.Add(entidad1);
            lst.Add(entidad2);
            return lst;
        }
        public List<TipoUnidadDTO> ListaTipoUnidad(short idempresa)
        {
            List<TipoUnidadDTO> lst = new List<TipoUnidadDTO>();
            TipoUnidadDTO entidad = new TipoUnidadDTO();
            entidad.IdTipoUnidad = TipoUnidadEqTransporteEnum.Camioneta;
            entidad.TipoUnidad = STipoUnidad.Camioneta.ToString();
            lst.Add(entidad);
            TipoUnidadDTO entidad1 = new TipoUnidadDTO();
            entidad1.IdTipoUnidad = TipoUnidadEqTransporteEnum.Pipa;
            entidad1.TipoUnidad = STipoUnidad.Pipa.ToString();
            lst.Add(entidad1);
            TipoUnidadDTO entidad2 = new TipoUnidadDTO();
            entidad2.IdTipoUnidad = TipoUnidadEqTransporteEnum.Utilitario;
            entidad2.TipoUnidad = STipoUnidad.Utilitario.ToString();
            lst.Add(entidad2);



            return lst;
        }

        #endregion

        #region Medidor Gas
        public List<DTOs.Mobile.MedidorDto> ObtenerMedidores()
        {
            return AdaptadoresDTO.Mobile.TipoMedidorAdapter.ToDto(TipoMedidorGasServicio.Obtener());
        }
        #endregion

        #region Banco
        public List<BancoDTO> ListaBanco()
        {
            //var resp = PermisosServicio.PuedeConsultarTipoProveedor();
            //if (!resp.Exito) return null;

            return BancoAdapter.ToDTO(BancoServicio.Obtener());
        }
        #endregion

        #region Forma de pago
        public List<FormaPagoDTO> ListaFormaPago()
        {
            //var resp = PermisosServicio.PuedeConsultarTipoProveedor();
            //if (!resp.Exito) return null;

            return FormaPagoAdapter.ToDTO(FormaPagoServicio.Obtener());
        }
        #endregion

        #region Metodo de pago
        public List<MetodoPagoDTO> ListaMetodosPago()
        {
            var metodos = MetodoPagoServicio.Buscar();
            return MetodoPagoAdapter.ToDTO(metodos);
        }
        #endregion

        #region UsoCFDI
        public List<UsoCFDIDTO> ListaUsoCFDI()
        {
            var usosCfdi = UsoCFDIServicio.Buscar();
            return UsoCFDIAdapter.ToDTO(usosCfdi);
        }
        #endregion

        #region Egreso
        public RespuestaDto CrearEgrereo(EgresoDTO dto)
        {
            var resp = PermisosServicio.PuedeRegistrarCliente();
            if (!resp.Exito) return resp;

            dto.IdEmpresa = TokenServicio.ObtenerIdEmpresa();
            var entidad = EgresoAdapter.FromDTO(dto);
            entidad.FechaRegistro = DateTime.Now;

            return EgresoServicio.Registrar(entidad);          
        }
        public RespuestaDto ModificarEgrereo(EgresoDTO dto)
        {
            var resp = PermisosServicio.PuedeModificarCliente();
            if (!resp.Exito) return resp;

            var entidad = EgresoAdapter.FromDTO(dto);

            return EgresoServicio.Actualizar(entidad);
        }
        public RespuestaDto BorrarEgrereo(int id)
        {
            var entidad = EgresoServicio.Buscar(id);

            var Emtity = EgresoAdapter.FormEmtity(entidad);
            Emtity.Activo = false;

            return EgresoServicio.Actualizar(entidad);
        }
        public EgresoDTO BuscarEgreso(int id)
        {
            return EgresoAdapter.ToDTO(EgresoServicio.Buscar(id));
        }
        public List<EgresoDTO> BuscarEgresos()
        {
            return EgresoAdapter.ToDTO(EgresoServicio.BuscarTodos(TokenServicio.ObtenerIdEmpresa()));
        }
        #endregion
    }
}
