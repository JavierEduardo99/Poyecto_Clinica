
using System;
using System.Collections.Generic;

namespace ClinicaConsola
{
    // MODELO: Clases que representan la estructura de datos
    class Paciente
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Telefono { get; set; }
    }

    class Doctor
    {
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
    }

    class Cita
    {
        public Paciente Paciente { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Fecha { get; set; }
    }

    // SERVICIOS: Capa que contiene la lógica del negocio
    class ClinicaService
    {
        private List<Paciente> pacientes = new List<Paciente>();
        private List<Doctor> doctores = new List<Doctor>();
        private List<Cita> citas = new List<Cita>();

        public void RegistrarPaciente(string nombre, int edad, string telefono)
        {
            Paciente paciente = new Paciente
            {
                Nombre = nombre,
                Edad = edad,
                Telefono = telefono
            };
            pacientes.Add(paciente);
        }

        public void RegistrarDoctor(string nombre, string especialidad)
        {
            Doctor doctor = new Doctor
            {
                Nombre = nombre,
                Especialidad = especialidad
            };
            doctores.Add(doctor);
        }

        public List<Paciente> ObtenerPacientes()
        {
            return pacientes;
        }

        public List<Doctor> ObtenerDoctores()
        {
            return doctores;
        }

        public void AgendarCita(Paciente paciente, Doctor doctor, DateTime fecha)
        {
            Cita cita = new Cita
            {
                Paciente = paciente,
                Doctor = doctor,
                Fecha = fecha
            };
            citas.Add(cita);
        }

        public List<Cita> ObtenerCitas()
        {
            return citas;
        }
    }

    // CAPA DE PRESENTACIÓN: Interacción con el usuario
    class ClinicaUI
    {
        private ClinicaService clinicaService = new ClinicaService();

        public void MostrarMenu()
        {
            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Gestión de Clínica ===");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Registrar Doctor");
                Console.WriteLine("3. Agendar Cita");
                Console.WriteLine("4. Ver Citas");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarPaciente();
                        break;
                    case 2:
                        RegistrarDoctor();
                        break;
                    case 3:
                        AgendarCita();
                        break;
                    case 4:
                        MostrarCitas();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
            } while (opcion != 5);
        }

        private void RegistrarPaciente()
        {
            Console.Write("Ingrese el nombre del paciente: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la edad del paciente: ");
            int edad = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el teléfono del paciente: ");
            string telefono = Console.ReadLine();

            clinicaService.RegistrarPaciente(nombre, edad, telefono);
            Console.WriteLine("Paciente registrado exitosamente.");
        }

        private void RegistrarDoctor()
        {
            Console.Write("Ingrese el nombre del doctor: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la especialidad del doctor: ");
            string especialidad = Console.ReadLine();

            clinicaService.RegistrarDoctor(nombre, especialidad);
            Console.WriteLine("Doctor registrado exitosamente.");
        }

        private void AgendarCita()
        {
            var pacientes = clinicaService.ObtenerPacientes();
            var doctores = clinicaService.ObtenerDoctores();

            if (pacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            if (doctores.Count == 0)
            {
                Console.WriteLine("No hay doctores registrados.");
                return;
            }

            Console.WriteLine("Seleccione un paciente:");
            for (int i = 0; i < pacientes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pacientes[i].Nombre}");
            }
            int seleccionPaciente = int.Parse(Console.ReadLine()) - 1;

            Console.WriteLine("Seleccione un doctor:");
            for (int i = 0; i < doctores.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {doctores[i].Nombre} ({doctores[i].Especialidad})");
            }
            int seleccionDoctor = int.Parse(Console.ReadLine()) - 1;

            Console.Write("Ingrese la fecha de la cita (dd/mm/aaaa): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            clinicaService.AgendarCita(pacientes[seleccionPaciente], doctores[seleccionDoctor], fecha);
            Console.WriteLine("Cita agendada exitosamente.");
        }

        private void MostrarCitas()
        {
            var citas = clinicaService.ObtenerCitas();

            if (citas.Count == 0)
            {
                Console.WriteLine("No hay citas agendadas.");
                return;
            }

            foreach (var cita in citas)
            {
                Console.WriteLine($"Paciente: {cita.Paciente.Nombre} | Doctor: {cita.Doctor.Nombre} ({cita.Doctor.Especialidad}) | Fecha: {cita.Fecha.ToShortDateString()}");
            }
        }
    }

    // PROGRAMA PRINCIPAL
    class Program
    {
        static void Main(string[] args)
        {
            ClinicaUI ui = new ClinicaUI();
            ui.MostrarMenu();
        }
    }
}

