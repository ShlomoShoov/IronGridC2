using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Producer.Models;
using Producer.Services;

namespace Producer.Orchestrators
{
    public class ProducingOrchestrator
    {
        private KafkaProducerService _kafkaProducer;
        private IReportLoader _reportLoader;
        private IIRonGridLogger _logger;
        private string _serviceName = "Producing Orchestrator";

        public ProducingOrchestrator(KafkaProducerService kafkaProducer, IReportLoader reportLoader, IIRonGridLogger logger)
        {
            _kafkaProducer = kafkaProducer;
            _reportLoader = reportLoader;
            _logger = logger;
        }
        public async Task Run()
        {
            var reports = _reportLoader.LoadReports();
            _logger.Debug(_serviceName, "Starting Loading and sending Reports");
            while(reports.MoveNext())
            {
                await _kafkaProducer.ProduceReport(reports.Current);
            }
            _kafkaProducer.Dispose();
            _logger.Debug(_serviceName, "finished Loading and sending Reports");



        }

    }
}