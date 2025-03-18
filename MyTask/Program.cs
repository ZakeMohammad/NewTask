using MyTask;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

string geolocationApiUrl = "https://ipinfo.io/json";
static async Task<(DateTime sunriseUtc, DateTime sunsetUtc)> GetSunTimes(string latitude, string longitude)
{

    try
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://api.sunrise-sunset.org/json?lat={latitude}&lng={longitude}&formatted=0";
            string response = await client.GetStringAsync(apiUrl);
            var json = JsonDocument.Parse(response);
            var results = json.RootElement.GetProperty("results");

            DateTime sunriseUtc = DateTime.Parse(results.GetProperty("sunrise").GetString());
            DateTime sunsetUtc = DateTime.Parse(results.GetProperty("sunset").GetString());

            return (sunriseUtc, sunsetUtc);
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error : {ex.Message}");
        return (DateTime.UtcNow.AddHours(3), DateTime.UtcNow.AddHours(3));

    }


}
static clsDates CalculateTimeCategories(DateTime sunriseUtc, DateTime sunsetUtc)
{
    DateTime earlyMorningStart = sunriseUtc.Date.AddHours(5);
    DateTime earlyMorningEnd = sunriseUtc;

    DateTime sunriseStart = sunriseUtc;
    DateTime sunriseEnd = sunriseUtc.AddMinutes(30);


    DateTime morningStart = sunriseUtc.AddMinutes(30);
    DateTime morningEnd = sunriseUtc.Date.AddHours(12);

    DateTime middayStart = sunriseUtc.Date.AddHours(12);
    DateTime middayEnd = middayStart.AddMinutes(59);

    DateTime afternoonStart = middayEnd.AddMinutes(1);
    DateTime afternoonEnd = sunriseUtc.Date.AddHours(16);

    DateTime eveningStart = sunriseUtc.Date.AddHours(16);
    DateTime eveningEnd = sunsetUtc;

    DateTime sunsetStart = sunsetUtc;
    DateTime sunsetEnd = sunsetUtc.AddMinutes(30);

    DateTime nightStart = sunsetUtc.AddMinutes(30);
    DateTime nightEnd = sunriseUtc.AddDays(1);

    clsDates dates = new clsDates()
    {
        earlyMorningStart = earlyMorningStart,
        earlyMorningEnd = earlyMorningEnd,
        sunriseStart = sunriseStart,
        sunriseEnd = sunriseEnd,
        morningStart = morningStart,
        morningEnd = morningEnd,
       noonStart = middayStart,
       noonEnd = middayEnd,
        eveningStart = eveningStart,
        eveningEnd = eveningEnd,
        sunsetStart = sunsetStart,
        sunsetEnd = sunsetEnd,
        nightStart = nightStart,
        nightEnd = nightEnd
    };

    return dates;
}

static string SelectWallpaper(clsDates dates)
{
    DateTime currentTime = dates.CurrentDate;

    if (currentTime >= dates.nightStart || currentTime < dates.earlyMorningStart)
    {
        return "night.png";
    }
    else if (currentTime >= dates.sunriseStart && currentTime < dates.sunriseEnd)
    {
        return "sunrise.png";
    }
    else if (currentTime >= dates.morningStart && currentTime < dates.morningEnd)
    {
        return "morning.png";
    }
    else if (currentTime >= dates.noonStart && currentTime < dates.noonEnd)
    {
        return "noon.png";
    }
    else if (currentTime >= dates.eveningStart && currentTime < dates.eveningEnd)
    {
        return "evening.png";
    }
    else if (currentTime >= dates.sunsetStart && currentTime < dates.sunsetEnd)
    {
        return "sunset.png";
    }
    else
    {
        return "night.png";
    }
}

static void ChangeWallpaper(string wallpaper)
{
    try
    {
        string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string imageDirectory = Path.Combine(projectDirectory, "Images");
        string wallpaperPath = Path.Combine(imageDirectory, wallpaper);   
         WallpaperSetter.SetWallpaper(wallpaperPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error changing wallpaper: {ex.Message}");
    }
}

Console.Clear();

string Answer ="Yes";

while (Answer.ToLower() == "yes")
{
Console.WriteLine("*****************************************************************");
Console.WriteLine("\t\t Welcome to my littel task ");
Console.WriteLine("*****************************************************************");


Console.WriteLine("\n\nWhat is your latitude?");
Console.Write("Answer:");
string latitude = Console.ReadLine().ToString();
Console.WriteLine("\nWhat is your longitude ?");
Console.Write("Answer:");
string longitude = Console.ReadLine().ToString();


var (sunriseUtc, sunsetUtc) = await GetSunTimes(latitude, longitude);

clsDates Dates = CalculateTimeCategories(sunriseUtc, sunsetUtc);
string wallpaper = SelectWallpaper(Dates);
ChangeWallpaper(wallpaper);

Console.WriteLine("\n*****************************************************************");
Console.WriteLine($"\t\t Image: {wallpaper} ");
Console.WriteLine("*****************************************************************");


Console.WriteLine("\n\nYou whant to change the Latitude and Longitude ?  (Yes/No)");
Console.Write("Answer:");
Answer = Console.ReadLine().ToString();
 Console.Clear();

}
Console.Clear();

Console.WriteLine("\n*****************************************************************");
Console.WriteLine("\t\t Thanks Maqsam for this opportunity ;)");
Console.WriteLine("\n*****************************************************************");
