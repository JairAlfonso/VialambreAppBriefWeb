using Microsoft.AspNetCore.Identity;
using VialambreAppTest1.Models;

namespace VialambreAppTest1.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Crear roles si no existen
            string[] roles = { "Admin", "AdminUser", "Costos", "Design", "Asesor", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Crear usuarios default con roles
            await CreateUserWithRole(userManager, "admin1@gmail.com", "password", "Admin", "Admin", "1", "1049626556", "3197885370");
            await CreateUserWithRole(userManager, "admin2@gmail.com", "password", "Admin", "Admin", "2", "1049626557", "3197885371");
            await CreateUserWithRole(userManager, "admin3@gmail.com", "password", "AdminUser", "AdminUser", "3", "1049626558", "3197885375");
            await CreateUserWithRole(userManager, "asesor1@gmail.com", "password", "Asesor", "Asesor", "1", "1049666558", "3102223344");
            await CreateUserWithRole(userManager, "asesor2@gmail.com", "password", "Asesor", "Asesor", "2", "1049666559", "3102223345");
            await CreateUserWithRole(userManager, "designer1@gmail.com", "password", "Design", "Design", "1", "1049555666", "3103334452");
            await CreateUserWithRole(userManager, "designer2@gmail.com", "password", "Design", "Design", "2", "1049555667", "3103334453");
            await CreateUserWithRole(userManager, "costos1@gmail.com", "password", "Costos", "Costos", "1", "1049444580", "3107778856");
            await CreateUserWithRole(userManager, "costos2@gmail.com", "password", "Costos", "Costos", "2", "1049444581", "3107778857");
        }

        private static async Task CreateUserWithRole(UserManager<AppUser> userManager, string email, string password, string roleName, string firstName, string lastName, string document, string phoneNumber)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                var user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    Password = password,
                    RolName = roleName,
                    FirstName = firstName,
                    LastName = lastName,
                    Document = document,
                    PhoneNumber = phoneNumber,
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }

        public static async Task InitializeAsync(ViAppContext context)
        {
            context.Database.EnsureCreated();

            if (context.Product.Any())
            {
                return;
            }
            else
            {
                var accesorio = new Product
                {
                    Name = "Accesorio",
                };

                var adecuacion = new Product
                {
                    Name = "Adecuación",
                };

                var aviso = new Product
                {
                    Name = "Aviso",
                };

                var barracocina = new Product
                {
                    Name = "Barra cocina",
                };

                var barraenu = new Product
                {
                    Name = "Barra en U",
                };

                var barraespecial = new Product
                {
                    Name = "Barra especial",
                };

                var barrarecta = new Product
                {
                    Name = "Barra recta",
                };

                var baseespecial = new Product
                {
                    Name = "Base especial",
                };

                var basemaniqui = new Product
                {
                    Name = "Base maniquí",
                };

                var botadero = new Product
                {
                    Name = "Botadero",
                };

                var botiquin = new Product
                {
                    Name = "Botiquín",
                };

                var brazo = new Product
                {
                    Name = "Brazo",
                };

                var butaco = new Product
                {
                    Name = "Butaco",
                };

                var buzon = new Product
                {
                    Name = "Buzón",
                };

                var cabezote = new Product
                {
                    Name = "Cabezote",
                };

                var canastaautoservicio = new Product
                {
                    Name = "Canasta autoservicio",
                };

                var canastaalambre = new Product
                {
                    Name = "Canasta en alambre",
                };

                var canastaplastica = new Product
                {
                    Name = "Canasta plastica",
                };

                var carretilla = new Product
                {
                    Name = "Carretilla",
                };

                var carroautoservicio = new Product
                {
                    Name = "Carro autoservicio",
                };

                var carroespecial = new Product
                {
                    Name = "Carro especial",
                };

                var carrotransportador = new Product
                {
                    Name = "Carro transportador",
                };

                var cercoalambre = new Product
                {
                    Name = "Cerco en alambre",
                };

                var closet = new Product
                {
                    Name = "Closet",
                };

                var counter = new Product
                {
                    Name = "Counter",
                };

                var cremallera = new Product
                {
                    Name = "Cremallera",
                };

                var dispensador = new Product
                {
                    Name = "Dispensador",
                };

                var entrepanoespecial = new Product
                {
                    Name = "Entrepaño especial",
                };

                var entrepanoestanteriapesada = new Product
                {
                    Name = "Entrepaño estanteria pesada",
                };

                var entrepanogondola = new Product
                {
                    Name = "Entrepaño góndola",
                };

                var escalera = new Product
                {
                    Name = "Escalera",
                };

                var escritorio = new Product
                {
                    Name = "Escritorio",
                };

                var estanteria = new Product
                {
                    Name = "Estantería",
                };

                var estanteriaespecial = new Product
                {
                    Name = "Estanteria especial",
                };

                var estanterialiviana = new Product
                {
                    Name = "Estanteria liviana",
                };

                var estanteriapesada = new Product
                {
                    Name = "Estanteria pesada",
                };

                var estanteriasemipesada = new Product
                {
                    Name = "Estanteria semi pesada",
                };

                var exhibidoraereo = new Product
                {
                    Name = "Exhibidor aereo",
                };

                var exhibidorpared = new Product
                {
                    Name = "Exhibidor de pared",
                };

                var exhibidorpiso = new Product
                {
                    Name = "Exhibidor de Piso",
                };

                var exhibidoracrilico = new Product
                {
                    Name = "Exhibidor en acrílico",
                };

                var exhibidorespecial = new Product
                {
                    Name = "Exhibidor especial",
                };

                var exhibidormostrador = new Product
                {
                    Name = "Exhibidor mostrador",
                };

                var flauta = new Product
                {
                    Name = "Flauta",
                };

                var forro = new Product
                {
                    Name = "Forro",
                };

                var ganchera = new Product
                {
                    Name = "Ganchera",
                };

                var gancherapared = new Product
                {
                    Name = "Ganchera de pared",
                };

                var gancherapiso = new Product
                {
                    Name = "Ganchera de piso",
                };

                var gancheramostrador = new Product
                {
                    Name = "Ganchera mostrador",
                };

                var ganchoespecial = new Product
                {
                    Name = "Gancho especial",
                };

                var glorificador = new Product
                {
                    Name = "Glorificador",
                };

                var gondola = new Product
                {
                    Name = "Góndola",
                };

                var isla = new Product
                {
                    Name = "Isla",
                };

                var laminainterline = new Product
                {
                    Name = "Lámina interline",
                };

                var laminaperforada = new Product
                {
                    Name = "Lámina perforada",
                };

                var lateralpuntadegondola = new Product
                {
                    Name = "Lateral punta de góndola",
                };

                var locker = new Product
                {
                    Name = "Locker",
                };

                var mallaalambre = new Product
                {
                    Name = "Malla en alámbre",
                };

                var mallamarcotubo = new Product
                {
                    Name = "Malla marco en tubo",
                };

                var mamut = new Product
                {
                    Name = "Mamut",
                };

                var maniqui = new Product
                {
                    Name = "Maniquí",
                };

                var mesaespecial = new Product
                {
                    Name = "Mesa especial",
                };

                var muebleespecial = new Product
                {
                    Name = "Mueble especial",
                };

                var organizador = new Product
                {
                    Name = "Organizador",
                };

                var panelmadera = new Product
                {
                    Name = "Panel en madera",
                };

                var papelera = new Product
                {
                    Name = "Papelera",
                };

                var paral = new Product
                {
                    Name = "Paral",
                };

                var portarollo = new Product
                {
                    Name = "Porta rollo",
                };

                var publicidadimpresion = new Product
                {
                    Name = "Publicidad e impresión",
                };

                var puntodepago = new Product
                {
                    Name = "Punto de pago",
                };

                var rack = new Product
                {
                    Name = "Rack",
                };

                var rackespecial = new Product
                {
                    Name = "Rack especial",
                };

                var rackinterline = new Product
                {
                    Name = "Rack interline",
                };

                var rackmadeflex = new Product
                {
                    Name = "Rack madeflex",
                };

                var rackmalla = new Product
                {
                    Name = "Rack malla",
                };

                var recepcion = new Product
                {
                    Name = "Recepción",
                };

                var recubrimientocolumna = new Product
                {
                    Name = "Recubrimiento de columna",
                };

                var rejillaestanteria = new Product
                {
                    Name = "Rejilla estantería ",
                };

                var ristra = new Product
                {
                    Name = "Ristra",
                };

                var ristrapp = new Product
                {
                    Name = "Ristra en pp",
                };

                var ristrametalica = new Product
                {
                    Name = "Ristra metálica",
                };

                var senalizador = new Product
                {
                    Name = "Señalizador",
                };

                var separadaor = new Product
                {
                    Name = "Separador",
                };

                var sillaespecial = new Product
                {
                    Name = "Silla especial",
                };

                var tarima = new Product
                {
                    Name = "Tarima",
                };

                var transporte = new Product
                {
                    Name = "Transporte",
                };

                var vigaestanteriapesada = new Product
                {
                    Name = "Viga estantería pesada",
                };

                var vitrina = new Product
                {
                    Name = "Vitrina",
                };

                var vitrinalateral = new Product
                {
                    Name = "Vitrina lateral",
                };

                var vitrinamostrador = new Product
                {
                    Name = "Vitrina mostrador",
                };

                var products = new Product[]
                {
                    accesorio, adecuacion, aviso, barracocina, barraenu, barraespecial, barrarecta, baseespecial, basemaniqui, botadero, botiquin, brazo, butaco, buzon, cabezote, canastaautoservicio, canastaalambre, canastaplastica, carretilla, carroautoservicio, carroespecial, carrotransportador, cercoalambre, closet, counter, cremallera, dispensador, entrepanoespecial, entrepanoestanteriapesada, entrepanogondola, escalera, escritorio, estanteria, estanteriaespecial, estanterialiviana, estanteriapesada, estanteriasemipesada, exhibidoraereo, exhibidorpared, exhibidorpiso, exhibidoracrilico, exhibidorespecial, exhibidormostrador, flauta, forro, ganchera, gancherapared, gancherapiso, gancheramostrador, ganchoespecial, glorificador, gondola, isla, laminainterline, laminaperforada, lateralpuntadegondola, locker, mallaalambre, mallamarcotubo, mamut, maniqui, mesaespecial, muebleespecial, organizador, panelmadera, papelera, paral, portarollo, publicidadimpresion, puntodepago, rack, rackespecial, rackinterline, rackmadeflex, rackmalla, recepcion, recubrimientocolumna, rejillaestanteria, ristra, ristrapp, ristrametalica, senalizador, separadaor, sillaespecial, tarima, transporte, vigaestanteriapesada, vitrina, vitrinalateral, vitrinamostrador

                };

                await context.Product.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            if (context.Empaque.Any())
            {
                return;
            }
            else
            {
                var pemp1 = new Empaque
                {
                    Name = "PEMP 1: Empaque refuerzos en cartón corrugado y vinipelado",
                };

                var pemp2 = new Empaque
                {
                    Name = "PEMP 2: Empaque forrado en cartón corrugado y vinipelado",
                };

                var pemp3 = new Empaque
                {
                    Name = "PEMP 3: Empaque forrado en cartón corrugado, protectores en esquinas, vinipelado y con zuncho",
                };

                var pemp4 = new Empaque
                {
                    Name = "PEMP 4: Empaque en caja corrugada, cierre con zuncho plástico",
                };

                var pemp5 = new Empaque
                {
                    Name = "PEMP 5: Empaque en caja corrugada, cierre con cinta de empaque",
                };

                var pemp6 = new Empaque
                {
                    Name = "PEMP 6: Empaque inicial en vinipel o bolsa, caja corrugada, cierre con cinta de empaque y zuncho plástico",
                };

                var pemp7 = new Empaque
                {
                    Name = "PEMP 7: Empaque inicial en vinipel o bolsa, caja corrugada tipo exportación, cierre con cinta de empaque y zuncho plástico",
                };

                var pemp8 = new Empaque
                {
                    Name = "PEMP 8: Empaque inicial en vinipel, refuerzos en cartón corrugado, embalado tipo guacal, sellado con grapa y con zuncho",
                };

                var pemp9 = new Empaque
                {
                    Name = "PEMP 9: Producto delicado, empaque inicial en plástico burbuja, caja corrugada, cierre con cinta de empque y zuncho plástico",
                };

                var empaques = new Empaque[]
                {
                pemp1, pemp2, pemp3, pemp4, pemp5, pemp6, pemp7, pemp8, pemp9
                };

                await context.Empaque.AddRangeAsync(empaques);
                await context.SaveChangesAsync();
            }

            if (context.LugarEntrega.Any())
            {
                return;
            }
            else
            {
                var puntoventa = new LugarEntrega
                {
                    Name = "Punto de venta",
                };

                var operadorlogistico = new LugarEntrega
                {
                    Name = "Operador logístico",
                };

                var bodegacliente = new LugarEntrega
                {
                    Name = "Bodega del cliente",
                };

                var distnacional = new LugarEntrega
                {
                    Name = "Distribuidor nacional",
                };

                var lugaresEntrega = new LugarEntrega[]
                {
                puntoventa, operadorlogistico, bodegacliente, distnacional
                };

                await context.LugarEntrega.AddRangeAsync(lugaresEntrega);
                await context.SaveChangesAsync();
            }

        }
    }
}
