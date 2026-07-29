using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Resto_Gest
{
  public static class ArchivoJson
  {
        //guadar cualquier lista en un archivo json
        public static void Guardar<T>(string rutaArchivo, List<T> datos)
        {
            try
            {
                var opciones = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(datos, opciones);
                File.WriteAllText(rutaArchivo, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en JSON: {ex.Message}");
            }
        }
        //Cargar datos desde un archivo JSON
        public static List<T> Cargar<T>(string rutaArchivo)
        {
            try
            {
                if (!File.Exists(rutaArchivo))
                {
                    return new List<T>();
                }
                string jsonString = File.ReadAllText(rutaArchivo);
                return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar JSON: {ex.Message}");
                return new List<T>();
            }
        }
  }
}