using System.Text.Json;
using Hosta_Hotel.Entities;

namespace Hosta_Hotel.Infrastructure;

public class HotelDataWrapper
{
    public List<Room> Rooms { get; set; } = new();
    public List<Client> Clients { get; set; } = new();
    public List<Administrator> Admins { get; set; } = new();
    public List<Cleaner> Cleaners { get; set; } = new();
    public List<Reservation> Reservations { get; set; } = new();
    
    // Setari de timp
    public TimeSpan CheckInStart { get; set; }
    public TimeSpan CheckOutLimit { get; set; }
}

public class PersistenceManager
{
    private const string FilePath = "hotel_data.json";

    public void SaveData(HotelDataWrapper data)
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(data, options);
            File.WriteAllText(FilePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CRITICAL] Eroare la salvarea datelor: {ex.Message}");
        }
    }

    public HotelDataWrapper LoadData()
    {
        if (!File.Exists(FilePath))
        {
            return new HotelDataWrapper();
        }

        try
        {
            string jsonString = File.ReadAllText(FilePath);
            var data = JsonSerializer.Deserialize<HotelDataWrapper>(jsonString);
            return data ?? new HotelDataWrapper();
        }
        catch (Exception ex)
        {
            // Daca baza de date e corupta, facem backup
            try
            {
                string backupPath = $"{FilePath}.backup_corrupt_{DateTime.Now.Ticks}.bak";
                File.Copy(FilePath, backupPath, true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ATENTIE!!! Baza de date este corupta. Backup salvat: {backupPath}");
                Console.ResetColor();
            }
            catch 
            {
                // In caz ca nu reuseste sa faca backup, ignoram ca sa nu crape aplicatia
            }
            
            return new HotelDataWrapper();
        }
    }
}