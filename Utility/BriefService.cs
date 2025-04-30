using Microsoft.EntityFrameworkCore;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Utility
{
    public class BriefService
    {
        private readonly ViAppContext _context;

        public BriefService(ViAppContext context)
        {
            _context = context;
        }
       
        public async Task AddBriefAsync(Brief brief)
        {
            // Guarda un brief nuevo 
            _context.Brief.Add(brief);
            await _context.SaveChangesAsync();
        }

        public async Task<Brief> GetBriefAsync(int briefid)
        {
            // Recupera los "briefs" con el id para ver detalles
            var brief = await _context.Brief.FirstOrDefaultAsync(b => b.BriefId == briefid);
            return brief!;
        }




       



       public async Task UpdateBriefAsync(Brief brief)
       {
        //Actualiza un brief
                _context.Brief.Update(brief);
        await _context.SaveChangesAsync();
        }





        public async Task AddPiezaAsync(Pieza pieza)
        {
            // Guarda una pieza nueva 
            _context.Pieza.Add(pieza);
            await _context.SaveChangesAsync();
        }       

        public async Task<List<Pieza>?> GetPiezasAsync(int briefid)
        {
            // Recupera todas las "piezas" con el id especificado
            var piezas = await _context.Pieza
                .Where(p => p.BriefId == briefid)
                .ToListAsync();

            // Verifica si piezas es nulo o está vacío
            if (piezas == null || !piezas.Any())
            {
                return null;
            }

            return piezas;
        }

        public async Task<Pieza> GetPiezaToDeleteAsync(int briefid, int numPieza)
        {
            var pieza = await _context.Pieza
                .Where(p => p.BriefId == briefid && p.NumeroPieza == numPieza)

                .FirstOrDefaultAsync();

            return pieza!;
        }

        public async Task<Pieza> GetPiezaToEditAsync(int briefid, int numPieza)
        {
            var pieza = await _context.Pieza
                .Where(p => p.BriefId == briefid && p.NumeroPieza == numPieza)

                .FirstOrDefaultAsync();

            return pieza!;
        }

        public async Task DeletePiezaAsync(Pieza pieza)
        {
            if (pieza != null)
            {
                // Marca la entidad como eliminada
                _context.Pieza.Remove(pieza);

                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePiezaAsync(Pieza pieza)
        {
            if (pieza != null)
            {
                //// Marca la entidad como editada
                //_context.Pieza.Update(pieza);
                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
        }

       
        // Genera el consecutivo

        public async Task<string> GenerateConsecutivoAsync()
        {
            // Obtén el último consecutivo ordenado de forma descendente
            var ultimoConsecutivo = await _context.Brief
                .OrderByDescending(b => b.Consecutivo)
                .FirstOrDefaultAsync();

            if (ultimoConsecutivo != null)
            {
                // Obtén el último número del consecutivo sin sufijos
                var ultimoConsecutivoSinSufijo = ultimoConsecutivo.Consecutivo!.Split("_")[0];

                if (ultimoConsecutivoSinSufijo.StartsWith("BF-"))
                {
                    // Obtén el último número del consecutivo y aumenta uno
                    int ultimoNumero = int.Parse(ultimoConsecutivoSinSufijo.Split("-")[1]);
                    ultimoNumero++;

                    // Formatea el nuevo consecutivo
                    string nuevoConsecutivo = $"BF-{ultimoNumero}";

                    // Verifica si ya existe un consecutivo igual y en caso afirmativo, genera uno único
                    while (await _context.Brief.AnyAsync(b => b.Consecutivo == nuevoConsecutivo))
                    {
                        ultimoNumero++;
                        nuevoConsecutivo = $"BF-{ultimoNumero}";
                    }

                    return nuevoConsecutivo; // D2 asegura que siempre tenga dos dígitos
                }
            }

            // Si no hay consecutivos anteriores, crea el primero
            return "BF-1";
        }

        public async Task<Brief> GetBriefDetailsAsync(int id)
        {
            // Recupera los "briefs" con el id para ver detalles
            var brief = await _context.Brief.FirstOrDefaultAsync(b => b.BriefId == id);
            return brief!;
        }

        public async Task<Brief> GetBriefForRecotzacionAsync(int id)
        {
            // Recupera los "briefs" con el id para ver detalles
            var brief = await _context.Brief.FirstOrDefaultAsync(b => b.BriefId == id);
            return brief!;
        }

        public async Task<List<Brief>> GetBriefsPendientesAsync(string? userId)
        {
            // Recupera los "briefs" pendientes
            return await _context.Brief.Where(b => b.UserId == userId && b.StatusCostos == BriefStatus.Pendiente || b.StatusDesign == BriefStatus.Pendiente)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }


        public async Task<List<Brief>> GetBriefsEntregadosAsync(string userId)
        {
            // Recupera los "briefs" entregados
            return await _context.Brief.Where(b => b.UserId == userId && b.StatusCostos == BriefStatus.Entregado || b.StatusDesign == BriefStatus.Entregado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }



        public async Task<List<Brief>> GetBriefsEntregadosAsync()
        {
            // Recupera los briefs aceptados"
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Entregado || b.StatusDesign == BriefStatus.Entregado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }


        public async Task<List<Brief>> GetBriefsByUserIdAsync(string userId)
        {
            // Recupera los "briefs" asociados al usuario por su UserId
            return await _context.Brief.Where(b => b.UserId == userId && b.EsRecotizacion == false)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsRechazadosAsync(string userId)
        {
            // Recupera los "briefs" rechazados
            return await _context.Brief.Where(b => b.UserId == userId && b.StatusCostos == BriefStatus.Rechazado || b.StatusDesign == BriefStatus.Rechazado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }
        public async Task<List<Brief>> GetBriefsAprobadosAsync(string userId)
        {
            // Recupera los "briefs" aprobados por su userId
            return await _context.Brief.Where(b => b.UserId == userId && b.StatusCostos == BriefStatus.Aceptado || b.StatusDesign == BriefStatus.Aceptado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }


        //editar

        public async Task<List<Brief>> GetBriefsCanceladosAsync(string userId)
        {
            // Recupera los "briefs" cancelados
            return await _context.Brief.Where(b => b.UserId == userId && b.StatusAsesor==BriefStatus.Cancelado || b.StatusDesign == BriefStatus.Cancelado || b.StatusCostos == BriefStatus.Cancelado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }






        public async Task<List<Brief>> GetBriefsRecotizadosAsync()
        {
       // Recupera los "briefs" recotizados
            return await _context.Brief.Where(b => b.EsRecotizacion == true)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsCostosSearchAsync(string Search)
        {
            // Recupera los "briefs" con el campo StatusCostos en pendiente
            return await _context.Brief
                .Where(e => e.Consecutivo!.Contains(Search))
                .Where(b => b.StatusCostos == BriefStatus.Pendiente && b.AsignadoCostos == false)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetCostosBriefsAsync()
        {
            // Recupera los "briefs" con el campo StatusCostos en pendiente
            return await _context.Brief
                .Where(b => (b.StatusDesign == BriefStatus.SinDiseño || b.StatusDesign == BriefStatus.Aceptado) && (b.AsignadoCostos == false && b.StatusCostos != BriefStatus.Aceptado && b.StatusCostos != BriefStatus.SinCostos))
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsAsignadosCostosAsync(string fullname)
        {
            // Recupera los "briefs" asignados por el costeador
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Pendiente && b.CosteadorAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsAsignadosDesignAsync(string fullname)
        {
            // Recupera los "briefs" por userId con el campo StatusDesign en asignado
            return await _context.Brief.Where(b => b.StatusDesign == BriefStatus.Pendiente && b.DesignerAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetCostosBriefsCheckedAsync(string fullname)
        {
            // Recupera los "briefs" por userId cuando el StatusCostos es aceptado
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Aceptado && b.CosteadorAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetCostosEntregadosAsync(string fullname)
        {
            // Recupera los "briefs" por userId cuando el StatusCostos es realizado
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Entregado && b.CosteadorAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetDesignEntregadoAsync(string fullname)
        {
            // Recupera los "briefs" por userId cuando el StatusCostos es realizado
            return await _context.Brief.Where(b => b.StatusDesign == BriefStatus.Entregado && b.DesignerAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsReasignadosCostosAsync(string Null)
        {
            // Recupera los "briefs" por el userId cuando el StatusCostos es reasignado
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Pendiente && b.CosteadorAsignado == Null)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsRechazadosCostosAsync(string fullname)
        {
            // Recupera los "briefs" por el userId cuando el StatusCostos es rechazado
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Rechazado && b.CosteadorAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }




        public async Task<List<Brief>> GetDesignBriefsAsync()
        {
            // Recupera los "briefs" con el campo Design en True
            return await _context.Brief.Where(b => b.Design == true && b.StatusDesign == BriefStatus.Pendiente && b.AsignadoDesign == false)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetDesignBriefsCheckedAsync(string fullname)
        {
            // Recupera los "briefs" por su userId cuando el StatusDesign es realizado
            return await _context.Brief.Where(b => b.StatusDesign == BriefStatus.Entregado && b.DesignerAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetDesignBriefsAceptadosAsync(string fullname)
        {
            // Recupera los "briefs" por su userId cuando el StatusDesign es realizado
            return await _context.Brief.Where(b => b.StatusDesign == BriefStatus.Aceptado && b.DesignerAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsRechazadosDesignAsync(string fullname)
        {
            // Recupera los "briefs" por su userId cuando el StatusCostos es rechazado
            return await _context.Brief.Where(b => b.StatusDesign == BriefStatus.Rechazado && b.DesignerAsignado == fullname)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetMontajesAsync(string userId)
        {
            // Recupera los "briefs" de montajes por userId
            return await _context.Brief.Where(b => b.UserId == userId && b.TipoCotizacion == "Montaje")
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetLicitacionesAsync(string userId)
        {
            // Recupera los "briefs" de licitaciones por userId
            return await _context.Brief.Where(b => b.UserId == userId && b.TipoCotizacion == "Licitacion")
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetRecotizacionesAsync(string userId)
        {
            // Recupera los briefs recotizados"
            return await _context.Brief.Where(b => b.UserId == userId && b.EsRecotizacion == true)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetNuevaCotizacionAsync()
        {
            // Recupera los "briefs" de montajes
            return await _context.Brief.Where(b => b.TipoCotizacion == "NuevaCotizacion")
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }

        public async Task<List<Brief>> GetBriefsAprobadosAsync()
        {
            // Recupera los briefs aceptados"
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Aceptado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }
        public async Task<List<Brief>> GetBriefsRechazadosAsync()
        {
            // Recupera los briefs rechazados"
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Rechazado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }




        //editar
        public async Task<List<Brief>> GetBriefsCanceladosAsync()
        {
            // Recupera los briefs cancelados"
            return await _context.Brief.Where(b => b.StatusAsesor == BriefStatus.Cancelado || b.StatusDesign == BriefStatus.Cancelado || b.StatusCostos == BriefStatus.Cancelado)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }







        public async Task<List<Brief>> GetBriefsPendientesAsync()
        {
            // Recupera los briefs pendientes"
            return await _context.Brief.Where(b => b.StatusCostos == BriefStatus.Pendiente)
                .OrderByDescending(b => b.CreateDate)
                .ToListAsync();
        }



        public async Task<List<Product>> GetProductsAsync()
        {
            // Recupera los productos de la base de datos"
            return await _context.Product.ToListAsync();
        }

        public async Task AddNewProductAsync(Product newProduct)
        {
            // Agrega un nuevo producto
            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Empaque>> GetEmpaquesAsync()
        {
            // Recupera los empaques de la base de datos"
            return await _context.Empaque.ToListAsync();
        }

        public async Task AddNewEmpaqueAsync(Empaque newEmpaque)
        {
            // Agrega un nuevo empaque
            _context.Empaque.Add(newEmpaque);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LugarEntrega>> GetLugarEntregaAsync()
        {
            // Recupera los lugares de entrega de la base de datos"
            return await _context.LugarEntrega.ToListAsync();
        }

      


        public async Task AddNewLugarEntregaAsync(LugarEntrega newLugarentrega)
        {
            // Agrega un nuevo empaque
            _context.LugarEntrega.Add(newLugarentrega);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> GetClientsAsync()
        {
            // Recupera los productos de la base de datos"
            return await _context.Cliente.ToListAsync();
        }

        public async Task AddNewClientAsync(Cliente newClient)
        {
            // Agrega un nuevo cliente
            _context.Cliente.Add(newClient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Pieza>> GetPiezasByBriefIdAsync(int briefId)
        {
            // consultar las piezas asociadas al Brief específico y filtra por el briefId
            var piezas = await _context.Pieza
                .Where(p => p.BriefId == briefId)
                .ToListAsync();

            return piezas;
        }

        public int ContarCotizaciones()
        {
            // Realiza la consulta a la base de datos para contar los registros de cotizaciones
            return _context.Brief.Count();
        }

        internal Task<bool> CanceladosBriefAsync(int briefId)
        {
            throw new NotImplementedException();
        }

        internal Task<IEnumerable<Brief>?> GetBriefsAsignadosCostosAsync(List<string> userFullNames)
        {
            throw new NotImplementedException();
        }

        internal Task<IEnumerable<Brief>?> GetAllBriefsAsignadosCostosAsync()
        {
            throw new NotImplementedException();
        }

        internal Task<IEnumerable<Brief>?> GetBriefsReasignadosCostosAsync(object @null)
        {
            throw new NotImplementedException();
        }
    }
}