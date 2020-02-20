using System;
using System.Collections.Generic;
using IngresoBodega.Controlador;
using IngresoBodega.Modelos;

namespace IngresoBodega
{
    class Program
    {
        static void Main(string[] args)
        {
            #region VARIABLES
            ConexionSql ConSql = new ConexionSql();
            var AplicacionActiva = true;
            #endregion

            while (AplicacionActiva)
            {
                var opcion = MenuInicial();
                switch (opcion)
                {
                    case 9:
                        AplicacionActiva = false;
                        break;
                    case 1:
                        RegistrarIngreso();
                        break;
                    case 2:
                        RegistrarSalida();
                        break;
                    case 3:
                        IngresarNuevo();
                        break;
                    case 4:
                        MostrarFuncionario();
                        break;
                    default:
                        Console.WriteLine("Opción no valida");
                        Console.ReadLine();
                        break;
                }
            }
            Console.WriteLine("Programa finalizado");
            Console.ReadLine();
        }

        public static int MenuInicial()
        {
            Console.Clear();

            Console.WriteLine("**** -- Bienvenido a la bodega de infraestructura de ChevyPlan -- ****");
            Console.WriteLine("\n Ingrese una opción \n");
            Console.WriteLine("1. Registrar ingreso");
            Console.WriteLine("2. Registrar salida");
            Console.WriteLine("3. Registrar nuevo funcionario");
            Console.WriteLine("4. Mostrar información de funcionario");
            Console.WriteLine("\n **** ----------------------------------------------------------- ****");

            try
            {
                var opcion = Convert.ToInt32(Console.ReadLine());
                return opcion;
            }
            catch (Exception)
            {
                Console.Clear();
                return 0;
            }
        }

        public static void IngresarNuevo()
        {
            ConexionSql ConSql = new ConexionSql();

            Console.Clear();
            Console.WriteLine("*** -- INGRESAR NUEVO FUNCIONARIO -- ***");
            Console.WriteLine("\n Ingrese documento de identidad \n");
            var DocumentoIdentidad = Console.ReadLine();

            //CONSULTAR EXISTENCIA
            var existeFuncionario = ConSql.validarFuncionario(DocumentoIdentidad, "funcionario");

            if (existeFuncionario)
            {
                //INSERTAR EN TABLA DE AUTORIZACIONES
                var resultadoInsertar = ConSql.Insertar(DocumentoIdentidad);

                if (resultadoInsertar)
                {
                    Console.WriteLine("Se ha autorizado el ingreso al funcionario");
                }
                else
                {
                    Console.WriteLine("No se logró autorizar el ingreso al funcionario");
                }
            }
            else
            {
                Console.WriteLine("Funcionario no localizado");
            }
            PulseTeclaClear();
        }

        public static void RegistrarIngreso()
        {
            ConexionSql ConSql = new ConexionSql();

            Console.Clear();
            Console.WriteLine("*** -- REGISTRAR INGRESO -- ***");
            Console.WriteLine("\n Ingrese documento de identidad \n");
            var DocumentoIdentidad = Console.ReadLine();

            //CONSULTA PARA INGRESAR
            var autorizacionIngreso = ConSql.validarFuncionario(DocumentoIdentidad, "autorizados");
            if (autorizacionIngreso)
            {
                //INGRESO AUTORIZADO
                Console.WriteLine("Ingreso autorizado");
                var registroInOut = ConSql.RegistrarInOut("in", DocumentoIdentidad);

                if (registroInOut)
                {
                    Console.WriteLine("Ingreso registrado");
                }
                else
                {
                    Console.WriteLine("Error al registrar ingreso");
                }
            }
            else
            {
                //Rechazar ingreso
                Console.WriteLine("Ingreso rechazado");
            }
            PulseTeclaClear();
        }

        public static void RegistrarSalida()
        {
            ConexionSql ConSql = new ConexionSql();
            Console.Clear();
            Console.WriteLine("*** -- REGISTRAR SALIDA -- ***");
            Console.WriteLine("\n Ingrese documento de identidad \n");
            var DocumentoIdentidad = Console.ReadLine();

            var registroInOut = ConSql.RegistrarInOut("out", DocumentoIdentidad);

            if (registroInOut)
            {
                Console.WriteLine("Salida registrada");
            }
            else
            {
                Console.WriteLine("Error al registrar salida");
            }
            PulseTeclaClear();
        }

        public static void MostrarFuncionario()
        {
            ConexionSql ConSql = new ConexionSql();
            List<Funcionarios> lstFuncionarios = new List<Funcionarios>();

            Console.Clear();
            Console.WriteLine("*** -- VISTA DE FUNCIONARIO -- ***");
            Console.WriteLine("\n Ingrese documento de identidad \n");
            var DocumentoIdentidad = Console.ReadLine();

            //MOSTRAR
            lstFuncionarios = ConSql.Mostrar(DocumentoIdentidad);

            foreach (var item in lstFuncionarios)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Nombre: " + item.fun_nombre);
                Console.WriteLine("Segundo nombre: " + item.fun_segundo_nombre);
                Console.WriteLine("Apellidos: " + item.fun_apellidos);
                Console.WriteLine("Número documento: " + item.fun_documento);
                Console.WriteLine("Imagen de perfil: " + item.fun_img_perfil);
                Console.WriteLine("Contacto 1: " + item.fun_contacto1);
                Console.WriteLine("Contacto 2: " + item.fun_contacto2);
                Console.WriteLine("Contacto 3: " + item.fun_contacto3);
                Console.WriteLine("Correo: " + item.fun_correo);
            }

            Console.WriteLine("\n");
            PulseTeclaClear();
        }

        private static void PulseTeclaClear()
        {
            Console.WriteLine("Pulse una tecla para continuar");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
