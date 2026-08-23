using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Schema;
using System.Threading.Tasks;
using Producer.Models;

namespace Producer.Services
{
    public class JsonReportLoader : IReportLoader
    {
        private List<Report> _reports;
        private IIRonGridLogger _logger;
        private string _serviceName = "json loader";

        public JsonReportLoader(string path, IIRonGridLogger logger)
        {
            _logger = logger;
            _reports = LoadFromJson(path);

        }

        public IEnumerator<Report> LoadReports()
        {
            foreach(Report report in _reports)
            {
                yield return report;
            }
        }


        private List<Report> LoadFromJson(string path)
        {
            List<Report> reports = [];
            if (!Path.Exists(path))
            {
                _logger.Error(_serviceName, $"File not exists! {path}");
                return reports;
            }
            string rawText = File.ReadAllText(path);
            if (string.IsNullOrEmpty(rawText))
            {
                _logger.Debug(_serviceName, $"File in {path} is empty!");
                return reports;
            }
            try
            {
                List<Report>? convertingReports = JsonSerializer.Deserialize<List<Report>>(rawText)!;
                if (convertingReports == null)
                {
                    _logger.Error(_serviceName, $"error in converting json file on path: {path}");
                    return reports;
                }
                return convertingReports;
            }
            catch(JsonException ex)
            {
                _logger.Error(_serviceName, $"error in converting json file: {ex.Message}");
                return reports;
            }
            
        }
    }
}