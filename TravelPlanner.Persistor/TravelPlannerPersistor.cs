using Newtonsoft.Json;

namespace TravelPlanner.Persistor
{
    public class TravelPlannerPersistor
    {
        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "travel_planner.data");
        public async Task Save(AppState data)
        {
            if (data is null)
            {
                File.Delete(_filePath);
                return;
            }

            try
            {
                string json = JsonConvert.SerializeObject(data);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch { }
        }
        public async Task<AppState?> Load()
        {
            try
            {
                AppState? data = null;
                if (File.Exists(_filePath))
                {
                    string json = await File.ReadAllTextAsync(_filePath);
                    data = JsonConvert.DeserializeObject<AppState>(json);
                }
                return data;
            }
            catch
            {
                return null;
            }
        }
    }
}
